using System;

namespace Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos
{
	// Token: 0x02000198 RID: 408
	public abstract class Credential
	{
		// Token: 0x060012FC RID: 4860
		internal abstract CredentialHandle Structify();

		// Token: 0x060012FD RID: 4861 RVA: 0x0004025C File Offset: 0x0003E45C
		public static Credential Current()
		{
			return new Credential.CurrentCredential();
		}

		// Token: 0x02000416 RID: 1046
		private class CurrentCredential : Credential
		{
			// Token: 0x06001ECF RID: 7887 RVA: 0x0006E78C File Offset: 0x0006C98C
			internal override CredentialHandle Structify()
			{
				return new CredentialHandle(null);
			}
		}
	}
}
