using System;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002AE RID: 686
	internal class DbConfigurationLoader
	{
		// Token: 0x060021B1 RID: 8625 RVA: 0x0005E834 File Offset: 0x0005CA34
		public virtual Type TryLoadFromConfig(AppConfig config)
		{
			string configurationTypeName = config.ConfigurationTypeName;
			if (string.IsNullOrWhiteSpace(configurationTypeName))
			{
				return null;
			}
			Type type;
			try
			{
				type = Type.GetType(configurationTypeName, true);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException(Strings.DbConfigurationTypeNotFound(configurationTypeName), ex);
			}
			if (!typeof(DbConfiguration).IsAssignableFrom(type))
			{
				throw new InvalidOperationException(Strings.CreateInstance_BadDbConfigurationType(type.ToString(), typeof(DbConfiguration).ToString()));
			}
			return type;
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x0005E8B0 File Offset: 0x0005CAB0
		public virtual bool AppConfigContainsDbConfigurationType(AppConfig config)
		{
			return !string.IsNullOrWhiteSpace(config.ConfigurationTypeName);
		}
	}
}
