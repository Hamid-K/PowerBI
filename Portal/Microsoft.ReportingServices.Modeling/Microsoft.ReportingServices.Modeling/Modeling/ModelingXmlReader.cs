using System;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000056 RID: 86
	internal class ModelingXmlReader
	{
		// Token: 0x0600036F RID: 879 RVA: 0x0000B9EC File Offset: 0x00009BEC
		public ModelingXmlReader(XmlReader xr, ModelingXmlSchema schema, DeserializationContext ctx)
		{
			if (xr == null || schema == null || ctx == null)
			{
				throw new InternalModelingException("xr, schema or ctx is null");
			}
			this.m_xr = schema.WrapXmlReader(xr);
			this.m_defaultNamespace = schema.TargetNamespace;
			this.m_ctx = ctx;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000BA28 File Offset: 0x00009C28
		public XmlReader Reader
		{
			get
			{
				return this.m_xr;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000BA30 File Offset: 0x00009C30
		public DeserializationContext Context
		{
			get
			{
				return this.m_ctx;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000BA38 File Offset: 0x00009C38
		public ValidationContext Validation
		{
			get
			{
				return this.m_ctx.Validation;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000BA48 File Offset: 0x00009C48
		public bool IsDefaultNamespace
		{
			get
			{
				if (this.m_xr.NodeType == XmlNodeType.Element)
				{
					return this.m_xr.NamespaceURI == this.m_defaultNamespace;
				}
				return this.m_xr.NodeType == XmlNodeType.Attribute && this.m_xr.NamespaceURI.Length == 0;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000BA9D File Offset: 0x00009C9D
		public string LocalName
		{
			get
			{
				return this.m_xr.LocalName;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000BAAA File Offset: 0x00009CAA
		public string NamespaceURI
		{
			get
			{
				return this.m_xr.NamespaceURI;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000BAB7 File Offset: 0x00009CB7
		public string Prefix
		{
			get
			{
				return this.m_xr.Prefix;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000BAC4 File Offset: 0x00009CC4
		public XmlNodeType NodeType
		{
			get
			{
				return this.m_xr.NodeType;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000BAD1 File Offset: 0x00009CD1
		public string Value
		{
			get
			{
				return this.m_xr.Value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000BADE File Offset: 0x00009CDE
		public IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.m_xr.SchemaInfo;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000BAEB File Offset: 0x00009CEB
		public bool IsNil
		{
			get
			{
				return this.m_xr.SchemaInfo != null && this.m_xr.SchemaInfo.IsNil;
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000BB0C File Offset: 0x00009D0C
		public bool MoveToAttribute(string name)
		{
			return this.m_xr.MoveToAttribute(name);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000BB1A File Offset: 0x00009D1A
		public bool MoveToElement()
		{
			return this.m_xr.MoveToElement();
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000BB27 File Offset: 0x00009D27
		public bool MoveToDescendant(string name)
		{
			return this.m_xr.ReadToDescendant(name, this.m_defaultNamespace);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000BB3C File Offset: 0x00009D3C
		public bool MoveToEndElement(string name)
		{
			while (!this.m_xr.EOF && (this.m_xr.NodeType != XmlNodeType.EndElement || !(this.m_xr.LocalName == name) || !(this.m_xr.NamespaceURI == this.m_defaultNamespace)))
			{
				this.m_xr.Read();
			}
			return !this.m_xr.EOF;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000BBAC File Offset: 0x00009DAC
		public void Skip()
		{
			this.m_xr.Skip();
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000BBBC File Offset: 0x00009DBC
		public object ReadTypedValue()
		{
			Type valueType = this.m_xr.ValueType;
			if (valueType == typeof(XmlQualifiedName))
			{
				return this.ReadValueAsQName();
			}
			if (valueType == typeof(string))
			{
				return this.ReadValueAsString();
			}
			if (this.m_xr.NodeType == XmlNodeType.Element)
			{
				return this.m_xr.ReadElementContentAsObject();
			}
			return this.m_xr.ReadContentAsObject();
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000BC34 File Offset: 0x00009E34
		public object ReadValueAs(Type type)
		{
			if (type == typeof(QName))
			{
				return this.ReadValueAsQName();
			}
			if (type == typeof(string))
			{
				return this.ReadValueAsString();
			}
			if (this.m_xr.NodeType == XmlNodeType.Element)
			{
				return this.m_xr.ReadElementContentAs(type, this.m_xr as IXmlNamespaceResolver);
			}
			return this.m_xr.ReadContentAs(type, this.m_xr as IXmlNamespaceResolver);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000BCB5 File Offset: 0x00009EB5
		public bool ReadValueAsBoolean()
		{
			if (this.m_xr.NodeType == XmlNodeType.Element)
			{
				return this.m_xr.ReadElementContentAsBoolean();
			}
			return this.m_xr.ReadContentAsBoolean();
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000BCDC File Offset: 0x00009EDC
		public int ReadValueAsInt()
		{
			if (this.m_xr.NodeType == XmlNodeType.Element)
			{
				return this.m_xr.ReadElementContentAsInt();
			}
			return this.m_xr.ReadContentAsInt();
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000BD04 File Offset: 0x00009F04
		public string ReadValueAsString()
		{
			string text;
			if (this.m_xr.NodeType == XmlNodeType.Element)
			{
				text = this.m_xr.ReadElementContentAsString();
			}
			else
			{
				text = this.m_xr.ReadContentAsString();
			}
			return text ?? string.Empty;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000BD43 File Offset: 0x00009F43
		public T ReadValueAsEnum<T>()
		{
			return XmlConvertEx.ToEnum<T>(this.ReadValueAsString());
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000BD50 File Offset: 0x00009F50
		public CultureInfo ReadValueAsCultureInfo()
		{
			string localName = this.m_xr.LocalName;
			string text = this.ReadValueAsString();
			CultureInfo cultureInfo;
			try
			{
				cultureInfo = XmlConvertEx.ToCultureInfo(text);
			}
			catch (ArgumentException)
			{
				this.Validation.AddScopedError(ModelingErrorCode.InvalidCulture, SRErrors.InvalidCulture(localName, this.Validation.CurrentObjectDescriptor, text));
				cultureInfo = null;
			}
			return cultureInfo;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000BDB0 File Offset: 0x00009FB0
		public QName ReadValueAsQName()
		{
			return new QName((XmlQualifiedName)this.ReadValueAs(typeof(XmlQualifiedName)));
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000BDCC File Offset: 0x00009FCC
		public QName ReadValueAsID()
		{
			return ModelItem.CanonicalizeID(this.ReadValueAsQName(), true);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000BDDA File Offset: 0x00009FDA
		public void LoadObject(string elementName, IXmlLoadable obj)
		{
			this.CheckElement(elementName);
			this.LoadObject(obj);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000BDEC File Offset: 0x00009FEC
		public void LoadObject(IXmlLoadable obj)
		{
			if (obj == null)
			{
				throw new InternalModelingException("obj is null");
			}
			this.LoadObjectAttributes(obj);
			int depth = this.m_xr.Depth;
			string localName = this.m_xr.LocalName;
			string namespaceURI = this.m_xr.NamespaceURI;
			bool isEmptyElement = this.m_xr.IsEmptyElement;
			this.m_xr.ReadStartElement(localName, namespaceURI);
			if (isEmptyElement)
			{
				return;
			}
			this.LoadObjectElements(obj, depth, localName, namespaceURI);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000BE58 File Offset: 0x0000A058
		private void LoadObjectAttributes(IXmlLoadable obj)
		{
			if (!this.m_xr.MoveToFirstAttribute())
			{
				return;
			}
			string name;
			for (;;)
			{
				name = this.m_xr.Name;
				if (!obj.LoadXmlAttribute(this) && this.m_xr.NamespaceURI != "http://www.w3.org/2000/xmlns/" && (this.m_xr.LocalName != "type" || this.m_xr.NamespaceURI != "http://www.w3.org/2001/XMLSchema-instance"))
				{
					break;
				}
				if (this.m_xr.NodeType != XmlNodeType.Attribute || this.m_xr.Name != name)
				{
					goto IL_009F;
				}
				if (!this.m_xr.MoveToNextAttribute())
				{
					goto Block_6;
				}
			}
			throw new InternalModelingException("Unhandled attribute '" + name + "' in LoadObjectAttributes");
			IL_009F:
			throw new InternalModelingException("LoadXmlAttribute left the reader at an invalid position.\n" + StringUtil.FormatInvariant("Current node: {0} '{1}', depth {2}", new object[]
			{
				this.m_xr.NodeType,
				this.m_xr.Name,
				this.m_xr.Depth
			}));
			Block_6:
			this.m_xr.MoveToElement();
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000BF74 File Offset: 0x0000A174
		private void LoadObjectElements(IXmlLoadable obj, int baseDepth, string baseLocalName, string baseNamespaceUri)
		{
			for (;;)
			{
				bool flag = false;
				this.m_xr.MoveToContent();
				if (this.m_xr.Depth == baseDepth)
				{
					if (this.m_xr.NodeType == XmlNodeType.EndElement && this.m_xr.LocalName == baseLocalName && this.m_xr.NamespaceURI == baseNamespaceUri)
					{
						break;
					}
					flag = true;
				}
				else if (this.m_xr.Depth == baseDepth + 1)
				{
					if (this.m_xr.NodeType != XmlNodeType.Element)
					{
						goto IL_011E;
					}
					string localName = this.m_xr.LocalName;
					string namespaceURI = this.m_xr.NamespaceURI;
					if (!obj.LoadXmlElement(this))
					{
						goto Block_6;
					}
					if (this.m_xr.Depth == baseDepth + 1 && this.m_xr.NodeType == XmlNodeType.EndElement && this.m_xr.LocalName == localName && this.m_xr.NamespaceURI == namespaceURI)
					{
						this.m_xr.Read();
					}
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					goto Block_11;
				}
			}
			this.m_xr.Read();
			return;
			Block_6:
			throw new InternalModelingException("Unhandled element '" + this.m_xr.Name + "' in LoadObjectElements");
			IL_011E:
			throw new InternalModelingException("Unhandled " + this.m_xr.NodeType.ToString() + " node in LoadObjectElements");
			Block_11:
			throw new InternalModelingException("LoadXmlElement left the reader at an invalid position.\n" + StringUtil.FormatInvariant("Current node: {0} '{1}', depth {2}", new object[]
			{
				this.m_xr.NodeType,
				this.m_xr.Name,
				this.m_xr.Depth
			}));
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000C128 File Offset: 0x0000A328
		public void CheckElement(string name)
		{
			if (!this.m_xr.IsStartElement(name, this.m_defaultNamespace))
			{
				throw new InternalModelingException(SRErrors.Xml_NodeMismatch(XmlNodeType.Element, name, this.m_defaultNamespace, this.m_xr.NodeType, this.m_xr.LocalName, this.m_xr.NamespaceURI));
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000C180 File Offset: 0x0000A380
		public XPathDocument ReadFragment()
		{
			bool isEmptyElement = this.m_xr.IsEmptyElement;
			XPathDocument xpathDocument = new XPathDocument(this.m_xr.ReadSubtree());
			if (isEmptyElement)
			{
				this.m_xr.Read();
			}
			return xpathDocument;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000C1B8 File Offset: 0x0000A3B8
		public ModelingReference ReadReferenceByID(string propertyName, bool multipleInScope)
		{
			return new ModelingReference(this.ReadValueAsID(), propertyName, multipleInScope);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000C1C7 File Offset: 0x0000A3C7
		public ModelingReference ReadReferenceByName(string propertyName, bool multipleInScope)
		{
			return new ModelingReference(this.ReadValueAsString(), propertyName, multipleInScope);
		}

		// Token: 0x04000213 RID: 531
		private readonly XmlReader m_xr;

		// Token: 0x04000214 RID: 532
		private readonly string m_defaultNamespace;

		// Token: 0x04000215 RID: 533
		private readonly DeserializationContext m_ctx;
	}
}
