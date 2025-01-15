using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000017 RID: 23
	internal interface IDependent : IFlushCaches
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000177 RID: 375
		HashSetInternal<IDependencyTrigger> DependsOn { get; }
	}
}
