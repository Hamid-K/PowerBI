using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000015 RID: 21
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class Pinnable<T>
	{
		// Token: 0x04000062 RID: 98
		public T Data;
	}
}
