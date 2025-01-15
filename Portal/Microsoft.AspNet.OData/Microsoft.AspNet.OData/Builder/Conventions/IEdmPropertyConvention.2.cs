using System;

namespace Microsoft.AspNet.OData.Builder.Conventions
{
	// Token: 0x02000150 RID: 336
	internal interface IEdmPropertyConvention<TPropertyConfiguration> : IEdmPropertyConvention, IConvention where TPropertyConfiguration : PropertyConfiguration
	{
		// Token: 0x06000C47 RID: 3143
		void Apply(TPropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, ODataConventionModelBuilder model);
	}
}
