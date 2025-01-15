using System;
using System.Text;
using AngleSharp.Dom;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.XHtml
{
	// Token: 0x02000023 RID: 35
	public sealed class XhtmlMarkupFormatter : IMarkupFormatter
	{
		// Token: 0x06000110 RID: 272 RVA: 0x00006ECC File Offset: 0x000050CC
		string IMarkupFormatter.CloseTag(IElement element, bool selfClosing)
		{
			string prefix = element.Prefix;
			string localName = element.LocalName;
			string text = ((!string.IsNullOrEmpty(prefix)) ? (prefix + ":" + localName) : localName);
			if (!selfClosing && element.HasChildNodes)
			{
				return "</" + text + ">";
			}
			return string.Empty;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006C04 File Offset: 0x00004E04
		string IMarkupFormatter.Comment(IComment comment)
		{
			return "<!--" + comment.Data + "-->";
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006F20 File Offset: 0x00005120
		string IMarkupFormatter.Doctype(IDocumentType doctype)
		{
			string publicIdentifier = doctype.PublicIdentifier;
			string systemIdentifier = doctype.SystemIdentifier;
			string text = ((string.IsNullOrEmpty(publicIdentifier) && string.IsNullOrEmpty(systemIdentifier)) ? string.Empty : (" " + (string.IsNullOrEmpty(publicIdentifier) ? ("SYSTEM \"" + systemIdentifier + "\"") : string.Concat(new string[] { "PUBLIC \"", publicIdentifier, "\" \"", systemIdentifier, "\"" }))));
			return "<!DOCTYPE " + doctype.Name + text + ">";
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00006FBC File Offset: 0x000051BC
		string IMarkupFormatter.OpenTag(IElement element, bool selfClosing)
		{
			string prefix = element.Prefix;
			StringBuilder stringBuilder = Pool.NewStringBuilder();
			stringBuilder.Append('<');
			if (!string.IsNullOrEmpty(prefix))
			{
				stringBuilder.Append(prefix).Append(':');
			}
			stringBuilder.Append(element.LocalName);
			foreach (IAttr attr in element.Attributes)
			{
				stringBuilder.Append(" ").Append(XhtmlMarkupFormatter.Instance.Attribute(attr));
			}
			if (selfClosing || !element.HasChildNodes)
			{
				stringBuilder.Append(" /");
			}
			stringBuilder.Append('>');
			return stringBuilder.ToPool();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00007080 File Offset: 0x00005280
		string IMarkupFormatter.Processing(IProcessingInstruction processing)
		{
			string text = processing.Target + " " + processing.Data;
			return "<?" + text + "?>";
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000070B4 File Offset: 0x000052B4
		string IMarkupFormatter.Text(string text)
		{
			StringBuilder stringBuilder = Pool.NewStringBuilder();
			int i = 0;
			while (i < text.Length)
			{
				char c = text[i];
				if (c <= '<')
				{
					if (c != '&')
					{
						if (c != '<')
						{
							goto IL_006A;
						}
						stringBuilder.Append("&lt;");
					}
					else
					{
						stringBuilder.Append("&amp;");
					}
				}
				else if (c != '>')
				{
					if (c != '\u00a0')
					{
						goto IL_006A;
					}
					stringBuilder.Append("&#160;");
				}
				else
				{
					stringBuilder.Append("&gt;");
				}
				IL_0078:
				i++;
				continue;
				IL_006A:
				stringBuilder.Append(text[i]);
				goto IL_0078;
			}
			return stringBuilder.ToPool();
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000714C File Offset: 0x0000534C
		string IMarkupFormatter.Attribute(IAttr attribute)
		{
			string namespaceUri = attribute.NamespaceUri;
			string localName = attribute.LocalName;
			string value = attribute.Value;
			StringBuilder stringBuilder = Pool.NewStringBuilder();
			if (string.IsNullOrEmpty(namespaceUri))
			{
				stringBuilder.Append(localName);
			}
			else if (namespaceUri.Is(NamespaceNames.XmlUri))
			{
				stringBuilder.Append(NamespaceNames.XmlPrefix).Append(':').Append(localName);
			}
			else if (namespaceUri.Is(NamespaceNames.XLinkUri))
			{
				stringBuilder.Append(NamespaceNames.XLinkPrefix).Append(':').Append(localName);
			}
			else if (namespaceUri.Is(NamespaceNames.XmlNsUri))
			{
				stringBuilder.Append(XhtmlMarkupFormatter.XmlNamespaceLocalName(localName));
			}
			else
			{
				stringBuilder.Append(attribute.Name);
			}
			stringBuilder.Append('=').Append('"');
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				if (c <= '&')
				{
					if (c != '"')
					{
						if (c != '&')
						{
							goto IL_0122;
						}
						stringBuilder.Append("&amp;");
					}
					else
					{
						stringBuilder.Append("&quot;");
					}
				}
				else if (c != '<')
				{
					if (c != '\u00a0')
					{
						goto IL_0122;
					}
					stringBuilder.Append("&#160;");
				}
				else
				{
					stringBuilder.Append("&lt;");
				}
				IL_0131:
				i++;
				continue;
				IL_0122:
				stringBuilder.Append(value[i]);
				goto IL_0131;
			}
			return stringBuilder.Append('"').ToPool();
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000072AA File Offset: 0x000054AA
		private static string XmlNamespaceLocalName(string name)
		{
			if (!(name != NamespaceNames.XmlNsPrefix))
			{
				return name;
			}
			return NamespaceNames.XmlNsPrefix + ":";
		}

		// Token: 0x040001B7 RID: 439
		public static readonly IMarkupFormatter Instance = new XhtmlMarkupFormatter();
	}
}
