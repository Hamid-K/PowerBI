using System;

namespace Microsoft.AspNet.OData.Builder.Conventions
{
	// Token: 0x0200014F RID: 335
	internal interface IEdmPropertyConvention : IConvention
	{
		// Token: 0x06000C46 RID: 3142
		void Apply(PropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, ODataConventionModelBuilder model);
	}
}
