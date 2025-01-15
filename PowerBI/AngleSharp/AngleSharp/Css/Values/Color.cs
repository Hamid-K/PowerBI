using System;
using System.Runtime.InteropServices;
using AngleSharp.Extensions;

namespace AngleSharp.Css.Values
{
	// Token: 0x02000113 RID: 275
	[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode, Pack = 1)]
	public struct Color : IEquatable<Color>, IComparable<Color>, IFormattable
	{
		// Token: 0x060008CA RID: 2250 RVA: 0x0003D3DD File Offset: 0x0003B5DD
		public Color(byte r, byte g, byte b)
		{
			this._hashcode = 0;
			this._alpha = byte.MaxValue;
			this._red = r;
			this._blue = b;
			this._green = g;
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0003D406 File Offset: 0x0003B606
		public Color(byte r, byte g, byte b, byte a)
		{
			this._hashcode = 0;
			this._alpha = a;
			this._red = r;
			this._blue = b;
			this._green = g;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0003D42C File Offset: 0x0003B62C
		public static Color FromRgba(byte r, byte g, byte b, float a)
		{
			return new Color(r, g, b, Color.Normalize(a));
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0003D43C File Offset: 0x0003B63C
		public static Color FromRgba(float r, float g, float b, float a)
		{
			return new Color(Color.Normalize(r), Color.Normalize(g), Color.Normalize(b), Color.Normalize(a));
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0003D45B File Offset: 0x0003B65B
		public static Color FromGray(byte number, float alpha = 1f)
		{
			return new Color(number, number, number, Color.Normalize(alpha));
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0003D46B File Offset: 0x0003B66B
		public static Color FromGray(float value, float alpha = 1f)
		{
			return Color.FromGray(Color.Normalize(value), alpha);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0003D479 File Offset: 0x0003B679
		public static Color? FromName(string name)
		{
			return Colors.GetColor(name);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0003D481 File Offset: 0x0003B681
		public static Color FromRgb(byte r, byte g, byte b)
		{
			return new Color(r, g, b);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0003D48C File Offset: 0x0003B68C
		public static Color FromHex(string color)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 255;
			switch (color.Length)
			{
			case 3:
				break;
			case 4:
				num4 = 17 * color[3].FromHex();
				break;
			case 5:
			case 7:
				goto IL_00F0;
			case 6:
				goto IL_0099;
			case 8:
				num4 = 16 * color[6].FromHex() + color[7].FromHex();
				goto IL_0099;
			default:
				goto IL_00F0;
			}
			num = 17 * color[0].FromHex();
			num2 = 17 * color[1].FromHex();
			num3 = 17 * color[2].FromHex();
			goto IL_00F0;
			IL_0099:
			num = 16 * color[0].FromHex() + color[1].FromHex();
			num2 = 16 * color[2].FromHex() + color[3].FromHex();
			num3 = 16 * color[4].FromHex() + color[5].FromHex();
			IL_00F0:
			return new Color((byte)num, (byte)num2, (byte)num3, (byte)num4);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0003D598 File Offset: 0x0003B798
		public static bool TryFromHex(string color, out Color value)
		{
			if (color.Length == 6 || color.Length == 3 || color.Length == 8 || color.Length == 4)
			{
				for (int i = 0; i < color.Length; i++)
				{
					if (!color[i].IsHex())
					{
						goto IL_0051;
					}
				}
				value = Color.FromHex(color);
				return true;
			}
			IL_0051:
			value = default(Color);
			return false;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0003D600 File Offset: 0x0003B800
		public static Color FromFlexHex(string color)
		{
			int num = Math.Max(color.Length, 3);
			int num2 = num % 3;
			if (num2 != 0)
			{
				num += 3 - num2;
			}
			int num3 = num / 3;
			int num4 = Math.Min(2, num3);
			int num5 = Math.Max(num3 - 8, 0);
			char[] array = new char[num];
			for (int i = 0; i < color.Length; i++)
			{
				array[i] = (color[i].IsHex() ? color[i] : '0');
			}
			for (int j = color.Length; j < num; j++)
			{
				array[j] = '0';
			}
			if (num4 == 1)
			{
				byte b = (byte)array[num5].FromHex();
				int num6 = array[num3 + num5].FromHex();
				int num7 = array[2 * num3 + num5].FromHex();
				return new Color(b, (byte)num6, (byte)num7);
			}
			byte b2 = (byte)(16 * array[num5].FromHex() + array[num5 + 1].FromHex());
			int num8 = 16 * array[num3 + num5].FromHex() + array[num3 + num5 + 1].FromHex();
			int num9 = 16 * array[2 * num3 + num5].FromHex() + array[2 * num3 + num5 + 1].FromHex();
			return new Color(b2, (byte)num8, (byte)num9);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0003D73A File Offset: 0x0003B93A
		public static Color FromHsl(float h, float s, float l)
		{
			return Color.FromHsla(h, s, l, 1f);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0003D74C File Offset: 0x0003B94C
		public static Color FromHsla(float h, float s, float l, float alpha)
		{
			float num = ((l <= 0.5f) ? (l * (s + 1f)) : (l + s - l * s));
			float num2 = 2f * l - num;
			byte b = Color.Convert(Color.HueToRgb(num2, num, h + 0.33333334f));
			byte b2 = Color.Convert(Color.HueToRgb(num2, num, h));
			byte b3 = Color.Convert(Color.HueToRgb(num2, num, h - 0.33333334f));
			return new Color(b, b2, b3, Color.Normalize(alpha));
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0003D7C0 File Offset: 0x0003B9C0
		public static Color FromHwb(float h, float w, float b)
		{
			return Color.FromHwba(h, w, b, 1f);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0003D7D0 File Offset: 0x0003B9D0
		public static Color FromHwba(float h, float w, float b, float alpha)
		{
			float num = 1f / (w + b);
			if (num < 1f)
			{
				w *= num;
				b *= num;
			}
			int num2 = (int)(6f * h);
			float num3 = 6f * h - (float)num2;
			if ((num2 & 1) != 0)
			{
				num3 = 1f - num3;
			}
			float num4 = 1f - b;
			float num5 = w + num3 * (num4 - w);
			float num6;
			float num7;
			float num8;
			switch (num2)
			{
			default:
				num6 = num4;
				num7 = num5;
				num8 = w;
				break;
			case 1:
				num6 = num5;
				num7 = num4;
				num8 = w;
				break;
			case 2:
				num6 = w;
				num7 = num4;
				num8 = num5;
				break;
			case 3:
				num6 = w;
				num7 = num5;
				num8 = num4;
				break;
			case 4:
				num6 = num5;
				num7 = w;
				num8 = num4;
				break;
			case 5:
				num6 = num4;
				num7 = w;
				num8 = num5;
				break;
			}
			return Color.FromRgba(num6, num7, num8, alpha);
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0003D8AC File Offset: 0x0003BAAC
		public int Value
		{
			get
			{
				return this._hashcode;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x0003D8B4 File Offset: 0x0003BAB4
		public byte A
		{
			get
			{
				return this._alpha;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x0003D8BC File Offset: 0x0003BABC
		public double Alpha
		{
			get
			{
				return (double)this._alpha / 255.0;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x0003D8CF File Offset: 0x0003BACF
		public byte R
		{
			get
			{
				return this._red;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x0003D8D7 File Offset: 0x0003BAD7
		public byte G
		{
			get
			{
				return this._green;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x0003D8DF File Offset: 0x0003BADF
		public byte B
		{
			get
			{
				return this._blue;
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0003D8E7 File Offset: 0x0003BAE7
		public static bool operator ==(Color a, Color b)
		{
			return a._hashcode == b._hashcode;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0003D8F7 File Offset: 0x0003BAF7
		public static bool operator !=(Color a, Color b)
		{
			return a._hashcode != b._hashcode;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0003D8E7 File Offset: 0x0003BAE7
		public bool Equals(Color other)
		{
			return this._hashcode == other._hashcode;
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0003D90C File Offset: 0x0003BB0C
		public override bool Equals(object obj)
		{
			Color? color = obj as Color?;
			return color != null && this.Equals(color.Value);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0003D93D File Offset: 0x0003BB3D
		int IComparable<Color>.CompareTo(Color other)
		{
			return this._hashcode - other._hashcode;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0003D8AC File Offset: 0x0003BAAC
		public override int GetHashCode()
		{
			return this._hashcode;
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0003D94C File Offset: 0x0003BB4C
		public static Color Mix(Color above, Color below)
		{
			return Color.Mix(above.Alpha, above, below);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0003D95C File Offset: 0x0003BB5C
		public static Color Mix(double alpha, Color above, Color below)
		{
			double num = 1.0 - alpha;
			double num2 = num * (double)below.R + alpha * (double)above.R;
			double num3 = num * (double)below.G + alpha * (double)above.G;
			double num4 = num * (double)below.B + alpha * (double)above.B;
			return new Color((byte)num2, (byte)num3, (byte)num4);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0003D9C0 File Offset: 0x0003BBC0
		private static byte Normalize(float value)
		{
			return (byte)Math.Max(Math.Min(Math.Round((double)(255f * value)), 255.0), 0.0);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0003D9EC File Offset: 0x0003BBEC
		private static byte Convert(float value)
		{
			return (byte)Math.Round((double)(255f * value));
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0003D9FC File Offset: 0x0003BBFC
		private static float HueToRgb(float m1, float m2, float h)
		{
			if (h < 0f)
			{
				h += 1f;
			}
			else if (h > 1f)
			{
				h -= 1f;
			}
			if (h < 0.16666667f)
			{
				return m1 + (m2 - m1) * h * 6f;
			}
			if ((double)h < 0.5)
			{
				return m2;
			}
			if (h < 0.6666667f)
			{
				return m1 + (m2 - m1) * (0.6666667f - h) * 6f;
			}
			return m1;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0003DA70 File Offset: 0x0003BC70
		public override string ToString()
		{
			if (this._alpha == 255)
			{
				string text = string.Join(", ", new string[]
				{
					this.R.ToString(),
					this.G.ToString(),
					this.B.ToString()
				});
				return FunctionNames.Rgb.CssFunction(text);
			}
			string text2 = string.Join(", ", new string[]
			{
				this.R.ToString(),
				this.G.ToString(),
				this.B.ToString(),
				this.Alpha.ToString()
			});
			return FunctionNames.Rgba.CssFunction(text2);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0003DB3C File Offset: 0x0003BD3C
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (this._alpha == 255)
			{
				string text = string.Join(", ", new string[]
				{
					this.R.ToString(format, formatProvider),
					this.G.ToString(format, formatProvider),
					this.B.ToString(format, formatProvider)
				});
				return FunctionNames.Rgb.CssFunction(text);
			}
			string text2 = string.Join(", ", new string[]
			{
				this.R.ToString(format, formatProvider),
				this.G.ToString(format, formatProvider),
				this.B.ToString(format, formatProvider),
				this.Alpha.ToString(format, formatProvider)
			});
			return FunctionNames.Rgba.CssFunction(text2);
		}

		// Token: 0x0400088B RID: 2187
		public static readonly Color Black = new Color(0, 0, 0);

		// Token: 0x0400088C RID: 2188
		public static readonly Color White = new Color(byte.MaxValue, byte.MaxValue, byte.MaxValue);

		// Token: 0x0400088D RID: 2189
		public static readonly Color Red = new Color(byte.MaxValue, 0, 0);

		// Token: 0x0400088E RID: 2190
		public static readonly Color Magenta = new Color(byte.MaxValue, 0, byte.MaxValue);

		// Token: 0x0400088F RID: 2191
		public static readonly Color Green = new Color(0, 128, 0);

		// Token: 0x04000890 RID: 2192
		public static readonly Color PureGreen = new Color(0, byte.MaxValue, 0);

		// Token: 0x04000891 RID: 2193
		public static readonly Color Blue = new Color(0, 0, byte.MaxValue);

		// Token: 0x04000892 RID: 2194
		public static readonly Color Transparent = new Color(0, 0, 0, 0);

		// Token: 0x04000893 RID: 2195
		[FieldOffset(0)]
		private readonly byte _alpha;

		// Token: 0x04000894 RID: 2196
		[FieldOffset(1)]
		private readonly byte _red;

		// Token: 0x04000895 RID: 2197
		[FieldOffset(2)]
		private readonly byte _green;

		// Token: 0x04000896 RID: 2198
		[FieldOffset(3)]
		private readonly byte _blue;

		// Token: 0x04000897 RID: 2199
		[FieldOffset(0)]
		private readonly int _hashcode;
	}
}
