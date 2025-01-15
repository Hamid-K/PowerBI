using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x02000145 RID: 325
	public static class SerializationExtensionMethods
	{
		// Token: 0x060007E1 RID: 2017 RVA: 0x00014D85 File Offset: 0x00012F85
		public static Version GetEdmxVersion(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "EdmxVersion");
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00014DA4 File Offset: 0x00012FA4
		public static void SetEdmxVersion(this IEdmModel model, Version version)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "EdmxVersion", version);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00014DC4 File Offset: 0x00012FC4
		public static void SetNamespacePrefixMappings(this IEdmModel model, IEnumerable<KeyValuePair<string, string>> mappings)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "NamespacePrefix", mappings);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00014DE4 File Offset: 0x00012FE4
		public static IEnumerable<KeyValuePair<string, string>> GetNamespacePrefixMappings(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "NamespacePrefix");
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00014E03 File Offset: 0x00013003
		public static void SetSerializationLocation(this IEdmVocabularyAnnotation annotation, IEdmModel model, EdmVocabularyAnnotationSerializationLocation? location)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotation>(annotation, "annotation");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(annotation, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "AnnotationSerializationLocation", location);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00014E34 File Offset: 0x00013034
		public static EdmVocabularyAnnotationSerializationLocation? GetSerializationLocation(this IEdmVocabularyAnnotation annotation, IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotation>(annotation, "annotation");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(annotation, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "AnnotationSerializationLocation") as EdmVocabularyAnnotationSerializationLocation?;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00014E69 File Offset: 0x00013069
		public static void SetSchemaNamespace(this IEdmVocabularyAnnotation annotation, IEdmModel model, string schemaNamespace)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotation>(annotation, "annotation");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(annotation, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "SchemaNamespace", schemaNamespace);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00014E95 File Offset: 0x00013095
		public static string GetSchemaNamespace(this IEdmVocabularyAnnotation annotation, IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotation>(annotation, "annotation");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(annotation, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "SchemaNamespace");
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00014EC0 File Offset: 0x000130C0
		public static void SetIsValueExplicit(this IEdmEnumMember member, IEdmModel model, bool? isExplicit)
		{
			EdmUtil.CheckArgumentNull<IEdmEnumMember>(member, "member");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(member, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "IsEnumMemberValueExplicit", isExplicit);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00014EF1 File Offset: 0x000130F1
		public static bool? IsValueExplicit(this IEdmEnumMember member, IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmEnumMember>(member, "member");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(member, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "IsEnumMemberValueExplicit") as bool?;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00014F28 File Offset: 0x00013128
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

		// Token: 0x060007EC RID: 2028 RVA: 0x00014F80 File Offset: 0x00013180
		public static bool IsSerializedAsElement(this IEdmValue value, IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(value, "value");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return (model.GetAnnotationValue(value, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "IsSerializedAsElement") as bool?) ?? false;
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00014FD4 File Offset: 0x000131D4
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
			if (!string.IsNullOrEmpty(namespaceName) && !Enumerable.Contains<string>(versioningList, namespaceName))
			{
				versioningList = versioningList.Add(namespaceName);
			}
			model.SetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "UsedNamespaces", versioningList);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00015088 File Offset: 0x00013288
		public static string GetNamespaceAlias(this IEdmModel model, string namespaceName)
		{
			VersioningDictionary<string, string> annotationValue = model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "NamespaceAlias");
			return annotationValue.Get(namespaceName);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x000150AE File Offset: 0x000132AE
		internal static VersioningDictionary<string, string> GetNamespaceAliases(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "NamespaceAlias");
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000150CD File Offset: 0x000132CD
		internal static VersioningList<string> GetUsedNamespacesHavingAlias(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "UsedNamespaces");
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x000150EC File Offset: 0x000132EC
		internal static bool IsInline(this IEdmVocabularyAnnotation annotation, IEdmModel model)
		{
			return annotation.GetSerializationLocation(model) == EdmVocabularyAnnotationSerializationLocation.Inline || annotation.TargetString() == null;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00015124 File Offset: 0x00013324
		internal static string TargetString(this IEdmVocabularyAnnotation annotation)
		{
			return EdmUtil.FullyQualifiedName(annotation.Target);
		}
	}
}
