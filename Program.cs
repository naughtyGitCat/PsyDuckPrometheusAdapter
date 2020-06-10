﻿// ////////////////////////////////////////////////////
// StartTime:      2020/6/10 13:20
// FileName:       Program.cs
// Author:           psyduck007@outlook.com
// Purpose:         Lazy dog does not write purpose
// TODO:
// ////////////////////////////////////////////////////
//
//
using System;
using DBACommonPackage.Utils;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using DBAPrometheusAPI.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text;

namespace DBAPrometheusAPI
{
    /// <summary>
    /// Promethues操作服务
    /// </summary>
    public interface IPrometheusAdapter
    {
        /// <summary>
        /// 传入查询的类,返回正在进行的查询
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public Task<PrometheusQueryResponse<object>> FetchPrometheusMonitorData(IEnumerable<KeyValuePair<string, string>> requestData);
    }
    public class PrometheusAdapter : IPrometheusAdapter
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PrometheusAdapter> _logger;
        public PrometheusAdapter(string promtheusAPIUri, HttpClient httpClient, ILogger<PrometheusAdapter> logger)
        {
            this._logger = logger;
            this._httpClient = httpClient;
            this._httpClient.BaseAddress = new Uri(new Uri(promtheusAPIUri), "api/v1/");
            _logger.LogDebug($"{this.GetType()} initialized");
        }

        public async Task<PrometheusQueryResponse<object>> FetchPrometheusMonitorData(IEnumerable<KeyValuePair<string, string>> requestData)
        {
            _logger.LogDebug($"Recieved requestData: {JsonSerializer.Serialize(requestData)}");
            var resp = await _httpClient.PostAsync("query", new FormUrlEncodedContent(requestData));
            if (resp.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Post ${resp.RequestMessage.RequestUri} failed,code: {resp.StatusCode}");
            }
            var rawBytes = await resp.Content.ReadAsByteArrayAsync();
            _logger.LogDebug($"Response content rawBytes to string: ${Encoding.UTF8.GetString(rawBytes)}");
            return JsonSerializer.Deserialize<PrometheusQueryResponse<object>>(rawBytes, JsonUtil.DeSerializerOptions);
        }
    }
}