using System;

namespace Microsoft.AspNet.OData.Builder.Conventions
{
	// Token: 0x02000148 RID: 328
	internal interface INavigationSourceConvention : IConvention
	{
		// Token: 0x06000C3A RID: 3130
		void Apply(NavigationSourceConfiguration configuration, ODataModelBuilder model);
	}
}
