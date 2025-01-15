using System;
using System.Diagnostics;
using System.Text;
using System.Xml;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200022D RID: 557
	internal static class XmlReaderExtensions
	{
		// Token: 0x060010CF RID: 4303 RVA: 0x0003F10E File Offset: 0x0003D30E
		[Conditional("DEBUG")]
		internal static void AssertNotBuffering(this BufferingXmlReader bufferedXmlReader)
		{
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0003F110 File Offset: 0x0003D310
		[Conditional("DEBUG")]
		internal static void AssertBuffering(this BufferingXmlReader bufferedXmlReader)
		{
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0003F114 File Offset: 0x0003D314
		internal static string ReadElementValue(this XmlReader reader)
		{
			string text = reader.ReadElementContentValue();
			reader.Read();
			return text;
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0003F130 File Offset: 0x0003D330
		internal static string ReadFirstTextNodeValue(this XmlReader reader)
		{
			reader.MoveToElement();
			string text = null;
			if (!reader.IsEmptyElement)
			{
				bool flag = false;
				while (!flag && reader.Read())
				{
					XmlNodeType nodeType = reader.NodeType;
					switch (nodeType)
					{
					case 1:
						reader.SkipElementContent();
						continue;
					case 2:
						continue;
					case 3:
					case 4:
						break;
					default:
						switch (nodeType)
						{
						case 14:
							break;
						case 15:
							flag = true;
							continue;
						default:
							continue;
						}
						break;
					}
					if (text == null)
					{
						text = reader.Value;
					}
				}
			}
			reader.Read();
			return text ?? string.Empty;
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0003F1B8 File Offset: 0x0003D3B8
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

		// Token: 0x060010D4 RID: 4308 RVA: 0x0003F2A8 File Offset: 0x0003D4A8
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

		// Token: 0x060010D5 RID: 4309 RVA: 0x0003F307 File Offset: 0x0003D507
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

		// Token: 0x060010D6 RID: 4310 RVA: 0x0003F331 File Offset: 0x0003D531
		internal static void ReadPayloadStart(this XmlReader reader)
		{
			reader.SkipInsignificantNodes();
			if (reader.NodeType != 1)
			{
				throw new ODataException(Strings.XmlReaderExtension_InvalidRootNode(reader.NodeType));
			}
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0003F358 File Offset: 0x0003D558
		internal static void ReadPayloadEnd(this XmlReader reader)
		{
			reader.SkipInsignificantNodes();
			if (reader.NodeType != null && !reader.EOF)
			{
				throw new ODataException(Strings.XmlReaderExtension_InvalidRootNode(reader.NodeType));
			}
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0003F386 File Offset: 0x0003D586
		internal static bool NamespaceEquals(this XmlReader reader, string namespaceUri)
		{
			return object.ReferenceEquals(reader.NamespaceURI, namespaceUri);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0003F394 File Offset: 0x0003D594
		internal static bool LocalNameEquals(this XmlReader reader, string localName)
		{
			return object.ReferenceEquals(reader.LocalName, localName);
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0003F3A2 File Offset: 0x0003D5A2
		internal static bool TryReadEmptyElement(this XmlReader reader)
		{
			reader.MoveToElement();
			return reader.IsEmptyElement || (reader.Read() && reader.NodeType == 15);
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0003F3CA File Offset: 0x0003D5CA
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

		// Token: 0x060010DC RID: 4316 RVA: 0x0003F3E4 File Offset: 0x0003D5E4
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
