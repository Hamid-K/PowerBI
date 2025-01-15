using System;
using System.Text;
using System.Xml;

namespace Microsoft.OData.Metadata
{
	// Token: 0x020000F8 RID: 248
	internal static class XmlReaderExtensions
	{
		// Token: 0x06000E94 RID: 3732 RVA: 0x00022EE4 File Offset: 0x000210E4
		internal static string ReadElementValue(this XmlReader reader)
		{
			string text = reader.ReadElementContentValue();
			reader.Read();
			return text;
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x00022F00 File Offset: 0x00021100
		internal static string ReadElementContentValue(this XmlReader reader)
		{
			reader.MoveToElement();
			string text = null;
			if (reader.IsEmptyElement)
			{
				text = string.Empty;
			}
			else
			{
				StringBuilder stringBuilder = null;
				bool flag = false;
				while (!flag && reader.Read())
				{
					switch (reader.NodeType)
					{
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
					case XmlNodeType.SignificantWhitespace:
						if (text == null)
						{
							text = reader.Value;
							continue;
						}
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder();
							stringBuilder.Append(text);
							stringBuilder.Append(reader.Value);
							continue;
						}
						stringBuilder.Append(reader.Value);
						continue;
					case XmlNodeType.ProcessingInstruction:
					case XmlNodeType.Comment:
					case XmlNodeType.Whitespace:
						continue;
					case XmlNodeType.EndElement:
						flag = true;
						continue;
					}
					throw new ODataException(Strings.XmlReaderExtension_InvalidNodeInStringValue(reader.NodeType));
				}
				if (stringBuilder != null)
				{
					text = stringBuilder.ToString();
				}
				else if (text == null)
				{
					text = string.Empty;
				}
			}
			return text;
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00022FF0 File Offset: 0x000211F0
		internal static bool NamespaceEquals(this XmlReader reader, string namespaceUri)
		{
			return reader.NamespaceURI == namespaceUri;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x00022FFB File Offset: 0x000211FB
		internal static bool LocalNameEquals(this XmlReader reader, string localName)
		{
			return reader.LocalName == localName;
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x00023006 File Offset: 0x00021206
		internal static bool TryReadToNextElement(this XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element)
				{
					return true;
				}
			}
			return false;
		}
	}
}
