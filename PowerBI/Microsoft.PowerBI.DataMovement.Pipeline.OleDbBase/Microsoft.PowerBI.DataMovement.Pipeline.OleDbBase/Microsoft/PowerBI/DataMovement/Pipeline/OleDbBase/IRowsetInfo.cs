using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000078 RID: 120
	[Guid("0c733a55-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IRowsetInfo
	{
		// Token: 0x060002B9 RID: 697
		[PreserveSig]
		unsafe int GetProperties(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets);

		// Token: 0x060002BA RID: 698
		void GetReferencedRowset(DBORDINAL ordinal, ref Guid iid, out IntPtr referencedRowset);

		// Token: 0x060002BB RID: 699
		void GetSpecification(ref Guid iid, out IntPtr specification);
	}
}
