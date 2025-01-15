using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Types;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000184 RID: 388
	internal class TypeConventionWithHaving<T> : TypeConventionWithHavingBase<T> where T : class
	{
		// Token: 0x06001706 RID: 5894 RVA: 0x0003D611 File Offset: 0x0003B811
		public TypeConventionWithHaving(IEnumerable<Func<Type, bool>> predicates, Func<Type, T> capturingPredicate, Action<ConventionTypeConfiguration, T> entityConfigurationAction)
			: base(predicates, capturingPredicate)
		{
			this._entityConfigurationAction = entityConfigurationAction;
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x0003D622 File Offset: 0x0003B822
		internal Action<ConventionTypeConfiguration, T> EntityConfigurationAction
		{
			get
			{
				return this._entityConfigurationAction;
			}
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x0003D62A File Offset: 0x0003B82A
		protected override void InvokeAction(Type memberInfo, ModelConfiguration modelConfiguration, T value)
		{
			this._entityConfigurationAction(new ConventionTypeConfiguration(memberInfo, modelConfiguration), value);
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0003D63F File Offset: 0x0003B83F
		protected override void InvokeAction(Type memberInfo, Func<EntityTypeConfiguration> configuration, ModelConfiguration modelConfiguration, T value)
		{
			this._entityConfigurationAction(new ConventionTypeConfiguration(memberInfo, configuration, modelConfiguration), value);
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x0003D656 File Offset: 0x0003B856
		protected override void InvokeAction(Type memberInfo, Func<ComplexTypeConfiguration> configuration, ModelConfiguration modelConfiguration, T value)
		{
			this._entityConfigurationAction(new ConventionTypeConfiguration(memberInfo, configuration, modelConfiguration), value);
		}

		// Token: 0x04000A26 RID: 2598
		private readonly Action<ConventionTypeConfiguration, T> _entityConfigurationAction;
	}
}
