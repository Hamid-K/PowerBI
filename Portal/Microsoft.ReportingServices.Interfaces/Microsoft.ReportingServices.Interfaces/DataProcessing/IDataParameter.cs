using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000015 RID: 21
	public interface IDataParameter
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002D RID: 45
		// (set) Token: 0x0600002E RID: 46
		string ParameterName { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002F RID: 47
		// (set) Token: 0x06000030 RID: 48
		object Value { get; set; }
	}
}
