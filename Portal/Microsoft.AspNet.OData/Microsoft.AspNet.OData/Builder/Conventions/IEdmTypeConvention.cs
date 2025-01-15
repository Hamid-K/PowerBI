using System;

namespace Microsoft.AspNet.OData.Builder.Conventions
{
	// Token: 0x02000156 RID: 342
	internal interface IEdmTypeConvention : IConvention
	{
		// Token: 0x06000C5D RID: 3165
		void Apply(IEdmTypeConfiguration edmTypeConfiguration, ODataConventionModelBuilder model);
	}
}
