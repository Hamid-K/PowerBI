using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000FF RID: 255
	internal sealed class DisableAppRequest : SimpleServiceRequestBase
	{
		// Token: 0x06000CB9 RID: 3257 RVA: 0x000296B5 File Offset: 0x000286B5
		internal DisableAppRequest(ExchangeService service, string id, DisableReasonType disableReason)
			: base(service)
		{
			this.Id = id;
			this.DisableReason = disableReason;
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x000296CC File Offset: 0x000286CC
		internal override string GetXmlElementName()
		{
			return "DisableApp";
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x000296D3 File Offset: 0x000286D3
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Messages, "ID", this.Id);
			writer.WriteElementValue(XmlNamespace.Messages, "DisableReason", this.DisableReason);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x000296FE File Offset: 0x000286FE
		internal override string GetResponseXmlElementName()
		{
			return "DisableAppResponse";
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00029708 File Offset: 0x00028708
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			DisableAppResponse disableAppResponse = new DisableAppResponse();
			disableAppResponse.LoadFromXml(reader, "DisableAppResponse");
			return disableAppResponse;
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00029728 File Offset: 0x00028728
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2013;
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0002972C File Offset: 0x0002872C
		internal DisableAppResponse Execute()
		{
			DisableAppResponse disableAppResponse = (DisableAppResponse)base.InternalExecute();
			disableAppResponse.ThrowIfNecessary();
			return disableAppResponse;
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0002974C File Offset: 0x0002874C
		// (set) Token: 0x06000CC1 RID: 3265 RVA: 0x00029754 File Offset: 0x00028754
		private string Id { get; set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x0002975D File Offset: 0x0002875D
		// (set) Token: 0x06000CC3 RID: 3267 RVA: 0x00029765 File Offset: 0x00028765
		private DisableReasonType DisableReason { get; set; }
	}
}
