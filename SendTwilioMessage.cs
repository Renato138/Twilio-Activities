using System;
using System.Activities;
using System.ComponentModel;

namespace RO.Twilio
{
	public abstract class SendTwilioMessage : TwilioBase
	{
		[Category("Input")]
		[RequiredArgument]
		public InArgument<string> From
		{
			get;
			set;
		}

		[Category("Input")]
		[RequiredArgument]
		public InArgument<string> To
		{
			get;
			set;
		}

		[Category("Input")]
		[RequiredArgument]
		public InArgument<string> Body
		{
			get;
			set;
		}

		[Category("Output")]
		public OutArgument<string> Sid
		{
			get;
			set;
		}

		protected abstract void SendMessage(NativeActivityContext context);

		protected override void Execute(NativeActivityContext context)
		{
			try
			{
				Init(context);
				SendMessage(context);
			}
			catch (Exception ex)
			{
				Sucess.Set(context, value: false);
                ErrorMessage.Set(context, ex.Message);
			}
		}
	}
}
