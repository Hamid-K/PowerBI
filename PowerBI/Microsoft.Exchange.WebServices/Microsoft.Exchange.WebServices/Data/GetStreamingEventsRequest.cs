using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000126 RID: 294
	internal class GetStreamingEventsRequest : HangingServiceRequestBase
	{
		// Token: 0x06000E52 RID: 3666 RVA: 0x0002C017 File Offset: 0x0002B017
		internal GetStreamingEventsRequest(ExchangeService service, HangingServiceRequestBase.HandleResponseObject serviceObjectHandler, IEnumerable<string> subscriptionIds, int connectionTimeout)
			: base(service, serviceObjectHandler, GetStreamingEventsRequest.heartbeatFrequency)
		{
			this.subscriptionIds = subscriptionIds;
			this.connectionTimeout = connectionTimeout;
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0002C035 File Offset: 0x0002B035
		internal override string GetXmlElementName()
		{
			return "GetStreamingEvents";
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0002C03C File Offset: 0x0002B03C
		internal override string GetResponseXmlElementName()
		{
			return "GetStreamingEventsResponse";
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0002C044 File Offset: 0x0002B044
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Messages, "SubscriptionIds");
			foreach (string text in this.subscriptionIds)
			{
				writer.WriteElementValue(XmlNamespace.Types, "SubscriptionId", text);
			}
			writer.WriteEndElement();
			writer.WriteElementValue(XmlNamespace.Messages, "ConnectionTimeout", this.connectionTimeout);
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0002C0C0 File Offset: 0x0002B0C0
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2010_SP1;
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x0002C0C4 File Offset: 0x0002B0C4
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			reader.ReadStartElement(XmlNamespace.Messages, "ResponseMessages");
			GetStreamingEventsResponse getStreamingEventsResponse = new GetStreamingEventsResponse(this);
			getStreamingEventsResponse.LoadFromXml(reader, "GetStreamingEventsResponseMessage");
			reader.ReadEndElementIfNecessary(XmlNamespace.Messages, "ResponseMessages");
			return getStreamingEventsResponse;
		}

		// Token: 0x170002F5 RID: 757
		// (set) Token: 0x06000E58 RID: 3672 RVA: 0x0002C0FD File Offset: 0x0002B0FD
		internal static int HeartbeatFrequency
		{
			set
			{
				GetStreamingEventsRequest.heartbeatFrequency = value;
			}
		}

		// Token: 0x04000924 RID: 2340
		internal const int HeartbeatFrequencyDefault = 45000;

		// Token: 0x04000925 RID: 2341
		private static int heartbeatFrequency = 45000;

		// Token: 0x04000926 RID: 2342
		private IEnumerable<string> subscriptionIds;

		// Token: 0x04000927 RID: 2343
		private int connectionTimeout;
	}
}
