using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000131 RID: 305
	[Flags]
	internal enum EndpointProtocolOptions
	{
		// Token: 0x04001162 RID: 4450
		None = 0,
		// Token: 0x04001163 RID: 4451
		HttpAuthenticationRealm = 1,
		// Token: 0x04001164 RID: 4452
		HttpAuthentication = 2,
		// Token: 0x04001165 RID: 4453
		HttpClearPort = 4,
		// Token: 0x04001166 RID: 4454
		HttpCompression = 8,
		// Token: 0x04001167 RID: 4455
		HttpDefaultLogonDomain = 16,
		// Token: 0x04001168 RID: 4456
		HttpPath = 32,
		// Token: 0x04001169 RID: 4457
		HttpPorts = 64,
		// Token: 0x0400116A RID: 4458
		HttpSite = 128,
		// Token: 0x0400116B RID: 4459
		HttpSslPort = 256,
		// Token: 0x0400116C RID: 4460
		HttpOptions = 511,
		// Token: 0x0400116D RID: 4461
		TcpListenerIP = 512,
		// Token: 0x0400116E RID: 4462
		TcpListenerPort = 1024,
		// Token: 0x0400116F RID: 4463
		TcpOptions = 1536
	}
}
