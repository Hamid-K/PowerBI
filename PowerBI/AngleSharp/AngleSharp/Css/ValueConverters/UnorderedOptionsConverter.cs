using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x02000146 RID: 326
	internal sealed class UnorderedOptionsConverter : IValueConverter
	{
		// Token: 0x060009F3 RID: 2547 RVA: 0x00040646 File Offset: 0x0003E846
		public UnorderedOptionsConverter(params IValueConverter[] converters)
		{
			this._converters = converters;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00040658 File Offset: 0x0003E858
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			List<CssToken> list = new List<CssToken>(value);
			IPropertyValue[] array = new IPropertyValue[this._converters.Length];
			for (int i = 0; i < this._converters.Length; i++)
			{
				array[i] = this._converters[i].VaryAll(list);
				if (array[i] == null)
				{
					return null;
				}
			}
			if (list.Count != 0)
			{
				return null;
			}
			return new UnorderedOptionsConverter.OptionsValue(array, value);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x000406B8 File Offset: 0x0003E8B8
		public IPropertyValue Construct(CssProperty[] properties)
		{
			IPropertyValue propertyValue = properties.Guard<UnorderedOptionsConverter.OptionsValue>();
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
				propertyValue = new UnorderedOptionsConverter.OptionsValue(array, Enumerable.Empty<CssToken>());
			}
			return propertyValue;
		}

		// Token: 0x04000906 RID: 2310
		private readonly IValueConverter[] _converters;

		// Token: 0x020004C9 RID: 1225
		private sealed class OptionsValue : IPropertyValue
		{
			// Token: 0x06002562 RID: 9570 RVA: 0x00061277 File Offset: 0x0005F477
			public OptionsValue(IPropertyValue[] options, IEnumerable<CssToken> tokens)
			{
				this._options = options;
				this._original = new CssValue(tokens);
			}

			// Token: 0x17000ABD RID: 2749
			// (get) Token: 0x06002563 RID: 9571 RVA: 0x00061294 File Offset: 0x0005F494
			public string CssText
			{
				get
				{
					return string.Join(" ", from m in this._options
						where !string.IsNullOrEmpty(m.CssText)
						select m.CssText);
				}
			}

			// Token: 0x17000ABE RID: 2750
			// (get) Token: 0x06002564 RID: 9572 RVA: 0x000612F9 File Offset: 0x0005F4F9
			public CssValue Original
			{
				get
				{
					return this._original;
				}
			}

			// Token: 0x06002565 RID: 9573 RVA: 0x00061304 File Offset: 0x0005F504
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

			// Token: 0x04001165 RID: 4453
			private readonly IPropertyValue[] _options;

			// Token: 0x04001166 RID: 4454
			private readonly CssValue _original;
		}
	}
}
