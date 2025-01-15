using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x0200012E RID: 302
	internal sealed class ArgumentsValueConverter : IValueConverter
	{
		// Token: 0x060009A2 RID: 2466 RVA: 0x0003F412 File Offset: 0x0003D612
		public ArgumentsValueConverter(params IValueConverter[] converters)
		{
			this._converters = converters;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0003F424 File Offset: 0x0003D624
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			List<List<CssToken>> list = value.ToList();
			int num = this._converters.Length;
			if (list.Count <= num)
			{
				IPropertyValue[] array = new IPropertyValue[num];
				for (int i = 0; i < num; i++)
				{
					IEnumerable<CssToken> enumerable;
					if (i >= list.Count)
					{
						enumerable = Enumerable.Empty<CssToken>();
					}
					else
					{
						IEnumerable<CssToken> enumerable2 = list[i];
						enumerable = enumerable2;
					}
					IEnumerable<CssToken> enumerable3 = enumerable;
					array[i] = this._converters[i].Convert(enumerable3);
					if (array[i] == null)
					{
						return null;
					}
				}
				return new ArgumentsValueConverter.ArgumentsValue(array, value);
			}
			return null;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0003F49C File Offset: 0x0003D69C
		public IPropertyValue Construct(CssProperty[] properties)
		{
			return properties.Guard<ArgumentsValueConverter.ArgumentsValue>();
		}

		// Token: 0x040008E5 RID: 2277
		private readonly IValueConverter[] _converters;

		// Token: 0x020004B3 RID: 1203
		private sealed class ArgumentsValue : IPropertyValue
		{
			// Token: 0x06002505 RID: 9477 RVA: 0x00060765 File Offset: 0x0005E965
			public ArgumentsValue(IPropertyValue[] arguments, IEnumerable<CssToken> tokens)
			{
				this._arguments = arguments;
				this._value = new CssValue(tokens);
			}

			// Token: 0x17000A92 RID: 2706
			// (get) Token: 0x06002506 RID: 9478 RVA: 0x00060780 File Offset: 0x0005E980
			public string CssText
			{
				get
				{
					return string.Join(", ", from m in this._arguments
						where !string.IsNullOrEmpty(m.CssText)
						select m.CssText);
				}
			}

			// Token: 0x17000A93 RID: 2707
			// (get) Token: 0x06002507 RID: 9479 RVA: 0x000607E5 File Offset: 0x0005E9E5
			public CssValue Original
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x06002508 RID: 9480 RVA: 0x000607E5 File Offset: 0x0005E9E5
			public CssValue ExtractFor(string name)
			{
				return this._value;
			}

			// Token: 0x0400112B RID: 4395
			private readonly IPropertyValue[] _arguments;

			// Token: 0x0400112C RID: 4396
			private readonly CssValue _value;
		}
	}
}
