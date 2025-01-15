using System;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x0200001A RID: 26
	public sealed class GetDomainSettingsResponseCollection : AutodiscoverResponseCollection<GetDomainSettingsResponse>
	{
		// Token: 0x06000125 RID: 293 RVA: 0x00006976 File Offset: 0x00005976
		internal GetDomainSettingsResponseCollection()
		{
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000697E File Offset: 0x0000597E
		internal override GetDomainSettingsResponse CreateResponseInstance()
		{
			return new GetDomainSettingsResponse();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006985 File Offset: 0x00005985
		internal override string GetResponseCollectionXmlElementName()
		{
			return "DomainResponses";
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000698C File Offset: 0x0000598C
		internal override string GetResponseInstanceXmlElementName()
		{
			return "DomainResponse";
		}
	}
}
