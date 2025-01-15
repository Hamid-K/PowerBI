using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200006F RID: 111
	[Guid("0c733a8a-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IDBProperties
	{
		// Token: 0x060002A3 RID: 675
		[PreserveSig]
		unsafe int GetProperties(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets);

		// Token: 0x060002A4 RID: 676
		[PreserveSig]
		unsafe int GetPropertyInfo(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertyInfoSets, out DBPROPINFOSET* nativePropertyInfoSets, char** descriptions);

		// Token: 0x060002A5 RID: 677
		[PreserveSig]
		unsafe int SetProperties(uint countPropertySets, DBPROPSET* nativePropertySets);
	}
}
