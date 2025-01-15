using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000193 RID: 403
	public abstract class PrimitivePropertyAttributeConfigurationConvention<TAttribute> : Convention where TAttribute : Attribute
	{
		// Token: 0x06001732 RID: 5938 RVA: 0x0003DCDF File Offset: 0x0003BEDF
		protected PrimitivePropertyAttributeConfigurationConvention()
		{
			base.Properties().Having<IEnumerable<TAttribute>>((PropertyInfo pi) => this._attributeProvider.GetAttributes(pi).OfType<TAttribute>()).Configure(delegate(ConventionPrimitivePropertyConfiguration configuration, IEnumerable<TAttribute> attributes)
			{
				foreach (TAttribute tattribute in attributes)
				{
					this.Apply(configuration, tattribute);
				}
			});
		}

		// Token: 0x06001733 RID: 5939
		public abstract void Apply(ConventionPrimitivePropertyConfiguration configuration, TAttribute attribute);

		// Token: 0x04000A2D RID: 2605
		private readonly AttributeProvider _attributeProvider = DbConfiguration.DependencyResolver.GetService<AttributeProvider>();
	}
}
