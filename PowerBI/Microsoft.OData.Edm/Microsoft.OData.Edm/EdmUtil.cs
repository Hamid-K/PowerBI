using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.OData.Edm.Csdl.CsdlSemantics;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000090 RID: 144
	public static class EdmUtil
	{
		// Token: 0x060003A2 RID: 930 RVA: 0x00009E7D File Offset: 0x0000807D
		public static string GetMimeType(this IEdmModel model, IEdmProperty annotatableProperty)
		{
			return model.GetStringAnnotationValue(annotatableProperty, "MimeType", () => Strings.EdmUtil_NullValueForMimeTypeAnnotation);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00009EAA File Offset: 0x000080AA
		public static void SetMimeType(this IEdmModel model, IEdmProperty annotatableProperty, string mimeType)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmProperty>(annotatableProperty, "annotatableProperty");
			model.SetAnnotation(annotatableProperty, "MimeType", mimeType);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00009ED1 File Offset: 0x000080D1
		public static string GetMimeType(this IEdmModel model, IEdmOperation annotatableOperation)
		{
			return model.GetStringAnnotationValue(annotatableOperation, "MimeType", () => Strings.EdmUtil_NullValueForMimeTypeAnnotation);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00009F00 File Offset: 0x00008100
		public static string GetSymbolicString(this IEdmVocabularyAnnotatable annotatedElement)
		{
			IEdmSchemaElement edmSchemaElement = annotatedElement as IEdmSchemaElement;
			if (edmSchemaElement != null)
			{
				if (edmSchemaElement.SchemaElementKind == EdmSchemaElementKind.TypeDefinition)
				{
					IEdmType edmType = (IEdmType)edmSchemaElement;
					switch (edmType.TypeKind)
					{
					case EdmTypeKind.Entity:
						return "EntityType";
					case EdmTypeKind.Complex:
						return "ComplexType";
					case EdmTypeKind.Enum:
						return "EnumType";
					case EdmTypeKind.TypeDefinition:
						return "TypeDefinition";
					}
					return null;
				}
				return edmSchemaElement.SchemaElementKind.ToString();
			}
			else
			{
				IEdmEntityContainerElement edmEntityContainerElement = annotatedElement as IEdmEntityContainerElement;
				if (edmEntityContainerElement != null)
				{
					return edmEntityContainerElement.ContainerElementKind.ToString();
				}
				IEdmProperty edmProperty = annotatedElement as IEdmProperty;
				if (edmProperty != null)
				{
					EdmPropertyKind propertyKind = edmProperty.PropertyKind;
					if (propertyKind == EdmPropertyKind.Structural)
					{
						return "Property";
					}
					if (propertyKind == EdmPropertyKind.Navigation)
					{
						return "NavigationProperty";
					}
					return null;
				}
				else
				{
					IEdmExpression edmExpression = annotatedElement as IEdmExpression;
					if (edmExpression != null)
					{
						switch (edmExpression.ExpressionKind)
						{
						case EdmExpressionKind.Null:
						case EdmExpressionKind.Record:
						case EdmExpressionKind.Collection:
						case EdmExpressionKind.If:
						case EdmExpressionKind.Cast:
							return edmExpression.ExpressionKind.ToString();
						case EdmExpressionKind.IsType:
							return "IsOf";
						case EdmExpressionKind.FunctionApplication:
							return "Apply";
						case EdmExpressionKind.Labeled:
							return "LabeledElement";
						}
						return null;
					}
					if (annotatedElement is IEdmOperationParameter)
					{
						return "Parameter";
					}
					if (annotatedElement is IEdmOperationReturn)
					{
						return "ReturnType";
					}
					if (annotatedElement is IEdmReference)
					{
						return "Reference";
					}
					if (annotatedElement is IEdmInclude)
					{
						return "Include";
					}
					if (annotatedElement is IEdmReferentialConstraint)
					{
						return "ReferentialConstraint";
					}
					if (annotatedElement is IEdmEnumMember)
					{
						return "Member";
					}
					if (annotatedElement is IEdmVocabularyAnnotation)
					{
						return "Annotation";
					}
					if (annotatedElement is IEdmPropertyConstructor)
					{
						return "PropertyValue";
					}
					return null;
				}
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000A0AF File Offset: 0x000082AF
		public static void SetMimeType(this IEdmModel model, IEdmOperation annotatableOperation, string mimeType)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmOperation>(annotatableOperation, "annotatableOperation");
			model.SetAnnotation(annotatableOperation, "MimeType", mimeType);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000A0D8 File Offset: 0x000082D8
		internal static bool TryParseContainerQualifiedElementName(string containerQualifiedElementName, out string containerName, out string containerElementName)
		{
			containerName = null;
			containerElementName = null;
			int num = containerQualifiedElementName.LastIndexOf('.');
			if (num < 0)
			{
				return false;
			}
			containerName = containerQualifiedElementName.Substring(0, num);
			containerElementName = containerQualifiedElementName.Substring(num + 1);
			return !string.IsNullOrEmpty(containerName) && !string.IsNullOrEmpty(containerElementName);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000A124 File Offset: 0x00008324
		internal static bool IsNullOrWhiteSpaceInternal(string value)
		{
			return value == null || value.ToCharArray().All(new Func<char, bool>(char.IsWhiteSpace));
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000A144 File Offset: 0x00008344
		internal static string JoinInternal<T>(string separator, IEnumerable<T> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (separator == null)
			{
				separator = string.Empty;
			}
			string text;
			using (IEnumerator<T> enumerator = values.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					text = string.Empty;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					if (enumerator.Current != null)
					{
						T t = enumerator.Current;
						string text2 = t.ToString();
						if (text2 != null)
						{
							stringBuilder.Append(text2);
						}
					}
					while (enumerator.MoveNext())
					{
						stringBuilder.Append(separator);
						if (enumerator.Current != null)
						{
							T t = enumerator.Current;
							string text3 = t.ToString();
							if (text3 != null)
							{
								stringBuilder.Append(text3);
							}
						}
					}
					text = stringBuilder.ToString();
				}
			}
			return text;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000A220 File Offset: 0x00008420
		internal static bool IsQualifiedName(string name)
		{
			string[] array = name.Split(new char[] { '.' });
			if (array.Count<string>() < 2)
			{
				return false;
			}
			foreach (string text in array)
			{
				if (EdmUtil.IsNullOrWhiteSpaceInternal(text))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000A269 File Offset: 0x00008469
		internal static bool IsValidUndottedName(string name)
		{
			return !string.IsNullOrEmpty(name) && EdmUtil.UndottedNameValidator.IsMatch(name);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000A280 File Offset: 0x00008480
		internal static bool IsValidDottedName(string name)
		{
			return name.Split(new char[] { '.' }).All(new Func<string, bool>(EdmUtil.IsValidUndottedName));
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000A2A4 File Offset: 0x000084A4
		internal static string ParameterizedName(IEdmOperation operation)
		{
			int num = 0;
			int num2 = operation.Parameters.Count<IEdmOperationParameter>();
			StringBuilder stringBuilder = new StringBuilder();
			UnresolvedOperation unresolvedOperation = operation as UnresolvedOperation;
			if (unresolvedOperation != null)
			{
				stringBuilder.Append(unresolvedOperation.Namespace);
				stringBuilder.Append("/");
				stringBuilder.Append(unresolvedOperation.Name);
				return stringBuilder.ToString();
			}
			if (operation != null)
			{
				stringBuilder.Append(operation.Namespace);
				stringBuilder.Append(".");
			}
			stringBuilder.Append(operation.Name);
			stringBuilder.Append("(");
			foreach (IEdmOperationParameter edmOperationParameter in operation.Parameters)
			{
				string text;
				if (edmOperationParameter.Type == null)
				{
					text = "Edm.Untyped";
				}
				else if (edmOperationParameter.Type.IsCollection())
				{
					text = "Collection(" + edmOperationParameter.Type.AsCollection().ElementType().FullName() + ")";
				}
				else if (edmOperationParameter.Type.IsEntityReference())
				{
					text = "Ref(" + edmOperationParameter.Type.AsEntityReference().EntityType().FullName() + ")";
				}
				else
				{
					text = edmOperationParameter.Type.FullName();
				}
				stringBuilder.Append(text);
				num++;
				if (num < num2)
				{
					stringBuilder.Append(", ");
				}
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000A440 File Offset: 0x00008640
		internal static bool TryGetNamespaceNameFromQualifiedName(string qualifiedName, out string namespaceName, out string name, out string fullName)
		{
			bool flag = EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out namespaceName, out name);
			fullName = EdmUtil.GetFullNameForSchemaElement(namespaceName, name);
			return flag;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000A464 File Offset: 0x00008664
		internal static bool TryGetNamespaceNameFromQualifiedName(string qualifiedName, out string namespaceName, out string name)
		{
			int num = qualifiedName.LastIndexOf('/');
			if (num >= 0)
			{
				namespaceName = qualifiedName.Substring(0, num);
				name = qualifiedName.Substring(num + 1);
				return true;
			}
			int num2 = qualifiedName.LastIndexOf('.');
			if (num2 < 0)
			{
				namespaceName = string.Empty;
				name = qualifiedName;
				return false;
			}
			namespaceName = qualifiedName.Substring(0, num2);
			name = qualifiedName.Substring(num2 + 1);
			return true;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000A4C4 File Offset: 0x000086C4
		internal static string FullyQualifiedName(IEdmVocabularyAnnotatable element)
		{
			IEdmSchemaElement edmSchemaElement = element as IEdmSchemaElement;
			if (edmSchemaElement != null)
			{
				IEdmOperation edmOperation = edmSchemaElement as IEdmOperation;
				if (edmOperation != null)
				{
					return EdmUtil.ParameterizedName(edmOperation);
				}
				return edmSchemaElement.FullName();
			}
			else
			{
				IEdmEntityContainerElement edmEntityContainerElement = element as IEdmEntityContainerElement;
				if (edmEntityContainerElement != null)
				{
					return edmEntityContainerElement.Container.FullName() + "/" + edmEntityContainerElement.Name;
				}
				IEdmProperty edmProperty = element as IEdmProperty;
				if (edmProperty != null)
				{
					IEdmSchemaType edmSchemaType = edmProperty.DeclaringType as IEdmSchemaType;
					if (edmSchemaType != null)
					{
						string text = EdmUtil.FullyQualifiedName(edmSchemaType);
						if (text != null)
						{
							return text + "/" + edmProperty.Name;
						}
					}
				}
				else
				{
					IEdmOperationParameter edmOperationParameter = element as IEdmOperationParameter;
					IEdmEnumMember edmEnumMember;
					IEdmOperationReturn edmOperationReturn;
					if (edmOperationParameter != null)
					{
						string text2 = EdmUtil.FullyQualifiedName(edmOperationParameter.DeclaringOperation);
						if (text2 != null)
						{
							return text2 + "/" + edmOperationParameter.Name;
						}
					}
					else if ((edmEnumMember = element as IEdmEnumMember) != null)
					{
						string text3 = EdmUtil.FullyQualifiedName(edmEnumMember.DeclaringType);
						if (text3 != null)
						{
							return text3 + "/" + edmEnumMember.Name;
						}
					}
					else if ((edmOperationReturn = element as IEdmOperationReturn) != null)
					{
						string text4 = EdmUtil.FullyQualifiedName(edmOperationReturn.DeclaringOperation);
						if (text4 != null)
						{
							return text4 + "/$ReturnType";
						}
					}
				}
				return null;
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000A5E8 File Offset: 0x000087E8
		internal static T CheckArgumentNull<T>([EdmUtil.ValidatedNotNullAttribute] T value, string parameterName) where T : class
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			return value;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000A5FA File Offset: 0x000087FA
		internal static bool EqualsOrdinal(this string string1, string string2)
		{
			return string.Equals(string1, string2, StringComparison.Ordinal);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000A604 File Offset: 0x00008804
		internal static bool EqualsOrdinalIgnoreCase(this string string1, string string2)
		{
			return string.Equals(string1, string2, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000A610 File Offset: 0x00008810
		internal static void SetAnnotation(this IEdmModel model, IEdmElement annotatable, string localName, string value)
		{
			IEdmStringValue edmStringValue = null;
			if (value != null)
			{
				IEdmStringTypeReference @string = EdmCoreModel.Instance.GetString(true);
				edmStringValue = new EdmStringConstant(@string, value);
			}
			model.SetAnnotationValue(annotatable, "http://docs.oasis-open.org/odata/ns/metadata", localName, edmStringValue);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000A644 File Offset: 0x00008844
		internal static bool TryGetAnnotation(this IEdmModel model, IEdmElement annotatable, string localName, out string value)
		{
			object annotationValue = model.GetAnnotationValue(annotatable, "http://docs.oasis-open.org/odata/ns/metadata", localName);
			if (annotationValue == null)
			{
				value = null;
				return false;
			}
			IEdmStringValue edmStringValue = annotationValue as IEdmStringValue;
			if (edmStringValue == null)
			{
				throw new InvalidOperationException(Strings.EdmUtil_InvalidAnnotationValue(localName, annotationValue.GetType().FullName));
			}
			value = edmStringValue.Value;
			return true;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000A691 File Offset: 0x00008891
		internal static TValue DictionaryGetOrUpdate<TKey, TValue>(ConcurrentDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> computeValue)
		{
			EdmUtil.CheckArgumentNull<ConcurrentDictionary<TKey, TValue>>(dictionary, "dictionary");
			EdmUtil.CheckArgumentNull<Func<TKey, TValue>>(computeValue, "computeValue");
			return dictionary.GetOrAdd(key, computeValue);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000A6B4 File Offset: 0x000088B4
		internal static TValue DictionarySafeGet<TKey, TValue>(ConcurrentDictionary<TKey, TValue> dictionary, TKey key)
		{
			EdmUtil.CheckArgumentNull<ConcurrentDictionary<TKey, TValue>>(dictionary, "dictionary");
			TValue tvalue;
			dictionary.TryGetValue(key, out tvalue);
			return tvalue;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000A6D8 File Offset: 0x000088D8
		internal static string GetFullNameForSchemaElement(string elementNamespace, string elementName)
		{
			if (elementName == null)
			{
				return string.Empty;
			}
			if (elementNamespace == null)
			{
				return elementName;
			}
			return elementNamespace + "." + elementName;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000A6F4 File Offset: 0x000088F4
		private static string GetStringAnnotationValue<TEdmElement>(this IEdmModel model, TEdmElement annotatable, string localName, Func<string> getFoundAnnotationValueErrorString) where TEdmElement : class, IEdmElement
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<TEdmElement>(annotatable, "annotatable");
			string text;
			if (!model.TryGetAnnotation(annotatable, localName, out text))
			{
				return null;
			}
			if (text == null)
			{
				throw new InvalidOperationException(getFoundAnnotationValueErrorString());
			}
			return text;
		}

		// Token: 0x04000110 RID: 272
		private const string StartCharacterExp = "[\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}]";

		// Token: 0x04000111 RID: 273
		private const string OtherCharacterExp = "[\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]";

		// Token: 0x04000112 RID: 274
		private const string NameExp = "[\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}][\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]{0,}";

		// Token: 0x04000113 RID: 275
		private static Regex UndottedNameValidator = PlatformHelper.CreateCompiled("^[\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}][\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]{0,}$", RegexOptions.Singleline);

		// Token: 0x0200022C RID: 556
		private sealed class ValidatedNotNullAttribute : Attribute
		{
		}
	}
}
