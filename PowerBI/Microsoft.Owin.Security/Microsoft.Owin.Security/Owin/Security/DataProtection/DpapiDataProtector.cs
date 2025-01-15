using System;
using System.Security.Cryptography;

namespace Microsoft.Owin.Security.DataProtection
{
	// Token: 0x02000027 RID: 39
	internal class DpapiDataProtector : IDataProtector
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x000034C3 File Offset: 0x000016C3
		public DpapiDataProtector(string appName, string[] purposes)
		{
			this._protector = new DpapiDataProtector(appName, "Microsoft.Owin.Security.IDataProtector", purposes)
			{
				Scope = DataProtectionScope.CurrentUser
			};
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000034E4 File Offset: 0x000016E4
		public byte[] Protect(byte[] userData)
		{
			return this._protector.Protect(userData);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000034F2 File Offset: 0x000016F2
		public byte[] Unprotect(byte[] protectedData)
		{
			return this._protector.Unprotect(protectedData);
		}

		// Token: 0x04000048 RID: 72
		private readonly DpapiDataProtector _protector;
	}
}
