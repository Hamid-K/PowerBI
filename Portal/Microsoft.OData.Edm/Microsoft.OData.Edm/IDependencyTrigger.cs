using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000AC RID: 172
	internal interface IDependencyTrigger
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060003EE RID: 1006
		HashSetInternal<IDependent> Dependents { get; }
	}
}
