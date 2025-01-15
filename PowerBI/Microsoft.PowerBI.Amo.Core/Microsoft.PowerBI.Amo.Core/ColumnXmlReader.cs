using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Xml;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000038 RID: 56
	internal sealed class ColumnXmlReader : XmlReader
	{
		// Token: 0x06000237 RID: 567 RVA: 0x0000BDF0 File Offset: 0x00009FF0
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

		// Token: 0x06000238 RID: 568 RVA: 0x0000BEA4 File Offset: 0x0000A0A4
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

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000BF41 File Offset: 0x0000A141
		public bool IsDataSet
		{
			get
			{
				return this.isDataSet;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000BF4C File Offset: 0x0000A14C
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

		// Token: 0x0600023B RID: 571 RVA: 0x0000BF80 File Offset: 0x0000A180
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

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000BFD7 File Offset: 0x0000A1D7
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

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000BFEE File Offset: 0x0000A1EE
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

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000C009 File Offset: 0x0000A209
		public override bool CanResolveEntity
		{
			get
			{
				return !this.EOF && this.srcReader.CanResolveEntity;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000C020 File Offset: 0x0000A220
		public override bool HasAttributes
		{
			get
			{
				return !this.EOF && this.srcReader.HasAttributes;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000C037 File Offset: 0x0000A237
		public override bool HasValue
		{
			get
			{
				return !this.EOF && this.srcReader.HasValue;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000C04E File Offset: 0x0000A24E
		public override bool IsDefault
		{
			get
			{
				return !this.EOF && this.srcReader.IsDefault;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000C065 File Offset: 0x0000A265
		public override bool IsEmptyElement
		{
			get
			{
				return !this.EOF && this.srcReader.IsEmptyElement;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000C07C File Offset: 0x0000A27C
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

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000C097 File Offset: 0x0000A297
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

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000C0B2 File Offset: 0x0000A2B2
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

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000C0CD File Offset: 0x0000A2CD
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

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000C0E4 File Offset: 0x0000A2E4
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

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000C0FB File Offset: 0x0000A2FB
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

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000C116 File Offset: 0x0000A316
		public override char QuoteChar
		{
			get
			{
				return this.srcReader.QuoteChar;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000C123 File Offset: 0x0000A323
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

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000C13E File Offset: 0x0000A33E
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

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000C159 File Offset: 0x0000A359
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

		// Token: 0x170000A4 RID: 164
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

		// Token: 0x170000A5 RID: 165
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

		// Token: 0x170000A6 RID: 166
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

		// Token: 0x06000250 RID: 592 RVA: 0x0000C1C5 File Offset: 0x0000A3C5
		public override string GetAttribute(int i)
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.GetAttribute(i);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000C1E1 File Offset: 0x0000A3E1
		public override string GetAttribute(string name)
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.GetAttribute(name);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000C1FD File Offset: 0x0000A3FD
		public override string GetAttribute(string name, string namespaceURI)
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000C21A File Offset: 0x0000A41A
		public override bool IsStartElement()
		{
			return !this.EOF && this.srcReader.IsStartElement();
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000C231 File Offset: 0x0000A431
		public override bool IsStartElement(string localname, string ns)
		{
			return !this.EOF && this.srcReader.IsStartElement(localname, ns);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000C24A File Offset: 0x0000A44A
		public override bool IsStartElement(string name)
		{
			return !this.EOF && this.srcReader.IsStartElement(name);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000C262 File Offset: 0x0000A462
		public override string LookupNamespace(string prefix)
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.LookupNamespace(prefix);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000C27E File Offset: 0x0000A47E
		public override void MoveToAttribute(int i)
		{
			if (!this.EOF)
			{
				this.srcReader.MoveToAttribute(i);
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000C294 File Offset: 0x0000A494
		public override bool MoveToAttribute(string name)
		{
			return !this.EOF && this.srcReader.MoveToAttribute(name);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000C2AC File Offset: 0x0000A4AC
		public override bool MoveToAttribute(string name, string ns)
		{
			return !this.EOF && this.srcReader.MoveToAttribute(name, ns);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000C2C5 File Offset: 0x0000A4C5
		public override XmlNodeType MoveToContent()
		{
			if (this.EOF)
			{
				return XmlNodeType.None;
			}
			return this.srcReader.MoveToContent();
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000C2DC File Offset: 0x0000A4DC
		public override bool MoveToElement()
		{
			return !this.EOF && this.srcReader.MoveToElement();
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000C2F3 File Offset: 0x0000A4F3
		public override bool MoveToFirstAttribute()
		{
			return !this.EOF && this.srcReader.MoveToFirstAttribute();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000C30A File Offset: 0x0000A50A
		public override bool MoveToNextAttribute()
		{
			return !this.EOF && this.srcReader.MoveToNextAttribute();
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000C321 File Offset: 0x0000A521
		public override bool Read()
		{
			return !this.EOF && this.srcReader.Read();
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000C338 File Offset: 0x0000A538
		public override bool ReadAttributeValue()
		{
			return !this.EOF && this.srcReader.ReadAttributeValue();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000C34F File Offset: 0x0000A54F
		public override string ReadElementString()
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.ReadElementString();
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000C36A File Offset: 0x0000A56A
		public override string ReadElementString(string localname, string ns)
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.ReadElementString(localname, ns);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000C387 File Offset: 0x0000A587
		public override string ReadElementString(string name)
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.ReadElementString(name);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000C3A3 File Offset: 0x0000A5A3
		public override void ReadEndElement()
		{
			if (!this.EOF)
			{
				this.srcReader.ReadEndElement();
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000C3B8 File Offset: 0x0000A5B8
		public override string ReadInnerXml()
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.ReadInnerXml();
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000C3D3 File Offset: 0x0000A5D3
		public override string ReadOuterXml()
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.ReadOuterXml();
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000C3EE File Offset: 0x0000A5EE
		public override void ReadStartElement()
		{
			if (!this.EOF)
			{
				this.srcReader.ReadStartElement();
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000C403 File Offset: 0x0000A603
		public override void ReadStartElement(string localname, string ns)
		{
			if (!this.EOF)
			{
				this.srcReader.ReadStartElement(localname, ns);
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000C41A File Offset: 0x0000A61A
		public override void ReadStartElement(string name)
		{
			if (!this.EOF)
			{
				this.srcReader.ReadStartElement(name);
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000C430 File Offset: 0x0000A630
		public override string ReadString()
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.ReadString();
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000C44B File Offset: 0x0000A64B
		public override void ResolveEntity()
		{
			if (this.EOF)
			{
				return;
			}
			this.srcReader.ResolveEntity();
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000C461 File Offset: 0x0000A661
		public override void Skip()
		{
			if (!this.EOF)
			{
				this.srcReader.Skip();
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000C476 File Offset: 0x0000A676
		public override int Depth
		{
			get
			{
				return this.srcReader.Depth - this.startDepth;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000C48A File Offset: 0x0000A68A
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

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000C4BC File Offset: 0x0000A6BC
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

		// Token: 0x0600026F RID: 623 RVA: 0x0000C4E0 File Offset: 0x0000A6E0
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

		// Token: 0x040001E7 RID: 487
		private XmlReader srcReader;

		// Token: 0x040001E8 RID: 488
		private string strXml;

		// Token: 0x040001E9 RID: 489
		private string strColumnName;

		// Token: 0x040001EA RID: 490
		private string strColumnNamespace;

		// Token: 0x040001EB RID: 491
		private bool isDelegate;

		// Token: 0x040001EC RID: 492
		private int startDepth;

		// Token: 0x040001ED RID: 493
		private bool isClosed;

		// Token: 0x040001EE RID: 494
		private bool isDataSet;

		// Token: 0x040001EF RID: 495
		private bool originalSkipElements = true;
	}
}
