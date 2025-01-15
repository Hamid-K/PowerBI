using System;

namespace System.Buffers
{
	// Token: 0x02000025 RID: 37
	public readonly struct StandardFormat : IEquatable<StandardFormat>
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000B510 File Offset: 0x00009710
		public char Symbol
		{
			get
			{
				return (char)this._format;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000B518 File Offset: 0x00009718
		public byte Precision
		{
			get
			{
				return this._precision;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000B520 File Offset: 0x00009720
		public bool HasPrecision
		{
			get
			{
				return this._precision != byte.MaxValue;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000B532 File Offset: 0x00009732
		public bool IsDefault
		{
			get
			{
				return this._format == 0 && this._precision == 0;
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000B547 File Offset: 0x00009747
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

		// Token: 0x060001CE RID: 462 RVA: 0x0000B574 File Offset: 0x00009774
		public static implicit operator StandardFormat(char symbol)
		{
			return new StandardFormat(symbol, byte.MaxValue);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000B584 File Offset: 0x00009784
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
						throw new FormatException(SR.Format(SR.Argument_CannotParsePrecision, 99));
					}
					num = num * 10U + num2;
					if (num > 99U)
					{
						throw new FormatException(SR.Format(SR.Argument_PrecisionTooLarge, 99));
					}
				}
				b = (byte)num;
			}
			return new StandardFormat(c, b);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000B634 File Offset: 0x00009834
		public static StandardFormat Parse(string format)
		{
			if (format != null)
			{
				return StandardFormat.Parse(format.AsSpan());
			}
			return default(StandardFormat);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000B65C File Offset: 0x0000985C
		public override bool Equals(object obj)
		{
			if (obj is StandardFormat)
			{
				StandardFormat standardFormat = (StandardFormat)obj;
				return this.Equals(standardFormat);
			}
			return false;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000B684 File Offset: 0x00009884
		public override int GetHashCode()
		{
			return this._format.GetHashCode() ^ this._precision.GetHashCode();
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000B6AE File Offset: 0x000098AE
		public bool Equals(StandardFormat other)
		{
			return this._format == other._format && this._precision == other._precision;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000B6D0 File Offset: 0x000098D0
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

		// Token: 0x060001D5 RID: 469 RVA: 0x0000B763 File Offset: 0x00009963
		public static bool operator ==(StandardFormat left, StandardFormat right)
		{
			return left.Equals(right);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000B76D File Offset: 0x0000996D
		public static bool operator !=(StandardFormat left, StandardFormat right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000086 RID: 134
		public const byte NoPrecision = 255;

		// Token: 0x04000087 RID: 135
		public const byte MaxPrecision = 99;

		// Token: 0x04000088 RID: 136
		private readonly byte _format;

		// Token: 0x04000089 RID: 137
		private readonly byte _precision;
	}
}
