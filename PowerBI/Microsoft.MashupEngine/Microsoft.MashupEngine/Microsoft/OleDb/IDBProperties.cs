using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EF8 RID: 7928
	[Guid("0c733a8a-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IDBProperties
	{
		// Token: 0x0600C28E RID: 49806
		[PreserveSig]
		unsafe int GetProperties(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets);

		// Token: 0x0600C28F RID: 49807
		[PreserveSig]
		unsafe int GetPropertyInfo(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertyInfoSets, out DBPROPINFOSET* nativePropertyInfoSets, char** descriptions);

		// Token: 0x0600C290 RID: 49808
		[PreserveSig]
		unsafe int SetProperties(uint countPropertySets, DBPROPSET* nativePropertySets);
	}
}
