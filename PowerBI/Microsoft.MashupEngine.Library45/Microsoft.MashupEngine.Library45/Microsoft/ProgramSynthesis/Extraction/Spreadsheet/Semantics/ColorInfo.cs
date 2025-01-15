using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics
{
	// Token: 0x02000ECD RID: 3789
	public class ColorInfo : IEquatable<ColorInfo>
	{
		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x06006710 RID: 26384 RVA: 0x0014FE92 File Offset: 0x0014E092
		public bool? Auto { get; }

		// Token: 0x17001259 RID: 4697
		// (get) Token: 0x06006711 RID: 26385 RVA: 0x0014FE9A File Offset: 0x0014E09A
		public int? Indexed { get; }

		// Token: 0x1700125A RID: 4698
		// (get) Token: 0x06006712 RID: 26386 RVA: 0x0014FEA2 File Offset: 0x0014E0A2
		public string Rgb { get; }

		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x06006713 RID: 26387 RVA: 0x0014FEAA File Offset: 0x0014E0AA
		public int? Theme { get; }

		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x06006714 RID: 26388 RVA: 0x0014FEB2 File Offset: 0x0014E0B2
		public double? Tint { get; }

		// Token: 0x06006715 RID: 26389 RVA: 0x0014FEBA File Offset: 0x0014E0BA
		public ColorInfo(bool? auto, int? indexed, string rgb, int? theme, double? tint)
		{
			this.Auto = auto;
			this.Indexed = indexed;
			this.Rgb = rgb;
			this.Theme = theme;
			this.Tint = tint;
		}

		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x06006716 RID: 26390 RVA: 0x0014FEE8 File Offset: 0x0014E0E8
		public bool IsWhite
		{
			get
			{
				if (this.Tint == null)
				{
					int? num = this.Theme;
					int num2 = 0;
					if (!((num.GetValueOrDefault() == num2) & (num != null)))
					{
						num = this.Indexed;
						num2 = 1;
						if (!((num.GetValueOrDefault() == num2) & (num != null)))
						{
							num = this.Indexed;
							num2 = 9;
							if (!((num.GetValueOrDefault() == num2) & (num != null)))
							{
								return this.Rgb != null && this.Rgb.EndsWith("FFFFFF");
							}
						}
					}
					return true;
				}
				return false;
			}
		}

		// Token: 0x06006717 RID: 26391 RVA: 0x0014FF7C File Offset: 0x0014E17C
		public override string ToString()
		{
			List<string> list = new List<string>(5);
			if (this.Auto != null)
			{
				list.Add("auto=" + this.Auto.Value.ToString());
			}
			if (this.Indexed != null)
			{
				list.Add("indexed=" + this.Indexed.Value.ToString());
			}
			if (this.Rgb != null)
			{
				list.Add("RGB=" + this.Rgb);
			}
			if (this.Theme != null)
			{
				list.Add("theme=" + this.Theme.Value.ToString());
			}
			if (this.Tint != null)
			{
				list.Add("tint=" + this.Tint.Value.ToString());
			}
			if (list.Count != 0)
			{
				return "Color(" + string.Join(", ", list) + ")";
			}
			return "Color(null)";
		}

		// Token: 0x06006718 RID: 26392 RVA: 0x001500B4 File Offset: 0x0014E2B4
		public bool Equals(ColorInfo other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			bool? auto = this.Auto;
			bool? auto2 = other.Auto;
			if ((auto.GetValueOrDefault() == auto2.GetValueOrDefault()) & (auto != null == (auto2 != null)))
			{
				int? num = this.Indexed;
				int? num2 = other.Indexed;
				if (((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null))) && this.Rgb == other.Rgb)
				{
					num2 = this.Theme;
					num = other.Theme;
					if ((num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null)))
					{
						return Nullable.Equals<double>(this.Tint, other.Tint);
					}
				}
			}
			return false;
		}

		// Token: 0x06006719 RID: 26393 RVA: 0x00150188 File Offset: 0x0014E388
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((ColorInfo)obj)));
		}

		// Token: 0x0600671A RID: 26394 RVA: 0x001501B8 File Offset: 0x0014E3B8
		public override int GetHashCode()
		{
			return (((((((this.Auto.GetHashCode() * 397) ^ this.Indexed.GetHashCode()) * 397) ^ ((this.Rgb != null) ? this.Rgb.GetHashCode() : 0)) * 397) ^ this.Theme.GetHashCode()) * 397) ^ this.Tint.GetHashCode();
		}

		// Token: 0x0600671B RID: 26395 RVA: 0x00150248 File Offset: 0x0014E448
		// Note: this type is marked as 'beforefieldinit'.
		static ColorInfo()
		{
			int? num = new int?(1);
			ColorInfo.White = new ColorInfo(null, num, null, null, null);
			num = new int?(0);
			ColorInfo.Black = new ColorInfo(null, num, null, null, null);
		}

		// Token: 0x04002D9D RID: 11677
		public static ColorInfo White;

		// Token: 0x04002D9E RID: 11678
		public static ColorInfo Black;
	}
}
