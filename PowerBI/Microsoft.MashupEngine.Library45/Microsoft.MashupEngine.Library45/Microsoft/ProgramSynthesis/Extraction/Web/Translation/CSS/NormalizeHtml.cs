using System;
using System.Linq;
using System.Runtime.CompilerServices;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Translation.CSS
{
	// Token: 0x0200119E RID: 4510
	public static class NormalizeHtml
	{
		// Token: 0x0600865A RID: 34394 RVA: 0x001C38B0 File Offset: 0x001C1AB0
		public static void NormalizeStyle(IElement node)
		{
			if (node.HasAttribute("style"))
			{
				try
				{
					ICssStyleDeclaration style = node.Style;
					if (style == null)
					{
						throw new ArgumentException("Parse HTML document using new HtmlParser(Configuration.Default.WithCss().WithLocaleBasedEncoding()). This ensures that the style attribute values get appropriately parsed using a CSS parser.");
					}
					string text = string.Join(";", from x in style
						where x.Value != null
						select FormattableString.Invariant(FormattableStringFactory.Create("{0}:{1}", new object[] { x.Name, x.Value })));
					node.SetAttribute("style", text);
				}
				catch (OverflowException)
				{
				}
			}
			foreach (INode node2 in node.ChildNodes)
			{
				IElement element = node2 as IElement;
				if (element != null)
				{
					NormalizeHtml.NormalizeStyle(element);
				}
			}
		}

		// Token: 0x0600865B RID: 34395 RVA: 0x001C3998 File Offset: 0x001C1B98
		public static IHtmlDocument ParseNormalize(string documentSource)
		{
			IHtmlDocument htmlDocument = new HtmlParser(Configuration.Default.WithCss(null).WithLocaleBasedEncoding()).Parse(documentSource);
			NormalizeHtml.NormalizeStyle(htmlDocument.DocumentElement);
			return htmlDocument;
		}
	}
}
