using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000181 RID: 385
	internal class PropertyConventionWithHaving<T> : PropertyConventionBase where T : class
	{
		// Token: 0x060016F5 RID: 5877 RVA: 0x0003D481 File Offset: 0x0003B681
		public PropertyConventionWithHaving(IEnumerable<Func<PropertyInfo, bool>> predicates, Func<PropertyInfo, T> capturingPredicate, Action<ConventionPrimitivePropertyConfiguration, T> propertyConfigurationAction)
			: base(predicates)
		{
			this._capturingPredicate = capturingPredicate;
			this._propertyConfigurationAction = propertyConfigurationAction;
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x0003D498 File Offset: 0x0003B698
		internal Func<PropertyInfo, T> CapturingPredicate
		{
			get
			{
				return this._capturingPredicate;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x0003D4A0 File Offset: 0x0003B6A0
		internal Action<ConventionPrimitivePropertyConfiguration, T> PropertyConfigurationAction
		{
			get
			{
				return this._propertyConfigurationAction;
			}
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x0003D4A8 File Offset: 0x0003B6A8
		protected override void ApplyCore(PropertyInfo memberInfo, Func<global::System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive.PrimitivePropertyConfiguration> configuration, ModelConfiguration modelConfiguration)
		{
			T t = this._capturingPredicate(memberInfo);
			if (t != null)
			{
				this._propertyConfigurationAction(new ConventionPrimitivePropertyConfiguration(memberInfo, configuration), t);
			}
		}

		// Token: 0x04000A22 RID: 2594
		private readonly Func<PropertyInfo, T> _capturingPredicate;

		// Token: 0x04000A23 RID: 2595
		private readonly Action<ConventionPrimitivePropertyConfiguration, T> _propertyConfigurationAction;
	}
}
