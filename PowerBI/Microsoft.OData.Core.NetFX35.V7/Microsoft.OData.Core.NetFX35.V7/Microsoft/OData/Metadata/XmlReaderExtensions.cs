using System;
using System.Text;
using System.Xml;

namespace Microsoft.OData.Metadata
{
	// Token: 0x020001DD RID: 477
	internal static class XmlReaderExtensions
	{
		// Token: 0x060012A9 RID: 4777 RVA: 0x0003594C File Offset: 0x00033B4C
		internal static string ReadElementValue(this XmlReader reader)
		{
			string text = reader.ReadElementContentValue();
			reader.Read();
			return text;
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00035968 File Offset: 0x00033B68
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
					case 3:
					case 4:
					case 14:
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
					case 7:
					case 8:
					case 13:
						continue;
					case 15:
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

		// Token: 0x060012AB RID: 4779 RVA: 0x00035A58 File Offset: 0x00033C58
		internal static bool NamespaceEquals(this XmlReader reader, string namespaceUri)
		{
			return reader.NamespaceURI == namespaceUri;
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00035A63 File Offset: 0x00033C63
		internal static bool LocalNameEquals(this XmlReader reader, string localName)
		{
			return reader.LocalName == localName;
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00035A6E File Offset: 0x00033C6E
		internal static bool TryReadToNextElement(this XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == 1)
				{
					return true;
				}
			}
			return false;
		}
	}
}
