using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000029 RID: 41
	public interface IDataMultiValueParameter : IDataParameter
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A9 RID: 169
		// (set) Token: 0x060000AA RID: 170
		object[] Values { get; set; }
	}
}
