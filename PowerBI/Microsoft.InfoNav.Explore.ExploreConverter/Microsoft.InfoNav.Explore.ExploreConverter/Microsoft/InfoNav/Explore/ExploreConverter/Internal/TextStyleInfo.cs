using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000088 RID: 136
	internal class TextStyleInfo
	{
		// Token: 0x0600029E RID: 670 RVA: 0x0000C710 File Offset: 0x0000A910
		internal TextStyleInfo(Style style)
		{
			this._size = style.GetPropertyValue("FontSize");
			this._weight = style.GetPropertyValue("FontWeight");
			this._wrapped = style.GetPropertyValue("Wrapped");
			this._fontStyle = style.GetPropertyValue("FontStyle");
			this._alignment = style.GetPropertyValue("TextAlignment");
			this._family = style.GetPropertyValue("FontFamily");
			this._decoration = style.GetPropertyValue("TextDecoration");
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000C79A File Offset: 0x0000A99A
		internal bool HasSize
		{
			get
			{
				return this._size != null;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000C7A5 File Offset: 0x0000A9A5
		internal bool HasFontStyle
		{
			get
			{
				return this._fontStyle != null;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000C7B0 File Offset: 0x0000A9B0
		internal bool HasWeight
		{
			get
			{
				return this._weight != null;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000C7BB File Offset: 0x0000A9BB
		internal bool HasFamily
		{
			get
			{
				return this._family != null;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000C7C6 File Offset: 0x0000A9C6
		internal bool HasWrapped
		{
			get
			{
				return this._wrapped != null;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000C7D1 File Offset: 0x0000A9D1
		internal bool HasTextAlignment
		{
			get
			{
				return this._alignment != null;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000C7DC File Offset: 0x0000A9DC
		internal bool HasTextDecoration
		{
			get
			{
				return this._decoration != null;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000C7E7 File Offset: 0x0000A9E7
		internal string Size
		{
			get
			{
				if (!this.HasSize)
				{
					return TextStyleInfo.defaultFontSize.ToString();
				}
				return this._size;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000C802 File Offset: 0x0000AA02
		internal string Wrapped
		{
			get
			{
				if (!this.HasWrapped)
				{
					return "false";
				}
				return this._wrapped;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000C818 File Offset: 0x0000AA18
		internal string Weight
		{
			get
			{
				if (!this.HasWeight)
				{
					return TextStyleInfo.defaultWeight;
				}
				return this._weight;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000C82E File Offset: 0x0000AA2E
		internal string FontStyle
		{
			get
			{
				if (!this.HasFontStyle)
				{
					return TextStyleInfo.defaultFontStyle;
				}
				return this._fontStyle;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000C844 File Offset: 0x0000AA44
		internal string Family
		{
			get
			{
				if (!this.HasFamily)
				{
					return TextStyleInfo.defaultFontFamily;
				}
				return this._family;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000C85A File Offset: 0x0000AA5A
		internal string TextAlignment
		{
			get
			{
				if (!this.HasTextAlignment)
				{
					return TextStyleInfo.defaultTextAlignment;
				}
				return this._alignment;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000C870 File Offset: 0x0000AA70
		internal string TextDecoration
		{
			get
			{
				if (!this.HasTextDecoration)
				{
					return TextStyleInfo.defaultTextDecoration;
				}
				return this._decoration;
			}
		}

		// Token: 0x040001AC RID: 428
		private static FontSize defaultFontSize = new FontSize(12f, FontSizeUnit.Pixels);

		// Token: 0x040001AD RID: 429
		private static string defaultFontStyle = "Normal";

		// Token: 0x040001AE RID: 430
		private static string defaultFontFamily = "Segoe UI";

		// Token: 0x040001AF RID: 431
		private static string defaultWeight = "Normal";

		// Token: 0x040001B0 RID: 432
		private static string defaultTextAlignment = "Left";

		// Token: 0x040001B1 RID: 433
		private static string defaultTextDecoration = "None";

		// Token: 0x040001B2 RID: 434
		private readonly string _size;

		// Token: 0x040001B3 RID: 435
		private readonly string _wrapped;

		// Token: 0x040001B4 RID: 436
		private readonly string _fontStyle;

		// Token: 0x040001B5 RID: 437
		private readonly string _weight;

		// Token: 0x040001B6 RID: 438
		private readonly string _family;

		// Token: 0x040001B7 RID: 439
		private readonly string _alignment;

		// Token: 0x040001B8 RID: 440
		private readonly string _decoration;
	}
}
