using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200010C RID: 268
	internal sealed class GetAppManifestsRequest : SimpleServiceRequestBase
	{
		// Token: 0x06000D52 RID: 3410 RVA: 0x0002A9A4 File Offset: 0x000299A4
		internal GetAppManifestsRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x0002A9AD File Offset: 0x000299AD
		// (set) Token: 0x06000D54 RID: 3412 RVA: 0x0002A9B5 File Offset: 0x000299B5
		internal string ApiVersionSupported { get; set; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x0002A9BE File Offset: 0x000299BE
		// (set) Token: 0x06000D56 RID: 3414 RVA: 0x0002A9C6 File Offset: 0x000299C6
		internal string SchemaVersionSupported { get; set; }

		// Token: 0x06000D57 RID: 3415 RVA: 0x0002A9CF File Offset: 0x000299CF
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateNonBlankStringParamAllowNull(this.ApiVersionSupported, "ApiVersionSupported");
			EwsUtilities.ValidateNonBlankStringParamAllowNull(this.SchemaVersionSupported, "SchemaVersionSupported");
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0002A9F7 File Offset: 0x000299F7
		internal override string GetXmlElementName()
		{
			return "GetAppManifests";
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0002A9FE File Offset: 0x000299FE
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

		// Token: 0x06000D5A RID: 3418 RVA: 0x0002AA3E File Offset: 0x00029A3E
		internal override string GetResponseXmlElementName()
		{
			return "GetAppManifestsResponse";
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0002AA48 File Offset: 0x00029A48
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			GetAppManifestsResponse getAppManifestsResponse = new GetAppManifestsResponse();
			getAppManifestsResponse.LoadFromXml(reader, "GetAppManifestsResponse");
			return getAppManifestsResponse;
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0002AA68 File Offset: 0x00029A68
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2013;
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0002AA6C File Offset: 0x00029A6C
		internal GetAppManifestsResponse Execute()
		{
			GetAppManifestsResponse getAppManifestsResponse = (GetAppManifestsResponse)base.InternalExecute();
			getAppManifestsResponse.ThrowIfNecessary();
			return getAppManifestsResponse;
		}
	}
}
