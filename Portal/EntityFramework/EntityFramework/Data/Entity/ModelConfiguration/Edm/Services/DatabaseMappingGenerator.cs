using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Edm.Services
{
	// Token: 0x02000175 RID: 373
	internal class DatabaseMappingGenerator
	{
		// Token: 0x060016C5 RID: 5829 RVA: 0x0003C2BC File Offset: 0x0003A4BC
		public DatabaseMappingGenerator(DbProviderInfo providerInfo, DbProviderManifest providerManifest)
		{
			this._providerInfo = providerInfo;
			this._providerManifest = providerManifest;
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x0003C2D2 File Offset: 0x0003A4D2
		public DbDatabaseMapping Generate(EdmModel conceptualModel)
		{
			DbDatabaseMapping dbDatabaseMapping = this.InitializeDatabaseMapping(conceptualModel);
			DatabaseMappingGenerator.GenerateEntityTypes(dbDatabaseMapping);
			DatabaseMappingGenerator.GenerateDiscriminators(dbDatabaseMapping);
			DatabaseMappingGenerator.GenerateAssociationTypes(dbDatabaseMapping);
			return dbDatabaseMapping;
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x0003C2F0 File Offset: 0x0003A4F0
		private DbDatabaseMapping InitializeDatabaseMapping(EdmModel conceptualModel)
		{
			EdmModel edmModel = EdmModel.CreateStoreModel(this._providerInfo, this._providerManifest, conceptualModel.SchemaVersion);
			return new DbDatabaseMapping().Initialize(conceptualModel, edmModel);
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x0003C324 File Offset: 0x0003A524
		private static void GenerateEntityTypes(DbDatabaseMapping databaseMapping)
		{
			using (IEnumerator<EntityType> enumerator = databaseMapping.Model.EntityTypes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EntityType entityType = enumerator.Current;
					if (entityType.Abstract && databaseMapping.Model.EntityTypes.All((EntityType e) => e.BaseType != entityType))
					{
						throw new InvalidOperationException(Strings.UnmappedAbstractType(entityType.GetClrType()));
					}
					new TableMappingGenerator(databaseMapping.ProviderManifest).Generate(entityType, databaseMapping);
				}
			}
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x0003C3D4 File Offset: 0x0003A5D4
		private static void GenerateDiscriminators(DbDatabaseMapping databaseMapping)
		{
			foreach (EntitySetMapping entitySetMapping in databaseMapping.GetEntitySetMappings())
			{
				if (entitySetMapping.EntityTypeMappings.Count<EntityTypeMapping>() > 1)
				{
					TypeUsage storeType = databaseMapping.ProviderManifest.GetStoreType(DatabaseMappingGenerator.DiscriminatorTypeUsage);
					EdmProperty edmProperty = new EdmProperty("Discriminator", storeType)
					{
						Nullable = false,
						DefaultValue = "(Undefined)"
					};
					entitySetMapping.EntityTypeMappings.First<EntityTypeMapping>().MappingFragments.Single<MappingFragment>().Table.AddColumn(edmProperty);
					foreach (EntityTypeMapping entityTypeMapping in entitySetMapping.EntityTypeMappings)
					{
						if (!entityTypeMapping.EntityType.Abstract)
						{
							MappingFragment mappingFragment = entityTypeMapping.MappingFragments.Single<MappingFragment>();
							mappingFragment.SetDefaultDiscriminator(edmProperty);
							mappingFragment.AddDiscriminatorCondition(edmProperty, entityTypeMapping.EntityType.Name);
						}
					}
				}
			}
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x0003C4F0 File Offset: 0x0003A6F0
		private static void GenerateAssociationTypes(DbDatabaseMapping databaseMapping)
		{
			foreach (AssociationType associationType in databaseMapping.Model.AssociationTypes)
			{
				new AssociationTypeMappingGenerator(databaseMapping.ProviderManifest).Generate(associationType, databaseMapping);
			}
		}

		// Token: 0x04000A13 RID: 2579
		private const string DiscriminatorColumnName = "Discriminator";

		// Token: 0x04000A14 RID: 2580
		public const int DiscriminatorMaxLength = 128;

		// Token: 0x04000A15 RID: 2581
		public static TypeUsage DiscriminatorTypeUsage = TypeUsage.CreateStringTypeUsage(PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.String), true, false, 128);

		// Token: 0x04000A16 RID: 2582
		private readonly DbProviderInfo _providerInfo;

		// Token: 0x04000A17 RID: 2583
		private readonly DbProviderManifest _providerManifest;
	}
}
