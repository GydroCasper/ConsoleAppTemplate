using System;
using System.Threading.Tasks;
using ConsoleAppTemplate.Abstractions;
using Microsoft.Extensions.Logging;

namespace ConsoleAppTemplate.Code
{
    public class App: IRun
    {
        private readonly ILogger<App> _logger;

        public App(ILogger<App> logger)
        {
            _logger = logger;
        }

        public async Task Run()
        {
            try
            {

            }
            catch (Exception e)
            {
                _logger.LogCritical($"{e.Message}\r\n{e.StackTrace}");
            }
        }
    }
}