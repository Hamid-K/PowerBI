using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200009C RID: 156
	[StructLayout(LayoutKind.Explicit, Size = 32)]
	public struct DBID
	{
		// Token: 0x040002DA RID: 730
		[FieldOffset(0)]
		public Guid Guid;

		// Token: 0x040002DB RID: 731
		[FieldOffset(0)]
		public unsafe Guid* GuidPointer;

		// Token: 0x040002DC RID: 732
		[FieldOffset(16)]
		public DBKIND Kind;

		// Token: 0x040002DD RID: 733
		[FieldOffset(24)]
		public unsafe char* Name;

		// Token: 0x040002DE RID: 734
		[FieldOffset(24)]
		public uint PropId;
	}
}
