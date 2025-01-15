using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EFC RID: 7932
	[Guid("FC4801A3-2BA9-11CF-A229-00AA003D7352")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IObjectWithSite
	{
		// Token: 0x0600C295 RID: 49813
		void SetSite(IntPtr punkSite);

		// Token: 0x0600C296 RID: 49814
		void GetSite(ref Guid iid, out IntPtr punkSite);
	}
}
