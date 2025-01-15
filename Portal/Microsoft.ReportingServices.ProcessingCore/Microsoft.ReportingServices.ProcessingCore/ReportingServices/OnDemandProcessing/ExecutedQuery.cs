using System;
using System.Threading;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007F0 RID: 2032
	internal sealed class ExecutedQuery
	{
		// Token: 0x06007194 RID: 29076 RVA: 0x001D8252 File Offset: 0x001D6452
		internal ExecutedQuery(DataSource dataSource, DataSet dataSet, OnDemandProcessingContext odpContext, DataProcessingMetrics executionMetrics, string commandText, DateTime queryExecutionTimestamp, DataSourceErrorInspector errorInspector)
		{
			this.m_dataSource = dataSource;
			this.m_dataSet = dataSet;
			this.m_odpContext = odpContext;
			this.m_executionMetrics = executionMetrics;
			this.m_commandText = commandText;
			this.m_queryExecutionTimestamp = queryExecutionTimestamp;
			this.m_errorInspector = errorInspector;
		}

		// Token: 0x17002698 RID: 9880
		// (get) Token: 0x06007195 RID: 29077 RVA: 0x001D828F File Offset: 0x001D648F
		internal DataSet DataSet
		{
			get
			{
				return this.m_dataSet;
			}
		}

		// Token: 0x17002699 RID: 9881
		// (get) Token: 0x06007196 RID: 29078 RVA: 0x001D8297 File Offset: 0x001D6497
		internal DateTime QueryExecutionTimestamp
		{
			get
			{
				return this.m_queryExecutionTimestamp;
			}
		}

		// Token: 0x1700269A RID: 9882
		// (get) Token: 0x06007197 RID: 29079 RVA: 0x001D829F File Offset: 0x001D649F
		internal string CommandText
		{
			get
			{
				return this.m_commandText;
			}
		}

		// Token: 0x1700269B RID: 9883
		// (get) Token: 0x06007198 RID: 29080 RVA: 0x001D82A7 File Offset: 0x001D64A7
		internal DataSourceErrorInspector ErrorInspector
		{
			get
			{
				return this.m_errorInspector;
			}
		}

		// Token: 0x1700269C RID: 9884
		// (get) Token: 0x06007199 RID: 29081 RVA: 0x001D82AF File Offset: 0x001D64AF
		internal DataProcessingMetrics ExecutionMetrics
		{
			get
			{
				return this.m_executionMetrics;
			}
		}

		// Token: 0x0600719A RID: 29082 RVA: 0x001D82B7 File Offset: 0x001D64B7
		internal void AssumeOwnership(ref IDbConnection connection, ref IDbCommand command, ref IDbCommand commandWrappedForCancel, ref IDataReader dataReader)
		{
			ExecutedQuery.AssignAndClear<IDbConnection>(ref this.m_connection, ref connection);
			ExecutedQuery.AssignAndClear<IDbCommand>(ref this.m_command, ref command);
			ExecutedQuery.AssignAndClear<IDbCommand>(ref this.m_commandWrappedForCancel, ref commandWrappedForCancel);
			ExecutedQuery.AssignAndClear<IDataReader>(ref this.m_dataReader, ref dataReader);
		}

		// Token: 0x0600719B RID: 29083 RVA: 0x001D82EA File Offset: 0x001D64EA
		internal void ReleaseOwnership(ref IDbConnection connection)
		{
			ExecutedQuery.AssignAndClear<IDbConnection>(ref connection, ref this.m_connection);
		}

		// Token: 0x0600719C RID: 29084 RVA: 0x001D82F8 File Offset: 0x001D64F8
		internal void ReleaseOwnership(ref IDbCommand command, ref IDbCommand commandWrappedForCancel, ref IDataReader dataReader)
		{
			ExecutedQuery.AssignAndClear<IDataReader>(ref dataReader, ref this.m_dataReader);
			ExecutedQuery.AssignAndClear<IDbCommand>(ref commandWrappedForCancel, ref this.m_commandWrappedForCancel);
			ExecutedQuery.AssignAndClear<IDbCommand>(ref command, ref this.m_command);
		}

		// Token: 0x0600719D RID: 29085 RVA: 0x001D8320 File Offset: 0x001D6520
		private static void AssignAndClear<T>(ref T target, ref T source) where T : class
		{
			Interlocked.Exchange<T>(ref target, source);
			Interlocked.Exchange<T>(ref source, default(T));
		}

		// Token: 0x0600719E RID: 29086 RVA: 0x001D834C File Offset: 0x001D654C
		internal void Close()
		{
			IDataReader dataReader = Interlocked.Exchange<IDataReader>(ref this.m_dataReader, null);
			if (dataReader != null)
			{
				QueryExecutionUtils.DisposeDataExtensionObject<IDataReader>(ref dataReader, "data reader", this.m_dataSet.Name, this.m_executionMetrics, new DataProcessingMetrics.MetricType?(DataProcessingMetrics.MetricType.DisposeDataReader));
			}
			this.m_commandWrappedForCancel = null;
			IDbCommand dbCommand = Interlocked.Exchange<IDbCommand>(ref this.m_command, null);
			if (dbCommand != null)
			{
				QueryExecutionUtils.DisposeDataExtensionObject<IDbCommand>(ref dbCommand, "command", this.m_dataSet.Name);
			}
			IDbConnection dbConnection = Interlocked.Exchange<IDbConnection>(ref this.m_connection, null);
			if (dbConnection != null)
			{
				RuntimeDataSource.CloseConnection(dbConnection, this.m_dataSource, this.m_odpContext, this.m_executionMetrics);
			}
		}

		// Token: 0x04003A74 RID: 14964
		private readonly DataSource m_dataSource;

		// Token: 0x04003A75 RID: 14965
		private readonly DataSet m_dataSet;

		// Token: 0x04003A76 RID: 14966
		private readonly OnDemandProcessingContext m_odpContext;

		// Token: 0x04003A77 RID: 14967
		private readonly DataProcessingMetrics m_executionMetrics;

		// Token: 0x04003A78 RID: 14968
		private readonly string m_commandText;

		// Token: 0x04003A79 RID: 14969
		private readonly DateTime m_queryExecutionTimestamp;

		// Token: 0x04003A7A RID: 14970
		private readonly DataSourceErrorInspector m_errorInspector;

		// Token: 0x04003A7B RID: 14971
		private IDbConnection m_connection;

		// Token: 0x04003A7C RID: 14972
		private IDbCommand m_command;

		// Token: 0x04003A7D RID: 14973
		private IDbCommand m_commandWrappedForCancel;

		// Token: 0x04003A7E RID: 14974
		private IDataReader m_dataReader;
	}
}
