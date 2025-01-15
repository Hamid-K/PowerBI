using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200004E RID: 78
	internal sealed class MachineKeyEncryption : Encryption
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000BF66 File Offset: 0x0000A166
		public static Encryption Instance
		{
			get
			{
				return MachineKeyEncryption._instance;
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000B311 File Offset: 0x00009511
		private MachineKeyEncryption()
		{
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000BF6D File Offset: 0x0000A16D
		protected override bool IsEncryptionChecked()
		{
			return this._encryptionChecked;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000BF75 File Offset: 0x0000A175
		protected override void SetEncryptionChecked()
		{
			this._encryptionChecked = true;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000BF7E File Offset: 0x0000A17E
		protected override byte[] EncryptInternal(byte[] data)
		{
			return Encryption.EncryptNative(data, 5);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000BF87 File Offset: 0x0000A187
		public override byte[] Decrypt(byte[] data, bool useSalt = false)
		{
			return Encryption.DecryptNative(data, 5);
		}

		// Token: 0x040002B9 RID: 697
		private const int MachineKeyCryptoFlags = 5;

		// Token: 0x040002BA RID: 698
		private static readonly Encryption _instance = new MachineKeyEncryption();

		// Token: 0x040002BB RID: 699
		private bool _encryptionChecked;
	}
}
