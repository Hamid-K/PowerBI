using System;
using System.IO;
using System.Text;
using AngleSharp.Css;
using AngleSharp.Html;

namespace AngleSharp.Extensions
{
	// Token: 0x020000EC RID: 236
	public static class FormatExtensions
	{
		// Token: 0x06000743 RID: 1859 RVA: 0x00034993 File Offset: 0x00032B93
		public static string ToCss(this IStyleFormattable style)
		{
			return style.ToCss(CssStyleFormatter.Instance);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x000349A0 File Offset: 0x00032BA0
		public static string ToCss(this IStyleFormattable style, IStyleFormatter formatter)
		{
			StringBuilder stringBuilder = Pool.NewStringBuilder();
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				style.ToCss(stringWriter, formatter);
			}
			return stringBuilder.ToPool();
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x000349E4 File Offset: 0x00032BE4
		public static void ToCss(this IStyleFormattable style, TextWriter writer)
		{
			style.ToCss(writer, CssStyleFormatter.Instance);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x000349F2 File Offset: 0x00032BF2
		public static string ToHtml(this IMarkupFormattable markup)
		{
			return markup.ToHtml(HtmlMarkupFormatter.Instance);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00034A00 File Offset: 0x00032C00
		public static string ToHtml(this IMarkupFormattable markup, IMarkupFormatter formatter)
		{
			StringBuilder stringBuilder = Pool.NewStringBuilder();
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				markup.ToHtml(stringWriter, formatter);
			}
			return stringBuilder.ToPool();
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00034A44 File Offset: 0x00032C44
		public static void ToHtml(this IMarkupFormattable markup, TextWriter writer)
		{
			markup.ToHtml(writer, HtmlMarkupFormatter.Instance);
		}
	}
}
