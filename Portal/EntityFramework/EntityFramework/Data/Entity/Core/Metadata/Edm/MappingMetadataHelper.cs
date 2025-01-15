using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004CD RID: 1229
	internal static class MappingMetadataHelper
	{
		// Token: 0x06003CE4 RID: 15588 RVA: 0x000C9940 File Offset: 0x000C7B40
		internal static IEnumerable<TypeMapping> GetMappingsForEntitySetAndType(StorageMappingItemCollection mappingCollection, EntityContainer container, EntitySetBase entitySet, EntityTypeBase entityType)
		{
			EntitySetBaseMapping setMapping = MappingMetadataHelper.GetEntityContainerMap(mappingCollection, container).GetSetMapping(entitySet.Name);
			if (setMapping != null)
			{
				IEnumerable<TypeMapping> typeMappings = setMapping.TypeMappings;
				Func<TypeMapping, bool> <>9__0;
				Func<TypeMapping, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (TypeMapping map) => map.Types.Union(map.IsOfTypes).Contains(entityType));
				}
				foreach (TypeMapping typeMapping in typeMappings.Where(func))
				{
					yield return typeMapping;
				}
				IEnumerator<TypeMapping> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06003CE5 RID: 15589 RVA: 0x000C9968 File Offset: 0x000C7B68
		internal static IEnumerable<TypeMapping> GetMappingsForEntitySetAndSuperTypes(StorageMappingItemCollection mappingCollection, EntityContainer container, EntitySetBase entitySet, EntityTypeBase childEntityType)
		{
			return MetadataHelper.GetTypeAndParentTypesOf(childEntityType, true).SelectMany(delegate(EdmType edmType)
			{
				EntityTypeBase entityTypeBase = edmType as EntityTypeBase;
				if (!edmType.EdmEquals(childEntityType))
				{
					return MappingMetadataHelper.GetIsTypeOfMappingsForEntitySetAndType(mappingCollection, container, entitySet, entityTypeBase, childEntityType);
				}
				return MappingMetadataHelper.GetMappingsForEntitySetAndType(mappingCollection, container, entitySet, entityTypeBase);
			}).ToList<TypeMapping>();
		}

		// Token: 0x06003CE6 RID: 15590 RVA: 0x000C99B9 File Offset: 0x000C7BB9
		private static IEnumerable<TypeMapping> GetIsTypeOfMappingsForEntitySetAndType(StorageMappingItemCollection mappingCollection, EntityContainer container, EntitySetBase entitySet, EntityTypeBase entityType, EntityTypeBase childEntityType)
		{
			Func<EntityTypeBase, bool> <>9__0;
			foreach (TypeMapping typeMapping in MappingMetadataHelper.GetMappingsForEntitySetAndType(mappingCollection, container, entitySet, entityType))
			{
				IEnumerable<EntityTypeBase> isOfTypes = typeMapping.IsOfTypes;
				Func<EntityTypeBase, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (EntityTypeBase parentType) => parentType.IsAssignableFrom(childEntityType));
				}
				if (isOfTypes.Any(func) || typeMapping.Types.Contains(childEntityType))
				{
					yield return typeMapping;
				}
			}
			IEnumerator<TypeMapping> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06003CE7 RID: 15591 RVA: 0x000C99E6 File Offset: 0x000C7BE6
		internal static IEnumerable<EntityTypeModificationFunctionMapping> GetModificationFunctionMappingsForEntitySetAndType(StorageMappingItemCollection mappingCollection, EntityContainer container, EntitySetBase entitySet, EntityTypeBase entityType)
		{
			EntitySetMapping entitySetMapping = MappingMetadataHelper.GetEntityContainerMap(mappingCollection, container).GetSetMapping(entitySet.Name) as EntitySetMapping;
			if (entitySetMapping != null && entitySetMapping != null)
			{
				IEnumerable<EntityTypeModificationFunctionMapping> modificationFunctionMappings = entitySetMapping.ModificationFunctionMappings;
				Func<EntityTypeModificationFunctionMapping, bool> <>9__0;
				Func<EntityTypeModificationFunctionMapping, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (EntityTypeModificationFunctionMapping functionMap) => functionMap.EntityType.Equals(entityType));
				}
				foreach (EntityTypeModificationFunctionMapping entityTypeModificationFunctionMapping in modificationFunctionMappings.Where(func))
				{
					yield return entityTypeModificationFunctionMapping;
				}
				IEnumerator<EntityTypeModificationFunctionMapping> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06003CE8 RID: 15592 RVA: 0x000C9A0C File Offset: 0x000C7C0C
		internal static EntityContainerMapping GetEntityContainerMap(StorageMappingItemCollection mappingCollection, EntityContainer entityContainer)
		{
			ReadOnlyCollection<EntityContainerMapping> items = mappingCollection.GetItems<EntityContainerMapping>();
			EntityContainerMapping entityContainerMapping = null;
			foreach (EntityContainerMapping entityContainerMapping2 in items)
			{
				if (entityContainer.Equals(entityContainerMapping2.EdmEntityContainer) || entityContainer.Equals(entityContainerMapping2.StorageEntityContainer))
				{
					entityContainerMapping = entityContainerMapping2;
					break;
				}
			}
			if (entityContainerMapping == null)
			{
				throw new MappingException(Strings.Mapping_NotFound_EntityContainer(entityContainer.Name));
			}
			return entityContainerMapping;
		}
	}
}
