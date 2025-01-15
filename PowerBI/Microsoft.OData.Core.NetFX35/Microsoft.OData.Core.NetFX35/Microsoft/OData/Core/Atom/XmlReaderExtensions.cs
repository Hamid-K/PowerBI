using System;
using System.Diagnostics;
using System.Text;
using System.Xml;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x0200006E RID: 110
	internal static class XmlReaderExtensions
	{
		// Token: 0x06000478 RID: 1144 RVA: 0x00010E17 File Offset: 0x0000F017
		[Conditional("DEBUG")]
		internal static void AssertNotBuffering(this BufferingXmlReader bufferedXmlReader)
		{
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00010E19 File Offset: 0x0000F019
		[Conditional("DEBUG")]
		internal static void AssertBuffering(this BufferingXmlReader bufferedXmlReader)
		{
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00010E1C File Offset: 0x0000F01C
		internal static string ReadElementValue(this XmlReader reader)
		{
			string text = reader.ReadElementContentValue();
			reader.Read();
			return text;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00010E38 File Offset: 0x0000F038
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

		// Token: 0x0600047C RID: 1148 RVA: 0x00010F28 File Offset: 0x0000F128
		internal static void SkipInsignificantNodes(this XmlReader reader)
		{
			for (;;)
			{
				XmlNodeType nodeType = reader.NodeType;
				switch (nodeType)
				{
				case 0:
				case 7:
				case 8:
					break;
				case 1:
					return;
				case 2:
				case 4:
				case 5:
				case 6:
					return;
				case 3:
					if (!XmlReaderExtensions.IsNullOrWhitespace(reader.Value))
					{
						return;
					}
					break;
				default:
					if (nodeType != 13 && nodeType != 17)
					{
						return;
					}
					break;
				}
				if (!reader.Read())
				{
					return;
				}
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00010F87 File Offset: 0x0000F187
		internal static void SkipElementContent(this XmlReader reader)
		{
			reader.MoveToElement();
			if (!reader.IsEmptyElement)
			{
				reader.Read();
				while (reader.NodeType != 15)
				{
					reader.Skip();
				}
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00010FB1 File Offset: 0x0000F1B1
		internal static void ReadPayloadStart(this XmlReader reader)
		{
			reader.SkipInsignificantNodes();
			if (reader.NodeType != 1)
			{
				throw new ODataException(Strings.XmlReaderExtension_InvalidRootNode(reader.NodeType));
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00010FD8 File Offset: 0x0000F1D8
		internal static void ReadPayloadEnd(this XmlReader reader)
		{
			reader.SkipInsignificantNodes();
			if (reader.NodeType != null && !reader.EOF)
			{
				throw new ODataException(Strings.XmlReaderExtension_InvalidRootNode(reader.NodeType));
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00011006 File Offset: 0x0000F206
		internal static bool NamespaceEquals(this XmlReader reader, string namespaceUri)
		{
			return object.ReferenceEquals(reader.NamespaceURI, namespaceUri);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00011014 File Offset: 0x0000F214
		internal static bool LocalNameEquals(this XmlReader reader, string localName)
		{
			return object.ReferenceEquals(reader.LocalName, localName);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00011022 File Offset: 0x0000F222
		internal static bool TryReadEmptyElement(this XmlReader reader)
		{
			reader.MoveToElement();
			return reader.IsEmptyElement || (reader.Read() && reader.NodeType == 15);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0001104A File Offset: 0x0000F24A
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

		// Token: 0x06000484 RID: 1156 RVA: 0x00011064 File Offset: 0x0000F264
		private static bool IsNullOrWhitespace(string text)
		{
			if (text == null)
			{
				return true;
			}
			for (int i = 0; i < text.Length; i++)
			{
				char c = text.get_Chars(i);
				if (!char.IsWhiteSpace(c))
				{
					return false;
				}
			}
			return true;
		}
	}
}
