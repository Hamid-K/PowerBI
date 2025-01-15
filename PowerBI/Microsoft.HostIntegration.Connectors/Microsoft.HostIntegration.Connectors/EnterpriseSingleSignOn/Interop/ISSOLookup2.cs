using System;
using System.Runtime.InteropServices;

namespace Microsoft.EnterpriseSingleSignOn.Interop
{
	// Token: 0x020004AF RID: 1199
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("DBD1E8D2-4743-4d22-9B74-7929E0BDC3E1")]
	[CoClass(typeof(SSOLookup))]
	[ComImport]
	public interface ISSOLookup2
	{
		// Token: 0x0600293C RID: 10556
		[DispId(1)]
		[return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)]
		string[] GetCredentials([MarshalAs(UnmanagedType.BStr)] string applicationName, int flags, [MarshalAs(UnmanagedType.BStr)] out string externalUserName);

		// Token: 0x0600293D RID: 10557
		[DispId(2)]
		[return: MarshalAs(UnmanagedType.U8)]
		long LogonExternalUser([MarshalAs(UnmanagedType.BStr)] string applicationName, [MarshalAs(UnmanagedType.BStr)] string userName, int flags, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] [In] ref string[] externalCredentials);
	}
}
