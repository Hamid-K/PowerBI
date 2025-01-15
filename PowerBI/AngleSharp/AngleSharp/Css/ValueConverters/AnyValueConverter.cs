using System;
using System.Collections.Generic;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x0200012D RID: 301
	internal sealed class AnyValueConverter : IValueConverter
	{
		// Token: 0x0600099F RID: 2463 RVA: 0x0003F402 File Offset: 0x0003D602
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			return new AnyValueConverter.AnyValue(value);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0003F40A File Offset: 0x0003D60A
		public IPropertyValue Construct(CssProperty[] properties)
		{
			return properties.Guard<AnyValueConverter.AnyValue>();
		}

		// Token: 0x020004B2 RID: 1202
		private sealed class AnyValue : IPropertyValue
		{
			// Token: 0x06002501 RID: 9473 RVA: 0x0006073C File Offset: 0x0005E93C
			public AnyValue(IEnumerable<CssToken> tokens)
			{
				this._value = new CssValue(tokens);
			}

			// Token: 0x17000A90 RID: 2704
			// (get) Token: 0x06002502 RID: 9474 RVA: 0x00060750 File Offset: 0x0005E950
			public string CssText
			{
				get
				{
					return this._value.ToText();
				}
			}

			// Token: 0x17000A91 RID: 2705
			// (get) Token: 0x06002503 RID: 9475 RVA: 0x0006075D File Offset: 0x0005E95D
			public CssValue Original
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x06002504 RID: 9476 RVA: 0x0006075D File Offset: 0x0005E95D
			public CssValue ExtractFor(string name)
			{
				return this._value;
			}

			// Token: 0x0400112A RID: 4394
			private readonly CssValue _value;
		}
	}
}
