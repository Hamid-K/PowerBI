using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x0200012F RID: 303
	internal sealed class BorderRadiusConverter : IValueConverter
	{
		// Token: 0x060009A5 RID: 2469 RVA: 0x0003F4A4 File Offset: 0x0003D6A4
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			List<CssToken> list = new List<CssToken>();
			List<CssToken> list2 = new List<CssToken>();
			List<CssToken> list3 = list;
			foreach (CssToken cssToken in value)
			{
				if (cssToken.Type == CssTokenType.Delim && cssToken.Data.Is("/"))
				{
					if (list3 == list2)
					{
						return null;
					}
					list3 = list2;
				}
				else
				{
					list3.Add(cssToken);
				}
			}
			IPropertyValue propertyValue = this._converter.Convert(list);
			if (propertyValue == null)
			{
				return null;
			}
			IPropertyValue propertyValue2 = ((list3 == list2) ? this._converter.Convert(list2) : propertyValue);
			if (propertyValue2 == null)
			{
				return null;
			}
			return new BorderRadiusConverter.BorderRadiusValue(propertyValue, propertyValue2, new CssValue(value));
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0003F56C File Offset: 0x0003D76C
		public IPropertyValue Construct(CssProperty[] properties)
		{
			if (properties.Length == 4)
			{
				List<CssToken> list = new List<CssToken>();
				List<CssToken> list2 = new List<CssToken>();
				List<CssProperty> list3 = new List<CssProperty>();
				list3.Add(properties.First((CssProperty m) => m.Name.Is(PropertyNames.BorderTopLeftRadius)));
				list3.Add(properties.First((CssProperty m) => m.Name.Is(PropertyNames.BorderTopRightRadius)));
				list3.Add(properties.First((CssProperty m) => m.Name.Is(PropertyNames.BorderBottomRightRadius)));
				list3.Add(properties.First((CssProperty m) => m.Name.Is(PropertyNames.BorderBottomLeftRadius)));
				List<CssProperty> list4 = list3;
				for (int i = 0; i < list4.Count; i++)
				{
					IEnumerable<IPropertyValue> enumerable = list4[i].DeclaredValue as IEnumerable<IPropertyValue>;
					if (enumerable == null)
					{
						return null;
					}
					CssValue original = enumerable.First<IPropertyValue>().Original;
					CssValue original2 = enumerable.Last<IPropertyValue>().Original;
					if (i != 0)
					{
						list.Add(CssToken.Whitespace);
						list2.Add(CssToken.Whitespace);
					}
					list.AddRange(original);
					list2.AddRange(original2);
				}
				IPropertyValue propertyValue = this._converter.Convert(list);
				IPropertyValue propertyValue2 = this._converter.Convert(list2);
				IEnumerable<CssToken> enumerable2 = list.Concat(new CssToken(CssTokenType.Delim, "/", TextPosition.Empty)).Concat(list2);
				return new BorderRadiusConverter.BorderRadiusValue(propertyValue, propertyValue2, new CssValue(enumerable2));
			}
			return null;
		}

		// Token: 0x040008E6 RID: 2278
		private readonly IValueConverter _converter = Converters.LengthOrPercentConverter.Periodic(new string[]
		{
			PropertyNames.BorderTopLeftRadius,
			PropertyNames.BorderTopRightRadius,
			PropertyNames.BorderBottomRightRadius,
			PropertyNames.BorderBottomLeftRadius
		});

		// Token: 0x020004B4 RID: 1204
		private sealed class BorderRadiusValue : IPropertyValue
		{
			// Token: 0x06002509 RID: 9481 RVA: 0x000607ED File Offset: 0x0005E9ED
			public BorderRadiusValue(IPropertyValue horizontal, IPropertyValue vertical, CssValue original)
			{
				this._horizontal = horizontal;
				this._vertical = vertical;
				this._original = original;
			}

			// Token: 0x17000A94 RID: 2708
			// (get) Token: 0x0600250A RID: 9482 RVA: 0x0006080C File Offset: 0x0005EA0C
			public string CssText
			{
				get
				{
					string cssText = this._horizontal.CssText;
					if (this._vertical != null)
					{
						string cssText2 = this._vertical.CssText;
						if (cssText != cssText2)
						{
							return cssText + " / " + cssText2;
						}
					}
					return cssText;
				}
			}

			// Token: 0x17000A95 RID: 2709
			// (get) Token: 0x0600250B RID: 9483 RVA: 0x00060850 File Offset: 0x0005EA50
			public CssValue Original
			{
				get
				{
					return this._original;
				}
			}

			// Token: 0x0600250C RID: 9484 RVA: 0x00060858 File Offset: 0x0005EA58
			public CssValue ExtractFor(string name)
			{
				IEnumerable<CssToken> enumerable = this._horizontal.ExtractFor(name);
				CssValue cssValue = this._vertical.ExtractFor(name);
				return new CssValue(enumerable.Concat(CssToken.Whitespace).Concat(cssValue));
			}

			// Token: 0x0400112D RID: 4397
			private readonly IPropertyValue _horizontal;

			// Token: 0x0400112E RID: 4398
			private readonly IPropertyValue _vertical;

			// Token: 0x0400112F RID: 4399
			private readonly CssValue _original;
		}
	}
}
