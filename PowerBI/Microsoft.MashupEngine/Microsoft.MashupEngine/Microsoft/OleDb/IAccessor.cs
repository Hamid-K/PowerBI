using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EFF RID: 7935
	[Guid("0c733a8c-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IAccessor
	{
		// Token: 0x0600C29A RID: 49818
		unsafe void AddRefAccessor(HACCESSOR hAccessor, uint* pcRefCount);

		// Token: 0x0600C29B RID: 49819
		unsafe void CreateAccessor(DBACCESSORFLAGS dwAccessorFlags, DBCOUNTITEM cBindings, DBBINDING* rgBindings, DBLENGTH cbRowSize, out HACCESSOR hAccessor, DBBINDSTATUS* rgStatus);

		// Token: 0x0600C29C RID: 49820
		unsafe void GetBindings(HACCESSOR hAccessor, out DBACCESSORFLAGS dwAccessorFlags, out DBCOUNTITEM pcBindings, out DBBINDING* rgBindings);

		// Token: 0x0600C29D RID: 49821
		unsafe void ReleaseAccessor(HACCESSOR hAccessor, uint* pcRefCount);
	}
}
