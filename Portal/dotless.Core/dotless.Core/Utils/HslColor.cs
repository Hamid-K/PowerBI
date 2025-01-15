using System;
using System.Linq;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Utils
{
	// Token: 0x0200000C RID: 12
	public class HslColor
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002CB0 File Offset: 0x00000EB0
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002CB8 File Offset: 0x00000EB8
		public double Hue
		{
			get
			{
				return this._hue;
			}
			set
			{
				this._hue = value % 1.0;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002CCB File Offset: 0x00000ECB
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002CD3 File Offset: 0x00000ED3
		public double Saturation
		{
			get
			{
				return this._saturation;
			}
			set
			{
				this._saturation = NumberExtensions.Normalize(value);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002CE1 File Offset: 0x00000EE1
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00002CE9 File Offset: 0x00000EE9
		public double Lightness
		{
			get
			{
				return this._lightness;
			}
			set
			{
				this._lightness = NumberExtensions.Normalize(value);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002CF7 File Offset: 0x00000EF7
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002CFF File Offset: 0x00000EFF
		public double Alpha { get; set; }

		// Token: 0x0600006C RID: 108 RVA: 0x00002D08 File Offset: 0x00000F08
		public HslColor(double hue, double saturation, double lightness)
			: this(hue, saturation, lightness, 1.0)
		{
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002D1C File Offset: 0x00000F1C
		public HslColor(double hue, double saturation, double lightness, double alpha)
		{
			this.Hue = hue;
			this.Saturation = saturation;
			this.Lightness = lightness;
			this.Alpha = alpha;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002D44 File Offset: 0x00000F44
		public HslColor(Number hue, Number saturation, Number lightness, Number alpha)
		{
			this.Hue = hue.ToNumber() / 360.0 % 1.0;
			this.Saturation = saturation.Normalize(100.0) / 100.0;
			this.Lightness = lightness.Normalize(100.0) / 100.0;
			this.Alpha = alpha.Normalize();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002DC4 File Offset: 0x00000FC4
		public static HslColor FromRgbColor(Color color)
		{
			double[] array = color.RGB.Select((double x) => x / 255.0).ToArray<double>();
			double num = array.Min();
			double max = array.Max();
			double range = max - num;
			double num2 = (max + num) / 2.0;
			double num3 = 0.0;
			double num4 = 0.0;
			if (range != 0.0)
			{
				if (num2 < 0.5)
				{
					num3 = range / (max + num);
				}
				else
				{
					num3 = range / (2.0 - max - num);
				}
				double[] array2 = array.Select((double x) => ((max - x) / 6.0 + range / 2.0) / range).ToArray<double>();
				if (array[0] == max)
				{
					num4 = array2[2] - array2[1];
				}
				else if (array[1] == max)
				{
					num4 = 0.3333333333333333 + array2[0] - array2[2];
				}
				else if (array[2] == max)
				{
					num4 = 0.6666666666666666 + array2[1] - array2[0];
				}
				if (num4 < 0.0)
				{
					num4 += 1.0;
				}
				if (num4 > 1.0)
				{
					num4 -= 1.0;
				}
			}
			return new HslColor(num4, num3, num2, color.Alpha);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002F58 File Offset: 0x00001158
		public Color ToRgbColor()
		{
			if (this.Saturation == 0.0)
			{
				double num = Math.Round(this.Lightness * 255.0);
				return new Color(num, num, num, this.Alpha);
			}
			double num2;
			if (this.Lightness < 0.5)
			{
				num2 = this.Lightness * (1.0 + this.Saturation);
			}
			else
			{
				num2 = this.Lightness + this.Saturation - this.Saturation * this.Lightness;
			}
			double num3 = 2.0 * this.Lightness - num2;
			double num4 = 255.0 * HslColor.Hue_2_RGB(num3, num2, this.Hue + 0.3333333333333333);
			double num5 = 255.0 * HslColor.Hue_2_RGB(num3, num2, this.Hue);
			double num6 = 255.0 * HslColor.Hue_2_RGB(num3, num2, this.Hue - 0.3333333333333333);
			return new Color(num4, num5, num6, this.Alpha);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000305C File Offset: 0x0000125C
		private static double Hue_2_RGB(double v1, double v2, double vH)
		{
			if (vH < 0.0)
			{
				vH += 1.0;
			}
			if (vH > 1.0)
			{
				vH -= 1.0;
			}
			if (6.0 * vH < 1.0)
			{
				return v1 + (v2 - v1) * 6.0 * vH;
			}
			if (2.0 * vH < 1.0)
			{
				return v2;
			}
			if (3.0 * vH < 2.0)
			{
				return v1 + (v2 - v1) * (0.6666666666666666 - vH) * 6.0;
			}
			return v1;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000310E File Offset: 0x0000130E
		public Number GetHueInDegrees()
		{
			return new Number(this.Hue * 360.0, "deg");
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000312A File Offset: 0x0000132A
		public Number GetSaturation()
		{
			return new Number(this.Saturation * 100.0, "%");
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003146 File Offset: 0x00001346
		public Number GetLightness()
		{
			return new Number(this.Lightness * 100.0, "%");
		}

		// Token: 0x04000014 RID: 20
		private double _hue;

		// Token: 0x04000015 RID: 21
		private double _saturation;

		// Token: 0x04000016 RID: 22
		private double _lightness;
	}
}
