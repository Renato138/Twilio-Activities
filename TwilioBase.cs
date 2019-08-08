using System.Activities;
using System.ComponentModel;
using Twilio;

namespace RO.Twilio
{
	public abstract class TwilioBase : NativeActivity
	{
		[RequiredArgument]
		[Category("Input")]
		public InArgument<string> AccountSid
		{
			get;
			set;
		}

		[RequiredArgument]
		[Category("Input")]
		public InArgument<string> AuthToken
		{
			get;
			set;
		}

		[Category("Output")]
		public OutArgument<bool> Sucess
		{
			get;
			set;
		}

		[Category("Output")]
		public OutArgument<string> ErrorMessage
		{
			get;
			set;
		}

		public TwilioBase()
		{
		}

		protected void Init(NativeActivityContext context)
		{
			string username = AccountSid.Get(context);
			string password = AuthToken.Get(context);

			TwilioClient.Init(username, password);
		}
	}
}
