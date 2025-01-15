using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000086 RID: 134
	[Guid("DF0B3D60-548F-101B-8E65-08002B2BD119")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ISupportErrorInfo
	{
		// Token: 0x060002E0 RID: 736
		[PreserveSig]
		int InterfaceSupportsErrorInfo(ref Guid iid);
	}
}
