using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001F00 RID: 7936
	[Guid("0c733a55-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IRowsetInfo
	{
		// Token: 0x0600C29E RID: 49822
		[PreserveSig]
		unsafe int GetProperties(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets);

		// Token: 0x0600C29F RID: 49823
		void GetReferencedRowset(DBORDINAL iOrdinal, ref Guid iid, out IntPtr referencedRowset);

		// Token: 0x0600C2A0 RID: 49824
		void GetSpecification(ref Guid iid, out IntPtr specification);
	}
}
