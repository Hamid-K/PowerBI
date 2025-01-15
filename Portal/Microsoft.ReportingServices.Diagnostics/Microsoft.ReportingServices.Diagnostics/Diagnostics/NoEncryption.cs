using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200004F RID: 79
	internal sealed class NoEncryption : Encryption
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000BF9C File Offset: 0x0000A19C
		public static Encryption Instance
		{
			get
			{
				return NoEncryption._instance;
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000B311 File Offset: 0x00009511
		private NoEncryption()
		{
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000BFA3 File Offset: 0x0000A1A3
		protected override bool IsEncryptionChecked()
		{
			return this._encryptionChecked;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000BFAB File Offset: 0x0000A1AB
		protected override void SetEncryptionChecked()
		{
			this._encryptionChecked = true;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000BFB4 File Offset: 0x0000A1B4
		protected override byte[] EncryptInternal(byte[] data)
		{
			return data;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000BFB4 File Offset: 0x0000A1B4
		public override byte[] Decrypt(byte[] data, bool useSalt = true)
		{
			return data;
		}

		// Token: 0x040002BC RID: 700
		private static readonly NoEncryption _instance = new NoEncryption();

		// Token: 0x040002BD RID: 701
		private bool _encryptionChecked;
	}
}
