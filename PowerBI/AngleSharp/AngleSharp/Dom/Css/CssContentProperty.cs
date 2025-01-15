using System;
using System.Collections.Generic;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200024C RID: 588
	internal sealed class CssContentProperty : CssProperty
	{
		// Token: 0x060013E8 RID: 5096 RVA: 0x0004B206 File Offset: 0x00049406
		internal CssContentProperty()
			: base(PropertyNames.Content, PropertyFlags.None)
		{
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x0004B214 File Offset: 0x00049414
		internal override IValueConverter Converter
		{
			get
			{
				return CssContentProperty.StyleConverter;
			}
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0004B21B File Offset: 0x0004941B
		private static CssContentProperty.ContentMode TransformUrl(string url)
		{
			return new CssContentProperty.UrlContentMode(url);
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0004B223 File Offset: 0x00049423
		private static CssContentProperty.ContentMode TransformString(string str)
		{
			return new CssContentProperty.TextContentMode(str);
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0004B22B File Offset: 0x0004942B
		private static CssContentProperty.ContentMode TransformAttr(string attr)
		{
			return new CssContentProperty.AttributeContentMode(attr);
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0004B233 File Offset: 0x00049433
		private static CssContentProperty.ContentMode TransformCounter(Counter counter)
		{
			return new CssContentProperty.CounterContentMode(counter);
		}

		// Token: 0x04000BDB RID: 3035
		private static readonly Dictionary<string, CssContentProperty.ContentMode> ContentModes = new Dictionary<string, CssContentProperty.ContentMode>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.OpenQuote,
				new CssContentProperty.OpenQuoteContentMode()
			},
			{
				Keywords.NoOpenQuote,
				new CssContentProperty.NoOpenQuoteContentMode()
			},
			{
				Keywords.CloseQuote,
				new CssContentProperty.CloseQuoteContentMode()
			},
			{
				Keywords.NoCloseQuote,
				new CssContentProperty.NoCloseQuoteContentMode()
			}
		};

		// Token: 0x04000BDC RID: 3036
		private static readonly CssContentProperty.ContentMode[] Default = new CssContentProperty.NormalContentMode[]
		{
			new CssContentProperty.NormalContentMode()
		};

		// Token: 0x04000BDD RID: 3037
		private static readonly IValueConverter StyleConverter = Converters.Assign<CssContentProperty.ContentMode[]>(Keywords.Normal, CssContentProperty.Default).OrNone().Or(CssContentProperty.ContentModes.ToConverter<CssContentProperty.ContentMode>().Or(Converters.UrlConverter).Or(Converters.StringConverter)
			.Or(Converters.AttrConverter)
			.Or(Converters.CounterConverter)
			.Many(1, 65535))
			.OrDefault();

		// Token: 0x020004F6 RID: 1270
		private abstract class ContentMode
		{
			// Token: 0x0600261B RID: 9755
			public abstract string Stringify(IElement element);
		}

		// Token: 0x020004F7 RID: 1271
		private sealed class NormalContentMode : CssContentProperty.ContentMode
		{
			// Token: 0x0600261D RID: 9757 RVA: 0x0004280F File Offset: 0x00040A0F
			public override string Stringify(IElement element)
			{
				return string.Empty;
			}
		}

		// Token: 0x020004F8 RID: 1272
		private sealed class OpenQuoteContentMode : CssContentProperty.ContentMode
		{
			// Token: 0x0600261F RID: 9759 RVA: 0x0004280F File Offset: 0x00040A0F
			public override string Stringify(IElement element)
			{
				return string.Empty;
			}
		}

		// Token: 0x020004F9 RID: 1273
		private sealed class CloseQuoteContentMode : CssContentProperty.ContentMode
		{
			// Token: 0x06002621 RID: 9761 RVA: 0x0004280F File Offset: 0x00040A0F
			public override string Stringify(IElement element)
			{
				return string.Empty;
			}
		}

		// Token: 0x020004FA RID: 1274
		private sealed class NoOpenQuoteContentMode : CssContentProperty.ContentMode
		{
			// Token: 0x06002623 RID: 9763 RVA: 0x0004280F File Offset: 0x00040A0F
			public override string Stringify(IElement element)
			{
				return string.Empty;
			}
		}

		// Token: 0x020004FB RID: 1275
		private sealed class NoCloseQuoteContentMode : CssContentProperty.ContentMode
		{
			// Token: 0x06002625 RID: 9765 RVA: 0x0004280F File Offset: 0x00040A0F
			public override string Stringify(IElement element)
			{
				return string.Empty;
			}
		}

		// Token: 0x020004FC RID: 1276
		private sealed class TextContentMode : CssContentProperty.ContentMode
		{
			// Token: 0x06002627 RID: 9767 RVA: 0x00062C0A File Offset: 0x00060E0A
			public TextContentMode(string text)
			{
				this._text = text;
			}

			// Token: 0x06002628 RID: 9768 RVA: 0x00062C19 File Offset: 0x00060E19
			public override string Stringify(IElement element)
			{
				return this._text;
			}

			// Token: 0x0400121B RID: 4635
			private readonly string _text;
		}

		// Token: 0x020004FD RID: 1277
		private sealed class CounterContentMode : CssContentProperty.ContentMode
		{
			// Token: 0x06002629 RID: 9769 RVA: 0x00062C21 File Offset: 0x00060E21
			public CounterContentMode(Counter counter)
			{
				this._counter = counter;
			}

			// Token: 0x0600262A RID: 9770 RVA: 0x0004280F File Offset: 0x00040A0F
			public override string Stringify(IElement element)
			{
				return string.Empty;
			}

			// Token: 0x0400121C RID: 4636
			private readonly Counter _counter;
		}

		// Token: 0x020004FE RID: 1278
		private sealed class AttributeContentMode : CssContentProperty.ContentMode
		{
			// Token: 0x0600262B RID: 9771 RVA: 0x00062C30 File Offset: 0x00060E30
			public AttributeContentMode(string attribute)
			{
				this._attribute = attribute;
			}

			// Token: 0x0600262C RID: 9772 RVA: 0x00062C3F File Offset: 0x00060E3F
			public override string Stringify(IElement element)
			{
				return element.GetAttribute(this._attribute) ?? string.Empty;
			}

			// Token: 0x0400121D RID: 4637
			private readonly string _attribute;
		}

		// Token: 0x020004FF RID: 1279
		private sealed class UrlContentMode : CssContentProperty.ContentMode
		{
			// Token: 0x0600262D RID: 9773 RVA: 0x00062C56 File Offset: 0x00060E56
			public UrlContentMode(string url)
			{
				this._url = url;
			}

			// Token: 0x0600262E RID: 9774 RVA: 0x0004280F File Offset: 0x00040A0F
			public override string Stringify(IElement element)
			{
				return string.Empty;
			}

			// Token: 0x0400121E RID: 4638
			private readonly string _url;
		}
	}
}
