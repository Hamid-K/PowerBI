using System;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x02000053 RID: 83
	public interface IParameter
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000EB RID: 235
		string Name { get; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000EC RID: 236
		bool IsMultiValue { get; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000ED RID: 237
		object[] Values { get; }
	}
}
