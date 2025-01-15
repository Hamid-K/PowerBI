using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001AE RID: 430
	internal abstract class XmlDocumentParser
	{
		// Token: 0x06000BF6 RID: 3062 RVA: 0x0002296C File Offset: 0x00020B6C
		protected XmlDocumentParser(XmlReader underlyingReader, string documentPath)
		{
			this.reader = underlyingReader;
			this.docPath = documentPath;
			this.errors = new List<EdmError>();
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x00022998 File Offset: 0x00020B98
		internal string DocumentPath
		{
			get
			{
				return this.docPath;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x000229A0 File Offset: 0x00020BA0
		// (set) Token: 0x06000BF9 RID: 3065 RVA: 0x000229A8 File Offset: 0x00020BA8
		internal string DocumentNamespace { get; private set; }

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x000229B1 File Offset: 0x00020BB1
		// (set) Token: 0x06000BFB RID: 3067 RVA: 0x000229B9 File Offset: 0x00020BB9
		internal Version DocumentVersion { get; private set; }

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x000229C2 File Offset: 0x00020BC2
		// (set) Token: 0x06000BFD RID: 3069 RVA: 0x000229CA File Offset: 0x00020BCA
		internal CsdlLocation DocumentElementLocation { get; private set; }

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x000229D3 File Offset: 0x00020BD3
		// (set) Token: 0x06000BFF RID: 3071 RVA: 0x000229DB File Offset: 0x00020BDB
		internal bool HasErrors { get; private set; }

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x000229E4 File Offset: 0x00020BE4
		// (set) Token: 0x06000C01 RID: 3073 RVA: 0x000229EC File Offset: 0x00020BEC
		internal XmlElementValue Result { get; private set; }

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x000229F8 File Offset: 0x00020BF8
		internal CsdlLocation Location
		{
			get
			{
				int num = 0;
				int num2 = 0;
				if (this.xmlLineInfo != null && this.xmlLineInfo.HasLineInfo())
				{
					num = this.xmlLineInfo.LineNumber;
					num2 = this.xmlLineInfo.LinePosition;
				}
				return new CsdlLocation(this.DocumentPath, num, num2);
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x00022A43 File Offset: 0x00020C43
		internal IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x00022A4C File Offset: 0x00020C4C
		private bool IsTextNode
		{
			get
			{
				XmlNodeType nodeType = this.reader.NodeType;
				return nodeType == 3 || nodeType == 4 || nodeType == 14;
			}
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00022A78 File Offset: 0x00020C78
		internal void ParseDocumentElement()
		{
			this.reader = this.InitializeReader(this.reader);
			this.xmlLineInfo = this.reader as IXmlLineInfo;
			if (this.reader.NodeType != 1)
			{
				while (this.reader.Read() && this.reader.NodeType != 1)
				{
				}
			}
			if (this.reader.EOF)
			{
				this.ReportEmptyFile();
				return;
			}
			this.DocumentNamespace = this.reader.NamespaceURI;
			Version version;
			string[] array;
			if (!this.TryGetDocumentVersion(this.DocumentNamespace, out version, out array))
			{
				this.ReportUnexpectedRootNamespace(this.reader.LocalName, this.DocumentNamespace, array);
				return;
			}
			this.DocumentVersion = version;
			this.DocumentElementLocation = this.Location;
			bool isEmptyElement = this.reader.IsEmptyElement;
			XmlElementInfo xmlElementInfo = this.ReadElement(this.reader.LocalName, this.DocumentElementLocation);
			XmlElementParser xmlElementParser;
			if (!this.TryGetRootElementParser(this.DocumentVersion, xmlElementInfo, out xmlElementParser))
			{
				this.ReportUnexpectedRootElement(xmlElementInfo.Location, xmlElementInfo.Name, this.DocumentNamespace);
				return;
			}
			this.BeginElement(xmlElementParser, xmlElementInfo);
			if (isEmptyElement)
			{
				this.EndElement();
				return;
			}
			this.Parse();
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00022B9D File Offset: 0x00020D9D
		protected void ReportError(CsdlLocation errorLocation, EdmErrorCode errorCode, string errorMessage)
		{
			this.errors.Add(new EdmError(errorLocation, errorCode, errorMessage));
			this.HasErrors = true;
		}

		// Token: 0x06000C07 RID: 3079
		protected abstract XmlReader InitializeReader(XmlReader inputReader);

		// Token: 0x06000C08 RID: 3080
		protected abstract bool TryGetDocumentVersion(string xmlNamespaceName, out Version version, out string[] expectedNamespaces);

		// Token: 0x06000C09 RID: 3081
		protected abstract bool TryGetRootElementParser(Version artifactVersion, XmlElementInfo rootElement, out XmlElementParser parser);

		// Token: 0x06000C0A RID: 3082 RVA: 0x00022BB9 File Offset: 0x00020DB9
		protected virtual bool IsOwnedNamespace(string namespaceName)
		{
			return this.DocumentNamespace.EqualsOrdinal(namespaceName);
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00022BC7 File Offset: 0x00020DC7
		protected virtual XmlElementParser<TResult> Element<TResult>(string elementName, Func<XmlElementInfo, XmlElementValueCollection, TResult> parserFunc, params XmlElementParser[] childParsers)
		{
			return XmlElementParser.Create<TResult>(elementName, parserFunc, childParsers, null);
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00022BD2 File Offset: 0x00020DD2
		private void Parse()
		{
			while (this.currentBranch.Count > 0 && this.reader.Read())
			{
				this.ProcessNode();
			}
			if (!this.reader.EOF)
			{
				this.reader.Read();
			}
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00022C10 File Offset: 0x00020E10
		private void EndElement()
		{
			XmlDocumentParser.ElementScope elementScope = this.currentBranch.Pop();
			this.currentScope = ((this.currentBranch.Count > 0) ? this.currentBranch.Peek() : null);
			XmlElementParser parser = elementScope.Parser;
			XmlElementValue xmlElementValue = parser.Parse(elementScope.Element, elementScope.ChildValues);
			if (xmlElementValue != null)
			{
				if (this.currentScope != null)
				{
					this.currentScope.AddChildValue(xmlElementValue);
				}
				else
				{
					this.Result = xmlElementValue;
				}
			}
			foreach (XmlAttributeInfo xmlAttributeInfo in elementScope.Element.Attributes.Unused)
			{
				this.ReportUnexpectedAttribute(xmlAttributeInfo.Location, xmlAttributeInfo.Name);
			}
			IEnumerable<XmlElementValue> enumerable = Enumerable.Where<XmlElementValue>(elementScope.ChildValues, (XmlElementValue v) => v.IsText);
			IEnumerable<XmlElementValue> enumerable2 = Enumerable.Where<XmlElementValue>(enumerable, (XmlElementValue t) => !t.IsUsed);
			if (Enumerable.Any<XmlElementValue>(enumerable2))
			{
				XmlTextValue xmlTextValue;
				if (Enumerable.Count<XmlElementValue>(enumerable2) == Enumerable.Count<XmlElementValue>(enumerable))
				{
					xmlTextValue = (XmlTextValue)Enumerable.First<XmlElementValue>(enumerable);
				}
				else
				{
					xmlTextValue = (XmlTextValue)Enumerable.First<XmlElementValue>(enumerable2);
				}
				this.ReportTextNotAllowed(xmlTextValue.Location, xmlTextValue.Value);
			}
			foreach (XmlElementValue xmlElementValue2 in Enumerable.Where<XmlElementValue>(elementScope.ChildValues, (XmlElementValue v) => !v.IsText && !v.IsUsed))
			{
				this.ReportUnusedElement(xmlElementValue2.Location, xmlElementValue2.Name);
			}
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00022DF0 File Offset: 0x00020FF0
		private void BeginElement(XmlElementParser elementParser, XmlElementInfo element)
		{
			XmlDocumentParser.ElementScope elementScope = new XmlDocumentParser.ElementScope(elementParser, element);
			this.currentBranch.Push(elementScope);
			this.currentScope = elementScope;
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00022E18 File Offset: 0x00021018
		private void ProcessNode()
		{
			if (this.IsTextNode)
			{
				if (this.currentText == null)
				{
					this.currentText = new StringBuilder();
					this.currentTextLocation = this.Location;
				}
				this.currentText.Append(this.reader.Value);
				return;
			}
			if (this.currentText != null)
			{
				string text = this.currentText.ToString();
				CsdlLocation csdlLocation = this.currentTextLocation;
				this.currentText = null;
				this.currentTextLocation = null;
				if (!EdmUtil.IsNullOrWhiteSpaceInternal(text) && !string.IsNullOrEmpty(text))
				{
					this.currentScope.AddChildValue(new XmlTextValue(csdlLocation, text));
				}
			}
			switch (this.reader.NodeType)
			{
			case 1:
				this.ProcessElement();
				return;
			case 5:
			case 10:
				this.reader.Skip();
				return;
			case 7:
			case 8:
			case 12:
			case 13:
			case 17:
				return;
			case 15:
				this.EndElement();
				return;
			}
			this.ReportUnexpectedNodeType(this.reader.NodeType);
			this.reader.Skip();
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00022F40 File Offset: 0x00021140
		private void ProcessElement()
		{
			bool isEmptyElement = this.reader.IsEmptyElement;
			string namespaceURI = this.reader.NamespaceURI;
			string localName = this.reader.LocalName;
			if (namespaceURI == this.DocumentNamespace)
			{
				XmlElementParser xmlElementParser;
				if (!this.currentScope.Parser.TryGetChildElementParser(localName, out xmlElementParser))
				{
					if (localName != "Annotation")
					{
						this.ReportUnexpectedElement(this.Location, this.reader.Name);
					}
					if (!isEmptyElement)
					{
						int depth = this.reader.Depth;
						do
						{
							this.reader.Read();
						}
						while (this.reader.Depth > depth);
					}
					return;
				}
				XmlElementInfo xmlElementInfo = this.ReadElement(localName, this.Location);
				this.BeginElement(xmlElementParser, xmlElementInfo);
				if (isEmptyElement)
				{
					this.EndElement();
					return;
				}
			}
			else
			{
				if (string.IsNullOrEmpty(namespaceURI) || this.IsOwnedNamespace(namespaceURI))
				{
					if (localName != "Annotation")
					{
						this.ReportUnexpectedElement(this.Location, this.reader.Name);
					}
					this.reader.Skip();
					return;
				}
				XmlReader xmlReader = this.reader.ReadSubtree();
				xmlReader.MoveToContent();
				string text = xmlReader.ReadOuterXml();
				this.currentScope.Element.AddAnnotation(new XmlAnnotationInfo(this.Location, namespaceURI, localName, text, false));
			}
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0002308C File Offset: 0x0002128C
		private XmlElementInfo ReadElement(string elementName, CsdlLocation elementLocation)
		{
			List<XmlAttributeInfo> list = null;
			List<XmlAnnotationInfo> list2 = null;
			bool flag = this.reader.MoveToFirstAttribute();
			while (flag)
			{
				string namespaceURI = this.reader.NamespaceURI;
				if (string.IsNullOrEmpty(namespaceURI) || namespaceURI.EqualsOrdinal(this.DocumentNamespace))
				{
					if (list == null)
					{
						list = new List<XmlAttributeInfo>();
					}
					list.Add(new XmlAttributeInfo(this.reader.LocalName, this.reader.Value, this.Location));
				}
				else if (this.IsOwnedNamespace(namespaceURI))
				{
					this.ReportUnexpectedAttribute(this.Location, this.reader.Name);
				}
				else
				{
					if (list2 == null)
					{
						list2 = new List<XmlAnnotationInfo>();
					}
					list2.Add(new XmlAnnotationInfo(this.Location, this.reader.NamespaceURI, this.reader.LocalName, this.reader.Value, true));
				}
				flag = this.reader.MoveToNextAttribute();
			}
			return new XmlElementInfo(elementName, elementLocation, list, list2);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0002317C File Offset: 0x0002137C
		private void ReportEmptyFile()
		{
			string text = ((this.DocumentPath == null) ? Strings.XmlParser_EmptySchemaTextReader : Strings.XmlParser_EmptyFile(this.DocumentPath));
			this.ReportError(this.Location, EdmErrorCode.EmptyFile, text);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x000231B4 File Offset: 0x000213B4
		private void ReportUnexpectedRootNamespace(string elementName, string namespaceUri, string[] expectedNamespaces)
		{
			string text = string.Join(", ", expectedNamespaces);
			string text2 = (string.IsNullOrEmpty(namespaceUri) ? Strings.XmlParser_UnexpectedRootElementNoNamespace(text) : Strings.XmlParser_UnexpectedRootElementWrongNamespace(namespaceUri, text));
			this.ReportError(this.Location, EdmErrorCode.UnexpectedXmlElement, text2);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x000231F4 File Offset: 0x000213F4
		private void ReportUnexpectedRootElement(CsdlLocation elementLocation, string elementName, string expectedNamespace)
		{
			this.ReportError(elementLocation, EdmErrorCode.UnexpectedXmlElement, Strings.XmlParser_UnexpectedRootElement(elementName, "Schema"));
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0002320A File Offset: 0x0002140A
		private void ReportUnexpectedAttribute(CsdlLocation errorLocation, string attributeName)
		{
			this.ReportError(errorLocation, EdmErrorCode.UnexpectedXmlAttribute, Strings.XmlParser_UnexpectedAttribute(attributeName));
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0002321B File Offset: 0x0002141B
		private void ReportUnexpectedNodeType(XmlNodeType nodeType)
		{
			this.ReportError(this.Location, EdmErrorCode.UnexpectedXmlNodeType, Strings.XmlParser_UnexpectedNodeType(nodeType));
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00023235 File Offset: 0x00021435
		private void ReportUnexpectedElement(CsdlLocation errorLocation, string elementName)
		{
			this.ReportError(errorLocation, EdmErrorCode.UnexpectedXmlElement, Strings.XmlParser_UnexpectedElement(elementName));
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00023246 File Offset: 0x00021446
		private void ReportUnusedElement(CsdlLocation errorLocation, string elementName)
		{
			this.ReportError(errorLocation, EdmErrorCode.UnexpectedXmlElement, Strings.XmlParser_UnusedElement(elementName));
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00023257 File Offset: 0x00021457
		private void ReportTextNotAllowed(CsdlLocation errorLocation, string textValue)
		{
			this.ReportError(errorLocation, EdmErrorCode.TextNotAllowed, Strings.XmlParser_TextNotAllowed(textValue));
		}

		// Token: 0x040006A0 RID: 1696
		private readonly string docPath;

		// Token: 0x040006A1 RID: 1697
		private readonly Stack<XmlDocumentParser.ElementScope> currentBranch = new Stack<XmlDocumentParser.ElementScope>();

		// Token: 0x040006A2 RID: 1698
		private XmlReader reader;

		// Token: 0x040006A3 RID: 1699
		private IXmlLineInfo xmlLineInfo;

		// Token: 0x040006A4 RID: 1700
		private List<EdmError> errors;

		// Token: 0x040006A5 RID: 1701
		private StringBuilder currentText;

		// Token: 0x040006A6 RID: 1702
		private CsdlLocation currentTextLocation;

		// Token: 0x040006A7 RID: 1703
		private XmlDocumentParser.ElementScope currentScope;

		// Token: 0x020002E2 RID: 738
		private class ElementScope
		{
			// Token: 0x06001099 RID: 4249 RVA: 0x0002BA55 File Offset: 0x00029C55
			internal ElementScope(XmlElementParser parser, XmlElementInfo element)
			{
				this.Parser = parser;
				this.Element = element;
			}

			// Token: 0x170004B3 RID: 1203
			// (get) Token: 0x0600109A RID: 4250 RVA: 0x0002BA6B File Offset: 0x00029C6B
			// (set) Token: 0x0600109B RID: 4251 RVA: 0x0002BA73 File Offset: 0x00029C73
			internal XmlElementParser Parser { get; private set; }

			// Token: 0x170004B4 RID: 1204
			// (get) Token: 0x0600109C RID: 4252 RVA: 0x0002BA7C File Offset: 0x00029C7C
			// (set) Token: 0x0600109D RID: 4253 RVA: 0x0002BA84 File Offset: 0x00029C84
			internal XmlElementInfo Element { get; private set; }

			// Token: 0x170004B5 RID: 1205
			// (get) Token: 0x0600109E RID: 4254 RVA: 0x0002BA90 File Offset: 0x00029C90
			internal IList<XmlElementValue> ChildValues
			{
				get
				{
					IList<XmlElementValue> list = this.childValues;
					return list ?? XmlDocumentParser.ElementScope.EmptyValues;
				}
			}

			// Token: 0x0600109F RID: 4255 RVA: 0x0002BAAE File Offset: 0x00029CAE
			internal void AddChildValue(XmlElementValue value)
			{
				if (this.childValues == null)
				{
					this.childValues = new List<XmlElementValue>();
				}
				this.childValues.Add(value);
			}

			// Token: 0x04000862 RID: 2146
			private static readonly IList<XmlElementValue> EmptyValues = new ReadOnlyCollection<XmlElementValue>(new XmlElementValue[0]);

			// Token: 0x04000863 RID: 2147
			private List<XmlElementValue> childValues;
		}
	}
}
