using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000198 RID: 408
	public class TimestampAttributeConvention : PrimitivePropertyAttributeConfigurationConvention<TimestampAttribute>
	{
		// Token: 0x0600173F RID: 5951 RVA: 0x0003DF44 File Offset: 0x0003C144
		public override void Apply(ConventionPrimitivePropertyConfiguration configuration, TimestampAttribute attribute)
		{
			Check.NotNull<ConventionPrimitivePropertyConfiguration>(configuration, "configuration");
			Check.NotNull<TimestampAttribute>(attribute, "attribute");
			configuration.IsRowVersion();
		}
	}
}
