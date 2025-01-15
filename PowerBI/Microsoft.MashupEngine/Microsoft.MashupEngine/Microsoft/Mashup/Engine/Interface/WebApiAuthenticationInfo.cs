using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200000D RID: 13
	public sealed class WebApiAuthenticationInfo : AuthenticationInfo
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000023FB File Offset: 0x000005FB
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002403 File Offset: 0x00000603
		public string KeyLabel { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000240C File Offset: 0x0000060C
		public override AuthenticationKind AuthenticationKind
		{
			get
			{
				return AuthenticationKind.WebApi;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000240F File Offset: 0x0000060F
		public override string Name
		{
			get
			{
				return "WebApi";
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002416 File Offset: 0x00000616
		public override string GetPropertyLabel(string propertyName)
		{
			if (propertyName == "Key")
			{
				return this.KeyLabel;
			}
			return null;
		}
	}
}
