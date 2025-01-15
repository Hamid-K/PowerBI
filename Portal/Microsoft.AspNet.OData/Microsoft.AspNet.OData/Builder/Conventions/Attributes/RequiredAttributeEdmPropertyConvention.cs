using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x0200017D RID: 381
	internal class RequiredAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<PropertyConfiguration>
	{
		// Token: 0x06000CB6 RID: 3254 RVA: 0x00032116 File Offset: 0x00030316
		public RequiredAttributeEdmPropertyConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(RequiredAttribute), false)
		{
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00032140 File Offset: 0x00030340
		public override void Apply(PropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			if (!edmProperty.AddedExplicitly)
			{
				StructuralPropertyConfiguration structuralPropertyConfiguration = edmProperty as StructuralPropertyConfiguration;
				if (structuralPropertyConfiguration != null)
				{
					structuralPropertyConfiguration.OptionalProperty = false;
				}
				NavigationPropertyConfiguration navigationPropertyConfiguration = edmProperty as NavigationPropertyConfiguration;
				if (navigationPropertyConfiguration != null && navigationPropertyConfiguration.Multiplicity != EdmMultiplicity.Many)
				{
					navigationPropertyConfiguration.Required();
				}
			}
		}
	}
}
