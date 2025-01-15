using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Schema;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200001D RID: 29
	internal class BinaryXmlReader : XmlReader, IXmlNamespaceResolver
	{
		// Token: 0x06000128 RID: 296 RVA: 0x00007EE5 File Offset: 0x000060E5
		public BinaryXmlReader(XmlReader xmlReader)
		{
			this.xmlReader = xmlReader;
			this.currentRowsetRoot = string.Empty;
			this.columnNameLookupTable = new Dictionary<string, string>();
			this.canSkipNameLookup = true;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00007F11 File Offset: 0x00006111
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.xmlReader.Settings;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00007F1E File Offset: 0x0000611E
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.xmlReader.SchemaInfo;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00007F2B File Offset: 0x0000612B
		public override Type ValueType
		{
			get
			{
				return this.xmlReader.ValueType;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00007F38 File Offset: 0x00006138
		public override XmlNodeType NodeType
		{
			get
			{
				return this.xmlReader.NodeType;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00007F45 File Offset: 0x00006145
		public override string Name
		{
			get
			{
				return this.xmlReader.Name;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00007F52 File Offset: 0x00006152
		public override string LocalName
		{
			get
			{
				return this.xmlReader.LocalName;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00007F5F File Offset: 0x0000615F
		public override string NamespaceURI
		{
			get
			{
				return this.xmlReader.NamespaceURI;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00007F6C File Offset: 0x0000616C
		public override string Prefix
		{
			get
			{
				return this.xmlReader.Prefix;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00007F79 File Offset: 0x00006179
		public override bool HasValue
		{
			get
			{
				return this.xmlReader.HasValue;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00007F88 File Offset: 0x00006188
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

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00007FD7 File Offset: 0x000061D7
		public override int Depth
		{
			get
			{
				return this.xmlReader.Depth;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00007FE4 File Offset: 0x000061E4
		public override string BaseURI
		{
			get
			{
				return this.xmlReader.BaseURI;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00007FF1 File Offset: 0x000061F1
		public override bool IsEmptyElement
		{
			get
			{
				return this.xmlReader.IsEmptyElement;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00007FFE File Offset: 0x000061FE
		public override bool IsDefault
		{
			get
			{
				return this.xmlReader.IsDefault;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000800B File Offset: 0x0000620B
		public override char QuoteChar
		{
			get
			{
				return this.xmlReader.QuoteChar;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00008018 File Offset: 0x00006218
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.xmlReader.XmlSpace;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00008025 File Offset: 0x00006225
		public override string XmlLang
		{
			get
			{
				return this.xmlReader.XmlLang;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00008032 File Offset: 0x00006232
		public override int AttributeCount
		{
			get
			{
				return this.xmlReader.AttributeCount;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000803F File Offset: 0x0000623F
		public override bool CanResolveEntity
		{
			get
			{
				return this.xmlReader.CanResolveEntity;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000804C File Offset: 0x0000624C
		public override bool EOF
		{
			get
			{
				return this.xmlReader.EOF;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00008059 File Offset: 0x00006259
		public override ReadState ReadState
		{
			get
			{
				return this.xmlReader.ReadState;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00008066 File Offset: 0x00006266
		public override bool HasAttributes
		{
			get
			{
				return this.xmlReader.HasAttributes;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00008073 File Offset: 0x00006273
		public override XmlNameTable NameTable
		{
			get
			{
				return this.xmlReader.NameTable;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00008080 File Offset: 0x00006280
		public override bool CanReadBinaryContent
		{
			get
			{
				return this.xmlReader.CanReadBinaryContent;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000808D File Offset: 0x0000628D
		public override bool CanReadValueChunk
		{
			get
			{
				return this.xmlReader.CanReadValueChunk;
			}
		}

		// Token: 0x17000080 RID: 128
		public override string this[int i]
		{
			get
			{
				return this.xmlReader[i];
			}
		}

		// Token: 0x17000081 RID: 129
		public override string this[string name]
		{
			get
			{
				return this.xmlReader[name];
			}
		}

		// Token: 0x17000082 RID: 130
		public override string this[string name, string namespaceURI]
		{
			get
			{
				return this.xmlReader[name, namespaceURI];
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000080C5 File Offset: 0x000062C5
		public override string GetAttribute(string name)
		{
			return this.xmlReader.GetAttribute(name);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000080D3 File Offset: 0x000062D3
		public override string GetAttribute(string name, string namespaceURI)
		{
			return this.xmlReader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000080E2 File Offset: 0x000062E2
		public override string GetAttribute(int i)
		{
			return this.xmlReader.GetAttribute(i);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000080F0 File Offset: 0x000062F0
		public override bool MoveToAttribute(string name)
		{
			return this.xmlReader.MoveToAttribute(name);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000080FE File Offset: 0x000062FE
		public override bool MoveToAttribute(string name, string ns)
		{
			return this.xmlReader.MoveToAttribute(name, ns);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000810D File Offset: 0x0000630D
		public override void MoveToAttribute(int i)
		{
			this.xmlReader.MoveToAttribute(i);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000811B File Offset: 0x0000631B
		public override bool MoveToFirstAttribute()
		{
			return this.xmlReader.MoveToFirstAttribute();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00008128 File Offset: 0x00006328
		public override bool MoveToNextAttribute()
		{
			return this.xmlReader.MoveToNextAttribute();
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00008135 File Offset: 0x00006335
		public override bool MoveToElement()
		{
			return this.xmlReader.MoveToElement();
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00008144 File Offset: 0x00006344
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

		// Token: 0x0600014F RID: 335 RVA: 0x0000818A File Offset: 0x0000638A
		public override void Close()
		{
			this.xmlReader.Close();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00008197 File Offset: 0x00006397
		public override void Skip()
		{
			if (this.IsStartElement("schema", "http://www.w3.org/2001/XMLSchema"))
			{
				this.LoadSchema();
				return;
			}
			this.xmlReader.Skip();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000081BD File Offset: 0x000063BD
		public override string ReadString()
		{
			if (this.xmlReader.ValueType == typeof(DateTime))
			{
				return BinaryXmlReader.ConvertDateTimeToString(this.ReadContentAsDateTime());
			}
			return this.xmlReader.ReadString();
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000081F2 File Offset: 0x000063F2
		public override XmlNodeType MoveToContent()
		{
			return this.xmlReader.MoveToContent();
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000081FF File Offset: 0x000063FF
		public override void ReadStartElement()
		{
			this.ReadStartElementImpl(null, null);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00008209 File Offset: 0x00006409
		public override void ReadStartElement(string name)
		{
			this.ReadStartElementImpl(name, null);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00008213 File Offset: 0x00006413
		public override void ReadStartElement(string localname, string ns)
		{
			this.ReadStartElementImpl(localname, ns);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000821D File Offset: 0x0000641D
		public override string ReadElementString()
		{
			return this.ReadElementStringImpl(null, null);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00008227 File Offset: 0x00006427
		public override string ReadElementString(string name)
		{
			return this.ReadElementStringImpl(name, null);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00008231 File Offset: 0x00006431
		public override string ReadElementString(string localname, string ns)
		{
			return this.ReadElementStringImpl(localname, ns);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000823B File Offset: 0x0000643B
		public override void ReadEndElement()
		{
			this.xmlReader.ReadEndElement();
			this.PopRowsetRoot();
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000824E File Offset: 0x0000644E
		public override bool IsStartElement()
		{
			return this.xmlReader.IsStartElement();
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000825B File Offset: 0x0000645B
		public override bool IsStartElement(string name)
		{
			return this.xmlReader.IsStartElement(this.GetColumnNameFromCaption(name));
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000826F File Offset: 0x0000646F
		public override bool IsStartElement(string localname, string ns)
		{
			return this.xmlReader.IsStartElement(this.GetColumnNameFromCaption(localname), ns);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00008284 File Offset: 0x00006484
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

		// Token: 0x0600015E RID: 350 RVA: 0x00008320 File Offset: 0x00006520
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

		// Token: 0x0600015F RID: 351 RVA: 0x000083C8 File Offset: 0x000065C8
		public override string LookupNamespace(string prefix)
		{
			return this.xmlReader.LookupNamespace(prefix);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000083D6 File Offset: 0x000065D6
		public override void ResolveEntity()
		{
			this.xmlReader.ResolveEntity();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000083E3 File Offset: 0x000065E3
		public override bool ReadAttributeValue()
		{
			return this.xmlReader.ReadAttributeValue();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000083F0 File Offset: 0x000065F0
		public override DateTime ReadContentAsDateTime()
		{
			return this.xmlReader.ReadContentAsDateTime();
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000083FD File Offset: 0x000065FD
		public override double ReadContentAsDouble()
		{
			return this.xmlReader.ReadContentAsDouble();
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000840A File Offset: 0x0000660A
		public override int ReadContentAsInt()
		{
			return this.xmlReader.ReadContentAsInt();
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00008417 File Offset: 0x00006617
		public override long ReadContentAsLong()
		{
			return this.xmlReader.ReadContentAsLong();
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00008424 File Offset: 0x00006624
		public override object ReadContentAsObject()
		{
			return this.xmlReader.ReadContentAsObject();
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00008431 File Offset: 0x00006631
		public override object ReadContentAs(Type type, IXmlNamespaceResolver resolver)
		{
			return this.xmlReader.ReadContentAs(type, resolver);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00008440 File Offset: 0x00006640
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.xmlReader.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00008450 File Offset: 0x00006650
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.xmlReader.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00008460 File Offset: 0x00006660
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			return this.xmlReader.ReadElementContentAs(returnType, namespaceResolver);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000846F File Offset: 0x0000666F
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver, string localName, string namespaceURI)
		{
			return this.xmlReader.ReadElementContentAs(returnType, namespaceResolver, localName, namespaceURI);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00008481 File Offset: 0x00006681
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.xmlReader.ReadElementContentAsBase64(buffer, index, count);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00008491 File Offset: 0x00006691
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.xmlReader.ReadElementContentAsBinHex(buffer, index, count);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000084A1 File Offset: 0x000066A1
		public override bool ReadElementContentAsBoolean()
		{
			return this.xmlReader.ReadElementContentAsBoolean();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000084AE File Offset: 0x000066AE
		public override bool ReadElementContentAsBoolean(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadElementContentAsBoolean(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000084C3 File Offset: 0x000066C3
		public override DateTime ReadElementContentAsDateTime()
		{
			return this.xmlReader.ReadElementContentAsDateTime();
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000084D0 File Offset: 0x000066D0
		public override DateTime ReadElementContentAsDateTime(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadElementContentAsDateTime(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000084E5 File Offset: 0x000066E5
		public override double ReadElementContentAsDouble()
		{
			return this.xmlReader.ReadElementContentAsDouble();
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000084F2 File Offset: 0x000066F2
		public override double ReadElementContentAsDouble(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadElementContentAsDouble(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00008507 File Offset: 0x00006707
		public override int ReadElementContentAsInt()
		{
			return this.xmlReader.ReadElementContentAsInt();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00008514 File Offset: 0x00006714
		public override int ReadElementContentAsInt(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadElementContentAsInt(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00008529 File Offset: 0x00006729
		public override long ReadElementContentAsLong()
		{
			return this.xmlReader.ReadElementContentAsLong();
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00008536 File Offset: 0x00006736
		public override long ReadElementContentAsLong(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadElementContentAsLong(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000854B File Offset: 0x0000674B
		public override object ReadElementContentAsObject()
		{
			return this.xmlReader.ReadElementContentAsObject();
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00008558 File Offset: 0x00006758
		public override object ReadElementContentAsObject(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadElementContentAsObject(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000856D File Offset: 0x0000676D
		public override string ReadElementContentAsString()
		{
			return this.xmlReader.ReadElementContentAsString();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000857A File Offset: 0x0000677A
		public override string ReadElementContentAsString(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadElementContentAsString(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000858F File Offset: 0x0000678F
		public override bool ReadToFollowing(string name)
		{
			return this.xmlReader.ReadToFollowing(this.GetColumnNameFromCaption(name));
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000085A3 File Offset: 0x000067A3
		public override bool ReadToFollowing(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadToFollowing(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000085B8 File Offset: 0x000067B8
		public override bool ReadToNextSibling(string name)
		{
			return this.xmlReader.ReadToNextSibling(this.GetColumnNameFromCaption(name));
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000085CC File Offset: 0x000067CC
		public override bool ReadToNextSibling(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadToNextSibling(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000085E1 File Offset: 0x000067E1
		public override bool ReadToDescendant(string name)
		{
			return this.xmlReader.ReadToDescendant(this.GetColumnNameFromCaption(name));
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000085F5 File Offset: 0x000067F5
		public override bool ReadToDescendant(string localName, string ns)
		{
			return this.xmlReader.ReadToDescendant(this.GetColumnNameFromCaption(localName), ns);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000860A File Offset: 0x0000680A
		public override XmlReader ReadSubtree()
		{
			return this.xmlReader.ReadSubtree();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00008617 File Offset: 0x00006817
		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			return this.xmlReader.ReadValueChunk(buffer, index, count);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00008627 File Offset: 0x00006827
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return ((IXmlNamespaceResolver)this.xmlReader).GetNamespacesInScope(scope);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000863A File Offset: 0x0000683A
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return ((IXmlNamespaceResolver)this.xmlReader).LookupNamespace(prefix);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000864D File Offset: 0x0000684D
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return ((IXmlNamespaceResolver)this.xmlReader).LookupPrefix(namespaceName);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00008660 File Offset: 0x00006860
		private static string ConvertDateTimeToString(DateTime dateTime)
		{
			string text = ((dateTime.Millisecond != 0) ? "yyyy-MM-dd\\THH:mm:ss.ffffff" : "yyyy-MM-dd\\THH:mm:ss");
			return XmlConvert.ToString(dateTime, text);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000868A File Offset: 0x0000688A
		private static string GetColumnNameLookupKey(string parent, string name)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", parent, '>', name);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000086A4 File Offset: 0x000068A4
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

		// Token: 0x0600018A RID: 394 RVA: 0x00008700 File Offset: 0x00006900
		private string ReadElementStringImpl(string localname, string ns)
		{
			string text = string.Empty;
			this.MoveToContent();
			if (!string.IsNullOrEmpty(localname))
			{
				string columnNameFromCaption = this.GetColumnNameFromCaption(localname);
				if (this.LocalName != columnNameFromCaption || (!string.IsNullOrEmpty(ns) && this.NamespaceURI != ns))
				{
					throw new AdomdUnknownResponseException(XmlaSR.UnexpectedElement(columnNameFromCaption, ns ?? string.Empty), "");
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

		// Token: 0x0600018B RID: 395 RVA: 0x00008790 File Offset: 0x00006990
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

		// Token: 0x0600018C RID: 396 RVA: 0x00008908 File Offset: 0x00006B08
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

		// Token: 0x0600018D RID: 397 RVA: 0x00008954 File Offset: 0x00006B54
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

		// Token: 0x0600018E RID: 398 RVA: 0x00008A04 File Offset: 0x00006C04
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

		// Token: 0x0600018F RID: 399 RVA: 0x00008ACC File Offset: 0x00006CCC
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

		// Token: 0x06000190 RID: 400 RVA: 0x00008B04 File Offset: 0x00006D04
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

		// Token: 0x06000191 RID: 401 RVA: 0x00008B54 File Offset: 0x00006D54
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

		// Token: 0x0400018B RID: 395
		private const char HashKeyDelimeter = '>';

		// Token: 0x0400018C RID: 396
		private const string HashKeyTemplate = "{0}{1}{2}";

		// Token: 0x0400018D RID: 397
		private readonly XmlReader xmlReader;

		// Token: 0x0400018E RID: 398
		private readonly Dictionary<string, string> columnNameLookupTable;

		// Token: 0x0400018F RID: 399
		private bool canSkipNameLookup;

		// Token: 0x04000190 RID: 400
		private string currentRowsetRoot;
	}
}
