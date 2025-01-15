using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000196 RID: 406
	public class RequiredPrimitivePropertyAttributeConvention : PrimitivePropertyAttributeConfigurationConvention<RequiredAttribute>
	{
		// Token: 0x0600173B RID: 5947 RVA: 0x0003DEBE File Offset: 0x0003C0BE
		public override void Apply(ConventionPrimitivePropertyConfiguration configuration, RequiredAttribute attribute)
		{
			Check.NotNull<ConventionPrimitivePropertyConfiguration>(configuration, "configuration");
			Check.NotNull<RequiredAttribute>(attribute, "attribute");
			configuration.IsRequired();
		}
	}
}
