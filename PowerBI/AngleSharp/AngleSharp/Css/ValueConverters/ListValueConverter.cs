using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x0200013A RID: 314
	internal sealed class ListValueConverter : IValueConverter
	{
		// Token: 0x060009C9 RID: 2505 RVA: 0x0003FE1C File Offset: 0x0003E01C
		public ListValueConverter(IValueConverter converter)
		{
			this._converter = converter;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0003FE2C File Offset: 0x0003E02C
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			List<List<CssToken>> list = value.ToList();
			IPropertyValue[] array = new IPropertyValue[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				array[i] = this._converter.Convert(list[i]);
				if (array[i] == null)
				{
					return null;
				}
			}
			if (array.Length == 1)
			{
				return array[0];
			}
			return new ListValueConverter.ListValue(array, value);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0003FE8C File Offset: 0x0003E08C
		public IPropertyValue Construct(CssProperty[] properties)
		{
			IPropertyValue propertyValue = properties.Guard<ListValueConverter.ListValue>();
			if (propertyValue == null)
			{
				List<List<CssToken>>[] array = new List<List<CssToken>>[properties.Length];
				CssProperty[] array2 = new CssProperty[properties.Length];
				int num = 0;
				for (int i = 0; i < properties.Length; i++)
				{
					IPropertyValue declaredValue = properties[i].DeclaredValue;
					array[i] = ((declaredValue != null) ? declaredValue.Original.ToList() : new List<List<CssToken>>());
					array2[i] = Factory.Properties.CreateLonghand(properties[i].Name);
					num = Math.Max(num, array[i].Count);
				}
				IPropertyValue[] array3 = new IPropertyValue[num];
				for (int j = 0; j < num; j++)
				{
					for (int k = 0; k < array2.Length; k++)
					{
						List<List<CssToken>> list = array[k];
						IEnumerable<CssToken> enumerable;
						if (list.Count <= j)
						{
							enumerable = Enumerable.Empty<CssToken>();
						}
						else
						{
							IEnumerable<CssToken> enumerable2 = list[j];
							enumerable = enumerable2;
						}
						IEnumerable<CssToken> enumerable3 = enumerable;
						array2[k].TrySetValue(new CssValue(enumerable3));
					}
					array3[j] = this._converter.Construct(array2);
				}
				propertyValue = new ListValueConverter.ListValue(array3, Enumerable.Empty<CssToken>());
			}
			return propertyValue;
		}

		// Token: 0x040008F5 RID: 2293
		private readonly IValueConverter _converter;

		// Token: 0x020004BF RID: 1215
		private sealed class ListValue : IPropertyValue
		{
			// Token: 0x06002537 RID: 9527 RVA: 0x00060CD4 File Offset: 0x0005EED4
			public ListValue(IPropertyValue[] values, IEnumerable<CssToken> tokens)
			{
				this._values = values;
				this._value = new CssValue(tokens);
			}

			// Token: 0x17000AA8 RID: 2728
			// (get) Token: 0x06002538 RID: 9528 RVA: 0x00060CEF File Offset: 0x0005EEEF
			public string CssText
			{
				get
				{
					return string.Join(", ", this._values.Select((IPropertyValue m) => m.CssText));
				}
			}

			// Token: 0x17000AA9 RID: 2729
			// (get) Token: 0x06002539 RID: 9529 RVA: 0x00060D25 File Offset: 0x0005EF25
			public CssValue Original
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x0600253A RID: 9530 RVA: 0x00060D30 File Offset: 0x0005EF30
			public CssValue ExtractFor(string name)
			{
				List<CssToken> list = new List<CssToken>();
				IPropertyValue[] values = this._values;
				for (int i = 0; i < values.Length; i++)
				{
					CssValue cssValue = values[i].ExtractFor(name);
					if (cssValue != null)
					{
						if (list.Count > 0)
						{
							list.Add(CssToken.Comma);
						}
						list.AddRange(cssValue);
					}
				}
				return new CssValue(list);
			}

			// Token: 0x0400114D RID: 4429
			private readonly IPropertyValue[] _values;

			// Token: 0x0400114E RID: 4430
			private readonly CssValue _value;
		}
	}
}
