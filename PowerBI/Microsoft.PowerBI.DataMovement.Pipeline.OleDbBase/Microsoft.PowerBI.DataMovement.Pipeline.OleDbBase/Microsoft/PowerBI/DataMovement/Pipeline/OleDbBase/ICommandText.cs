using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000081 RID: 129
	[Guid("0c733a27-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ICommandText
	{
		// Token: 0x060002D3 RID: 723
		void Cancel();

		// Token: 0x060002D4 RID: 724
		[PreserveSig]
		unsafe int Execute(IntPtr punkOuter, ref Guid iid, DBPARAMS* parameters, DBROWCOUNT* rowsAffected, out IntPtr ppv);

		// Token: 0x060002D5 RID: 725
		void GetDBSession(ref Guid iid, out IntPtr session);

		// Token: 0x060002D6 RID: 726
		[PreserveSig]
		unsafe int GetCommandText(Guid* dialect, out char* command);

		// Token: 0x060002D7 RID: 727
		[PreserveSig]
		unsafe int SetCommandText(ref Guid dialect, char* command);
	}
}
