using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000834 RID: 2100
	public enum SecurityCheckCode : byte
	{
		// Token: 0x04002ECC RID: 11980
		Success,
		// Token: 0x04002ECD RID: 11981
		NotSupported,
		// Token: 0x04002ECE RID: 11982
		DCEInfoStatus,
		// Token: 0x04002ECF RID: 11983
		DCEErrorRetry,
		// Token: 0x04002ED0 RID: 11984
		DCEErrorNoRetry,
		// Token: 0x04002ED1 RID: 11985
		GSSAPIInfoStatus,
		// Token: 0x04002ED2 RID: 11986
		GSSAPIErrorRetry,
		// Token: 0x04002ED3 RID: 11987
		GSSAPIErrorNoRetry,
		// Token: 0x04002ED4 RID: 11988
		LocalInfoStatus,
		// Token: 0x04002ED5 RID: 11989
		LocalErrorRetry,
		// Token: 0x04002ED6 RID: 11990
		LocalErrorNoRetry,
		// Token: 0x04002ED7 RID: 11991
		InvalidToken,
		// Token: 0x04002ED8 RID: 11992
		PasswordExpired = 14,
		// Token: 0x04002ED9 RID: 11993
		PasswordInvalid,
		// Token: 0x04002EDA RID: 11994
		PasswordMissing,
		// Token: 0x04002EDB RID: 11995
		UserIdMissing = 18,
		// Token: 0x04002EDC RID: 11996
		UserIdInvalid,
		// Token: 0x04002EDD RID: 11997
		UserIdRevoked,
		// Token: 0x04002EDE RID: 11998
		NewPasswordInvalid,
		// Token: 0x04002EDF RID: 11999
		AuthenticationFailed,
		// Token: 0x04002EE0 RID: 12000
		InvalidGSSAPIServerCredential,
		// Token: 0x04002EE1 RID: 12001
		GSSAPIServerCredentialExpired,
		// Token: 0x04002EE2 RID: 12002
		RequireMoreSecurityContext,
		// Token: 0x04002EE3 RID: 12003
		EncAlgNotSupported = 27
	}
}
