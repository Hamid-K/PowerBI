using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Csdl;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000108 RID: 264
	internal static class CapabilitiesVocabularyExtensionMethods
	{
		// Token: 0x0600090E RID: 2318 RVA: 0x00025EF4 File Offset: 0x000240F4
		public static void SetCountRestrictionsAnnotation(this EdmModel model, IEdmEntitySet target, bool isCountable, IEnumerable<IEdmProperty> nonCountableProperties, IEnumerable<IEdmNavigationProperty> nonCountableNavigationProperties)
		{
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			if (target == null)
			{
				throw Error.ArgumentNull("target");
			}
			IEnumerable<IEdmProperty> enumerable;
			if ((enumerable = nonCountableProperties) == null)
			{
				IEnumerable<IEdmProperty> emptyStructuralProperties = CapabilitiesVocabularyExtensionMethods.EmptyStructuralProperties;
				enumerable = emptyStructuralProperties;
			}
			nonCountableProperties = enumerable;
			nonCountableNavigationProperties = nonCountableNavigationProperties ?? CapabilitiesVocabularyExtensionMethods.EmptyNavigationProperties;
			List<IEdmPropertyConstructor> list = new List<IEdmPropertyConstructor>();
			list.Add(new EdmPropertyConstructor("Countable", new EdmBooleanConstant(isCountable)));
			string text = "NonCountableProperties";
			IEdmExpression[] array = nonCountableProperties.Select((IEdmProperty p) => new EdmPropertyPathExpression(p.Name)).ToArray<EdmPropertyPathExpression>();
			list.Add(new EdmPropertyConstructor(text, new EdmCollectionExpression(array)));
			string text2 = "NonCountableNavigationProperties";
			array = nonCountableNavigationProperties.Select((IEdmNavigationProperty p) => new EdmNavigationPropertyPathExpression(p.Name)).ToArray<EdmNavigationPropertyPathExpression>();
			list.Add(new EdmPropertyConstructor(text2, new EdmCollectionExpression(array)));
			IList<IEdmPropertyConstructor> list2 = list;
			model.SetVocabularyAnnotation(target, list2, "Org.OData.Capabilities.V1.CountRestrictions");
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00025FE4 File Offset: 0x000241E4
		public static void SetNavigationRestrictionsAnnotation(this EdmModel model, IEdmEntitySet target, CapabilitiesNavigationType navigability, IEnumerable<Tuple<IEdmNavigationProperty, CapabilitiesNavigationType>> restrictedProperties)
		{
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			if (target == null)
			{
				throw Error.ArgumentNull("target");
			}
			IEdmEnumType navigationType = model.GetCapabilitiesNavigationType();
			if (navigationType == null)
			{
				return;
			}
			restrictedProperties = restrictedProperties ?? new Tuple<IEdmNavigationProperty, CapabilitiesNavigationType>[0];
			string type = new EdmEnumTypeReference(navigationType, false).ToStringLiteral((long)navigability);
			IEnumerable<EdmRecordExpression> enumerable = restrictedProperties.Select(delegate(Tuple<IEdmNavigationProperty, CapabilitiesNavigationType> p)
			{
				string name = new EdmEnumTypeReference(navigationType, false).ToStringLiteral((long)p.Item2);
				return new EdmRecordExpression(new IEdmPropertyConstructor[]
				{
					new EdmPropertyConstructor("NavigationProperty", new EdmNavigationPropertyPathExpression(p.Item1.Name)),
					new EdmPropertyConstructor("Navigability", new EdmEnumMemberExpression(new IEdmEnumMember[] { navigationType.Members.Single((IEdmEnumMember m) => m.Name == name) }))
				});
			});
			IList<IEdmPropertyConstructor> list = new List<IEdmPropertyConstructor>
			{
				new EdmPropertyConstructor("Navigability", new EdmEnumMemberExpression(new IEdmEnumMember[] { navigationType.Members.Single((IEdmEnumMember m) => m.Name == type) })),
				new EdmPropertyConstructor("RestrictedProperties", new EdmCollectionExpression(enumerable))
			};
			model.SetVocabularyAnnotation(target, list, "Org.OData.Capabilities.V1.NavigationRestrictions");
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x000260C8 File Offset: 0x000242C8
		public static void SetFilterRestrictionsAnnotation(this EdmModel model, IEdmEntitySet target, bool isFilterable, bool isRequiresFilter, IEnumerable<IEdmProperty> requiredProperties, IEnumerable<IEdmProperty> nonFilterableProperties)
		{
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			if (target == null)
			{
				throw Error.ArgumentNull("target");
			}
			IEnumerable<IEdmProperty> enumerable;
			if ((enumerable = requiredProperties) == null)
			{
				IEnumerable<IEdmProperty> enumerable2 = CapabilitiesVocabularyExtensionMethods.EmptyStructuralProperties;
				enumerable = enumerable2;
			}
			requiredProperties = enumerable;
			IEnumerable<IEdmProperty> enumerable3;
			if ((enumerable3 = nonFilterableProperties) == null)
			{
				IEnumerable<IEdmProperty> enumerable2 = CapabilitiesVocabularyExtensionMethods.EmptyStructuralProperties;
				enumerable3 = enumerable2;
			}
			nonFilterableProperties = enumerable3;
			List<IEdmPropertyConstructor> list = new List<IEdmPropertyConstructor>();
			list.Add(new EdmPropertyConstructor("Filterable", new EdmBooleanConstant(isFilterable)));
			list.Add(new EdmPropertyConstructor("RequiresFilter", new EdmBooleanConstant(isRequiresFilter)));
			string text = "RequiredProperties";
			IEdmExpression[] array = requiredProperties.Select((IEdmProperty p) => new EdmPropertyPathExpression(p.Name)).ToArray<EdmPropertyPathExpression>();
			list.Add(new EdmPropertyConstructor(text, new EdmCollectionExpression(array)));
			string text2 = "NonFilterableProperties";
			array = nonFilterableProperties.Select((IEdmProperty p) => new EdmPropertyPathExpression(p.Name)).ToArray<EdmPropertyPathExpression>();
			list.Add(new EdmPropertyConstructor(text2, new EdmCollectionExpression(array)));
			IList<IEdmPropertyConstructor> list2 = list;
			model.SetVocabularyAnnotation(target, list2, "Org.OData.Capabilities.V1.FilterRestrictions");
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x000261D4 File Offset: 0x000243D4
		public static void SetSortRestrictionsAnnotation(this EdmModel model, IEdmEntitySet target, bool isSortable, IEnumerable<IEdmProperty> ascendingOnlyProperties, IEnumerable<IEdmProperty> descendingOnlyProperties, IEnumerable<IEdmProperty> nonSortableProperties)
		{
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			if (target == null)
			{
				throw Error.ArgumentNull("target");
			}
			IEnumerable<IEdmProperty> enumerable;
			if ((enumerable = ascendingOnlyProperties) == null)
			{
				IEnumerable<IEdmProperty> enumerable2 = CapabilitiesVocabularyExtensionMethods.EmptyStructuralProperties;
				enumerable = enumerable2;
			}
			ascendingOnlyProperties = enumerable;
			IEnumerable<IEdmProperty> enumerable3;
			if ((enumerable3 = descendingOnlyProperties) == null)
			{
				IEnumerable<IEdmProperty> enumerable2 = CapabilitiesVocabularyExtensionMethods.EmptyStructuralProperties;
				enumerable3 = enumerable2;
			}
			descendingOnlyProperties = enumerable3;
			IEnumerable<IEdmProperty> enumerable4;
			if ((enumerable4 = nonSortableProperties) == null)
			{
				IEnumerable<IEdmProperty> enumerable2 = CapabilitiesVocabularyExtensionMethods.EmptyStructuralProperties;
				enumerable4 = enumerable2;
			}
			nonSortableProperties = enumerable4;
			List<IEdmPropertyConstructor> list = new List<IEdmPropertyConstructor>();
			list.Add(new EdmPropertyConstructor("Sortable", new EdmBooleanConstant(isSortable)));
			string text = "AscendingOnlyProperties";
			IEdmExpression[] array = ascendingOnlyProperties.Select((IEdmProperty p) => new EdmPropertyPathExpression(p.Name)).ToArray<EdmPropertyPathExpression>();
			list.Add(new EdmPropertyConstructor(text, new EdmCollectionExpression(array)));
			string text2 = "DescendingOnlyProperties";
			array = descendingOnlyProperties.Select((IEdmProperty p) => new EdmPropertyPathExpression(p.Name)).ToArray<EdmPropertyPathExpression>();
			list.Add(new EdmPropertyConstructor(text2, new EdmCollectionExpression(array)));
			string text3 = "NonSortableProperties";
			array = nonSortableProperties.Select((IEdmProperty p) => new EdmPropertyPathExpression(p.Name)).ToArray<EdmPropertyPathExpression>();
			list.Add(new EdmPropertyConstructor(text3, new EdmCollectionExpression(array)));
			IList<IEdmPropertyConstructor> list2 = list;
			model.SetVocabularyAnnotation(target, list2, "Org.OData.Capabilities.V1.SortRestrictions");
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00026318 File Offset: 0x00024518
		public static void SetExpandRestrictionsAnnotation(this EdmModel model, IEdmEntitySet target, bool isExpandable, IEnumerable<IEdmNavigationProperty> nonExpandableProperties)
		{
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			if (target == null)
			{
				throw Error.ArgumentNull("target");
			}
			nonExpandableProperties = nonExpandableProperties ?? CapabilitiesVocabularyExtensionMethods.EmptyNavigationProperties;
			List<IEdmPropertyConstructor> list = new List<IEdmPropertyConstructor>();
			list.Add(new EdmPropertyConstructor("Expandable", new EdmBooleanConstant(isExpandable)));
			string text = "NonExpandableProperties";
			IEdmExpression[] array = nonExpandableProperties.Select((IEdmNavigationProperty p) => new EdmNavigationPropertyPathExpression(p.Name)).ToArray<EdmNavigationPropertyPathExpression>();
			list.Add(new EdmPropertyConstructor(text, new EdmCollectionExpression(array)));
			IList<IEdmPropertyConstructor> list2 = list;
			model.SetVocabularyAnnotation(target, list2, "Org.OData.Capabilities.V1.ExpandRestrictions");
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x000263B8 File Offset: 0x000245B8
		private static void SetVocabularyAnnotation(this EdmModel model, IEdmVocabularyAnnotatable target, IList<IEdmPropertyConstructor> properties, string qualifiedName)
		{
			IEdmTerm edmTerm = model.FindTerm(qualifiedName);
			if (edmTerm != null)
			{
				IEdmRecordExpression edmRecordExpression = new EdmRecordExpression(properties);
				EdmVocabularyAnnotation edmVocabularyAnnotation = new EdmVocabularyAnnotation(target, edmTerm, edmRecordExpression);
				edmVocabularyAnnotation.SetSerializationLocation(model, new EdmVocabularyAnnotationSerializationLocation?(EdmVocabularyAnnotationSerializationLocation.Inline));
				model.SetVocabularyAnnotation(edmVocabularyAnnotation);
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x000263F4 File Offset: 0x000245F4
		private static IEdmEnumType GetCapabilitiesNavigationType(this EdmModel model)
		{
			IEdmEnumType edmEnumType;
			if ((edmEnumType = CapabilitiesVocabularyExtensionMethods._navigationType) == null)
			{
				edmEnumType = (CapabilitiesVocabularyExtensionMethods._navigationType = model.FindType("Org.OData.Capabilities.V1.NavigationType") as IEdmEnumType);
			}
			return edmEnumType;
		}

		// Token: 0x040002EA RID: 746
		private static readonly IEnumerable<IEdmStructuralProperty> EmptyStructuralProperties = Enumerable.Empty<IEdmStructuralProperty>();

		// Token: 0x040002EB RID: 747
		private static readonly IEnumerable<IEdmNavigationProperty> EmptyNavigationProperties = Enumerable.Empty<IEdmNavigationProperty>();

		// Token: 0x040002EC RID: 748
		private static IEdmEnumType _navigationType;
	}
}
