using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001F08 RID: 7944
	[Guid("0c733a79-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ICommandProperties
	{
		// Token: 0x0600C2B7 RID: 49847
		[PreserveSig]
		unsafe int GetProperties(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets);

		// Token: 0x0600C2B8 RID: 49848
		[PreserveSig]
		unsafe int SetProperties(uint cPropertySets, DBPROPSET* rgPropertySets);
	}
}
