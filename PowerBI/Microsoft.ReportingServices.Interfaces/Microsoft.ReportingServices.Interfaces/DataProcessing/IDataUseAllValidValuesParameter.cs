using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000017 RID: 23
	public interface IDataUseAllValidValuesParameter : IDataMultiValueParameter, IDataParameter
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000033 RID: 51
		// (set) Token: 0x06000034 RID: 52
		bool UseAllValidValues { get; set; }
	}
}
