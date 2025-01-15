using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000167 RID: 359
	internal sealed class GetStreamingEventsResponse : ServiceResponse
	{
		// Token: 0x060010A9 RID: 4265 RVA: 0x000310C5 File Offset: 0x000300C5
		internal GetStreamingEventsResponse(HangingServiceRequestBase request)
		{
			this.ErrorSubscriptionIds = new List<string>();
			this.request = request;
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x000310EC File Offset: 0x000300EC
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			reader.Read();
			if (reader.LocalName == "Notifications")
			{
				this.results.LoadFromXml(reader);
				return;
			}
			if (reader.LocalName == "ConnectionStatus")
			{
				string text = reader.ReadElementValue(XmlNamespace.Messages, "ConnectionStatus");
				if (text.Equals(GetStreamingEventsResponse.ConnectionStatus.Closed.ToString()))
				{
					this.request.Disconnect(HangingRequestDisconnectReason.Clean, null);
				}
			}
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00031164 File Offset: 0x00030164
		internal override bool LoadExtraErrorDetailsFromXml(EwsServiceXmlReader reader, string xmlElementName)
		{
			bool flag = base.LoadExtraErrorDetailsFromXml(reader, xmlElementName);
			if (reader.IsStartElement(XmlNamespace.Messages, "ErrorSubscriptionIds"))
			{
				do
				{
					reader.Read();
					if (reader.NodeType == 1 && reader.LocalName == "SubscriptionId")
					{
						this.ErrorSubscriptionIds.Add(reader.ReadElementValue(XmlNamespace.Messages, "SubscriptionId"));
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Messages, "ErrorSubscriptionIds"));
				return true;
			}
			return flag;
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060010AC RID: 4268 RVA: 0x000311D1 File Offset: 0x000301D1
		internal GetStreamingEventsResults Results
		{
			get
			{
				return this.results;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x000311D9 File Offset: 0x000301D9
		// (set) Token: 0x060010AE RID: 4270 RVA: 0x000311E1 File Offset: 0x000301E1
		internal List<string> ErrorSubscriptionIds { get; private set; }

		// Token: 0x040009BA RID: 2490
		private GetStreamingEventsResults results = new GetStreamingEventsResults();

		// Token: 0x040009BB RID: 2491
		private HangingServiceRequestBase request;

		// Token: 0x02000168 RID: 360
		private enum ConnectionStatus
		{
			// Token: 0x040009BE RID: 2494
			OK,
			// Token: 0x040009BF RID: 2495
			Closed
		}
	}
}
