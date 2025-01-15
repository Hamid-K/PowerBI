using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x0200016E RID: 366
	internal class NotCountableAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<PropertyConfiguration>
	{
		// Token: 0x06000C8E RID: 3214 RVA: 0x0003199C File Offset: 0x0002FB9C
		public NotCountableAttributeEdmPropertyConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(NotCountableAttribute), false)
		{
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x000319C4 File Offset: 0x0002FBC4
		public override void Apply(PropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			if (!edmProperty.AddedExplicitly)
			{
				edmProperty.IsNotCountable();
			}
		}
	}
}
