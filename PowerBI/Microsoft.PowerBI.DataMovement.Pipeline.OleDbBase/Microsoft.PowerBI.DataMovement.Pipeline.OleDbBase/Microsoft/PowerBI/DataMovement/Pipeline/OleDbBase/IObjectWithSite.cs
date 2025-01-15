using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000074 RID: 116
	[Guid("FC4801A3-2BA9-11CF-A229-00AA003D7352")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IObjectWithSite
	{
		// Token: 0x060002B0 RID: 688
		void SetSite(IntPtr punkSite);

		// Token: 0x060002B1 RID: 689
		void GetSite(ref Guid iid, out IntPtr unkSite);
	}
}
