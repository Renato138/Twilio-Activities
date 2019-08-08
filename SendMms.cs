using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace RO.Twilio
{
	public class SendMms : SendTwilioMessage
	{
		[Category("Input")]
        public InArgument<string> MediaUrl
		{
			get;
			set;
		}

		protected override void SendMessage(NativeActivityContext context)
		{
			var number = base.From.Get(context);
			var number2 = base.To.Get(context);
			var text = base.Body.Get(context);
			var list = new List<Uri>(new Uri[] { new Uri(MediaUrl.Get(context)) });
			var from = number;
			var body = text;
			var mediaUrl = list;

			MessageResource messageResource = MessageResource.Create(number2, null, from, null, body, mediaUrl);
			
            Sid.Set(context, messageResource.Sid);
			Sucess.Set(context, value: true);
		}
	}
}
