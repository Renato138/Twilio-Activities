using System;
using System.Activities;
using System.ComponentModel;
using System.Data;
using Twilio.Base;
using Twilio.Rest.Api.V2010.Account;

namespace RO.Twilio
{
	public class ReadMessages : TwilioBase
	{
		[Category("Input")]
		public InArgument<string> From
		{
			get;
			set;
		}

		[Category("Input")]
		public InArgument<string> To
		{
			get;
			set;
		}

		[Category("Input")]
		public InArgument<DateTime?> DateSentBefore
		{
			get;
			set;
		}

		[Category("Input")]
		public InArgument<DateTime?> DateSent
		{
			get;
			set;
		}

		[Category("Input")]
		public InArgument<DateTime?> DateSentAfter
		{
			get;
			set;
		}

		[Category("Input")]
		public InArgument<int> Limit
		{
			get;
			set;
		}

		[Category("Output")]
		public OutArgument<DataTable> Result
		{
			get;
			set;
		}

		public ReadMessages()
		{
			Limit = 10;
		}

		protected override void Execute(NativeActivityContext context)
		{
			try
			{
				Init(context);

				var text = From.Get(context);
				var text2 = To.Get(context);
				var dateSentBefore = DateSentBefore.Get(context);
				var dateSent = DateSent.Get(context);
				var dateSentAfter = DateSentAfter.Get(context);
				var num = Limit.Get(context);

				ReadMessageOptions readMessageOptions = new ReadMessageOptions();

				if (!string.IsNullOrWhiteSpace(text))
				{
					readMessageOptions.From = text;
				}
				if (!string.IsNullOrWhiteSpace(text2))
				{
					readMessageOptions.To = text2;
				}

				readMessageOptions.DateSentBefore = dateSentBefore;
				readMessageOptions.DateSent = dateSent;
				readMessageOptions.DateSentAfter = dateSentAfter;
				readMessageOptions.Limit = ((num < 1) ? 1 : num);

				ResourceSet<MessageResource> resourceSet = MessageResource.Read(readMessageOptions);

				var dataTable = CriarDataTable();

				foreach (MessageResource item in resourceSet)
				{
					dataTable.Rows.Add(item.DateCreated, item.DateUpdated, item.DateSent, item.Direction.ToString(), item.ErrorCode, item.ErrorMessage, item.From, item.Sid, item.Status.ToString(), item.To, item.Body);
				}
				Sucess.Set(context, value: true);
				Result.Set(context, dataTable);
			}
			catch (Exception ex)
			{
				Sucess.Set(context, value: false);
				ErrorMessage.Set(context, ex.Message);
			}
		}

		private DataTable CriarDataTable()
		{
			return new DataTable
			{
				Columns = 
				{
					{
						"DateCreated",
						typeof(DateTime)
					},
					{
						"DateUpdated",
						typeof(DateTime)
					},
					{
						"DateSent",
						typeof(DateTime)
					},
					{
						"Direction",
						typeof(string)
					},
					{
						"ErrorCode",
						typeof(int)
					},
					{
						"ErrorMessage",
						typeof(string)
					},
					{
						"From",
						typeof(string)
					},
					{
						"Sid",
						typeof(string)
					},
					{
						"Status",
						typeof(string)
					},
					{
						"To",
						typeof(string)
					},
					{
						"Body",
						typeof(string)
					}
				}
			};
		}
	}
}
