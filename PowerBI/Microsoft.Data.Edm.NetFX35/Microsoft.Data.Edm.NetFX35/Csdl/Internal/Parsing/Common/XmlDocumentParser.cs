using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Common
{
	// Token: 0x0200014F RID: 335
	internal abstract class XmlDocumentParser
	{
		// Token: 0x0600063E RID: 1598 RVA: 0x0000F9DB File Offset: 0x0000DBDB
		protected XmlDocumentParser(XmlReader underlyingReader, string documentPath)
		{
			this.reader = underlyingReader;
			this.docPath = documentPath;
			this.errors = new List<EdmError>();
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0000FA07 File Offset: 0x0000DC07
		internal string DocumentPath
		{
			get
			{
				return this.docPath;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x0000FA0F File Offset: 0x0000DC0F
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x0000FA17 File Offset: 0x0000DC17
		internal string DocumentNamespace { get; private set; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x0000FA20 File Offset: 0x0000DC20
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x0000FA28 File Offset: 0x0000DC28
		internal Version DocumentVersion { get; private set; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x0000FA31 File Offset: 0x0000DC31
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x0000FA39 File Offset: 0x0000DC39
		internal CsdlLocation DocumentElementLocation { get; private set; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x0000FA42 File Offset: 0x0000DC42
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x0000FA4A File Offset: 0x0000DC4A
		internal bool HasErrors { get; private set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x0000FA53 File Offset: 0x0000DC53
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x0000FA5B File Offset: 0x0000DC5B
		internal XmlElementValue Result { get; private set; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x0000FA64 File Offset: 0x0000DC64
		internal CsdlLocation Location
		{
			get
			{
				if (this.xmlLineInfo != null && this.xmlLineInfo.HasLineInfo())
				{
					return new CsdlLocation(this.xmlLineInfo.LineNumber, this.xmlLineInfo.LinePosition);
				}
				return new CsdlLocation(0, 0);
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0000FA9E File Offset: 0x0000DC9E
		internal IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x0000FAA8 File Offset: 0x0000DCA8
		private bool IsTextNode
		{
			get
			{
				XmlNodeType nodeType = this.reader.NodeType;
				switch (nodeType)
				{
				case 3:
				case 4:
					break;
				default:
					if (nodeType != 14)
					{
						return false;
					}
					break;
				}
				return true;
			}
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0000FADC File Offset: 0x0000DCDC
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

		// Token: 0x0600064E RID: 1614 RVA: 0x0000FC01 File Offset: 0x0000DE01
		protected void ReportError(CsdlLocation errorLocation, EdmErrorCode errorCode, string errorMessage)
		{
			this.errors.Add(new EdmError(errorLocation, errorCode, errorMessage));
			this.HasErrors = true;
		}

		// Token: 0x0600064F RID: 1615
		protected abstract XmlReader InitializeReader(XmlReader inputReader);

		// Token: 0x06000650 RID: 1616
		protected abstract bool TryGetDocumentVersion(string xmlNamespaceName, out Version version, out string[] expectedNamespaces);

		// Token: 0x06000651 RID: 1617
		protected abstract bool TryGetRootElementParser(Version artifactVersion, XmlElementInfo rootElement, out XmlElementParser parser);

		// Token: 0x06000652 RID: 1618 RVA: 0x0000FC1D File Offset: 0x0000DE1D
		protected virtual bool IsOwnedNamespace(string namespaceName)
		{
			return this.DocumentNamespace.EqualsOrdinal(namespaceName);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0000FC2B File Offset: 0x0000DE2B
		protected virtual XmlElementParser<TResult> Element<TResult>(string elementName, Func<XmlElementInfo, XmlElementValueCollection, TResult> parserFunc, params XmlElementParser[] childParsers)
		{
			return XmlElementParser.Create<TResult>(elementName, parserFunc, childParsers, null);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0000FC36 File Offset: 0x0000DE36
		private void Parse()
		{
			while (this.currentBranch.Count > 0 && this.reader.Read())
			{
				this.ProcessNode();
			}
			if (this.reader.EOF)
			{
				return;
			}
			this.reader.Read();
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0000FCA0 File Offset: 0x0000DEA0
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

		// Token: 0x06000656 RID: 1622 RVA: 0x0000FE7C File Offset: 0x0000E07C
		private void BeginElement(XmlElementParser elementParser, XmlElementInfo element)
		{
			XmlDocumentParser.ElementScope elementScope = new XmlDocumentParser.ElementScope(elementParser, element);
			this.currentBranch.Push(elementScope);
			this.currentScope = elementScope;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0000FEA4 File Offset: 0x0000E0A4
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

		// Token: 0x06000658 RID: 1624 RVA: 0x0000FFCC File Offset: 0x0000E1CC
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
					this.ReportUnexpectedElement(this.Location, this.reader.Name);
					if (!isEmptyElement)
					{
						this.reader.Read();
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
					this.ReportUnexpectedElement(this.Location, this.reader.Name);
					this.reader.Skip();
					return;
				}
				XmlReader xmlReader = this.reader.ReadSubtree();
				xmlReader.MoveToContent();
				string text = xmlReader.ReadOuterXml();
				this.currentScope.Element.AddAnnotation(new XmlAnnotationInfo(this.Location, namespaceURI, localName, text, false));
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x000100DC File Offset: 0x0000E2DC
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

		// Token: 0x0600065A RID: 1626 RVA: 0x000101CC File Offset: 0x0000E3CC
		private void ReportEmptyFile()
		{
			string text = ((this.DocumentPath == null) ? Strings.XmlParser_EmptySchemaTextReader : Strings.XmlParser_EmptyFile(this.DocumentPath));
			this.ReportError(this.Location, EdmErrorCode.EmptyFile, text);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00010204 File Offset: 0x0000E404
		private void ReportUnexpectedRootNamespace(string elementName, string namespaceUri, string[] expectedNamespaces)
		{
			string text = string.Join(", ", expectedNamespaces);
			string text2 = (string.IsNullOrEmpty(namespaceUri) ? Strings.XmlParser_UnexpectedRootElementNoNamespace(text) : Strings.XmlParser_UnexpectedRootElementWrongNamespace(namespaceUri, text));
			this.ReportError(this.Location, EdmErrorCode.UnexpectedXmlElement, text2);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00010244 File Offset: 0x0000E444
		private void ReportUnexpectedRootElement(CsdlLocation elementLocation, string elementName, string expectedNamespace)
		{
			this.ReportError(elementLocation, EdmErrorCode.UnexpectedXmlElement, Strings.XmlParser_UnexpectedRootElement(elementName, "Schema"));
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001025A File Offset: 0x0000E45A
		private void ReportUnexpectedAttribute(CsdlLocation errorLocation, string attributeName)
		{
			this.ReportError(errorLocation, EdmErrorCode.UnexpectedXmlAttribute, Strings.XmlParser_UnexpectedAttribute(attributeName));
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001026B File Offset: 0x0000E46B
		private void ReportUnexpectedNodeType(XmlNodeType nodeType)
		{
			this.ReportError(this.Location, EdmErrorCode.UnexpectedXmlNodeType, Strings.XmlParser_UnexpectedNodeType(nodeType));
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00010285 File Offset: 0x0000E485
		private void ReportUnexpectedElement(CsdlLocation errorLocation, string elementName)
		{
			this.ReportError(errorLocation, EdmErrorCode.UnexpectedXmlElement, Strings.XmlParser_UnexpectedElement(elementName));
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00010296 File Offset: 0x0000E496
		private void ReportUnusedElement(CsdlLocation errorLocation, string elementName)
		{
			this.ReportError(errorLocation, EdmErrorCode.UnexpectedXmlElement, Strings.XmlParser_UnusedElement(elementName));
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x000102A7 File Offset: 0x0000E4A7
		private void ReportTextNotAllowed(CsdlLocation errorLocation, string textValue)
		{
			this.ReportError(errorLocation, EdmErrorCode.TextNotAllowed, Strings.XmlParser_TextNotAllowed(textValue));
		}

		// Token: 0x0400035B RID: 859
		private readonly string docPath;

		// Token: 0x0400035C RID: 860
		private readonly Stack<XmlDocumentParser.ElementScope> currentBranch = new Stack<XmlDocumentParser.ElementScope>();

		// Token: 0x0400035D RID: 861
		private XmlReader reader;

		// Token: 0x0400035E RID: 862
		private IXmlLineInfo xmlLineInfo;

		// Token: 0x0400035F RID: 863
		private List<EdmError> errors;

		// Token: 0x04000360 RID: 864
		private StringBuilder currentText;

		// Token: 0x04000361 RID: 865
		private CsdlLocation currentTextLocation;

		// Token: 0x04000362 RID: 866
		private XmlDocumentParser.ElementScope currentScope;

		// Token: 0x02000150 RID: 336
		private class ElementScope
		{
			// Token: 0x06000665 RID: 1637 RVA: 0x000102B8 File Offset: 0x0000E4B8
			internal ElementScope(XmlElementParser parser, XmlElementInfo element)
			{
				this.Parser = parser;
				this.Element = element;
			}

			// Token: 0x170002B5 RID: 693
			// (get) Token: 0x06000666 RID: 1638 RVA: 0x000102CE File Offset: 0x0000E4CE
			// (set) Token: 0x06000667 RID: 1639 RVA: 0x000102D6 File Offset: 0x0000E4D6
			internal XmlElementParser Parser { get; private set; }

			// Token: 0x170002B6 RID: 694
			// (get) Token: 0x06000668 RID: 1640 RVA: 0x000102DF File Offset: 0x0000E4DF
			// (set) Token: 0x06000669 RID: 1641 RVA: 0x000102E7 File Offset: 0x0000E4E7
			internal XmlElementInfo Element { get; private set; }

			// Token: 0x170002B7 RID: 695
			// (get) Token: 0x0600066A RID: 1642 RVA: 0x000102F0 File Offset: 0x0000E4F0
			internal IList<XmlElementValue> ChildValues
			{
				get
				{
					return this.childValues ?? XmlDocumentParser.ElementScope.EmptyValues;
				}
			}

			// Token: 0x0600066B RID: 1643 RVA: 0x00010301 File Offset: 0x0000E501
			internal void AddChildValue(XmlElementValue value)
			{
				if (this.childValues == null)
				{
					this.childValues = new List<XmlElementValue>();
				}
				this.childValues.Add(value);
			}

			// Token: 0x0400036B RID: 875
			private static readonly IList<XmlElementValue> EmptyValues = new ReadOnlyCollection<XmlElementValue>(new XmlElementValue[0]);

			// Token: 0x0400036C RID: 876
			private List<XmlElementValue> childValues;
		}
	}
}
