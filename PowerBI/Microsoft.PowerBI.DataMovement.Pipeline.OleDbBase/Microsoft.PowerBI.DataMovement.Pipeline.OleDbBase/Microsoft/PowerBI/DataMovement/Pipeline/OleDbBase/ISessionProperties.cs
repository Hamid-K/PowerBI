using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000073 RID: 115
	[Guid("0c733a85-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ISessionProperties
	{
		// Token: 0x060002AE RID: 686
		[PreserveSig]
		unsafe int GetProperties(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets);

		// Token: 0x060002AF RID: 687
		[PreserveSig]
		unsafe int SetProperties(uint propertySetCount, DBPROPSET* propertySets);
	}
}
