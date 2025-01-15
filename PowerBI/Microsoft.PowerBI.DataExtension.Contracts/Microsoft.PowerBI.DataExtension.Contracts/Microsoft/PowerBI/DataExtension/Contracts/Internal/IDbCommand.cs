using System;
using System.Data;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.DataExtension.Contracts.Internal
{
	// Token: 0x0200001A RID: 26
	public interface IDbCommand : IDisposable
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000067 RID: 103
		bool IsOpen { get; }

		// Token: 0x06000068 RID: 104
		Task<IDataReader> ExecuteReaderAsync();

		// Token: 0x06000069 RID: 105
		IDataReader ExecuteReader();

		// Token: 0x0600006A RID: 106
		void Close();

		// Token: 0x0600006B RID: 107
		Task CancelAsync();

		// Token: 0x0600006C RID: 108
		void SetMemoryLimit(int memoryLimitKB);

		// Token: 0x0600006D RID: 109
		void SetTimeout(int timeout);

		// Token: 0x0600006E RID: 110
		void SetTelemetryIds(string clientActivityId, string currentActivityId, string rootActivityId);

		// Token: 0x0600006F RID: 111
		void SetRequestPriority(RequestPriorityKind reqPriority);

		// Token: 0x06000070 RID: 112
		void SetRequestExecutionMetrics(RequestExecutionMetricsKind metricsKind, int? maxEventCount);

		// Token: 0x06000071 RID: 113
		void ReadExecutionMetrics(IExecutionMetricsVisitor visitor);

		// Token: 0x06000072 RID: 114
		void SetApplicationContext(string applicationContext);

		// Token: 0x06000073 RID: 115
		void ExecuteNonQuery(CommandType commandType, string commandText, string operationName);

		// Token: 0x06000074 RID: 116
		void AddParameter(string parameterName, object parameterValue);
	}
}
