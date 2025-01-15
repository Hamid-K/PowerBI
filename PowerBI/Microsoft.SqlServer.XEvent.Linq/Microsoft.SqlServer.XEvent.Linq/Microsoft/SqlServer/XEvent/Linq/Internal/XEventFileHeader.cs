using System;
using System.Runtime.InteropServices;

namespace Microsoft.SqlServer.XEvent.Linq.Internal
{
	// Token: 0x0200002E RID: 46
	internal class XEventFileHeader
	{
		// Token: 0x06000183 RID: 387 RVA: 0x0000F25C File Offset: 0x0000F25C
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe static bool IsVersionCompatible(byte[] xeheader)
		{
			return ((*((ref xeheader[0]) + 4L) == 10) ? 1 : 0) != 0;
		}
	}
}
