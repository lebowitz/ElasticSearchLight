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
    public class Program
    {
        static void Main()
        {
            HostFactory.Run(x =>
                {
                    x.Service<ElasticSearchLight>(s =>
                    {
                        s.ConstructUsing(name => new ElasticSearchLight());
                        s.WhenStarted(tc => tc.Start());
                        s.WhenStopped(tc => tc.Stop());
                    });
                    x.RunAsLocalSystem();
                    x.StartAutomatically();
                    x.SetDescription("ElasticSearchLight");
                    x.SetDisplayName("ElasticSearchLight");
                    x.SetServiceName("ElasticSearchLight");
                });
        }
    }
}
