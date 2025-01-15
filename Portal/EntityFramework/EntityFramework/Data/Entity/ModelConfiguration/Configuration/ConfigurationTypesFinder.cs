using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration.Types;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001C4 RID: 452
	internal class ConfigurationTypesFinder
	{
		// Token: 0x060017E9 RID: 6121 RVA: 0x00040BA3 File Offset: 0x0003EDA3
		public ConfigurationTypesFinder()
			: this(new ConfigurationTypeActivator(), new ConfigurationTypeFilter())
		{
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00040BB5 File Offset: 0x0003EDB5
		public ConfigurationTypesFinder(ConfigurationTypeActivator activator, ConfigurationTypeFilter filter)
		{
			this._activator = activator;
			this._filter = filter;
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x00040BCC File Offset: 0x0003EDCC
		public virtual void AddConfigurationTypesToModel(IEnumerable<Type> types, ModelConfiguration modelConfiguration)
		{
			foreach (Type type in types)
			{
				if (this._filter.IsEntityTypeConfiguration(type))
				{
					modelConfiguration.Add(this._activator.Activate<EntityTypeConfiguration>(type));
				}
				else if (this._filter.IsComplexTypeConfiguration(type))
				{
					modelConfiguration.Add(this._activator.Activate<ComplexTypeConfiguration>(type));
				}
			}
		}

		// Token: 0x04000A48 RID: 2632
		private readonly ConfigurationTypeActivator _activator;

		// Token: 0x04000A49 RID: 2633
		private readonly ConfigurationTypeFilter _filter;
	}
}
