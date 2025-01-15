using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EB6 RID: 7862
	[StructLayout(LayoutKind.Explicit, Size = 24)]
	public struct VARIANT
	{
		// Token: 0x04006200 RID: 25088
		[FieldOffset(0)]
		public VARTYPE vt;

		// Token: 0x04006201 RID: 25089
		[FieldOffset(2)]
		public ushort reserved1;

		// Token: 0x04006202 RID: 25090
		[FieldOffset(4)]
		public ushort reserved2;

		// Token: 0x04006203 RID: 25091
		[FieldOffset(6)]
		public ushort reserved3;

		// Token: 0x04006204 RID: 25092
		[FieldOffset(8)]
		public byte value8;

		// Token: 0x04006205 RID: 25093
		[FieldOffset(8)]
		public ushort value16;

		// Token: 0x04006206 RID: 25094
		[FieldOffset(8)]
		public uint value32;

		// Token: 0x04006207 RID: 25095
		[FieldOffset(8)]
		public ulong value64;

		// Token: 0x04006208 RID: 25096
		[FieldOffset(8)]
		public float valueFloat;

		// Token: 0x04006209 RID: 25097
		[FieldOffset(8)]
		public double valueDouble;

		// Token: 0x0400620A RID: 25098
		[FieldOffset(8)]
		public unsafe void* valuePtr;

		// Token: 0x0400620B RID: 25099
		[FieldOffset(0)]
		public DecimalBlittable valueDecimal;
	}
}
