using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000077 RID: 119
	[Guid("0c733a8c-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IAccessor
	{
		// Token: 0x060002B5 RID: 693
		unsafe void AddRefAccessor(HACCESSOR accessor, uint* refCount);

		// Token: 0x060002B6 RID: 694
		unsafe void CreateAccessor(DBACCESSORFLAGS acessorFlags, DBCOUNTITEM bindingCount, DBBINDING* bindings, DBLENGTH rowSize, out HACCESSOR accessor, DBBINDSTATUS* status);

		// Token: 0x060002B7 RID: 695
		unsafe void GetBindings(HACCESSOR accessor, out DBACCESSORFLAGS accessorFlags, out DBCOUNTITEM bindingCount, out DBBINDING* bindings);

		// Token: 0x060002B8 RID: 696
		unsafe void ReleaseAccessor(HACCESSOR accessor, uint* refCount);
	}
}
