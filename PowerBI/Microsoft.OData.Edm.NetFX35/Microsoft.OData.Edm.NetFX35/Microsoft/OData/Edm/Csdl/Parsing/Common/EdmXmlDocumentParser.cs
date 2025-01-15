using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x02000193 RID: 403
	internal abstract class EdmXmlDocumentParser<TResult> : XmlDocumentParser<TResult>
	{
		// Token: 0x0600079B RID: 1947 RVA: 0x0001261E File Offset: 0x0001081E
		internal EdmXmlDocumentParser(string artifactLocation, XmlReader reader)
			: base(reader, artifactLocation)
		{
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x0600079C RID: 1948
		internal abstract IEnumerable<KeyValuePair<Version, string>> SupportedVersions { get; }

		// Token: 0x0600079D RID: 1949 RVA: 0x00012633 File Offset: 0x00010833
		internal XmlAttributeInfo GetOptionalAttribute(XmlElementInfo element, string attributeName)
		{
			return element.Attributes[attributeName];
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00012644 File Offset: 0x00010844
		internal XmlAttributeInfo GetRequiredAttribute(XmlElementInfo element, string attributeName)
		{
			XmlAttributeInfo xmlAttributeInfo = element.Attributes[attributeName];
			if (xmlAttributeInfo.IsMissing)
			{
				base.ReportError(element.Location, EdmErrorCode.MissingAttribute, Strings.XmlParser_MissingAttribute(attributeName, element.Name));
				return xmlAttributeInfo;
			}
			return xmlAttributeInfo;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00012684 File Offset: 0x00010884
		protected override XmlReader InitializeReader(XmlReader reader)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings
			{
				CheckCharacters = true,
				CloseInput = false,
				IgnoreWhitespace = true,
				ConformanceLevel = 0,
				IgnoreComments = true,
				IgnoreProcessingInstructions = true
			};
			return XmlReader.Create(reader, xmlReaderSettings);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x000126F8 File Offset: 0x000108F8
		protected override bool TryGetDocumentVersion(string xmlNamespaceName, out Version version, out string[] expectedNamespaces)
		{
			expectedNamespaces = Enumerable.ToArray<string>(Enumerable.Select<KeyValuePair<Version, string>, string>(this.SupportedVersions, (KeyValuePair<Version, string> v) => v.Value));
			version = Enumerable.FirstOrDefault<Version>(Enumerable.Select<KeyValuePair<Version, string>, Version>(Enumerable.Where<KeyValuePair<Version, string>>(this.SupportedVersions, (KeyValuePair<Version, string> v) => v.Value == xmlNamespaceName), (KeyValuePair<Version, string> v) => v.Key));
			return version != null;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00012789 File Offset: 0x00010989
		protected override bool IsOwnedNamespace(string namespaceName)
		{
			return this.IsEdmNamespace(namespaceName);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x000127E4 File Offset: 0x000109E4
		protected XmlElementParser<TItem> CsdlElement<TItem>(string elementName, Func<XmlElementInfo, XmlElementValueCollection, TItem> initializer, params XmlElementParser[] childParsers) where TItem : class
		{
			return this.Element<TItem>(elementName, delegate(XmlElementInfo element, XmlElementValueCollection childValues)
			{
				this.BeginItem(element);
				TItem titem = initializer.Invoke(element, childValues);
				this.AnnotateItem(titem, childValues);
				this.EndItem();
				return titem;
			}, childParsers);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00012819 File Offset: 0x00010A19
		protected void BeginItem(XmlElementInfo element)
		{
			this.elementStack.Push(element);
			this.currentElement = element;
		}

		// Token: 0x060007A4 RID: 1956
		protected abstract void AnnotateItem(object result, XmlElementValueCollection childValues);

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001282E File Offset: 0x00010A2E
		protected void EndItem()
		{
			this.elementStack.Pop();
			this.currentElement = ((this.elementStack.Count == 0) ? null : this.elementStack.Peek());
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00012860 File Offset: 0x00010A60
		protected int? OptionalInteger(string attributeName)
		{
			XmlAttributeInfo optionalAttribute = this.GetOptionalAttribute(this.currentElement, attributeName);
			if (!optionalAttribute.IsMissing)
			{
				int? num;
				if (!EdmValueParser.TryParseInt(optionalAttribute.Value, out num))
				{
					base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidInteger, Strings.ValueParser_InvalidInteger(optionalAttribute.Value));
				}
				return num;
			}
			return default(int?);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x000128C0 File Offset: 0x00010AC0
		protected long? OptionalLong(string attributeName)
		{
			XmlAttributeInfo optionalAttribute = this.GetOptionalAttribute(this.currentElement, attributeName);
			if (!optionalAttribute.IsMissing)
			{
				long? num;
				if (!EdmValueParser.TryParseLong(optionalAttribute.Value, out num))
				{
					base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidLong, Strings.ValueParser_InvalidLong(optionalAttribute.Value));
				}
				return num;
			}
			return default(long?);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00012920 File Offset: 0x00010B20
		protected int? OptionalSrid(string attributeName, int defaultSrid)
		{
			XmlAttributeInfo optionalAttribute = this.GetOptionalAttribute(this.currentElement, attributeName);
			if (!optionalAttribute.IsMissing)
			{
				int? num;
				if (optionalAttribute.Value.EqualsOrdinalIgnoreCase("Variable"))
				{
					num = default(int?);
				}
				else if (!EdmValueParser.TryParseInt(optionalAttribute.Value, out num))
				{
					base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidSrid, Strings.ValueParser_InvalidSrid(optionalAttribute.Value));
				}
				return num;
			}
			return new int?(defaultSrid);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00012998 File Offset: 0x00010B98
		protected int? OptionalScale(string attributeName)
		{
			XmlAttributeInfo optionalAttribute = this.GetOptionalAttribute(this.currentElement, attributeName);
			if (!optionalAttribute.IsMissing)
			{
				int? num;
				if (optionalAttribute.Value.EqualsOrdinalIgnoreCase("Variable"))
				{
					num = default(int?);
				}
				else if (!EdmValueParser.TryParseInt(optionalAttribute.Value, out num))
				{
					base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidSrid, Strings.ValueParser_InvalidScale(optionalAttribute.Value));
				}
				return num;
			}
			return new int?(0);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00012A10 File Offset: 0x00010C10
		protected int? OptionalMaxLength(string attributeName)
		{
			XmlAttributeInfo optionalAttribute = this.GetOptionalAttribute(this.currentElement, attributeName);
			if (!optionalAttribute.IsMissing)
			{
				int? num;
				if (!EdmValueParser.TryParseInt(optionalAttribute.Value, out num))
				{
					base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidMaxLength, Strings.ValueParser_InvalidMaxLength(optionalAttribute.Value));
				}
				return num;
			}
			return default(int?);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00012A70 File Offset: 0x00010C70
		protected EdmConcurrencyMode? OptionalConcurrencyMode(string attributeName)
		{
			XmlAttributeInfo optionalAttribute = this.GetOptionalAttribute(this.currentElement, attributeName);
			if (!optionalAttribute.IsMissing)
			{
				string value;
				if ((value = optionalAttribute.Value) != null)
				{
					if (value == "None")
					{
						return new EdmConcurrencyMode?(EdmConcurrencyMode.None);
					}
					if (value == "Fixed")
					{
						return new EdmConcurrencyMode?(EdmConcurrencyMode.Fixed);
					}
				}
				base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidConcurrencyMode, Strings.CsdlParser_InvalidConcurrencyMode(optionalAttribute.Value));
			}
			return default(EdmConcurrencyMode?);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00012AF4 File Offset: 0x00010CF4
		protected EdmMultiplicity RequiredMultiplicity(string attributeName)
		{
			XmlAttributeInfo requiredAttribute = this.GetRequiredAttribute(this.currentElement, attributeName);
			if (!requiredAttribute.IsMissing)
			{
				string value;
				if ((value = requiredAttribute.Value) != null)
				{
					if (value == "1")
					{
						return EdmMultiplicity.One;
					}
					if (value == "0..1")
					{
						return EdmMultiplicity.ZeroOrOne;
					}
					if (value == "*")
					{
						return EdmMultiplicity.Many;
					}
				}
				base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidMultiplicity, Strings.CsdlParser_InvalidMultiplicity(requiredAttribute.Value));
			}
			return EdmMultiplicity.One;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00012B70 File Offset: 0x00010D70
		protected EdmOnDeleteAction RequiredOnDeleteAction(string attributeName)
		{
			XmlAttributeInfo requiredAttribute = this.GetRequiredAttribute(this.currentElement, attributeName);
			if (!requiredAttribute.IsMissing)
			{
				string value;
				if ((value = requiredAttribute.Value) != null)
				{
					if (value == "None")
					{
						return EdmOnDeleteAction.None;
					}
					if (value == "Cascade")
					{
						return EdmOnDeleteAction.Cascade;
					}
				}
				base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidOnDelete, Strings.CsdlParser_InvalidDeleteAction(requiredAttribute.Value));
			}
			return EdmOnDeleteAction.None;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00012BDC File Offset: 0x00010DDC
		protected bool? OptionalBoolean(string attributeName)
		{
			XmlAttributeInfo optionalAttribute = this.GetOptionalAttribute(this.currentElement, attributeName);
			if (!optionalAttribute.IsMissing)
			{
				bool? flag;
				if (!EdmValueParser.TryParseBool(optionalAttribute.Value, out flag))
				{
					base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidBoolean, Strings.ValueParser_InvalidBoolean(optionalAttribute.Value));
				}
				return flag;
			}
			return default(bool?);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00012C38 File Offset: 0x00010E38
		protected string Optional(string attributeName)
		{
			XmlAttributeInfo optionalAttribute = this.GetOptionalAttribute(this.currentElement, attributeName);
			if (optionalAttribute.IsMissing)
			{
				return null;
			}
			return optionalAttribute.Value;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00012C64 File Offset: 0x00010E64
		protected string Required(string attributeName)
		{
			XmlAttributeInfo requiredAttribute = this.GetRequiredAttribute(this.currentElement, attributeName);
			if (requiredAttribute.IsMissing)
			{
				return string.Empty;
			}
			return requiredAttribute.Value;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00012C94 File Offset: 0x00010E94
		protected string OptionalAlias(string attributeName)
		{
			XmlAttributeInfo optionalAttribute = this.GetOptionalAttribute(this.currentElement, attributeName);
			if (!optionalAttribute.IsMissing)
			{
				return this.ValidateAlias(optionalAttribute.Value);
			}
			return null;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00012CC8 File Offset: 0x00010EC8
		protected string RequiredAlias(string attributeName)
		{
			XmlAttributeInfo requiredAttribute = this.GetRequiredAttribute(this.currentElement, attributeName);
			if (!requiredAttribute.IsMissing)
			{
				return this.ValidateAlias(requiredAttribute.Value);
			}
			return null;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00012CFC File Offset: 0x00010EFC
		protected string RequiredEntitySetPath(string attributeName)
		{
			XmlAttributeInfo requiredAttribute = this.GetRequiredAttribute(this.currentElement, attributeName);
			if (!requiredAttribute.IsMissing)
			{
				return this.ValidateEntitySetPath(requiredAttribute.Value);
			}
			return null;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00012D30 File Offset: 0x00010F30
		protected string RequiredEnumMemberPath(string attributeName)
		{
			XmlAttributeInfo requiredAttribute = this.GetRequiredAttribute(this.currentElement, attributeName);
			if (!requiredAttribute.IsMissing)
			{
				return this.ValidateEnumMemberPath(requiredAttribute.Value);
			}
			return null;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00012D64 File Offset: 0x00010F64
		protected string RequiredEnumMemberPath(XmlTextValue text)
		{
			string text2 = ((text != null) ? text.TextValue : string.Empty);
			return this.ValidateEnumMembersPath(text2);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00012D8C File Offset: 0x00010F8C
		protected string OptionalType(string attributeName)
		{
			XmlAttributeInfo optionalAttribute = this.GetOptionalAttribute(this.currentElement, attributeName);
			if (!optionalAttribute.IsMissing)
			{
				return this.ValidateTypeName(optionalAttribute.Value);
			}
			return null;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00012DC0 File Offset: 0x00010FC0
		protected string RequiredType(string attributeName)
		{
			XmlAttributeInfo requiredAttribute = this.GetRequiredAttribute(this.currentElement, attributeName);
			if (!requiredAttribute.IsMissing)
			{
				return this.ValidateTypeName(requiredAttribute.Value);
			}
			return null;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00012DF4 File Offset: 0x00010FF4
		protected string OptionalQualifiedName(string attributeName)
		{
			XmlAttributeInfo optionalAttribute = this.GetOptionalAttribute(this.currentElement, attributeName);
			if (!optionalAttribute.IsMissing)
			{
				return this.ValidateQualifiedName(optionalAttribute.Value);
			}
			return null;
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00012E28 File Offset: 0x00011028
		protected string RequiredQualifiedName(string attributeName)
		{
			XmlAttributeInfo requiredAttribute = this.GetRequiredAttribute(this.currentElement, attributeName);
			if (!requiredAttribute.IsMissing)
			{
				return this.ValidateQualifiedName(requiredAttribute.Value);
			}
			return null;
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00012E64 File Offset: 0x00011064
		protected string ValidateEnumMembersPath(string path)
		{
			if (string.IsNullOrEmpty(path.Trim()))
			{
				base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidEnumMemberPath, Strings.CsdlParser_InvalidEnumMemberPath(path));
			}
			string[] array = Enumerable.ToArray<string>(Enumerable.Where<string>(path.Split(new char[] { ' ' }), (string s) => !string.IsNullOrEmpty(s)));
			string text = null;
			foreach (string text2 in array)
			{
				string[] array3 = text2.Split(new char[] { '/' });
				if (Enumerable.Count<string>(array3) != 2 || !EdmUtil.IsValidDottedName(array3[0]) || !EdmUtil.IsValidUndottedName(array3[1]))
				{
					base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidEnumMemberPath, Strings.CsdlParser_InvalidEnumMemberPath(path));
				}
				if (text != null && array3[0] != text)
				{
					base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidEnumMemberPath, Strings.CsdlParser_InvalidEnumMemberPath(path));
				}
				text = array3[0];
			}
			return string.Join(" ", array);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00012F88 File Offset: 0x00011188
		private string ValidateTypeName(string name)
		{
			string[] array = name.Split(new char[] { '(', ')' });
			string text = array[0];
			string text2;
			if ((text2 = text) != null)
			{
				if (!(text2 == "Collection"))
				{
					if (text2 == "Ref")
					{
						if (Enumerable.Count<string>(array) == 1)
						{
							base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidTypeName, Strings.CsdlParser_InvalidTypeName(name));
							return name;
						}
						text = array[1];
					}
				}
				else
				{
					if (Enumerable.Count<string>(array) == 1)
					{
						return name;
					}
					text = array[1];
				}
			}
			if (EdmUtil.IsQualifiedName(text) || EdmCoreModel.Instance.GetPrimitiveTypeKind(text) != EdmPrimitiveTypeKind.None)
			{
				return name;
			}
			base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidTypeName, Strings.CsdlParser_InvalidTypeName(name));
			return name;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00013043 File Offset: 0x00011243
		private string ValidateAlias(string name)
		{
			if (!EdmUtil.IsValidUndottedName(name))
			{
				base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidQualifiedName, Strings.CsdlParser_InvalidAlias(name));
			}
			return name;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001306C File Offset: 0x0001126C
		private string ValidateEntitySetPath(string path)
		{
			string[] array = path.Split(new char[] { '/' });
			if (Enumerable.Count<string>(array) != 2 || !EdmUtil.IsValidDottedName(array[0]) || !EdmUtil.IsValidUndottedName(array[1]))
			{
				base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidEntitySetPath, Strings.CsdlParser_InvalidEntitySetPath(path));
			}
			return path;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x000130C8 File Offset: 0x000112C8
		private string ValidateEnumMemberPath(string path)
		{
			string[] array = path.Split(new char[] { '/' });
			if (Enumerable.Count<string>(array) != 2 || !EdmUtil.IsValidDottedName(array[0]) || !EdmUtil.IsValidUndottedName(array[1]))
			{
				base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidEnumMemberPath, Strings.CsdlParser_InvalidEnumMemberPath(path));
			}
			return path;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00013123 File Offset: 0x00011323
		private string ValidateQualifiedName(string qualifiedName)
		{
			if (!EdmUtil.IsQualifiedName(qualifiedName))
			{
				base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidQualifiedName, Strings.CsdlParser_InvalidQualifiedName(qualifiedName));
			}
			return qualifiedName;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001314C File Offset: 0x0001134C
		private bool IsEdmNamespace(string xmlNamespaceUri)
		{
			if (this.edmNamespaces == null)
			{
				this.edmNamespaces = new HashSetInternal<string>();
				foreach (string[] array in CsdlConstants.SupportedVersions.Values)
				{
					foreach (string text in array)
					{
						this.edmNamespaces.Add(text);
					}
				}
			}
			return this.edmNamespaces.Contains(xmlNamespaceUri);
		}

		// Token: 0x040003FE RID: 1022
		protected XmlElementInfo currentElement;

		// Token: 0x040003FF RID: 1023
		private readonly Stack<XmlElementInfo> elementStack = new Stack<XmlElementInfo>();

		// Token: 0x04000400 RID: 1024
		private HashSetInternal<string> edmNamespaces;
	}
}
