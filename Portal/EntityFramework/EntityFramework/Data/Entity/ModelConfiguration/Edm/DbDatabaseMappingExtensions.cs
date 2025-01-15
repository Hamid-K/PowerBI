using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000162 RID: 354
	internal static class DbDatabaseMappingExtensions
	{
		// Token: 0x0600162A RID: 5674 RVA: 0x0003A134 File Offset: 0x00038334
		public static DbDatabaseMapping Initialize(this DbDatabaseMapping databaseMapping, EdmModel model, EdmModel database)
		{
			databaseMapping.Model = model;
			databaseMapping.Database = database;
			databaseMapping.AddEntityContainerMapping(new EntityContainerMapping(model.Containers.Single<EntityContainer>()));
			return databaseMapping;
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x0003A15C File Offset: 0x0003835C
		public static MetadataWorkspace ToMetadataWorkspace(this DbDatabaseMapping databaseMapping)
		{
			EdmItemCollection itemCollection = new EdmItemCollection(databaseMapping.Model);
			StoreItemCollection storeItemCollection = new StoreItemCollection(databaseMapping.Database);
			StorageMappingItemCollection storageMappingItemCollection = databaseMapping.ToStorageMappingItemCollection(itemCollection, storeItemCollection);
			MetadataWorkspace metadataWorkspace = new MetadataWorkspace(() => itemCollection, () => storeItemCollection, () => storageMappingItemCollection);
			new CodeFirstOSpaceLoader(null).LoadTypes(itemCollection, (ObjectItemCollection)metadataWorkspace.GetItemCollection(DataSpace.OSpace));
			return metadataWorkspace;
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x0003A1F4 File Offset: 0x000383F4
		public static StorageMappingItemCollection ToStorageMappingItemCollection(this DbDatabaseMapping databaseMapping, EdmItemCollection itemCollection, StoreItemCollection storeItemCollection)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, new XmlWriterSettings
			{
				Indent = true
			}))
			{
				new MslSerializer().Serialize(databaseMapping, xmlWriter);
			}
			StorageMappingItemCollection storageMappingItemCollection;
			using (XmlReader xmlReader = XmlReader.Create(new StringReader(stringBuilder.ToString())))
			{
				storageMappingItemCollection = new StorageMappingItemCollection(itemCollection, storeItemCollection, new XmlReader[] { xmlReader });
			}
			return storageMappingItemCollection;
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x0003A280 File Offset: 0x00038480
		public static EntityTypeMapping GetEntityTypeMapping(this DbDatabaseMapping databaseMapping, EntityType entityType)
		{
			IList<EntityTypeMapping> entityTypeMappings = databaseMapping.GetEntityTypeMappings(entityType);
			if (entityTypeMappings.Count <= 1)
			{
				return entityTypeMappings.FirstOrDefault<EntityTypeMapping>();
			}
			return entityTypeMappings.SingleOrDefault((EntityTypeMapping m) => m.IsHierarchyMapping);
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x0003A2CC File Offset: 0x000384CC
		public static IList<EntityTypeMapping> GetEntityTypeMappings(this DbDatabaseMapping databaseMapping, EntityType entityType)
		{
			List<EntityTypeMapping> list = new List<EntityTypeMapping>();
			foreach (EntitySetMapping entitySetMapping in databaseMapping.EntityContainerMappings.Single<EntityContainerMapping>().EntitySetMappings)
			{
				foreach (EntityTypeMapping entityTypeMapping in entitySetMapping.EntityTypeMappings)
				{
					if (entityTypeMapping.EntityType == entityType)
					{
						list.Add(entityTypeMapping);
					}
				}
			}
			return list;
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x0003A368 File Offset: 0x00038568
		public static EntityTypeMapping GetEntityTypeMapping(this DbDatabaseMapping databaseMapping, Type clrType)
		{
			List<EntityTypeMapping> list = new List<EntityTypeMapping>();
			foreach (EntitySetMapping entitySetMapping in databaseMapping.EntityContainerMappings.Single<EntityContainerMapping>().EntitySetMappings)
			{
				foreach (EntityTypeMapping entityTypeMapping in entitySetMapping.EntityTypeMappings)
				{
					if (entityTypeMapping.GetClrType() == clrType)
					{
						list.Add(entityTypeMapping);
					}
				}
			}
			if (list.Count <= 1)
			{
				return list.FirstOrDefault<EntityTypeMapping>();
			}
			return list.SingleOrDefault((EntityTypeMapping m) => m.IsHierarchyMapping);
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x0003A43C File Offset: 0x0003863C
		public static IEnumerable<Tuple<ColumnMappingBuilder, EntityType>> GetComplexPropertyMappings(this DbDatabaseMapping databaseMapping, Type complexType)
		{
			return from esm in databaseMapping.EntityContainerMappings.Single<EntityContainerMapping>().EntitySetMappings
				from etm in esm.EntityTypeMappings
				from etmf in etm.MappingFragments
				from epm in etmf.ColumnMappings
				where epm.PropertyPath.Any((EdmProperty p) => p.IsComplexType && p.ComplexType.GetClrType() == complexType)
				select Tuple.Create<ColumnMappingBuilder, EntityType>(epm, etmf.Table);
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x0003A564 File Offset: 0x00038764
		public static IEnumerable<ModificationFunctionParameterBinding> GetComplexParameterBindings(this DbDatabaseMapping databaseMapping, Type complexType)
		{
			return from esm in databaseMapping.GetEntitySetMappings()
				from mfm in esm.ModificationFunctionMappings
				from pb in mfm.PrimaryParameterBindings
				where pb.MemberPath.Members.OfType<EdmProperty>().Any((EdmProperty p) => p.IsComplexType && p.ComplexType.GetClrType() == complexType)
				select pb;
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x0003A640 File Offset: 0x00038840
		public static EntitySetMapping GetEntitySetMapping(this DbDatabaseMapping databaseMapping, EntitySet entitySet)
		{
			return databaseMapping.EntityContainerMappings.Single<EntityContainerMapping>().EntitySetMappings.SingleOrDefault((EntitySetMapping e) => e.EntitySet == entitySet);
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x0003A67B File Offset: 0x0003887B
		public static IEnumerable<EntitySetMapping> GetEntitySetMappings(this DbDatabaseMapping databaseMapping)
		{
			return databaseMapping.EntityContainerMappings.Single<EntityContainerMapping>().EntitySetMappings;
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x0003A68D File Offset: 0x0003888D
		public static IEnumerable<AssociationSetMapping> GetAssociationSetMappings(this DbDatabaseMapping databaseMapping)
		{
			return databaseMapping.EntityContainerMappings.Single<EntityContainerMapping>().AssociationSetMappings;
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x0003A6A0 File Offset: 0x000388A0
		public static EntitySetMapping AddEntitySetMapping(this DbDatabaseMapping databaseMapping, EntitySet entitySet)
		{
			EntitySetMapping entitySetMapping = new EntitySetMapping(entitySet, null);
			databaseMapping.EntityContainerMappings.Single<EntityContainerMapping>().AddSetMapping(entitySetMapping);
			return entitySetMapping;
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x0003A6C8 File Offset: 0x000388C8
		public static AssociationSetMapping AddAssociationSetMapping(this DbDatabaseMapping databaseMapping, AssociationSet associationSet, EntitySet entitySet)
		{
			EntityContainerMapping entityContainerMapping = databaseMapping.EntityContainerMappings.Single<EntityContainerMapping>();
			AssociationSetMapping associationSetMapping = new AssociationSetMapping(associationSet, entitySet, entityContainerMapping).Initialize();
			entityContainerMapping.AddSetMapping(associationSetMapping);
			return associationSetMapping;
		}
	}
}
