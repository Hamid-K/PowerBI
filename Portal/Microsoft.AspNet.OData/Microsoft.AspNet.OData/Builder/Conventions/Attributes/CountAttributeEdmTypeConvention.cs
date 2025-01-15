using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x0200015A RID: 346
	internal class CountAttributeEdmTypeConvention : AttributeEdmTypeConvention<StructuralTypeConfiguration>
	{
		// Token: 0x06000C64 RID: 3172 RVA: 0x000307E9 File Offset: 0x0002E9E9
		public CountAttributeEdmTypeConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(CountAttribute), false)
		{
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00030814 File Offset: 0x0002EA14
		public override void Apply(StructuralTypeConfiguration edmTypeConfiguration, ODataConventionModelBuilder model, Attribute attribute)
		{
			if (edmTypeConfiguration == null)
			{
				throw Error.ArgumentNull("edmTypeConfiguration");
			}
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			if (!edmTypeConfiguration.AddedExplicitly)
			{
				if ((attribute as CountAttribute).Disabled)
				{
					edmTypeConfiguration.QueryConfiguration.GetModelBoundQuerySettingsOrDefault().Countable = new bool?(false);
					return;
				}
				edmTypeConfiguration.QueryConfiguration.GetModelBoundQuerySettingsOrDefault().Countable = new bool?(true);
			}
		}
	}
}
