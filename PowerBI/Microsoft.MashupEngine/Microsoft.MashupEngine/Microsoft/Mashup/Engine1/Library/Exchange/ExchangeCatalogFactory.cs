using System;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BD4 RID: 3028
	internal static class ExchangeCatalogFactory
	{
		// Token: 0x06005297 RID: 21143 RVA: 0x00117450 File Offset: 0x00115650
		public static bool TryGetCatalog(ExchangeVersion exchangeVersion, string itemClass, out ExchangeCatalog exchangeCatalog)
		{
			if (itemClass != null)
			{
				int length = itemClass.Length;
				if (length <= 11)
				{
					if (length != 8)
					{
						if (length == 11)
						{
							if (itemClass == "IPM.Contact")
							{
								exchangeCatalog = new ExchangeContactCatalog(exchangeVersion);
								return true;
							}
						}
					}
					else
					{
						char c = itemClass[4];
						if (c != 'N')
						{
							if (c == 'T')
							{
								if (itemClass == "IPM.Task")
								{
									exchangeCatalog = new ExchangeTaskCatalog(exchangeVersion);
									return true;
								}
							}
						}
						else if (itemClass == "IPM.Note")
						{
							exchangeCatalog = new ExchangeEmailMessageCatalog(exchangeVersion);
							return true;
						}
					}
				}
				else if (length != 15)
				{
					switch (length)
					{
					case 25:
						if (itemClass == "IPM.Schedule.Meeting.Resp")
						{
							exchangeCatalog = new ExchangeMeetingRequestCatalog(exchangeVersion);
							return true;
						}
						break;
					case 28:
						if (itemClass == "IPM.Schedule.Meeting.Request")
						{
							exchangeCatalog = new ExchangeMeetingRequestCatalog(exchangeVersion);
							return true;
						}
						break;
					case 29:
						if (itemClass == "IPM.Schedule.Meeting.Canceled")
						{
							exchangeCatalog = new ExchangeMeetingRequestCatalog(exchangeVersion);
							return true;
						}
						break;
					}
				}
				else if (itemClass == "IPM.Appointment")
				{
					exchangeCatalog = new ExchangeAppointmentCatalog(exchangeVersion);
					return true;
				}
			}
			exchangeCatalog = null;
			return false;
		}
	}
}
