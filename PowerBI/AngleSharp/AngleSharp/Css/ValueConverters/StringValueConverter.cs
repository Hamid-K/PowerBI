using System;
using System.Collections.Generic;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x02000144 RID: 324
	internal sealed class StringValueConverter : IValueConverter
	{
		// Token: 0x060009ED RID: 2541 RVA: 0x000405D4 File Offset: 0x0003E7D4
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			string text = value.ToCssString();
			if (text == null)
			{
				return null;
			}
			return new StringValueConverter.StringValue(text, value);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x000405F4 File Offset: 0x0003E7F4
		public IPropertyValue Construct(CssProperty[] properties)
		{
			return properties.Guard<StringValueConverter.StringValue>();
		}

		// Token: 0x020004C7 RID: 1223
		private sealed class StringValue : IPropertyValue
		{
			// Token: 0x0600255A RID: 9562 RVA: 0x000611FC File Offset: 0x0005F3FC
			public StringValue(string value, IEnumerable<CssToken> tokens)
			{
				this._value = value;
				this._original = new CssValue(tokens);
			}

			// Token: 0x17000AB9 RID: 2745
			// (get) Token: 0x0600255B RID: 9563 RVA: 0x00061217 File Offset: 0x0005F417
			public string CssText
			{
				get
				{
					return this._value.CssString();
				}
			}

			// Token: 0x17000ABA RID: 2746
			// (get) Token: 0x0600255C RID: 9564 RVA: 0x00061224 File Offset: 0x0005F424
			public CssValue Original
			{
				get
				{
					return this._original;
				}
			}

			// Token: 0x0600255D RID: 9565 RVA: 0x00061224 File Offset: 0x0005F424
			public CssValue ExtractFor(string name)
			{
				return this._original;
			}

			// Token: 0x04001161 RID: 4449
			private readonly string _value;

			// Token: 0x04001162 RID: 4450
			private readonly CssValue _original;
		}
	}
}
