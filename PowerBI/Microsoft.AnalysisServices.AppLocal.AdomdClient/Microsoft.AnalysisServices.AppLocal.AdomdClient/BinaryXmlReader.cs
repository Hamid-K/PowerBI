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
		// Token: 0x06000135 RID: 309 RVA: 0x000081E5 File Offset: 0x000063E5
		public BinaryXmlReader(XmlReader xmlReader)
		{
			this.xmlReader = xmlReader;
			this.currentRowsetRoot = string.Empty;
			this.columnNameLookupTable = new Dictionary<string, string>();
			this.canSkipNameLookup = true;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00008211 File Offset: 0x00006411
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.xmlReader.Settings;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000821E File Offset: 0x0000641E
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.xmlReader.SchemaInfo;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000138 RID: 312 RVA: 0x0000822B File Offset: 0x0000642B
		public override Type ValueType
		{
			get
			{
				return this.xmlReader.ValueType;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00008238 File Offset: 0x00006438
		public override XmlNodeType NodeType
		{
			get
			{
				return this.xmlReader.NodeType;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00008245 File Offset: 0x00006445
		public override string Name
		{
			get
			{
				return this.xmlReader.Name;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00008252 File Offset: 0x00006452
		public override string LocalName
		{
			get
			{
				return this.xmlReader.LocalName;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000825F File Offset: 0x0000645F
		public override string NamespaceURI
		{
			get
			{
				return this.xmlReader.NamespaceURI;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000826C File Offset: 0x0000646C
		public override string Prefix
		{
			get
			{
				return this.xmlReader.Prefix;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00008279 File Offset: 0x00006479
		public override bool HasValue
		{
			get
			{
				return this.xmlReader.HasValue;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00008288 File Offset: 0x00006488
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

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000082D7 File Offset: 0x000064D7
		public override int Depth
		{
			get
			{
				return this.xmlReader.Depth;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000082E4 File Offset: 0x000064E4
		public override string BaseURI
		{
			get
			{
				return this.xmlReader.BaseURI;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000082F1 File Offset: 0x000064F1
		public override bool IsEmptyElement
		{
			get
			{
				return this.xmlReader.IsEmptyElement;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000082FE File Offset: 0x000064FE
		public override bool IsDefault
		{
			get
			{
				return this.xmlReader.IsDefault;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000830B File Offset: 0x0000650B
		public override char QuoteChar
		{
			get
			{
				return this.xmlReader.QuoteChar;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00008318 File Offset: 0x00006518
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.xmlReader.XmlSpace;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00008325 File Offset: 0x00006525
		public override string XmlLang
		{
			get
			{
				return this.xmlReader.XmlLang;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00008332 File Offset: 0x00006532
		public override int AttributeCount
		{
			get
			{
				return this.xmlReader.AttributeCount;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000833F File Offset: 0x0000653F
		public override bool CanResolveEntity
		{
			get
			{
				return this.xmlReader.CanResolveEntity;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000834C File Offset: 0x0000654C
		public override bool EOF
		{
			get
			{
				return this.xmlReader.EOF;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00008359 File Offset: 0x00006559
		public override ReadState ReadState
		{
			get
			{
				return this.xmlReader.ReadState;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00008366 File Offset: 0x00006566
		public override bool HasAttributes
		{
			get
			{
				return this.xmlReader.HasAttributes;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00008373 File Offset: 0x00006573
		public override XmlNameTable NameTable
		{
			get
			{
				return this.xmlReader.NameTable;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00008380 File Offset: 0x00006580
		public override bool CanReadBinaryContent
		{
			get
			{
				return this.xmlReader.CanReadBinaryContent;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600014E RID: 334 RVA: 0x0000838D File Offset: 0x0000658D
		public override bool CanReadValueChunk
		{
			get
			{
				return this.xmlReader.CanReadValueChunk;
			}
		}

		// Token: 0x17000086 RID: 134
		public override string this[int i]
		{
			get
			{
				return this.xmlReader[i];
			}
		}

		// Token: 0x17000087 RID: 135
		public override string this[string name]
		{
			get
			{
				return this.xmlReader[name];
			}
		}

		// Token: 0x17000088 RID: 136
		public override string this[string name, string namespaceURI]
		{
			get
			{
				return this.xmlReader[name, namespaceURI];
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000083C5 File Offset: 0x000065C5
		public override string GetAttribute(string name)
		{
			return this.xmlReader.GetAttribute(name);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000083D3 File Offset: 0x000065D3
		public override string GetAttribute(string name, string namespaceURI)
		{
			return this.xmlReader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000083E2 File Offset: 0x000065E2
		public override string GetAttribute(int i)
		{
			return this.xmlReader.GetAttribute(i);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000083F0 File Offset: 0x000065F0
		public override bool MoveToAttribute(string name)
		{
			return this.xmlReader.MoveToAttribute(name);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000083FE File Offset: 0x000065FE
		public override bool MoveToAttribute(string name, string ns)
		{
			return this.xmlReader.MoveToAttribute(name, ns);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000840D File Offset: 0x0000660D
		public override void MoveToAttribute(int i)
		{
			this.xmlReader.MoveToAttribute(i);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000841B File Offset: 0x0000661B
		public override bool MoveToFirstAttribute()
		{
			return this.xmlReader.MoveToFirstAttribute();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00008428 File Offset: 0x00006628
		public override bool MoveToNextAttribute()
		{
			return this.xmlReader.MoveToNextAttribute();
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00008435 File Offset: 0x00006635
		public override bool MoveToElement()
		{
			return this.xmlReader.MoveToElement();
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00008444 File Offset: 0x00006644
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

		// Token: 0x0600015C RID: 348 RVA: 0x0000848A File Offset: 0x0000668A
		public override void Close()
		{
			this.xmlReader.Close();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00008497 File Offset: 0x00006697
		public override void Skip()
		{
			if (this.IsStartElement("schema", "http://www.w3.org/2001/XMLSchema"))
			{
				this.LoadSchema();
				return;
			}
			this.xmlReader.Skip();
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000084BD File Offset: 0x000066BD
		public override string ReadString()
		{
			if (this.xmlReader.ValueType == typeof(DateTime))
			{
				return BinaryXmlReader.ConvertDateTimeToString(this.ReadContentAsDateTime());
			}
			return this.xmlReader.ReadString();
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000084F2 File Offset: 0x000066F2
		public override XmlNodeType MoveToContent()
		{
			return this.xmlReader.MoveToContent();
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000084FF File Offset: 0x000066FF
		public override void ReadStartElement()
		{
			this.ReadStartElementImpl(null, null);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00008509 File Offset: 0x00006709
		public override void ReadStartElement(string name)
		{
			this.ReadStartElementImpl(name, null);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00008513 File Offset: 0x00006713
		public override void ReadStartElement(string localname, string ns)
		{
			this.ReadStartElementImpl(localname, ns);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000851D File Offset: 0x0000671D
		public override string ReadElementString()
		{
			return this.ReadElementStringImpl(null, null);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00008527 File Offset: 0x00006727
		public override string ReadElementString(string name)
		{
			return this.ReadElementStringImpl(name, null);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00008531 File Offset: 0x00006731
		public override string ReadElementString(string localname, string ns)
		{
			return this.ReadElementStringImpl(localname, ns);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000853B File Offset: 0x0000673B
		public override void ReadEndElement()
		{
			this.xmlReader.ReadEndElement();
			this.PopRowsetRoot();
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000854E File Offset: 0x0000674E
		public override bool IsStartElement()
		{
			return this.xmlReader.IsStartElement();
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000855B File Offset: 0x0000675B
		public override bool IsStartElement(string name)
		{
			return this.xmlReader.IsStartElement(this.GetColumnNameFromCaption(name));
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000856F File Offset: 0x0000676F
		public override bool IsStartElement(string localname, string ns)
		{
			return this.xmlReader.IsStartElement(this.GetColumnNameFromCaption(localname), ns);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00008584 File Offset: 0x00006784
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

		// Token: 0x0600016B RID: 363 RVA: 0x00008620 File Offset: 0x00006820
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

		// Token: 0x0600016C RID: 364 RVA: 0x000086C8 File Offset: 0x000068C8
		public override string LookupNamespace(string prefix)
		{
			return this.xmlReader.LookupNamespace(prefix);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000086D6 File Offset: 0x000068D6
		public override void ResolveEntity()
		{
			this.xmlReader.ResolveEntity();
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000086E3 File Offset: 0x000068E3
		public override bool ReadAttributeValue()
		{
			return this.xmlReader.ReadAttributeValue();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000086F0 File Offset: 0x000068F0
		public override DateTime ReadContentAsDateTime()
		{
			return this.xmlReader.ReadContentAsDateTime();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000086FD File Offset: 0x000068FD
		public override double ReadContentAsDouble()
		{
			return this.xmlReader.ReadContentAsDouble();
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000870A File Offset: 0x0000690A
		public override int ReadContentAsInt()
		{
			return this.xmlReader.ReadContentAsInt();
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00008717 File Offset: 0x00006917
		public override long ReadContentAsLong()
		{
			return this.xmlReader.ReadContentAsLong();
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00008724 File Offset: 0x00006924
		public override object ReadContentAsObject()
		{
			return this.xmlReader.ReadContentAsObject();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00008731 File Offset: 0x00006931
		public override object ReadContentAs(Type type, IXmlNamespaceResolver resolver)
		{
			return this.xmlReader.ReadContentAs(type, resolver);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00008740 File Offset: 0x00006940
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.xmlReader.ReadContentAsBase64(buffer, index, count);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00008750 File Offset: 0x00006950
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.xmlReader.ReadContentAsBinHex(buffer, index, count);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00008760 File Offset: 0x00006960
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			return this.xmlReader.ReadElementContentAs(returnType, namespaceResolver);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000876F File Offset: 0x0000696F
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver, string localName, string namespaceURI)
		{
			return this.xmlReader.ReadElementContentAs(returnType, namespaceResolver, localName, namespaceURI);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00008781 File Offset: 0x00006981
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.xmlReader.ReadElementContentAsBase64(buffer, index, count);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00008791 File Offset: 0x00006991
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.xmlReader.ReadElementContentAsBinHex(buffer, index, count);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000087A1 File Offset: 0x000069A1
		public override bool ReadElementContentAsBoolean()
		{
			return this.xmlReader.ReadElementContentAsBoolean();
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000087AE File Offset: 0x000069AE
		public override bool ReadElementContentAsBoolean(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadElementContentAsBoolean(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000087C3 File Offset: 0x000069C3
		public override DateTime ReadElementContentAsDateTime()
		{
			return this.xmlReader.ReadElementContentAsDateTime();
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000087D0 File Offset: 0x000069D0
		public override DateTime ReadElementContentAsDateTime(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadElementContentAsDateTime(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000087E5 File Offset: 0x000069E5
		public override double ReadElementContentAsDouble()
		{
			return this.xmlReader.ReadElementContentAsDouble();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000087F2 File Offset: 0x000069F2
		public override double ReadElementContentAsDouble(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadElementContentAsDouble(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00008807 File Offset: 0x00006A07
		public override int ReadElementContentAsInt()
		{
			return this.xmlReader.ReadElementContentAsInt();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00008814 File Offset: 0x00006A14
		public override int ReadElementContentAsInt(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadElementContentAsInt(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00008829 File Offset: 0x00006A29
		public override long ReadElementContentAsLong()
		{
			return this.xmlReader.ReadElementContentAsLong();
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00008836 File Offset: 0x00006A36
		public override long ReadElementContentAsLong(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadElementContentAsLong(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000884B File Offset: 0x00006A4B
		public override object ReadElementContentAsObject()
		{
			return this.xmlReader.ReadElementContentAsObject();
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00008858 File Offset: 0x00006A58
		public override object ReadElementContentAsObject(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadElementContentAsObject(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000886D File Offset: 0x00006A6D
		public override string ReadElementContentAsString()
		{
			return this.xmlReader.ReadElementContentAsString();
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000887A File Offset: 0x00006A7A
		public override string ReadElementContentAsString(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadElementContentAsString(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000888F File Offset: 0x00006A8F
		public override bool ReadToFollowing(string name)
		{
			return this.xmlReader.ReadToFollowing(this.GetColumnNameFromCaption(name));
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000088A3 File Offset: 0x00006AA3
		public override bool ReadToFollowing(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadToFollowing(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000088B8 File Offset: 0x00006AB8
		public override bool ReadToNextSibling(string name)
		{
			return this.xmlReader.ReadToNextSibling(this.GetColumnNameFromCaption(name));
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000088CC File Offset: 0x00006ACC
		public override bool ReadToNextSibling(string localName, string namespaceURI)
		{
			return this.xmlReader.ReadToNextSibling(this.GetColumnNameFromCaption(localName), namespaceURI);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000088E1 File Offset: 0x00006AE1
		public override bool ReadToDescendant(string name)
		{
			return this.xmlReader.ReadToDescendant(this.GetColumnNameFromCaption(name));
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000088F5 File Offset: 0x00006AF5
		public override bool ReadToDescendant(string localName, string ns)
		{
			return this.xmlReader.ReadToDescendant(this.GetColumnNameFromCaption(localName), ns);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000890A File Offset: 0x00006B0A
		public override XmlReader ReadSubtree()
		{
			return this.xmlReader.ReadSubtree();
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00008917 File Offset: 0x00006B17
		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			return this.xmlReader.ReadValueChunk(buffer, index, count);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00008927 File Offset: 0x00006B27
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return ((IXmlNamespaceResolver)this.xmlReader).GetNamespacesInScope(scope);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000893A File Offset: 0x00006B3A
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return ((IXmlNamespaceResolver)this.xmlReader).LookupNamespace(prefix);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000894D File Offset: 0x00006B4D
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return ((IXmlNamespaceResolver)this.xmlReader).LookupPrefix(namespaceName);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00008960 File Offset: 0x00006B60
		private static string ConvertDateTimeToString(DateTime dateTime)
		{
			string text = ((dateTime.Millisecond != 0) ? "yyyy-MM-dd\\THH:mm:ss.ffffff" : "yyyy-MM-dd\\THH:mm:ss");
			return XmlConvert.ToString(dateTime, text);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000898A File Offset: 0x00006B8A
		private static string GetColumnNameLookupKey(string parent, string name)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", parent, '>', name);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000089A4 File Offset: 0x00006BA4
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

		// Token: 0x06000197 RID: 407 RVA: 0x00008A00 File Offset: 0x00006C00
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

		// Token: 0x06000198 RID: 408 RVA: 0x00008A90 File Offset: 0x00006C90
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

		// Token: 0x06000199 RID: 409 RVA: 0x00008C08 File Offset: 0x00006E08
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

		// Token: 0x0600019A RID: 410 RVA: 0x00008C54 File Offset: 0x00006E54
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

		// Token: 0x0600019B RID: 411 RVA: 0x00008D04 File Offset: 0x00006F04
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

		// Token: 0x0600019C RID: 412 RVA: 0x00008DCC File Offset: 0x00006FCC
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

		// Token: 0x0600019D RID: 413 RVA: 0x00008E04 File Offset: 0x00007004
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

		// Token: 0x0600019E RID: 414 RVA: 0x00008E54 File Offset: 0x00007054
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

		// Token: 0x04000198 RID: 408
		private const char HashKeyDelimeter = '>';

		// Token: 0x04000199 RID: 409
		private const string HashKeyTemplate = "{0}{1}{2}";

		// Token: 0x0400019A RID: 410
		private readonly XmlReader xmlReader;

		// Token: 0x0400019B RID: 411
		private readonly Dictionary<string, string> columnNameLookupTable;

		// Token: 0x0400019C RID: 412
		private bool canSkipNameLookup;

		// Token: 0x0400019D RID: 413
		private string currentRowsetRoot;
	}
}
