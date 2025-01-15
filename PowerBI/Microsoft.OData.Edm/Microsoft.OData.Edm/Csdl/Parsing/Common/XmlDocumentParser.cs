using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001BB RID: 443
	internal abstract class XmlDocumentParser
	{
		// Token: 0x06000CA8 RID: 3240 RVA: 0x00024B28 File Offset: 0x00022D28
		protected XmlDocumentParser(XmlReader underlyingReader, string documentPath)
		{
			this.reader = underlyingReader;
			this.docPath = documentPath;
			this.errors = new List<EdmError>();
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x00024B54 File Offset: 0x00022D54
		internal string DocumentPath
		{
			get
			{
				return this.docPath;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x00024B5C File Offset: 0x00022D5C
		// (set) Token: 0x06000CAB RID: 3243 RVA: 0x00024B64 File Offset: 0x00022D64
		internal string DocumentNamespace { get; private set; }

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x00024B6D File Offset: 0x00022D6D
		// (set) Token: 0x06000CAD RID: 3245 RVA: 0x00024B75 File Offset: 0x00022D75
		internal Version DocumentVersion { get; private set; }

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x00024B7E File Offset: 0x00022D7E
		// (set) Token: 0x06000CAF RID: 3247 RVA: 0x00024B86 File Offset: 0x00022D86
		internal CsdlLocation DocumentElementLocation { get; private set; }

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x00024B8F File Offset: 0x00022D8F
		// (set) Token: 0x06000CB1 RID: 3249 RVA: 0x00024B97 File Offset: 0x00022D97
		internal bool HasErrors { get; private set; }

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x00024BA0 File Offset: 0x00022DA0
		// (set) Token: 0x06000CB3 RID: 3251 RVA: 0x00024BA8 File Offset: 0x00022DA8
		internal XmlElementValue Result { get; private set; }

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x00024BB4 File Offset: 0x00022DB4
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

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x00024BFF File Offset: 0x00022DFF
		internal IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x00024C08 File Offset: 0x00022E08
		private bool IsTextNode
		{
			get
			{
				XmlNodeType nodeType = this.reader.NodeType;
				return nodeType == XmlNodeType.Text || nodeType == XmlNodeType.CDATA || nodeType == XmlNodeType.SignificantWhitespace;
			}
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00024C34 File Offset: 0x00022E34
		internal void ParseDocumentElement()
		{
			this.reader = this.InitializeReader(this.reader);
			this.xmlLineInfo = this.reader as IXmlLineInfo;
			if (this.reader.NodeType != XmlNodeType.Element)
			{
				while (this.reader.Read() && this.reader.NodeType != XmlNodeType.Element)
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

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00024D59 File Offset: 0x00022F59
		protected void ReportError(CsdlLocation errorLocation, EdmErrorCode errorCode, string errorMessage)
		{
			this.errors.Add(new EdmError(errorLocation, errorCode, errorMessage));
			this.HasErrors = true;
		}

		// Token: 0x06000CB9 RID: 3257
		protected abstract XmlReader InitializeReader(XmlReader inputReader);

		// Token: 0x06000CBA RID: 3258
		protected abstract bool TryGetDocumentVersion(string xmlNamespaceName, out Version version, out string[] expectedNamespaces);

		// Token: 0x06000CBB RID: 3259
		protected abstract bool TryGetRootElementParser(Version artifactVersion, XmlElementInfo rootElement, out XmlElementParser parser);

		// Token: 0x06000CBC RID: 3260 RVA: 0x00024D75 File Offset: 0x00022F75
		protected virtual bool IsOwnedNamespace(string namespaceName)
		{
			return this.DocumentNamespace.EqualsOrdinal(namespaceName);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00024D83 File Offset: 0x00022F83
		protected virtual XmlElementParser<TResult> Element<TResult>(string elementName, Func<XmlElementInfo, XmlElementValueCollection, TResult> parserFunc, params XmlElementParser[] childParsers)
		{
			return XmlElementParser.Create<TResult>(elementName, parserFunc, childParsers, null);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00024D8E File Offset: 0x00022F8E
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

		// Token: 0x06000CBF RID: 3263 RVA: 0x00024DCC File Offset: 0x00022FCC
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
			IEnumerable<XmlElementValue> enumerable = elementScope.ChildValues.Where((XmlElementValue v) => v.IsText);
			IEnumerable<XmlElementValue> enumerable2 = enumerable.Where((XmlElementValue t) => !t.IsUsed);
			if (enumerable2.Any<XmlElementValue>())
			{
				XmlTextValue xmlTextValue;
				if (enumerable2.Count<XmlElementValue>() == enumerable.Count<XmlElementValue>())
				{
					xmlTextValue = (XmlTextValue)enumerable.First<XmlElementValue>();
				}
				else
				{
					xmlTextValue = (XmlTextValue)enumerable2.First<XmlElementValue>();
				}
				this.ReportTextNotAllowed(xmlTextValue.Location, xmlTextValue.Value);
			}
			foreach (XmlElementValue xmlElementValue2 in elementScope.ChildValues.Where((XmlElementValue v) => !v.IsText && !v.IsUsed))
			{
				this.ReportUnusedElement(xmlElementValue2.Location, xmlElementValue2.Name);
			}
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00024FAC File Offset: 0x000231AC
		private void BeginElement(XmlElementParser elementParser, XmlElementInfo element)
		{
			XmlDocumentParser.ElementScope elementScope = new XmlDocumentParser.ElementScope(elementParser, element);
			this.currentBranch.Push(elementScope);
			this.currentScope = elementScope;
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00024FD4 File Offset: 0x000231D4
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
			case XmlNodeType.Element:
				this.ProcessElement();
				return;
			case XmlNodeType.EntityReference:
			case XmlNodeType.DocumentType:
				this.reader.Skip();
				return;
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Comment:
			case XmlNodeType.Notation:
			case XmlNodeType.Whitespace:
			case XmlNodeType.XmlDeclaration:
				return;
			case XmlNodeType.EndElement:
				this.EndElement();
				return;
			}
			this.ReportUnexpectedNodeType(this.reader.NodeType);
			this.reader.Skip();
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x000250FC File Offset: 0x000232FC
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

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00025248 File Offset: 0x00023448
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

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00025338 File Offset: 0x00023538
		private void ReportEmptyFile()
		{
			string text = ((this.DocumentPath == null) ? Strings.XmlParser_EmptySchemaTextReader : Strings.XmlParser_EmptyFile(this.DocumentPath));
			this.ReportError(this.Location, EdmErrorCode.EmptyFile, text);
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00025370 File Offset: 0x00023570
		private void ReportUnexpectedRootNamespace(string elementName, string namespaceUri, string[] expectedNamespaces)
		{
			string text = string.Join(", ", expectedNamespaces);
			string text2 = (string.IsNullOrEmpty(namespaceUri) ? Strings.XmlParser_UnexpectedRootElementNoNamespace(text) : Strings.XmlParser_UnexpectedRootElementWrongNamespace(namespaceUri, text));
			this.ReportError(this.Location, EdmErrorCode.UnexpectedXmlElement, text2);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x000253B0 File Offset: 0x000235B0
		private void ReportUnexpectedRootElement(CsdlLocation elementLocation, string elementName, string expectedNamespace)
		{
			this.ReportError(elementLocation, EdmErrorCode.UnexpectedXmlElement, Strings.XmlParser_UnexpectedRootElement(elementName, "Schema"));
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x000253C6 File Offset: 0x000235C6
		private void ReportUnexpectedAttribute(CsdlLocation errorLocation, string attributeName)
		{
			this.ReportError(errorLocation, EdmErrorCode.UnexpectedXmlAttribute, Strings.XmlParser_UnexpectedAttribute(attributeName));
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x000253D7 File Offset: 0x000235D7
		private void ReportUnexpectedNodeType(XmlNodeType nodeType)
		{
			this.ReportError(this.Location, EdmErrorCode.UnexpectedXmlNodeType, Strings.XmlParser_UnexpectedNodeType(nodeType));
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x000253F1 File Offset: 0x000235F1
		private void ReportUnexpectedElement(CsdlLocation errorLocation, string elementName)
		{
			if (elementName != "Annotation")
			{
				this.ReportError(errorLocation, EdmErrorCode.UnexpectedXmlElement, Strings.XmlParser_UnexpectedElement(elementName));
			}
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0002540F File Offset: 0x0002360F
		private void ReportUnusedElement(CsdlLocation errorLocation, string elementName)
		{
			this.ReportError(errorLocation, EdmErrorCode.UnexpectedXmlElement, Strings.XmlParser_UnusedElement(elementName));
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00025420 File Offset: 0x00023620
		private void ReportTextNotAllowed(CsdlLocation errorLocation, string textValue)
		{
			this.ReportError(errorLocation, EdmErrorCode.TextNotAllowed, Strings.XmlParser_TextNotAllowed(textValue));
		}

		// Token: 0x04000719 RID: 1817
		private readonly string docPath;

		// Token: 0x0400071A RID: 1818
		private readonly Stack<XmlDocumentParser.ElementScope> currentBranch = new Stack<XmlDocumentParser.ElementScope>();

		// Token: 0x0400071B RID: 1819
		private XmlReader reader;

		// Token: 0x0400071C RID: 1820
		private IXmlLineInfo xmlLineInfo;

		// Token: 0x0400071D RID: 1821
		private List<EdmError> errors;

		// Token: 0x0400071E RID: 1822
		private StringBuilder currentText;

		// Token: 0x0400071F RID: 1823
		private CsdlLocation currentTextLocation;

		// Token: 0x04000720 RID: 1824
		private XmlDocumentParser.ElementScope currentScope;

		// Token: 0x020002FB RID: 763
		private class ElementScope
		{
			// Token: 0x0600117C RID: 4476 RVA: 0x0002E3F1 File Offset: 0x0002C5F1
			internal ElementScope(XmlElementParser parser, XmlElementInfo element)
			{
				this.Parser = parser;
				this.Element = element;
			}

			// Token: 0x170004EF RID: 1263
			// (get) Token: 0x0600117D RID: 4477 RVA: 0x0002E407 File Offset: 0x0002C607
			// (set) Token: 0x0600117E RID: 4478 RVA: 0x0002E40F File Offset: 0x0002C60F
			internal XmlElementParser Parser { get; private set; }

			// Token: 0x170004F0 RID: 1264
			// (get) Token: 0x0600117F RID: 4479 RVA: 0x0002E418 File Offset: 0x0002C618
			// (set) Token: 0x06001180 RID: 4480 RVA: 0x0002E420 File Offset: 0x0002C620
			internal XmlElementInfo Element { get; private set; }

			// Token: 0x170004F1 RID: 1265
			// (get) Token: 0x06001181 RID: 4481 RVA: 0x0002E42C File Offset: 0x0002C62C
			internal IList<XmlElementValue> ChildValues
			{
				get
				{
					IList<XmlElementValue> list = this.childValues;
					return list ?? XmlDocumentParser.ElementScope.EmptyValues;
				}
			}

			// Token: 0x06001182 RID: 4482 RVA: 0x0002E44A File Offset: 0x0002C64A
			internal void AddChildValue(XmlElementValue value)
			{
				if (this.childValues == null)
				{
					this.childValues = new List<XmlElementValue>();
				}
				this.childValues.Add(value);
			}

			// Token: 0x040008F3 RID: 2291
			private static readonly IList<XmlElementValue> EmptyValues = new ReadOnlyCollection<XmlElementValue>(new XmlElementValue[0]);

			// Token: 0x040008F4 RID: 2292
			private List<XmlElementValue> childValues;
		}
	}
}
