using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Xml;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000020 RID: 32
	internal sealed class ColumnXmlReader : XmlReader
	{
		// Token: 0x060001AB RID: 427 RVA: 0x00008EF4 File Offset: 0x000070F4
		public ColumnXmlReader(XmlReader xmlReader, string columnName, string strNamespace)
		{
			this.strColumnName = columnName;
			this.strColumnNamespace = strNamespace;
			this.srcReader = xmlReader;
			this.strXml = string.Empty;
			this.isDelegate = true;
			this.isClosed = false;
			this.startDepth = this.srcReader.Depth;
			XmlaReader xmlaReader = this.srcReader as XmlaReader;
			if (xmlaReader != null)
			{
				this.originalSkipElements = xmlaReader.SkipElements;
				xmlaReader.SkipElements = false;
			}
			do
			{
				xmlReader.Read();
			}
			while (!xmlReader.IsStartElement() && xmlReader.NodeType != XmlNodeType.EndElement && !xmlReader.EOF);
			this.isDataSet = xmlReader.NamespaceURI == "urn:schemas-microsoft-com:xml-analysis:xmlDocumentDataset";
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00008FA8 File Offset: 0x000071A8
		public ColumnXmlReader(string strIn)
		{
			this.strXml = strIn;
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Fragment;
			this.srcReader = XmlReader.Create(new StringReader(this.strXml), xmlReaderSettings, new XmlParserContext(null, null, null, XmlSpace.Default));
			this.isDelegate = false;
			this.isClosed = false;
			do
			{
				this.srcReader.Read();
			}
			while (!this.srcReader.IsStartElement() && !this.srcReader.EOF);
			this.isDataSet = this.srcReader.NamespaceURI == "urn:schemas-microsoft-com:xml-analysis:xmlDocumentDataset";
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00009045 File Offset: 0x00007245
		public bool IsDataSet
		{
			get
			{
				return this.isDataSet;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00009050 File Offset: 0x00007250
		public DataSet Dataset
		{
			get
			{
				DataSet dataSet = null;
				if (this.IsDataSet)
				{
					dataSet = new DataSet();
					dataSet.Locale = CultureInfo.InvariantCulture;
					dataSet.ReadXml(this, XmlReadMode.Auto);
				}
				return dataSet;
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00009084 File Offset: 0x00007284
		public override string ToString()
		{
			if (this.strXml.Length == 0)
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				new XmlTextWriter(stringWriter).WriteNode(this, true);
				if (this.isDelegate)
				{
					this.Close();
				}
				this.strXml = stringWriter.GetStringBuilder().ToString();
			}
			return this.strXml;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x000090DB File Offset: 0x000072DB
		public override int AttributeCount
		{
			get
			{
				if (this.EOF)
				{
					return 0;
				}
				return this.srcReader.AttributeCount;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x000090F2 File Offset: 0x000072F2
		public override string BaseURI
		{
			get
			{
				if (this.EOF)
				{
					return "";
				}
				return this.srcReader.BaseURI;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000910D File Offset: 0x0000730D
		public override bool CanResolveEntity
		{
			get
			{
				return !this.EOF && this.srcReader.CanResolveEntity;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00009124 File Offset: 0x00007324
		public override bool HasAttributes
		{
			get
			{
				return !this.EOF && this.srcReader.HasAttributes;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000913B File Offset: 0x0000733B
		public override bool HasValue
		{
			get
			{
				return !this.EOF && this.srcReader.HasValue;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00009152 File Offset: 0x00007352
		public override bool IsDefault
		{
			get
			{
				return !this.EOF && this.srcReader.IsDefault;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00009169 File Offset: 0x00007369
		public override bool IsEmptyElement
		{
			get
			{
				return !this.EOF && this.srcReader.IsEmptyElement;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00009180 File Offset: 0x00007380
		public override string LocalName
		{
			get
			{
				if (this.EOF)
				{
					return "";
				}
				return this.srcReader.LocalName;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000919B File Offset: 0x0000739B
		public override string Name
		{
			get
			{
				if (this.EOF)
				{
					return "";
				}
				return this.srcReader.Name;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x000091B6 File Offset: 0x000073B6
		public override string NamespaceURI
		{
			get
			{
				if (this.EOF)
				{
					return "";
				}
				return this.srcReader.NamespaceURI;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001BA RID: 442 RVA: 0x000091D1 File Offset: 0x000073D1
		public override XmlNameTable NameTable
		{
			get
			{
				if (this.EOF)
				{
					return null;
				}
				return this.srcReader.NameTable;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000091E8 File Offset: 0x000073E8
		public override XmlNodeType NodeType
		{
			get
			{
				if (this.EOF)
				{
					return XmlNodeType.None;
				}
				return this.srcReader.NodeType;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001BC RID: 444 RVA: 0x000091FF File Offset: 0x000073FF
		public override string Prefix
		{
			get
			{
				if (this.EOF)
				{
					return "";
				}
				return this.srcReader.Prefix;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000921A File Offset: 0x0000741A
		public override char QuoteChar
		{
			get
			{
				return this.srcReader.QuoteChar;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00009227 File Offset: 0x00007427
		public override string Value
		{
			get
			{
				if (this.EOF)
				{
					return "";
				}
				return this.srcReader.Value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00009242 File Offset: 0x00007442
		public override string XmlLang
		{
			get
			{
				if (this.EOF)
				{
					return "";
				}
				return this.srcReader.XmlLang;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000925D File Offset: 0x0000745D
		public override XmlSpace XmlSpace
		{
			get
			{
				if (this.EOF)
				{
					return XmlSpace.None;
				}
				return this.srcReader.XmlSpace;
			}
		}

		// Token: 0x170000A0 RID: 160
		public override string this[int i]
		{
			get
			{
				if (this.EOF)
				{
					return "";
				}
				return this.srcReader[i];
			}
		}

		// Token: 0x170000A1 RID: 161
		public override string this[string name, string namespaceURI]
		{
			get
			{
				if (this.EOF)
				{
					return "";
				}
				return this.srcReader[name, namespaceURI];
			}
		}

		// Token: 0x170000A2 RID: 162
		public override string this[string name]
		{
			get
			{
				if (this.EOF)
				{
					return "";
				}
				return this.srcReader[name];
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000092C9 File Offset: 0x000074C9
		public override string GetAttribute(int i)
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.GetAttribute(i);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000092E5 File Offset: 0x000074E5
		public override string GetAttribute(string name)
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.GetAttribute(name);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00009301 File Offset: 0x00007501
		public override string GetAttribute(string name, string namespaceURI)
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000931E File Offset: 0x0000751E
		public override bool IsStartElement()
		{
			return !this.EOF && this.srcReader.IsStartElement();
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00009335 File Offset: 0x00007535
		public override bool IsStartElement(string localname, string ns)
		{
			return !this.EOF && this.srcReader.IsStartElement(localname, ns);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000934E File Offset: 0x0000754E
		public override bool IsStartElement(string name)
		{
			return !this.EOF && this.srcReader.IsStartElement(name);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00009366 File Offset: 0x00007566
		public override string LookupNamespace(string prefix)
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.LookupNamespace(prefix);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00009382 File Offset: 0x00007582
		public override void MoveToAttribute(int i)
		{
			if (!this.EOF)
			{
				this.srcReader.MoveToAttribute(i);
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00009398 File Offset: 0x00007598
		public override bool MoveToAttribute(string name)
		{
			return !this.EOF && this.srcReader.MoveToAttribute(name);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000093B0 File Offset: 0x000075B0
		public override bool MoveToAttribute(string name, string ns)
		{
			return !this.EOF && this.srcReader.MoveToAttribute(name, ns);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000093C9 File Offset: 0x000075C9
		public override XmlNodeType MoveToContent()
		{
			if (this.EOF)
			{
				return XmlNodeType.None;
			}
			return this.srcReader.MoveToContent();
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000093E0 File Offset: 0x000075E0
		public override bool MoveToElement()
		{
			return !this.EOF && this.srcReader.MoveToElement();
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000093F7 File Offset: 0x000075F7
		public override bool MoveToFirstAttribute()
		{
			return !this.EOF && this.srcReader.MoveToFirstAttribute();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000940E File Offset: 0x0000760E
		public override bool MoveToNextAttribute()
		{
			return !this.EOF && this.srcReader.MoveToNextAttribute();
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00009425 File Offset: 0x00007625
		public override bool Read()
		{
			return !this.EOF && this.srcReader.Read();
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000943C File Offset: 0x0000763C
		public override bool ReadAttributeValue()
		{
			return !this.EOF && this.srcReader.ReadAttributeValue();
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00009453 File Offset: 0x00007653
		public override string ReadElementString()
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.ReadElementString();
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000946E File Offset: 0x0000766E
		public override string ReadElementString(string localname, string ns)
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.ReadElementString(localname, ns);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000948B File Offset: 0x0000768B
		public override string ReadElementString(string name)
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.ReadElementString(name);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000094A7 File Offset: 0x000076A7
		public override void ReadEndElement()
		{
			if (!this.EOF)
			{
				this.srcReader.ReadEndElement();
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000094BC File Offset: 0x000076BC
		public override string ReadInnerXml()
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.ReadInnerXml();
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000094D7 File Offset: 0x000076D7
		public override string ReadOuterXml()
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.ReadOuterXml();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000094F2 File Offset: 0x000076F2
		public override void ReadStartElement()
		{
			if (!this.EOF)
			{
				this.srcReader.ReadStartElement();
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00009507 File Offset: 0x00007707
		public override void ReadStartElement(string localname, string ns)
		{
			if (!this.EOF)
			{
				this.srcReader.ReadStartElement(localname, ns);
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000951E File Offset: 0x0000771E
		public override void ReadStartElement(string name)
		{
			if (!this.EOF)
			{
				this.srcReader.ReadStartElement(name);
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00009534 File Offset: 0x00007734
		public override string ReadString()
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.ReadString();
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000954F File Offset: 0x0000774F
		public override void ResolveEntity()
		{
			if (this.EOF)
			{
				return;
			}
			this.srcReader.ResolveEntity();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00009565 File Offset: 0x00007765
		public override void Skip()
		{
			if (!this.EOF)
			{
				this.srcReader.Skip();
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000957A File Offset: 0x0000777A
		public override int Depth
		{
			get
			{
				return this.srcReader.Depth - this.startDepth;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000958E File Offset: 0x0000778E
		public override bool EOF
		{
			get
			{
				if (this.srcReader.EOF)
				{
					this.startDepth = this.srcReader.Depth;
				}
				bool flag = this.Depth < 0;
				if (flag)
				{
					this.Close();
				}
				return flag;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000095C0 File Offset: 0x000077C0
		public override ReadState ReadState
		{
			get
			{
				if (!this.EOF)
				{
					return this.srcReader.ReadState;
				}
				if (this.isClosed)
				{
					return ReadState.Closed;
				}
				return ReadState.EndOfFile;
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000095E4 File Offset: 0x000077E4
		public override void Close()
		{
			if (!this.isClosed)
			{
				this.isClosed = true;
				if (!this.isDelegate)
				{
					this.srcReader.Close();
					return;
				}
				while (this.Depth >= 0 && (this.srcReader.NodeType != XmlNodeType.EndElement || this.strColumnName != this.srcReader.Name || this.strColumnNamespace != this.srcReader.NamespaceURI))
				{
					this.srcReader.Read();
				}
				this.srcReader.ReadEndElement();
				XmlaReader xmlaReader = this.srcReader as XmlaReader;
				if (xmlaReader != null)
				{
					xmlaReader.SkipElements = this.originalSkipElements;
				}
			}
		}

		// Token: 0x040001A2 RID: 418
		private XmlReader srcReader;

		// Token: 0x040001A3 RID: 419
		private string strXml;

		// Token: 0x040001A4 RID: 420
		private string strColumnName;

		// Token: 0x040001A5 RID: 421
		private string strColumnNamespace;

		// Token: 0x040001A6 RID: 422
		private bool isDelegate;

		// Token: 0x040001A7 RID: 423
		private int startDepth;

		// Token: 0x040001A8 RID: 424
		private bool isClosed;

		// Token: 0x040001A9 RID: 425
		private bool isDataSet;

		// Token: 0x040001AA RID: 426
		private bool originalSkipElements = true;
	}
}
