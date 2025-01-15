using System;

namespace Microsoft.BIServer.HostingEnvironment.Cryptography
{
	// Token: 0x02000038 RID: 56
	public class NoCrypto : Crypto
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00005471 File Offset: 0x00003671
		public static NoCrypto Instance
		{
			get
			{
				return NoCrypto._instance;
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005442 File Offset: 0x00003642
		private NoCrypto()
		{
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005478 File Offset: 0x00003678
		public override string GetName()
		{
			return "NoCrypto";
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005480 File Offset: 0x00003680
		protected override byte[] EncryptInternal(byte[] data)
		{
			byte[] array = new byte[data.Length];
			data.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000054A0 File Offset: 0x000036A0
		protected override byte[] DecryptInternal(byte[] data)
		{
			byte[] array = new byte[data.Length];
			data.CopyTo(array, 0);
			return array;
		}

		// Token: 0x04000097 RID: 151
		private static readonly NoCrypto _instance = new NoCrypto();
	}
}
