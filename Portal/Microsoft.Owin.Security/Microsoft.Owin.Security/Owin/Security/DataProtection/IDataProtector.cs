using System;

namespace Microsoft.Owin.Security.DataProtection
{
	// Token: 0x02000029 RID: 41
	public interface IDataProtector
	{
		// Token: 0x060000BD RID: 189
		byte[] Protect(byte[] userData);

		// Token: 0x060000BE RID: 190
		byte[] Unprotect(byte[] protectedData);
	}
}
