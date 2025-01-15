using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000028 RID: 40
	public interface IDataParameter
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A5 RID: 165
		// (set) Token: 0x060000A6 RID: 166
		string ParameterName { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A7 RID: 167
		// (set) Token: 0x060000A8 RID: 168
		object Value { get; set; }
	}
}
