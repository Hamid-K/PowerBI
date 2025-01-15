using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom.Css;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x0200013C RID: 316
	internal sealed class OptionValueConverter : IValueConverter
	{
		// Token: 0x060009CF RID: 2511 RVA: 0x00040080 File Offset: 0x0003E280
		public OptionValueConverter(IValueConverter converter)
		{
			this._converter = converter;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00040090 File Offset: 0x0003E290
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			if (!value.Any<CssToken>())
			{
				return new OptionValueConverter.OptionValue(value);
			}
			return this._converter.Convert(value);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x000400BA File Offset: 0x0003E2BA
		public IPropertyValue Construct(CssProperty[] properties)
		{
			return this._converter.Construct(properties) ?? new OptionValueConverter.OptionValue(Enumerable.Empty<CssToken>());
		}

		// Token: 0x040008F9 RID: 2297
		private readonly IValueConverter _converter;

		// Token: 0x020004C1 RID: 1217
		private sealed class OptionValue : IPropertyValue
		{
			// Token: 0x0600253F RID: 9535 RVA: 0x00060E6A File Offset: 0x0005F06A
			public OptionValue(IEnumerable<CssToken> tokens)
			{
				this._original = new CssValue(tokens);
			}

			// Token: 0x17000AAC RID: 2732
			// (get) Token: 0x06002540 RID: 9536 RVA: 0x0004280F File Offset: 0x00040A0F
			public string CssText
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x17000AAD RID: 2733
			// (get) Token: 0x06002541 RID: 9537 RVA: 0x00060E7E File Offset: 0x0005F07E
			public CssValue Original
			{
				get
				{
					return this._original;
				}
			}

			// Token: 0x06002542 RID: 9538 RVA: 0x0000C295 File Offset: 0x0000A495
			public CssValue ExtractFor(string name)
			{
				return null;
			}

			// Token: 0x04001151 RID: 4433
			private readonly CssValue _original;
		}
	}
}
