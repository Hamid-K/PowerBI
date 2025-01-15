using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x02000143 RID: 323
	internal sealed class StringsValueConverter : IValueConverter
	{
		// Token: 0x060009EA RID: 2538 RVA: 0x00040574 File Offset: 0x0003E774
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			List<List<CssToken>> list = value.ToItems();
			int count = list.Count;
			if (count % 2 == 0)
			{
				string[] array = new string[list.Count];
				for (int i = 0; i < count; i++)
				{
					array[i] = list[i].ToCssString();
					if (array[i] == null)
					{
						return null;
					}
				}
				return new StringsValueConverter.StringsValue(array, value);
			}
			return null;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x000405CB File Offset: 0x0003E7CB
		public IPropertyValue Construct(CssProperty[] properties)
		{
			return properties.Guard<StringsValueConverter.StringsValue>();
		}

		// Token: 0x020004C6 RID: 1222
		private sealed class StringsValue : IPropertyValue
		{
			// Token: 0x06002556 RID: 9558 RVA: 0x000611A3 File Offset: 0x0005F3A3
			public StringsValue(string[] values, IEnumerable<CssToken> tokens)
			{
				this._values = values;
				this._original = new CssValue(tokens);
			}

			// Token: 0x17000AB7 RID: 2743
			// (get) Token: 0x06002557 RID: 9559 RVA: 0x000611BE File Offset: 0x0005F3BE
			public string CssText
			{
				get
				{
					return string.Join(" ", this._values.Select((string m) => m.CssString()));
				}
			}

			// Token: 0x17000AB8 RID: 2744
			// (get) Token: 0x06002558 RID: 9560 RVA: 0x000611F4 File Offset: 0x0005F3F4
			public CssValue Original
			{
				get
				{
					return this._original;
				}
			}

			// Token: 0x06002559 RID: 9561 RVA: 0x000611F4 File Offset: 0x0005F3F4
			public CssValue ExtractFor(string name)
			{
				return this._original;
			}

			// Token: 0x0400115F RID: 4447
			private readonly string[] _values;

			// Token: 0x04001160 RID: 4448
			private readonly CssValue _original;
		}
	}
}
