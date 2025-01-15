using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EFD RID: 7933
	[Guid("0c733a7b-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IDBSchemaRowset
	{
		// Token: 0x0600C297 RID: 49815
		[PreserveSig]
		unsafe int GetRowset(IntPtr punkOuter, ref Guid guidSchema, uint cRestrictions, VARIANT* rgRestrictions, ref Guid iid, uint cPropertySets, DBPROPSET* rgPropertySets, out IntPtr rowset);

		// Token: 0x0600C298 RID: 49816
		unsafe void GetSchemas(out uint cSchemas, out Guid* rgSchemas, out uint* rgRestrictionSupport);
	}
}
