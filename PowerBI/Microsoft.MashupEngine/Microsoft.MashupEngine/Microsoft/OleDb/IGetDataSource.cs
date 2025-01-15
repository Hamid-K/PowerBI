using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EF9 RID: 7929
	[Guid("0c733a75-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IGetDataSource
	{
		// Token: 0x0600C291 RID: 49809
		[PreserveSig]
		int GetDataSource(ref Guid iid, out IntPtr dataSource);
	}
}
