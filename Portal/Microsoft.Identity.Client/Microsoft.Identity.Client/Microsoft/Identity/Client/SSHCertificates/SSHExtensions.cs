using System;
using Microsoft.Identity.Client.AuthScheme.SSHCertificates;

namespace Microsoft.Identity.Client.SSHCertificates
{
	// Token: 0x0200028F RID: 655
	public static class SSHExtensions
	{
		// Token: 0x0600191B RID: 6427 RVA: 0x00052B8F File Offset: 0x00050D8F
		public static AcquireTokenInteractiveParameterBuilder WithSSHCertificateAuthenticationScheme(this AcquireTokenInteractiveParameterBuilder builder, string publicKeyJwk, string keyId)
		{
			builder.CommonParameters.AuthenticationScheme = new SSHCertAuthenticationScheme(keyId, publicKeyJwk);
			return builder;
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x00052BA4 File Offset: 0x00050DA4
		public static AcquireTokenSilentParameterBuilder WithSSHCertificateAuthenticationScheme(this AcquireTokenSilentParameterBuilder builder, string publicKeyJwk, string keyId)
		{
			builder.CommonParameters.AuthenticationScheme = new SSHCertAuthenticationScheme(keyId, publicKeyJwk);
			return builder;
		}
	}
}
