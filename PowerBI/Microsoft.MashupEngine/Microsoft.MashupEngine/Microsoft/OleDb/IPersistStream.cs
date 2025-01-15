using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.OleDb
{
	// Token: 0x02001EF4 RID: 7924
	[Guid("00000109-0000-0000-C000-000000000046")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IPersistStream : IPersist
	{
		// Token: 0x0600C280 RID: 49792
		void GetClassID(out Guid pClassID);

		// Token: 0x0600C281 RID: 49793
		[PreserveSig]
		int IsDirty();

		// Token: 0x0600C282 RID: 49794
		void Load([MarshalAs(UnmanagedType.Interface)] [In] IStream pstm);

		// Token: 0x0600C283 RID: 49795
		void Save([MarshalAs(UnmanagedType.Interface)] [In] IStream pstm, [In] int fClearDirty);

		// Token: 0x0600C284 RID: 49796
		void GetSizeMax(out ULARGE_INTEGER pcbSize);
	}
}
