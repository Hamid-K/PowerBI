using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000168 RID: 360
	internal static class EntityTypeExtensions
	{
		// Token: 0x06001672 RID: 5746 RVA: 0x0003B424 File Offset: 0x00039624
		public static void AddColumn(this EntityType table, EdmProperty column)
		{
			column.SetPreferredName(column.Name);
			column.Name = table.Properties.UniquifyName(column.Name);
			table.AddMember(column);
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x0003B450 File Offset: 0x00039650
		public static void SetConfiguration(this EntityType table, object configuration)
		{
			table.GetMetadataProperties().SetConfiguration(configuration);
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x0003B45E File Offset: 0x0003965E
		public static DatabaseName GetTableName(this EntityType table)
		{
			return (DatabaseName)table.Annotations.GetAnnotation("TableName");
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x0003B475 File Offset: 0x00039675
		public static void SetTableName(this EntityType table, DatabaseName tableName)
		{
			table.GetMetadataProperties().SetAnnotation("TableName", tableName);
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x0003B488 File Offset: 0x00039688
		internal static IEnumerable<EntityType> ToHierarchy(this EntityType edmType)
		{
			return EdmType.SafeTraverseHierarchy<EntityType>(edmType);
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x0003B490 File Offset: 0x00039690
		public static IEnumerable<EdmProperty> GetValidKey(this EntityType entityType)
		{
			List<EdmProperty> list = null;
			List<EntityType> list2 = entityType.ToHierarchy().ToList<EntityType>();
			for (int i = list2.Count - 1; i >= 0; i--)
			{
				EntityType entityType2 = list2[i];
				if (entityType2.BaseType == null && entityType2.KeyProperties.Count > 0)
				{
					if (list != null)
					{
						return Enumerable.Empty<EdmProperty>();
					}
					list = new List<EdmProperty>();
					HashSet<EdmProperty> hashSet = new HashSet<EdmProperty>();
					HashSet<string> hashSet2 = new HashSet<string>();
					HashSet<EdmProperty> hashSet3 = new HashSet<EdmProperty>(entityType2.DeclaredProperties.Where((EdmProperty p) => p != null));
					for (int j = 0; j < entityType2.KeyProperties.Count; j++)
					{
						EdmProperty edmProperty = entityType2.KeyProperties[j];
						if (edmProperty == null || hashSet.Contains(edmProperty) || !hashSet3.Contains(edmProperty) || string.IsNullOrEmpty(edmProperty.Name) || string.IsNullOrWhiteSpace(edmProperty.Name) || hashSet2.Contains(edmProperty.Name))
						{
							return Enumerable.Empty<EdmProperty>();
						}
						list.Add(edmProperty);
						hashSet.Add(edmProperty);
						hashSet2.Add(edmProperty.Name);
					}
				}
			}
			IEnumerable<EdmProperty> enumerable = list;
			return enumerable ?? Enumerable.Empty<EdmProperty>();
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x0003B5E4 File Offset: 0x000397E4
		public static List<EdmProperty> GetKeyProperties(this EntityType entityType)
		{
			HashSet<EntityType> hashSet = new HashSet<EntityType>();
			List<EdmProperty> list = new List<EdmProperty>();
			EntityTypeExtensions.GetKeyProperties(hashSet, entityType, list);
			return list;
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x0003B604 File Offset: 0x00039804
		private static void GetKeyProperties(HashSet<EntityType> visitedTypes, EntityType visitingType, List<EdmProperty> keyProperties)
		{
			if (visitedTypes.Contains(visitingType))
			{
				return;
			}
			visitedTypes.Add(visitingType);
			if (visitingType.BaseType != null)
			{
				EntityTypeExtensions.GetKeyProperties(visitedTypes, (EntityType)visitingType.BaseType, keyProperties);
				return;
			}
			ReadOnlyMetadataCollection<EdmProperty> keyProperties2 = visitingType.KeyProperties;
			if (keyProperties2.Count > 0)
			{
				keyProperties.AddRange(keyProperties2);
			}
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x0003B658 File Offset: 0x00039858
		public static EntityType GetRootType(this EntityType entityType)
		{
			EdmType edmType = entityType;
			while (edmType.BaseType != null)
			{
				edmType = edmType.BaseType;
			}
			return (EntityType)edmType;
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x0003B67E File Offset: 0x0003987E
		public static bool IsAncestorOf(this EntityType ancestor, EntityType entityType)
		{
			while (entityType != null)
			{
				if (entityType.BaseType == ancestor)
				{
					return true;
				}
				entityType = (EntityType)entityType.BaseType;
			}
			return false;
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x0003B69E File Offset: 0x0003989E
		public static IEnumerable<EdmProperty> KeyProperties(this EntityType entityType)
		{
			return entityType.GetRootType().KeyProperties;
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x0003B6AB File Offset: 0x000398AB
		public static object GetConfiguration(this EntityType entityType)
		{
			return entityType.Annotations.GetConfiguration();
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x0003B6B8 File Offset: 0x000398B8
		public static Type GetClrType(this EntityType entityType)
		{
			return entityType.Annotations.GetClrType();
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x0003B6C5 File Offset: 0x000398C5
		public static IEnumerable<EntityType> TypeHierarchyIterator(this EntityType entityType, EdmModel model)
		{
			yield return entityType;
			IEnumerable<EntityType> derivedTypes = model.GetDerivedTypes(entityType);
			if (derivedTypes != null)
			{
				foreach (EntityType entityType2 in derivedTypes)
				{
					foreach (EntityType entityType3 in entityType2.TypeHierarchyIterator(model))
					{
						yield return entityType3;
					}
					IEnumerator<EntityType> enumerator2 = null;
				}
				IEnumerator<EntityType> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x0003B6DC File Offset: 0x000398DC
		public static EdmProperty AddComplexProperty(this EntityType entityType, string name, ComplexType complexType)
		{
			EdmProperty edmProperty = EdmProperty.CreateComplex(name, complexType);
			entityType.AddMember(edmProperty);
			return edmProperty;
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x0003B6FC File Offset: 0x000398FC
		public static EdmProperty GetDeclaredPrimitiveProperty(this EntityType entityType, PropertyInfo propertyInfo)
		{
			return entityType.GetDeclaredPrimitiveProperties().SingleOrDefault((EdmProperty p) => p.GetClrPropertyInfo().IsSameAs(propertyInfo));
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x0003B72D File Offset: 0x0003992D
		public static IEnumerable<EdmProperty> GetDeclaredPrimitiveProperties(this EntityType entityType)
		{
			return entityType.DeclaredProperties.Where((EdmProperty p) => p.IsUnderlyingPrimitiveType);
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x0003B75C File Offset: 0x0003995C
		public static NavigationProperty AddNavigationProperty(this EntityType entityType, string name, AssociationType associationType)
		{
			EntityType entityType2 = associationType.TargetEnd.GetEntityType();
			EdmType edmType = (associationType.TargetEnd.RelationshipMultiplicity.IsMany() ? entityType2.GetCollectionType() : entityType2);
			NavigationProperty navigationProperty = new NavigationProperty(name, TypeUsage.Create(edmType))
			{
				RelationshipType = associationType,
				FromEndMember = associationType.SourceEnd,
				ToEndMember = associationType.TargetEnd
			};
			entityType.AddMember(navigationProperty);
			return navigationProperty;
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x0003B7C8 File Offset: 0x000399C8
		public static NavigationProperty GetNavigationProperty(this EntityType entityType, PropertyInfo propertyInfo)
		{
			return entityType.NavigationProperties.SingleOrDefault((NavigationProperty np) => np.GetClrPropertyInfo().IsSameAs(propertyInfo));
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x0003B7FC File Offset: 0x000399FC
		public static bool IsRootOfSet(this EntityType entityType, IEnumerable<EntityType> set)
		{
			return set.All((EntityType et) => et == entityType || entityType.IsAncestorOf(et) || et.GetRootType() != entityType.GetRootType());
		}

		// Token: 0x04000A08 RID: 2568
		private const string TableNameAnnotation = "TableName";
	}
}
