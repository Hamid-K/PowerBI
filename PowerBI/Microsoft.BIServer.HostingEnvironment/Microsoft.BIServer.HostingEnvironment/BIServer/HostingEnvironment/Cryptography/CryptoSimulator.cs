using System;
using System.Collections.Generic;

namespace Microsoft.BIServer.HostingEnvironment.Cryptography
{
	// Token: 0x02000035 RID: 53
	public class CryptoSimulator : Crypto
	{
		// Token: 0x06000161 RID: 353 RVA: 0x000053DC File Offset: 0x000035DC
		public override string GetName()
		{
			return "CryptoSimulator";
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000053E3 File Offset: 0x000035E3
		public void Reset()
		{
			this.encryptionHistory.Clear();
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000053F0 File Offset: 0x000035F0
		protected override byte[] EncryptInternal(byte[] data)
		{
			byte[] array = new byte[data.Length];
			data.CopyTo(array, 0);
			this.encryptionHistory.Enqueue(array);
			return array;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000541B File Offset: 0x0000361B
		protected override byte[] DecryptInternal(byte[] data)
		{
			return this.encryptionHistory.Dequeue();
		}

		// Token: 0x04000095 RID: 149
		private Queue<byte[]> encryptionHistory = new Queue<byte[]>();
	}
}
