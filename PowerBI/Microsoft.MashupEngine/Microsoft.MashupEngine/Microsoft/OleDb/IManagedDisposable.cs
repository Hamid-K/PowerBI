using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001F0D RID: 7949
	[Guid("835DA3EA-448F-47BE-8A60-67B2D1430297")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IManagedDisposable
	{
		// Token: 0x0600C2BF RID: 49855
		void Dispose();
	}
}
