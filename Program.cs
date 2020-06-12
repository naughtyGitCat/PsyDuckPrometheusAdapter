// ////////////////////////////////////////////////////
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
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text;
using DBAPrometheusAdapter.Models;

namespace DBAPrometheusAdapter
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
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        /// <summary>
        /// Initialize with ILoggerFactory
        /// <inheritdoc cref="IPrometheusAdapter.FetchPrometheusMonitorData(IEnumerable{KeyValuePair{string, string}})"/>
        /// </summary>
        /// <param name="config"></param>
        /// <param name="httpClient"></param>
        /// <param name="logger"></param>
        public PrometheusAdapter(PrometheusConfig config, HttpClient httpClient, ILogger logger)
        {
            this._logger = logger;
            this._httpClient = httpClient;
            this._httpClient.BaseAddress = new Uri(new Uri($"http://{config.Host}:{config.Port}"), config.APIPath);
            _logger.LogDebug($"{this.GetType()} initialized");
        }
        /// <summary>
        /// Initialize with ILoggerFactory
        /// <inheritdoc cref="IPrometheusAdapter.FetchPrometheusMonitorData(IEnumerable{KeyValuePair{string, string}})"/>
        /// </summary>
        /// <param name="config"></param>
        /// <param name="httpClient"></param>
        /// <param name="loggerFactory"></param>
        public PrometheusAdapter(PrometheusConfig config, HttpClient httpClient, ILoggerFactory loggerFactory)
        {
            this._logger = loggerFactory.CreateLogger<PrometheusAdapter>();
            this._httpClient = httpClient;
            this._httpClient.BaseAddress = new Uri(new Uri($"http://{config.Host}:{config.Port}"), config.APIPath);
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
