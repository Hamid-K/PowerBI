using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x0200019B RID: 411
	public class TableAttributeConvention : TypeAttributeConfigurationConvention<TableAttribute>
	{
		// Token: 0x06001745 RID: 5957 RVA: 0x0003DFC0 File Offset: 0x0003C1C0
		public override void Apply(ConventionTypeConfiguration configuration, TableAttribute attribute)
		{
			Check.NotNull<ConventionTypeConfiguration>(configuration, "configuration");
			Check.NotNull<TableAttribute>(attribute, "attribute");
			if (string.IsNullOrWhiteSpace(attribute.Schema))
			{
				configuration.ToTable(attribute.Name);
				return;
			}
			configuration.ToTable(attribute.Name, attribute.Schema);
		}
	}
}
