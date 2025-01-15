using System;
using System.Collections.Generic;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x02000147 RID: 327
	internal sealed class UrlValueConverter : IValueConverter
	{
		// Token: 0x060009F6 RID: 2550 RVA: 0x00040718 File Offset: 0x0003E918
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			string text = value.ToUri();
			if (text == null)
			{
				return null;
			}
			return new UrlValueConverter.UrlValue(text, value);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00040738 File Offset: 0x0003E938
		public IPropertyValue Construct(CssProperty[] properties)
		{
			return properties.Guard<UrlValueConverter.UrlValue>();
		}

		// Token: 0x020004CA RID: 1226
		private sealed class UrlValue : IPropertyValue
		{
			// Token: 0x06002566 RID: 9574 RVA: 0x00061363 File Offset: 0x0005F563
			public UrlValue(string value, IEnumerable<CssToken> tokens)
			{
				this._value = value;
				this._original = new CssValue(tokens);
			}

			// Token: 0x17000ABF RID: 2751
			// (get) Token: 0x06002567 RID: 9575 RVA: 0x0006137E File Offset: 0x0005F57E
			public string CssText
			{
				get
				{
					return this._value.CssUrl();
				}
			}

			// Token: 0x17000AC0 RID: 2752
			// (get) Token: 0x06002568 RID: 9576 RVA: 0x0006138B File Offset: 0x0005F58B
			public CssValue Original
			{
				get
				{
					return this._original;
				}
			}

			// Token: 0x06002569 RID: 9577 RVA: 0x0006138B File Offset: 0x0005F58B
			public CssValue ExtractFor(string name)
			{
				return this._original;
			}

			// Token: 0x04001167 RID: 4455
			private readonly string _value;

			// Token: 0x04001168 RID: 4456
			private readonly CssValue _original;
		}
	}
}
