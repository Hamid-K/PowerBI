using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000075 RID: 117
	[Guid("0c733a7b-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IDBSchemaRowset
	{
		// Token: 0x060002B2 RID: 690
		[PreserveSig]
		unsafe int GetRowset(IntPtr punkOuter, ref Guid schema, uint restrictionCount, VARIANT* restrictions, ref Guid iid, uint propertySetCount, DBPROPSET* propertySets, out IntPtr rowset);

		// Token: 0x060002B3 RID: 691
		unsafe void GetSchemas(out uint schemaCount, out Guid* schemas, out uint* restrictionSupport);
	}
}
