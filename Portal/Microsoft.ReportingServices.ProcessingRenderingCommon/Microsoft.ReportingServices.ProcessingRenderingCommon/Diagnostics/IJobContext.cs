using System;
using System.Threading;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics.Internal;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000074 RID: 116
	public interface IJobContext
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000332 RID: 818
		object SyncRoot { get; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000333 RID: 819
		ExecutionLogLevel ExecutionLogLevel { get; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000334 RID: 820
		// (set) Token: 0x06000335 RID: 821
		TimeSpan TimeDataRetrieval { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000336 RID: 822
		// (set) Token: 0x06000337 RID: 823
		TimeSpan TimeProcessing { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000338 RID: 824
		// (set) Token: 0x06000339 RID: 825
		TimeSpan TimeRendering { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600033A RID: 826
		AdditionalInfo AdditionalInfo { get; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600033B RID: 827
		// (set) Token: 0x0600033C RID: 828
		long RowCount { get; set; }

		// Token: 0x0600033D RID: 829
		void AddAbortHelper(IAbortHelper abortHelper);

		// Token: 0x0600033E RID: 830
		IAbortHelper GetAbortHelper();

		// Token: 0x0600033F RID: 831
		void RemoveAbortHelper();

		// Token: 0x06000340 RID: 832
		void AddCommand(IDbCommand cmd);

		// Token: 0x06000341 RID: 833
		void RemoveCommand(IDbCommand cmd);

		// Token: 0x06000342 RID: 834
		bool ApplyCommandMemoryLimit(IDbCommand cmd);

		// Token: 0x06000343 RID: 835
		bool SetAdditionalCorrelation(IDbCommand cmd);

		// Token: 0x06000344 RID: 836
		void TryQueueWorkItem(WaitCallback callback, object state);

		// Token: 0x06000345 RID: 837
		void QueueWorkItem(WaitCallback callback, object state);

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000346 RID: 838
		string ExecutionId { get; }
	}
}
