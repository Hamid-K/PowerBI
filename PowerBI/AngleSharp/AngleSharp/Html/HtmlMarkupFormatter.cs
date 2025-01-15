using System;
using System.Text;
using AngleSharp.Dom;
using AngleSharp.Extensions;

namespace AngleSharp.Html
{
	// Token: 0x020000B8 RID: 184
	public sealed class HtmlMarkupFormatter : IMarkupFormatter
	{
		// Token: 0x06000590 RID: 1424 RVA: 0x00006C04 File Offset: 0x00004E04
		string IMarkupFormatter.Comment(IComment comment)
		{
			return "<!--" + comment.Data + "-->";
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0002C5EC File Offset: 0x0002A7EC
		string IMarkupFormatter.Doctype(IDocumentType doctype)
		{
			string ids = HtmlMarkupFormatter.GetIds(doctype.PublicIdentifier, doctype.SystemIdentifier);
			return "<!DOCTYPE " + doctype.Name + ids + ">";
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0002C624 File Offset: 0x0002A824
		string IMarkupFormatter.Processing(IProcessingInstruction processing)
		{
			string text = processing.Target + " " + processing.Data;
			return "<?" + text + ">";
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0002C658 File Offset: 0x0002A858
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
					stringBuilder.Append("&nbsp;");
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

		// Token: 0x06000594 RID: 1428 RVA: 0x0002C6F0 File Offset: 0x0002A8F0
		string IMarkupFormatter.OpenTag(IElement element, bool selfClosing)
		{
			StringBuilder stringBuilder = Pool.NewStringBuilder();
			stringBuilder.Append('<');
			if (!string.IsNullOrEmpty(element.Prefix))
			{
				stringBuilder.Append(element.Prefix).Append(':');
			}
			stringBuilder.Append(element.LocalName);
			foreach (IAttr attr in element.Attributes)
			{
				stringBuilder.Append(" ").Append(HtmlMarkupFormatter.Instance.Attribute(attr));
			}
			stringBuilder.Append('>');
			return stringBuilder.ToPool();
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0002C7A0 File Offset: 0x0002A9A0
		string IMarkupFormatter.CloseTag(IElement element, bool selfClosing)
		{
			string prefix = element.Prefix;
			string localName = element.LocalName;
			string text = ((!string.IsNullOrEmpty(prefix)) ? (prefix + ":" + localName) : localName);
			if (!selfClosing)
			{
				return "</" + text + ">";
			}
			return string.Empty;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0002C7EC File Offset: 0x0002A9EC
		string IMarkupFormatter.Attribute(IAttr attr)
		{
			string namespaceUri = attr.NamespaceUri;
			string localName = attr.LocalName;
			string value = attr.Value;
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
				stringBuilder.Append(HtmlMarkupFormatter.XmlNamespaceLocalName(localName));
			}
			else
			{
				stringBuilder.Append(attr.Name);
			}
			stringBuilder.Append('=').Append('"');
			for (int i = 0; i < value.Length; i++)
			{
				char c = value[i];
				if (c != '"')
				{
					if (c != '&')
					{
						if (c != '\u00a0')
						{
							stringBuilder.Append(value[i]);
						}
						else
						{
							stringBuilder.Append("&nbsp;");
						}
					}
					else
					{
						stringBuilder.Append("&amp;");
					}
				}
				else
				{
					stringBuilder.Append("&quot;");
				}
			}
			return stringBuilder.Append('"').ToPool();
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0002C92C File Offset: 0x0002AB2C
		private static string GetIds(string publicId, string systemId)
		{
			if (string.IsNullOrEmpty(publicId) && string.IsNullOrEmpty(systemId))
			{
				return string.Empty;
			}
			if (string.IsNullOrEmpty(systemId))
			{
				return string.Format(" PUBLIC \"{0}\"", publicId);
			}
			if (string.IsNullOrEmpty(publicId))
			{
				return string.Format(" SYSTEM \"{0}\"", systemId);
			}
			return string.Format(" PUBLIC \"{0}\" \"{1}\"", publicId, systemId);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x000072AA File Offset: 0x000054AA
		private static string XmlNamespaceLocalName(string name)
		{
			if (!(name != NamespaceNames.XmlNsPrefix))
			{
				return name;
			}
			return NamespaceNames.XmlNsPrefix + ":";
		}

		// Token: 0x040004E6 RID: 1254
		public static readonly IMarkupFormatter Instance = new HtmlMarkupFormatter();
	}
}
