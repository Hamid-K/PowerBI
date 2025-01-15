using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001BA RID: 442
	internal abstract class EdmXmlDocumentParser<TResult> : XmlDocumentParser<TResult>
	{
		// Token: 0x06000C83 RID: 3203 RVA: 0x0002408B File Offset: 0x0002228B
		internal EdmXmlDocumentParser(string artifactLocation, XmlReader reader)
			: base(reader, artifactLocation)
		{
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000C84 RID: 3204
		internal abstract IEnumerable<KeyValuePair<Version, string>> SupportedVersions { get; }

		// Token: 0x06000C85 RID: 3205 RVA: 0x000240A0 File Offset: 0x000222A0
		internal static XmlAttributeInfo GetOptionalAttribute(XmlElementInfo element, string attributeName)
		{
			return element.Attributes[attributeName];
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x000240B0 File Offset: 0x000222B0
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

		// Token: 0x06000C87 RID: 3207 RVA: 0x000240F0 File Offset: 0x000222F0
		protected override XmlReader InitializeReader(XmlReader reader)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings
			{
				CheckCharacters = true,
				CloseInput = false,
				IgnoreWhitespace = true,
				ConformanceLevel = ConformanceLevel.Auto,
				IgnoreComments = true,
				IgnoreProcessingInstructions = true,
				DtdProcessing = DtdProcessing.Prohibit
			};
			return XmlReader.Create(reader, xmlReaderSettings);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0002413C File Offset: 0x0002233C
		protected override bool TryGetDocumentVersion(string xmlNamespaceName, out Version version, out string[] expectedNamespaces)
		{
			expectedNamespaces = this.SupportedVersions.Select((KeyValuePair<Version, string> v) => v.Value).ToArray<string>();
			version = (from v in this.SupportedVersions
				where v.Value == xmlNamespaceName
				select v.Key).FirstOrDefault<Version>();
			return version != null;
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x000241D1 File Offset: 0x000223D1
		protected override bool IsOwnedNamespace(string namespaceName)
		{
			return this.IsEdmNamespace(namespaceName);
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x000241DC File Offset: 0x000223DC
		protected XmlElementParser<TItem> CsdlElement<TItem>(string elementName, Func<XmlElementInfo, XmlElementValueCollection, TItem> initializer, params XmlElementParser[] childParsers) where TItem : class
		{
			return this.Element<TItem>(elementName, delegate(XmlElementInfo element, XmlElementValueCollection childValues)
			{
				this.BeginItem(element);
				TItem titem = initializer(element, childValues);
				this.AnnotateItem(titem, childValues);
				this.EndItem();
				return titem;
			}, childParsers);
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x00024211 File Offset: 0x00022411
		protected void BeginItem(XmlElementInfo element)
		{
			this.elementStack.Push(element);
			this.currentElement = element;
		}

		// Token: 0x06000C8C RID: 3212
		protected abstract void AnnotateItem(object result, XmlElementValueCollection childValues);

		// Token: 0x06000C8D RID: 3213 RVA: 0x00024226 File Offset: 0x00022426
		protected void EndItem()
		{
			this.elementStack.Pop();
			this.currentElement = ((this.elementStack.Count == 0) ? null : this.elementStack.Peek());
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00024258 File Offset: 0x00022458
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
			return null;
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x000242B8 File Offset: 0x000224B8
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
			return null;
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00024318 File Offset: 0x00022518
		protected int? OptionalSrid(string attributeName, int defaultSrid)
		{
			XmlAttributeInfo optionalAttribute = EdmXmlDocumentParser<TResult>.GetOptionalAttribute(this.currentElement, attributeName);
			if (!optionalAttribute.IsMissing)
			{
				int? num;
				if (optionalAttribute.Value.EqualsOrdinalIgnoreCase("Variable"))
				{
					num = null;
				}
				else if (!EdmValueParser.TryParseInt(optionalAttribute.Value, out num))
				{
					base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidSrid, Strings.ValueParser_InvalidSrid(optionalAttribute.Value));
				}
				return num;
			}
			return new int?(defaultSrid);
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00024390 File Offset: 0x00022590
		protected int? OptionalScale(string attributeName)
		{
			XmlAttributeInfo optionalAttribute = EdmXmlDocumentParser<TResult>.GetOptionalAttribute(this.currentElement, attributeName);
			if (!optionalAttribute.IsMissing)
			{
				int? num;
				if (optionalAttribute.Value.EqualsOrdinalIgnoreCase("Variable"))
				{
					num = null;
				}
				else if (!EdmValueParser.TryParseInt(optionalAttribute.Value, out num))
				{
					base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidSrid, Strings.ValueParser_InvalidScale(optionalAttribute.Value));
				}
				return num;
			}
			return new int?(0);
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00024408 File Offset: 0x00022608
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
			return null;
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00024468 File Offset: 0x00022668
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

		// Token: 0x06000C94 RID: 3220 RVA: 0x000244E0 File Offset: 0x000226E0
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

		// Token: 0x06000C95 RID: 3221 RVA: 0x0002454C File Offset: 0x0002274C
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
			return null;
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x000245A8 File Offset: 0x000227A8
		protected string Optional(string attributeName)
		{
			XmlAttributeInfo optionalAttribute = EdmXmlDocumentParser<TResult>.GetOptionalAttribute(this.currentElement, attributeName);
			if (optionalAttribute.IsMissing)
			{
				return null;
			}
			return optionalAttribute.Value;
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x000245D4 File Offset: 0x000227D4
		protected string Required(string attributeName)
		{
			XmlAttributeInfo requiredAttribute = this.GetRequiredAttribute(this.currentElement, attributeName);
			if (requiredAttribute.IsMissing)
			{
				return string.Empty;
			}
			return requiredAttribute.Value;
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00024604 File Offset: 0x00022804
		protected string OptionalAlias(string attributeName)
		{
			XmlAttributeInfo optionalAttribute = EdmXmlDocumentParser<TResult>.GetOptionalAttribute(this.currentElement, attributeName);
			if (!optionalAttribute.IsMissing)
			{
				return this.ValidateAlias(optionalAttribute.Value);
			}
			return null;
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00024634 File Offset: 0x00022834
		protected string RequiredAlias(string attributeName)
		{
			XmlAttributeInfo requiredAttribute = this.GetRequiredAttribute(this.currentElement, attributeName);
			if (!requiredAttribute.IsMissing)
			{
				return this.ValidateAlias(requiredAttribute.Value);
			}
			return null;
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00024668 File Offset: 0x00022868
		protected string RequiredEntitySetPath(string attributeName)
		{
			XmlAttributeInfo requiredAttribute = this.GetRequiredAttribute(this.currentElement, attributeName);
			if (!requiredAttribute.IsMissing)
			{
				return this.ValidateEntitySetPath(requiredAttribute.Value);
			}
			return null;
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0002469C File Offset: 0x0002289C
		protected string RequiredEnumMemberPath(string attributeName)
		{
			XmlAttributeInfo requiredAttribute = this.GetRequiredAttribute(this.currentElement, attributeName);
			if (!requiredAttribute.IsMissing)
			{
				return this.ValidateEnumMemberPath(requiredAttribute.Value);
			}
			return null;
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x000246D0 File Offset: 0x000228D0
		protected string RequiredEnumMemberPath(XmlTextValue text)
		{
			string text2 = ((text != null) ? text.TextValue : string.Empty);
			return this.ValidateEnumMembersPath(text2);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x000246F8 File Offset: 0x000228F8
		protected string OptionalType(string attributeName)
		{
			XmlAttributeInfo optionalAttribute = EdmXmlDocumentParser<TResult>.GetOptionalAttribute(this.currentElement, attributeName);
			if (!optionalAttribute.IsMissing)
			{
				return this.ValidateTypeName(optionalAttribute.Value);
			}
			return null;
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00024728 File Offset: 0x00022928
		protected string RequiredType(string attributeName)
		{
			XmlAttributeInfo requiredAttribute = this.GetRequiredAttribute(this.currentElement, attributeName);
			if (!requiredAttribute.IsMissing)
			{
				return this.ValidateTypeName(requiredAttribute.Value);
			}
			return null;
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0002475C File Offset: 0x0002295C
		protected string OptionalQualifiedName(string attributeName)
		{
			XmlAttributeInfo optionalAttribute = EdmXmlDocumentParser<TResult>.GetOptionalAttribute(this.currentElement, attributeName);
			if (!optionalAttribute.IsMissing)
			{
				return this.ValidateQualifiedName(optionalAttribute.Value);
			}
			return null;
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0002478C File Offset: 0x0002298C
		protected string RequiredQualifiedName(string attributeName)
		{
			XmlAttributeInfo requiredAttribute = this.GetRequiredAttribute(this.currentElement, attributeName);
			if (!requiredAttribute.IsMissing)
			{
				return this.ValidateQualifiedName(requiredAttribute.Value);
			}
			return null;
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x000247C0 File Offset: 0x000229C0
		protected string ValidateEnumMembersPath(string path)
		{
			if (string.IsNullOrEmpty(path.Trim()))
			{
				base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidEnumMemberPath, Strings.CsdlParser_InvalidEnumMemberPath(path));
			}
			string[] array = (from s in path.Split(new char[] { ' ' })
				where !string.IsNullOrEmpty(s)
				select s).ToArray<string>();
			string text = null;
			foreach (string text2 in array)
			{
				string[] array3 = text2.Split(new char[] { '/' });
				if (array3.Count<string>() != 2 || !EdmUtil.IsValidDottedName(array3[0]) || !EdmUtil.IsValidUndottedName(array3[1]))
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

		// Token: 0x06000CA2 RID: 3234 RVA: 0x000248DC File Offset: 0x00022ADC
		private string ValidateTypeName(string name)
		{
			string[] array = name.Split(new char[] { '(', ')' });
			string text = array[0];
			if (!(text == "Collection"))
			{
				if (text == "Ref")
				{
					if (array.Count<string>() == 1)
					{
						base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidTypeName, Strings.CsdlParser_InvalidTypeName(name));
						return name;
					}
					text = array[1];
				}
			}
			else
			{
				if (array.Count<string>() == 1)
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

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00024990 File Offset: 0x00022B90
		private string ValidateAlias(string name)
		{
			if (!EdmUtil.IsValidUndottedName(name))
			{
				base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidQualifiedName, Strings.CsdlParser_InvalidAlias(name));
			}
			return name;
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x000249B8 File Offset: 0x00022BB8
		private string ValidateEntitySetPath(string path)
		{
			string[] array = path.Split(new char[] { '/' });
			if (array.Count<string>() != 2 || !EdmUtil.IsValidDottedName(array[0]) || !EdmUtil.IsValidUndottedName(array[1]))
			{
				base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidEntitySetPath, Strings.CsdlParser_InvalidEntitySetPath(path));
			}
			return path;
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00024A14 File Offset: 0x00022C14
		private string ValidateEnumMemberPath(string path)
		{
			string[] array = path.Split(new char[] { '/' });
			if (array.Count<string>() != 2 || !EdmUtil.IsValidDottedName(array[0]) || !EdmUtil.IsValidUndottedName(array[1]))
			{
				base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidEnumMemberPath, Strings.CsdlParser_InvalidEnumMemberPath(path));
			}
			return path;
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00024A6D File Offset: 0x00022C6D
		private string ValidateQualifiedName(string qualifiedName)
		{
			if (!EdmUtil.IsQualifiedName(qualifiedName))
			{
				base.ReportError(this.currentElement.Location, EdmErrorCode.InvalidQualifiedName, Strings.CsdlParser_InvalidQualifiedName(qualifiedName));
			}
			return qualifiedName;
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00024A94 File Offset: 0x00022C94
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

		// Token: 0x04000716 RID: 1814
		protected XmlElementInfo currentElement;

		// Token: 0x04000717 RID: 1815
		private readonly Stack<XmlElementInfo> elementStack = new Stack<XmlElementInfo>();

		// Token: 0x04000718 RID: 1816
		private HashSetInternal<string> edmNamespaces;
	}
}
