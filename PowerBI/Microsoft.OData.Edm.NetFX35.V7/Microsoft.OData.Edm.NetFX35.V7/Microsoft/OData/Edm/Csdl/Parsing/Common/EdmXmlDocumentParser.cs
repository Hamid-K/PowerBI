using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001AD RID: 429
	internal abstract class EdmXmlDocumentParser<TResult> : XmlDocumentParser<TResult>
	{
		// Token: 0x06000BD1 RID: 3025 RVA: 0x00021ED7 File Offset: 0x000200D7
		internal EdmXmlDocumentParser(string artifactLocation, XmlReader reader)
			: base(reader, artifactLocation)
		{
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000BD2 RID: 3026
		internal abstract IEnumerable<KeyValuePair<Version, string>> SupportedVersions { get; }

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00021EEC File Offset: 0x000200EC
		internal static XmlAttributeInfo GetOptionalAttribute(XmlElementInfo element, string attributeName)
		{
			return element.Attributes[attributeName];
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00021EFC File Offset: 0x000200FC
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

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00021F3C File Offset: 0x0002013C
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

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00021F80 File Offset: 0x00020180
		protected override bool TryGetDocumentVersion(string xmlNamespaceName, out Version version, out string[] expectedNamespaces)
		{
			expectedNamespaces = Enumerable.ToArray<string>(Enumerable.Select<KeyValuePair<Version, string>, string>(this.SupportedVersions, (KeyValuePair<Version, string> v) => v.Value));
			version = Enumerable.FirstOrDefault<Version>(Enumerable.Select<KeyValuePair<Version, string>, Version>(Enumerable.Where<KeyValuePair<Version, string>>(this.SupportedVersions, (KeyValuePair<Version, string> v) => v.Value == xmlNamespaceName), (KeyValuePair<Version, string> v) => v.Key));
			return version != null;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00022015 File Offset: 0x00020215
		protected override bool IsOwnedNamespace(string namespaceName)
		{
			return this.IsEdmNamespace(namespaceName);
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00022020 File Offset: 0x00020220
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

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00022055 File Offset: 0x00020255
		protected void BeginItem(XmlElementInfo element)
		{
			this.elementStack.Push(element);
			this.currentElement = element;
		}

		// Token: 0x06000BDA RID: 3034
		protected abstract void AnnotateItem(object result, XmlElementValueCollection childValues);

		// Token: 0x06000BDB RID: 3035 RVA: 0x0002206A File Offset: 0x0002026A
		protected void EndItem()
		{
			this.elementStack.Pop();
			this.currentElement = ((this.elementStack.Count == 0) ? null : this.elementStack.Peek());
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0002209C File Offset: 0x0002029C
		protected int? OptionalInteger(string attributeName)
		{
			XmlAttributeInfo optionalAttribute = EdmXmlDocumentParser<TResult>.GetOptionalAttribute(this.currentElement, attributeName);
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

		// Token: 0x06000BDD RID: 3037 RVA: 0x000220FC File Offset: 0x000202FC
		protected long? OptionalLong(string attributeName)
		{
			XmlAttributeInfo optionalAttribute = EdmXmlDocumentParser<TResult>.GetOptionalAttribute(this.currentElement, attributeName);
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

		// Token: 0x06000BDE RID: 3038 RVA: 0x0002215C File Offset: 0x0002035C
		protected int? OptionalSrid(string attributeName, int defaultSrid)
		{
			XmlAttributeInfo optionalAttribute = EdmXmlDocumentParser<TResult>.GetOptionalAttribute(this.currentElement, attributeName);
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

		// Token: 0x06000BDF RID: 3039 RVA: 0x000221D4 File Offset: 0x000203D4
		protected int? OptionalScale(string attributeName)
		{
			XmlAttributeInfo optionalAttribute = EdmXmlDocumentParser<TResult>.GetOptionalAttribute(this.currentElement, attributeName);
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

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0002224C File Offset: 0x0002044C
		protected int? OptionalMaxLength(string attributeName)
		{
			XmlAttributeInfo optionalAttribute = EdmXmlDocumentParser<TResult>.GetOptionalAttribute(this.currentElement, attributeName);
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

		// Token: 0x06000BE1 RID: 3041 RVA: 0x000222AC File Offset: 0x000204AC
		protected EdmMultiplicity RequiredMultiplicity(string attributeName)
		{
			XmlAttributeInfo requiredAttribute = this.GetRequiredAttribute(this.currentElement, attributeName);
			if (!requiredAttribute.IsMissing)
			{
				string value = requiredAttribute.Value;
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
				base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidMultiplicity, Strings.CsdlParser_InvalidMultiplicity(requiredAttribute.Value));
			}
			return EdmMultiplicity.One;
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00022324 File Offset: 0x00020524
		protected EdmOnDeleteAction RequiredOnDeleteAction(string attributeName)
		{
			XmlAttributeInfo requiredAttribute = this.GetRequiredAttribute(this.currentElement, attributeName);
			if (!requiredAttribute.IsMissing)
			{
				string value = requiredAttribute.Value;
				if (value == "None")
				{
					return EdmOnDeleteAction.None;
				}
				if (value == "Cascade")
				{
					return EdmOnDeleteAction.Cascade;
				}
				base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidOnDelete, Strings.CsdlParser_InvalidDeleteAction(requiredAttribute.Value));
			}
			return EdmOnDeleteAction.None;
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00022390 File Offset: 0x00020590
		protected bool? OptionalBoolean(string attributeName)
		{
			XmlAttributeInfo optionalAttribute = EdmXmlDocumentParser<TResult>.GetOptionalAttribute(this.currentElement, attributeName);
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

		// Token: 0x06000BE4 RID: 3044 RVA: 0x000223EC File Offset: 0x000205EC
		protected string Optional(string attributeName)
		{
			XmlAttributeInfo optionalAttribute = EdmXmlDocumentParser<TResult>.GetOptionalAttribute(this.currentElement, attributeName);
			if (optionalAttribute.IsMissing)
			{
				return null;
			}
			return optionalAttribute.Value;
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00022418 File Offset: 0x00020618
		protected string Required(string attributeName)
		{
			XmlAttributeInfo requiredAttribute = this.GetRequiredAttribute(this.currentElement, attributeName);
			if (requiredAttribute.IsMissing)
			{
				return string.Empty;
			}
			return requiredAttribute.Value;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00022448 File Offset: 0x00020648
		protected string OptionalAlias(string attributeName)
		{
			XmlAttributeInfo optionalAttribute = EdmXmlDocumentParser<TResult>.GetOptionalAttribute(this.currentElement, attributeName);
			if (!optionalAttribute.IsMissing)
			{
				return this.ValidateAlias(optionalAttribute.Value);
			}
			return null;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00022478 File Offset: 0x00020678
		protected string RequiredAlias(string attributeName)
		{
			XmlAttributeInfo requiredAttribute = this.GetRequiredAttribute(this.currentElement, attributeName);
			if (!requiredAttribute.IsMissing)
			{
				return this.ValidateAlias(requiredAttribute.Value);
			}
			return null;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x000224AC File Offset: 0x000206AC
		protected string RequiredEntitySetPath(string attributeName)
		{
			XmlAttributeInfo requiredAttribute = this.GetRequiredAttribute(this.currentElement, attributeName);
			if (!requiredAttribute.IsMissing)
			{
				return this.ValidateEntitySetPath(requiredAttribute.Value);
			}
			return null;
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x000224E0 File Offset: 0x000206E0
		protected string RequiredEnumMemberPath(string attributeName)
		{
			XmlAttributeInfo requiredAttribute = this.GetRequiredAttribute(this.currentElement, attributeName);
			if (!requiredAttribute.IsMissing)
			{
				return this.ValidateEnumMemberPath(requiredAttribute.Value);
			}
			return null;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00022514 File Offset: 0x00020714
		protected string RequiredEnumMemberPath(XmlTextValue text)
		{
			string text2 = ((text != null) ? text.TextValue : string.Empty);
			return this.ValidateEnumMembersPath(text2);
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0002253C File Offset: 0x0002073C
		protected string OptionalType(string attributeName)
		{
			XmlAttributeInfo optionalAttribute = EdmXmlDocumentParser<TResult>.GetOptionalAttribute(this.currentElement, attributeName);
			if (!optionalAttribute.IsMissing)
			{
				return this.ValidateTypeName(optionalAttribute.Value);
			}
			return null;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0002256C File Offset: 0x0002076C
		protected string RequiredType(string attributeName)
		{
			XmlAttributeInfo requiredAttribute = this.GetRequiredAttribute(this.currentElement, attributeName);
			if (!requiredAttribute.IsMissing)
			{
				return this.ValidateTypeName(requiredAttribute.Value);
			}
			return null;
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x000225A0 File Offset: 0x000207A0
		protected string OptionalQualifiedName(string attributeName)
		{
			XmlAttributeInfo optionalAttribute = EdmXmlDocumentParser<TResult>.GetOptionalAttribute(this.currentElement, attributeName);
			if (!optionalAttribute.IsMissing)
			{
				return this.ValidateQualifiedName(optionalAttribute.Value);
			}
			return null;
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x000225D0 File Offset: 0x000207D0
		protected string RequiredQualifiedName(string attributeName)
		{
			XmlAttributeInfo requiredAttribute = this.GetRequiredAttribute(this.currentElement, attributeName);
			if (!requiredAttribute.IsMissing)
			{
				return this.ValidateQualifiedName(requiredAttribute.Value);
			}
			return null;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00022604 File Offset: 0x00020804
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

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00022720 File Offset: 0x00020920
		private string ValidateTypeName(string name)
		{
			string[] array = name.Split(new char[] { '(', ')' });
			string text = array[0];
			if (!(text == "Collection"))
			{
				if (text == "Ref")
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
			if (EdmUtil.IsQualifiedName(text) || EdmCoreModel.Instance.GetPrimitiveTypeKind(text) != EdmPrimitiveTypeKind.None)
			{
				return name;
			}
			base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidTypeName, Strings.CsdlParser_InvalidTypeName(name));
			return name;
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x000227D4 File Offset: 0x000209D4
		private string ValidateAlias(string name)
		{
			if (!EdmUtil.IsValidUndottedName(name))
			{
				base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidQualifiedName, Strings.CsdlParser_InvalidAlias(name));
			}
			return name;
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x000227FC File Offset: 0x000209FC
		private string ValidateEntitySetPath(string path)
		{
			string[] array = path.Split(new char[] { '/' });
			if (Enumerable.Count<string>(array) != 2 || !EdmUtil.IsValidDottedName(array[0]) || !EdmUtil.IsValidUndottedName(array[1]))
			{
				base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidEntitySetPath, Strings.CsdlParser_InvalidEntitySetPath(path));
			}
			return path;
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00022858 File Offset: 0x00020A58
		private string ValidateEnumMemberPath(string path)
		{
			string[] array = path.Split(new char[] { '/' });
			if (Enumerable.Count<string>(array) != 2 || !EdmUtil.IsValidDottedName(array[0]) || !EdmUtil.IsValidUndottedName(array[1]))
			{
				base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidEnumMemberPath, Strings.CsdlParser_InvalidEnumMemberPath(path));
			}
			return path;
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x000228B1 File Offset: 0x00020AB1
		private string ValidateQualifiedName(string qualifiedName)
		{
			if (!EdmUtil.IsQualifiedName(qualifiedName))
			{
				base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidQualifiedName, Strings.CsdlParser_InvalidQualifiedName(qualifiedName));
			}
			return qualifiedName;
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x000228D8 File Offset: 0x00020AD8
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

		// Token: 0x0400069D RID: 1693
		protected XmlElementInfo currentElement;

		// Token: 0x0400069E RID: 1694
		private readonly Stack<XmlElementInfo> elementStack = new Stack<XmlElementInfo>();

		// Token: 0x0400069F RID: 1695
		private HashSetInternal<string> edmNamespaces;
	}
}
