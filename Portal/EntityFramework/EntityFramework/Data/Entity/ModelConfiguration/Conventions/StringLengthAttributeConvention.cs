using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000197 RID: 407
	public class StringLengthAttributeConvention : PrimitivePropertyAttributeConfigurationConvention<StringLengthAttribute>
	{
		// Token: 0x0600173D RID: 5949 RVA: 0x0003DEE8 File Offset: 0x0003C0E8
		public override void Apply(ConventionPrimitivePropertyConfiguration configuration, StringLengthAttribute attribute)
		{
			Check.NotNull<ConventionPrimitivePropertyConfiguration>(configuration, "configuration");
			Check.NotNull<StringLengthAttribute>(attribute, "attribute");
			if (attribute.MaximumLength < 1)
			{
				PropertyInfo clrPropertyInfo = configuration.ClrPropertyInfo;
				throw Error.StringLengthAttributeConvention_InvalidMaximumLength(clrPropertyInfo.Name, clrPropertyInfo.ReflectedType);
			}
			configuration.HasMaxLength(attribute.MaximumLength);
		}
	}
}
