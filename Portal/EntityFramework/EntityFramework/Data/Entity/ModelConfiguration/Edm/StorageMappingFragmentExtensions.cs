using System;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000173 RID: 371
	internal static class StorageMappingFragmentExtensions
	{
		// Token: 0x060016B2 RID: 5810 RVA: 0x0003BCA2 File Offset: 0x00039EA2
		public static EdmProperty GetDefaultDiscriminator(this MappingFragment entityTypeMapppingFragment)
		{
			return (EdmProperty)entityTypeMapppingFragment.Annotations.GetAnnotation("DefaultDiscriminator");
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0003BCB9 File Offset: 0x00039EB9
		public static void SetDefaultDiscriminator(this MappingFragment entityTypeMappingFragment, EdmProperty discriminator)
		{
			entityTypeMappingFragment.Annotations.SetAnnotation("DefaultDiscriminator", discriminator);
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0003BCCC File Offset: 0x00039ECC
		public static void RemoveDefaultDiscriminatorAnnotation(this MappingFragment entityTypeMappingFragment)
		{
			entityTypeMappingFragment.Annotations.RemoveAnnotation("DefaultDiscriminator");
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0003BCE0 File Offset: 0x00039EE0
		public static void RemoveDefaultDiscriminator(this MappingFragment entityTypeMappingFragment, EntitySetMapping entitySetMapping)
		{
			EdmProperty discriminatorColumn = entityTypeMappingFragment.RemoveDefaultDiscriminatorCondition();
			if (discriminatorColumn != null)
			{
				EntityType table = entityTypeMappingFragment.Table;
				table.Properties.Where((EdmProperty c) => c.Name.Equals(discriminatorColumn.Name, StringComparison.Ordinal)).ToList<EdmProperty>().Each(new Action<EdmProperty>(table.RemoveMember));
			}
			if (entitySetMapping != null && entityTypeMappingFragment.IsConditionOnlyFragment() && !entityTypeMappingFragment.ColumnConditions.Any<ConditionPropertyMapping>())
			{
				EntityTypeMapping entityTypeMapping = entitySetMapping.EntityTypeMappings.Single((EntityTypeMapping etm) => etm.MappingFragments.Contains(entityTypeMappingFragment));
				entityTypeMapping.RemoveFragment(entityTypeMappingFragment);
				if (entityTypeMapping.MappingFragments.Count == 0)
				{
					entitySetMapping.RemoveTypeMapping(entityTypeMapping);
				}
			}
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x0003BDA7 File Offset: 0x00039FA7
		public static EdmProperty RemoveDefaultDiscriminatorCondition(this MappingFragment entityTypeMappingFragment)
		{
			EdmProperty defaultDiscriminator = entityTypeMappingFragment.GetDefaultDiscriminator();
			if (defaultDiscriminator != null && entityTypeMappingFragment.ColumnConditions.Any<ConditionPropertyMapping>())
			{
				entityTypeMappingFragment.ClearConditions();
			}
			entityTypeMappingFragment.RemoveDefaultDiscriminatorAnnotation();
			return defaultDiscriminator;
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x0003BDCB File Offset: 0x00039FCB
		public static void AddDiscriminatorCondition(this MappingFragment entityTypeMapppingFragment, EdmProperty discriminatorColumn, object value)
		{
			entityTypeMapppingFragment.AddConditionProperty(new ValueConditionMapping(discriminatorColumn, value));
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x0003BDDA File Offset: 0x00039FDA
		public static void AddNullabilityCondition(this MappingFragment entityTypeMapppingFragment, EdmProperty column, bool isNull)
		{
			entityTypeMapppingFragment.AddConditionProperty(new IsNullConditionMapping(column, isNull));
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x0003BDEC File Offset: 0x00039FEC
		public static bool IsConditionOnlyFragment(this MappingFragment entityTypeMapppingFragment)
		{
			object annotation = entityTypeMapppingFragment.Annotations.GetAnnotation("ConditionOnlyFragment");
			return annotation != null && (bool)annotation;
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x0003BE15 File Offset: 0x0003A015
		public static void SetIsConditionOnlyFragment(this MappingFragment entityTypeMapppingFragment, bool isConditionOnlyFragment)
		{
			if (isConditionOnlyFragment)
			{
				entityTypeMapppingFragment.Annotations.SetAnnotation("ConditionOnlyFragment", isConditionOnlyFragment);
				return;
			}
			entityTypeMapppingFragment.Annotations.RemoveAnnotation("ConditionOnlyFragment");
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x0003BE44 File Offset: 0x0003A044
		public static bool IsUnmappedPropertiesFragment(this MappingFragment entityTypeMapppingFragment)
		{
			object annotation = entityTypeMapppingFragment.Annotations.GetAnnotation("UnmappedPropertiesFragment");
			return annotation != null && (bool)annotation;
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x0003BE6D File Offset: 0x0003A06D
		public static void SetIsUnmappedPropertiesFragment(this MappingFragment entityTypeMapppingFragment, bool isUnmappedPropertiesFragment)
		{
			if (isUnmappedPropertiesFragment)
			{
				entityTypeMapppingFragment.Annotations.SetAnnotation("UnmappedPropertiesFragment", isUnmappedPropertiesFragment);
				return;
			}
			entityTypeMapppingFragment.Annotations.RemoveAnnotation("UnmappedPropertiesFragment");
		}

		// Token: 0x04000A10 RID: 2576
		private const string DefaultDiscriminatorAnnotation = "DefaultDiscriminator";

		// Token: 0x04000A11 RID: 2577
		private const string ConditionOnlyFragmentAnnotation = "ConditionOnlyFragment";

		// Token: 0x04000A12 RID: 2578
		private const string UnmappedPropertiesFragmentAnnotation = "UnmappedPropertiesFragment";
	}
}
