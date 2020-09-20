using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Quotes.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Quotes.Tasks
{

    public class RemoveQuotesWorker : IHostedService
    {
        private Timer _timer;

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(call, null, 0, 300000); 

            return Task.CompletedTask;
        }

        private void call(object state)
        {
            QuotesController.QuotesList.RemoveAll(p => DateTime.Now.Subtract(p.CreationTime).TotalMinutes > 60);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
