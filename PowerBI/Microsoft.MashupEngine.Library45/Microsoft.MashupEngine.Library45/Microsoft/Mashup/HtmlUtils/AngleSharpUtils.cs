using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Css;
using AngleSharp.Parser.Html;

namespace Microsoft.Mashup.HtmlUtils
{
	// Token: 0x02002042 RID: 8258
	internal static class AngleSharpUtils
	{
		// Token: 0x06011348 RID: 70472 RVA: 0x003B3978 File Offset: 0x003B1B78
		public static string GetInnerText(INode n)
		{
			StringBuilder stringBuilder = new StringBuilder();
			AngleSharpUtils.AppendInnerText(n, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06011349 RID: 70473 RVA: 0x003B3998 File Offset: 0x003B1B98
		public static bool TryGetAttributeValue(INode n, string attributeName, out string attributeValue)
		{
			attributeValue = null;
			if (!AngleSharpUtils.excludedNodeNames.Contains(n.NodeName))
			{
				IElement element = n as IElement;
				if (element != null)
				{
					string attribute = element.GetAttribute(attributeName);
					if (attribute != null)
					{
						attributeValue = attribute;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0601134A RID: 70474 RVA: 0x003B39D5 File Offset: 0x003B1BD5
		public static IHtmlDocument ParseHtmlAndNormalizeStyles(string documentSource)
		{
			IHtmlDocument htmlDocument = AngleSharpUtils.defaultHtmlParser.Parse(documentSource);
			AngleSharpUtils.NormalizeStyles(htmlDocument.DocumentElement);
			return htmlDocument;
		}

		// Token: 0x0601134B RID: 70475 RVA: 0x003B39ED File Offset: 0x003B1BED
		public static bool IsValidSelector(string selector)
		{
			return AngleSharpUtils.defaultCssParser.ParseSelector(selector) != null;
		}

		// Token: 0x0601134C RID: 70476 RVA: 0x003B3A00 File Offset: 0x003B1C00
		private static void AppendInnerText(INode n, StringBuilder sb)
		{
			if (AngleSharpUtils.excludedNodeNames.Contains(n.NodeName))
			{
				return;
			}
			IText text = n as IText;
			if (text != null)
			{
				sb.Append(text.Data);
			}
			foreach (INode node in n.ChildNodes)
			{
				AngleSharpUtils.AppendInnerText(node, sb);
			}
		}

		// Token: 0x0601134D RID: 70477 RVA: 0x003B3A78 File Offset: 0x003B1C78
		private static void NormalizeStyles(IElement element)
		{
			if (element.HasAttribute("style"))
			{
				try
				{
					ICssStyleDeclaration style = element.Style;
					if (style != null)
					{
						string text = string.Join(";", from x in style
							where x.Value != null
							select x.Name + ":" + x.Value);
						element.SetAttribute("style", text);
					}
				}
				catch (OverflowException)
				{
				}
			}
			foreach (IElement element2 in element.Children)
			{
				AngleSharpUtils.NormalizeStyles(element2);
			}
		}

		// Token: 0x04006853 RID: 26707
		private static readonly CssParser defaultCssParser = new CssParser();

		// Token: 0x04006854 RID: 26708
		private static readonly HtmlParser defaultHtmlParser = new HtmlParser(Configuration.Default.WithCss(null));

		// Token: 0x04006855 RID: 26709
		private static readonly HashSet<string> excludedNodeNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "SCRIPT", "STYLE" };
	}
}
