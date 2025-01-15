using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001F06 RID: 7942
	[Guid("0c733a63-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ICommand
	{
		// Token: 0x0600C2AF RID: 49839
		void Cancel();

		// Token: 0x0600C2B0 RID: 49840
		[PreserveSig]
		unsafe int Execute(IntPtr pUnkOuter, ref Guid iid, DBPARAMS* pParams, DBROWCOUNT* cRowsAffected, out IntPtr ppv);

		// Token: 0x0600C2B1 RID: 49841
		void GetDBSession(ref Guid iid, out IntPtr session);
	}
}
