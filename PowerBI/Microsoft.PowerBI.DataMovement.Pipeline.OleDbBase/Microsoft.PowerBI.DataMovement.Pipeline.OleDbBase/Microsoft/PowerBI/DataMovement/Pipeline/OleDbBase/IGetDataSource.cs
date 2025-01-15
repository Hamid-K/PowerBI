using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000071 RID: 113
	[Guid("0c733a75-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IGetDataSource
	{
		// Token: 0x060002AC RID: 684
		[PreserveSig]
		int GetDataSource(ref Guid iid, out IntPtr dataSource);
	}
}
