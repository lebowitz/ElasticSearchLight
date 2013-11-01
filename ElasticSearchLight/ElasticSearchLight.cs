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
using Topshelf;
using Topshelf.Configurators;

namespace ElasticSearchLight
{
    public class ElasticSearchLight
    {
        readonly System.Timers.Timer _timer;
        static System.Drawing.Color current = Color.Snow;
        static string bl = @"buildlight.exe";
        Uri esClusterHealthUrl = new Uri(string.Format(@"{0}/_cluster/health", System.Environment.GetEnvironmentVariable("ES_URL")));
        int pollIntervalInSec = 1;
        SharpHue.LightCollection _lights;

        public ElasticSearchLight()
        {            
            SharpHue.Configuration.Initialize("newdeveloper");
            SharpHue.LightService.Discover();
            _lights = new LightCollection();

            _timer = new System.Timers.Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) =>
            {
                var b = new WebClient();
                string status = JObject.Parse(b.DownloadString(esClusterHealthUrl))["status"].ToString();
                Set(status);
            };
        }

        private void Set(string color)
        {
            var next = Color.FromName(color);
            if (current != next)
            {
                Process.Start(bl, "AllColors Off");
                Thread.Sleep(100);
                current = next;
                _lights.ToList().ForEach(l => l.SetState((new LightStateBuilder()).Color(next)));
                string args = string.Format("{0} On", next.Name.ToString());
                Process.Start(bl, args);
            }
        }

        public void Start() { _timer.Start(); }
        public void Stop() { _timer.Stop(); }
    }
}
