using System;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001C7 RID: 455
	internal class ConventionsTypeFilter
	{
		// Token: 0x0600180F RID: 6159 RVA: 0x00041769 File Offset: 0x0003F969
		public virtual bool IsConvention(Type conventionType)
		{
			return ConventionsTypeFilter.IsConfigurationConvention(conventionType) || ConventionsTypeFilter.IsConceptualModelConvention(conventionType) || ConventionsTypeFilter.IsConceptualToStoreMappingConvention(conventionType) || ConventionsTypeFilter.IsStoreModelConvention(conventionType);
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x0004178C File Offset: 0x0003F98C
		public static bool IsConfigurationConvention(Type conventionType)
		{
			return typeof(IConfigurationConvention).IsAssignableFrom(conventionType) || typeof(Convention).IsAssignableFrom(conventionType) || conventionType.GetGenericTypeImplementations(typeof(IConfigurationConvention<>)).Any<Type>() || conventionType.GetGenericTypeImplementations(typeof(IConfigurationConvention<, >)).Any<Type>();
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x000417EB File Offset: 0x0003F9EB
		public static bool IsConceptualModelConvention(Type conventionType)
		{
			return conventionType.GetGenericTypeImplementations(typeof(IConceptualModelConvention<>)).Any<Type>();
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x00041802 File Offset: 0x0003FA02
		public static bool IsStoreModelConvention(Type conventionType)
		{
			return conventionType.GetGenericTypeImplementations(typeof(IStoreModelConvention<>)).Any<Type>();
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00041819 File Offset: 0x0003FA19
		public static bool IsConceptualToStoreMappingConvention(Type conventionType)
		{
			return typeof(IDbMappingConvention).IsAssignableFrom(conventionType);
		}
	}
}
