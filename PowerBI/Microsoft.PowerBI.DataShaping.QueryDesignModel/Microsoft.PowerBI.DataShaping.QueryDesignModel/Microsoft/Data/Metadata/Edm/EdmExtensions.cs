using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000079 RID: 121
	internal static class EdmExtensions
	{
		// Token: 0x06000946 RID: 2374 RVA: 0x00015028 File Offset: 0x00013228
		internal static EntityType Clone(this EntityType entityType, IReadOnlyList<EdmMember> additionalEdmMembers = null)
		{
			ReadOnlyMetadataCollection<EdmMember> members = entityType.Members;
			int num2;
			if (members == null)
			{
				int? num = ((additionalEdmMembers != null) ? new int?(additionalEdmMembers.Count) : null);
				num2 = ((num != null) ? new int?(num.GetValueOrDefault()) : null).GetValueOrDefault();
			}
			else
			{
				num2 = members.Count;
			}
			List<EdmMember> list = new List<EdmMember>(num2);
			if (entityType.Members != null)
			{
				foreach (EdmMember edmMember in entityType.Members)
				{
					list.Add(edmMember.Clone());
				}
			}
			if (additionalEdmMembers != null)
			{
				list.AddRange(additionalEdmMembers);
			}
			EntityType entityType2 = new EntityType(entityType.Name, entityType.NamespaceName, entityType.DataSpace, entityType.KeyMemberNames, list);
			entityType2.AddMetadataProperties(entityType);
			entityType2.SetReadOnly();
			return entityType2;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00015118 File Offset: 0x00013318
		internal static EntityContainer Clone(this EntityContainer entityContainer, IReadOnlyList<EntitySetBase> newEntitySetBases)
		{
			EntityContainer entityContainer2 = new EntityContainer(entityContainer.Name, entityContainer.DataSpace);
			foreach (EntitySetBase entitySetBase in newEntitySetBases)
			{
				entityContainer2.AddEntitySetBase(entitySetBase);
			}
			foreach (EdmFunction edmFunction in entityContainer.FunctionImports)
			{
				entityContainer2.AddFunctionImport(edmFunction.Clone(newEntitySetBases));
			}
			if (entityContainer.Documentation != null)
			{
				entityContainer2.Documentation = entityContainer.Documentation;
			}
			entityContainer2.AddMetadataProperties(entityContainer);
			return entityContainer2;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x000151D8 File Offset: 0x000133D8
		internal static List<EntitySetBase> Clone(this IReadOnlyList<EntitySetBase> entitySetBases, Dictionary<string, EntityType> entityTypeDic)
		{
			List<EntitySetBase> list = new List<EntitySetBase>(entitySetBases.Count);
			Dictionary<string, EntitySet> dictionary = null;
			foreach (EntitySetBase entitySetBase in entitySetBases)
			{
				EntitySet entitySet = entitySetBase as EntitySet;
				if (entitySet != null)
				{
					EntityType elementType;
					if (!entityTypeDic.TryGetValue(entitySetBase.ElementType.Name, out elementType))
					{
						elementType = entitySet.ElementType;
					}
					EntitySet entitySet2 = entitySet.Clone(elementType);
					list.Add(entitySet2);
					Util.AddToLazyDictionary<string, EntitySet>(ref dictionary, entitySet2.Name, entitySet2, null);
				}
				else
				{
					AssociationSet associationSet = entitySetBase as AssociationSet;
					if (associationSet == null)
					{
						throw new InvalidOperationException("Unknown EntitySetBase type");
					}
					list.Add(associationSet.Clone(dictionary));
				}
			}
			return list;
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x000152A4 File Offset: 0x000134A4
		private static EntitySet Clone(this EntitySet entitySet, EntityType entityType)
		{
			EntitySet entitySet2 = new EntitySet(entitySet.Name, entitySet.Schema, entitySet.Table, entitySet.DefiningQuery, entityType);
			entitySet2.AddMetadataProperties(entitySet);
			entitySet2.SetReadOnly();
			return entitySet2;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x000152D4 File Offset: 0x000134D4
		private static AssociationSet Clone(this AssociationSet associationSet, Dictionary<string, EntitySet> entitySetDic)
		{
			AssociationSet associationSet2 = new AssociationSet(associationSet.Name, associationSet.ElementType);
			foreach (AssociationSetEnd associationSetEnd in associationSet.AssociationSetEnds)
			{
				AssociationSetEnd associationSetEnd2 = new AssociationSetEnd(entitySetDic[associationSetEnd.EntitySet.Name], associationSet2, associationSet.ElementType.Members[associationSetEnd.Name] as AssociationEndMember);
				associationSetEnd2.AddMetadataProperties(associationSetEnd);
				associationSet2.AddAssociationSetEnd(associationSetEnd2);
			}
			associationSet2.AddMetadataProperties(associationSet);
			associationSet2.SetReadOnly();
			return associationSet2;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00015384 File Offset: 0x00013584
		private static EdmFunction Clone(this EdmFunction edmFunction, IReadOnlyList<EntitySetBase> entitySets)
		{
			EntitySet entitySet = (EntitySet)entitySets.Single((EntitySetBase e) => e.Name == edmFunction.EntitySet.Name, "Excepted one entity set", Array.Empty<string>());
			FunctionParameter functionParameter = edmFunction.ReturnParameter.Clone();
			List<FunctionParameter> list = new List<FunctionParameter>(edmFunction.Parameters.Count);
			foreach (FunctionParameter functionParameter2 in edmFunction.Parameters)
			{
				list.Add(functionParameter2.Clone());
			}
			EdmFunction edmFunction2 = new EdmFunction(edmFunction.Name, edmFunction.NamespaceName, edmFunction.DataSpace, new EdmFunctionPayload
			{
				Schema = edmFunction.Schema,
				StoreFunctionName = edmFunction.StoreFunctionNameAttribute,
				CommandText = edmFunction.CommandTextAttribute,
				EntitySet = entitySet,
				IsAggregate = new bool?(edmFunction.AggregateAttribute),
				IsBuiltIn = new bool?(edmFunction.BuiltInAttribute),
				IsNiladic = new bool?(edmFunction.NiladicFunctionAttribute),
				IsComposable = new bool?(edmFunction.IsComposableAttribute),
				IsFromProviderManifest = new bool?(edmFunction.IsFromProviderManifest),
				ReturnParameter = functionParameter,
				Parameters = list.ToArray(),
				ParameterTypeSemantics = new ParameterTypeSemantics?(edmFunction.ParameterTypeSemanticsAttribute)
			});
			edmFunction2.Documentation = edmFunction.Documentation;
			edmFunction2.AddMetadataProperties(edmFunction);
			return edmFunction2;
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00015564 File Offset: 0x00013764
		private static FunctionParameter Clone(this FunctionParameter functionParameter)
		{
			FunctionParameter functionParameter2 = new FunctionParameter(functionParameter.Name, functionParameter.TypeUsage, functionParameter.Mode);
			functionParameter2.AddMetadataProperties(functionParameter);
			functionParameter2.SetReadOnly();
			return functionParameter2;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0001558C File Offset: 0x0001378C
		private static EdmMember Clone(this EdmMember edmMember)
		{
			EdmProperty edmProperty = edmMember as EdmProperty;
			if (edmProperty != null)
			{
				return edmProperty.Clone();
			}
			NavigationProperty navigationProperty = edmMember as NavigationProperty;
			if (navigationProperty != null)
			{
				return navigationProperty.Clone();
			}
			throw new InvalidOperationException("Unknown EdmMember type");
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x000155C5 File Offset: 0x000137C5
		private static EdmProperty Clone(this EdmProperty edmProperty)
		{
			EdmProperty edmProperty2 = new EdmProperty(edmProperty.Name, edmProperty.TypeUsage);
			edmProperty2.AddMetadataProperties(edmProperty);
			edmProperty2.SetReadOnly();
			return edmProperty2;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x000155E8 File Offset: 0x000137E8
		private static NavigationProperty Clone(this NavigationProperty navigationProperty)
		{
			NavigationProperty navigationProperty2 = new NavigationProperty(navigationProperty.Name, navigationProperty.TypeUsage);
			navigationProperty2.RelationshipType = navigationProperty.RelationshipType;
			navigationProperty2.ToEndMember = navigationProperty.ToEndMember;
			navigationProperty2.FromEndMember = navigationProperty.FromEndMember;
			navigationProperty2.AddMetadataProperties(navigationProperty);
			navigationProperty2.SetReadOnly();
			return navigationProperty2;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00015638 File Offset: 0x00013838
		private static void AddMetadataProperties(this MetadataItem newMetadataItem, MetadataItem metadataItem)
		{
			IList<string> existingProperties = newMetadataItem.MetadataProperties.Select((MetadataProperty p) => p.Name).Evaluate<string>();
			List<MetadataProperty> list = metadataItem.MetadataProperties.Where((MetadataProperty p) => !existingProperties.Contains(p.Name)).ToList<MetadataProperty>();
			newMetadataItem.AddMetadataProperties(list);
		}
	}
}
