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
		// Token: 0x0600019E RID: 414 RVA: 0x00008BF4 File Offset: 0x00006DF4
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

		// Token: 0x0600019F RID: 415 RVA: 0x00008CA8 File Offset: 0x00006EA8
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

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00008D45 File Offset: 0x00006F45
		public bool IsDataSet
		{
			get
			{
				return this.isDataSet;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00008D50 File Offset: 0x00006F50
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

		// Token: 0x060001A2 RID: 418 RVA: 0x00008D84 File Offset: 0x00006F84
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

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00008DDB File Offset: 0x00006FDB
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

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00008DF2 File Offset: 0x00006FF2
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

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00008E0D File Offset: 0x0000700D
		public override bool CanResolveEntity
		{
			get
			{
				return !this.EOF && this.srcReader.CanResolveEntity;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00008E24 File Offset: 0x00007024
		public override bool HasAttributes
		{
			get
			{
				return !this.EOF && this.srcReader.HasAttributes;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00008E3B File Offset: 0x0000703B
		public override bool HasValue
		{
			get
			{
				return !this.EOF && this.srcReader.HasValue;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00008E52 File Offset: 0x00007052
		public override bool IsDefault
		{
			get
			{
				return !this.EOF && this.srcReader.IsDefault;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00008E69 File Offset: 0x00007069
		public override bool IsEmptyElement
		{
			get
			{
				return !this.EOF && this.srcReader.IsEmptyElement;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00008E80 File Offset: 0x00007080
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

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00008E9B File Offset: 0x0000709B
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

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00008EB6 File Offset: 0x000070B6
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

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00008ED1 File Offset: 0x000070D1
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

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00008EE8 File Offset: 0x000070E8
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

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00008EFF File Offset: 0x000070FF
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

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00008F1A File Offset: 0x0000711A
		public override char QuoteChar
		{
			get
			{
				return this.srcReader.QuoteChar;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00008F27 File Offset: 0x00007127
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

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00008F42 File Offset: 0x00007142
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

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00008F5D File Offset: 0x0000715D
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

		// Token: 0x1700009A RID: 154
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

		// Token: 0x1700009B RID: 155
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

		// Token: 0x1700009C RID: 156
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

		// Token: 0x060001B7 RID: 439 RVA: 0x00008FC9 File Offset: 0x000071C9
		public override string GetAttribute(int i)
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.GetAttribute(i);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00008FE5 File Offset: 0x000071E5
		public override string GetAttribute(string name)
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.GetAttribute(name);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00009001 File Offset: 0x00007201
		public override string GetAttribute(string name, string namespaceURI)
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000901E File Offset: 0x0000721E
		public override bool IsStartElement()
		{
			return !this.EOF && this.srcReader.IsStartElement();
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00009035 File Offset: 0x00007235
		public override bool IsStartElement(string localname, string ns)
		{
			return !this.EOF && this.srcReader.IsStartElement(localname, ns);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000904E File Offset: 0x0000724E
		public override bool IsStartElement(string name)
		{
			return !this.EOF && this.srcReader.IsStartElement(name);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00009066 File Offset: 0x00007266
		public override string LookupNamespace(string prefix)
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.LookupNamespace(prefix);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00009082 File Offset: 0x00007282
		public override void MoveToAttribute(int i)
		{
			if (!this.EOF)
			{
				this.srcReader.MoveToAttribute(i);
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00009098 File Offset: 0x00007298
		public override bool MoveToAttribute(string name)
		{
			return !this.EOF && this.srcReader.MoveToAttribute(name);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000090B0 File Offset: 0x000072B0
		public override bool MoveToAttribute(string name, string ns)
		{
			return !this.EOF && this.srcReader.MoveToAttribute(name, ns);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000090C9 File Offset: 0x000072C9
		public override XmlNodeType MoveToContent()
		{
			if (this.EOF)
			{
				return XmlNodeType.None;
			}
			return this.srcReader.MoveToContent();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000090E0 File Offset: 0x000072E0
		public override bool MoveToElement()
		{
			return !this.EOF && this.srcReader.MoveToElement();
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000090F7 File Offset: 0x000072F7
		public override bool MoveToFirstAttribute()
		{
			return !this.EOF && this.srcReader.MoveToFirstAttribute();
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000910E File Offset: 0x0000730E
		public override bool MoveToNextAttribute()
		{
			return !this.EOF && this.srcReader.MoveToNextAttribute();
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00009125 File Offset: 0x00007325
		public override bool Read()
		{
			return !this.EOF && this.srcReader.Read();
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000913C File Offset: 0x0000733C
		public override bool ReadAttributeValue()
		{
			return !this.EOF && this.srcReader.ReadAttributeValue();
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00009153 File Offset: 0x00007353
		public override string ReadElementString()
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.ReadElementString();
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000916E File Offset: 0x0000736E
		public override string ReadElementString(string localname, string ns)
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.ReadElementString(localname, ns);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000918B File Offset: 0x0000738B
		public override string ReadElementString(string name)
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.ReadElementString(name);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000091A7 File Offset: 0x000073A7
		public override void ReadEndElement()
		{
			if (!this.EOF)
			{
				this.srcReader.ReadEndElement();
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000091BC File Offset: 0x000073BC
		public override string ReadInnerXml()
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.ReadInnerXml();
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000091D7 File Offset: 0x000073D7
		public override string ReadOuterXml()
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.ReadOuterXml();
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000091F2 File Offset: 0x000073F2
		public override void ReadStartElement()
		{
			if (!this.EOF)
			{
				this.srcReader.ReadStartElement();
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00009207 File Offset: 0x00007407
		public override void ReadStartElement(string localname, string ns)
		{
			if (!this.EOF)
			{
				this.srcReader.ReadStartElement(localname, ns);
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000921E File Offset: 0x0000741E
		public override void ReadStartElement(string name)
		{
			if (!this.EOF)
			{
				this.srcReader.ReadStartElement(name);
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00009234 File Offset: 0x00007434
		public override string ReadString()
		{
			if (this.EOF)
			{
				return "";
			}
			return this.srcReader.ReadString();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000924F File Offset: 0x0000744F
		public override void ResolveEntity()
		{
			if (this.EOF)
			{
				return;
			}
			this.srcReader.ResolveEntity();
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00009265 File Offset: 0x00007465
		public override void Skip()
		{
			if (!this.EOF)
			{
				this.srcReader.Skip();
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000927A File Offset: 0x0000747A
		public override int Depth
		{
			get
			{
				return this.srcReader.Depth - this.startDepth;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000928E File Offset: 0x0000748E
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

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000092C0 File Offset: 0x000074C0
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

		// Token: 0x060001D6 RID: 470 RVA: 0x000092E4 File Offset: 0x000074E4
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

		// Token: 0x04000195 RID: 405
		private XmlReader srcReader;

		// Token: 0x04000196 RID: 406
		private string strXml;

		// Token: 0x04000197 RID: 407
		private string strColumnName;

		// Token: 0x04000198 RID: 408
		private string strColumnNamespace;

		// Token: 0x04000199 RID: 409
		private bool isDelegate;

		// Token: 0x0400019A RID: 410
		private int startDepth;

		// Token: 0x0400019B RID: 411
		private bool isClosed;

		// Token: 0x0400019C RID: 412
		private bool isDataSet;

		// Token: 0x0400019D RID: 413
		private bool originalSkipElements = true;
	}
}
