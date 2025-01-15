using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Schema;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000035 RID: 53
	internal class BinaryXmlReader : XmlReader, IXmlNamespaceResolver
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x0000B0E1 File Offset: 0x000092E1
		public BinaryXmlReader(XmlReader xmlReader)
		{
			this.xmlReader = xmlReader;
			this.currentRowsetRoot = string.Empty;
			this.columnNameLookupTable = new Dictionary<string, string>();
			this.canSkipNameLookup = true;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000B10D File Offset: 0x0000930D
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.xmlReader.Settings;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000B11A File Offset: 0x0000931A
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.xmlReader.SchemaInfo;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000B127 File Offset: 0x00009327
		public override Type ValueType
		{
			get
			{
				return this.xmlReader.ValueType;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000B134 File Offset: 0x00009334
		public override XmlNodeType NodeType
		{
			get
			{
				return this.xmlReader.NodeType;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000B141 File Offset: 0x00009341
		public override string Name
		{
			get
			{
				return this.xmlReader.Name;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000B14E File Offset: 0x0000934E
		public override string LocalName
		{
			get
			{
				return this.xmlReader.LocalName;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000B15B File Offset: 0x0000935B
		public override string NamespaceURI
		{
			get
			{
				return this.xmlReader.NamespaceURI;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000B168 File Offset: 0x00009368
		public override string Prefix
		{
			get
			{
				return this.xmlReader.Prefix;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000B175 File Offset: 0x00009375
		public override bool HasValue
		{
			get
			{
				return this.xmlReader.HasValue;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000B184 File Offset: 0x00009384
		public override string Value
		{
			get
			{
				if (this.xmlReader.ValueType == typeof(DateTime))
				{
					return BinaryXmlReader.ConvertDateTimeToString(DateTime.Parse(this.xmlReader.Value, CultureInfo.InvariantCulture));
				}
				return this.xmlReader.Value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000B1D3 File Offset: 0x000093D3
		public override int Depth
		{
			get
			{
				return this.xmlReader.Depth;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000B1E0 File Offset: 0x000093E0
		public override string BaseURI
		{
			get
			{
				return this.xmlReader.BaseURI;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000B1ED File Offset: 0x000093ED
		public override bool IsEmptyElement
		{
			get
			{
				return this.xmlReader.IsEmptyElement;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000B1FA File Offset: 0x000093FA
		public override bool IsDefault
		{
			get
			{
				return this.xmlReader.IsDefault;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000B207 File Offset: 0x00009407
		public override char QuoteChar
		{
			get
			{
				return this.xmlReader.QuoteChar;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000B214 File Offset: 0x00009414
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.xmlReader.XmlSpace;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000B221 File Offset: 0x00009421
		public override string XmlLang
		{
			get
			{
				return this.xmlReader.XmlLang;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000B22E File Offset: 0x0000942E
		public override int AttributeCount
		{
			get
			{
				return this.xmlReader.AttributeCount;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000B23B File Offset: 0x0000943B
		public override bool CanResolveEntity
		{
			get
			{
				return this.xmlReader.CanResolveEntity;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000B248 File Offset: 0x00009448
		public override bool EOF
		{
			get
			{
				return this.xmlReader.EOF;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000B255 File Offset: 0x00009455
		public override ReadState ReadState
		{
			get
			{
				return this.xmlReader.ReadState;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000B262 File Offset: 0x00009462
		public override bool HasAttributes
		{
			get
			{
				return this.xmlReader.HasAttributes;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000B26F File Offset: 0x0000946F
		public override XmlNameTable NameTable
		{
			get
			{
				return this.xmlReader.NameTable;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000B27C File Offset: 0x0000947C
		public override bool CanReadBinaryContent
		{
			get
			{
				return this.xmlReader.CanReadBinaryContent;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000B289 File Offset: 0x00009489
		public override bool CanReadValueChunk
		{
			get
			{
				return this.xmlReader.CanReadValueChunk;
			}
		}

		// Token: 0x1700008A RID: 138
		public override string this[int i]
		{
			get
			{
				return this.xmlReader[i];
			}
		}

		// Token: 0x1700008B RID: 139
		public override string this[string name]
		{
			get
			{
				return this.xmlReader[name];
			}
		}

		// Token: 0x1700008C RID: 140
		public override string this[string name, string namespaceURI]
		{
			get
			{
				return this.xmlReader[name, namespaceURI];
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000B2C1 File Offset: 0x000094C1
		public override string GetAttribute(string name)
		{
			return this.xmlReader.GetAttribute(name);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000B2CF File Offset: 0x000094CF
		public override string GetAttribute(string name, string namespaceURI)
		{
			return this.xmlReader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000B2DE File Offset: 0x000094DE
		public override string GetAttribute(int i)
		{
			return this.xmlReader.GetAttribute(i);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000B2EC File Offset: 0x000094EC
		public override bool MoveToAttribute(string name)
		{
			return this.xmlReader.MoveToAttribute(name);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000B2FA File Offset: 0x000094FA
		public override bool MoveToAttribute(string name, string ns)
		{
			return this.xmlReader.MoveToAttribute(name, ns);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000B309 File Offset: 0x00009509
		public override void MoveToAttribute(int i)
		{
			this.xmlReader.MoveToAttribute(i);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000B317 File Offset: 0x00009517
		public override bool MoveToFirstAttribute()
		{
			return this.xmlReader.MoveToFirstAttribute();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000B324 File Offset: 0x00009524
		public override bool MoveToNextAttribute()
		{
			return this.xmlReader.MoveToNextAttribute();
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000B331 File Offset: 0x00009531
		public override bool MoveToElement()
		{
			return this.xmlReader.MoveToElement();
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000B340 File Offset: 0x00009540
		public override bool Read()
		{
			if (!this.IsEmptyElement)
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						this.PopRowsetRoot();
					}
				}
				else
				{
					this.PushRowsetRoot(this.Name);
				}
			}
			return this.xmlReader.Read();
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000B386 File Offset: 0x00009586
		public override void Close()
		{
			this.xmlReader.Close();
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000B393 File Offset: 0x00009593
		public override void Skip()
		{
			if (this.IsStartElement("schema", "http://www.w3.org/2001/XMLSchema"))
			{
				this.LoadSchema();
				return;
			}
			this.xmlReader.Skip();
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000B3B9 File Offset: 0x000095B9
		public override string ReadString()
		{
			if (this.xmlReader.ValueType == typeof(DateTime))
			{
				return BinaryXmlReader.ConvertDateTimeToString(this.ReadContentAsDateTime());
			}
			return this.xmlReader.ReadString();
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000B3EE File Offset: 0x000095EE
		public override XmlNodeType MoveToContent()
		{
			return this.xmlReader.MoveToContent();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000B3FB File Offset: 0x000095FB
		public override void ReadStartElement()
		{
			this.ReadStartElementImpl(null, null);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000B405 File Offset: 0x00009605
		public override void ReadStartElement(string name)
		{
			this.ReadStartElementImpl(name, null);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000B40F File Offset: 0x0000960F
		public override void ReadStartElement(string localname, string ns)
		{
			this.ReadStartElementImpl(localname, ns);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000B419 File Offset: 0x00009619
		public override string ReadElementString()
		{
			return this.ReadElementStringImpl(null, null);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000B423 File Offset: 0x00009623
		public override string ReadElementString(string name)
		{
			return this.ReadElementStringImpl(name, null);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000B42D File Offset: 0x0000962D
		public override string ReadElementString(string localname, string ns)
		{
			return this.ReadElementStringImpl(localname, ns);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000B437 File Offset: 0x00009637
		public override void ReadEndElement()
		{
			this.xmlReader.ReadEndElement();
			this.PopRowsetRoot();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000B44A File Offset: 0x0000964A
		public override bool IsStartElement()
		{
			return this.xmlReader.IsStartElement();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000B457 File Offset: 0x00009657
		public override bool IsStartElement(string name)
		{
			return this.xmlReader.IsStartElement(this.GetColumnNameFromCaption(name));
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000B46B File Offset: 0x0000966B
		public override bool IsStartElement(string localname, string ns)
		{
			return this.xmlReader.IsStartElement(this.GetColumnNameFromCaption(localname), ns);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000B480 File Offset: 0x00009680
		public override string ReadInnerXml()
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return string.Empty;
			}
			if (this.NodeType != XmlNodeType.Attribute && this.NodeType != XmlNodeType.Element)
			{
				this.Read();
				return string.Empty;
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				if (this.NodeType == XmlNodeType.Attribute)
				{
					xmlTextWriter.QuoteChar = this.QuoteChar;
					this.WriteAttributeValue(xmlTextWriter);
				}
				if (this.NodeType == XmlNodeType.Element)
				{
					this.WriteNode(xmlTextWriter, false);
				}
			}
			return stringWriter.ToString();
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000B51C File Offset: 0x0000971C
		public override string ReadOuterXml()
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return string.Empty;
			}
			if (this.NodeType != XmlNodeType.Attribute && this.NodeType != XmlNodeType.Element)
			{
				this.Read();
				return string.Empty;
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				if (this.NodeType == XmlNodeType.Attribute)
				{
					xmlTextWriter.WriteStartAttribute(this.Prefix, this.LocalName, this.NamespaceURI);
					this.WriteAttributeValue(xmlTextWriter);
					xmlTextWriter.WriteEndAttribute();
				}
				else
				{
					xmlTextWriter.WriteNode(this, false);
				}
			}
			return stringWriter.ToString();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000B5C4 File Offset: 0x000097C4
		public override string LookupNamespace(string prefix)
		{
			return this.xmlReader.LookupNamespace(prefix);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000B5D2 File Offset: 0x000097D2
		public override void ResolveEntity()
		{
			this.xmlReader.ResolveEntity();
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000B5DF File Offset: 0x000097DF
		public override bool ReadAttributeValue()
		{
			return this.xmlReader.ReadAttributeValue();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000B5EC File Offset: 0x000097EC
		public override DateTime ReadContentAsDateTime()
		{
			return this.xmlReader.ReadContentAsDateTime();
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000B5F9 File Offset: 0x000097F9
		public override double ReadContentAsDouble()
		{
			return this.xmlReader.ReadContentAsDouble();
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000B606 File Offset: 0x00009806
		public override int ReadContentAsInt()
		{
			return this.xmlReader.ReadContentAsInt();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000B613 File Offset: 0x00009813
		public override long ReadContentAsLong()
		{
			return this.xmlReader.ReadContentAsLong();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000B620 File Offset: 0x00009820
		public override object ReadContentAsObject()
		{
			return this.xmlReader.ReadContentAsObject();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000B62D File Offset: 0x0000982D
		public override object ReadContentAs(Type type, IXmlNamespaceResolver resolver)
		{
			return this.xmlReader.ReadContentAs(type, resolver);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000B63C File Offset: 0x0000983C
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.xmlReader.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000B64C File Offset: 0x0000984C
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.xmlReader.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000B65C File Offset: 0x0000985C
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			return this.xmlReader.ReadElementContentAs(returnType, namespaceResolver);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000B66B File Offset: 0x0000986B
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver, string localName, string namespaceURI)
		{
			return this.xmlReader.ReadElementContentAs(returnType, namespaceResolver, localName, namespaceURI);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000B67D File Offset: 0x0000987D
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.xmlReader.ReadElementContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000B68D File Offset: 0x0000988D
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.xmlReader.ReadElementContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000B69D File Offset: 0x0000989D
		public override bool ReadElementContentAsBoolean()
		{
			return this.xmlReader.ReadElementContentAsBoolean();
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000B6AA File Offset: 0x000098AA
		public override bool ReadElementContentAsBoolean(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadElementContentAsBoolean(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000B6BF File Offset: 0x000098BF
		public override DateTime ReadElementContentAsDateTime()
		{
			return this.xmlReader.ReadElementContentAsDateTime();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000B6CC File Offset: 0x000098CC
		public override DateTime ReadElementContentAsDateTime(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadElementContentAsDateTime(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000B6E1 File Offset: 0x000098E1
		public override double ReadElementContentAsDouble()
		{
			return this.xmlReader.ReadElementContentAsDouble();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000B6EE File Offset: 0x000098EE
		public override double ReadElementContentAsDouble(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadElementContentAsDouble(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000B703 File Offset: 0x00009903
		public override int ReadElementContentAsInt()
		{
			return this.xmlReader.ReadElementContentAsInt();
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000B710 File Offset: 0x00009910
		public override int ReadElementContentAsInt(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadElementContentAsInt(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000B725 File Offset: 0x00009925
		public override long ReadElementContentAsLong()
		{
			return this.xmlReader.ReadElementContentAsLong();
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000B732 File Offset: 0x00009932
		public override long ReadElementContentAsLong(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadElementContentAsLong(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000B747 File Offset: 0x00009947
		public override object ReadElementContentAsObject()
		{
			return this.xmlReader.ReadElementContentAsObject();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000B754 File Offset: 0x00009954
		public override object ReadElementContentAsObject(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadElementContentAsObject(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000B769 File Offset: 0x00009969
		public override string ReadElementContentAsString()
		{
			return this.xmlReader.ReadElementContentAsString();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000B776 File Offset: 0x00009976
		public override string ReadElementContentAsString(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadElementContentAsString(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000B78B File Offset: 0x0000998B
		public override bool ReadToFollowing(string name)
		{
			return this.xmlReader.ReadToFollowing(this.GetColumnNameFromCaption(name));
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000B79F File Offset: 0x0000999F
		public override bool ReadToFollowing(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadToFollowing(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000B7B4 File Offset: 0x000099B4
		public override bool ReadToNextSibling(string name)
		{
			return this.xmlReader.ReadToNextSibling(this.GetColumnNameFromCaption(name));
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000B7C8 File Offset: 0x000099C8
		public override bool ReadToNextSibling(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadToNextSibling(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000B7DD File Offset: 0x000099DD
		public override bool ReadToDescendant(string name)
		{
			return this.xmlReader.ReadToDescendant(this.GetColumnNameFromCaption(name));
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000B7F1 File Offset: 0x000099F1
		public override bool ReadToDescendant(string localName, string ns)
		{
			return this.xmlReader.ReadToDescendant(this.GetColumnNameFromCaption(localName), ns);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000B806 File Offset: 0x00009A06
		public override XmlReader ReadSubtree()
		{
			return this.xmlReader.ReadSubtree();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000B813 File Offset: 0x00009A13
		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			return this.xmlReader.ReadValueChunk(buffer, index, count);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000B823 File Offset: 0x00009A23
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return ((IXmlNamespaceResolver)this.xmlReader).GetNamespacesInScope(scope);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000B836 File Offset: 0x00009A36
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return ((IXmlNamespaceResolver)this.xmlReader).LookupNamespace(prefix);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000B849 File Offset: 0x00009A49
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return ((IXmlNamespaceResolver)this.xmlReader).LookupPrefix(namespaceName);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000B85C File Offset: 0x00009A5C
		private static string ConvertDateTimeToString(DateTime dateTime)
		{
			string text = ((dateTime.Millisecond != 0) ? "yyyy-MM-dd\\THH:mm:ss.ffffff" : "yyyy-MM-dd\\THH:mm:ss");
			return XmlConvert.ToString(dateTime, text);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000B886 File Offset: 0x00009A86
		private static string GetColumnNameLookupKey(string parent, string name)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", parent, '>', name);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000B8A0 File Offset: 0x00009AA0
		private void ReadStartElementImpl(string localname, string ns)
		{
			string name = this.Name;
			if (!string.IsNullOrEmpty(localname))
			{
				string columnNameFromCaption = this.GetColumnNameFromCaption(localname);
				if (!string.IsNullOrEmpty(ns))
				{
					this.xmlReader.ReadStartElement(columnNameFromCaption, ns);
				}
				else
				{
					this.xmlReader.ReadStartElement(columnNameFromCaption);
				}
			}
			else
			{
				this.xmlReader.ReadStartElement();
			}
			this.PushRowsetRoot(name);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000B8FC File Offset: 0x00009AFC
		private string ReadElementStringImpl(string localname, string ns)
		{
			string text = string.Empty;
			this.MoveToContent();
			if (!string.IsNullOrEmpty(localname))
			{
				string columnNameFromCaption = this.GetColumnNameFromCaption(localname);
				if (this.LocalName != columnNameFromCaption || (!string.IsNullOrEmpty(ns) && this.NamespaceURI != ns))
				{
					throw new ResponseFormatException(XmlaSR.UnexpectedElement(columnNameFromCaption, ns ?? string.Empty), "");
				}
			}
			if (!this.IsEmptyElement)
			{
				this.Read();
				text = this.ReadString();
				this.Read();
			}
			else
			{
				this.Read();
			}
			return text;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000B98C File Offset: 0x00009B8C
		private void WriteNode(XmlTextWriter xtw, bool defattr)
		{
			int num = ((this.NodeType == XmlNodeType.None) ? (-1) : this.Depth);
			while (this.Read() && num < this.Depth)
			{
				switch (this.NodeType)
				{
				case XmlNodeType.Element:
					xtw.WriteStartElement(this.Prefix, this.LocalName, this.NamespaceURI);
					xtw.QuoteChar = this.QuoteChar;
					xtw.WriteAttributes(this, defattr);
					if (this.IsEmptyElement)
					{
						xtw.WriteEndElement();
					}
					break;
				case XmlNodeType.Text:
					xtw.WriteString(this.Value);
					break;
				case XmlNodeType.CDATA:
					xtw.WriteCData(this.Value);
					break;
				case XmlNodeType.EntityReference:
					xtw.WriteEntityRef(this.Name);
					break;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.XmlDeclaration:
					xtw.WriteProcessingInstruction(this.Name, this.Value);
					break;
				case XmlNodeType.Comment:
					xtw.WriteComment(this.Value);
					break;
				case XmlNodeType.DocumentType:
					xtw.WriteDocType(this.Name, this.GetAttribute("PUBLIC"), this.GetAttribute("SYSTEM"), this.Value);
					break;
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					xtw.WriteWhitespace(this.Value);
					break;
				case XmlNodeType.EndElement:
					xtw.WriteFullEndElement();
					break;
				}
			}
			if (num == this.Depth && this.NodeType == XmlNodeType.EndElement)
			{
				this.Read();
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000BB04 File Offset: 0x00009D04
		private void WriteAttributeValue(XmlWriter xtw)
		{
			string name = this.Name;
			while (this.ReadAttributeValue())
			{
				if (this.NodeType == XmlNodeType.EntityReference)
				{
					xtw.WriteEntityRef(this.Name);
				}
				else
				{
					xtw.WriteString(this.Value);
				}
			}
			this.MoveToAttribute(name);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000BB50 File Offset: 0x00009D50
		private void LoadSchema()
		{
			XmlSchema xmlSchema = XmlSchema.Read(this.xmlReader, null);
			this.xmlReader.ReadEndElement();
			this.columnNameLookupTable.Clear();
			this.currentRowsetRoot = string.Empty;
			int num = 0;
			foreach (XmlSchemaObject xmlSchemaObject in xmlSchema.Items)
			{
				XmlSchemaComplexType xmlSchemaComplexType = xmlSchemaObject as XmlSchemaComplexType;
				if (xmlSchemaComplexType != null && xmlSchemaComplexType.Name == "row")
				{
					num += this.LoadComplexType(xmlSchemaComplexType, "row");
					break;
				}
			}
			this.canSkipNameLookup = num == 0;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000BC00 File Offset: 0x00009E00
		private int LoadComplexType(XmlSchemaComplexType complexType, string parent)
		{
			int num = 1;
			foreach (XmlSchemaObject xmlSchemaObject in ((XmlSchemaSequence)complexType.Particle).Items)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)xmlSchemaObject;
				string text = ((xmlSchemaElement.Name != null) ? xmlSchemaElement.Name : xmlSchemaElement.QualifiedName.Name);
				string columnCaptionFromSchemaElement = FormattersHelpers.GetColumnCaptionFromSchemaElement(xmlSchemaElement);
				string columnNameLookupKey = BinaryXmlReader.GetColumnNameLookupKey(parent, columnCaptionFromSchemaElement);
				this.columnNameLookupTable[columnNameLookupKey] = text;
				if (FormattersHelpers.IsNestedRowsetColumn(xmlSchemaElement))
				{
					string columnNameLookupKey2 = BinaryXmlReader.GetColumnNameLookupKey(parent, text);
					num += this.LoadComplexType((XmlSchemaComplexType)xmlSchemaElement.SchemaType, columnNameLookupKey2);
				}
			}
			return num;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000BCC8 File Offset: 0x00009EC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private string GetColumnNameFromCaption(string caption)
		{
			if (this.canSkipNameLookup)
			{
				return caption;
			}
			string columnNameLookupKey = BinaryXmlReader.GetColumnNameLookupKey(this.currentRowsetRoot, caption);
			string text;
			if (this.columnNameLookupTable.TryGetValue(columnNameLookupKey, out text))
			{
				return text;
			}
			return caption;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000BD00 File Offset: 0x00009F00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void PushRowsetRoot(string name)
		{
			if (this.canSkipNameLookup)
			{
				return;
			}
			if (string.IsNullOrEmpty(this.currentRowsetRoot))
			{
				this.currentRowsetRoot = name;
				return;
			}
			this.currentRowsetRoot = string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", this.currentRowsetRoot, '>', name);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000BD50 File Offset: 0x00009F50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void PopRowsetRoot()
		{
			if (this.canSkipNameLookup)
			{
				return;
			}
			int num = this.currentRowsetRoot.LastIndexOf('>');
			this.currentRowsetRoot = ((num == -1) ? string.Empty : this.currentRowsetRoot.Substring(0, num));
		}

		// Token: 0x040001DD RID: 477
		private const char HashKeyDelimeter = '>';

		// Token: 0x040001DE RID: 478
		private const string HashKeyTemplate = "{0}{1}{2}";

		// Token: 0x040001DF RID: 479
		private readonly XmlReader xmlReader;

		// Token: 0x040001E0 RID: 480
		private readonly Dictionary<string, string> columnNameLookupTable;

		// Token: 0x040001E1 RID: 481
		private bool canSkipNameLookup;

		// Token: 0x040001E2 RID: 482
		private string currentRowsetRoot;
	}
}
