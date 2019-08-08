using System.Activities;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace RO.Twilio
{
	public class SendSms : SendTwilioMessage
	{
		protected override void SendMessage(NativeActivityContext context)
		{
			var number = base.From.Get(context);
			var number2 = base.To.Get(context);
			var text = base.Body.Get(context);
			PhoneNumber from = number;
			var body = text;

			var messageResource = MessageResource.Create(number2, null, from, null, body);

			Sid.Set(context, messageResource.Sid);
			Sucess.Set(context, value: true);
		}
	}
}
