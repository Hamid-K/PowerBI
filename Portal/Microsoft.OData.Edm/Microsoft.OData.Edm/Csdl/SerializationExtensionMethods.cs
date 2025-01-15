using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x02000153 RID: 339
	public static class SerializationExtensionMethods
	{
		// Token: 0x06000884 RID: 2180 RVA: 0x000169BD File Offset: 0x00014BBD
		public static Version GetEdmxVersion(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "EdmxVersion");
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x000169DC File Offset: 0x00014BDC
		public static void SetEdmxVersion(this IEdmModel model, Version version)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "EdmxVersion", version);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x000169FC File Offset: 0x00014BFC
		public static void SetNamespacePrefixMappings(this IEdmModel model, IEnumerable<KeyValuePair<string, string>> mappings)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "NamespacePrefix", mappings);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00016A1C File Offset: 0x00014C1C
		public static IEnumerable<KeyValuePair<string, string>> GetNamespacePrefixMappings(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "NamespacePrefix");
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00016A3B File Offset: 0x00014C3B
		public static void SetSerializationLocation(this IEdmVocabularyAnnotation annotation, IEdmModel model, EdmVocabularyAnnotationSerializationLocation? location)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotation>(annotation, "annotation");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(annotation, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "AnnotationSerializationLocation", location);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00016A6C File Offset: 0x00014C6C
		public static EdmVocabularyAnnotationSerializationLocation? GetSerializationLocation(this IEdmVocabularyAnnotation annotation, IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotation>(annotation, "annotation");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(annotation, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "AnnotationSerializationLocation") as EdmVocabularyAnnotationSerializationLocation?;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00016AA1 File Offset: 0x00014CA1
		public static void SetSchemaNamespace(this IEdmVocabularyAnnotation annotation, IEdmModel model, string schemaNamespace)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotation>(annotation, "annotation");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(annotation, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "SchemaNamespace", schemaNamespace);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00016ACD File Offset: 0x00014CCD
		public static string GetSchemaNamespace(this IEdmVocabularyAnnotation annotation, IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotation>(annotation, "annotation");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(annotation, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "SchemaNamespace");
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00016AF8 File Offset: 0x00014CF8
		public static void SetIsValueExplicit(this IEdmEnumMember member, IEdmModel model, bool? isExplicit)
		{
			EdmUtil.CheckArgumentNull<IEdmEnumMember>(member, "member");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(member, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "IsEnumMemberValueExplicit", isExplicit);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00016B29 File Offset: 0x00014D29
		public static bool? IsValueExplicit(this IEdmEnumMember member, IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmEnumMember>(member, "member");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(member, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "IsEnumMemberValueExplicit") as bool?;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00016B60 File Offset: 0x00014D60
		public static void SetIsSerializedAsElement(this IEdmValue value, IEdmModel model, bool isSerializedAsElement)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(value, "value");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmError edmError;
			if (isSerializedAsElement && !ValidationHelper.ValidateValueCanBeWrittenAsXmlElementAnnotation(value, null, null, out edmError))
			{
				throw new InvalidOperationException(edmError.ToString());
			}
			model.SetAnnotationValue(value, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "IsSerializedAsElement", isSerializedAsElement);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00016BB8 File Offset: 0x00014DB8
		public static bool IsSerializedAsElement(this IEdmValue value, IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(value, "value");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return (model.GetAnnotationValue(value, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "IsSerializedAsElement") as bool?) ?? false;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00016C0C File Offset: 0x00014E0C
		public static void SetNamespaceAlias(this IEdmModel model, string namespaceName, string alias)
		{
			VersioningDictionary<string, string> versioningDictionary = model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "NamespaceAlias");
			if (versioningDictionary == null)
			{
				versioningDictionary = VersioningDictionary<string, string>.Create(new Func<string, string, int>(string.CompareOrdinal));
			}
			if (EdmUtil.IsNullOrWhiteSpaceInternal(alias))
			{
				string text;
				if (versioningDictionary.TryGetValue(namespaceName, out text))
				{
					versioningDictionary = versioningDictionary.Remove(namespaceName);
				}
			}
			else
			{
				versioningDictionary = versioningDictionary.Set(namespaceName, alias);
			}
			model.SetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "NamespaceAlias", versioningDictionary);
			VersioningList<string> versioningList = model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "UsedNamespaces");
			if (versioningList == null)
			{
				versioningList = VersioningList<string>.Create();
			}
			if (!string.IsNullOrEmpty(namespaceName) && !versioningList.Contains(namespaceName))
			{
				versioningList = versioningList.Add(namespaceName);
			}
			model.SetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "UsedNamespaces", versioningList);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00016CC0 File Offset: 0x00014EC0
		public static string GetNamespaceAlias(this IEdmModel model, string namespaceName)
		{
			VersioningDictionary<string, string> annotationValue = model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "NamespaceAlias");
			return annotationValue.Get(namespaceName);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00016CE6 File Offset: 0x00014EE6
		internal static VersioningDictionary<string, string> GetNamespaceAliases(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "NamespaceAlias");
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00016D05 File Offset: 0x00014F05
		internal static VersioningList<string> GetUsedNamespacesHavingAlias(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "UsedNamespaces");
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00016D24 File Offset: 0x00014F24
		internal static bool IsInline(this IEdmVocabularyAnnotation annotation, IEdmModel model)
		{
			return annotation.GetSerializationLocation(model) == EdmVocabularyAnnotationSerializationLocation.Inline || annotation.TargetString() == null;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00016D5C File Offset: 0x00014F5C
		internal static string TargetString(this IEdmVocabularyAnnotation annotation)
		{
			return EdmUtil.FullyQualifiedName(annotation.Target);
		}
	}
}
