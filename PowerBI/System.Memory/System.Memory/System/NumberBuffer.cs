using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x0200000B RID: 11
	internal ref struct NumberBuffer
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002C29 File Offset: 0x00000E29
		public Span<byte> Digits
		{
			get
			{
				return new Span<byte>(Unsafe.AsPointer<byte>(ref this._b0), 51);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002C3D File Offset: 0x00000E3D
		public unsafe byte* UnsafeDigits
		{
			get
			{
				return (byte*)Unsafe.AsPointer<byte>(ref this._b0);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002C4A File Offset: 0x00000E4A
		public int NumDigits
		{
			get
			{
				return this.Digits.IndexOf(0);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002C58 File Offset: 0x00000E58
		[Conditional("DEBUG")]
		public void CheckConsistency()
		{
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C5C File Offset: 0x00000E5C
		public unsafe override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			stringBuilder.Append('"');
			Span<byte> digits = this.Digits;
			for (int i = 0; i < 51; i++)
			{
				byte b = *digits[i];
				if (b == 0)
				{
					break;
				}
				stringBuilder.Append((char)b);
			}
			stringBuilder.Append('"');
			stringBuilder.Append(", Scale = " + this.Scale);
			stringBuilder.Append(", IsNegative   = " + this.IsNegative.ToString());
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x04000018 RID: 24
		public int Scale;

		// Token: 0x04000019 RID: 25
		public bool IsNegative;

		// Token: 0x0400001A RID: 26
		public const int BufferSize = 51;

		// Token: 0x0400001B RID: 27
		private byte _b0;

		// Token: 0x0400001C RID: 28
		private byte _b1;

		// Token: 0x0400001D RID: 29
		private byte _b2;

		// Token: 0x0400001E RID: 30
		private byte _b3;

		// Token: 0x0400001F RID: 31
		private byte _b4;

		// Token: 0x04000020 RID: 32
		private byte _b5;

		// Token: 0x04000021 RID: 33
		private byte _b6;

		// Token: 0x04000022 RID: 34
		private byte _b7;

		// Token: 0x04000023 RID: 35
		private byte _b8;

		// Token: 0x04000024 RID: 36
		private byte _b9;

		// Token: 0x04000025 RID: 37
		private byte _b10;

		// Token: 0x04000026 RID: 38
		private byte _b11;

		// Token: 0x04000027 RID: 39
		private byte _b12;

		// Token: 0x04000028 RID: 40
		private byte _b13;

		// Token: 0x04000029 RID: 41
		private byte _b14;

		// Token: 0x0400002A RID: 42
		private byte _b15;

		// Token: 0x0400002B RID: 43
		private byte _b16;

		// Token: 0x0400002C RID: 44
		private byte _b17;

		// Token: 0x0400002D RID: 45
		private byte _b18;

		// Token: 0x0400002E RID: 46
		private byte _b19;

		// Token: 0x0400002F RID: 47
		private byte _b20;

		// Token: 0x04000030 RID: 48
		private byte _b21;

		// Token: 0x04000031 RID: 49
		private byte _b22;

		// Token: 0x04000032 RID: 50
		private byte _b23;

		// Token: 0x04000033 RID: 51
		private byte _b24;

		// Token: 0x04000034 RID: 52
		private byte _b25;

		// Token: 0x04000035 RID: 53
		private byte _b26;

		// Token: 0x04000036 RID: 54
		private byte _b27;

		// Token: 0x04000037 RID: 55
		private byte _b28;

		// Token: 0x04000038 RID: 56
		private byte _b29;

		// Token: 0x04000039 RID: 57
		private byte _b30;

		// Token: 0x0400003A RID: 58
		private byte _b31;

		// Token: 0x0400003B RID: 59
		private byte _b32;

		// Token: 0x0400003C RID: 60
		private byte _b33;

		// Token: 0x0400003D RID: 61
		private byte _b34;

		// Token: 0x0400003E RID: 62
		private byte _b35;

		// Token: 0x0400003F RID: 63
		private byte _b36;

		// Token: 0x04000040 RID: 64
		private byte _b37;

		// Token: 0x04000041 RID: 65
		private byte _b38;

		// Token: 0x04000042 RID: 66
		private byte _b39;

		// Token: 0x04000043 RID: 67
		private byte _b40;

		// Token: 0x04000044 RID: 68
		private byte _b41;

		// Token: 0x04000045 RID: 69
		private byte _b42;

		// Token: 0x04000046 RID: 70
		private byte _b43;

		// Token: 0x04000047 RID: 71
		private byte _b44;

		// Token: 0x04000048 RID: 72
		private byte _b45;

		// Token: 0x04000049 RID: 73
		private byte _b46;

		// Token: 0x0400004A RID: 74
		private byte _b47;

		// Token: 0x0400004B RID: 75
		private byte _b48;

		// Token: 0x0400004C RID: 76
		private byte _b49;

		// Token: 0x0400004D RID: 77
		private byte _b50;
	}
}
