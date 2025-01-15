using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000AD RID: 173
	internal interface IDependent : IFlushCaches
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060003EF RID: 1007
		HashSetInternal<IDependencyTrigger> DependsOn { get; }
	}
}
