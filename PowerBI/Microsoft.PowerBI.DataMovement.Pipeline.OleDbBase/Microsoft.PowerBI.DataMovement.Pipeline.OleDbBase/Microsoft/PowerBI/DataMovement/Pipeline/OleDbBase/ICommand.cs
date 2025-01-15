using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000080 RID: 128
	[Guid("0c733a63-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ICommand
	{
		// Token: 0x060002D0 RID: 720
		void Cancel();

		// Token: 0x060002D1 RID: 721
		[PreserveSig]
		unsafe int Execute(IntPtr punkOuter, ref Guid iid, DBPARAMS* parameters, DBROWCOUNT* rowsAffected, out IntPtr ppv);

		// Token: 0x060002D2 RID: 722
		void GetDBSession(ref Guid iid, out IntPtr session);
	}
}
