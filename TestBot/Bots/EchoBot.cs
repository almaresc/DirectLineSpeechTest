// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.15.2

using Microsoft.ApplicationInsights;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TestBot.Bots
{
    public class EchoBot : ActivityHandler
    {
        TelemetryClient _client;
        public EchoBot(TelemetryClient client)
        {
            _client = client;
        }
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {


            Dictionary<string, string> props = new Dictionary<string, string>();

            //props.Add("Turncontext", JsonConvert.SerializeObject(turnContext.));
            props.Add("Activity", JsonConvert.SerializeObject(turnContext.Activity));

            _client.TrackEvent("OnMessageActivityAsync", props);

            var replyText = $"Echo: {turnContext.Activity.Text}";
            await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }

        protected override Task OnConversationUpdateActivityAsync(ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            Dictionary<string, string> props = new Dictionary<string, string>();

            //props.Add("Turncontext", JsonConvert.SerializeObject(turnContext.));
            props.Add("Activity", JsonConvert.SerializeObject(turnContext.Activity));

            _client.TrackEvent("OnConversationUpdateActivityAsync", props);

            return base.OnConversationUpdateActivityAsync(turnContext, cancellationToken);
        }

    }
}
