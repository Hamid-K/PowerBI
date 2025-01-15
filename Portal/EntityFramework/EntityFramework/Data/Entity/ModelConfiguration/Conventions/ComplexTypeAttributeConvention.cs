using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000199 RID: 409
	public class ComplexTypeAttributeConvention : TypeAttributeConfigurationConvention<ComplexTypeAttribute>
	{
		// Token: 0x06001741 RID: 5953 RVA: 0x0003DF6D File Offset: 0x0003C16D
		public override void Apply(ConventionTypeConfiguration configuration, ComplexTypeAttribute attribute)
		{
			Check.NotNull<ConventionTypeConfiguration>(configuration, "configuration");
			Check.NotNull<ComplexTypeAttribute>(attribute, "attribute");
			configuration.IsComplexType();
		}
	}
}
