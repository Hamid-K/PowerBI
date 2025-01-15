using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000082 RID: 130
	public class FileProtectionInformation
	{
		// Token: 0x0400015D RID: 349
		public static readonly FileProtectionInformation Unprotected = new FileProtectionInformation();

		// Token: 0x0400015E RID: 350
		public bool Encrypted;

		// Token: 0x0400015F RID: 351
		public bool Classified;

		// Token: 0x04000160 RID: 352
		public ProtectionInformation ProtectionInformation;
	}
}
