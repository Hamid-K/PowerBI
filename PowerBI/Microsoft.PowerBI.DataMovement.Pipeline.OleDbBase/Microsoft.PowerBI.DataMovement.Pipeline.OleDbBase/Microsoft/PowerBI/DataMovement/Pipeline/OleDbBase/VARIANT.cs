using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000051 RID: 81
	[StructLayout(LayoutKind.Explicit, Size = 24)]
	public struct VARIANT
	{
		// Token: 0x040000D3 RID: 211
		[FieldOffset(0)]
		public VARTYPE Type;

		// Token: 0x040000D4 RID: 212
		[FieldOffset(2)]
		public ushort Reserved1;

		// Token: 0x040000D5 RID: 213
		[FieldOffset(4)]
		public ushort Reserved2;

		// Token: 0x040000D6 RID: 214
		[FieldOffset(6)]
		public ushort Reserved3;

		// Token: 0x040000D7 RID: 215
		[FieldOffset(8)]
		public uint Value32;

		// Token: 0x040000D8 RID: 216
		[FieldOffset(8)]
		public ulong Value64;

		// Token: 0x040000D9 RID: 217
		[FieldOffset(8)]
		public unsafe void* ValuePointer;
	}
}
