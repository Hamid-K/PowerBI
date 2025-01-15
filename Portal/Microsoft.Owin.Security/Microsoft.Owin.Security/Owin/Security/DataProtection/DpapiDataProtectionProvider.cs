using System;

namespace Microsoft.Owin.Security.DataProtection
{
	// Token: 0x02000026 RID: 38
	public class DpapiDataProtectionProvider : IDataProtectionProvider
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00003464 File Offset: 0x00001664
		public DpapiDataProtectionProvider()
			: this(Guid.NewGuid().ToString())
		{
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000348A File Offset: 0x0000168A
		public DpapiDataProtectionProvider(string appName)
		{
			if (appName == null)
			{
				throw new ArgumentNullException("appName");
			}
			this._appName = appName;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000034A7 File Offset: 0x000016A7
		public IDataProtector Create(params string[] purposes)
		{
			if (purposes == null)
			{
				throw new ArgumentNullException("purposes");
			}
			return new DpapiDataProtector(this._appName, purposes);
		}

		// Token: 0x04000047 RID: 71
		private readonly string _appName;
	}
}
