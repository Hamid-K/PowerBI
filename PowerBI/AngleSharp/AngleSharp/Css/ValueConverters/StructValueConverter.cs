using System;
using System.Collections.Generic;
using System.Globalization;
using AngleSharp.Dom.Css;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x02000145 RID: 325
	internal sealed class StructValueConverter<T> : IValueConverter where T : struct, IFormattable
	{
		// Token: 0x060009F0 RID: 2544 RVA: 0x000405FC File Offset: 0x0003E7FC
		public StructValueConverter(Func<IEnumerable<CssToken>, T?> converter)
		{
			this._converter = converter;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0004060C File Offset: 0x0003E80C
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			T? t = this._converter(value);
			if (t == null)
			{
				return null;
			}
			return new StructValueConverter<T>.StructValue(t.Value, value);
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0004063E File Offset: 0x0003E83E
		public IPropertyValue Construct(CssProperty[] properties)
		{
			return properties.Guard<StructValueConverter<T>.StructValue>();
		}

		// Token: 0x04000905 RID: 2309
		private readonly Func<IEnumerable<CssToken>, T?> _converter;

		// Token: 0x020004C8 RID: 1224
		private sealed class StructValue : IPropertyValue
		{
			// Token: 0x0600255E RID: 9566 RVA: 0x0006122C File Offset: 0x0005F42C
			public StructValue(T value, IEnumerable<CssToken> tokens)
			{
				this._value = value;
				this._original = new CssValue(tokens);
			}

			// Token: 0x17000ABB RID: 2747
			// (get) Token: 0x0600255F RID: 9567 RVA: 0x00061248 File Offset: 0x0005F448
			public string CssText
			{
				get
				{
					T value = this._value;
					return value.ToString(null, CultureInfo.InvariantCulture);
				}
			}

			// Token: 0x17000ABC RID: 2748
			// (get) Token: 0x06002560 RID: 9568 RVA: 0x0006126F File Offset: 0x0005F46F
			public CssValue Original
			{
				get
				{
					return this._original;
				}
			}

			// Token: 0x06002561 RID: 9569 RVA: 0x0006126F File Offset: 0x0005F46F
			public CssValue ExtractFor(string name)
			{
				return this._original;
			}

			// Token: 0x04001163 RID: 4451
			private readonly T _value;

			// Token: 0x04001164 RID: 4452
			private readonly CssValue _original;
		}
	}
}
