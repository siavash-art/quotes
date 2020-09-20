using Microsoft.Extensions.Hosting;
using MimeKit;
using Quotes.Controllers;
using Quotes.Models;
using SocialApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Quotes.Tasks
{
    public class SendQuotesWorker : IHostedService
    {
        private Timer timer;

        public async Task Send(List<SubscriberModel> subscribers, List<QuoteModel> quotes)
        {
            Email emailService = new Email();
            var mails = subscribers.Select(p => new MailboxAddress(p.Email)).ToList();
            await emailService.SendEmailAsync(mails, "Quotes", string.Join('\n', quotes.Select(p => p.Quote)));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(Canecellation, null, 0, 86400000);
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public async void Canecellation(object state)
        {
            var subscribers = QuotesController.subscribersList;
            var quotes = QuotesController.QuotesList;
            if (subscribers.Count > 0 && quotes.Count > 0)
                await Send(subscribers, quotes);
        }
    }
}
