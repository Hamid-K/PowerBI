using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Annotations;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.Edm.Library.Annotations;
using Microsoft.Data.Edm.Library.Values;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x0200020F RID: 527
	internal static class EpmExtensionMethods
	{
		// Token: 0x06000F4F RID: 3919 RVA: 0x00038324 File Offset: 0x00036524
		internal static ODataEntityPropertyMappingCache EnsureEpmCache(this IEdmModel model, IEdmEntityType entityType, int maxMappingCount)
		{
			bool flag;
			return EpmExtensionMethods.EnsureEpmCacheInternal(model, entityType, maxMappingCount, out flag);
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0003833C File Offset: 0x0003653C
		internal static bool HasEntityPropertyMappings(this IEdmModel model, IEdmEntityType entityType)
		{
			for (IEdmEntityType edmEntityType = entityType; edmEntityType != null; edmEntityType = edmEntityType.BaseEntityType())
			{
				if (model.GetEntityPropertyMappings(edmEntityType) != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x00038363 File Offset: 0x00036563
		internal static ODataEntityPropertyMappingCollection GetEntityPropertyMappings(this IEdmModel model, IEdmEntityType entityType)
		{
			return model.GetAnnotationValue(entityType);
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0003836C File Offset: 0x0003656C
		internal static ODataEntityPropertyMappingCache GetEpmCache(this IEdmModel model, IEdmEntityType entityType)
		{
			return model.GetAnnotationValue(entityType);
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x00038378 File Offset: 0x00036578
		internal static Dictionary<string, IEdmDirectValueAnnotationBinding> GetAnnotationBindingsToRemoveSerializableEpmAnnotations(this IEdmModel model, IEdmElement annotatable)
		{
			Dictionary<string, IEdmDirectValueAnnotationBinding> dictionary = new Dictionary<string, IEdmDirectValueAnnotationBinding>(StringComparer.Ordinal);
			IEnumerable<IEdmDirectValueAnnotation> odataAnnotations = model.GetODataAnnotations(annotatable);
			if (odataAnnotations != null)
			{
				foreach (IEdmDirectValueAnnotation edmDirectValueAnnotation in odataAnnotations)
				{
					if (edmDirectValueAnnotation.IsEpmAnnotation())
					{
						dictionary.Add(edmDirectValueAnnotation.Name, new EdmDirectValueAnnotationBinding(annotatable, edmDirectValueAnnotation.NamespaceUri, edmDirectValueAnnotation.Name, null));
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x000383F8 File Offset: 0x000365F8
		internal static void ClearInMemoryEpmAnnotations(this IEdmModel model, IEdmElement annotatable)
		{
			model.SetAnnotationValues(new IEdmDirectValueAnnotationBinding[]
			{
				new EdmTypedDirectValueAnnotationBinding<ODataEntityPropertyMappingCollection>(annotatable, null),
				new EdmTypedDirectValueAnnotationBinding<ODataEntityPropertyMappingCache>(annotatable, null)
			});
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0003849C File Offset: 0x0003669C
		internal static void SaveEpmAnnotationsForProperty(this IEdmModel model, IEdmProperty property, ODataEntityPropertyMappingCache epmCache)
		{
			string propertyName = property.Name;
			IEnumerable<EntityPropertyMappingAttribute> enumerable = Enumerable.Where<EntityPropertyMappingAttribute>(epmCache.MappingsForDeclaredProperties, (EntityPropertyMappingAttribute m) => m.SourcePath.StartsWith(propertyName, 4) && (m.SourcePath.Length == propertyName.Length || m.SourcePath.get_Chars(propertyName.Length) == '/'));
			bool flag;
			bool flag2;
			if (property.Type.IsODataPrimitiveTypeKind())
			{
				flag = true;
				flag2 = false;
			}
			else
			{
				flag2 = true;
				flag = Enumerable.Any<EntityPropertyMappingAttribute>(enumerable, (EntityPropertyMappingAttribute m) => m.SourcePath == propertyName);
			}
			model.SaveEpmAnnotations(property, enumerable, flag, flag2);
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00038510 File Offset: 0x00036710
		internal static void SaveEpmAnnotations(this IEdmModel model, IEdmElement annotatable, IEnumerable<EntityPropertyMappingAttribute> mappings, bool skipSourcePath, bool removePrefix)
		{
			EpmAttributeNameBuilder epmAttributeNameBuilder = new EpmAttributeNameBuilder();
			Dictionary<string, IEdmDirectValueAnnotationBinding> annotationBindingsToRemoveSerializableEpmAnnotations = model.GetAnnotationBindingsToRemoveSerializableEpmAnnotations(annotatable);
			foreach (EntityPropertyMappingAttribute entityPropertyMappingAttribute in mappings)
			{
				string text;
				if (entityPropertyMappingAttribute.TargetSyndicationItem == SyndicationItemProperty.CustomProperty)
				{
					text = epmAttributeNameBuilder.EpmTargetPath;
					annotationBindingsToRemoveSerializableEpmAnnotations[text] = EpmExtensionMethods.GetODataAnnotationBinding(annotatable, text, entityPropertyMappingAttribute.TargetPath);
					text = epmAttributeNameBuilder.EpmNsUri;
					annotationBindingsToRemoveSerializableEpmAnnotations[text] = EpmExtensionMethods.GetODataAnnotationBinding(annotatable, text, entityPropertyMappingAttribute.TargetNamespaceUri);
					string targetNamespacePrefix = entityPropertyMappingAttribute.TargetNamespacePrefix;
					if (!string.IsNullOrEmpty(targetNamespacePrefix))
					{
						text = epmAttributeNameBuilder.EpmNsPrefix;
						annotationBindingsToRemoveSerializableEpmAnnotations[text] = EpmExtensionMethods.GetODataAnnotationBinding(annotatable, text, targetNamespacePrefix);
					}
				}
				else
				{
					text = epmAttributeNameBuilder.EpmTargetPath;
					annotationBindingsToRemoveSerializableEpmAnnotations[text] = EpmExtensionMethods.GetODataAnnotationBinding(annotatable, text, entityPropertyMappingAttribute.TargetSyndicationItem.ToAttributeValue());
					text = epmAttributeNameBuilder.EpmContentKind;
					annotationBindingsToRemoveSerializableEpmAnnotations[text] = EpmExtensionMethods.GetODataAnnotationBinding(annotatable, text, entityPropertyMappingAttribute.TargetTextContentKind.ToAttributeValue());
				}
				if (!skipSourcePath)
				{
					string text2 = entityPropertyMappingAttribute.SourcePath;
					if (removePrefix)
					{
						text2 = text2.Substring(text2.IndexOf('/') + 1);
					}
					text = epmAttributeNameBuilder.EpmSourcePath;
					annotationBindingsToRemoveSerializableEpmAnnotations[text] = EpmExtensionMethods.GetODataAnnotationBinding(annotatable, text, text2);
				}
				string text3 = (entityPropertyMappingAttribute.KeepInContent ? "true" : "false");
				text = epmAttributeNameBuilder.EpmKeepInContent;
				annotationBindingsToRemoveSerializableEpmAnnotations[text] = EpmExtensionMethods.GetODataAnnotationBinding(annotatable, text, text3);
				epmAttributeNameBuilder.MoveNext();
			}
			model.SetAnnotationValues(annotationBindingsToRemoveSerializableEpmAnnotations.Values);
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00038698 File Offset: 0x00036898
		internal static CachedPrimitiveKeepInContentAnnotation EpmCachedKeepPrimitiveInContent(this IEdmModel model, IEdmComplexType complexType)
		{
			return model.GetAnnotationValue(complexType);
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x000386A4 File Offset: 0x000368A4
		internal static string ToTargetPath(this SyndicationItemProperty targetSyndicationItem)
		{
			switch (targetSyndicationItem)
			{
			case SyndicationItemProperty.AuthorEmail:
				return "author/email";
			case SyndicationItemProperty.AuthorName:
				return "author/name";
			case SyndicationItemProperty.AuthorUri:
				return "author/uri";
			case SyndicationItemProperty.ContributorEmail:
				return "contributor/email";
			case SyndicationItemProperty.ContributorName:
				return "contributor/name";
			case SyndicationItemProperty.ContributorUri:
				return "contributor/uri";
			case SyndicationItemProperty.Updated:
				return "updated";
			case SyndicationItemProperty.Published:
				return "published";
			case SyndicationItemProperty.Rights:
				return "rights";
			case SyndicationItemProperty.Summary:
				return "summary";
			case SyndicationItemProperty.Title:
				return "title";
			default:
				throw new ArgumentException(Strings.EntityPropertyMapping_EpmAttribute("targetSyndicationItem"));
			}
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0003873C File Offset: 0x0003693C
		private static void LoadEpmAnnotations(IEdmModel model, IEdmEntityType entityType)
		{
			string text = entityType.ODataFullName();
			ODataEntityPropertyMappingCollection odataEntityPropertyMappingCollection = new ODataEntityPropertyMappingCollection();
			model.LoadEpmAnnotations(entityType, odataEntityPropertyMappingCollection, text, null);
			IEnumerable<IEdmProperty> declaredProperties = entityType.DeclaredProperties;
			if (declaredProperties != null)
			{
				foreach (IEdmProperty edmProperty in declaredProperties)
				{
					model.LoadEpmAnnotations(edmProperty, odataEntityPropertyMappingCollection, text, edmProperty);
				}
			}
			model.SetAnnotationValue(entityType, odataEntityPropertyMappingCollection);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x000387B8 File Offset: 0x000369B8
		private static void LoadEpmAnnotations(this IEdmModel model, IEdmElement annotatable, ODataEntityPropertyMappingCollection mappings, string typeName, IEdmProperty property)
		{
			IEnumerable<EpmExtensionMethods.EpmAnnotationValues> enumerable = model.ParseSerializableEpmAnnotations(annotatable, typeName, property);
			if (enumerable != null)
			{
				foreach (EpmExtensionMethods.EpmAnnotationValues epmAnnotationValues in enumerable)
				{
					EntityPropertyMappingAttribute entityPropertyMappingAttribute = EpmExtensionMethods.ValidateAnnotationValues(epmAnnotationValues, typeName, property);
					mappings.Add(entityPropertyMappingAttribute);
				}
			}
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x00038818 File Offset: 0x00036A18
		private static SyndicationItemProperty MapTargetPathToSyndicationProperty(string targetPath)
		{
			SyndicationItemProperty syndicationItemProperty;
			if (!EpmExtensionMethods.TargetPathToSyndicationItemMap.TryGetValue(targetPath, ref syndicationItemProperty))
			{
				return SyndicationItemProperty.CustomProperty;
			}
			return syndicationItemProperty;
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x00038838 File Offset: 0x00036A38
		private static string ToAttributeValue(this SyndicationTextContentKind contentKind)
		{
			switch (contentKind)
			{
			case SyndicationTextContentKind.Html:
				return "html";
			case SyndicationTextContentKind.Xhtml:
				return "xhtml";
			default:
				return "text";
			}
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0003886C File Offset: 0x00036A6C
		private static string ToAttributeValue(this SyndicationItemProperty syndicationItemProperty)
		{
			switch (syndicationItemProperty)
			{
			case SyndicationItemProperty.AuthorEmail:
				return "SyndicationAuthorEmail";
			case SyndicationItemProperty.AuthorName:
				return "SyndicationAuthorName";
			case SyndicationItemProperty.AuthorUri:
				return "SyndicationAuthorUri";
			case SyndicationItemProperty.ContributorEmail:
				return "SyndicationContributorEmail";
			case SyndicationItemProperty.ContributorName:
				return "SyndicationContributorName";
			case SyndicationItemProperty.ContributorUri:
				return "SyndicationContributorUri";
			case SyndicationItemProperty.Updated:
				return "SyndicationUpdated";
			case SyndicationItemProperty.Published:
				return "SyndicationPublished";
			case SyndicationItemProperty.Rights:
				return "SyndicationRights";
			case SyndicationItemProperty.Summary:
				return "SyndicationSummary";
			case SyndicationItemProperty.Title:
				return "SyndicationTitle";
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.EpmExtensionMethods_ToAttributeValue_SyndicationItemProperty));
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x00038908 File Offset: 0x00036B08
		private static SyndicationTextContentKind MapContentKindToSyndicationTextContentKind(string contentKind, string attributeSuffix, string typeName, string propertyName)
		{
			if (contentKind != null)
			{
				if (contentKind == "text")
				{
					return SyndicationTextContentKind.Plaintext;
				}
				if (contentKind == "html")
				{
					return SyndicationTextContentKind.Html;
				}
				if (contentKind == "xhtml")
				{
					return SyndicationTextContentKind.Xhtml;
				}
			}
			string text = ((propertyName == null) ? Strings.EpmExtensionMethods_InvalidTargetTextContentKindOnType("FC_ContentKind" + attributeSuffix, typeName) : Strings.EpmExtensionMethods_InvalidTargetTextContentKindOnProperty("FC_ContentKind" + attributeSuffix, propertyName, typeName));
			throw new ODataException(text);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x00038978 File Offset: 0x00036B78
		private static IEnumerable<EpmExtensionMethods.EpmAnnotationValues> ParseSerializableEpmAnnotations(this IEdmModel model, IEdmElement annotatable, string typeName, IEdmProperty property)
		{
			Dictionary<string, EpmExtensionMethods.EpmAnnotationValues> dictionary = null;
			IEnumerable<IEdmDirectValueAnnotation> odataAnnotations = model.GetODataAnnotations(annotatable);
			if (odataAnnotations != null)
			{
				foreach (IEdmDirectValueAnnotation edmDirectValueAnnotation in odataAnnotations)
				{
					string text;
					string text2;
					if (edmDirectValueAnnotation.IsEpmAnnotation(out text, out text2))
					{
						string text3 = EpmExtensionMethods.ConvertEdmAnnotationValue(edmDirectValueAnnotation);
						if (dictionary == null)
						{
							dictionary = new Dictionary<string, EpmExtensionMethods.EpmAnnotationValues>(StringComparer.Ordinal);
						}
						EpmExtensionMethods.EpmAnnotationValues epmAnnotationValues;
						if (!dictionary.TryGetValue(text2, ref epmAnnotationValues))
						{
							epmAnnotationValues = new EpmExtensionMethods.EpmAnnotationValues
							{
								AttributeSuffix = text2
							};
							dictionary[text2] = epmAnnotationValues;
						}
						if (EpmExtensionMethods.NamesMatchByReference("FC_TargetPath", text))
						{
							epmAnnotationValues.TargetPath = text3;
						}
						else if (EpmExtensionMethods.NamesMatchByReference("FC_SourcePath", text))
						{
							epmAnnotationValues.SourcePath = text3;
						}
						else if (EpmExtensionMethods.NamesMatchByReference("FC_KeepInContent", text))
						{
							epmAnnotationValues.KeepInContent = text3;
						}
						else if (EpmExtensionMethods.NamesMatchByReference("FC_ContentKind", text))
						{
							epmAnnotationValues.ContentKind = text3;
						}
						else if (EpmExtensionMethods.NamesMatchByReference("FC_NsUri", text))
						{
							epmAnnotationValues.NamespaceUri = text3;
						}
						else
						{
							if (!EpmExtensionMethods.NamesMatchByReference("FC_NsPrefix", text))
							{
								throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataUtils_ParseSerializableEpmAnnotations_UnreachableCodePath));
							}
							epmAnnotationValues.NamespacePrefix = text3;
						}
					}
				}
				if (dictionary != null)
				{
					foreach (EpmExtensionMethods.EpmAnnotationValues epmAnnotationValues2 in dictionary.Values)
					{
						string sourcePath = epmAnnotationValues2.SourcePath;
						if (sourcePath == null)
						{
							if (property == null)
							{
								string text4 = "FC_SourcePath" + epmAnnotationValues2.AttributeSuffix;
								throw new ODataException(Strings.EpmExtensionMethods_MissingAttributeOnType(text4, typeName));
							}
							epmAnnotationValues2.SourcePath = property.Name;
						}
						else if (property != null && !property.Type.IsODataPrimitiveTypeKind())
						{
							epmAnnotationValues2.SourcePath = property.Name + "/" + sourcePath;
						}
					}
				}
			}
			if (dictionary != null)
			{
				return dictionary.Values;
			}
			return null;
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x00038B9C File Offset: 0x00036D9C
		private static EntityPropertyMappingAttribute ValidateAnnotationValues(EpmExtensionMethods.EpmAnnotationValues annotationValues, string typeName, IEdmProperty property)
		{
			if (annotationValues.TargetPath == null)
			{
				string text = "FC_TargetPath" + annotationValues.AttributeSuffix;
				string text2 = ((property == null) ? Strings.EpmExtensionMethods_MissingAttributeOnType(text, typeName) : Strings.EpmExtensionMethods_MissingAttributeOnProperty(text, property.Name, typeName));
				throw new ODataException(text2);
			}
			bool flag = true;
			if (annotationValues.KeepInContent != null && !bool.TryParse(annotationValues.KeepInContent, ref flag))
			{
				string text3 = "FC_KeepInContent" + annotationValues.AttributeSuffix;
				throw new InvalidOperationException((property == null) ? Strings.EpmExtensionMethods_InvalidKeepInContentOnType(text3, typeName) : Strings.EpmExtensionMethods_InvalidKeepInContentOnProperty(text3, property.Name, typeName));
			}
			SyndicationItemProperty syndicationItemProperty = EpmExtensionMethods.MapTargetPathToSyndicationProperty(annotationValues.TargetPath);
			EntityPropertyMappingAttribute entityPropertyMappingAttribute;
			if (syndicationItemProperty == SyndicationItemProperty.CustomProperty)
			{
				if (annotationValues.ContentKind != null)
				{
					string text4 = "FC_ContentKind" + annotationValues.AttributeSuffix;
					string text5 = ((property == null) ? Strings.EpmExtensionMethods_AttributeNotAllowedForCustomMappingOnType(text4, typeName) : Strings.EpmExtensionMethods_AttributeNotAllowedForCustomMappingOnProperty(text4, property.Name, typeName));
					throw new ODataException(text5);
				}
				entityPropertyMappingAttribute = new EntityPropertyMappingAttribute(annotationValues.SourcePath, annotationValues.TargetPath, annotationValues.NamespacePrefix, annotationValues.NamespaceUri, flag);
			}
			else
			{
				if (annotationValues.NamespaceUri != null)
				{
					string text6 = "FC_NsUri" + annotationValues.AttributeSuffix;
					string text7 = ((property == null) ? Strings.EpmExtensionMethods_AttributeNotAllowedForAtomPubMappingOnType(text6, typeName) : Strings.EpmExtensionMethods_AttributeNotAllowedForAtomPubMappingOnProperty(text6, property.Name, typeName));
					throw new ODataException(text7);
				}
				if (annotationValues.NamespacePrefix != null)
				{
					string text8 = "FC_NsPrefix" + annotationValues.AttributeSuffix;
					string text9 = ((property == null) ? Strings.EpmExtensionMethods_AttributeNotAllowedForAtomPubMappingOnType(text8, typeName) : Strings.EpmExtensionMethods_AttributeNotAllowedForAtomPubMappingOnProperty(text8, property.Name, typeName));
					throw new ODataException(text9);
				}
				SyndicationTextContentKind syndicationTextContentKind = SyndicationTextContentKind.Plaintext;
				if (annotationValues.ContentKind != null)
				{
					syndicationTextContentKind = EpmExtensionMethods.MapContentKindToSyndicationTextContentKind(annotationValues.ContentKind, annotationValues.AttributeSuffix, typeName, (property == null) ? null : property.Name);
				}
				entityPropertyMappingAttribute = new EntityPropertyMappingAttribute(annotationValues.SourcePath, syndicationItemProperty, syndicationTextContentKind, flag);
			}
			return entityPropertyMappingAttribute;
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x00038D5E File Offset: 0x00036F5E
		private static void RemoveEpmCache(this IEdmModel model, IEdmEntityType entityType)
		{
			model.SetAnnotationValue(entityType, null);
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x00038D68 File Offset: 0x00036F68
		private static bool IsEpmAnnotation(this IEdmDirectValueAnnotation annotation)
		{
			string text;
			string text2;
			return annotation.IsEpmAnnotation(out text, out text2);
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x00038D80 File Offset: 0x00036F80
		private static bool IsEpmAnnotation(this IEdmDirectValueAnnotation annotation, out string baseName, out string suffix)
		{
			string name = annotation.Name;
			for (int i = 0; i < EpmExtensionMethods.EpmAnnotationBaseNames.Length; i++)
			{
				string text = EpmExtensionMethods.EpmAnnotationBaseNames[i];
				if (name.StartsWith(text, 4))
				{
					baseName = text;
					suffix = name.Substring(text.Length);
					return true;
				}
			}
			baseName = null;
			suffix = null;
			return false;
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x00038DD4 File Offset: 0x00036FD4
		private static string ConvertEdmAnnotationValue(IEdmDirectValueAnnotation annotation)
		{
			object value = annotation.Value;
			if (value == null)
			{
				return null;
			}
			IEdmStringValue edmStringValue = value as IEdmStringValue;
			if (edmStringValue != null)
			{
				return edmStringValue.Value;
			}
			throw new ODataException(Strings.EpmExtensionMethods_CannotConvertEdmAnnotationValue(annotation.NamespaceUri, annotation.Name, annotation.GetType().FullName));
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x00038E1F File Offset: 0x0003701F
		private static bool NamesMatchByReference(string first, string second)
		{
			return object.ReferenceEquals(first, second);
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x00038E28 File Offset: 0x00037028
		private static bool HasOwnOrInheritedEpm(this IEdmModel model, IEdmEntityType entityType)
		{
			if (entityType == null)
			{
				return false;
			}
			if (model.GetAnnotationValue(entityType) != null)
			{
				return true;
			}
			EpmExtensionMethods.LoadEpmAnnotations(model, entityType);
			return model.GetAnnotationValue(entityType) != null || model.HasOwnOrInheritedEpm(entityType.BaseEntityType());
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x00038E58 File Offset: 0x00037058
		private static IEdmDirectValueAnnotationBinding GetODataAnnotationBinding(IEdmElement annotatable, string localName, string value)
		{
			IEdmStringValue edmStringValue = null;
			if (value != null)
			{
				IEdmStringTypeReference @string = EdmCoreModel.Instance.GetString(true);
				edmStringValue = new EdmStringConstant(@string, value);
			}
			return new EdmDirectValueAnnotationBinding(annotatable, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", localName, edmStringValue);
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x00038E8C File Offset: 0x0003708C
		private static ODataEntityPropertyMappingCache EnsureEpmCacheInternal(IEdmModel model, IEdmEntityType entityType, int maxMappingCount, out bool cacheModified)
		{
			cacheModified = false;
			if (entityType == null)
			{
				return null;
			}
			IEdmEntityType edmEntityType = entityType.BaseEntityType();
			ODataEntityPropertyMappingCache odataEntityPropertyMappingCache = null;
			if (edmEntityType != null)
			{
				odataEntityPropertyMappingCache = EpmExtensionMethods.EnsureEpmCacheInternal(model, edmEntityType, maxMappingCount, out cacheModified);
			}
			ODataEntityPropertyMappingCache odataEntityPropertyMappingCache2 = model.GetEpmCache(entityType);
			if (model.HasOwnOrInheritedEpm(entityType))
			{
				ODataEntityPropertyMappingCollection entityPropertyMappings = model.GetEntityPropertyMappings(entityType);
				bool flag = odataEntityPropertyMappingCache2 == null || cacheModified || odataEntityPropertyMappingCache2.IsDirty(entityPropertyMappings);
				if (!flag)
				{
					return odataEntityPropertyMappingCache2;
				}
				cacheModified = true;
				int num = ValidationUtils.ValidateTotalEntityPropertyMappingCount(odataEntityPropertyMappingCache, entityPropertyMappings, maxMappingCount);
				odataEntityPropertyMappingCache2 = new ODataEntityPropertyMappingCache(entityPropertyMappings, model, num);
				try
				{
					odataEntityPropertyMappingCache2.BuildEpmForType(entityType, entityType);
					odataEntityPropertyMappingCache2.EpmSourceTree.Validate(entityType);
					model.SetAnnotationValue(entityType, odataEntityPropertyMappingCache2);
					return odataEntityPropertyMappingCache2;
				}
				catch
				{
					model.RemoveEpmCache(entityType);
					throw;
				}
			}
			if (odataEntityPropertyMappingCache2 != null)
			{
				cacheModified = true;
				model.RemoveEpmCache(entityType);
			}
			return odataEntityPropertyMappingCache2;
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x00038F44 File Offset: 0x00037144
		// Note: this type is marked as 'beforefieldinit'.
		static EpmExtensionMethods()
		{
			Dictionary<string, SyndicationItemProperty> dictionary = new Dictionary<string, SyndicationItemProperty>(StringComparer.OrdinalIgnoreCase);
			dictionary.Add("SyndicationAuthorEmail", SyndicationItemProperty.AuthorEmail);
			dictionary.Add("SyndicationAuthorName", SyndicationItemProperty.AuthorName);
			dictionary.Add("SyndicationAuthorUri", SyndicationItemProperty.AuthorUri);
			dictionary.Add("SyndicationContributorEmail", SyndicationItemProperty.ContributorEmail);
			dictionary.Add("SyndicationContributorName", SyndicationItemProperty.ContributorName);
			dictionary.Add("SyndicationContributorUri", SyndicationItemProperty.ContributorUri);
			dictionary.Add("SyndicationUpdated", SyndicationItemProperty.Updated);
			dictionary.Add("SyndicationPublished", SyndicationItemProperty.Published);
			dictionary.Add("SyndicationRights", SyndicationItemProperty.Rights);
			dictionary.Add("SyndicationSummary", SyndicationItemProperty.Summary);
			dictionary.Add("SyndicationTitle", SyndicationItemProperty.Title);
			EpmExtensionMethods.TargetPathToSyndicationItemMap = dictionary;
		}

		// Token: 0x040005ED RID: 1517
		private static readonly string[] EpmAnnotationBaseNames = new string[] { "FC_TargetPath", "FC_SourcePath", "FC_KeepInContent", "FC_ContentKind", "FC_NsUri", "FC_NsPrefix" };

		// Token: 0x040005EE RID: 1518
		private static readonly Dictionary<string, SyndicationItemProperty> TargetPathToSyndicationItemMap;

		// Token: 0x02000210 RID: 528
		private sealed class EpmAnnotationValues
		{
			// Token: 0x17000365 RID: 869
			// (get) Token: 0x06000F6A RID: 3946 RVA: 0x00039026 File Offset: 0x00037226
			// (set) Token: 0x06000F6B RID: 3947 RVA: 0x0003902E File Offset: 0x0003722E
			internal string SourcePath { get; set; }

			// Token: 0x17000366 RID: 870
			// (get) Token: 0x06000F6C RID: 3948 RVA: 0x00039037 File Offset: 0x00037237
			// (set) Token: 0x06000F6D RID: 3949 RVA: 0x0003903F File Offset: 0x0003723F
			internal string TargetPath { get; set; }

			// Token: 0x17000367 RID: 871
			// (get) Token: 0x06000F6E RID: 3950 RVA: 0x00039048 File Offset: 0x00037248
			// (set) Token: 0x06000F6F RID: 3951 RVA: 0x00039050 File Offset: 0x00037250
			internal string KeepInContent { get; set; }

			// Token: 0x17000368 RID: 872
			// (get) Token: 0x06000F70 RID: 3952 RVA: 0x00039059 File Offset: 0x00037259
			// (set) Token: 0x06000F71 RID: 3953 RVA: 0x00039061 File Offset: 0x00037261
			internal string ContentKind { get; set; }

			// Token: 0x17000369 RID: 873
			// (get) Token: 0x06000F72 RID: 3954 RVA: 0x0003906A File Offset: 0x0003726A
			// (set) Token: 0x06000F73 RID: 3955 RVA: 0x00039072 File Offset: 0x00037272
			internal string NamespaceUri { get; set; }

			// Token: 0x1700036A RID: 874
			// (get) Token: 0x06000F74 RID: 3956 RVA: 0x0003907B File Offset: 0x0003727B
			// (set) Token: 0x06000F75 RID: 3957 RVA: 0x00039083 File Offset: 0x00037283
			internal string NamespacePrefix { get; set; }

			// Token: 0x1700036B RID: 875
			// (get) Token: 0x06000F76 RID: 3958 RVA: 0x0003908C File Offset: 0x0003728C
			// (set) Token: 0x06000F77 RID: 3959 RVA: 0x00039094 File Offset: 0x00037294
			internal string AttributeSuffix { get; set; }
		}
	}
}
