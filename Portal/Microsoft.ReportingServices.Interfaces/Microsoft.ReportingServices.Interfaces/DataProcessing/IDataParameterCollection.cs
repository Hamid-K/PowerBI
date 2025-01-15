using System;
using System.Collections;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000014 RID: 20
	public interface IDataParameterCollection : IEnumerable
	{
		// Token: 0x0600002C RID: 44
		int Add(IDataParameter parameter);
	}
}
