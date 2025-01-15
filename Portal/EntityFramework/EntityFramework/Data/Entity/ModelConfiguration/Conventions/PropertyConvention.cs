using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x0200017F RID: 383
	internal class PropertyConvention : PropertyConventionBase
	{
		// Token: 0x060016EE RID: 5870 RVA: 0x0003D3FD File Offset: 0x0003B5FD
		public PropertyConvention(IEnumerable<Func<PropertyInfo, bool>> predicates, Action<ConventionPrimitivePropertyConfiguration> propertyConfigurationAction)
			: base(predicates)
		{
			this._propertyConfigurationAction = propertyConfigurationAction;
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x0003D40D File Offset: 0x0003B60D
		internal Action<ConventionPrimitivePropertyConfiguration> PropertyConfigurationAction
		{
			get
			{
				return this._propertyConfigurationAction;
			}
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x0003D415 File Offset: 0x0003B615
		protected override void ApplyCore(PropertyInfo memberInfo, Func<global::System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive.PrimitivePropertyConfiguration> configuration, ModelConfiguration modelConfiguration)
		{
			this._propertyConfigurationAction(new ConventionPrimitivePropertyConfiguration(memberInfo, configuration));
		}

		// Token: 0x04000A20 RID: 2592
		private readonly Action<ConventionPrimitivePropertyConfiguration> _propertyConfigurationAction;
	}
}
