using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000042 RID: 66
	[Obsolete("Deprecated!")]
	public class IdentityTransferToken
	{
		// Token: 0x0600031A RID: 794 RVA: 0x0000F649 File Offset: 0x0000D849
		public IdentityTransferToken(string identityProviderName, string identityName)
		{
			throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000F65B File Offset: 0x0000D85B
		public IdentityTransferToken(string identityProviderName, string identityName, TokenSigner signer)
		{
			throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000F66D File Offset: 0x0000D86D
		public IdentityTransferToken(string identityProviderName, string identityName, bool assumeDbAdmin, TokenSigner signer)
		{
			throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000F67F File Offset: 0x0000D87F
		public IdentityTransferToken(string identityProviderName, string identityName, DateTime issueDate, bool assumeDbAdmin, TokenSigner signer)
		{
			throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000F691 File Offset: 0x0000D891
		public string IdentityProviderName
		{
			get
			{
				throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000F69D File Offset: 0x0000D89D
		public string IdentityName
		{
			get
			{
				throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000F6A9 File Offset: 0x0000D8A9
		public DateTime IssueDate
		{
			get
			{
				throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000F6B5 File Offset: 0x0000D8B5
		public bool AssumeDbAdmin
		{
			get
			{
				throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000F6C1 File Offset: 0x0000D8C1
		public TokenSigner Signer
		{
			get
			{
				throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000F6CD File Offset: 0x0000D8CD
		public string GetTokenXml()
		{
			throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
		}
	}
}
