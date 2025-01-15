using System;
using System.Runtime.InteropServices;

namespace Microsoft.SqlServer.XEvent.Linq.Internal
{
	// Token: 0x0200008D RID: 141
	// (Invoke) Token: 0x060001E6 RID: 486
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe delegate bool LogCreateDelegate(char* logPath);
}
