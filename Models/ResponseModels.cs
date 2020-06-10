// ////////////////////////////////////////////////////
// StartTime:      2020/6/10 9:47
// FileName:       ResponseModels.cs
// Author:           psyduck007@outlook.com
// Purpose:         Lazy dog does not write purpose
// TODO:
// ////////////////////////////////////////////////////
//
//
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace DBAPrometheusAPI.Models
{
    /// <summary>
    /// 查询的结果
    /// </summary>
    public class PrometheusExpressionInstantQueryResult
    {
        public Dictionary<string, string> Metric { get; set; }
        public List<JsonElement> Value { get; set; }
    }
    public class PrometheusExpressionRangeQueryResult
    {
        public Dictionary<string, string> Metric { get; set; }
        public List<List<JsonElement>> Value { get; set; }
    }
    /// <summary>
    /// 查询的结果及查询结果的类型
    /// </summary>
    public class PrometheusExpressionQueryContent<T>
    {
        public string ResultType { get; set; }
        public List<T> Result { get; set; }
    }
    /// <summary>
    /// Prometheus服务查询的返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PrometheusQueryResponse<T>
    {
        public string Status { get; set; }
        public T Data { get; set; }
    }
}
