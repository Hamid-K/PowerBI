using System;

namespace Microsoft.AspNet.OData.Builder.Conventions
{
	// Token: 0x0200014B RID: 331
	internal interface IOperationConvention : IConvention
	{
		// Token: 0x06000C3F RID: 3135
		void Apply(OperationConfiguration configuration, ODataModelBuilder model);
	}
}
