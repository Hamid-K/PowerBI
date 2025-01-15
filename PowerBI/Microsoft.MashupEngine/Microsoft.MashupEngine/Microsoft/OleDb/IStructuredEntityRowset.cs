using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EAD RID: 7853
	[Guid("557BAE0E-84AB-4324-891D-039417ED193C")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IStructuredEntityRowset
	{
		// Token: 0x0600C211 RID: 49681
		unsafe void GetEntityColumnInfo(DBORDINAL cOrdinals, DBORDINAL* nativeOrdinals, out DBORDINAL countColumnInfos, out EntityDbcolumninfo* nativeColumnInfos, out char* nativeStrings);

		// Token: 0x0600C212 RID: 49682
		void BindAccessor(HACCESSOR hAccessor, DBORDINAL dbOrdinal, HACCESSOR hAccessorChild);
	}
}
