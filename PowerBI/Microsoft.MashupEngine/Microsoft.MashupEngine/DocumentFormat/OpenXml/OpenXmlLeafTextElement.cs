using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Xml;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002117 RID: 8471
	[DebuggerDisplay("{Text}")]
	internal abstract class OpenXmlLeafTextElement : OpenXmlLeafElement
	{
		// Token: 0x0600D14B RID: 53579 RVA: 0x0029A389 File Offset: 0x00298589
		protected OpenXmlLeafTextElement()
		{
		}

		// Token: 0x0600D14C RID: 53580 RVA: 0x0029A391 File Offset: 0x00298591
		protected OpenXmlLeafTextElement(string text)
		{
			this._rawInnerText = text;
		}

		// Token: 0x17003294 RID: 12948
		// (get) Token: 0x0600D14D RID: 53581 RVA: 0x0029A3A0 File Offset: 0x002985A0
		// (set) Token: 0x0600D14E RID: 53582 RVA: 0x0029A3A8 File Offset: 0x002985A8
		internal string RawInnerText
		{
			get
			{
				return this._rawInnerText;
			}
			set
			{
				this._rawInnerText = value;
			}
		}

		// Token: 0x0600D14F RID: 53583 RVA: 0x000020FA File Offset: 0x000002FA
		internal virtual OpenXmlSimpleType InnerTextToValue(string text)
		{
			return null;
		}

		// Token: 0x17003295 RID: 12949
		// (get) Token: 0x0600D150 RID: 53584 RVA: 0x00002105 File Offset: 0x00000305
		public override bool HasChildren
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17003296 RID: 12950
		// (get) Token: 0x0600D151 RID: 53585 RVA: 0x0029A3B1 File Offset: 0x002985B1
		// (set) Token: 0x0600D152 RID: 53586 RVA: 0x0029A3CD File Offset: 0x002985CD
		public override string InnerText
		{
			get
			{
				base.MakeSureParsed();
				if (this.RawInnerText != null)
				{
					return this._rawInnerText;
				}
				return string.Empty;
			}
			protected set
			{
				base.MakeSureParsed();
				this.RawInnerText = value;
				base.ShadowElement = null;
			}
		}

		// Token: 0x17003297 RID: 12951
		// (get) Token: 0x0600D153 RID: 53587 RVA: 0x0029A3E4 File Offset: 0x002985E4
		// (set) Token: 0x0600D154 RID: 53588 RVA: 0x0029A2F8 File Offset: 0x002984F8
		public override string InnerXml
		{
			get
			{
				if (base.ShadowElement != null)
				{
					return base.ShadowElement.InnerXml;
				}
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				XmlDOMTextWriter xmlDOMTextWriter = new XmlDOMTextWriter(stringWriter);
				try
				{
					this.WriteContentTo(xmlDOMTextWriter);
				}
				finally
				{
					xmlDOMTextWriter.Close();
				}
				return stringWriter.ToString();
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.ShadowElement = null;
					return;
				}
				throw new InvalidOperationException(ExceptionMessages.LeafElementInnerXmlCannotBeSet);
			}
		}

		// Token: 0x17003298 RID: 12952
		// (get) Token: 0x0600D155 RID: 53589 RVA: 0x0029A440 File Offset: 0x00298640
		// (set) Token: 0x0600D156 RID: 53590 RVA: 0x0029A448 File Offset: 0x00298648
		public virtual string Text
		{
			get
			{
				return this.InnerText;
			}
			set
			{
				this.InnerText = value;
			}
		}

		// Token: 0x0600D157 RID: 53591 RVA: 0x0029A451 File Offset: 0x00298651
		internal override void WriteContentTo(XmlWriter w)
		{
			if (base.ShadowElement != null)
			{
				base.ShadowElement.WriteContentTo(w);
				return;
			}
			w.WriteString(this.Text);
		}

		// Token: 0x0600D158 RID: 53592 RVA: 0x0029A474 File Offset: 0x00298674
		public override void RemoveAllChildren()
		{
			this.RawInnerText = null;
		}

		// Token: 0x0600D159 RID: 53593 RVA: 0x0029A480 File Offset: 0x00298680
		internal override void Populate(XmlReader xmlReader, OpenXmlLoadMode loadMode)
		{
			this.LoadAttributes(xmlReader);
			if (!xmlReader.IsEmptyElement)
			{
				xmlReader.Read();
				this.RawInnerText = string.Empty;
				int num = 0;
				int num2 = -1;
				XmlNodeType xmlNodeType = XmlNodeType.Text;
				if (xmlReader.NodeType != XmlNodeType.EndElement)
				{
					while (!xmlReader.EOF && xmlReader.NodeType != XmlNodeType.EndElement)
					{
						if (string.IsNullOrEmpty(this.RawInnerText) && (xmlReader.NodeType == XmlNodeType.Text || xmlReader.NodeType == XmlNodeType.CDATA || xmlReader.NodeType == XmlNodeType.SignificantWhitespace))
						{
							this.RawInnerText = xmlReader.Value;
							num2 = num;
							xmlNodeType = xmlReader.NodeType;
							xmlReader.Read();
						}
						else
						{
							OpenXmlElement openXmlElement = base.ElementFactory(xmlReader);
							openXmlElement.Load(xmlReader, OpenXmlLoadMode.Full);
							num++;
							if (base.ShadowElement == null)
							{
								base.ShadowElement = new OpenXmlUnknownElement(this.Prefix, this.LocalName, this.NamespaceUri);
							}
							base.ShadowElement.AppendChild<OpenXmlElement>(openXmlElement);
						}
					}
				}
				if (num != 0 && num2 > -1)
				{
					OpenXmlMiscNode openXmlMiscNode = null;
					XmlNodeType xmlNodeType2 = xmlNodeType;
					switch (xmlNodeType2)
					{
					case XmlNodeType.Text:
						openXmlMiscNode = OpenXmlMiscNode.CreateFromText(this.RawInnerText);
						break;
					case XmlNodeType.CDATA:
						openXmlMiscNode = OpenXmlMiscNode.CreateFromCdata(this.RawInnerText);
						break;
					default:
						if (xmlNodeType2 == XmlNodeType.SignificantWhitespace)
						{
							openXmlMiscNode = OpenXmlMiscNode.CreateFromSignificantWhitespace(this.RawInnerText);
						}
						break;
					}
					base.ShadowElement.InsertAt<OpenXmlMiscNode>(openXmlMiscNode, num2);
				}
			}
			xmlReader.Skip();
			base.RawOuterXml = string.Empty;
		}

		// Token: 0x0600D15A RID: 53594 RVA: 0x0029A5E0 File Offset: 0x002987E0
		internal override T CloneImp<T>(bool deep)
		{
			T t = base.CloneImp<T>(deep);
			(t as OpenXmlLeafTextElement).Text = this.Text;
			return t;
		}

		// Token: 0x04006940 RID: 26944
		private string _rawInnerText;
	}
}
