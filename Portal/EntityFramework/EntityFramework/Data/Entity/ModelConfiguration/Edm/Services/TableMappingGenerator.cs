using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Edm.Services
{
	// Token: 0x0200017A RID: 378
	internal class TableMappingGenerator : StructuralTypeMappingGenerator
	{
		// Token: 0x060016E0 RID: 5856 RVA: 0x0003CE7E File Offset: 0x0003B07E
		public TableMappingGenerator(DbProviderManifest providerManifest)
			: base(providerManifest)
		{
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x0003CE88 File Offset: 0x0003B088
		public void Generate(EntityType entityType, DbDatabaseMapping databaseMapping)
		{
			EntitySet entitySet = databaseMapping.Model.GetEntitySet(entityType);
			EntitySetMapping entitySetMapping = databaseMapping.GetEntitySetMapping(entitySet) ?? databaseMapping.AddEntitySetMapping(entitySet);
			EntityTypeMapping entityTypeMapping = entitySetMapping.EntityTypeMappings.FirstOrDefault((EntityTypeMapping m) => m.EntityTypes.Contains(entitySet.ElementType)) ?? entitySetMapping.EntityTypeMappings.FirstOrDefault<EntityTypeMapping>();
			EntityType entityType2 = ((entityTypeMapping != null) ? entityTypeMapping.MappingFragments.First<MappingFragment>().Table : databaseMapping.Database.AddTable(entityType.GetRootType().Name));
			entityTypeMapping = new EntityTypeMapping(null);
			MappingFragment mappingFragment = new MappingFragment(databaseMapping.Database.GetEntitySet(entityType2), entityTypeMapping, false);
			entityTypeMapping.AddType(entityType);
			entityTypeMapping.AddFragment(mappingFragment);
			entityTypeMapping.SetClrType(entityType.GetClrType());
			entitySetMapping.AddTypeMapping(entityTypeMapping);
			new PropertyMappingGenerator(this._providerManifest).Generate(entityType, entityType.Properties, entitySetMapping, mappingFragment, new List<EdmProperty>(), false);
		}
	}
}
