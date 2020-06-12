// ////////////////////////////////////////////////////
// StartTime:      2020/6/10 23:05
// FileName:       Settings.cs
// Author:           psyduck007@outlook.com
// Purpose:         Lazy dog does not write purpose
// TODO:
// ////////////////////////////////////////////////////
//
//
using System;
namespace DBAPrometheusAdapter.Models
{
    public class HostPort
    {
        /// <summary>
        /// Host 主机
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// Port 端口
        /// </summary>
        public int    Port { get; set; }
    }
    public class PrometheusConfig : HostPort
    {
        /// <summary>
        /// HTTP API, a typical example: /api/v1 or something specified
        /// </summary>
        public string APIPath { get; set; }
    }
}
