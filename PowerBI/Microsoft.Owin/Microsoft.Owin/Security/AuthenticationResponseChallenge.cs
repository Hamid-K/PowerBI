using System;

namespace Microsoft.Owin.Security
{
	// Token: 0x02000021 RID: 33
	public class AuthenticationResponseChallenge
	{
		// Token: 0x0600019B RID: 411 RVA: 0x000048C2 File Offset: 0x00002AC2
		public AuthenticationResponseChallenge(string[] authenticationTypes, AuthenticationProperties properties)
		{
			this.AuthenticationTypes = authenticationTypes;
			this.Properties = properties ?? new AuthenticationProperties();
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600019C RID: 412 RVA: 0x000048E1 File Offset: 0x00002AE1
		// (set) Token: 0x0600019D RID: 413 RVA: 0x000048E9 File Offset: 0x00002AE9
		public string[] AuthenticationTypes { get; private set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600019E RID: 414 RVA: 0x000048F2 File Offset: 0x00002AF2
		// (set) Token: 0x0600019F RID: 415 RVA: 0x000048FA File Offset: 0x00002AFA
		public AuthenticationProperties Properties { get; private set; }
	}
}
