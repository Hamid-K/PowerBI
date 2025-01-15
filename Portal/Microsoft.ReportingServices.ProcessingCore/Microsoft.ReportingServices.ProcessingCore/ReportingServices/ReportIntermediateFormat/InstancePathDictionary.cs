using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003B7 RID: 951
	internal sealed class InstancePathDictionary<TValue> : Dictionary<List<InstancePathItem>, TValue>
	{
		// Token: 0x060026AC RID: 9900 RVA: 0x000B96B9 File Offset: 0x000B78B9
		public InstancePathDictionary()
			: base(InstancePathComparer.Instance)
		{
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x000B96C6 File Offset: 0x000B78C6
		public InstancePathDictionary(int capacity)
			: base(capacity, InstancePathComparer.Instance)
		{
		}
	}
}
