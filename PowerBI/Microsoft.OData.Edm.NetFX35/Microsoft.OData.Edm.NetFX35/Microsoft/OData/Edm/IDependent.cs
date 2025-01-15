using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020001F5 RID: 501
	internal interface IDependent : IFlushCaches
	{
		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000BA4 RID: 2980
		HashSetInternal<IDependencyTrigger> DependsOn { get; }
	}
}
