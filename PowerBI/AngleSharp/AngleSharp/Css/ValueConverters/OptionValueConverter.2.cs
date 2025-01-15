using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom.Css;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x0200013D RID: 317
	internal sealed class OptionValueConverter<T> : IValueConverter
	{
		// Token: 0x060009D2 RID: 2514 RVA: 0x000400D6 File Offset: 0x0003E2D6
		public OptionValueConverter(IValueConverter converter, T defaultValue)
		{
			this._converter = converter;
			this._defaultValue = defaultValue;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x000400EC File Offset: 0x0003E2EC
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			if (!value.Any<CssToken>())
			{
				return new OptionValueConverter<T>.OptionValue(this._defaultValue, value);
			}
			return this._converter.Convert(value);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0004011C File Offset: 0x0003E31C
		public IPropertyValue Construct(CssProperty[] properties)
		{
			return this._converter.Construct(properties) ?? new OptionValueConverter<T>.OptionValue(this._defaultValue, Enumerable.Empty<CssToken>());
		}

		// Token: 0x040008FA RID: 2298
		private readonly IValueConverter _converter;

		// Token: 0x040008FB RID: 2299
		private readonly T _defaultValue;

		// Token: 0x020004C2 RID: 1218
		private sealed class OptionValue : IPropertyValue
		{
			// Token: 0x06002543 RID: 9539 RVA: 0x00060E86 File Offset: 0x0005F086
			public OptionValue(T value, IEnumerable<CssToken> tokens)
			{
				this._value = value;
				this._original = new CssValue(tokens);
			}

			// Token: 0x17000AAE RID: 2734
			// (get) Token: 0x06002544 RID: 9540 RVA: 0x0004280F File Offset: 0x00040A0F
			public string CssText
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x17000AAF RID: 2735
			// (get) Token: 0x06002545 RID: 9541 RVA: 0x00060EA1 File Offset: 0x0005F0A1
			public CssValue Original
			{
				get
				{
					return this._original;
				}
			}

			// Token: 0x06002546 RID: 9542 RVA: 0x0000C295 File Offset: 0x0000A495
			public CssValue ExtractFor(string name)
			{
				return null;
			}

			// Token: 0x04001152 RID: 4434
			private readonly T _value;

			// Token: 0x04001153 RID: 4435
			private readonly CssValue _original;
		}
	}
}
