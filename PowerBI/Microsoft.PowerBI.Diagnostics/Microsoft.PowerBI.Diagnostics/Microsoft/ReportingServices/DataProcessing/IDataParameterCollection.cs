using System;
using System.Collections;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000027 RID: 39
	public interface IDataParameterCollection : IEnumerable
	{
		// Token: 0x060000A4 RID: 164
		int Add(IDataParameter parameter);
	}
}
