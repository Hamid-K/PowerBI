using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000186 RID: 390
	internal class TypeConventionWithHaving<T, TValue> : TypeConventionWithHavingBase<TValue> where T : class where TValue : class
	{
		// Token: 0x06001713 RID: 5907 RVA: 0x0003D70C File Offset: 0x0003B90C
		public TypeConventionWithHaving(IEnumerable<Func<Type, bool>> predicates, Func<Type, TValue> capturingPredicate, Action<ConventionTypeConfiguration<T>, TValue> entityConfigurationAction)
			: base(predicates.Prepend(TypeConvention<T>.OfTypePredicate), capturingPredicate)
		{
			this._entityConfigurationAction = entityConfigurationAction;
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001714 RID: 5908 RVA: 0x0003D727 File Offset: 0x0003B927
		internal Action<ConventionTypeConfiguration<T>, TValue> EntityConfigurationAction
		{
			get
			{
				return this._entityConfigurationAction;
			}
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x0003D72F File Offset: 0x0003B92F
		protected override void InvokeAction(Type memberInfo, ModelConfiguration modelConfiguration, TValue value)
		{
			this._entityConfigurationAction(new ConventionTypeConfiguration<T>(memberInfo, modelConfiguration), value);
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x0003D744 File Offset: 0x0003B944
		protected override void InvokeAction(Type memberInfo, Func<EntityTypeConfiguration> configuration, ModelConfiguration modelConfiguration, TValue value)
		{
			this._entityConfigurationAction(new ConventionTypeConfiguration<T>(memberInfo, configuration, modelConfiguration), value);
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x0003D75B File Offset: 0x0003B95B
		protected override void InvokeAction(Type memberInfo, Func<ComplexTypeConfiguration> configuration, ModelConfiguration modelConfiguration, TValue value)
		{
			this._entityConfigurationAction(new ConventionTypeConfiguration<T>(memberInfo, configuration, modelConfiguration), value);
		}

		// Token: 0x04000A28 RID: 2600
		private readonly Action<ConventionTypeConfiguration<T>, TValue> _entityConfigurationAction;
	}
}
