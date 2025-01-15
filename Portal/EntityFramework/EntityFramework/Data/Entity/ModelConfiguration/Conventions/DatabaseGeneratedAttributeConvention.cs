using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x0200018C RID: 396
	public class DatabaseGeneratedAttributeConvention : PrimitivePropertyAttributeConfigurationConvention<DatabaseGeneratedAttribute>
	{
		// Token: 0x06001725 RID: 5925 RVA: 0x0003D9A0 File Offset: 0x0003BBA0
		public override void Apply(ConventionPrimitivePropertyConfiguration configuration, DatabaseGeneratedAttribute attribute)
		{
			Check.NotNull<ConventionPrimitivePropertyConfiguration>(configuration, "configuration");
			Check.NotNull<DatabaseGeneratedAttribute>(attribute, "attribute");
			configuration.HasDatabaseGeneratedOption(attribute.DatabaseGeneratedOption);
		}
	}
}
