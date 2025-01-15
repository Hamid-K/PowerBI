using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200004A RID: 74
	[DebuggerDisplay("Name={Name}")]
	internal abstract class SchemaElement
	{
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x000108E1 File Offset: 0x0000EAE1
		internal int LineNumber
		{
			get
			{
				return this._lineNumber;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x000108E9 File Offset: 0x0000EAE9
		internal int LinePosition
		{
			get
			{
				return this._linePosition;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x000108F1 File Offset: 0x0000EAF1
		// (set) Token: 0x060007F5 RID: 2037 RVA: 0x000108F9 File Offset: 0x0000EAF9
		public virtual string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x00010902 File Offset: 0x0000EB02
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x0001090A File Offset: 0x0000EB0A
		internal DocumentationElement Documentation
		{
			get
			{
				return this._documentation;
			}
			set
			{
				this._documentation = value;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00010913 File Offset: 0x0000EB13
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x0001091B File Offset: 0x0000EB1B
		internal SchemaElement ParentElement
		{
			get
			{
				return this._parentElement;
			}
			private set
			{
				this._parentElement = value;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x00010924 File Offset: 0x0000EB24
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x0001092C File Offset: 0x0000EB2C
		internal Schema Schema
		{
			get
			{
				return this._schema;
			}
			set
			{
				this._schema = value;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x00010935 File Offset: 0x0000EB35
		public virtual string FQName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x0001093D File Offset: 0x0000EB3D
		public virtual string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x00010945 File Offset: 0x0000EB45
		public List<MetadataProperty> OtherContent
		{
			get
			{
				if (this._otherContent == null)
				{
					this._otherContent = new List<MetadataProperty>();
				}
				return this._otherContent;
			}
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00010960 File Offset: 0x0000EB60
		internal virtual void Validate()
		{
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00010962 File Offset: 0x0000EB62
		internal void AddError(ErrorCode errorCode, EdmSchemaErrorSeverity severity, int lineNumber, int linePosition, object message)
		{
			this.AddError(errorCode, severity, this.SchemaLocation, lineNumber, linePosition, message);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00010978 File Offset: 0x0000EB78
		internal void AddError(ErrorCode errorCode, EdmSchemaErrorSeverity severity, XmlReader reader, object message)
		{
			int num;
			int num2;
			SchemaElement.GetPositionInfo(reader, out num, out num2);
			this.AddError(errorCode, severity, this.SchemaLocation, num, num2, message);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x000109A1 File Offset: 0x0000EBA1
		internal void AddError(ErrorCode errorCode, EdmSchemaErrorSeverity severity, object message)
		{
			this.AddError(errorCode, severity, this.SchemaLocation, this.LineNumber, this.LinePosition, message);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x000109BE File Offset: 0x0000EBBE
		internal void AddError(ErrorCode errorCode, EdmSchemaErrorSeverity severity, SchemaElement element, object message)
		{
			this.AddError(errorCode, severity, element.Schema.Location, element.LineNumber, element.LinePosition, message);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x000109E4 File Offset: 0x0000EBE4
		internal void Parse(XmlReader reader)
		{
			this.GetPositionInfo(reader);
			bool flag = !reader.IsEmptyElement;
			bool flag2 = reader.MoveToFirstAttribute();
			while (flag2)
			{
				this.ParseAttribute(reader);
				flag2 = reader.MoveToNextAttribute();
			}
			this.HandleAttributesComplete();
			bool flag3 = !flag;
			bool flag4 = false;
			while (!flag3)
			{
				if (flag4)
				{
					flag4 = false;
					reader.Skip();
					if (reader.EOF)
					{
						break;
					}
				}
				else if (!reader.Read())
				{
					break;
				}
				switch (reader.NodeType)
				{
				case XmlNodeType.Element:
					flag4 = this.ParseElement(reader);
					continue;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
				case XmlNodeType.SignificantWhitespace:
					this.ParseText(reader);
					continue;
				case XmlNodeType.EntityReference:
				case XmlNodeType.DocumentType:
					flag4 = true;
					continue;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
				case XmlNodeType.Notation:
				case XmlNodeType.Whitespace:
				case XmlNodeType.XmlDeclaration:
					continue;
				case XmlNodeType.EndElement:
					flag3 = true;
					continue;
				}
				this.AddError(ErrorCode.UnexpectedXmlNodeType, EdmSchemaErrorSeverity.Error, reader, Strings.UnexpectedXmlNodeType(reader.NodeType));
				flag4 = true;
			}
			this.HandleChildElementsComplete();
			if (reader.EOF && reader.Depth > 0)
			{
				this.AddError(ErrorCode.MalformedXml, EdmSchemaErrorSeverity.Error, 0, 0, Strings.MalformedXml(this.LineNumber, this.LinePosition));
			}
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00010B20 File Offset: 0x0000ED20
		internal void GetPositionInfo(XmlReader reader)
		{
			SchemaElement.GetPositionInfo(reader, out this._lineNumber, out this._linePosition);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00010B34 File Offset: 0x0000ED34
		internal static void GetPositionInfo(XmlReader reader, out int lineNumber, out int linePosition)
		{
			IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
			if (xmlLineInfo != null && xmlLineInfo.HasLineInfo())
			{
				lineNumber = xmlLineInfo.LineNumber;
				linePosition = xmlLineInfo.LinePosition;
				return;
			}
			lineNumber = 0;
			linePosition = 0;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00010B6A File Offset: 0x0000ED6A
		internal virtual void ResolveTopLevelNames()
		{
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00010B6C File Offset: 0x0000ED6C
		internal virtual void ResolveSecondLevelNames()
		{
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00010B70 File Offset: 0x0000ED70
		internal SchemaElement(SchemaElement parentElement)
		{
			if (parentElement != null)
			{
				this.ParentElement = parentElement;
				for (SchemaElement schemaElement = parentElement; schemaElement != null; schemaElement = schemaElement.ParentElement)
				{
					Schema schema = schemaElement as Schema;
					if (schema != null)
					{
						this.Schema = schema;
						break;
					}
				}
				if (this.Schema == null)
				{
					throw EntityUtil.InvalidOperation(Strings.AllElementsMustBeInSchema);
				}
			}
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00010BC1 File Offset: 0x0000EDC1
		internal SchemaElement(SchemaElement parentElement, string name)
			: this(parentElement)
		{
			this._name = name;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00010BD1 File Offset: 0x0000EDD1
		protected virtual void HandleAttributesComplete()
		{
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00010BD3 File Offset: 0x0000EDD3
		protected virtual void HandleChildElementsComplete()
		{
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00010BD8 File Offset: 0x0000EDD8
		protected string HandleUndottedNameAttribute(XmlReader reader, string field)
		{
			string text = field;
			Utils.GetUndottedName(this.Schema, reader, out text);
			return text;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00010BF8 File Offset: 0x0000EDF8
		protected ReturnValue<string> HandleDottedNameAttribute(XmlReader reader, string field, Func<object, string> errorFormat)
		{
			ReturnValue<string> returnValue = new ReturnValue<string>();
			string text;
			if (!Utils.GetDottedName(this.Schema, reader, out text))
			{
				return returnValue;
			}
			returnValue.Value = text;
			return returnValue;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00010C28 File Offset: 0x0000EE28
		internal bool HandleIntAttribute(XmlReader reader, ref int field)
		{
			int num;
			if (!Utils.GetInt(this.Schema, reader, out num))
			{
				return false;
			}
			field = num;
			return true;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00010C4C File Offset: 0x0000EE4C
		internal bool HandleByteAttribute(XmlReader reader, ref byte field)
		{
			byte b;
			if (!Utils.GetByte(this.Schema, reader, out b))
			{
				return false;
			}
			field = b;
			return true;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00010C70 File Offset: 0x0000EE70
		internal bool HandleBoolAttribute(XmlReader reader, ref bool field)
		{
			bool flag;
			if (!Utils.GetBool(this.Schema, reader, out flag))
			{
				return false;
			}
			field = flag;
			return true;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00010C93 File Offset: 0x0000EE93
		protected virtual void SkipThroughElement(XmlReader reader)
		{
			this.Parse(reader);
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x00010C9C File Offset: 0x0000EE9C
		protected string SchemaLocation
		{
			get
			{
				if (this.Schema != null)
				{
					return this.Schema.Location;
				}
				return null;
			}
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00010CB3 File Offset: 0x0000EEB3
		protected virtual bool HandleText(XmlReader reader)
		{
			return false;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00010CB6 File Offset: 0x0000EEB6
		internal virtual SchemaElement Clone(SchemaElement parentElement)
		{
			throw Error.NotImplemented();
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00010CBD File Offset: 0x0000EEBD
		private void HandleDocumentationElement(XmlReader reader)
		{
			this.Documentation = new DocumentationElement(this);
			this.Documentation.Parse(reader);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00010CD7 File Offset: 0x0000EED7
		protected virtual void HandleNameAttribute(XmlReader reader)
		{
			this.Name = this.HandleUndottedNameAttribute(reader, this.Name);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00010CEC File Offset: 0x0000EEEC
		private void AddError(ErrorCode errorCode, EdmSchemaErrorSeverity severity, string sourceLocation, int lineNumber, int linePosition, object message)
		{
			string text = message as string;
			EdmSchemaError edmSchemaError;
			if (text != null)
			{
				edmSchemaError = new EdmSchemaError(text, (int)errorCode, severity, sourceLocation, lineNumber, linePosition);
			}
			else
			{
				Exception ex = message as Exception;
				if (ex != null)
				{
					edmSchemaError = new EdmSchemaError(ex.Message, (int)errorCode, severity, sourceLocation, lineNumber, linePosition, ex);
				}
				else
				{
					edmSchemaError = new EdmSchemaError(message.ToString(), (int)errorCode, severity, sourceLocation, lineNumber, linePosition);
				}
			}
			this.Schema.AddError(edmSchemaError);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00010D58 File Offset: 0x0000EF58
		private void ParseAttribute(XmlReader reader)
		{
			string namespaceURI = reader.NamespaceURI;
			if (!this.Schema.IsParseableXmlNamespace(namespaceURI, true))
			{
				this.AddOtherContent(reader);
				return;
			}
			if (!this.ProhibitAttribute(namespaceURI, reader.LocalName))
			{
				this.HandleAttribute(reader);
				return;
			}
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00010D9C File Offset: 0x0000EF9C
		protected virtual bool ProhibitAttribute(string namespaceUri, string localName)
		{
			return false;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00010D9F File Offset: 0x0000EF9F
		internal static bool CanHandleAttribute(XmlReader reader, string localName)
		{
			return reader.NamespaceURI.Length == 0 && reader.LocalName == localName;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00010DBC File Offset: 0x0000EFBC
		protected virtual bool HandleAttribute(XmlReader reader)
		{
			if (SchemaElement.CanHandleAttribute(reader, "Name"))
			{
				this.HandleNameAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00010DD8 File Offset: 0x0000EFD8
		private bool AddOtherContent(XmlReader reader)
		{
			int num;
			int num2;
			SchemaElement.GetPositionInfo(reader, out num, out num2);
			MetadataProperty property;
			if (reader.NodeType == XmlNodeType.Element)
			{
				if (this._schema.SchemaVersion == 1.0 || this._schema.SchemaVersion == 1.1)
				{
					return true;
				}
				if (this._schema.SchemaVersion == 2.0 && reader.NamespaceURI == "http://schemas.microsoft.com/ado/2006/04/codegeneration")
				{
					this.AddError(ErrorCode.NoCodeGenNamespaceInStructuralAnnotation, EdmSchemaErrorSeverity.Error, num, num2, Strings.NoCodeGenNamespaceInStructuralAnnotation("http://schemas.microsoft.com/ado/2006/04/codegeneration"));
					return true;
				}
				using (XmlReader xmlReader = reader.ReadSubtree())
				{
					xmlReader.Read();
					XElement xelement = XElement.Load(new StringReader(xmlReader.ReadOuterXml()));
					property = SchemaElement.CreateMetadataPropertyFromOtherNamespaceXmlArtifact(xelement.Name.NamespaceName, xelement.Name.LocalName, xelement);
					goto IL_010E;
				}
			}
			if (reader.NamespaceURI == "http://www.w3.org/2000/xmlns/")
			{
				return true;
			}
			property = SchemaElement.CreateMetadataPropertyFromOtherNamespaceXmlArtifact(reader.NamespaceURI, reader.LocalName, reader.Value);
			IL_010E:
			if (!this.OtherContent.Any((MetadataProperty mp) => mp.Identity == property.Identity))
			{
				this.OtherContent.Add(property);
			}
			else
			{
				this.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, num, num2, Strings.DuplicateAnnotation(property.Identity, this.FQName));
			}
			return false;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00010F54 File Offset: 0x0000F154
		internal static MetadataProperty CreateMetadataPropertyFromOtherNamespaceXmlArtifact(string xmlNamespaceUri, string artifactName, object value)
		{
			return new MetadataProperty(xmlNamespaceUri + ":" + artifactName, TypeUsage.Create(EdmProviderManifest.Instance.GetPrimitiveType(PrimitiveTypeKind.String)), value);
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00010F7C File Offset: 0x0000F17C
		private bool ParseElement(XmlReader reader)
		{
			string namespaceURI = reader.NamespaceURI;
			if (!this.Schema.IsParseableXmlNamespace(namespaceURI, true) && this.ParentElement != null)
			{
				return this.AddOtherContent(reader);
			}
			if (this.HandleElement(reader))
			{
				return false;
			}
			if (string.IsNullOrEmpty(namespaceURI) || this.Schema.IsParseableXmlNamespace(reader.NamespaceURI, false))
			{
				this.AddError(ErrorCode.UnexpectedXmlElement, EdmSchemaErrorSeverity.Error, reader, Strings.UnexpectedXmlElement(reader.Name));
			}
			return true;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00010FEC File Offset: 0x0000F1EC
		protected bool CanHandleElement(XmlReader reader, string localName)
		{
			return reader.NamespaceURI == this.Schema.SchemaXmlNamespace && reader.LocalName == localName;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00011014 File Offset: 0x0000F214
		protected virtual bool HandleElement(XmlReader reader)
		{
			if (this.CanHandleElement(reader, "Documentation"))
			{
				this.HandleDocumentationElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0001102E File Offset: 0x0000F22E
		private void ParseText(XmlReader reader)
		{
			if (this.HandleText(reader))
			{
				return;
			}
			if (reader.Value == null || reader.Value.Trim().Length != 0)
			{
				this.AddError(ErrorCode.TextNotAllowed, EdmSchemaErrorSeverity.Error, reader, Strings.TextNotAllowed(reader.Value));
			}
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00011069 File Offset: 0x0000F269
		[Conditional("DEBUG")]
		internal static void AssertReaderConsidersSchemaInvalid(XmlReader reader)
		{
		}

		// Token: 0x040006A9 RID: 1705
		internal const string XmlNamespaceNamespace = "http://www.w3.org/2000/xmlns/";

		// Token: 0x040006AA RID: 1706
		private SchemaElement _parentElement;

		// Token: 0x040006AB RID: 1707
		private Schema _schema;

		// Token: 0x040006AC RID: 1708
		private int _lineNumber;

		// Token: 0x040006AD RID: 1709
		private int _linePosition;

		// Token: 0x040006AE RID: 1710
		private string _name;

		// Token: 0x040006AF RID: 1711
		private DocumentationElement _documentation;

		// Token: 0x040006B0 RID: 1712
		private List<MetadataProperty> _otherContent;

		// Token: 0x040006B1 RID: 1713
		protected const int MaxValueVersionComponent = 32767;
	}
}
