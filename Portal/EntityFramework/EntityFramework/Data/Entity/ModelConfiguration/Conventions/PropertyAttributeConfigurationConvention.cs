using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000194 RID: 404
	public abstract class PropertyAttributeConfigurationConvention<TAttribute> : Convention where TAttribute : Attribute
	{
		// Token: 0x06001736 RID: 5942 RVA: 0x0003DD80 File Offset: 0x0003BF80
		protected PropertyAttributeConfigurationConvention()
		{
			base.Types().Configure(delegate(ConventionTypeConfiguration ec)
			{
				foreach (PropertyInfo propertyInfo in ec.ClrType.GetInstanceProperties())
				{
					IList<Attribute> list = (IList<Attribute>)this._attributeProvider.GetAttributes(propertyInfo);
					for (int i = 0; i < list.Count; i++)
					{
						TAttribute tattribute = list[i] as TAttribute;
						if (tattribute != null)
						{
							this.Apply(propertyInfo, ec, tattribute);
						}
					}
				}
			});
		}

		// Token: 0x06001737 RID: 5943
		public abstract void Apply(PropertyInfo memberInfo, ConventionTypeConfiguration configuration, TAttribute attribute);

		// Token: 0x04000A2E RID: 2606
		private readonly AttributeProvider _attributeProvider = DbConfiguration.DependencyResolver.GetService<AttributeProvider>();
	}
}
