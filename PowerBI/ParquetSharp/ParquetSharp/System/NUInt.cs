using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000D2 RID: 210
	internal struct NUInt
	{
		// Token: 0x0600077C RID: 1916 RVA: 0x00020F6C File Offset: 0x0001F16C
		private NUInt(uint value)
		{
			this._value = value;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00020F78 File Offset: 0x0001F178
		private NUInt(ulong value)
		{
			this._value = value;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00020F84 File Offset: 0x0001F184
		public static implicit operator NUInt(uint value)
		{
			return new NUInt(value);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00020F8C File Offset: 0x0001F18C
		public static implicit operator IntPtr(NUInt value)
		{
			return (IntPtr)value._value;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00020F9C File Offset: 0x0001F19C
		public static explicit operator NUInt(int value)
		{
			return new NUInt((uint)value);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00020FA4 File Offset: 0x0001F1A4
		public unsafe static explicit operator void*(NUInt value)
		{
			return value._value;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00020FAC File Offset: 0x0001F1AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static NUInt operator *(NUInt left, NUInt right)
		{
			if (sizeof(IntPtr) != 4)
			{
				return new NUInt(left._value * right._value);
			}
			return new NUInt(left._value * right._value);
		}

		// Token: 0x04000239 RID: 569
		private unsafe readonly void* _value;
	}
}
