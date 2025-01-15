using System;
using System.IO;
using System.Xml;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002143 RID: 8515
	internal class OpenXmlUnknownElement : OpenXmlCompositeElement
	{
		// Token: 0x0600D38D RID: 54157 RVA: 0x0029F826 File Offset: 0x0029DA26
		protected internal OpenXmlUnknownElement()
		{
			this._tagName = string.Empty;
			this._prefix = string.Empty;
			this._namespaceUri = string.Empty;
		}

		// Token: 0x0600D38E RID: 54158 RVA: 0x0029F84F File Offset: 0x0029DA4F
		public OpenXmlUnknownElement(string name)
			: this()
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			OpenXmlElement.SplitName(name, out this._prefix, out this._tagName);
		}

		// Token: 0x0600D38F RID: 54159 RVA: 0x0029F877 File Offset: 0x0029DA77
		public OpenXmlUnknownElement(string qualifiedName, string namespaceUri)
			: this()
		{
			if (qualifiedName == null)
			{
				throw new ArgumentNullException("qualifiedName");
			}
			OpenXmlElement.SplitName(qualifiedName, out this._prefix, out this._tagName);
			this._namespaceUri = namespaceUri;
		}

		// Token: 0x0600D390 RID: 54160 RVA: 0x0029F8A6 File Offset: 0x0029DAA6
		public OpenXmlUnknownElement(string prefix, string localName, string namespaceUri)
			: this()
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			if (namespaceUri == null)
			{
				namespaceUri = string.Empty;
			}
			this._prefix = prefix;
			this._namespaceUri = namespaceUri;
			this._tagName = localName;
		}

		// Token: 0x0600D391 RID: 54161 RVA: 0x0029F8E8 File Offset: 0x0029DAE8
		public static OpenXmlUnknownElement CreateOpenXmlUnknownElement(string outerXml)
		{
			if (string.IsNullOrEmpty(outerXml))
			{
				throw new ArgumentNullException("outerXml");
			}
			TextReader textReader = new StringReader(outerXml);
			OpenXmlUnknownElement openXmlUnknownElement;
			using (XmlReader xmlReader = XmlReader.Create(textReader, OpenXmlElementContext.CreateDefaultXmlReaderSettings()))
			{
				if (!xmlReader.Read() || xmlReader.NodeType != XmlNodeType.Element)
				{
					throw new ArgumentException(ExceptionMessages.InvalidOuterXml, "outerXml");
				}
				openXmlUnknownElement = new OpenXmlUnknownElement(xmlReader.Prefix, xmlReader.LocalName, xmlReader.NamespaceURI)
				{
					OuterXml = outerXml
				};
			}
			return openXmlUnknownElement;
		}

		// Token: 0x17003315 RID: 13077
		// (get) Token: 0x0600D392 RID: 54162 RVA: 0x0029F97C File Offset: 0x0029DB7C
		public override string LocalName
		{
			get
			{
				return this._tagName;
			}
		}

		// Token: 0x17003316 RID: 13078
		// (get) Token: 0x0600D393 RID: 54163 RVA: 0x0029F984 File Offset: 0x0029DB84
		public override string NamespaceUri
		{
			get
			{
				return this._namespaceUri;
			}
		}

		// Token: 0x17003317 RID: 13079
		// (get) Token: 0x0600D394 RID: 54164 RVA: 0x0029F98C File Offset: 0x0029DB8C
		public override string Prefix
		{
			get
			{
				return this._prefix;
			}
		}

		// Token: 0x17003318 RID: 13080
		// (get) Token: 0x0600D395 RID: 54165 RVA: 0x0029F994 File Offset: 0x0029DB94
		public override XmlQualifiedName XmlQualifiedName
		{
			get
			{
				return new XmlQualifiedName(this._tagName, this._namespaceUri);
			}
		}

		// Token: 0x17003319 RID: 13081
		// (get) Token: 0x0600D396 RID: 54166 RVA: 0x0000EE09 File Offset: 0x0000D009
		internal override byte NamespaceId
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700331A RID: 13082
		// (get) Token: 0x0600D397 RID: 54167 RVA: 0x0029F9A7 File Offset: 0x0029DBA7
		internal override int ElementTypeId
		{
			get
			{
				return 9002;
			}
		}

		// Token: 0x1700331B RID: 13083
		// (get) Token: 0x0600D398 RID: 54168 RVA: 0x0029F9AE File Offset: 0x0029DBAE
		public override string InnerText
		{
			get
			{
				if (this.HasChildren)
				{
					return base.InnerText;
				}
				return this._text;
			}
		}

		// Token: 0x1700331C RID: 13084
		// (get) Token: 0x0600D399 RID: 54169 RVA: 0x0029F9C5 File Offset: 0x0029DBC5
		public string Text
		{
			get
			{
				base.MakeSureParsed();
				return this._text;
			}
		}

		// Token: 0x0600D39A RID: 54170 RVA: 0x0029F9D4 File Offset: 0x0029DBD4
		public override OpenXmlElement CloneNode(bool deep)
		{
			OpenXmlUnknownElement openXmlUnknownElement = new OpenXmlUnknownElement(this._prefix, this._tagName, this._namespaceUri);
			openXmlUnknownElement._text = this.Text;
			openXmlUnknownElement.CopyAttributes(this);
			if (deep)
			{
				openXmlUnknownElement.CopyChilden(this, deep);
			}
			return openXmlUnknownElement;
		}

		// Token: 0x0600D39B RID: 54171 RVA: 0x0029FA18 File Offset: 0x0029DC18
		internal override void WriteContentTo(XmlWriter w)
		{
			if (this.HasChildren)
			{
				base.WriteContentTo(w);
				return;
			}
			if (this.Text != null)
			{
				w.WriteString(this.Text);
			}
		}

		// Token: 0x0600D39C RID: 54172 RVA: 0x0029FA40 File Offset: 0x0029DC40
		public override void WriteTo(XmlWriter xmlWriter)
		{
			if (xmlWriter == null)
			{
				throw new ArgumentNullException("xmlWriter");
			}
			if (base.XmlParsed)
			{
				xmlWriter.WriteStartElement(this.Prefix, this.LocalName, this.NamespaceUri);
				this.WriteAttributesTo(xmlWriter);
				this.WriteContentTo(xmlWriter);
				xmlWriter.WriteEndElement();
				return;
			}
			xmlWriter.WriteRaw(base.RawOuterXml);
		}

		// Token: 0x0600D39D RID: 54173 RVA: 0x0029FA9C File Offset: 0x0029DC9C
		internal override void LazyLoad(XmlReader xmlReader)
		{
			this._tagName = xmlReader.LocalName;
			this._prefix = xmlReader.Prefix;
			this._namespaceUri = xmlReader.NamespaceURI;
			base.RawOuterXml = xmlReader.ReadOuterXml();
		}

		// Token: 0x0600D39E RID: 54174 RVA: 0x0029FAD0 File Offset: 0x0029DCD0
		internal override void Populate(XmlReader xmlReader, OpenXmlLoadMode loadMode)
		{
			if (string.IsNullOrEmpty(this._tagName))
			{
				this._tagName = xmlReader.LocalName;
				this._prefix = xmlReader.Prefix;
				this._namespaceUri = xmlReader.NamespaceURI;
			}
			base.Populate(xmlReader, loadMode);
			if (this.FirstChild != null && this.FirstChild.NextSibling() == null)
			{
				OpenXmlMiscNode openXmlMiscNode = this.FirstChild as OpenXmlMiscNode;
				if (openXmlMiscNode != null)
				{
					XmlNodeType xmlNodeType = openXmlMiscNode.XmlNodeType;
					switch (xmlNodeType)
					{
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
						break;
					default:
						if (xmlNodeType != XmlNodeType.SignificantWhitespace)
						{
							return;
						}
						break;
					}
					this._text = openXmlMiscNode.OuterXml;
					this.RemoveChild<OpenXmlMiscNode>(openXmlMiscNode);
				}
			}
		}

		// Token: 0x0600D39F RID: 54175 RVA: 0x00002105 File Offset: 0x00000305
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return false;
		}

		// Token: 0x0400699B RID: 27035
		private string _namespaceUri;

		// Token: 0x0400699C RID: 27036
		private string _tagName;

		// Token: 0x0400699D RID: 27037
		private string _prefix;

		// Token: 0x0400699E RID: 27038
		private string _text;
	}
}
