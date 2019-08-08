using System;
using System.Activities;
using System.ComponentModel;
using Twilio.Rest.Api.V2010.Account;

namespace RO.Twilio
{
	public class Fetch : TwilioBase
	{
		[RequiredArgument]
		[Category("Input")]
		public InArgument<string> Sid
		{
			get;
			set;
		}

		[Category("Output")]
		[DisplayName("Date Created")]
		public OutArgument<DateTime?> DateCreated
		{
			get;
			set;
		}

		[Category("Output")]
		[DisplayName("Date Sent")]
		public OutArgument<DateTime?> DateSent
		{
			get;
			set;
		}

		[Category("Output")]
		public OutArgument<string> From
		{
			get;
			set;
		}

		[Category("Output")]
		public OutArgument<string> To
		{
			get;
			set;
		}

		[Category("Output")]
		public OutArgument<string> Body
		{
			get;
			set;
		}

		[Category("Output")]
		public OutArgument<string> Status
		{
			get;
			set;
		}

		[Category("Output")]
		public OutArgument<string> Direction
		{
			get;
			set;
		}

		protected override void Execute(NativeActivityContext context)
		{
			try
			{
				Init(context);

				var messageResource = MessageResource.Fetch(Sid.Get(context));

				Sucess.Set(context,true);
				DateCreated.Set(context, messageResource.DateCreated);
				DateSent.Set(context, messageResource.DateSent);
				Direction.Set(context, messageResource.Direction.ToString());
				From.Set(context, messageResource.From);
				Status.Set(context, messageResource.Status.ToString());
				To.Set(context, messageResource.To);
				Body.Set(context, messageResource.Body);
			}
			catch (Exception ex)
			{
				Sucess.Set(context, value: false);
				ErrorMessage.Set(context, ex.Message);
			}
		}
	}
}
