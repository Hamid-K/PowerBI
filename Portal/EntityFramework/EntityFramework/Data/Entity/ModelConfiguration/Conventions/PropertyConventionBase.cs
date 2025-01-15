using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000180 RID: 384
	internal abstract class PropertyConventionBase : IConfigurationConvention<PropertyInfo, global::System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive.PrimitivePropertyConfiguration>, IConvention
	{
		// Token: 0x060016F1 RID: 5873 RVA: 0x0003D429 File Offset: 0x0003B629
		public PropertyConventionBase(IEnumerable<Func<PropertyInfo, bool>> predicates)
		{
			this._predicates = predicates;
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x0003D438 File Offset: 0x0003B638
		internal IEnumerable<Func<PropertyInfo, bool>> Predicates
		{
			get
			{
				return this._predicates;
			}
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x0003D440 File Offset: 0x0003B640
		public void Apply(PropertyInfo memberInfo, Func<global::System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive.PrimitivePropertyConfiguration> configuration, ModelConfiguration modelConfiguration)
		{
			if (this._predicates.All((Func<PropertyInfo, bool> p) => p(memberInfo)))
			{
				this.ApplyCore(memberInfo, configuration, modelConfiguration);
			}
		}

		// Token: 0x060016F4 RID: 5876
		protected abstract void ApplyCore(PropertyInfo memberInfo, Func<global::System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive.PrimitivePropertyConfiguration> configuration, ModelConfiguration modelConfiguration);

		// Token: 0x04000A21 RID: 2593
		private readonly IEnumerable<Func<PropertyInfo, bool>> _predicates;
	}
}
