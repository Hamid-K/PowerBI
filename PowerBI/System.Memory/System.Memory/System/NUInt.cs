using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000014 RID: 20
	internal struct NUInt
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00009AFA File Offset: 0x00007CFA
		private NUInt(uint value)
		{
			this._value = value;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00009AFA File Offset: 0x00007CFA
		private NUInt(ulong value)
		{
			this._value = value;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00009B04 File Offset: 0x00007D04
		public static implicit operator NUInt(uint value)
		{
			return new NUInt(value);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00009B0C File Offset: 0x00007D0C
		public static implicit operator IntPtr(NUInt value)
		{
			return (IntPtr)value._value;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00009B04 File Offset: 0x00007D04
		public static explicit operator NUInt(int value)
		{
			return new NUInt((uint)value);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00009B19 File Offset: 0x00007D19
		public unsafe static explicit operator void*(NUInt value)
		{
			return value._value;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00009B21 File Offset: 0x00007D21
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static NUInt operator *(NUInt left, NUInt right)
		{
			if (sizeof(IntPtr) != 4)
			{
				return new NUInt(left._value * right._value);
			}
			return new NUInt(left._value * right._value);
		}

		// Token: 0x04000061 RID: 97
		private unsafe readonly void* _value;
	}
}
