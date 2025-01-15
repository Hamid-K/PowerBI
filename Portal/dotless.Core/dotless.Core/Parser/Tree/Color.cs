using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using dotless.Core.Exceptions;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x0200002E RID: 46
	public class Color : Node, IOperable, IComparable
	{
		// Token: 0x0600019E RID: 414 RVA: 0x00008904 File Offset: 0x00006B04
		public static Color From(string keywordOrHex)
		{
			return Color.GetColorFromKeyword(keywordOrHex) ?? Color.FromHex(keywordOrHex);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00008918 File Offset: 0x00006B18
		public static Color GetColorFromKeyword(string keyword)
		{
			if (keyword == "transparent")
			{
				return new Color(0.0, 0.0, 0.0, 0.0, keyword);
			}
			int num;
			if (Color.Html4Colors.TryGetValue(keyword, out num))
			{
				double num2 = (double)((num >> 16) & 255);
				int num3 = (num >> 8) & 255;
				int num4 = num & 255;
				return new Color(num2, (double)num3, (double)num4, 1.0, keyword);
			}
			return null;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000089A0 File Offset: 0x00006BA0
		public static string GetKeyword(int[] rgb)
		{
			int num = (rgb[0] << 16) + (rgb[1] << 8) + rgb[2];
			string text;
			if (Color.Html4ColorsReverse.TryGetValue(num, out text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000089D0 File Offset: 0x00006BD0
		public static Color FromHex(string hex)
		{
			hex = hex.TrimStart(new char[] { '#' });
			double num = 1.0;
			string text = "#" + hex;
			double[] array;
			if (hex.Length == 8)
			{
				array = Color.ParseRgb(hex.Substring(2));
				num = Color.Parse(hex.Substring(0, 2)) / 255.0;
			}
			else if (hex.Length == 6)
			{
				array = Color.ParseRgb(hex);
			}
			else
			{
				array = (from c in hex.ToCharArray()
					select Color.Parse(c.ToString() + c.ToString())).ToArray<double>();
			}
			return new Color(array, num, text);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00008A80 File Offset: 0x00006C80
		private static double[] ParseRgb(string hex)
		{
			return (from i in Enumerable.Range(0, 3)
				select hex.Substring(i * 2, 2)).Select(new Func<string, double>(Color.Parse)).ToArray<double>();
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00008AC8 File Offset: 0x00006CC8
		private static double Parse(string hex)
		{
			return (double)int.Parse(hex, NumberStyles.HexNumber);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00008AD8 File Offset: 0x00006CD8
		public Color(int color)
		{
			this.RGB = new double[3];
			this.B = (double)(color & 255);
			color >>= 8;
			this.G = (double)(color & 255);
			color >>= 8;
			this.R = (double)(color & 255);
			this.Alpha = 1.0;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00008B48 File Offset: 0x00006D48
		public Color(string hex)
		{
			hex = hex.TrimStart(new char[] { '#' });
			double num = 1.0;
			string text = "#" + hex;
			double[] array;
			if (hex.Length == 8)
			{
				array = Color.ParseRgb(hex.Substring(2));
				num = Color.Parse(hex.Substring(0, 2)) / 255.0;
			}
			else if (hex.Length == 6)
			{
				array = Color.ParseRgb(hex);
			}
			else
			{
				array = (from c in hex.ToCharArray()
					select Color.Parse(c.ToString() + c.ToString())).ToArray<double>();
			}
			this.R = array[0];
			this.G = array[1];
			this.B = array[2];
			this.Alpha = num;
			this._text = text;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00008C2C File Offset: 0x00006E2C
		public Color(IEnumerable<Number> rgb, Number alpha)
		{
			this.RGB = rgb.Select((Number d) => d.Normalize()).ToArray<double>();
			this.Alpha = alpha.Normalize();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00008C87 File Offset: 0x00006E87
		public Color(double r, double g, double b)
			: this(r, g, b, 1.0)
		{
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00008C9B File Offset: 0x00006E9B
		public Color(double r, double g, double b, double alpha)
			: this(new double[] { r, g, b }, alpha)
		{
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00008CB7 File Offset: 0x00006EB7
		public Color(double[] rgb)
			: this(rgb, 1.0)
		{
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00008CC9 File Offset: 0x00006EC9
		public Color(double[] rgb, double alpha)
			: this(rgb, alpha, null)
		{
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00008CD4 File Offset: 0x00006ED4
		public Color(double[] rgb, double alpha, string text)
		{
			this.RGB = rgb.Select((double c) => NumberExtensions.Normalize(c, 255.0)).ToArray<double>();
			this.Alpha = NumberExtensions.Normalize(alpha, 1.0);
			this._text = text;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00008D3F File Offset: 0x00006F3F
		public Color(double red, double green, double blue, double alpha = 1.0, string text = null)
			: this(new double[] { red, green, blue }, alpha, text)
		{
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00008D5D File Offset: 0x00006F5D
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00008D67 File Offset: 0x00006F67
		public double R
		{
			get
			{
				return this.RGB[0];
			}
			set
			{
				this.RGB[0] = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00008D72 File Offset: 0x00006F72
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00008D7C File Offset: 0x00006F7C
		public double G
		{
			get
			{
				return this.RGB[1];
			}
			set
			{
				this.RGB[1] = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00008D87 File Offset: 0x00006F87
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00008D91 File Offset: 0x00006F91
		public double B
		{
			get
			{
				return this.RGB[2];
			}
			set
			{
				this.RGB[2] = value;
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00008D9C File Offset: 0x00006F9C
		private double TransformLinearToSrbg(double linearChannel)
		{
			if (linearChannel > 0.03928)
			{
				return Math.Pow((linearChannel + 0.055) / 1.055, 2.4);
			}
			return linearChannel / 12.92;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00008DDC File Offset: 0x00006FDC
		public double Luma
		{
			get
			{
				double num = this.R / 255.0;
				double num2 = this.G / 255.0;
				double num3 = this.B / 255.0;
				double num4 = this.TransformLinearToSrbg(num);
				double num5 = this.TransformLinearToSrbg(num2);
				double num6 = this.TransformLinearToSrbg(num3);
				return 0.2126 * num4 + 0.7152 * num5 + 0.0722 * num6;
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00008E5B File Offset: 0x0000705B
		protected override Node CloneCore()
		{
			return new Color(this.RGB.ToArray<double>(), this.Alpha);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00008E74 File Offset: 0x00007074
		public override void AppendCSS(Env env)
		{
			if (this._text != null)
			{
				env.Output.Append(this._text);
				return;
			}
			List<int> list = this.ConvertToInt(this.RGB);
			if (this.Alpha < 1.0)
			{
				env.Output.AppendFormat("rgba({0}, {1}, {2}, {3})", new object[]
				{
					list[0],
					list[1],
					list[2],
					this.Alpha
				});
				return;
			}
			string hexString = this.GetHexString(list);
			env.Output.Append(hexString);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00008F22 File Offset: 0x00007122
		private List<int> ConvertToInt(IEnumerable<double> rgb)
		{
			return rgb.Select((double d) => (int)Math.Round(d, MidpointRounding.AwayFromZero)).ToList<int>();
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00008F4E File Offset: 0x0000714E
		private string GetHexString(IEnumerable<int> rgb)
		{
			return "#" + rgb.Select((int i) => i.ToString("x2")).JoinStrings("");
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00008F8C File Offset: 0x0000718C
		public Node Operate(Operation op, Node other)
		{
			Color color = other as Color;
			IOperable operable = other as IOperable;
			if (color == null && operable == null)
			{
				throw new ParsingException(string.Format("Unable to convert right hand side of {0} to a color", op.Operator), op.Location);
			}
			color = color ?? operable.ToColor();
			return new Color((from i in Enumerable.Range(0, 3)
				select Operation.Operate(op.Operator, this.RGB[i], color.RGB[i])).ToArray<double>(), 1.0).ReducedFrom<Node>(new Node[] { this, other });
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00009045 File Offset: 0x00007245
		public Color ToColor()
		{
			return this;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00009048 File Offset: 0x00007248
		public string ToArgb()
		{
			IEnumerable<double> enumerable = new double[] { this.Alpha * 255.0 }.Concat(this.RGB);
			List<int> list = this.ConvertToInt(enumerable);
			return this.GetHexString(list);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000908C File Offset: 0x0000728C
		public int CompareTo(object obj)
		{
			Color color = obj as Color;
			if (color == null)
			{
				return -1;
			}
			if (color.R == this.R && color.G == this.G && color.B == this.B && color.Alpha == this.Alpha)
			{
				return 0;
			}
			if ((768.0 - (color.R + color.G + color.B)) * color.Alpha >= (768.0 - (this.R + this.G + this.B)) * this.Alpha)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000912E File Offset: 0x0000732E
		public static explicit operator Color(Color color)
		{
			if (color == null)
			{
				throw new ArgumentNullException("color");
			}
			return Color.FromArgb((int)Math.Round(color.Alpha * 255.0), (int)color.R, (int)color.G, (int)color.B);
		}

		// Token: 0x04000058 RID: 88
		private static readonly Dictionary<string, int> Html4Colors = new Dictionary<string, int>
		{
			{ "aliceblue", 15792383 },
			{ "antiquewhite", 16444375 },
			{ "aqua", 65535 },
			{ "aquamarine", 8388564 },
			{ "azure", 15794175 },
			{ "beige", 16119260 },
			{ "bisque", 16770244 },
			{ "black", 0 },
			{ "blanchedalmond", 16772045 },
			{ "blue", 255 },
			{ "blueviolet", 9055202 },
			{ "brown", 10824234 },
			{ "burlywood", 14596231 },
			{ "cadetblue", 6266528 },
			{ "chartreuse", 8388352 },
			{ "chocolate", 13789470 },
			{ "coral", 16744272 },
			{ "cornflowerblue", 6591981 },
			{ "cornsilk", 16775388 },
			{ "crimson", 14423100 },
			{ "cyan", 65535 },
			{ "darkblue", 139 },
			{ "darkcyan", 35723 },
			{ "darkgoldenrod", 12092939 },
			{ "darkgray", 11119017 },
			{ "darkgrey", 11119017 },
			{ "darkgreen", 25600 },
			{ "darkkhaki", 12433259 },
			{ "darkmagenta", 9109643 },
			{ "darkolivegreen", 5597999 },
			{ "darkorange", 16747520 },
			{ "darkorchid", 10040012 },
			{ "darkred", 9109504 },
			{ "darksalmon", 15308410 },
			{ "darkseagreen", 9419919 },
			{ "darkslateblue", 4734347 },
			{ "darkslategray", 3100495 },
			{ "darkslategrey", 3100495 },
			{ "darkturquoise", 52945 },
			{ "darkviolet", 9699539 },
			{ "deeppink", 16716947 },
			{ "deepskyblue", 49151 },
			{ "dimgray", 6908265 },
			{ "dimgrey", 6908265 },
			{ "dodgerblue", 2003199 },
			{ "firebrick", 11674146 },
			{ "floralwhite", 16775920 },
			{ "forestgreen", 2263842 },
			{ "fuchsia", 16711935 },
			{ "gainsboro", 14474460 },
			{ "ghostwhite", 16316671 },
			{ "gold", 16766720 },
			{ "goldenrod", 14329120 },
			{ "gray", 8421504 },
			{ "grey", 8421504 },
			{ "green", 32768 },
			{ "greenyellow", 11403055 },
			{ "honeydew", 15794160 },
			{ "hotpink", 16738740 },
			{ "indianred", 13458524 },
			{ "indigo", 4915330 },
			{ "ivory", 16777200 },
			{ "khaki", 15787660 },
			{ "lavender", 15132410 },
			{ "lavenderblush", 16773365 },
			{ "lawngreen", 8190976 },
			{ "lemonchiffon", 16775885 },
			{ "lightblue", 11393254 },
			{ "lightcoral", 15761536 },
			{ "lightcyan", 14745599 },
			{ "lightgoldenrodyellow", 16448210 },
			{ "lightgray", 13882323 },
			{ "lightgrey", 13882323 },
			{ "lightgreen", 9498256 },
			{ "lightpink", 16758465 },
			{ "lightsalmon", 16752762 },
			{ "lightseagreen", 2142890 },
			{ "lightskyblue", 8900346 },
			{ "lightslategray", 7833753 },
			{ "lightslategrey", 7833753 },
			{ "lightsteelblue", 11584734 },
			{ "lightyellow", 16777184 },
			{ "lime", 65280 },
			{ "limegreen", 3329330 },
			{ "linen", 16445670 },
			{ "magenta", 16711935 },
			{ "maroon", 8388608 },
			{ "mediumaquamarine", 6737322 },
			{ "mediumblue", 205 },
			{ "mediumorchid", 12211667 },
			{ "mediumpurple", 9662680 },
			{ "mediumseagreen", 3978097 },
			{ "mediumslateblue", 8087790 },
			{ "mediumspringgreen", 64154 },
			{ "mediumturquoise", 4772300 },
			{ "mediumvioletred", 13047173 },
			{ "midnightblue", 1644912 },
			{ "mintcream", 16121850 },
			{ "mistyrose", 16770273 },
			{ "moccasin", 16770229 },
			{ "navajowhite", 16768685 },
			{ "navy", 128 },
			{ "oldlace", 16643558 },
			{ "olive", 8421376 },
			{ "olivedrab", 7048739 },
			{ "orange", 16753920 },
			{ "orangered", 16729344 },
			{ "orchid", 14315734 },
			{ "palegoldenrod", 15657130 },
			{ "palegreen", 10025880 },
			{ "paleturquoise", 11529966 },
			{ "palevioletred", 14184595 },
			{ "papayawhip", 16773077 },
			{ "peachpuff", 16767673 },
			{ "peru", 13468991 },
			{ "pink", 16761035 },
			{ "plum", 14524637 },
			{ "powderblue", 11591910 },
			{ "purple", 8388736 },
			{ "red", 16711680 },
			{ "rosybrown", 12357519 },
			{ "royalblue", 4286945 },
			{ "saddlebrown", 9127187 },
			{ "salmon", 16416882 },
			{ "sandybrown", 16032864 },
			{ "seagreen", 3050327 },
			{ "seashell", 16774638 },
			{ "sienna", 10506797 },
			{ "silver", 12632256 },
			{ "skyblue", 8900331 },
			{ "slateblue", 6970061 },
			{ "slategray", 7372944 },
			{ "slategrey", 7372944 },
			{ "snow", 16775930 },
			{ "springgreen", 65407 },
			{ "steelblue", 4620980 },
			{ "tan", 13808780 },
			{ "teal", 32896 },
			{ "thistle", 14204888 },
			{ "tomato", 16737095 },
			{ "turquoise", 4251856 },
			{ "violet", 15631086 },
			{ "wheat", 16113331 },
			{ "white", 16777215 },
			{ "whitesmoke", 16119285 },
			{ "yellow", 16776960 },
			{ "yellowgreen", 10145074 }
		};

		// Token: 0x04000059 RID: 89
		private static readonly Dictionary<int, string> Html4ColorsReverse = (from kvp in Color.Html4Colors
			group kvp by kvp.Value).ToDictionary((IGrouping<int, KeyValuePair<string, int>> g) => g.Key, (IGrouping<int, KeyValuePair<string, int>> g) => g.First<KeyValuePair<string, int>>().Key);

		// Token: 0x0400005A RID: 90
		private readonly string _text;

		// Token: 0x0400005B RID: 91
		public readonly double[] RGB = new double[3];

		// Token: 0x0400005C RID: 92
		public readonly double Alpha;
	}
}
