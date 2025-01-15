using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EFB RID: 7931
	[Guid("0c733a85-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ISessionProperties
	{
		// Token: 0x0600C293 RID: 49811
		[PreserveSig]
		unsafe int GetProperties(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets);

		// Token: 0x0600C294 RID: 49812
		[PreserveSig]
		unsafe int SetProperties(uint cPropertySets, DBPROPSET* rgPropertySets);
	}
}
