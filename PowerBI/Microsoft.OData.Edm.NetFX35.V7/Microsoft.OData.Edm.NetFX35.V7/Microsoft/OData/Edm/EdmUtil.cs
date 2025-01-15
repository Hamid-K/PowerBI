using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.OData.Edm.Csdl.CsdlSemantics;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000014 RID: 20
	public static class EdmUtil
	{
		// Token: 0x0600015B RID: 347 RVA: 0x00006E09 File Offset: 0x00005009
		public static string GetMimeType(this IEdmModel model, IEdmProperty annotatableProperty)
		{
			return model.GetStringAnnotationValue(annotatableProperty, "MimeType", () => Strings.EdmUtil_NullValueForMimeTypeAnnotation);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00006E36 File Offset: 0x00005036
		public static void SetMimeType(this IEdmModel model, IEdmProperty annotatableProperty, string mimeType)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmProperty>(annotatableProperty, "annotatableProperty");
			model.SetAnnotation(annotatableProperty, "MimeType", mimeType);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006E5D File Offset: 0x0000505D
		public static string GetMimeType(this IEdmModel model, IEdmOperation annotatableOperation)
		{
			return model.GetStringAnnotationValue(annotatableOperation, "MimeType", () => Strings.EdmUtil_NullValueForMimeTypeAnnotation);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006E8A File Offset: 0x0000508A
		public static void SetMimeType(this IEdmModel model, IEdmOperation annotatableOperation, string mimeType)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmOperation>(annotatableOperation, "annotatableOperation");
			model.SetAnnotation(annotatableOperation, "MimeType", mimeType);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006EB4 File Offset: 0x000050B4
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

		// Token: 0x06000160 RID: 352 RVA: 0x00006F00 File Offset: 0x00005100
		internal static bool IsNullOrWhiteSpaceInternal(string value)
		{
			return value == null || Enumerable.All<char>(value.ToCharArray(), new Func<char, bool>(char.IsWhiteSpace));
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006F20 File Offset: 0x00005120
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

		// Token: 0x06000162 RID: 354 RVA: 0x00006FFC File Offset: 0x000051FC
		internal static bool IsQualifiedName(string name)
		{
			string[] array = name.Split(new char[] { '.' });
			if (Enumerable.Count<string>(array) < 2)
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

		// Token: 0x06000163 RID: 355 RVA: 0x00007045 File Offset: 0x00005245
		internal static bool IsValidUndottedName(string name)
		{
			return !string.IsNullOrEmpty(name) && EdmUtil.UndottedNameValidator.IsMatch(name);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000705C File Offset: 0x0000525C
		internal static bool IsValidDottedName(string name)
		{
			return Enumerable.All<string>(name.Split(new char[] { '.' }), new Func<string, bool>(EdmUtil.IsValidUndottedName));
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00007080 File Offset: 0x00005280
		internal static string ParameterizedName(IEdmOperation operation)
		{
			int num = 0;
			int num2 = Enumerable.Count<IEdmOperationParameter>(operation.Parameters);
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
				if (edmOperationParameter.Type.IsCollection())
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

		// Token: 0x06000166 RID: 358 RVA: 0x0000720C File Offset: 0x0000540C
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

		// Token: 0x06000167 RID: 359 RVA: 0x0000726C File Offset: 0x0000546C
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
					if (edmOperationParameter != null)
					{
						string text2 = EdmUtil.FullyQualifiedName(edmOperationParameter.DeclaringOperation);
						if (text2 != null)
						{
							return text2 + "/" + edmOperationParameter.Name;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000732F File Offset: 0x0000552F
		internal static T CheckArgumentNull<T>([EdmUtil.ValidatedNotNullAttribute] T value, string parameterName) where T : class
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			return value;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00007341 File Offset: 0x00005541
		internal static bool EqualsOrdinal(this string string1, string string2)
		{
			return string.Equals(string1, string2, 4);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000734B File Offset: 0x0000554B
		internal static bool EqualsOrdinalIgnoreCase(this string string1, string string2)
		{
			return string.Equals(string1, string2, 5);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00007358 File Offset: 0x00005558
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

		// Token: 0x0600016C RID: 364 RVA: 0x0000738C File Offset: 0x0000558C
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

		// Token: 0x0600016D RID: 365 RVA: 0x000073DC File Offset: 0x000055DC
		internal static TValue DictionaryGetOrUpdate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> computeValue)
		{
			EdmUtil.CheckArgumentNull<IDictionary<TKey, TValue>>(dictionary, "dictionary");
			EdmUtil.CheckArgumentNull<Func<TKey, TValue>>(computeValue, "computeValue");
			TValue tvalue;
			lock (dictionary)
			{
				if (dictionary.TryGetValue(key, ref tvalue))
				{
					return tvalue;
				}
			}
			TValue tvalue2 = computeValue.Invoke(key);
			lock (dictionary)
			{
				if (!dictionary.TryGetValue(key, ref tvalue))
				{
					tvalue = tvalue2;
					dictionary.Add(key, tvalue2);
				}
			}
			return tvalue;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00007474 File Offset: 0x00005674
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
				throw new InvalidOperationException(getFoundAnnotationValueErrorString.Invoke());
			}
			return text;
		}

		// Token: 0x0400002A RID: 42
		private const string StartCharacterExp = "[\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}]";

		// Token: 0x0400002B RID: 43
		private const string OtherCharacterExp = "[\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]";

		// Token: 0x0400002C RID: 44
		private const string NameExp = "[\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}][\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]{0,}";

		// Token: 0x0400002D RID: 45
		private static Regex UndottedNameValidator = PlatformHelper.CreateCompiled("^[\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}][\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]{0,}$", 16);

		// Token: 0x02000214 RID: 532
		private sealed class ValidatedNotNullAttribute : Attribute
		{
		}
	}
}
