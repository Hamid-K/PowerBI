using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x02000142 RID: 322
	internal sealed class StartsWithValueConverter : IValueConverter
	{
		// Token: 0x060009E5 RID: 2533 RVA: 0x0004045B File Offset: 0x0003E65B
		public StartsWithValueConverter(CssTokenType type, string data, IValueConverter converter)
		{
			this._type = type;
			this._data = data;
			this._converter = converter;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00040478 File Offset: 0x0003E678
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			List<CssToken> list = this.Transform(value);
			if (list == null)
			{
				return null;
			}
			return this.CreateFrom(this._converter.Convert(list), value);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x000404A8 File Offset: 0x0003E6A8
		public IPropertyValue Construct(CssProperty[] properties)
		{
			IPropertyValue propertyValue = this._converter.Construct(properties);
			if (propertyValue == null)
			{
				return null;
			}
			return this.CreateFrom(propertyValue, Enumerable.Empty<CssToken>());
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x000404D3 File Offset: 0x0003E6D3
		private IPropertyValue CreateFrom(IPropertyValue value, IEnumerable<CssToken> tokens)
		{
			if (value == null)
			{
				return null;
			}
			return new StartsWithValueConverter.StartValue(this._data, value, tokens);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x000404E8 File Offset: 0x0003E6E8
		private List<CssToken> Transform(IEnumerable<CssToken> values)
		{
			IEnumerator<CssToken> enumerator = values.GetEnumerator();
			while (enumerator.MoveNext() && enumerator.Current.Type == CssTokenType.Whitespace)
			{
			}
			if (enumerator.Current.Type == this._type && enumerator.Current.Data.Isi(this._data))
			{
				List<CssToken> list = new List<CssToken>();
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Type != CssTokenType.Whitespace || list.Count != 0)
					{
						list.Add(enumerator.Current);
					}
				}
				return list;
			}
			return null;
		}

		// Token: 0x04000902 RID: 2306
		private readonly CssTokenType _type;

		// Token: 0x04000903 RID: 2307
		private readonly string _data;

		// Token: 0x04000904 RID: 2308
		private readonly IValueConverter _converter;

		// Token: 0x020004C5 RID: 1221
		private sealed class StartValue : IPropertyValue
		{
			// Token: 0x06002552 RID: 9554 RVA: 0x0006114E File Offset: 0x0005F34E
			public StartValue(string start, IPropertyValue value, IEnumerable<CssToken> tokens)
			{
				this._start = start;
				this._value = value;
				this._original = new CssValue(tokens);
			}

			// Token: 0x17000AB5 RID: 2741
			// (get) Token: 0x06002553 RID: 9555 RVA: 0x00061170 File Offset: 0x0005F370
			public string CssText
			{
				get
				{
					return this._start + " " + this._value.CssText;
				}
			}

			// Token: 0x17000AB6 RID: 2742
			// (get) Token: 0x06002554 RID: 9556 RVA: 0x0006118D File Offset: 0x0005F38D
			public CssValue Original
			{
				get
				{
					return this._original;
				}
			}

			// Token: 0x06002555 RID: 9557 RVA: 0x00061195 File Offset: 0x0005F395
			public CssValue ExtractFor(string name)
			{
				return this._value.ExtractFor(name);
			}

			// Token: 0x0400115C RID: 4444
			private readonly string _start;

			// Token: 0x0400115D RID: 4445
			private readonly IPropertyValue _value;

			// Token: 0x0400115E RID: 4446
			private readonly CssValue _original;
		}
	}
}
