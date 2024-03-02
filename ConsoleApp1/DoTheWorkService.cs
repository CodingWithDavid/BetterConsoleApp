using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class DoTheWorkService : IDoTheWorkService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<DoTheWorkService> _log;

        public DoTheWorkService(ILogger<DoTheWorkService> log, IConfiguration config)
        {
            _config = config;
            _log = log;

        }

        public void Run()
        {
            for (int i = 0; i < _config.GetValue<int>("LoopTimes"); i++)
            {
                _log.LogInformation("Hello {i}", i);
            }
        }
    }
}
