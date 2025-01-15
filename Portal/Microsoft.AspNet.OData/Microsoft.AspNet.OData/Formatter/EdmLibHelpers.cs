using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Query.Expressions;
using Microsoft.AspNet.OData.Routing;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;
using Microsoft.OData.Edm.Vocabularies.V1;
using Microsoft.OData.UriParser;
using Microsoft.Spatial;

namespace Microsoft.AspNet.OData.Formatter
{
	// Token: 0x02000196 RID: 406
	internal static class EdmLibHelpers
	{
		// Token: 0x06000D32 RID: 3378 RVA: 0x00034724 File Offset: 0x00032924
		public static IEdmType GetEdmType(this IEdmModel edmModel, Type clrType)
		{
			if (edmModel == null)
			{
				throw Error.ArgumentNull("edmModel");
			}
			if (clrType == null)
			{
				throw Error.ArgumentNull("clrType");
			}
			return EdmLibHelpers.GetEdmType(edmModel, clrType, true);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00034750 File Offset: 0x00032950
		private static IEdmType GetEdmType(IEdmModel edmModel, Type clrType, bool testCollections)
		{
			IEdmPrimitiveType edmPrimitiveTypeOrNull = EdmLibHelpers.GetEdmPrimitiveTypeOrNull(clrType);
			if (edmPrimitiveTypeOrNull != null)
			{
				return edmPrimitiveTypeOrNull;
			}
			if (testCollections)
			{
				Type type = EdmLibHelpers.ExtractGenericInterface(clrType, typeof(IEnumerable<>));
				if (type != null)
				{
					Type type2 = type.GetGenericArguments()[0];
					Type type3;
					if (EdmLibHelpers.IsSelectExpandWrapper(type2, out type3))
					{
						type2 = type3;
					}
					IEdmType edmType3 = EdmLibHelpers.GetEdmType(edmModel, type2, false);
					if (edmType3 != null)
					{
						return new EdmCollectionType(edmType3.ToEdmTypeReference(EdmLibHelpers.IsNullable(type2)));
					}
				}
			}
			Type underlyingTypeOrSelf = TypeHelper.GetUnderlyingTypeOrSelf(clrType);
			if (TypeHelper.IsEnum(underlyingTypeOrSelf))
			{
				clrType = underlyingTypeOrSelf;
			}
			IEdmType edmType2 = (from edmType in edmModel.SchemaElements.OfType<IEdmType>()
				select new
				{
					EdmType = edmType,
					Annotation = edmModel.GetAnnotationValue(edmType)
				} into tuple
				where tuple.Annotation != null && tuple.Annotation.ClrType == clrType
				select tuple.EdmType).SingleOrDefault<IEdmType>();
			edmType2 = edmType2 ?? edmModel.FindType(clrType.EdmFullName());
			if (TypeHelper.GetBaseType(clrType) != null)
			{
				edmType2 = edmType2 ?? EdmLibHelpers.GetEdmType(edmModel, TypeHelper.GetBaseType(clrType), testCollections);
			}
			return edmType2;
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x000348AC File Offset: 0x00032AAC
		public static IEdmTypeReference GetEdmTypeReference(this IEdmModel edmModel, Type clrType)
		{
			IEdmType edmType = edmModel.GetEdmType(clrType);
			if (edmType != null)
			{
				bool flag = EdmLibHelpers.IsNullable(clrType);
				return edmType.ToEdmTypeReference(flag);
			}
			return null;
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x000348D4 File Offset: 0x00032AD4
		public static IEdmTypeReference ToEdmTypeReference(this IEdmType edmType, bool isNullable)
		{
			switch (edmType.TypeKind)
			{
			case EdmTypeKind.Primitive:
				return EdmLibHelpers._coreModel.GetPrimitive((edmType as IEdmPrimitiveType).PrimitiveKind, isNullable);
			case EdmTypeKind.Entity:
				return new EdmEntityTypeReference(edmType as IEdmEntityType, isNullable);
			case EdmTypeKind.Complex:
				return new EdmComplexTypeReference(edmType as IEdmComplexType, isNullable);
			case EdmTypeKind.Collection:
				return new EdmCollectionTypeReference(edmType as IEdmCollectionType);
			case EdmTypeKind.EntityReference:
				return new EdmEntityReferenceTypeReference(edmType as IEdmEntityReferenceType, isNullable);
			case EdmTypeKind.Enum:
				return new EdmEnumTypeReference(edmType as IEdmEnumType, isNullable);
			default:
				throw Error.NotSupported(SRResources.EdmTypeNotSupported, new object[] { edmType.ToTraceString() });
			}
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0003497A File Offset: 0x00032B7A
		public static IEdmCollectionType GetCollection(this IEdmEntityType entityType)
		{
			return new EdmCollectionType(new EdmEntityTypeReference(entityType, false));
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00034988 File Offset: 0x00032B88
		public static Type GetClrType(IEdmTypeReference edmTypeReference, IEdmModel edmModel)
		{
			return EdmLibHelpers.GetClrType(edmTypeReference, edmModel, WebApiAssembliesResolver.Default);
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x00034998 File Offset: 0x00032B98
		public static Type GetClrType(IEdmTypeReference edmTypeReference, IEdmModel edmModel, IWebApiAssembliesResolver assembliesResolver)
		{
			if (edmTypeReference == null)
			{
				throw Error.ArgumentNull("edmTypeReference");
			}
			Type type = (from kvp in EdmLibHelpers._builtInTypesMapping
				where edmTypeReference.Definition.IsEquivalentTo(kvp.Value) && (!edmTypeReference.IsNullable || EdmLibHelpers.IsNullable(kvp.Key))
				select kvp.Key).FirstOrDefault<Type>();
			if (type != null)
			{
				return type;
			}
			Type clrType = EdmLibHelpers.GetClrType(edmTypeReference.Definition, edmModel, assembliesResolver);
			if (clrType != null && TypeHelper.IsEnum(clrType) && edmTypeReference.IsNullable)
			{
				return TypeHelper.ToNullable(clrType);
			}
			return clrType;
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00034A49 File Offset: 0x00032C49
		public static Type GetClrType(IEdmType edmType, IEdmModel edmModel)
		{
			return EdmLibHelpers.GetClrType(edmType, edmModel, WebApiAssembliesResolver.Default);
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00034A58 File Offset: 0x00032C58
		public static Type GetClrType(IEdmType edmType, IEdmModel edmModel, IWebApiAssembliesResolver assembliesResolver)
		{
			IEdmSchemaType edmSchemaType = edmType as IEdmSchemaType;
			ClrTypeAnnotation annotationValue = edmModel.GetAnnotationValue(edmSchemaType);
			if (annotationValue != null)
			{
				return annotationValue.ClrType;
			}
			string text = edmSchemaType.FullName();
			IEnumerable<Type> matchingTypes = EdmLibHelpers.GetMatchingTypes(text, assembliesResolver);
			if (matchingTypes.Count<Type>() > 1)
			{
				string text2 = "edmTypeReference";
				string multipleMatchingClrTypesForEdmType = SRResources.MultipleMatchingClrTypesForEdmType;
				object[] array = new object[2];
				array[0] = text;
				array[1] = string.Join(",", matchingTypes.Select((Type type) => type.AssemblyQualifiedName));
				throw Error.Argument(text2, multipleMatchingClrTypesForEdmType, array);
			}
			edmModel.SetAnnotationValue(edmSchemaType, new ClrTypeAnnotation(matchingTypes.SingleOrDefault<Type>()));
			return matchingTypes.SingleOrDefault<Type>();
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00034AFC File Offset: 0x00032CFC
		public static bool IsNotFilterable(IEdmProperty edmProperty, IEdmProperty pathEdmProperty, IEdmStructuredType pathEdmStructuredType, IEdmModel edmModel, bool enableFilter)
		{
			QueryableRestrictionsAnnotation propertyRestrictions = EdmLibHelpers.GetPropertyRestrictions(edmProperty, edmModel);
			if (propertyRestrictions != null && propertyRestrictions.Restrictions.NotFilterable)
			{
				return true;
			}
			if (pathEdmStructuredType == null)
			{
				pathEdmStructuredType = edmProperty.DeclaringType;
			}
			ModelBoundQuerySettings modelBoundQuerySettings = EdmLibHelpers.GetModelBoundQuerySettings(pathEdmProperty, pathEdmStructuredType, edmModel, null);
			if (!enableFilter)
			{
				return !modelBoundQuerySettings.Filterable(edmProperty.Name);
			}
			bool flag;
			if (modelBoundQuerySettings.FilterConfigurations.TryGetValue(edmProperty.Name, out flag))
			{
				return !flag;
			}
			bool? defaultEnableFilter = modelBoundQuerySettings.DefaultEnableFilter;
			bool flag2 = false;
			return (defaultEnableFilter.GetValueOrDefault() == flag2) & (defaultEnableFilter != null);
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00034B84 File Offset: 0x00032D84
		public static bool IsNotSortable(IEdmProperty edmProperty, IEdmProperty pathEdmProperty, IEdmStructuredType pathEdmStructuredType, IEdmModel edmModel, bool enableOrderBy)
		{
			QueryableRestrictionsAnnotation propertyRestrictions = EdmLibHelpers.GetPropertyRestrictions(edmProperty, edmModel);
			if (propertyRestrictions != null && propertyRestrictions.Restrictions.NotSortable)
			{
				return true;
			}
			if (pathEdmStructuredType == null)
			{
				pathEdmStructuredType = edmProperty.DeclaringType;
			}
			ModelBoundQuerySettings modelBoundQuerySettings = EdmLibHelpers.GetModelBoundQuerySettings(pathEdmProperty, pathEdmStructuredType, edmModel, null);
			if (!enableOrderBy)
			{
				return !modelBoundQuerySettings.Sortable(edmProperty.Name);
			}
			bool flag;
			if (modelBoundQuerySettings.OrderByConfigurations.TryGetValue(edmProperty.Name, out flag))
			{
				return !flag;
			}
			bool? defaultEnableOrderBy = modelBoundQuerySettings.DefaultEnableOrderBy;
			bool flag2 = false;
			return (defaultEnableOrderBy.GetValueOrDefault() == flag2) & (defaultEnableOrderBy != null);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00034C0C File Offset: 0x00032E0C
		public static bool IsNotSelectable(IEdmProperty edmProperty, IEdmProperty pathEdmProperty, IEdmStructuredType pathEdmStructuredType, IEdmModel edmModel, bool enableSelect)
		{
			if (pathEdmStructuredType == null)
			{
				pathEdmStructuredType = edmProperty.DeclaringType;
			}
			ModelBoundQuerySettings modelBoundQuerySettings = EdmLibHelpers.GetModelBoundQuerySettings(pathEdmProperty, pathEdmStructuredType, edmModel, null);
			if (!enableSelect)
			{
				return !modelBoundQuerySettings.Selectable(edmProperty.Name);
			}
			SelectExpandType selectExpandType;
			if (modelBoundQuerySettings.SelectConfigurations.TryGetValue(edmProperty.Name, out selectExpandType))
			{
				return selectExpandType == SelectExpandType.Disabled;
			}
			SelectExpandType? defaultSelectType = modelBoundQuerySettings.DefaultSelectType;
			SelectExpandType selectExpandType2 = SelectExpandType.Disabled;
			return (defaultSelectType.GetValueOrDefault() == selectExpandType2) & (defaultSelectType != null);
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00034C78 File Offset: 0x00032E78
		public static bool IsNotNavigable(IEdmProperty edmProperty, IEdmModel edmModel)
		{
			QueryableRestrictionsAnnotation propertyRestrictions = EdmLibHelpers.GetPropertyRestrictions(edmProperty, edmModel);
			return propertyRestrictions != null && propertyRestrictions.Restrictions.NotNavigable;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00034CA0 File Offset: 0x00032EA0
		public static bool IsNotExpandable(IEdmProperty edmProperty, IEdmModel edmModel)
		{
			QueryableRestrictionsAnnotation propertyRestrictions = EdmLibHelpers.GetPropertyRestrictions(edmProperty, edmModel);
			return propertyRestrictions != null && propertyRestrictions.Restrictions.NotExpandable;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00034CC5 File Offset: 0x00032EC5
		public static bool IsAutoSelect(IEdmProperty property, IEdmProperty pathProperty, IEdmStructuredType pathStructuredType, IEdmModel edmModel, ModelBoundQuerySettings querySettings = null)
		{
			if (querySettings == null)
			{
				querySettings = EdmLibHelpers.GetModelBoundQuerySettings(pathProperty, pathStructuredType, edmModel, null);
			}
			return querySettings != null && querySettings.IsAutomaticSelect(property.Name);
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00034CEC File Offset: 0x00032EEC
		public static bool IsAutoExpand(IEdmProperty navigationProperty, IEdmProperty pathProperty, IEdmStructuredType pathStructuredType, IEdmModel edmModel, bool isSelectPresent = false, ModelBoundQuerySettings querySettings = null)
		{
			QueryableRestrictionsAnnotation propertyRestrictions = EdmLibHelpers.GetPropertyRestrictions(navigationProperty, edmModel);
			if (propertyRestrictions != null && propertyRestrictions.Restrictions.AutoExpand)
			{
				return !propertyRestrictions.Restrictions.DisableAutoExpandWhenSelectIsPresent || !isSelectPresent;
			}
			if (querySettings == null)
			{
				querySettings = EdmLibHelpers.GetModelBoundQuerySettings(pathProperty, pathStructuredType, edmModel, null);
			}
			return querySettings != null && querySettings.IsAutomaticExpand(navigationProperty.Name);
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x00034D4C File Offset: 0x00032F4C
		public static IEnumerable<IEdmNavigationProperty> GetAutoExpandNavigationProperties(IEdmProperty pathProperty, IEdmStructuredType pathStructuredType, IEdmModel edmModel, bool isSelectPresent = false, ModelBoundQuerySettings querySettings = null)
		{
			List<IEdmNavigationProperty> list = new List<IEdmNavigationProperty>();
			IEdmEntityType edmEntityType = pathStructuredType as IEdmEntityType;
			if (edmEntityType != null)
			{
				List<IEdmEntityType> list2 = new List<IEdmEntityType>();
				list2.Add(edmEntityType);
				list2.AddRange(EdmLibHelpers.GetAllDerivedEntityTypes(edmEntityType, edmModel));
				using (List<IEdmEntityType>.Enumerator enumerator = list2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IEdmEntityType entityType = enumerator.Current;
						IEnumerable<IEdmNavigationProperty> enumerable = ((entityType == edmEntityType) ? entityType.NavigationProperties() : entityType.DeclaredNavigationProperties());
						if (enumerable != null)
						{
							list.AddRange(enumerable.Where((IEdmNavigationProperty navigationProperty) => EdmLibHelpers.IsAutoExpand(navigationProperty, pathProperty, entityType, edmModel, isSelectPresent, querySettings)));
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00034E40 File Offset: 0x00033040
		public static IEnumerable<IEdmStructuralProperty> GetAutoSelectProperties(IEdmProperty pathProperty, IEdmStructuredType pathStructuredType, IEdmModel edmModel, ModelBoundQuerySettings querySettings = null)
		{
			List<IEdmStructuralProperty> list = new List<IEdmStructuralProperty>();
			IEdmEntityType edmEntityType = pathStructuredType as IEdmEntityType;
			if (edmEntityType != null)
			{
				List<IEdmEntityType> list2 = new List<IEdmEntityType>();
				list2.Add(edmEntityType);
				list2.AddRange(EdmLibHelpers.GetAllDerivedEntityTypes(edmEntityType, edmModel));
				using (List<IEdmEntityType>.Enumerator enumerator = list2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IEdmEntityType entityType = enumerator.Current;
						IEnumerable<IEdmStructuralProperty> enumerable = ((entityType == edmEntityType) ? entityType.StructuralProperties() : entityType.DeclaredStructuralProperties());
						if (enumerable != null)
						{
							list.AddRange(enumerable.Where((IEdmStructuralProperty property) => EdmLibHelpers.IsAutoSelect(property, pathProperty, entityType, edmModel, querySettings)));
						}
					}
					return list;
				}
			}
			if (pathStructuredType != null)
			{
				IEnumerable<IEdmStructuralProperty> enumerable2 = pathStructuredType.StructuralProperties();
				if (enumerable2 != null)
				{
					list.AddRange(enumerable2.Where((IEdmStructuralProperty property) => EdmLibHelpers.IsAutoSelect(property, pathProperty, pathStructuredType, edmModel, querySettings)));
				}
			}
			return list;
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00034F6C File Offset: 0x0003316C
		public static bool IsTopLimitExceeded(IEdmProperty property, IEdmStructuredType structuredType, IEdmModel edmModel, int top, DefaultQuerySettings defaultQuerySettings, out int maxTop)
		{
			maxTop = 0;
			ModelBoundQuerySettings modelBoundQuerySettings = EdmLibHelpers.GetModelBoundQuerySettings(property, structuredType, edmModel, defaultQuerySettings);
			if (modelBoundQuerySettings != null)
			{
				int? maxTop2 = modelBoundQuerySettings.MaxTop;
				if ((top > maxTop2.GetValueOrDefault()) & (maxTop2 != null))
				{
					maxTop = modelBoundQuerySettings.MaxTop.Value;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00034FBC File Offset: 0x000331BC
		public static bool IsNotCountable(IEdmProperty property, IEdmStructuredType structuredType, IEdmModel edmModel, bool enableCount)
		{
			if (property != null)
			{
				QueryableRestrictionsAnnotation propertyRestrictions = EdmLibHelpers.GetPropertyRestrictions(property, edmModel);
				if (propertyRestrictions != null && propertyRestrictions.Restrictions.NotCountable)
				{
					return true;
				}
			}
			ModelBoundQuerySettings modelBoundQuerySettings = EdmLibHelpers.GetModelBoundQuerySettings(property, structuredType, edmModel, null);
			if (modelBoundQuerySettings != null)
			{
				if (modelBoundQuerySettings.Countable != null || enableCount)
				{
					bool? countable = modelBoundQuerySettings.Countable;
					bool flag = false;
					if (!((countable.GetValueOrDefault() == flag) & (countable != null)))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00035028 File Offset: 0x00033228
		public static bool IsExpandable(string propertyName, IEdmProperty property, IEdmStructuredType structuredType, IEdmModel edmModel, out ExpandConfiguration expandConfiguration)
		{
			expandConfiguration = null;
			ModelBoundQuerySettings modelBoundQuerySettings = EdmLibHelpers.GetModelBoundQuerySettings(property, structuredType, edmModel, null);
			if (modelBoundQuerySettings != null)
			{
				bool flag = modelBoundQuerySettings.Expandable(propertyName);
				if (!modelBoundQuerySettings.ExpandConfigurations.TryGetValue(propertyName, out expandConfiguration) && flag)
				{
					expandConfiguration = new ExpandConfiguration
					{
						ExpandType = modelBoundQuerySettings.DefaultExpandType.Value,
						MaxDepth = modelBoundQuerySettings.DefaultMaxDepth
					};
				}
				return flag;
			}
			return false;
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00035090 File Offset: 0x00033290
		public static ModelBoundQuerySettings GetModelBoundQuerySettings(IEdmProperty property, IEdmStructuredType structuredType, IEdmModel edmModel, DefaultQuerySettings defaultQuerySettings = null)
		{
			ModelBoundQuerySettings modelBoundQuerySettings = EdmLibHelpers.GetModelBoundQuerySettings<IEdmStructuredType>(structuredType, edmModel, defaultQuerySettings);
			if (property == null)
			{
				return modelBoundQuerySettings;
			}
			return EdmLibHelpers.GetMergedPropertyQuerySettings(EdmLibHelpers.GetModelBoundQuerySettings<IEdmProperty>(property, edmModel, defaultQuerySettings), modelBoundQuerySettings);
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x000350BC File Offset: 0x000332BC
		public static IEnumerable<IEdmEntityType> GetAllDerivedEntityTypes(IEdmEntityType entityType, IEdmModel edmModel)
		{
			List<IEdmEntityType> list = new List<IEdmEntityType>();
			if (entityType != null)
			{
				List<IEdmStructuredType> list2 = new List<IEdmStructuredType>();
				list2.Add(entityType);
				while (list2.Count > 0)
				{
					IEdmStructuredType edmStructuredType = list2[0];
					list.Add(edmStructuredType as IEdmEntityType);
					IEnumerable<IEdmStructuredType> enumerable = edmModel.FindDirectlyDerivedTypes(edmStructuredType);
					if (enumerable != null)
					{
						list2.AddRange(enumerable);
					}
					list2.RemoveAt(0);
				}
			}
			list.RemoveAt(0);
			return list;
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0003511F File Offset: 0x0003331F
		public static IEdmType GetElementType(IEdmTypeReference edmTypeReference)
		{
			if (edmTypeReference.IsCollection())
			{
				return edmTypeReference.AsCollection().ElementType().Definition;
			}
			return edmTypeReference.Definition;
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00035140 File Offset: 0x00033340
		public static void GetPropertyAndStructuredTypeFromPath(IEnumerable<ODataPathSegment> segments, out IEdmProperty property, out IEdmStructuredType structuredType, out string name)
		{
			property = null;
			structuredType = null;
			name = string.Empty;
			string text = string.Empty;
			if (segments != null)
			{
				foreach (ODataPathSegment odataPathSegment in segments.Reverse<ODataPathSegment>())
				{
					NavigationPropertySegment navigationPropertySegment = odataPathSegment as NavigationPropertySegment;
					if (navigationPropertySegment != null)
					{
						property = navigationPropertySegment.NavigationProperty;
						if (structuredType == null)
						{
							structuredType = navigationPropertySegment.NavigationProperty.ToEntityType();
						}
						name = navigationPropertySegment.NavigationProperty.Name + text;
						break;
					}
					PropertySegment propertySegment = odataPathSegment as PropertySegment;
					if (propertySegment != null)
					{
						property = propertySegment.Property;
						if (structuredType == null)
						{
							structuredType = EdmLibHelpers.GetElementType(property.Type) as IEdmStructuredType;
						}
						name = property.Name + text;
						break;
					}
					EntitySetSegment entitySetSegment = odataPathSegment as EntitySetSegment;
					if (entitySetSegment != null)
					{
						if (structuredType == null)
						{
							structuredType = entitySetSegment.EntitySet.EntityType();
						}
						name = entitySetSegment.EntitySet.Name + text;
						break;
					}
					TypeSegment typeSegment = odataPathSegment as TypeSegment;
					if (typeSegment != null)
					{
						structuredType = EdmLibHelpers.GetElementType(typeSegment.EdmType.ToEdmTypeReference(false)) as IEdmStructuredType;
						string text2 = "/";
						IEdmStructuredType edmStructuredType = structuredType;
						text = text2 + ((edmStructuredType != null) ? edmStructuredType.ToString() : null);
					}
				}
			}
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x000352A0 File Offset: 0x000334A0
		public static string GetClrPropertyName(IEdmProperty edmProperty, IEdmModel edmModel)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			if (edmModel == null)
			{
				throw Error.ArgumentNull("edmModel");
			}
			string text = edmProperty.Name;
			ClrPropertyInfoAnnotation annotationValue = edmModel.GetAnnotationValue(edmProperty);
			if (annotationValue != null)
			{
				PropertyInfo clrPropertyInfo = annotationValue.ClrPropertyInfo;
				if (clrPropertyInfo != null)
				{
					text = clrPropertyInfo.Name;
				}
			}
			return text;
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x000352F4 File Offset: 0x000334F4
		public static ClrEnumMemberAnnotation GetClrEnumMemberAnnotation(this IEdmModel edmModel, IEdmEnumType enumType)
		{
			if (edmModel == null)
			{
				throw Error.ArgumentNull("edmModel");
			}
			ClrEnumMemberAnnotation annotationValue = edmModel.GetAnnotationValue(enumType);
			if (annotationValue != null)
			{
				return annotationValue;
			}
			return null;
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00035320 File Offset: 0x00033520
		public static PropertyInfo GetDynamicPropertyDictionary(IEdmStructuredType edmType, IEdmModel edmModel)
		{
			if (edmType == null)
			{
				throw Error.ArgumentNull("edmType");
			}
			if (edmModel == null)
			{
				throw Error.ArgumentNull("edmModel");
			}
			DynamicPropertyDictionaryAnnotation annotationValue = edmModel.GetAnnotationValue(edmType);
			if (annotationValue != null)
			{
				return annotationValue.PropertyInfo;
			}
			return null;
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0003535C File Offset: 0x0003355C
		public static bool HasLength(EdmPrimitiveTypeKind primitiveTypeKind)
		{
			return primitiveTypeKind == EdmPrimitiveTypeKind.Binary || primitiveTypeKind == EdmPrimitiveTypeKind.String;
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00035369 File Offset: 0x00033569
		public static bool HasPrecision(EdmPrimitiveTypeKind primitiveTypeKind)
		{
			return primitiveTypeKind == EdmPrimitiveTypeKind.Decimal || primitiveTypeKind == EdmPrimitiveTypeKind.DateTimeOffset || primitiveTypeKind == EdmPrimitiveTypeKind.Duration || primitiveTypeKind == EdmPrimitiveTypeKind.TimeOfDay;
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00035380 File Offset: 0x00033580
		public static IEdmPrimitiveType GetEdmPrimitiveTypeOrNull(Type clrType)
		{
			IEdmPrimitiveType edmPrimitiveType;
			if (!EdmLibHelpers._builtInTypesMapping.TryGetValue(clrType, out edmPrimitiveType))
			{
				return null;
			}
			return edmPrimitiveType;
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x000353A0 File Offset: 0x000335A0
		public static IEdmPrimitiveTypeReference GetEdmPrimitiveTypeReferenceOrNull(Type clrType)
		{
			IEdmPrimitiveType edmPrimitiveTypeOrNull = EdmLibHelpers.GetEdmPrimitiveTypeOrNull(clrType);
			if (edmPrimitiveTypeOrNull == null)
			{
				return null;
			}
			return EdmLibHelpers._coreModel.GetPrimitive(edmPrimitiveTypeOrNull.PrimitiveKind, EdmLibHelpers.IsNullable(clrType));
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x000353D0 File Offset: 0x000335D0
		public static Type IsNonstandardEdmPrimitive(Type type, out bool isNonstandardEdmPrimitive)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReferenceOrNull = EdmLibHelpers.GetEdmPrimitiveTypeReferenceOrNull(type);
			if (edmPrimitiveTypeReferenceOrNull == null)
			{
				isNonstandardEdmPrimitive = false;
				return type;
			}
			Type clrType = EdmLibHelpers.GetClrType(edmPrimitiveTypeReferenceOrNull, EdmCoreModel.Instance);
			isNonstandardEdmPrimitive = type != clrType;
			return clrType;
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00035402 File Offset: 0x00033602
		public static string EdmName(this Type clrType)
		{
			return EdmLibHelpers.MangleClrTypeName(clrType);
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0003540A File Offset: 0x0003360A
		public static string EdmFullName(this Type clrType)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				clrType.Namespace,
				clrType.EdmName()
			});
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00035434 File Offset: 0x00033634
		public static IEnumerable<IEdmStructuralProperty> GetConcurrencyProperties(this IEdmModel model, IEdmNavigationSource navigationSource)
		{
			ConcurrencyPropertiesAnnotation concurrencyPropertiesAnnotation = model.GetAnnotationValue(model);
			if (concurrencyPropertiesAnnotation == null)
			{
				concurrencyPropertiesAnnotation = new ConcurrencyPropertiesAnnotation();
				model.SetAnnotationValue(model, concurrencyPropertiesAnnotation);
			}
			IEnumerable<IEdmStructuralProperty> enumerable;
			if (concurrencyPropertiesAnnotation.TryGetValue(navigationSource, out enumerable))
			{
				return enumerable;
			}
			IList<IEdmStructuralProperty> list = new List<IEdmStructuralProperty>();
			IEdmEntityType edmEntityType = navigationSource.EntityType();
			IEdmVocabularyAnnotatable edmVocabularyAnnotatable = navigationSource as IEdmVocabularyAnnotatable;
			if (edmVocabularyAnnotatable != null)
			{
				IEdmVocabularyAnnotation edmVocabularyAnnotation = model.FindVocabularyAnnotations(edmVocabularyAnnotatable, CoreVocabularyModel.ConcurrencyTerm).FirstOrDefault<IEdmVocabularyAnnotation>();
				if (edmVocabularyAnnotation != null)
				{
					IEdmCollectionExpression edmCollectionExpression = edmVocabularyAnnotation.Value as IEdmCollectionExpression;
					if (edmCollectionExpression != null)
					{
						foreach (IEdmExpression edmExpression in edmCollectionExpression.Elements)
						{
							IEdmPathExpression edmPathExpression = edmExpression as IEdmPathExpression;
							if (edmPathExpression != null)
							{
								string text = edmPathExpression.PathSegments.First<string>();
								IEdmStructuralProperty edmStructuralProperty = edmEntityType.FindProperty(text) as IEdmStructuralProperty;
								if (edmStructuralProperty != null)
								{
									list.Add(edmStructuralProperty);
								}
							}
						}
					}
				}
			}
			concurrencyPropertiesAnnotation[navigationSource] = list;
			return list;
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00035528 File Offset: 0x00033728
		public static bool IsDynamicTypeWrapper(Type type)
		{
			return type != null && typeof(DynamicTypeWrapper).IsAssignableFrom(type);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00035545 File Offset: 0x00033745
		public static bool IsNullable(Type type)
		{
			return !TypeHelper.IsValueType(type) || Nullable.GetUnderlyingType(type) != null;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00035560 File Offset: 0x00033760
		internal static IEdmTypeReference GetExpectedPayloadType(Type type, Microsoft.AspNet.OData.Routing.ODataPath path, IEdmModel model)
		{
			IEdmTypeReference edmTypeReference = null;
			if (typeof(IEdmObject).IsAssignableFrom(type))
			{
				IEdmType edmType = path.EdmType;
				if (edmType != null)
				{
					edmTypeReference = edmType.ToEdmTypeReference(false);
					if (edmTypeReference.TypeKind() == EdmTypeKind.Collection)
					{
						IEdmTypeReference edmTypeReference2 = edmTypeReference.AsCollection().ElementType();
						if (edmTypeReference2.IsEntity())
						{
							edmTypeReference = edmTypeReference2;
						}
					}
				}
			}
			else
			{
				EdmLibHelpers.TryGetInnerTypeForDelta(ref type);
				edmTypeReference = model.GetEdmTypeReference(type);
			}
			return edmTypeReference;
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x000355C5 File Offset: 0x000337C5
		internal static bool TryGetInnerTypeForDelta(ref Type type)
		{
			if (type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(Delta<>))
			{
				type = type.GetGenericArguments()[0];
				return true;
			}
			return false;
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x000355F8 File Offset: 0x000337F8
		private static ModelBoundQuerySettings GetMergedPropertyQuerySettings(ModelBoundQuerySettings propertyQuerySettings, ModelBoundQuerySettings propertyTypeQuerySettings)
		{
			ModelBoundQuerySettings modelBoundQuerySettings = new ModelBoundQuerySettings(propertyQuerySettings);
			if (propertyTypeQuerySettings != null)
			{
				if (modelBoundQuerySettings.PageSize == null)
				{
					modelBoundQuerySettings.PageSize = propertyTypeQuerySettings.PageSize;
				}
				int? num = modelBoundQuerySettings.MaxTop;
				int num2 = 0;
				if ((num.GetValueOrDefault() == num2) & (num != null))
				{
					num = propertyTypeQuerySettings.MaxTop;
					num2 = 0;
					if (!((num.GetValueOrDefault() == num2) & (num != null)))
					{
						modelBoundQuerySettings.MaxTop = propertyTypeQuerySettings.MaxTop;
					}
				}
				if (modelBoundQuerySettings.Countable == null)
				{
					modelBoundQuerySettings.Countable = propertyTypeQuerySettings.Countable;
				}
				if (modelBoundQuerySettings.OrderByConfigurations.Count == 0 && modelBoundQuerySettings.DefaultEnableOrderBy == null)
				{
					modelBoundQuerySettings.CopyOrderByConfigurations(propertyTypeQuerySettings.OrderByConfigurations);
					modelBoundQuerySettings.DefaultEnableOrderBy = propertyTypeQuerySettings.DefaultEnableOrderBy;
				}
				if (modelBoundQuerySettings.FilterConfigurations.Count == 0 && modelBoundQuerySettings.DefaultEnableFilter == null)
				{
					modelBoundQuerySettings.CopyFilterConfigurations(propertyTypeQuerySettings.FilterConfigurations);
					modelBoundQuerySettings.DefaultEnableFilter = propertyTypeQuerySettings.DefaultEnableFilter;
				}
				if (modelBoundQuerySettings.SelectConfigurations.Count == 0 && modelBoundQuerySettings.DefaultSelectType == null)
				{
					modelBoundQuerySettings.CopySelectConfigurations(propertyTypeQuerySettings.SelectConfigurations);
					modelBoundQuerySettings.DefaultSelectType = propertyTypeQuerySettings.DefaultSelectType;
				}
				if (modelBoundQuerySettings.ExpandConfigurations.Count == 0 && modelBoundQuerySettings.DefaultExpandType == null)
				{
					modelBoundQuerySettings.CopyExpandConfigurations(propertyTypeQuerySettings.ExpandConfigurations);
					modelBoundQuerySettings.DefaultExpandType = propertyTypeQuerySettings.DefaultExpandType;
					modelBoundQuerySettings.DefaultMaxDepth = propertyTypeQuerySettings.DefaultMaxDepth;
				}
			}
			return modelBoundQuerySettings;
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00035774 File Offset: 0x00033974
		private static ModelBoundQuerySettings GetModelBoundQuerySettings<T>(T key, IEdmModel edmModel, DefaultQuerySettings defaultQuerySettings = null) where T : IEdmElement
		{
			if (key == null)
			{
				return null;
			}
			ModelBoundQuerySettings modelBoundQuerySettings = edmModel.GetAnnotationValue(key);
			if (modelBoundQuerySettings == null)
			{
				modelBoundQuerySettings = new ModelBoundQuerySettings();
				if (defaultQuerySettings != null)
				{
					if (defaultQuerySettings.MaxTop != null)
					{
						int? maxTop = defaultQuerySettings.MaxTop;
						int num = 0;
						if (!((maxTop.GetValueOrDefault() > num) & (maxTop != null)))
						{
							return modelBoundQuerySettings;
						}
					}
					modelBoundQuerySettings.MaxTop = defaultQuerySettings.MaxTop;
				}
			}
			return modelBoundQuerySettings;
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x000357DE File Offset: 0x000339DE
		private static QueryableRestrictionsAnnotation GetPropertyRestrictions(IEdmProperty edmProperty, IEdmModel edmModel)
		{
			return edmModel.GetAnnotationValue(edmProperty);
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x000357E7 File Offset: 0x000339E7
		private static IEdmPrimitiveType GetPrimitiveType(EdmPrimitiveTypeKind primitiveKind)
		{
			return EdmLibHelpers._coreModel.GetPrimitiveType(primitiveKind);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x000357F4 File Offset: 0x000339F4
		private static bool IsSelectExpandWrapper(Type type, out Type entityType)
		{
			if (type == null)
			{
				entityType = null;
				return false;
			}
			if (type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(SelectExpandWrapper<>))
			{
				entityType = type.GetGenericArguments()[0];
				return true;
			}
			return EdmLibHelpers.IsSelectExpandWrapper(TypeHelper.GetBaseType(type), out entityType);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00035848 File Offset: 0x00033A48
		private static Type ExtractGenericInterface(Type queryType, Type interfaceType)
		{
			Func<Type, bool> func = (Type t) => t.IsGenericType() && t.GetGenericTypeDefinition() == interfaceType;
			if (!func(queryType))
			{
				return queryType.GetInterfaces().FirstOrDefault(func);
			}
			return queryType;
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00035884 File Offset: 0x00033A84
		private static IEnumerable<Type> GetMatchingTypes(string edmFullName, IWebApiAssembliesResolver assembliesResolver)
		{
			return from t in TypeHelper.GetLoadedTypes(assembliesResolver)
				where TypeHelper.IsPublic(t) && t.EdmFullName() == edmFullName
				select t;
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x000358B8 File Offset: 0x00033AB8
		private static string MangleClrTypeName(Type type)
		{
			if (!type.IsGenericType())
			{
				return type.Name;
			}
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			string text = "{0}Of{1}";
			object[] array = new object[2];
			array[0] = type.Name.Replace('`', '_');
			array[1] = string.Join("_", from t in type.GetGenericArguments()
				select EdmLibHelpers.MangleClrTypeName(t));
			return string.Format(invariantCulture, text, array);
		}

		// Token: 0x040003D1 RID: 977
		private static readonly EdmCoreModel _coreModel = EdmCoreModel.Instance;

		// Token: 0x040003D2 RID: 978
		private static readonly Dictionary<Type, IEdmPrimitiveType> _builtInTypesMapping = new KeyValuePair<Type, IEdmPrimitiveType>[]
		{
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(string), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.String)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(bool), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Boolean)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(bool?), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Boolean)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(byte), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Byte)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(byte?), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Byte)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(decimal), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Decimal)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(decimal?), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Decimal)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(double), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Double)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(double?), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Double)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(Guid), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Guid)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(Guid?), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Guid)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(short), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Int16)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(short?), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Int16)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(int), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Int32)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(int?), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Int32)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(long), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Int64)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(long?), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Int64)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(sbyte), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.SByte)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(sbyte?), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.SByte)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(float), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Single)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(float?), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Single)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(byte[]), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Binary)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(Stream), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Stream)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(Geography), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Geography)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(GeographyPoint), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyPoint)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(GeographyLineString), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyLineString)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(GeographyPolygon), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyPolygon)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(GeographyCollection), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyCollection)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(GeographyMultiLineString), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyMultiLineString)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(GeographyMultiPoint), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyMultiPoint)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(GeographyMultiPolygon), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyMultiPolygon)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(Geometry), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Geometry)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(GeometryPoint), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryPoint)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(GeometryLineString), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryLineString)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(GeometryPolygon), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryPolygon)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(GeometryCollection), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryCollection)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(GeometryMultiLineString), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryMultiLineString)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(GeometryMultiPoint), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryMultiPoint)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(GeometryMultiPolygon), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryMultiPolygon)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(DateTimeOffset), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.DateTimeOffset)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(DateTimeOffset?), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.DateTimeOffset)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(TimeSpan), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Duration)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(TimeSpan?), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Duration)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(Date), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Date)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(Date?), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Date)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(TimeOfDay), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.TimeOfDay)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(TimeOfDay?), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.TimeOfDay)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(XElement), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.String)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(Binary), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Binary)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(ushort), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Int32)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(ushort?), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Int32)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(uint), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Int64)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(uint?), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Int64)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(ulong), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Int64)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(ulong?), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.Int64)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(char[]), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.String)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(char), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.String)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(char?), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.String)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(DateTime), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.DateTimeOffset)),
			new KeyValuePair<Type, IEdmPrimitiveType>(typeof(DateTime?), EdmLibHelpers.GetPrimitiveType(EdmPrimitiveTypeKind.DateTimeOffset))
		}.ToDictionary((KeyValuePair<Type, IEdmPrimitiveType> kvp) => kvp.Key, (KeyValuePair<Type, IEdmPrimitiveType> kvp) => kvp.Value);
	}
}
