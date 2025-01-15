using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x0200019C RID: 412
	public abstract class TypeAttributeConfigurationConvention<TAttribute> : Convention where TAttribute : Attribute
	{
		// Token: 0x06001747 RID: 5959 RVA: 0x0003E01B File Offset: 0x0003C21B
		protected TypeAttributeConfigurationConvention()
		{
			base.Types().Having<IEnumerable<TAttribute>>((Type t) => this._attributeProvider.GetAttributes(t).OfType<TAttribute>()).Configure(delegate(ConventionTypeConfiguration configuration, IEnumerable<TAttribute> attributes)
			{
				foreach (TAttribute tattribute in attributes)
				{
					this.Apply(configuration, tattribute);
				}
			});
		}

		// Token: 0x06001748 RID: 5960
		public abstract void Apply(ConventionTypeConfiguration configuration, TAttribute attribute);

		// Token: 0x04000A30 RID: 2608
		private readonly AttributeProvider _attributeProvider = DbConfiguration.DependencyResolver.GetService<AttributeProvider>();
	}
}
