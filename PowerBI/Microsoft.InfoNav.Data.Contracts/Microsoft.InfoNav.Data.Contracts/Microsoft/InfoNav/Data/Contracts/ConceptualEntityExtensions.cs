using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x0200007B RID: 123
	public static class ConceptualEntityExtensions
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x0000790C File Offset: 0x00005B0C
		public static IConceptualProperty GetPropertyByEdmName(this IConceptualEntity entity, string edmName)
		{
			IConceptualProperty conceptualProperty;
			Contract.Check(entity.TryGetPropertyByEdmName(edmName, out conceptualProperty), "Unable to find specified field on the entity");
			return conceptualProperty;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000792D File Offset: 0x00005B2D
		public static bool IsFromEntity(this IConceptualProperty property, IConceptualEntity entity)
		{
			return ConceptualEntityExtensionAwareEqualityComparer.Instance.Equals(property.Entity, entity);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00007940 File Offset: 0x00005B40
		public static IConceptualEntity GetBaseEntityOrSelf(this IConceptualEntity entity)
		{
			IExtensionConceptualEntity extensionConceptualEntity = entity as IExtensionConceptualEntity;
			if (extensionConceptualEntity != null && extensionConceptualEntity.ExtendedEntity != null)
			{
				return extensionConceptualEntity.ExtendedEntity;
			}
			return entity;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00007968 File Offset: 0x00005B68
		public static IConceptualEntity GetSupersetExtensionEntity(this IReadOnlyList<IConceptualEntity> entities)
		{
			if (entities.Count == 0)
			{
				return null;
			}
			if (entities.Count == 1)
			{
				return entities[0];
			}
			IConceptualEntity conceptualEntity = entities[0];
			for (int i = 1; i < entities.Count; i++)
			{
				IConceptualEntity conceptualEntity2 = entities[i];
				if (!ConceptualEntityExtensionAwareEqualityComparer.Instance.Equals(conceptualEntity, conceptualEntity2))
				{
					return null;
				}
				IExtensionConceptualEntity extensionConceptualEntity = conceptualEntity2 as IExtensionConceptualEntity;
				if (extensionConceptualEntity != null && extensionConceptualEntity.ExtendedEntity == conceptualEntity)
				{
					conceptualEntity = conceptualEntity2;
				}
			}
			return conceptualEntity;
		}
	}
}
