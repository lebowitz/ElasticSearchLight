using Newtonsoft.Json.Linq;
using SharpHue;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElasticSearchLight
{
    public class Program
    {        
        static System.Drawing.Color current = Color.Snow;        
        static Uri esClusterHealthUrl = new Uri(string.Format(@"{0}/_cluster/health", System.Environment.GetEnvironmentVariable("ES_URL")));        
        static SharpHue.LightCollection _lights;

        public static void Main()
        {            
            SharpHue.Configuration.Initialize("newdeveloper");
            SharpHue.LightService.Discover();
            _lights = new LightCollection();            
            var b = new WebClient();
            string status = JObject.Parse(b.DownloadString(esClusterHealthUrl))["status"].ToString();
            _lights.ToList().ForEach(l => l.SetState((new LightStateBuilder()).Color(Color.FromName(status))));
        }
    }
}
