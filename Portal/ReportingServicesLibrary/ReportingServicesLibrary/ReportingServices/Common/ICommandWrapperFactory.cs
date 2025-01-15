using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000356 RID: 854
	internal interface ICommandWrapperFactory
	{
		// Token: 0x06001C32 RID: 7218
		IDbCommand WrapSqlCommand(SqlCommand command);
	}
}
