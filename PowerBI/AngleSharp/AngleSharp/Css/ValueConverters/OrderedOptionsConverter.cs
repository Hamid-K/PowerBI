using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x0200013E RID: 318
	internal sealed class OrderedOptionsConverter : IValueConverter
	{
		// Token: 0x060009D5 RID: 2517 RVA: 0x0004013E File Offset: 0x0003E33E
		public OrderedOptionsConverter(params IValueConverter[] converters)
		{
			this._converters = converters;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00040150 File Offset: 0x0003E350
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			List<CssToken> list = new List<CssToken>(value);
			IPropertyValue[] array = new IPropertyValue[this._converters.Length];
			for (int i = 0; i < this._converters.Length; i++)
			{
				array[i] = this._converters[i].VaryStart(list);
				if (array[i] == null)
				{
					return null;
				}
			}
			if (list.Count != 0)
			{
				return null;
			}
			return new OrderedOptionsConverter.OptionsValue(array, value);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x000401B0 File Offset: 0x0003E3B0
		public IPropertyValue Construct(CssProperty[] properties)
		{
			IPropertyValue propertyValue = properties.Guard<OrderedOptionsConverter.OptionsValue>();
			if (propertyValue == null)
			{
				IPropertyValue[] array = new IPropertyValue[this._converters.Length];
				for (int i = 0; i < this._converters.Length; i++)
				{
					IPropertyValue propertyValue2 = this._converters[i].Construct(properties);
					if (propertyValue2 == null)
					{
						return null;
					}
					array[i] = propertyValue2;
				}
				propertyValue = new OrderedOptionsConverter.OptionsValue(array, Enumerable.Empty<CssToken>());
			}
			return propertyValue;
		}

		// Token: 0x040008FC RID: 2300
		private readonly IValueConverter[] _converters;

		// Token: 0x020004C3 RID: 1219
		private sealed class OptionsValue : IPropertyValue, IEnumerable<IPropertyValue>, IEnumerable
		{
			// Token: 0x06002547 RID: 9543 RVA: 0x00060EA9 File Offset: 0x0005F0A9
			public OptionsValue(IPropertyValue[] options, IEnumerable<CssToken> tokens)
			{
				this._options = options;
				this._original = new CssValue(tokens);
			}

			// Token: 0x17000AB0 RID: 2736
			// (get) Token: 0x06002548 RID: 9544 RVA: 0x00060EC4 File Offset: 0x0005F0C4
			public string CssText
			{
				get
				{
					return string.Join(" ", from m in this._options
						where !string.IsNullOrEmpty(m.CssText)
						select m.CssText);
				}
			}

			// Token: 0x17000AB1 RID: 2737
			// (get) Token: 0x06002549 RID: 9545 RVA: 0x00060F29 File Offset: 0x0005F129
			public CssValue Original
			{
				get
				{
					return this._original;
				}
			}

			// Token: 0x0600254A RID: 9546 RVA: 0x00060F34 File Offset: 0x0005F134
			public CssValue ExtractFor(string name)
			{
				List<CssToken> list = new List<CssToken>();
				IPropertyValue[] options = this._options;
				for (int i = 0; i < options.Length; i++)
				{
					CssValue cssValue = options[i].ExtractFor(name);
					if (cssValue != null && cssValue.Count > 0)
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

			// Token: 0x0600254B RID: 9547 RVA: 0x00060F93 File Offset: 0x0005F193
			IEnumerator<IPropertyValue> IEnumerable<IPropertyValue>.GetEnumerator()
			{
				foreach (IPropertyValue propertyValue in this._options)
				{
					yield return propertyValue;
				}
				IPropertyValue[] array = null;
				yield break;
			}

			// Token: 0x0600254C RID: 9548 RVA: 0x00060FA2 File Offset: 0x0005F1A2
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this._options.GetEnumerator();
			}

			// Token: 0x04001154 RID: 4436
			private readonly IPropertyValue[] _options;

			// Token: 0x04001155 RID: 4437
			private readonly CssValue _original;
		}
	}
}
