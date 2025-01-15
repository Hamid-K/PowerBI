using System;
using System.Collections.Generic;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x02000132 RID: 306
	internal sealed class DictionaryValueConverter<T> : IValueConverter
	{
		// Token: 0x060009AF RID: 2479 RVA: 0x0003F89C File Offset: 0x0003DA9C
		public DictionaryValueConverter(Dictionary<string, T> values)
		{
			this._values = values;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0003F8AC File Offset: 0x0003DAAC
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			string text = value.ToIdentifier();
			T t = default(T);
			if (text == null || !this._values.TryGetValue(text, out t))
			{
				return null;
			}
			return new DictionaryValueConverter<T>.EnumeratedValue(text, t, value);
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0003F8E5 File Offset: 0x0003DAE5
		public IPropertyValue Construct(CssProperty[] properties)
		{
			return properties.Guard<DictionaryValueConverter<T>.EnumeratedValue>();
		}

		// Token: 0x040008EA RID: 2282
		private readonly Dictionary<string, T> _values;

		// Token: 0x020004B8 RID: 1208
		private sealed class EnumeratedValue : IPropertyValue
		{
			// Token: 0x0600251B RID: 9499 RVA: 0x00060A16 File Offset: 0x0005EC16
			public EnumeratedValue(string identifier, T value, IEnumerable<CssToken> tokens)
			{
				this._identifier = identifier;
				this._value = value;
				this._original = new CssValue(tokens);
			}

			// Token: 0x17000A9A RID: 2714
			// (get) Token: 0x0600251C RID: 9500 RVA: 0x00060A38 File Offset: 0x0005EC38
			public string CssText
			{
				get
				{
					return this._identifier;
				}
			}

			// Token: 0x17000A9B RID: 2715
			// (get) Token: 0x0600251D RID: 9501 RVA: 0x00060A40 File Offset: 0x0005EC40
			public CssValue Original
			{
				get
				{
					return this._original;
				}
			}

			// Token: 0x0600251E RID: 9502 RVA: 0x00060A40 File Offset: 0x0005EC40
			public CssValue ExtractFor(string name)
			{
				return this._original;
			}

			// Token: 0x04001139 RID: 4409
			private readonly string _identifier;

			// Token: 0x0400113A RID: 4410
			private readonly T _value;

			// Token: 0x0400113B RID: 4411
			private readonly CssValue _original;
		}
	}
}
