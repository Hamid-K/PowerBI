using System;
using System.Runtime.InteropServices;

namespace Microsoft.EnterpriseSingleSignOn.Interop
{
	// Token: 0x020004AE RID: 1198
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("66E51591-90D4-4E75-A47A-6CC0B20B0C04")]
	[CoClass(typeof(SSOLookup))]
	[ComImport]
	public interface ISSOLookup1
	{
		// Token: 0x0600293B RID: 10555
		string[] GetCredentials(string applicationName, int flags, out string externalUserName);
	}
}
