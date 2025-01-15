using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000110 RID: 272
	internal sealed class GetDiscoverySearchConfigurationRequest : SimpleServiceRequestBase, IJsonSerializable
	{
		// Token: 0x06000D8B RID: 3467 RVA: 0x0002AFCA File Offset: 0x00029FCA
		internal GetDiscoverySearchConfigurationRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x0002AFD3 File Offset: 0x00029FD3
		internal override string GetResponseXmlElementName()
		{
			return "GetDiscoverySearchConfigurationResponse";
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0002AFDA File Offset: 0x00029FDA
		internal override string GetXmlElementName()
		{
			return "GetDiscoverySearchConfiguration";
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0002AFE4 File Offset: 0x00029FE4
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			GetDiscoverySearchConfigurationResponse getDiscoverySearchConfigurationResponse = new GetDiscoverySearchConfigurationResponse();
			getDiscoverySearchConfigurationResponse.LoadFromXml(reader, this.GetResponseXmlElementName());
			return getDiscoverySearchConfigurationResponse;
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x0002B008 File Offset: 0x0002A008
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Messages, "SearchId", this.SearchId ?? string.Empty);
			writer.WriteElementValue(XmlNamespace.Messages, "ExpandGroupMembership", this.ExpandGroupMembership.ToString().ToLower());
			writer.WriteElementValue(XmlNamespace.Messages, "InPlaceHoldConfigurationOnly", this.InPlaceHoldConfigurationOnly.ToString().ToLower());
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0002B06E File Offset: 0x0002A06E
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2013;
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0002B074 File Offset: 0x0002A074
		internal GetDiscoverySearchConfigurationResponse Execute()
		{
			return (GetDiscoverySearchConfigurationResponse)base.InternalExecute();
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0002B090 File Offset: 0x0002A090
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			return new JsonObject();
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x0002B0A4 File Offset: 0x0002A0A4
		// (set) Token: 0x06000D94 RID: 3476 RVA: 0x0002B0AC File Offset: 0x0002A0AC
		public string SearchId { get; set; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x0002B0B5 File Offset: 0x0002A0B5
		// (set) Token: 0x06000D96 RID: 3478 RVA: 0x0002B0BD File Offset: 0x0002A0BD
		public bool ExpandGroupMembership { get; set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x0002B0C6 File Offset: 0x0002A0C6
		// (set) Token: 0x06000D98 RID: 3480 RVA: 0x0002B0CE File Offset: 0x0002A0CE
		public bool InPlaceHoldConfigurationOnly { get; set; }
	}
}
