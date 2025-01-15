using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000088 RID: 136
	[Guid("835DA3EA-448F-47BE-8A60-67B2D1430297")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IManagedDisposable
	{
		// Token: 0x060002E3 RID: 739
		void Dispose();
	}
}
