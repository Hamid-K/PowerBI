using System;
using System.Globalization;
using System.IO;
using System.Xml;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002118 RID: 8472
	internal class OpenXmlMiscNode : OpenXmlElement
	{
		// Token: 0x0600D15B RID: 53595 RVA: 0x0029A60C File Offset: 0x0029880C
		public OpenXmlMiscNode(XmlNodeType nodeType)
		{
			switch (nodeType)
			{
			case XmlNodeType.None:
			case XmlNodeType.Element:
			case XmlNodeType.Attribute:
			case XmlNodeType.EntityReference:
			case XmlNodeType.Entity:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentType:
			case XmlNodeType.DocumentFragment:
			case XmlNodeType.Notation:
			case XmlNodeType.EndElement:
			case XmlNodeType.EndEntity:
				throw new ArgumentOutOfRangeException("nodeType");
			}
			this.XmlNodeType = nodeType;
		}

		// Token: 0x0600D15C RID: 53596 RVA: 0x0029A680 File Offset: 0x00298880
		public OpenXmlMiscNode(XmlNodeType nodeType, string outerXml)
			: this(nodeType)
		{
			if (string.IsNullOrEmpty(outerXml))
			{
				throw new ArgumentNullException("outerXml");
			}
			using (StringReader stringReader = new StringReader(outerXml))
			{
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				using (XmlReader xmlReader = XmlReader.Create(stringReader, xmlReaderSettings))
				{
					xmlReader.Read();
					if (xmlReader.NodeType != nodeType)
					{
						throw new ArgumentException(ExceptionMessages.InvalidOuterXmlForMiscNode);
					}
					xmlReader.Close();
				}
			}
			base.RawOuterXml = outerXml;
		}

		// Token: 0x17003299 RID: 12953
		// (get) Token: 0x0600D15D RID: 53597 RVA: 0x0029A718 File Offset: 0x00298918
		// (set) Token: 0x0600D15E RID: 53598 RVA: 0x0029A720 File Offset: 0x00298920
		public XmlNodeType XmlNodeType
		{
			get
			{
				return this._nodeType;
			}
			internal set
			{
				this._nodeType = value;
			}
		}

		// Token: 0x1700329A RID: 12954
		// (get) Token: 0x0600D15F RID: 53599 RVA: 0x0029A729 File Offset: 0x00298929
		internal override int ElementTypeId
		{
			get
			{
				return 9001;
			}
		}

		// Token: 0x1700329B RID: 12955
		// (get) Token: 0x0600D160 RID: 53600 RVA: 0x0000EE09 File Offset: 0x0000D009
		internal override byte NamespaceId
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700329C RID: 12956
		// (get) Token: 0x0600D161 RID: 53601 RVA: 0x00002105 File Offset: 0x00000305
		public override bool HasChildren
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700329D RID: 12957
		// (get) Token: 0x0600D162 RID: 53602 RVA: 0x0029A730 File Offset: 0x00298930
		public override string LocalName
		{
			get
			{
				string text = string.Empty;
				switch (this._nodeType)
				{
				case XmlNodeType.None:
				case XmlNodeType.Element:
				case XmlNodeType.Attribute:
				case XmlNodeType.EntityReference:
				case XmlNodeType.Entity:
				case XmlNodeType.Document:
				case XmlNodeType.DocumentType:
				case XmlNodeType.DocumentFragment:
				case XmlNodeType.Notation:
				case XmlNodeType.EndElement:
				case XmlNodeType.EndEntity:
					return text;
				case XmlNodeType.Text:
					break;
				case XmlNodeType.CDATA:
					return "#cdata-section";
				case XmlNodeType.ProcessingInstruction:
				{
					using (StringReader stringReader = new StringReader(base.OuterXml))
					{
						XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
						using (XmlReader xmlReader = XmlReader.Create(stringReader, xmlReaderSettings))
						{
							xmlReader.Read();
							text = xmlReader.LocalName;
							xmlReader.Close();
						}
						return text;
					}
					break;
				}
				case XmlNodeType.Comment:
					return "#comment";
				case XmlNodeType.Whitespace:
					return "#whitespace";
				case XmlNodeType.SignificantWhitespace:
					return "#significant-whitespace";
				case XmlNodeType.XmlDeclaration:
					return "xml-declaration";
				default:
					return text;
				}
				text = "#text";
				return text;
			}
		}

		// Token: 0x1700329E RID: 12958
		// (get) Token: 0x0600D163 RID: 53603 RVA: 0x0007E4B5 File Offset: 0x0007C6B5
		public override string NamespaceUri
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700329F RID: 12959
		// (get) Token: 0x0600D164 RID: 53604 RVA: 0x0007E4B5 File Offset: 0x0007C6B5
		public override string Prefix
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170032A0 RID: 12960
		// (get) Token: 0x0600D165 RID: 53605 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override XmlQualifiedName XmlQualifiedName
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170032A1 RID: 12961
		// (get) Token: 0x0600D166 RID: 53606 RVA: 0x0007E4B5 File Offset: 0x0007C6B5
		// (set) Token: 0x0600D167 RID: 53607 RVA: 0x00290188 File Offset: 0x0028E388
		public override string InnerXml
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new InvalidOperationException(ExceptionMessages.InnerXmlCannotBeSet);
			}
		}

		// Token: 0x0600D168 RID: 53608 RVA: 0x0029A830 File Offset: 0x00298A30
		public override OpenXmlElement CloneNode(bool deep)
		{
			return new OpenXmlMiscNode(this.XmlNodeType)
			{
				OuterXml = base.OuterXml
			};
		}

		// Token: 0x0600D169 RID: 53609 RVA: 0x0000336E File Offset: 0x0000156E
		public override void RemoveAllChildren()
		{
		}

		// Token: 0x0600D16A RID: 53610 RVA: 0x00290AF1 File Offset: 0x0028ECF1
		internal override void WriteContentTo(XmlWriter w)
		{
			throw new NotImplementedException(ExceptionMessages.NonImplemented);
		}

		// Token: 0x0600D16B RID: 53611 RVA: 0x0029A856 File Offset: 0x00298A56
		public override void WriteTo(XmlWriter xmlWriter)
		{
			if (xmlWriter == null)
			{
				throw new ArgumentNullException("xmlWriter");
			}
			xmlWriter.WriteRaw(base.RawOuterXml);
		}

		// Token: 0x0600D16C RID: 53612 RVA: 0x0029A872 File Offset: 0x00298A72
		internal override void LazyLoad(XmlReader xmlReader)
		{
			this.Populate(xmlReader, OpenXmlLoadMode.Full);
		}

		// Token: 0x0600D16D RID: 53613 RVA: 0x0000336E File Offset: 0x0000156E
		internal override void ParseXml()
		{
		}

		// Token: 0x170032A2 RID: 12962
		// (get) Token: 0x0600D16E RID: 53614 RVA: 0x0029A87C File Offset: 0x00298A7C
		// (set) Token: 0x0600D16F RID: 53615 RVA: 0x0029A884 File Offset: 0x00298A84
		internal string Value { get; private set; }

		// Token: 0x0600D170 RID: 53616 RVA: 0x0029A890 File Offset: 0x00298A90
		internal void LoadOuterXml(XmlReader xmlReader)
		{
			switch (xmlReader.NodeType)
			{
			case XmlNodeType.None:
			case XmlNodeType.Element:
			case XmlNodeType.Attribute:
			case XmlNodeType.Entity:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentType:
			case XmlNodeType.DocumentFragment:
			case XmlNodeType.Notation:
			case XmlNodeType.Whitespace:
			case XmlNodeType.EndElement:
			case XmlNodeType.EndEntity:
				break;
			case XmlNodeType.Text:
				this.Value = xmlReader.Value;
				base.RawOuterXml = xmlReader.Value;
				return;
			case XmlNodeType.CDATA:
				this.Value = xmlReader.Value;
				base.RawOuterXml = string.Format(CultureInfo.InvariantCulture, "<![CDATA[{0}]]>", new object[] { xmlReader.Value });
				return;
			case XmlNodeType.EntityReference:
				base.RawOuterXml = xmlReader.Name;
				break;
			case XmlNodeType.ProcessingInstruction:
				this.Value = xmlReader.Value;
				base.RawOuterXml = string.Format(CultureInfo.InvariantCulture, "<?{0} {1}?>", new object[] { xmlReader.Name, xmlReader.Value });
				return;
			case XmlNodeType.Comment:
				this.Value = xmlReader.Value;
				base.RawOuterXml = string.Format(CultureInfo.InvariantCulture, "<!--{0}-->", new object[] { xmlReader.Value });
				return;
			case XmlNodeType.SignificantWhitespace:
				this.Value = xmlReader.Value;
				base.RawOuterXml = xmlReader.Value;
				return;
			case XmlNodeType.XmlDeclaration:
				this.Value = xmlReader.Value;
				return;
			default:
				return;
			}
		}

		// Token: 0x0600D171 RID: 53617 RVA: 0x0000336E File Offset: 0x0000156E
		internal override void LoadAttributes(XmlReader xmlReader)
		{
		}

		// Token: 0x0600D172 RID: 53618 RVA: 0x0029A9E0 File Offset: 0x00298BE0
		internal override void Populate(XmlReader xmlReader, OpenXmlLoadMode loadMode)
		{
			this.LoadOuterXml(xmlReader);
			xmlReader.Read();
		}

		// Token: 0x0600D173 RID: 53619 RVA: 0x00002139 File Offset: 0x00000339
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return true;
		}

		// Token: 0x0600D174 RID: 53620 RVA: 0x0029A9F0 File Offset: 0x00298BF0
		internal static OpenXmlMiscNode CreateFromText(string text)
		{
			return new OpenXmlMiscNode(XmlNodeType.Text)
			{
				Value = text,
				RawOuterXml = text
			};
		}

		// Token: 0x0600D175 RID: 53621 RVA: 0x0029AA14 File Offset: 0x00298C14
		internal static OpenXmlMiscNode CreateFromCdata(string value)
		{
			return new OpenXmlMiscNode(XmlNodeType.CDATA)
			{
				Value = value,
				RawOuterXml = string.Format(CultureInfo.InvariantCulture, "<![CDATA[{0}]]>", new object[] { value })
			};
		}

		// Token: 0x0600D176 RID: 53622 RVA: 0x0029AA54 File Offset: 0x00298C54
		internal static OpenXmlMiscNode CreateFromSignificantWhitespace(string whitespace)
		{
			return new OpenXmlMiscNode(XmlNodeType.SignificantWhitespace)
			{
				Value = whitespace,
				RawOuterXml = whitespace
			};
		}

		// Token: 0x04006941 RID: 26945
		private const string strCDataSectionName = "#cdata-section";

		// Token: 0x04006942 RID: 26946
		private const string strCommentName = "#comment";

		// Token: 0x04006943 RID: 26947
		private const string strTextName = "#text";

		// Token: 0x04006944 RID: 26948
		private const string strNonSignificantWhitespaceName = "#whitespace";

		// Token: 0x04006945 RID: 26949
		private const string strSignificantWhitespaceName = "#significant-whitespace";

		// Token: 0x04006946 RID: 26950
		private const string strXmlDeclaration = "xml-declaration";

		// Token: 0x04006947 RID: 26951
		private XmlNodeType _nodeType;
	}
}
