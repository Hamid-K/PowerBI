using System;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000058 RID: 88
	internal interface IIndexedValue<T>
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000389 RID: 905
		int Index { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600038A RID: 906
		T Value { get; }
	}
}
