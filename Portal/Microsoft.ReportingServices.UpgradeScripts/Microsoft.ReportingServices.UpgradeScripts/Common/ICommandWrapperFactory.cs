using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000008 RID: 8
	internal interface ICommandWrapperFactory
	{
		// Token: 0x06000052 RID: 82
		IDbCommand WrapSqlCommand(SqlCommand command);
	}
}
