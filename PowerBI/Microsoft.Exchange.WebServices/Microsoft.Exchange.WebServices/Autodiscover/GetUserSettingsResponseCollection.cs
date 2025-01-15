using System;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x0200001C RID: 28
	public sealed class GetUserSettingsResponseCollection : AutodiscoverResponseCollection<GetUserSettingsResponse>
	{
		// Token: 0x06000137 RID: 311 RVA: 0x00006CD6 File Offset: 0x00005CD6
		internal GetUserSettingsResponseCollection()
		{
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006CDE File Offset: 0x00005CDE
		internal override GetUserSettingsResponse CreateResponseInstance()
		{
			return new GetUserSettingsResponse();
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006CE5 File Offset: 0x00005CE5
		internal override string GetResponseCollectionXmlElementName()
		{
			return "UserResponses";
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006CEC File Offset: 0x00005CEC
		internal override string GetResponseInstanceXmlElementName()
		{
			return "UserResponse";
		}
	}
}
