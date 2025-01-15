using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020001F3 RID: 499
	internal interface IDependencyTrigger
	{
		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000BA2 RID: 2978
		HashSetInternal<IDependent> Dependents { get; }
	}
}
