using System;

namespace Microsoft.Owin.Security
{
	// Token: 0x0200000B RID: 11
	public interface ISecureDataFormat<TData>
	{
		// Token: 0x06000019 RID: 25
		string Protect(TData data);

		// Token: 0x0600001A RID: 26
		TData Unprotect(string protectedText);
	}
}
