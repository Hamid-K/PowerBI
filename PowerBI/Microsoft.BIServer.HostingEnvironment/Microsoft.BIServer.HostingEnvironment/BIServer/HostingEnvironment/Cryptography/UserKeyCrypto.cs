using System;
using System.Security.Cryptography;

namespace Microsoft.BIServer.HostingEnvironment.Cryptography
{
	// Token: 0x0200003A RID: 58
	public class UserKeyCrypto : Crypto
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00005A6F File Offset: 0x00003C6F
		public static Crypto Instance
		{
			get
			{
				return UserKeyCrypto._instance;
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00005442 File Offset: 0x00003642
		private UserKeyCrypto()
		{
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005A76 File Offset: 0x00003C76
		public override string GetName()
		{
			return "UserKeyCrypto";
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005A7D File Offset: 0x00003C7D
		protected override byte[] EncryptInternal(byte[] data)
		{
			return ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00005A87 File Offset: 0x00003C87
		protected override byte[] DecryptInternal(byte[] data)
		{
			return ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
		}

		// Token: 0x040000A6 RID: 166
		private static readonly Crypto _instance = new UserKeyCrypto();
	}
}
