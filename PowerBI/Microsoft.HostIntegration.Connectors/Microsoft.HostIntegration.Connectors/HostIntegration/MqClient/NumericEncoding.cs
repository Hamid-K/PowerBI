using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B2B RID: 2859
	public class NumericEncoding
	{
		// Token: 0x17001593 RID: 5523
		// (get) Token: 0x060059F6 RID: 23030 RVA: 0x00173875 File Offset: 0x00171A75
		// (set) Token: 0x060059F7 RID: 23031 RVA: 0x0017387D File Offset: 0x00171A7D
		public IntegerEncoding IntegerEncoding { get; private set; }

		// Token: 0x17001594 RID: 5524
		// (get) Token: 0x060059F8 RID: 23032 RVA: 0x00173886 File Offset: 0x00171A86
		// (set) Token: 0x060059F9 RID: 23033 RVA: 0x0017388E File Offset: 0x00171A8E
		public PackedDecimalEncoding PackedDecimalEncoding { get; private set; }

		// Token: 0x17001595 RID: 5525
		// (get) Token: 0x060059FA RID: 23034 RVA: 0x00173897 File Offset: 0x00171A97
		// (set) Token: 0x060059FB RID: 23035 RVA: 0x0017389F File Offset: 0x00171A9F
		public FloatingPointEncoding FloatingPointEncoding { get; private set; }

		// Token: 0x17001596 RID: 5526
		// (get) Token: 0x060059FC RID: 23036 RVA: 0x001738A8 File Offset: 0x00171AA8
		// (set) Token: 0x060059FD RID: 23037 RVA: 0x001738B0 File Offset: 0x00171AB0
		public bool IsLittleEndian { get; private set; }

		// Token: 0x17001597 RID: 5527
		// (get) Token: 0x060059FE RID: 23038 RVA: 0x001738B9 File Offset: 0x00171AB9
		// (set) Token: 0x060059FF RID: 23039 RVA: 0x001738C1 File Offset: 0x00171AC1
		public int NumericValue { get; private set; }

		// Token: 0x06005A00 RID: 23040 RVA: 0x001738CC File Offset: 0x00171ACC
		static NumericEncoding()
		{
			NumericEncoding.AddIntegerEncoding(IntegerEncoding.Normal);
			NumericEncoding.AddIntegerEncoding(IntegerEncoding.Reversed);
			NumericEncoding.encodingToNumericEncoding.Add(17, new NumericEncoding(17));
			NumericEncoding.NativeISeriesEncoding = NumericEncoding.encodingToNumericEncoding[273];
			NumericEncoding.NativeLinuxEncoding = NumericEncoding.encodingToNumericEncoding[546];
			NumericEncoding.NativeLinuxOnSparcEncoding = NumericEncoding.encodingToNumericEncoding[273];
			NumericEncoding.NativeLinuxOnX86Encoding = NumericEncoding.encodingToNumericEncoding[546];
			NumericEncoding.NativeSolarisOnSparcEncoding = NumericEncoding.encodingToNumericEncoding[273];
			NumericEncoding.NativeUnixEncoding = NumericEncoding.encodingToNumericEncoding[273];
			NumericEncoding.NativeWindowsEncoding = NumericEncoding.encodingToNumericEncoding[546];
			NumericEncoding.NativeMicroFocusCobolOnWindowsEncoding = NumericEncoding.encodingToNumericEncoding[17];
			NumericEncoding.NativeZOsEncoding = NumericEncoding.encodingToNumericEncoding[785];
			NumericEncoding.encodingToNumericEncoding.Add(0, null);
		}

		// Token: 0x06005A01 RID: 23041 RVA: 0x001739C1 File Offset: 0x00171BC1
		private static void AddIntegerEncoding(IntegerEncoding integerEncoding)
		{
			NumericEncoding.AddPackedDecimalEncoding(PackedDecimalEncoding.Normal, integerEncoding);
			NumericEncoding.AddPackedDecimalEncoding(PackedDecimalEncoding.Reversed, integerEncoding);
		}

		// Token: 0x06005A02 RID: 23042 RVA: 0x001739D3 File Offset: 0x00171BD3
		private static void AddPackedDecimalEncoding(PackedDecimalEncoding packedDecimalEncoding, IntegerEncoding integerEncoding)
		{
			NumericEncoding.AddFloatingPointEncoding(FloatingPointEncoding.IeeeNormal, packedDecimalEncoding, integerEncoding);
			NumericEncoding.AddFloatingPointEncoding(FloatingPointEncoding.IeeeReversed, packedDecimalEncoding, integerEncoding);
			NumericEncoding.AddFloatingPointEncoding(FloatingPointEncoding.S390, packedDecimalEncoding, integerEncoding);
			NumericEncoding.AddFloatingPointEncoding(FloatingPointEncoding.Tns, packedDecimalEncoding, integerEncoding);
		}

		// Token: 0x06005A03 RID: 23043 RVA: 0x00173A08 File Offset: 0x00171C08
		private static void AddFloatingPointEncoding(FloatingPointEncoding floatingPointEncoding, PackedDecimalEncoding packedDecimalEncoding, IntegerEncoding integerEncoding)
		{
			int num = (int)(floatingPointEncoding + (int)packedDecimalEncoding + (int)integerEncoding);
			NumericEncoding.encodingToNumericEncoding.Add(num, new NumericEncoding(num));
		}

		// Token: 0x06005A04 RID: 23044 RVA: 0x00173A2C File Offset: 0x00171C2C
		public NumericEncoding(IntegerEncoding integerEncoding, PackedDecimalEncoding packedDecimalEncoding, FloatingPointEncoding floatingPointEncoding)
		{
			if (integerEncoding != IntegerEncoding.Normal && integerEncoding != IntegerEncoding.Reversed)
			{
				throw new ArgumentException("integerEncoding");
			}
			if (packedDecimalEncoding != PackedDecimalEncoding.Unknown && packedDecimalEncoding != PackedDecimalEncoding.Normal && packedDecimalEncoding != PackedDecimalEncoding.Reversed)
			{
				throw new ArgumentException("packedDecimalEncoding");
			}
			if (floatingPointEncoding != FloatingPointEncoding.Unknown && floatingPointEncoding != FloatingPointEncoding.IeeeNormal && floatingPointEncoding != FloatingPointEncoding.IeeeReversed && floatingPointEncoding != FloatingPointEncoding.S390 && floatingPointEncoding != FloatingPointEncoding.Tns)
			{
				throw new ArgumentException("floatingPointEncoding");
			}
			this.IntegerEncoding = integerEncoding;
			this.PackedDecimalEncoding = packedDecimalEncoding;
			this.FloatingPointEncoding = floatingPointEncoding;
			this.IsLittleEndian = this.IntegerEncoding == IntegerEncoding.Reversed;
			this.NumericValue = (int)(this.IntegerEncoding + (int)this.PackedDecimalEncoding + (int)this.FloatingPointEncoding);
		}

		// Token: 0x06005A05 RID: 23045 RVA: 0x00173AD6 File Offset: 0x00171CD6
		private NumericEncoding(int numericEncodingValue)
			: this(numericEncodingValue, false)
		{
		}

		// Token: 0x06005A06 RID: 23046 RVA: 0x00173AE0 File Offset: 0x00171CE0
		private NumericEncoding(int numericEncodingValue, bool allowBadIntegers)
		{
			this.IntegerEncoding = (IntegerEncoding)(numericEncodingValue & 15);
			if (!allowBadIntegers && this.IntegerEncoding != IntegerEncoding.Normal && this.IntegerEncoding != IntegerEncoding.Reversed)
			{
				throw new ArgumentException("numericEncodingValue");
			}
			this.PackedDecimalEncoding = (PackedDecimalEncoding)(numericEncodingValue & 240);
			this.FloatingPointEncoding = (FloatingPointEncoding)(numericEncodingValue & 3840);
			this.IsLittleEndian = this.IntegerEncoding == IntegerEncoding.Reversed;
			this.NumericValue = (int)(this.IntegerEncoding + (int)this.PackedDecimalEncoding + (int)this.FloatingPointEncoding);
		}

		// Token: 0x06005A07 RID: 23047 RVA: 0x00173B60 File Offset: 0x00171D60
		public static NumericEncoding GetInstance(int numericEncodingValue)
		{
			return NumericEncoding.GetInstance(numericEncodingValue, false);
		}

		// Token: 0x06005A08 RID: 23048 RVA: 0x00173B6C File Offset: 0x00171D6C
		internal static NumericEncoding GetInstance(int numericEncodingValue, bool allowBadIntegers)
		{
			NumericEncoding numericEncoding;
			if (NumericEncoding.encodingToNumericEncoding.TryGetValue(numericEncodingValue, out numericEncoding))
			{
				return numericEncoding;
			}
			if (allowBadIntegers)
			{
				IntegerEncoding integerEncoding = (IntegerEncoding)(numericEncodingValue & 15);
				if (integerEncoding != IntegerEncoding.Normal && integerEncoding != IntegerEncoding.Reversed)
				{
					return null;
				}
			}
			return new NumericEncoding(numericEncodingValue);
		}

		// Token: 0x06005A09 RID: 23049 RVA: 0x00173BA4 File Offset: 0x00171DA4
		internal static NumericEncoding GetAnyInstance(int numericEncodingValue)
		{
			NumericEncoding numericEncoding;
			if (NumericEncoding.encodingToNumericEncoding.TryGetValue(numericEncodingValue, out numericEncoding))
			{
				return numericEncoding;
			}
			return new NumericEncoding(numericEncodingValue, true);
		}

		// Token: 0x06005A0A RID: 23050 RVA: 0x00173BCC File Offset: 0x00171DCC
		public static bool IsValidIntegerEndianness(int numericEncodingValue)
		{
			IntegerEncoding integerEncoding = (IntegerEncoding)(numericEncodingValue & 15);
			return integerEncoding == IntegerEncoding.Normal || integerEncoding == IntegerEncoding.Reversed;
		}

		// Token: 0x06005A0B RID: 23051 RVA: 0x00173BE8 File Offset: 0x00171DE8
		public static bool EncodingValueIsLittleEndian(int numericEncodingValue)
		{
			IntegerEncoding integerEncoding = (IntegerEncoding)(numericEncodingValue & 15);
			if (integerEncoding != IntegerEncoding.Normal && integerEncoding != IntegerEncoding.Reversed)
			{
				throw new ArgumentException("numericEncodingValue");
			}
			return integerEncoding == IntegerEncoding.Reversed;
		}

		// Token: 0x06005A0C RID: 23052 RVA: 0x00173C14 File Offset: 0x00171E14
		public override string ToString()
		{
			string text = ((this.PackedDecimalEncoding == PackedDecimalEncoding.Reversed) ? "Reversed" : ((this.PackedDecimalEncoding == PackedDecimalEncoding.Normal) ? "Normal" : "Unknown"));
			FloatingPointEncoding floatingPointEncoding = this.FloatingPointEncoding;
			string text2;
			if (floatingPointEncoding <= FloatingPointEncoding.IeeeReversed)
			{
				if (floatingPointEncoding == FloatingPointEncoding.IeeeNormal)
				{
					text2 = "Normal";
					goto IL_0081;
				}
				if (floatingPointEncoding == FloatingPointEncoding.IeeeReversed)
				{
					text2 = "Reversed";
					goto IL_0081;
				}
			}
			else
			{
				if (floatingPointEncoding == FloatingPointEncoding.S390)
				{
					text2 = "S390";
					goto IL_0081;
				}
				if (floatingPointEncoding == FloatingPointEncoding.Tns)
				{
					text2 = "TNS";
					goto IL_0081;
				}
			}
			text2 = "Unknown";
			IL_0081:
			return string.Concat(new string[]
			{
				"Int: ",
				this.IsLittleEndian ? "Reversed" : "Normal",
				", PD: ",
				text,
				" FP: ",
				text2
			});
		}

		// Token: 0x04004730 RID: 18224
		private static Dictionary<int, NumericEncoding> encodingToNumericEncoding = new Dictionary<int, NumericEncoding>(17);

		// Token: 0x04004731 RID: 18225
		public static NumericEncoding NativeISeriesEncoding;

		// Token: 0x04004732 RID: 18226
		public static NumericEncoding NativeLinuxEncoding;

		// Token: 0x04004733 RID: 18227
		public static NumericEncoding NativeLinuxOnSparcEncoding;

		// Token: 0x04004734 RID: 18228
		public static NumericEncoding NativeLinuxOnX86Encoding;

		// Token: 0x04004735 RID: 18229
		public static NumericEncoding NativeSolarisOnSparcEncoding;

		// Token: 0x04004736 RID: 18230
		public static NumericEncoding NativeUnixEncoding;

		// Token: 0x04004737 RID: 18231
		public static NumericEncoding NativeWindowsEncoding;

		// Token: 0x04004738 RID: 18232
		public static NumericEncoding NativeMicroFocusCobolOnWindowsEncoding;

		// Token: 0x04004739 RID: 18233
		public static NumericEncoding NativeZOsEncoding;
	}
}
