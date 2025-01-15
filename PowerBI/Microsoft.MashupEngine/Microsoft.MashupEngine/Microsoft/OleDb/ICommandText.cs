using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001F07 RID: 7943
	[Guid("0c733a27-2a1c-11ce-ade5-00aa0044773d")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ICommandText
	{
		// Token: 0x0600C2B2 RID: 49842
		void Cancel();

		// Token: 0x0600C2B3 RID: 49843
		[PreserveSig]
		unsafe int Execute(IntPtr pUnkOuter, ref Guid iid, DBPARAMS* pParams, DBROWCOUNT* cRowsAffected, out IntPtr ppv);

		// Token: 0x0600C2B4 RID: 49844
		void GetDBSession(ref Guid iid, out IntPtr session);

		// Token: 0x0600C2B5 RID: 49845
		[PreserveSig]
		unsafe int GetCommandText(Guid* pguidDialect, out char* command);

		// Token: 0x0600C2B6 RID: 49846
		[PreserveSig]
		unsafe int SetCommandText(ref Guid guidDialect, char* command);
	}
}
