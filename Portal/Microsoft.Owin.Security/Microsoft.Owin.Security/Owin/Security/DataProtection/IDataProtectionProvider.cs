using System;

namespace Microsoft.Owin.Security.DataProtection
{
	// Token: 0x02000028 RID: 40
	public interface IDataProtectionProvider
	{
		// Token: 0x060000BC RID: 188
		IDataProtector Create(params string[] purposes);
	}
}
