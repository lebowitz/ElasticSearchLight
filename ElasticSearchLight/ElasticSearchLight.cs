using Nest;
using SharpHue;
using System;
using System.Drawing;
using System.Linq;

namespace ElasticSearchLight
{
    public class Program
    {
        private static LightCollection _lights;

        public static void Main()
        {
            var client =new ElasticClient(new ConnectionSettings(new Uri(Environment.GetEnvironmentVariable("ES_LIGHT_URL"))));
            Configuration.Initialize("newdeveloper");
            LightService.Discover();
            _lights = new LightCollection();            

            var health = client.Health(HealthLevel.Cluster);
            
            string status = health.Status;
            
            Color esColor = Color.FromName(status);
            
            if (status == "green")
            {
                esColor = Color.DarkGreen;
            }

            if (health.RelocatingShards > 0)
            {
                esColor = Color.Purple;
            }

            SetAllLights(esColor);
        }

        private static void SetAllLights(Color c)
        {
            var ls = (new LightStateBuilder());
            ls.Color(c);                       
            _lights.ToList().ForEach(l => l.SetState(ls));
        }
    }
}