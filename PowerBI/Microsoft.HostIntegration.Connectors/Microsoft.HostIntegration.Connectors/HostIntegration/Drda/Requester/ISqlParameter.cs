using System;
using System.Data;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x020008FE RID: 2302
	public interface ISqlParameter : IDbDataParameter, IDataParameter
	{
		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x06004895 RID: 18581
		DrdaClientType DrdaType { get; }
	}
}
