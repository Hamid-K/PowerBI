using System;
using System.Runtime.InteropServices;

namespace System.Threading.Tasks.Dataflow.Internal
{
	// Token: 0x02000040 RID: 64
	[StructLayout(LayoutKind.Explicit, Size = 256)]
	internal struct PaddedInt64
	{
		// Token: 0x0400009B RID: 155
		[FieldOffset(128)]
		internal long Value;
	}
}
