using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000273 RID: 627
	public sealed class MailboxHoldResult
	{
		// Token: 0x0600160A RID: 5642 RVA: 0x0003D580 File Offset: 0x0003C580
		internal static MailboxHoldResult LoadFromXml(EwsServiceXmlReader reader)
		{
			List<MailboxHoldStatus> list = new List<MailboxHoldStatus>();
			reader.ReadStartElement(XmlNamespace.Messages, "MailboxHoldResult");
			MailboxHoldResult mailboxHoldResult = new MailboxHoldResult();
			mailboxHoldResult.HoldId = reader.ReadElementValue(XmlNamespace.Types, "HoldId");
			reader.Read();
			mailboxHoldResult.Query = string.Empty;
			if (reader.IsStartElement(XmlNamespace.Types, "Query"))
			{
				mailboxHoldResult.Query = reader.ReadElementValue(XmlNamespace.Types, "Query");
				reader.ReadStartElement(XmlNamespace.Types, "MailboxHoldStatuses");
			}
			do
			{
				reader.Read();
				if (reader.IsStartElement(XmlNamespace.Types, "MailboxHoldStatus"))
				{
					string text = reader.ReadElementValue(XmlNamespace.Types, "Mailbox");
					HoldStatus holdStatus = (HoldStatus)Enum.Parse(typeof(HoldStatus), reader.ReadElementValue(XmlNamespace.Types, "Status"));
					string text2 = reader.ReadElementValue(XmlNamespace.Types, "AdditionalInfo");
					list.Add(new MailboxHoldStatus(text, holdStatus, text2));
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Messages, "MailboxHoldResult"));
			mailboxHoldResult.Statuses = ((list.Count == 0) ? null : list.ToArray());
			return mailboxHoldResult;
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x0003D67C File Offset: 0x0003C67C
		internal static MailboxHoldResult LoadFromJson(JsonObject jsonObject)
		{
			List<MailboxHoldStatus> list = new List<MailboxHoldStatus>();
			MailboxHoldResult mailboxHoldResult = new MailboxHoldResult();
			if (jsonObject.ContainsKey("HoldId"))
			{
				mailboxHoldResult.HoldId = jsonObject.ReadAsString("HoldId");
			}
			if (jsonObject.ContainsKey("Query"))
			{
				mailboxHoldResult.Query = jsonObject.ReadAsString("Query");
			}
			if (jsonObject.ContainsKey("Statuses"))
			{
				foreach (object obj in jsonObject.ReadAsArray("Statuses"))
				{
					MailboxHoldStatus mailboxHoldStatus = new MailboxHoldStatus();
					JsonObject jsonObject2 = obj as JsonObject;
					if (jsonObject2.ContainsKey("Mailbox"))
					{
						mailboxHoldStatus.Mailbox = jsonObject2.ReadAsString("Mailbox");
					}
					if (jsonObject2.ContainsKey("Status"))
					{
						mailboxHoldStatus.Status = (HoldStatus)Enum.Parse(typeof(HoldStatus), jsonObject2.ReadAsString("Status"));
					}
					if (jsonObject2.ContainsKey("AdditionalInfo"))
					{
						mailboxHoldStatus.AdditionalInfo = jsonObject2.ReadAsString("AdditionalInfo");
					}
					list.Add(mailboxHoldStatus);
				}
			}
			mailboxHoldResult.Statuses = ((list.Count == 0) ? null : list.ToArray());
			return mailboxHoldResult;
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x0003D7AE File Offset: 0x0003C7AE
		// (set) Token: 0x0600160D RID: 5645 RVA: 0x0003D7B6 File Offset: 0x0003C7B6
		public string HoldId { get; set; }

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x0003D7BF File Offset: 0x0003C7BF
		// (set) Token: 0x0600160F RID: 5647 RVA: 0x0003D7C7 File Offset: 0x0003C7C7
		public string Query { get; set; }

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001610 RID: 5648 RVA: 0x0003D7D0 File Offset: 0x0003C7D0
		// (set) Token: 0x06001611 RID: 5649 RVA: 0x0003D7D8 File Offset: 0x0003C7D8
		public MailboxHoldStatus[] Statuses { get; set; }
	}
}
