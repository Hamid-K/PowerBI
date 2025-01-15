using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200000F RID: 15
	public class OAuth2AuthenticationInfo : AuthenticationInfo
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000242D File Offset: 0x0000062D
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002435 File Offset: 0x00000635
		public OAuthClientApplicationType ClientApplicationType { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000243E File Offset: 0x0000063E
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002446 File Offset: 0x00000646
		public IOAuthFactory ProviderFactory { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000244F File Offset: 0x0000064F
		public sealed override AuthenticationKind AuthenticationKind
		{
			get
			{
				return AuthenticationKind.OAuth2;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002452 File Offset: 0x00000652
		public override string Name
		{
			get
			{
				return "OAuth2";
			}
		}
	}
}
