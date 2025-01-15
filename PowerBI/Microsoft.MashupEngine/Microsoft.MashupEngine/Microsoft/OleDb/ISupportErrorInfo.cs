using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001F0B RID: 7947
	[Guid("DF0B3D60-548F-101B-8E65-08002B2BD119")]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ISupportErrorInfo
	{
		// Token: 0x0600C2BC RID: 49852
		[PreserveSig]
		int InterfaceSupportsErrorInfo(ref Guid iid);
	}
}
