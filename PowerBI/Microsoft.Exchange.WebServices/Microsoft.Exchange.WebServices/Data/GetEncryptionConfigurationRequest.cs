using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200010B RID: 267
	internal sealed class GetEncryptionConfigurationRequest : SimpleServiceRequestBase
	{
		// Token: 0x06000D4B RID: 3403 RVA: 0x0002A944 File Offset: 0x00029944
		internal GetEncryptionConfigurationRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0002A94D File Offset: 0x0002994D
		internal override string GetXmlElementName()
		{
			return "GetEncryptionConfiguration";
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0002A954 File Offset: 0x00029954
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0002A956 File Offset: 0x00029956
		internal override string GetResponseXmlElementName()
		{
			return "GetEncryptionConfigurationResponse";
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0002A960 File Offset: 0x00029960
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			GetEncryptionConfigurationResponse getEncryptionConfigurationResponse = new GetEncryptionConfigurationResponse();
			getEncryptionConfigurationResponse.LoadFromXml(reader, "GetEncryptionConfigurationResponse");
			return getEncryptionConfigurationResponse;
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0002A980 File Offset: 0x00029980
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2013;
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0002A984 File Offset: 0x00029984
		internal GetEncryptionConfigurationResponse Execute()
		{
			GetEncryptionConfigurationResponse getEncryptionConfigurationResponse = (GetEncryptionConfigurationResponse)base.InternalExecute();
			getEncryptionConfigurationResponse.ThrowIfNecessary();
			return getEncryptionConfigurationResponse;
		}
	}
}
