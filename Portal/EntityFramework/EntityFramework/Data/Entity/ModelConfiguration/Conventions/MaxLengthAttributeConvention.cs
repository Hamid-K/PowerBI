using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000191 RID: 401
	public class MaxLengthAttributeConvention : PrimitivePropertyAttributeConfigurationConvention<MaxLengthAttribute>
	{
		// Token: 0x0600172E RID: 5934 RVA: 0x0003DC34 File Offset: 0x0003BE34
		public override void Apply(ConventionPrimitivePropertyConfiguration configuration, MaxLengthAttribute attribute)
		{
			Check.NotNull<ConventionPrimitivePropertyConfiguration>(configuration, "configuration");
			Check.NotNull<MaxLengthAttribute>(attribute, "attribute");
			PropertyInfo clrPropertyInfo = configuration.ClrPropertyInfo;
			if (attribute.Length == 0 || attribute.Length < -1)
			{
				throw Error.MaxLengthAttributeConvention_InvalidMaxLength(clrPropertyInfo.Name, clrPropertyInfo.ReflectedType);
			}
			if (attribute.Length == -1)
			{
				configuration.IsMaxLength();
				return;
			}
			configuration.HasMaxLength(attribute.Length);
		}

		// Token: 0x04000A2C RID: 2604
		private const int MaxLengthIndicator = -1;
	}
}
