using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Resources;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000313 RID: 787
	[DebuggerDisplay("Name={Name}")]
	internal abstract class SchemaElement
	{
		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x0600256C RID: 9580 RVA: 0x0006ADF1 File Offset: 0x00068FF1
		internal int LineNumber
		{
			get
			{
				return this._lineNumber;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x0600256D RID: 9581 RVA: 0x0006ADF9 File Offset: 0x00068FF9
		internal int LinePosition
		{
			get
			{
				return this._linePosition;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x0600256E RID: 9582 RVA: 0x0006AE01 File Offset: 0x00069001
		// (set) Token: 0x0600256F RID: 9583 RVA: 0x0006AE09 File Offset: 0x00069009
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

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06002570 RID: 9584 RVA: 0x0006AE12 File Offset: 0x00069012
		// (set) Token: 0x06002571 RID: 9585 RVA: 0x0006AE1A File Offset: 0x0006901A
		internal DocumentationElement Documentation { get; set; }

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06002572 RID: 9586 RVA: 0x0006AE23 File Offset: 0x00069023
		// (set) Token: 0x06002573 RID: 9587 RVA: 0x0006AE2B File Offset: 0x0006902B
		internal SchemaElement ParentElement { get; private set; }

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06002574 RID: 9588 RVA: 0x0006AE34 File Offset: 0x00069034
		// (set) Token: 0x06002575 RID: 9589 RVA: 0x0006AE3C File Offset: 0x0006903C
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

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06002576 RID: 9590 RVA: 0x0006AE45 File Offset: 0x00069045
		public virtual string FQName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06002577 RID: 9591 RVA: 0x0006AE4D File Offset: 0x0006904D
		public virtual string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06002578 RID: 9592 RVA: 0x0006AE55 File Offset: 0x00069055
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

		// Token: 0x06002579 RID: 9593 RVA: 0x0006AE70 File Offset: 0x00069070
		internal virtual void Validate()
		{
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x0006AE72 File Offset: 0x00069072
		internal void AddError(ErrorCode errorCode, EdmSchemaErrorSeverity severity, int lineNumber, int linePosition, object message)
		{
			this.AddError(errorCode, severity, this.SchemaLocation, lineNumber, linePosition, message);
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x0006AE88 File Offset: 0x00069088
		internal void AddError(ErrorCode errorCode, EdmSchemaErrorSeverity severity, XmlReader reader, object message)
		{
			int num;
			int num2;
			SchemaElement.GetPositionInfo(reader, out num, out num2);
			this.AddError(errorCode, severity, this.SchemaLocation, num, num2, message);
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x0006AEB1 File Offset: 0x000690B1
		internal void AddError(ErrorCode errorCode, EdmSchemaErrorSeverity severity, object message)
		{
			this.AddError(errorCode, severity, this.SchemaLocation, this.LineNumber, this.LinePosition, message);
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x0006AECE File Offset: 0x000690CE
		internal void AddError(ErrorCode errorCode, EdmSchemaErrorSeverity severity, SchemaElement element, object message)
		{
			this.AddError(errorCode, severity, element.Schema.Location, element.LineNumber, element.LinePosition, message);
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x0006AEF4 File Offset: 0x000690F4
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

		// Token: 0x0600257F RID: 9599 RVA: 0x0006B030 File Offset: 0x00069230
		internal void GetPositionInfo(XmlReader reader)
		{
			SchemaElement.GetPositionInfo(reader, out this._lineNumber, out this._linePosition);
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x0006B044 File Offset: 0x00069244
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

		// Token: 0x06002581 RID: 9601 RVA: 0x0006B07A File Offset: 0x0006927A
		internal virtual void ResolveTopLevelNames()
		{
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x0006B07C File Offset: 0x0006927C
		internal virtual void ResolveSecondLevelNames()
		{
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x0006B080 File Offset: 0x00069280
		internal SchemaElement(SchemaElement parentElement, IDbDependencyResolver resolver = null)
		{
			this._resolver = resolver ?? DbConfiguration.DependencyResolver;
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
					throw new InvalidOperationException(Strings.AllElementsMustBeInSchema);
				}
			}
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x0006B0E1 File Offset: 0x000692E1
		internal SchemaElement(SchemaElement parentElement, string name, IDbDependencyResolver resolver = null)
			: this(parentElement, resolver)
		{
			this._name = name;
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x0006B0F2 File Offset: 0x000692F2
		protected virtual void HandleAttributesComplete()
		{
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x0006B0F4 File Offset: 0x000692F4
		protected virtual void HandleChildElementsComplete()
		{
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x0006B0F8 File Offset: 0x000692F8
		protected string HandleUndottedNameAttribute(XmlReader reader, string field)
		{
			string text = field;
			Utils.GetUndottedName(this.Schema, reader, out text);
			return text;
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x0006B118 File Offset: 0x00069318
		protected ReturnValue<string> HandleDottedNameAttribute(XmlReader reader, string field)
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

		// Token: 0x06002589 RID: 9609 RVA: 0x0006B148 File Offset: 0x00069348
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

		// Token: 0x0600258A RID: 9610 RVA: 0x0006B16C File Offset: 0x0006936C
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

		// Token: 0x0600258B RID: 9611 RVA: 0x0006B190 File Offset: 0x00069390
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

		// Token: 0x0600258C RID: 9612 RVA: 0x0006B1B3 File Offset: 0x000693B3
		protected virtual void SkipThroughElement(XmlReader reader)
		{
			this.Parse(reader);
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x0006B1BC File Offset: 0x000693BC
		protected virtual void SkipElement(XmlReader reader)
		{
			using (XmlReader xmlReader = reader.ReadSubtree())
			{
				while (xmlReader.Read())
				{
				}
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x0600258E RID: 9614 RVA: 0x0006B1F4 File Offset: 0x000693F4
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

		// Token: 0x0600258F RID: 9615 RVA: 0x0006B20B File Offset: 0x0006940B
		protected virtual bool HandleText(XmlReader reader)
		{
			return false;
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x0006B20E File Offset: 0x0006940E
		internal virtual SchemaElement Clone(SchemaElement parentElement)
		{
			throw Error.NotImplemented();
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x0006B215 File Offset: 0x00069415
		private void HandleDocumentationElement(XmlReader reader)
		{
			this.Documentation = new DocumentationElement(this);
			this.Documentation.Parse(reader);
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x0006B22F File Offset: 0x0006942F
		protected virtual void HandleNameAttribute(XmlReader reader)
		{
			this.Name = this.HandleUndottedNameAttribute(reader, this.Name);
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x0006B244 File Offset: 0x00069444
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

		// Token: 0x06002594 RID: 9620 RVA: 0x0006B2B0 File Offset: 0x000694B0
		private void ParseAttribute(XmlReader reader)
		{
			string namespaceURI = reader.NamespaceURI;
			if (namespaceURI == "http://schemas.microsoft.com/ado/2009/02/edm/annotation" && reader.LocalName == "UseStrongSpatialTypes" && !this.ProhibitAttribute(namespaceURI, reader.LocalName) && this.HandleAttribute(reader))
			{
				return;
			}
			if (!this.Schema.IsParseableXmlNamespace(namespaceURI, true))
			{
				this.AddOtherContent(reader);
				return;
			}
			if (!this.ProhibitAttribute(namespaceURI, reader.LocalName) && this.HandleAttribute(reader))
			{
				return;
			}
			if ((reader.SchemaInfo == null || reader.SchemaInfo.Validity != XmlSchemaValidity.Invalid) && (string.IsNullOrEmpty(namespaceURI) || this.Schema.IsParseableXmlNamespace(namespaceURI, true)))
			{
				this.AddError(ErrorCode.UnexpectedXmlAttribute, EdmSchemaErrorSeverity.Error, reader, Strings.UnexpectedXmlAttribute(reader.Name));
			}
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x0006B36F File Offset: 0x0006956F
		protected virtual bool ProhibitAttribute(string namespaceUri, string localName)
		{
			return false;
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x0006B372 File Offset: 0x00069572
		internal static bool CanHandleAttribute(XmlReader reader, string localName)
		{
			return reader.NamespaceURI.Length == 0 && reader.LocalName == localName;
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x0006B38F File Offset: 0x0006958F
		protected virtual bool HandleAttribute(XmlReader reader)
		{
			if (SchemaElement.CanHandleAttribute(reader, "Name"))
			{
				this.HandleNameAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x0006B3A8 File Offset: 0x000695A8
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
				if (this._schema.SchemaVersion >= 2.0 && reader.NamespaceURI == "http://schemas.microsoft.com/ado/2006/04/codegeneration")
				{
					this.AddError(ErrorCode.NoCodeGenNamespaceInStructuralAnnotation, EdmSchemaErrorSeverity.Error, num, num2, Strings.NoCodeGenNamespaceInStructuralAnnotation("http://schemas.microsoft.com/ado/2006/04/codegeneration"));
					return true;
				}
				using (XmlReader xmlReader = reader.ReadSubtree())
				{
					xmlReader.Read();
					using (StringReader stringReader = new StringReader(xmlReader.ReadOuterXml()))
					{
						XElement xelement = XElement.Load(stringReader);
						property = SchemaElement.CreateMetadataPropertyFromXmlElement(xelement.Name.NamespaceName, xelement.Name.LocalName, xelement);
						goto IL_011F;
					}
				}
			}
			if (reader.NamespaceURI == "http://www.w3.org/2000/xmlns/")
			{
				return true;
			}
			property = this.CreateMetadataPropertyFromXmlAttribute(reader.NamespaceURI, reader.LocalName, reader.Value);
			IL_011F:
			if (!this.OtherContent.Exists((MetadataProperty mp) => mp.Identity == property.Identity))
			{
				this.OtherContent.Add(property);
			}
			else
			{
				this.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, num, num2, Strings.DuplicateAnnotation(property.Identity, this.FQName));
			}
			return false;
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x0006B540 File Offset: 0x00069740
		internal static MetadataProperty CreateMetadataPropertyFromXmlElement(string xmlNamespaceUri, string elementName, XElement value)
		{
			return MetadataProperty.CreateAnnotation(xmlNamespaceUri + ":" + elementName, value);
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x0006B554 File Offset: 0x00069754
		internal MetadataProperty CreateMetadataPropertyFromXmlAttribute(string xmlNamespaceUri, string attributeName, string value)
		{
			Func<IMetadataAnnotationSerializer> service = this._resolver.GetService(attributeName);
			object obj = ((service == null) ? value : service().Deserialize(attributeName, value));
			return MetadataProperty.CreateAnnotation(xmlNamespaceUri + ":" + attributeName, obj);
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x0006B594 File Offset: 0x00069794
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

		// Token: 0x0600259C RID: 9628 RVA: 0x0006B604 File Offset: 0x00069804
		protected bool CanHandleElement(XmlReader reader, string localName)
		{
			return reader.NamespaceURI == this.Schema.SchemaXmlNamespace && reader.LocalName == localName;
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x0006B62C File Offset: 0x0006982C
		protected virtual bool HandleElement(XmlReader reader)
		{
			if (this.CanHandleElement(reader, "Documentation"))
			{
				this.HandleDocumentationElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x0006B646 File Offset: 0x00069846
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

		// Token: 0x0600259F RID: 9631 RVA: 0x0006B681 File Offset: 0x00069881
		[Conditional("DEBUG")]
		internal static void AssertReaderConsidersSchemaInvalid(XmlReader reader)
		{
		}

		// Token: 0x04000D33 RID: 3379
		internal const string XmlNamespaceNamespace = "http://www.w3.org/2000/xmlns/";

		// Token: 0x04000D34 RID: 3380
		private Schema _schema;

		// Token: 0x04000D35 RID: 3381
		private int _lineNumber;

		// Token: 0x04000D36 RID: 3382
		private int _linePosition;

		// Token: 0x04000D37 RID: 3383
		private string _name;

		// Token: 0x04000D38 RID: 3384
		private List<MetadataProperty> _otherContent;

		// Token: 0x04000D39 RID: 3385
		private readonly IDbDependencyResolver _resolver;

		// Token: 0x04000D3A RID: 3386
		protected const int MaxValueVersionComponent = 32767;
	}
}
