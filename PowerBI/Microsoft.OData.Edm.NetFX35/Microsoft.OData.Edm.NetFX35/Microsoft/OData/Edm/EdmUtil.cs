using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.OData.Edm.Csdl.CsdlSemantics;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Library.Values;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm
{
	// Token: 0x020001EA RID: 490
	public static class EdmUtil
	{
		// Token: 0x06000B84 RID: 2948 RVA: 0x00020DB8 File Offset: 0x0001EFB8
		public static string GetMimeType(this IEdmModel model, IEdmProperty annotatableProperty)
		{
			return model.GetStringAnnotationValue(annotatableProperty, "MimeType", () => Strings.EdmUtil_NullValueForMimeTypeAnnotation);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00020DE3 File Offset: 0x0001EFE3
		public static void SetMimeType(this IEdmModel model, IEdmProperty annotatableProperty, string mimeType)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmProperty>(annotatableProperty, "annotatableProperty");
			model.SetAnnotation(annotatableProperty, "MimeType", mimeType);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00020E11 File Offset: 0x0001F011
		public static string GetMimeType(this IEdmModel model, IEdmOperation annotatableOperation)
		{
			return model.GetStringAnnotationValue(annotatableOperation, "MimeType", () => Strings.EdmUtil_NullValueForMimeTypeAnnotation);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00020E3C File Offset: 0x0001F03C
		public static void SetMimeType(this IEdmModel model, IEdmOperation annotatableOperation, string mimeType)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmOperation>(annotatableOperation, "annotatableOperation");
			model.SetAnnotation(annotatableOperation, "MimeType", mimeType);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00020E64 File Offset: 0x0001F064
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

		// Token: 0x06000B89 RID: 2953 RVA: 0x00020EB0 File Offset: 0x0001F0B0
		internal static bool IsNullOrWhiteSpaceInternal(string value)
		{
			return value == null || Enumerable.All<char>(value.ToCharArray(), new Func<char, bool>(char.IsWhiteSpace));
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00020ED0 File Offset: 0x0001F0D0
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
							T t2 = enumerator.Current;
							string text3 = t2.ToString();
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

		// Token: 0x06000B8B RID: 2955 RVA: 0x00020FAC File Offset: 0x0001F1AC
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

		// Token: 0x06000B8C RID: 2956 RVA: 0x00021003 File Offset: 0x0001F203
		internal static bool IsValidUndottedName(string name)
		{
			return !string.IsNullOrEmpty(name) && EdmUtil.UndottedNameValidator.IsMatch(name);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002101C File Offset: 0x0001F21C
		internal static bool IsValidDottedName(string name)
		{
			return Enumerable.All<string>(name.Split(new char[] { '.' }), new Func<string, bool>(EdmUtil.IsValidUndottedName));
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00021050 File Offset: 0x0001F250
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

		// Token: 0x06000B8F RID: 2959 RVA: 0x000211DC File Offset: 0x0001F3DC
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

		// Token: 0x06000B90 RID: 2960 RVA: 0x0002123C File Offset: 0x0001F43C
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

		// Token: 0x06000B91 RID: 2961 RVA: 0x000212FF File Offset: 0x0001F4FF
		internal static T CheckArgumentNull<T>([EdmUtil.ValidatedNotNullAttribute] T value, string parameterName) where T : class
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			return value;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00021311 File Offset: 0x0001F511
		internal static bool EqualsOrdinal(this string string1, string string2)
		{
			return string.Equals(string1, string2, 4);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0002131B File Offset: 0x0001F51B
		internal static bool EqualsOrdinalIgnoreCase(this string string1, string string2)
		{
			return string.Equals(string1, string2, 5);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00021328 File Offset: 0x0001F528
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

		// Token: 0x06000B95 RID: 2965 RVA: 0x0002135C File Offset: 0x0001F55C
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

		// Token: 0x06000B96 RID: 2966 RVA: 0x000213AC File Offset: 0x0001F5AC
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

		// Token: 0x06000B97 RID: 2967 RVA: 0x00021440 File Offset: 0x0001F640
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

		// Token: 0x0400053A RID: 1338
		private const string StartCharacterExp = "[\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}]";

		// Token: 0x0400053B RID: 1339
		private const string OtherCharacterExp = "[\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]";

		// Token: 0x0400053C RID: 1340
		private const string NameExp = "[\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}][\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]{0,}";

		// Token: 0x0400053D RID: 1341
		private static Regex UndottedNameValidator = PlatformHelper.CreateCompiled("^[\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}][\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]{0,}$", 16);

		// Token: 0x020001EB RID: 491
		private sealed class ValidatedNotNullAttribute : Attribute
		{
		}
	}
}
