using System;
using System.Data;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009FD RID: 2557
	internal class SqlParameter : DrdaParameter, ISqlParameter, IDbDataParameter, IDataParameter
	{
		// Token: 0x17001386 RID: 4998
		// (get) Token: 0x06005065 RID: 20581 RVA: 0x00142093 File Offset: 0x00140293
		DrdaClientType ISqlParameter.DrdaType
		{
			get
			{
				return base.Binding.Type.DrdaType;
			}
		}
	}
}
