using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000016 RID: 22
	internal interface IDependencyTrigger
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000176 RID: 374
		HashSetInternal<IDependent> Dependents { get; }
	}
}
