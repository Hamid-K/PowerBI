using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x0200018B RID: 395
	public class ConcurrencyCheckAttributeConvention : PrimitivePropertyAttributeConfigurationConvention<ConcurrencyCheckAttribute>
	{
		// Token: 0x06001723 RID: 5923 RVA: 0x0003D977 File Offset: 0x0003BB77
		public override void Apply(ConventionPrimitivePropertyConfiguration configuration, ConcurrencyCheckAttribute attribute)
		{
			Check.NotNull<ConventionPrimitivePropertyConfiguration>(configuration, "configuration");
			Check.NotNull<ConcurrencyCheckAttribute>(attribute, "attribute");
			configuration.IsConcurrencyToken();
		}
	}
}
