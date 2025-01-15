using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EE4 RID: 7908
	[StructLayout(LayoutKind.Explicit, Size = 32)]
	public struct DBID
	{
		// Token: 0x0400640F RID: 25615
		[FieldOffset(0)]
		public Guid guid;

		// Token: 0x04006410 RID: 25616
		[FieldOffset(0)]
		public unsafe Guid* pguid;

		// Token: 0x04006411 RID: 25617
		[FieldOffset(16)]
		public DBKIND eKind;

		// Token: 0x04006412 RID: 25618
		[FieldOffset(24)]
		public unsafe char* pwszName;

		// Token: 0x04006413 RID: 25619
		[FieldOffset(24)]
		public uint ulPropid;
	}
}
