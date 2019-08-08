using System;
using System.Activities;
using System.ComponentModel;
using Twilio.Rest.Api.V2010.Account;

namespace RO.Twilio
{
	public class DeleteMessage : TwilioBase
	{
		[RequiredArgument]
		[Category("Input")]
		public InArgument<string> Sid
		{
			get;
			set;
		}

		protected override void Execute(NativeActivityContext context)
		{
			try
			{
				Init(context);

				var flag = MessageResource.Delete(Sid.Get(context));
				
                Sucess.Set(context, flag);
				ErrorMessage.Set(context, flag ? "" : "Erro ao excluir mensagem.");
			}
			catch (Exception ex)
			{
				Sucess.Set(context, value: false);
				ErrorMessage.Set(context, ex.Message);
			}
		}
	}
}
