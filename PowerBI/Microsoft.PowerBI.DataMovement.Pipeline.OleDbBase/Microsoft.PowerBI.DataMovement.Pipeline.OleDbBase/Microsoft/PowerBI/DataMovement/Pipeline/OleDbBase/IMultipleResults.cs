using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200007A RID: 122
	[Guid("0c733a90-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IMultipleResults
	{
		// Token: 0x060002C1 RID: 705
		[PreserveSig]
		unsafe int GetResult(IntPtr unkOuter, DBRESULTFLAG resultFlag, ref Guid riid, DBROWCOUNT* rowsAffected, out IntPtr rowset);
	}
}
