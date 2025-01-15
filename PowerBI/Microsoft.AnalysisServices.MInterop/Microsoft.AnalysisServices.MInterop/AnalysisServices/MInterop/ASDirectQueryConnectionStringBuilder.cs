using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x02000012 RID: 18
	internal sealed class ASDirectQueryConnectionStringBuilder : DirectQueryConnectionStringBuilder
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00003054 File Offset: 0x00001254
		public override void AddCredential(Credential credential)
		{
			if (credential == null)
			{
				return;
			}
			base.AddCredentialCommon(credential);
			base.AddEncryptionCommon(credential);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003068 File Offset: 0x00001268
		protected override void AddOptions(Dictionary<string, object> options)
		{
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000306A File Offset: 0x0000126A
		protected override void InferProviderAndDriver()
		{
			this["Provider"] = "MSOLAP";
		}
	}
}
