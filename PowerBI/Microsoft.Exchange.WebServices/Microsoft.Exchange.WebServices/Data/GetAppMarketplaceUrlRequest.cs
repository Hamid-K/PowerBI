using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200010D RID: 269
	internal sealed class GetAppMarketplaceUrlRequest : SimpleServiceRequestBase
	{
		// Token: 0x06000D5E RID: 3422 RVA: 0x0002AA8C File Offset: 0x00029A8C
		internal GetAppMarketplaceUrlRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x0002AA95 File Offset: 0x00029A95
		// (set) Token: 0x06000D60 RID: 3424 RVA: 0x0002AA9D File Offset: 0x00029A9D
		internal string ApiVersionSupported { get; set; }

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x0002AAA6 File Offset: 0x00029AA6
		// (set) Token: 0x06000D62 RID: 3426 RVA: 0x0002AAAE File Offset: 0x00029AAE
		internal string SchemaVersionSupported { get; set; }

		// Token: 0x06000D63 RID: 3427 RVA: 0x0002AAB7 File Offset: 0x00029AB7
		internal override string GetXmlElementName()
		{
			return "GetAppMarketplaceUrl";
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0002AABE File Offset: 0x00029ABE
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateNonBlankStringParamAllowNull(this.ApiVersionSupported, "ApiVersionSupported");
			EwsUtilities.ValidateNonBlankStringParamAllowNull(this.SchemaVersionSupported, "SchemaVersionSupported");
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0002AAE6 File Offset: 0x00029AE6
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (!string.IsNullOrEmpty(this.ApiVersionSupported))
			{
				writer.WriteElementValue(XmlNamespace.Messages, "ApiVersionSupported", this.ApiVersionSupported);
			}
			if (!string.IsNullOrEmpty(this.SchemaVersionSupported))
			{
				writer.WriteElementValue(XmlNamespace.Messages, "SchemaVersionSupported", this.SchemaVersionSupported);
			}
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0002AB26 File Offset: 0x00029B26
		internal override string GetResponseXmlElementName()
		{
			return "GetAppMarketplaceUrlResponse";
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0002AB30 File Offset: 0x00029B30
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			GetAppMarketplaceUrlResponse getAppMarketplaceUrlResponse = new GetAppMarketplaceUrlResponse();
			getAppMarketplaceUrlResponse.LoadFromXml(reader, "GetAppMarketplaceUrlResponse");
			return getAppMarketplaceUrlResponse;
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0002AB50 File Offset: 0x00029B50
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2013;
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0002AB54 File Offset: 0x00029B54
		internal GetAppMarketplaceUrlResponse Execute()
		{
			GetAppMarketplaceUrlResponse getAppMarketplaceUrlResponse = (GetAppMarketplaceUrlResponse)base.InternalExecute();
			getAppMarketplaceUrlResponse.ThrowIfNecessary();
			return getAppMarketplaceUrlResponse;
		}
	}
}
