using System;
using System.Data.SqlClient;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000836 RID: 2102
	public class SqlDbException : DbException
	{
		// Token: 0x060042E5 RID: 17125 RVA: 0x000E0534 File Offset: 0x000DE734
		public SqlDbException(SqlException ex)
		{
			this._exception = ex;
			this._exceptionInfo = new SqlExceptionInfo(ex);
		}

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x060042E6 RID: 17126 RVA: 0x000E054F File Offset: 0x000DE74F
		public override IDbExceptionInfo ExceptionInfo
		{
			get
			{
				return this._exceptionInfo;
			}
		}

		// Token: 0x04002EEC RID: 12012
		private SqlException _exception;

		// Token: 0x04002EED RID: 12013
		private IDbExceptionInfo _exceptionInfo;
	}
}
