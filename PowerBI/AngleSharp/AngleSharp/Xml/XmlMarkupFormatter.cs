using System;
using System.Text;
using AngleSharp.Dom;

namespace AngleSharp.Xml
{
	// Token: 0x02000022 RID: 34
	public sealed class XmlMarkupFormatter : IMarkupFormatter
	{
		// Token: 0x06000107 RID: 263 RVA: 0x00006BB8 File Offset: 0x00004DB8
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

		// Token: 0x06000108 RID: 264 RVA: 0x00006C04 File Offset: 0x00004E04
		string IMarkupFormatter.Comment(IComment comment)
		{
			return "<!--" + comment.Data + "-->";
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00006C1C File Offset: 0x00004E1C
		string IMarkupFormatter.Doctype(IDocumentType doctype)
		{
			string publicIdentifier = doctype.PublicIdentifier;
			string systemIdentifier = doctype.SystemIdentifier;
			string text = ((string.IsNullOrEmpty(publicIdentifier) && string.IsNullOrEmpty(systemIdentifier)) ? string.Empty : (" " + (string.IsNullOrEmpty(publicIdentifier) ? ("SYSTEM \"" + systemIdentifier + "\"") : string.Concat(new string[] { "PUBLIC \"", publicIdentifier, "\" \"", systemIdentifier, "\"" }))));
			return "<!DOCTYPE " + doctype.Name + text + ">";
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006CB8 File Offset: 0x00004EB8
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
				stringBuilder.Append(" ").Append(XmlMarkupFormatter.Instance.Attribute(attr));
			}
			if (selfClosing)
			{
				stringBuilder.Append(" /");
			}
			stringBuilder.Append('>');
			return stringBuilder.ToPool();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006D74 File Offset: 0x00004F74
		string IMarkupFormatter.Processing(IProcessingInstruction processing)
		{
			string text = processing.Target + " " + processing.Data;
			return "<?" + text + "?>";
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006DA8 File Offset: 0x00004FA8
		string IMarkupFormatter.Text(string text)
		{
			StringBuilder stringBuilder = Pool.NewStringBuilder();
			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];
				if (c != '&')
				{
					if (c != '<')
					{
						stringBuilder.Append(text[i]);
					}
					else
					{
						stringBuilder.Append("&lt;");
					}
				}
				else
				{
					stringBuilder.Append("&amp;");
				}
			}
			return stringBuilder.ToPool();
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006E10 File Offset: 0x00005010
		string IMarkupFormatter.Attribute(IAttr attribute)
		{
			string value = attribute.Value;
			StringBuilder stringBuilder = Pool.NewStringBuilder();
			stringBuilder.Append(attribute.Name);
			stringBuilder.Append('=').Append('"');
			for (int i = 0; i < value.Length; i++)
			{
				char c = value[i];
				if (c != '"')
				{
					if (c != '&')
					{
						if (c != '<')
						{
							stringBuilder.Append(value[i]);
						}
						else
						{
							stringBuilder.Append("&lt;");
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

		// Token: 0x040001B6 RID: 438
		public static readonly IMarkupFormatter Instance = new XmlMarkupFormatter();
	}
}
