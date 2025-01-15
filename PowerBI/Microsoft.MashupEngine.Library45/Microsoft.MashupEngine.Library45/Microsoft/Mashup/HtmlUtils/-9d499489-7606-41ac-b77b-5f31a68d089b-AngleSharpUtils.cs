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
	// Token: 0x0200002D RID: 45
	internal static class <9d499489-7606-41ac-b77b-5f31a68d089b>AngleSharpUtils
	{
		// Token: 0x06000102 RID: 258 RVA: 0x0000620C File Offset: 0x0000440C
		public static string GetInnerText(INode n)
		{
			StringBuilder stringBuilder = new StringBuilder();
			<9d499489-7606-41ac-b77b-5f31a68d089b>AngleSharpUtils.AppendInnerText(n, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000622C File Offset: 0x0000442C
		public static bool TryGetAttributeValue(INode n, string attributeName, out string attributeValue)
		{
			attributeValue = null;
			if (!<9d499489-7606-41ac-b77b-5f31a68d089b>AngleSharpUtils.excludedNodeNames.Contains(n.NodeName))
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

		// Token: 0x06000104 RID: 260 RVA: 0x00006269 File Offset: 0x00004469
		public static IHtmlDocument ParseHtmlAndNormalizeStyles(string documentSource)
		{
			IHtmlDocument htmlDocument = <9d499489-7606-41ac-b77b-5f31a68d089b>AngleSharpUtils.defaultHtmlParser.Parse(documentSource);
			<9d499489-7606-41ac-b77b-5f31a68d089b>AngleSharpUtils.NormalizeStyles(htmlDocument.DocumentElement);
			return htmlDocument;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00006281 File Offset: 0x00004481
		public static bool IsValidSelector(string selector)
		{
			return <9d499489-7606-41ac-b77b-5f31a68d089b>AngleSharpUtils.defaultCssParser.ParseSelector(selector) != null;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00006294 File Offset: 0x00004494
		private static void AppendInnerText(INode n, StringBuilder sb)
		{
			if (<9d499489-7606-41ac-b77b-5f31a68d089b>AngleSharpUtils.excludedNodeNames.Contains(n.NodeName))
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
				<9d499489-7606-41ac-b77b-5f31a68d089b>AngleSharpUtils.AppendInnerText(node, sb);
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000630C File Offset: 0x0000450C
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
				<9d499489-7606-41ac-b77b-5f31a68d089b>AngleSharpUtils.NormalizeStyles(element2);
			}
		}

		// Token: 0x040000B4 RID: 180
		private static readonly CssParser defaultCssParser = new CssParser();

		// Token: 0x040000B5 RID: 181
		private static readonly HtmlParser defaultHtmlParser = new HtmlParser(Configuration.Default.WithCss(null));

		// Token: 0x040000B6 RID: 182
		private static readonly HashSet<string> excludedNodeNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "SCRIPT", "STYLE" };
	}
}
