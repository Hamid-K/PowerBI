using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001F12 RID: 7954
	[Guid("0c733a74-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ISQLErrorInfo
	{
		// Token: 0x0600C2CC RID: 49868
		[PreserveSig]
		int GetSQLInfo([MarshalAs(UnmanagedType.BStr)] out string pbstrSQLState, out int plNativeError);
	}
}
