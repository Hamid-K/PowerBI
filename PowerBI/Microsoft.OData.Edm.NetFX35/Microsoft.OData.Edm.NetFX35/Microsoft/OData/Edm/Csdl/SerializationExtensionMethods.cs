using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Validation.Internal;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x020000D0 RID: 208
	public static class SerializationExtensionMethods
	{
		// Token: 0x0600038F RID: 911 RVA: 0x00008628 File Offset: 0x00006828
		public static Version GetEdmxVersion(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "EdmxVersion");
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00008647 File Offset: 0x00006847
		public static void SetEdmxVersion(this IEdmModel model, Version version)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "EdmxVersion", version);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00008667 File Offset: 0x00006867
		public static void SetNamespacePrefixMappings(this IEdmModel model, IEnumerable<KeyValuePair<string, string>> mappings)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "NamespacePrefix", mappings);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00008687 File Offset: 0x00006887
		public static IEnumerable<KeyValuePair<string, string>> GetNamespacePrefixMappings(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "NamespacePrefix");
		}

		// Token: 0x06000393 RID: 915 RVA: 0x000086A6 File Offset: 0x000068A6
		public static void SetSerializationLocation(this IEdmVocabularyAnnotation annotation, IEdmModel model, EdmVocabularyAnnotationSerializationLocation? location)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotation>(annotation, "annotation");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(annotation, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "AnnotationSerializationLocation", location);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x000086D7 File Offset: 0x000068D7
		public static EdmVocabularyAnnotationSerializationLocation? GetSerializationLocation(this IEdmVocabularyAnnotation annotation, IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotation>(annotation, "annotation");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(annotation, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "AnnotationSerializationLocation") as EdmVocabularyAnnotationSerializationLocation?;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000870C File Offset: 0x0000690C
		public static void SetSchemaNamespace(this IEdmVocabularyAnnotation annotation, IEdmModel model, string schemaNamespace)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotation>(annotation, "annotation");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(annotation, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "SchemaNamespace", schemaNamespace);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00008738 File Offset: 0x00006938
		public static string GetSchemaNamespace(this IEdmVocabularyAnnotation annotation, IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotation>(annotation, "annotation");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(annotation, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "SchemaNamespace");
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00008763 File Offset: 0x00006963
		public static void SetIsValueExplicit(this IEdmEnumMember member, IEdmModel model, bool? isExplicit)
		{
			EdmUtil.CheckArgumentNull<IEdmEnumMember>(member, "member");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(member, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "IsEnumMemberValueExplicit", isExplicit);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00008794 File Offset: 0x00006994
		public static bool? IsValueExplicit(this IEdmEnumMember member, IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmEnumMember>(member, "member");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(member, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "IsEnumMemberValueExplicit") as bool?;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000087CC File Offset: 0x000069CC
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

		// Token: 0x0600039A RID: 922 RVA: 0x00008824 File Offset: 0x00006A24
		public static bool IsSerializedAsElement(this IEdmValue value, IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmValue>(value, "value");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return (model.GetAnnotationValue(value, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "IsSerializedAsElement") as bool?) ?? false;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00008878 File Offset: 0x00006A78
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

		// Token: 0x0600039C RID: 924 RVA: 0x0000892C File Offset: 0x00006B2C
		public static string GetNamespaceAlias(this IEdmModel model, string namespaceName)
		{
			VersioningDictionary<string, string> annotationValue = model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "NamespaceAlias");
			return annotationValue.Get(namespaceName);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00008952 File Offset: 0x00006B52
		internal static VersioningDictionary<string, string> GetNamespaceAliases(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "NamespaceAlias");
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00008971 File Offset: 0x00006B71
		internal static VersioningList<string> GetUsedNamespacesHavingAlias(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "UsedNamespaces");
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00008990 File Offset: 0x00006B90
		internal static bool IsInline(this IEdmVocabularyAnnotation annotation, IEdmModel model)
		{
			return annotation.GetSerializationLocation(model) == EdmVocabularyAnnotationSerializationLocation.Inline || annotation.TargetString() == null;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000089C5 File Offset: 0x00006BC5
		internal static string TargetString(this IEdmVocabularyAnnotation annotation)
		{
			return EdmUtil.FullyQualifiedName(annotation.Target);
		}
	}
}
