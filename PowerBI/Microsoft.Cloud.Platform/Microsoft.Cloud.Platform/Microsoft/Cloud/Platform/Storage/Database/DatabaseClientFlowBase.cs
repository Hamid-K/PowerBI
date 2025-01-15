using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x0200002E RID: 46
	public abstract class DatabaseClientFlowBase : Sequencer
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00004648 File Offset: 0x00002848
		protected DatabaseClientFlowBase(DatabaseClientBase client)
		{
			this.m_client = client;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004657 File Offset: 0x00002857
		protected IAsyncResult BeginExecuteNonQuery(string procedure, SqlParameter[] parameters, QueryExecutionOptions options, ExceptionTranslator exceptionTranslator, AsyncCallback asyncCallback, object asyncState)
		{
			return this.m_client.BeginExecuteNonQuery(procedure, parameters, options, exceptionTranslator, asyncCallback, asyncState);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000466D File Offset: 0x0000286D
		protected int EndExecuteNonQuery(IAsyncResult asyncResult)
		{
			return this.m_client.EndExecuteNonQuery(asyncResult);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000467C File Offset: 0x0000287C
		protected IAsyncResult BeginExecuteReaderSingleRow<T>(string procedure, SqlParameter[] parameters, RowProcessor<T> processor, QueryExecutionOptions executionOptions, QueryResultOptions resultOptions, ExceptionTranslator exceptionTranslator, AsyncCallback asyncCallback, object asyncState)
		{
			return this.m_client.BeginExecuteReaderSingleRow<T>(procedure, parameters, processor, executionOptions, resultOptions, exceptionTranslator, asyncCallback, asyncState);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000046A1 File Offset: 0x000028A1
		protected T EndExecuteReaderSingleRow<T>(IAsyncResult asyncResult)
		{
			return this.m_client.EndExecuteReaderSingleRow<T>(asyncResult);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000046AF File Offset: 0x000028AF
		protected IAsyncResult BeginExecuteReader<T>(string procedure, SqlParameter[] parameters, RowProcessor<T> processor, QueryExecutionOptions executionOptions, ExceptionTranslator exceptionTranslator, AsyncCallback asyncCallback, object asyncState)
		{
			return this.m_client.BeginExecuteReader<T>(procedure, parameters, processor, executionOptions, exceptionTranslator, asyncCallback, asyncState);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000046C7 File Offset: 0x000028C7
		protected IEnumerable<T> EndExecuteReader<T>(IAsyncResult asyncResult)
		{
			return this.m_client.EndExecuteReader<T>(asyncResult);
		}

		// Token: 0x04000086 RID: 134
		private readonly DatabaseClientBase m_client;
	}
}
