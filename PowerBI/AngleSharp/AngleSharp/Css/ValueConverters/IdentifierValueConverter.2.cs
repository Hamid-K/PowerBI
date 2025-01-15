using System;
using System.Collections.Generic;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x02000139 RID: 313
	internal sealed class IdentifierValueConverter<T> : IValueConverter
	{
		// Token: 0x060009C6 RID: 2502 RVA: 0x0003FDDA File Offset: 0x0003DFDA
		public IdentifierValueConverter(string identifier, T result)
		{
			this._identifier = identifier;
			this._result = result;
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0003FDF0 File Offset: 0x0003DFF0
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			if (!value.Is(this._identifier))
			{
				return null;
			}
			return new IdentifierValueConverter<T>.IdentifierValue(this._identifier, this._result, value);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0003FE14 File Offset: 0x0003E014
		public IPropertyValue Construct(CssProperty[] properties)
		{
			return properties.Guard<IdentifierValueConverter<T>.IdentifierValue>();
		}

		// Token: 0x040008F3 RID: 2291
		private readonly string _identifier;

		// Token: 0x040008F4 RID: 2292
		private readonly T _result;

		// Token: 0x020004BE RID: 1214
		private sealed class IdentifierValue : IPropertyValue
		{
			// Token: 0x06002533 RID: 9523 RVA: 0x00060CA2 File Offset: 0x0005EEA2
			public IdentifierValue(string identifier, T value, IEnumerable<CssToken> tokens)
			{
				this._identifier = identifier;
				this._value = value;
				this._original = new CssValue(tokens);
			}

			// Token: 0x17000AA6 RID: 2726
			// (get) Token: 0x06002534 RID: 9524 RVA: 0x00060CC4 File Offset: 0x0005EEC4
			public string CssText
			{
				get
				{
					return this._identifier;
				}
			}

			// Token: 0x17000AA7 RID: 2727
			// (get) Token: 0x06002535 RID: 9525 RVA: 0x00060CCC File Offset: 0x0005EECC
			public CssValue Original
			{
				get
				{
					return this._original;
				}
			}

			// Token: 0x06002536 RID: 9526 RVA: 0x00060CCC File Offset: 0x0005EECC
			public CssValue ExtractFor(string name)
			{
				return this._original;
			}

			// Token: 0x0400114A RID: 4426
			private readonly string _identifier;

			// Token: 0x0400114B RID: 4427
			private readonly T _value;

			// Token: 0x0400114C RID: 4428
			private readonly CssValue _original;
		}
	}
}
