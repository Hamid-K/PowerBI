using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x02000662 RID: 1634
	public interface ITracePointPropertyInformation
	{
		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06003671 RID: 13937
		int Identifier { get; }

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06003672 RID: 13938
		string Name { get; }

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06003673 RID: 13939
		PropertyType ValueType { get; }

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06003674 RID: 13940
		List<ITracePointPropertyEnumerationValue> EnumerationValues { get; }
	}
}
