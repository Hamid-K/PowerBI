using System;
using System.Runtime.InteropServices;

namespace Microsoft.SqlServer.XEvent.Internal
{
	// Token: 0x02000058 RID: 88
	// (Invoke) Token: 0x060001D8 RID: 472
	[return: MarshalAs(UnmanagedType.U1)]
	internal delegate bool BufferProcessor(IntPtr target, IntPtr buffer);
}
