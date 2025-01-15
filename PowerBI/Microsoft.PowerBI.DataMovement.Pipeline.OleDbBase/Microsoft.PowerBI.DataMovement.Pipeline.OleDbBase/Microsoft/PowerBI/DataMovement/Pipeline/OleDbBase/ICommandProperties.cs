using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000083 RID: 131
	[Guid("0c733a79-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ICommandProperties
	{
		// Token: 0x060002DB RID: 731
		[PreserveSig]
		unsafe int GetProperties(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets);

		// Token: 0x060002DC RID: 732
		[PreserveSig]
		unsafe int SetProperties(uint propertySetCount, DBPROPSET* propertySets);
	}
}
