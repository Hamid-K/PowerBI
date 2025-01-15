using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x02000131 RID: 305
	internal sealed class ContinuousValueConverter : IValueConverter
	{
		// Token: 0x060009AC RID: 2476 RVA: 0x0003F82F File Offset: 0x0003DA2F
		public ContinuousValueConverter(IValueConverter converter)
		{
			this._converter = converter;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0003F840 File Offset: 0x0003DA40
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			List<CssToken> list = new List<CssToken>(value);
			List<IPropertyValue> list2 = new List<IPropertyValue>();
			if (list.Count > 0)
			{
				while (list.Count != 0)
				{
					IPropertyValue propertyValue = this._converter.VaryStart(list);
					if (propertyValue == null)
					{
						return null;
					}
					list2.Add(propertyValue);
				}
				return new ContinuousValueConverter.OptionsValue(list2.ToArray(), value);
			}
			return null;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0003F894 File Offset: 0x0003DA94
		public IPropertyValue Construct(CssProperty[] properties)
		{
			return properties.Guard<ContinuousValueConverter.OptionsValue>();
		}

		// Token: 0x040008E9 RID: 2281
		private readonly IValueConverter _converter;

		// Token: 0x020004B7 RID: 1207
		private sealed class OptionsValue : IPropertyValue
		{
			// Token: 0x06002517 RID: 9495 RVA: 0x00060935 File Offset: 0x0005EB35
			public OptionsValue(IPropertyValue[] options, IEnumerable<CssToken> tokens)
			{
				this._options = options;
				this._value = new CssValue(tokens);
			}

			// Token: 0x17000A98 RID: 2712
			// (get) Token: 0x06002518 RID: 9496 RVA: 0x00060950 File Offset: 0x0005EB50
			public string CssText
			{
				get
				{
					return string.Join(" ", from m in this._options
						where !string.IsNullOrEmpty(m.CssText)
						select m.CssText);
				}
			}

			// Token: 0x17000A99 RID: 2713
			// (get) Token: 0x06002519 RID: 9497 RVA: 0x000609B5 File Offset: 0x0005EBB5
			public CssValue Original
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x0600251A RID: 9498 RVA: 0x000609C0 File Offset: 0x0005EBC0
			public CssValue ExtractFor(string name)
			{
				List<CssToken> list = new List<CssToken>();
				IPropertyValue[] options = this._options;
				for (int i = 0; i < options.Length; i++)
				{
					CssValue cssValue = options[i].ExtractFor(name);
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

			// Token: 0x04001137 RID: 4407
			private readonly IPropertyValue[] _options;

			// Token: 0x04001138 RID: 4408
			private readonly CssValue _value;
		}
	}
}
