using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000106 RID: 262
	internal sealed class FindFolderRequest : FindRequest<FindFolderResponse>
	{
		// Token: 0x06000D17 RID: 3351 RVA: 0x0002A126 File Offset: 0x00029126
		internal FindFolderRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0002A130 File Offset: 0x00029130
		internal override FindFolderResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new FindFolderResponse(base.View.GetPropertySetOrDefault());
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0002A142 File Offset: 0x00029142
		internal override string GetXmlElementName()
		{
			return "FindFolder";
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0002A149 File Offset: 0x00029149
		internal override string GetResponseXmlElementName()
		{
			return "FindFolderResponse";
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0002A150 File Offset: 0x00029150
		internal override string GetResponseMessageXmlElementName()
		{
			return "FindFolderResponseMessage";
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0002A157 File Offset: 0x00029157
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}
	}
}
