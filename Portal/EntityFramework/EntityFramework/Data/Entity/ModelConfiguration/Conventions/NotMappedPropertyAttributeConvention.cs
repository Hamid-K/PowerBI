using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000192 RID: 402
	public class NotMappedPropertyAttributeConvention : PropertyAttributeConfigurationConvention<NotMappedAttribute>
	{
		// Token: 0x06001730 RID: 5936 RVA: 0x0003DCA9 File Offset: 0x0003BEA9
		public override void Apply(PropertyInfo memberInfo, ConventionTypeConfiguration configuration, NotMappedAttribute attribute)
		{
			Check.NotNull<PropertyInfo>(memberInfo, "memberInfo");
			Check.NotNull<ConventionTypeConfiguration>(configuration, "configuration");
			Check.NotNull<NotMappedAttribute>(attribute, "attribute");
			configuration.Ignore(memberInfo);
		}
	}
}
