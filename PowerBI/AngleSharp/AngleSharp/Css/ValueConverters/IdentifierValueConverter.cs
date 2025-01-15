using System;
using System.Collections.Generic;
using AngleSharp.Dom.Css;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x02000138 RID: 312
	internal sealed class IdentifierValueConverter : IValueConverter
	{
		// Token: 0x060009C3 RID: 2499 RVA: 0x0003FD9B File Offset: 0x0003DF9B
		public IdentifierValueConverter(Func<IEnumerable<CssToken>, string> converter)
		{
			this._converter = converter;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0003FDAC File Offset: 0x0003DFAC
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			string text = this._converter(value);
			if (text == null)
			{
				return null;
			}
			return new IdentifierValueConverter.IdentifierValue(text, value);
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0003FDD2 File Offset: 0x0003DFD2
		public IPropertyValue Construct(CssProperty[] properties)
		{
			return properties.Guard<IdentifierValueConverter.IdentifierValue>();
		}

		// Token: 0x040008F2 RID: 2290
		private readonly Func<IEnumerable<CssToken>, string> _converter;

		// Token: 0x020004BD RID: 1213
		private sealed class IdentifierValue : IPropertyValue
		{
			// Token: 0x0600252F RID: 9519 RVA: 0x00060C77 File Offset: 0x0005EE77
			public IdentifierValue(string identifier, IEnumerable<CssToken> tokens)
			{
				this._identifier = identifier;
				this._value = new CssValue(tokens);
			}

			// Token: 0x17000AA4 RID: 2724
			// (get) Token: 0x06002530 RID: 9520 RVA: 0x00060C92 File Offset: 0x0005EE92
			public string CssText
			{
				get
				{
					return this._identifier;
				}
			}

			// Token: 0x17000AA5 RID: 2725
			// (get) Token: 0x06002531 RID: 9521 RVA: 0x00060C9A File Offset: 0x0005EE9A
			public CssValue Original
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x06002532 RID: 9522 RVA: 0x00060C9A File Offset: 0x0005EE9A
			public CssValue ExtractFor(string name)
			{
				return this._value;
			}

			// Token: 0x04001148 RID: 4424
			private readonly string _identifier;

			// Token: 0x04001149 RID: 4425
			private readonly CssValue _value;
		}
	}
}
