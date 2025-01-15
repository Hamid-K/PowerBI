using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000016 RID: 22
	public interface IDataMultiValueParameter : IDataParameter
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000031 RID: 49
		// (set) Token: 0x06000032 RID: 50
		object[] Values { get; set; }
	}
}
