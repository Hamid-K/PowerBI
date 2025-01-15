using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001B5 RID: 437
	public class StoreGeneratedIdentityKeyConvention : IConceptualModelConvention<EntityType>, IConvention
	{
		// Token: 0x0600179B RID: 6043 RVA: 0x0003FFC4 File Offset: 0x0003E1C4
		public virtual void Apply(EntityType item, DbModel model)
		{
			Check.NotNull<EntityType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			if (item.BaseType == null && item.KeyProperties.Count == 1)
			{
				if (!(from <>h__TransparentIdentifier0 in item.DeclaredProperties.Select((EdmProperty p) => new
					{
						p = p,
						sgp = p.GetStoreGeneratedPattern()
					}).Where(delegate(<>h__TransparentIdentifier0)
					{
						if (<>h__TransparentIdentifier0.sgp != null)
						{
							StoreGeneratedPattern? sgp = <>h__TransparentIdentifier0.sgp;
							StoreGeneratedPattern storeGeneratedPattern = StoreGeneratedPattern.Identity;
							return (sgp.GetValueOrDefault() == storeGeneratedPattern) & (sgp != null);
						}
						return false;
					})
					select <>h__TransparentIdentifier0.sgp).Any<StoreGeneratedPattern?>())
				{
					EdmProperty property = item.KeyProperties.Single<EdmProperty>();
					if (property.GetStoreGeneratedPattern() == null && property.PrimitiveType != null && StoreGeneratedIdentityKeyConvention._applicableTypes.Contains(property.PrimitiveType.PrimitiveTypeKind) && !model.ConceptualModel.AssociationTypes.Any((AssociationType a) => StoreGeneratedIdentityKeyConvention.IsNonTableSplittingForeignKey(a, property)) && !StoreGeneratedIdentityKeyConvention.ParentOfTpc(item, model.ConceptualModel))
					{
						property.SetStoreGeneratedPattern(StoreGeneratedPattern.Identity);
					}
				}
			}
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x00040110 File Offset: 0x0003E310
		private static bool IsNonTableSplittingForeignKey(AssociationType association, EdmProperty property)
		{
			if (association.Constraint != null && association.Constraint.ToProperties.Contains(property))
			{
				EntityTypeConfiguration entityTypeConfiguration = (EntityTypeConfiguration)association.SourceEnd.GetEntityType().GetConfiguration();
				EntityTypeConfiguration entityTypeConfiguration2 = (EntityTypeConfiguration)association.TargetEnd.GetEntityType().GetConfiguration();
				return entityTypeConfiguration == null || entityTypeConfiguration2 == null || entityTypeConfiguration.GetTableName() == null || entityTypeConfiguration2.GetTableName() == null || !entityTypeConfiguration.GetTableName().Equals(entityTypeConfiguration2.GetTableName());
			}
			return false;
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x00040194 File Offset: 0x0003E394
		private static bool ParentOfTpc(EntityType entityType, EdmModel model)
		{
			return (from et in model.EntityTypes
				where et.GetRootType() == entityType
				select et into e
				let configuration = e.GetConfiguration() as EntityTypeConfiguration
				where configuration != null && configuration.IsMappingAnyInheritedProperty(e)
				select e).Any<EntityType>();
		}

		// Token: 0x04000A3B RID: 2619
		private static readonly IEnumerable<PrimitiveTypeKind> _applicableTypes = new PrimitiveTypeKind[]
		{
			PrimitiveTypeKind.Int16,
			PrimitiveTypeKind.Int32,
			PrimitiveTypeKind.Int64
		};
	}
}
