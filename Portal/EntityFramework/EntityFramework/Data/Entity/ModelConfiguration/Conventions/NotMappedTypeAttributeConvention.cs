using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x0200019A RID: 410
	public class NotMappedTypeAttributeConvention : TypeAttributeConfigurationConvention<NotMappedAttribute>
	{
		// Token: 0x06001743 RID: 5955 RVA: 0x0003DF96 File Offset: 0x0003C196
		public override void Apply(ConventionTypeConfiguration configuration, NotMappedAttribute attribute)
		{
			Check.NotNull<ConventionTypeConfiguration>(configuration, "configuration");
			Check.NotNull<NotMappedAttribute>(attribute, "attribute");
			configuration.Ignore();
		}
	}
}
