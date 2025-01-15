using System;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000172 RID: 370
	internal static class StorageEntityTypeMappingExtensions
	{
		// Token: 0x060016AA RID: 5802 RVA: 0x0003BBA0 File Offset: 0x00039DA0
		public static object GetConfiguration(this EntityTypeMapping entityTypeMapping)
		{
			return entityTypeMapping.Annotations.GetConfiguration();
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x0003BBAD File Offset: 0x00039DAD
		public static void SetConfiguration(this EntityTypeMapping entityTypeMapping, object configuration)
		{
			entityTypeMapping.Annotations.SetConfiguration(configuration);
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x0003BBBC File Offset: 0x00039DBC
		public static ColumnMappingBuilder GetPropertyMapping(this EntityTypeMapping entityTypeMapping, params EdmProperty[] propertyPath)
		{
			return entityTypeMapping.MappingFragments.SelectMany((MappingFragment f) => f.ColumnMappings).Single((ColumnMappingBuilder p) => p.PropertyPath.SequenceEqual(propertyPath));
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x0003BC11 File Offset: 0x00039E11
		public static EntityType GetPrimaryTable(this EntityTypeMapping entityTypeMapping)
		{
			return entityTypeMapping.MappingFragments.First<MappingFragment>().Table;
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x0003BC24 File Offset: 0x00039E24
		public static bool UsesOtherTables(this EntityTypeMapping entityTypeMapping, EntityType table)
		{
			return entityTypeMapping.MappingFragments.Any((MappingFragment f) => f.Table != table);
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x0003BC55 File Offset: 0x00039E55
		public static Type GetClrType(this EntityTypeMapping entityTypeMappping)
		{
			return entityTypeMappping.Annotations.GetClrType();
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x0003BC62 File Offset: 0x00039E62
		public static void SetClrType(this EntityTypeMapping entityTypeMapping, Type type)
		{
			entityTypeMapping.Annotations.SetClrType(type);
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x0003BC70 File Offset: 0x00039E70
		public static EntityTypeMapping Clone(this EntityTypeMapping entityTypeMapping)
		{
			EntityTypeMapping entityTypeMapping2 = new EntityTypeMapping(null);
			entityTypeMapping2.AddType(entityTypeMapping.EntityType);
			entityTypeMapping.Annotations.Copy(entityTypeMapping2.Annotations);
			return entityTypeMapping2;
		}
	}
}
