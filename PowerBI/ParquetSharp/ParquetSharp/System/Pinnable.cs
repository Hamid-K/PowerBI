using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000D3 RID: 211
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class Pinnable<T>
	{
		// Token: 0x0400023A RID: 570
		public T Data;
	}
}
