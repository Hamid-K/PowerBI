using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x02000190 RID: 400
	internal abstract class XmlDocumentParser
	{
		// Token: 0x06000768 RID: 1896 RVA: 0x00011C51 File Offset: 0x0000FE51
		protected XmlDocumentParser(XmlReader underlyingReader, string documentPath)
		{
			this.reader = underlyingReader;
			this.docPath = documentPath;
			this.errors = new List<EdmError>();
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x00011C7D File Offset: 0x0000FE7D
		internal string DocumentPath
		{
			get
			{
				return this.docPath;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x00011C85 File Offset: 0x0000FE85
		// (set) Token: 0x0600076B RID: 1899 RVA: 0x00011C8D File Offset: 0x0000FE8D
		internal string DocumentNamespace { get; private set; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x00011C96 File Offset: 0x0000FE96
		// (set) Token: 0x0600076D RID: 1901 RVA: 0x00011C9E File Offset: 0x0000FE9E
		internal Version DocumentVersion { get; private set; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x00011CA7 File Offset: 0x0000FEA7
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x00011CAF File Offset: 0x0000FEAF
		internal CsdlLocation DocumentElementLocation { get; private set; }

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x00011CB8 File Offset: 0x0000FEB8
		// (set) Token: 0x06000771 RID: 1905 RVA: 0x00011CC0 File Offset: 0x0000FEC0
		internal bool HasErrors { get; private set; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x00011CC9 File Offset: 0x0000FEC9
		// (set) Token: 0x06000773 RID: 1907 RVA: 0x00011CD1 File Offset: 0x0000FED1
		internal XmlElementValue Result { get; private set; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x00011CDC File Offset: 0x0000FEDC
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

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x00011D27 File Offset: 0x0000FF27
		internal IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x00011D30 File Offset: 0x0000FF30
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

		// Token: 0x06000777 RID: 1911 RVA: 0x00011D64 File Offset: 0x0000FF64
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

		// Token: 0x06000778 RID: 1912 RVA: 0x00011E89 File Offset: 0x00010089
		protected void ReportError(CsdlLocation errorLocation, EdmErrorCode errorCode, string errorMessage)
		{
			this.errors.Add(new EdmError(errorLocation, errorCode, errorMessage));
			this.HasErrors = true;
		}

		// Token: 0x06000779 RID: 1913
		protected abstract XmlReader InitializeReader(XmlReader inputReader);

		// Token: 0x0600077A RID: 1914
		protected abstract bool TryGetDocumentVersion(string xmlNamespaceName, out Version version, out string[] expectedNamespaces);

		// Token: 0x0600077B RID: 1915
		protected abstract bool TryGetRootElementParser(Version artifactVersion, XmlElementInfo rootElement, out XmlElementParser parser);

		// Token: 0x0600077C RID: 1916 RVA: 0x00011EA5 File Offset: 0x000100A5
		protected virtual bool IsOwnedNamespace(string namespaceName)
		{
			return this.DocumentNamespace.EqualsOrdinal(namespaceName);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00011EB3 File Offset: 0x000100B3
		protected virtual XmlElementParser<TResult> Element<TResult>(string elementName, Func<XmlElementInfo, XmlElementValueCollection, TResult> parserFunc, params XmlElementParser[] childParsers)
		{
			return XmlElementParser.Create<TResult>(elementName, parserFunc, childParsers, null);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00011EBE File Offset: 0x000100BE
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

		// Token: 0x0600077F RID: 1919 RVA: 0x00011F28 File Offset: 0x00010128
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

		// Token: 0x06000780 RID: 1920 RVA: 0x00012104 File Offset: 0x00010304
		private void BeginElement(XmlElementParser elementParser, XmlElementInfo element)
		{
			XmlDocumentParser.ElementScope elementScope = new XmlDocumentParser.ElementScope(elementParser, element);
			this.currentBranch.Push(elementScope);
			this.currentScope = elementScope;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001212C File Offset: 0x0001032C
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

		// Token: 0x06000782 RID: 1922 RVA: 0x00012254 File Offset: 0x00010454
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

		// Token: 0x06000783 RID: 1923 RVA: 0x00012380 File Offset: 0x00010580
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

		// Token: 0x06000784 RID: 1924 RVA: 0x00012470 File Offset: 0x00010670
		private void ReportEmptyFile()
		{
			string text = ((this.DocumentPath == null) ? Strings.XmlParser_EmptySchemaTextReader : Strings.XmlParser_EmptyFile(this.DocumentPath));
			this.ReportError(this.Location, EdmErrorCode.EmptyFile, text);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x000124A8 File Offset: 0x000106A8
		private void ReportUnexpectedRootNamespace(string elementName, string namespaceUri, string[] expectedNamespaces)
		{
			string text = string.Join(", ", expectedNamespaces);
			string text2 = (string.IsNullOrEmpty(namespaceUri) ? Strings.XmlParser_UnexpectedRootElementNoNamespace(text) : Strings.XmlParser_UnexpectedRootElementWrongNamespace(namespaceUri, text));
			this.ReportError(this.Location, EdmErrorCode.UnexpectedXmlElement, text2);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x000124E8 File Offset: 0x000106E8
		private void ReportUnexpectedRootElement(CsdlLocation elementLocation, string elementName, string expectedNamespace)
		{
			this.ReportError(elementLocation, EdmErrorCode.UnexpectedXmlElement, Strings.XmlParser_UnexpectedRootElement(elementName, "Schema"));
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x000124FE File Offset: 0x000106FE
		private void ReportUnexpectedAttribute(CsdlLocation errorLocation, string attributeName)
		{
			this.ReportError(errorLocation, EdmErrorCode.UnexpectedXmlAttribute, Strings.XmlParser_UnexpectedAttribute(attributeName));
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001250F File Offset: 0x0001070F
		private void ReportUnexpectedNodeType(XmlNodeType nodeType)
		{
			this.ReportError(this.Location, EdmErrorCode.UnexpectedXmlNodeType, Strings.XmlParser_UnexpectedNodeType(nodeType));
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00012529 File Offset: 0x00010729
		private void ReportUnexpectedElement(CsdlLocation errorLocation, string elementName)
		{
			this.ReportError(errorLocation, EdmErrorCode.UnexpectedXmlElement, Strings.XmlParser_UnexpectedElement(elementName));
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001253A File Offset: 0x0001073A
		private void ReportUnusedElement(CsdlLocation errorLocation, string elementName)
		{
			this.ReportError(errorLocation, EdmErrorCode.UnexpectedXmlElement, Strings.XmlParser_UnusedElement(elementName));
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001254B File Offset: 0x0001074B
		private void ReportTextNotAllowed(CsdlLocation errorLocation, string textValue)
		{
			this.ReportError(errorLocation, EdmErrorCode.TextNotAllowed, Strings.XmlParser_TextNotAllowed(textValue));
		}

		// Token: 0x040003EA RID: 1002
		private readonly string docPath;

		// Token: 0x040003EB RID: 1003
		private readonly Stack<XmlDocumentParser.ElementScope> currentBranch = new Stack<XmlDocumentParser.ElementScope>();

		// Token: 0x040003EC RID: 1004
		private XmlReader reader;

		// Token: 0x040003ED RID: 1005
		private IXmlLineInfo xmlLineInfo;

		// Token: 0x040003EE RID: 1006
		private List<EdmError> errors;

		// Token: 0x040003EF RID: 1007
		private StringBuilder currentText;

		// Token: 0x040003F0 RID: 1008
		private CsdlLocation currentTextLocation;

		// Token: 0x040003F1 RID: 1009
		private XmlDocumentParser.ElementScope currentScope;

		// Token: 0x02000191 RID: 401
		private class ElementScope
		{
			// Token: 0x0600078F RID: 1935 RVA: 0x0001255C File Offset: 0x0001075C
			internal ElementScope(XmlElementParser parser, XmlElementInfo element)
			{
				this.Parser = parser;
				this.Element = element;
			}

			// Token: 0x1700032B RID: 811
			// (get) Token: 0x06000790 RID: 1936 RVA: 0x00012572 File Offset: 0x00010772
			// (set) Token: 0x06000791 RID: 1937 RVA: 0x0001257A File Offset: 0x0001077A
			internal XmlElementParser Parser { get; private set; }

			// Token: 0x1700032C RID: 812
			// (get) Token: 0x06000792 RID: 1938 RVA: 0x00012583 File Offset: 0x00010783
			// (set) Token: 0x06000793 RID: 1939 RVA: 0x0001258B File Offset: 0x0001078B
			internal XmlElementInfo Element { get; private set; }

			// Token: 0x1700032D RID: 813
			// (get) Token: 0x06000794 RID: 1940 RVA: 0x00012594 File Offset: 0x00010794
			internal IList<XmlElementValue> ChildValues
			{
				get
				{
					return this.childValues ?? XmlDocumentParser.ElementScope.EmptyValues;
				}
			}

			// Token: 0x06000795 RID: 1941 RVA: 0x000125A5 File Offset: 0x000107A5
			internal void AddChildValue(XmlElementValue value)
			{
				if (this.childValues == null)
				{
					this.childValues = new List<XmlElementValue>();
				}
				this.childValues.Add(value);
			}

			// Token: 0x040003FA RID: 1018
			private static readonly IList<XmlElementValue> EmptyValues = new ReadOnlyCollection<XmlElementValue>(new XmlElementValue[0]);

			// Token: 0x040003FB RID: 1019
			private List<XmlElementValue> childValues;
		}
	}
}
