using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001C3 RID: 451
	internal class ConfigurationTypeFilter
	{
		// Token: 0x060017E5 RID: 6117 RVA: 0x00040B5E File Offset: 0x0003ED5E
		public virtual bool IsEntityTypeConfiguration(Type type)
		{
			return ConfigurationTypeFilter.IsStructuralTypeConfiguration(type, typeof(EntityTypeConfiguration<>));
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x00040B70 File Offset: 0x0003ED70
		public virtual bool IsComplexTypeConfiguration(Type type)
		{
			return ConfigurationTypeFilter.IsStructuralTypeConfiguration(type, typeof(ComplexTypeConfiguration<>));
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x00040B82 File Offset: 0x0003ED82
		private static bool IsStructuralTypeConfiguration(Type type, Type structuralTypeConfiguration)
		{
			return !type.IsAbstract() && type.TryGetElementType(structuralTypeConfiguration) != null;
		}
	}
}
