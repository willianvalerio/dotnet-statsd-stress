using System;
using System.Diagnostics;
using StatsdClient;
using System.Threading;

namespace dotnet_statsd_stress
{
    class Program
    {
        static void Main(string[] args)
        {

            var dogstatsdConfig = new StatsdConfig
            {
                StatsdServerName = "127.0.0.1",
                StatsdPort = 8125,
            };
            using (var service = new DogStatsdService()){
                service.Configure(dogstatsdConfig);

                var watch = System.Diagnostics.Stopwatch.StartNew();

                while(watch.Elapsed.TotalSeconds < 60)
                {
                    service.Increment("custom.increment.stress", tags: new[] { "statds_enabled:true" });
                    //Console.WriteLine("teste: " + watch.ElapsedMilliseconds);
                    Thread.Sleep(1000);

                }
                // the code that you want to measure comes here
                watch.Stop();
                
                Console.WriteLine("Time: " + watch.ElapsedMilliseconds);
                service.Gauge("stress.totaltime", watch.ElapsedMilliseconds, tags: new[] { "statds_enabled:true" });
                //DogStatsd.Dispose(); // Flush all metrics not yet sent

            }

           
            

        }
    }
}
