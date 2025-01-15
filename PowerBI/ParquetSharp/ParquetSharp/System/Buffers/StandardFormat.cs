using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x020000E3 RID: 227
	[System.Memory.IsReadOnly]
	internal struct StandardFormat : IEquatable<StandardFormat>
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x00022D24 File Offset: 0x00020F24
		public char Symbol
		{
			get
			{
				return (char)this._format;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x00022D2C File Offset: 0x00020F2C
		public byte Precision
		{
			get
			{
				return this._precision;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x00022D34 File Offset: 0x00020F34
		public bool HasPrecision
		{
			get
			{
				return this._precision != byte.MaxValue;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x00022D48 File Offset: 0x00020F48
		public bool IsDefault
		{
			get
			{
				return this._format == 0 && this._precision == 0;
			}
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00022D60 File Offset: 0x00020F60
		public StandardFormat(char symbol, byte precision = 255)
		{
			if (precision != 255 && precision > 99)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException_PrecisionTooLarge();
			}
			if (symbol != (char)((byte)symbol))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException_SymbolDoesNotFit();
			}
			this._format = (byte)symbol;
			this._precision = precision;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00022D98 File Offset: 0x00020F98
		public static implicit operator StandardFormat(char symbol)
		{
			return new StandardFormat(symbol, byte.MaxValue);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00022DA8 File Offset: 0x00020FA8
		public unsafe static StandardFormat Parse(ReadOnlySpan<char> format)
		{
			if (format.Length == 0)
			{
				return default(StandardFormat);
			}
			char c = (char)(*format[0]);
			byte b;
			if (format.Length == 1)
			{
				b = byte.MaxValue;
			}
			else
			{
				uint num = 0U;
				for (int i = 1; i < format.Length; i++)
				{
					uint num2 = (uint)(*format[i] - 48);
					if (num2 > 9U)
					{
						throw new FormatException(System.Memory189091.SR.Format(System.Memory189091.SR.Argument_CannotParsePrecision, 99));
					}
					num = num * 10U + num2;
					if (num > 99U)
					{
						throw new FormatException(System.Memory189091.SR.Format(System.Memory189091.SR.Argument_PrecisionTooLarge, 99));
					}
				}
				b = (byte)num;
			}
			return new StandardFormat(c, b);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00022E6C File Offset: 0x0002106C
		public static StandardFormat Parse(string format)
		{
			if (format != null)
			{
				return StandardFormat.Parse(format.AsSpan());
			}
			return default(StandardFormat);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00022E98 File Offset: 0x00021098
		public override bool Equals(object obj)
		{
			if (obj is StandardFormat)
			{
				StandardFormat standardFormat = (StandardFormat)obj;
				return this.Equals(standardFormat);
			}
			return false;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00022EC8 File Offset: 0x000210C8
		public override int GetHashCode()
		{
			return this._format.GetHashCode() ^ this._precision.GetHashCode();
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00022EF8 File Offset: 0x000210F8
		public bool Equals(StandardFormat other)
		{
			return this._format == other._format && this._precision == other._precision;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00022F1C File Offset: 0x0002111C
		public unsafe override string ToString()
		{
			char* ptr = stackalloc char[(UIntPtr)8];
			int num = 0;
			char symbol = this.Symbol;
			if (symbol != '\0')
			{
				ptr[(IntPtr)(num++) * 2] = symbol;
				byte b = this.Precision;
				if (b != 255)
				{
					if (b >= 100)
					{
						ptr[(IntPtr)(num++) * 2] = (char)(48 + b / 100 % 10);
						b %= 100;
					}
					if (b >= 10)
					{
						ptr[(IntPtr)(num++) * 2] = (char)(48 + b / 10 % 10);
						b %= 10;
					}
					ptr[(IntPtr)(num++) * 2] = (char)(48 + b);
				}
			}
			return new string(ptr, 0, num);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00022FC0 File Offset: 0x000211C0
		public static bool operator ==(StandardFormat left, StandardFormat right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00022FCC File Offset: 0x000211CC
		public static bool operator !=(StandardFormat left, StandardFormat right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0400025E RID: 606
		public const byte NoPrecision = 255;

		// Token: 0x0400025F RID: 607
		public const byte MaxPrecision = 99;

		// Token: 0x04000260 RID: 608
		private readonly byte _format;

		// Token: 0x04000261 RID: 609
		private readonly byte _precision;
	}
}
