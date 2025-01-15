using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Types;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000182 RID: 386
	internal class TypeConvention : TypeConventionBase
	{
		// Token: 0x060016F9 RID: 5881 RVA: 0x0003D4DD File Offset: 0x0003B6DD
		public TypeConvention(IEnumerable<Func<Type, bool>> predicates, Action<ConventionTypeConfiguration> entityConfigurationAction)
			: base(predicates)
		{
			this._entityConfigurationAction = entityConfigurationAction;
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060016FA RID: 5882 RVA: 0x0003D4ED File Offset: 0x0003B6ED
		internal Action<ConventionTypeConfiguration> EntityConfigurationAction
		{
			get
			{
				return this._entityConfigurationAction;
			}
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x0003D4F5 File Offset: 0x0003B6F5
		protected override void ApplyCore(Type memberInfo, ModelConfiguration modelConfiguration)
		{
			this._entityConfigurationAction(new ConventionTypeConfiguration(memberInfo, modelConfiguration));
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x0003D509 File Offset: 0x0003B709
		protected override void ApplyCore(Type memberInfo, Func<EntityTypeConfiguration> configuration, ModelConfiguration modelConfiguration)
		{
			this._entityConfigurationAction(new ConventionTypeConfiguration(memberInfo, configuration, modelConfiguration));
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x0003D51E File Offset: 0x0003B71E
		protected override void ApplyCore(Type memberInfo, Func<ComplexTypeConfiguration> configuration, ModelConfiguration modelConfiguration)
		{
			this._entityConfigurationAction(new ConventionTypeConfiguration(memberInfo, configuration, modelConfiguration));
		}

		// Token: 0x04000A24 RID: 2596
		private readonly Action<ConventionTypeConfiguration> _entityConfigurationAction;
	}
}
