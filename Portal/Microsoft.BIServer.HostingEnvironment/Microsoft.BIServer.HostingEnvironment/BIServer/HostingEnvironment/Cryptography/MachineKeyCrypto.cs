using System;
using System.Security.Cryptography;

namespace Microsoft.BIServer.HostingEnvironment.Cryptography
{
	// Token: 0x02000037 RID: 55
	public class MachineKeyCrypto : Crypto
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000543B File Offset: 0x0000363B
		public static Crypto Instance
		{
			get
			{
				return MachineKeyCrypto._instance;
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005442 File Offset: 0x00003642
		private MachineKeyCrypto()
		{
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000544A File Offset: 0x0000364A
		public override string GetName()
		{
			return "MachineKeyCrypto";
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005451 File Offset: 0x00003651
		protected override byte[] EncryptInternal(byte[] data)
		{
			return ProtectedData.Protect(data, null, DataProtectionScope.LocalMachine);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000545B File Offset: 0x0000365B
		protected override byte[] DecryptInternal(byte[] data)
		{
			return ProtectedData.Unprotect(data, null, DataProtectionScope.LocalMachine);
		}

		// Token: 0x04000096 RID: 150
		private static readonly Crypto _instance = new MachineKeyCrypto();
	}
}
