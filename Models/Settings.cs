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
        public string Host { get; set; }
        public int    Port { get; set; }
    }
    public class PrometheusConfig : HostPort
    {
        public string APIPath { get; set; }
    }
}
