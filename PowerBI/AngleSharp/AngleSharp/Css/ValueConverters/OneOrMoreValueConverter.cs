using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x0200013B RID: 315
	internal sealed class OneOrMoreValueConverter : IValueConverter
	{
		// Token: 0x060009CC RID: 2508 RVA: 0x0003FF9A File Offset: 0x0003E19A
		public OneOrMoreValueConverter(IValueConverter converter, int minimum, int maximum)
		{
			this._converter = converter;
			this._minimum = minimum;
			this._maximum = maximum;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0003FFB8 File Offset: 0x0003E1B8
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			List<List<CssToken>> list = value.ToItems();
			int count = list.Count;
			if (count >= this._minimum && count <= this._maximum)
			{
				IPropertyValue[] array = new IPropertyValue[list.Count];
				for (int i = 0; i < count; i++)
				{
					array[i] = this._converter.Convert(list[i]);
					if (array[i] == null)
					{
						return null;
					}
				}
				return new OneOrMoreValueConverter.MultipleValue(array, value);
			}
			return null;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00040024 File Offset: 0x0003E224
		public IPropertyValue Construct(CssProperty[] properties)
		{
			IPropertyValue propertyValue = properties.Guard<OneOrMoreValueConverter.MultipleValue>();
			if (propertyValue == null)
			{
				IPropertyValue[] array = new IPropertyValue[properties.Length];
				for (int i = 0; i < properties.Length; i++)
				{
					IPropertyValue propertyValue2 = this._converter.Construct(new CssProperty[] { properties[i] });
					if (propertyValue2 == null)
					{
						return null;
					}
					array[i] = propertyValue2;
				}
				propertyValue = new OneOrMoreValueConverter.MultipleValue(array, Enumerable.Empty<CssToken>());
			}
			return propertyValue;
		}

		// Token: 0x040008F6 RID: 2294
		private readonly IValueConverter _converter;

		// Token: 0x040008F7 RID: 2295
		private readonly int _minimum;

		// Token: 0x040008F8 RID: 2296
		private readonly int _maximum;

		// Token: 0x020004C0 RID: 1216
		private sealed class MultipleValue : IPropertyValue
		{
			// Token: 0x0600253B RID: 9531 RVA: 0x00060D86 File Offset: 0x0005EF86
			public MultipleValue(IPropertyValue[] values, IEnumerable<CssToken> tokens)
			{
				this._values = values;
				this._value = new CssValue(tokens);
			}

			// Token: 0x17000AAA RID: 2730
			// (get) Token: 0x0600253C RID: 9532 RVA: 0x00060DA4 File Offset: 0x0005EFA4
			public string CssText
			{
				get
				{
					return string.Join(" ", from m in this._values
						where !string.IsNullOrEmpty(m.CssText)
						select m.CssText);
				}
			}

			// Token: 0x17000AAB RID: 2731
			// (get) Token: 0x0600253D RID: 9533 RVA: 0x00060E09 File Offset: 0x0005F009
			public CssValue Original
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x0600253E RID: 9534 RVA: 0x00060E14 File Offset: 0x0005F014
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
							list.Add(CssToken.Whitespace);
						}
						list.AddRange(cssValue);
					}
				}
				return new CssValue(list);
			}

			// Token: 0x0400114F RID: 4431
			private readonly IPropertyValue[] _values;

			// Token: 0x04001150 RID: 4432
			private readonly CssValue _value;
		}
	}
}
