using System;
using System.Data;
using System.Threading;
using Microsoft.DataIntegration.FuzzyMatching;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.FuzzyMatching
{
	// Token: 0x02000B40 RID: 2880
	internal sealed class NonStreamingFuzzyJoinRequest : FuzzyJoinRequest, IDisposable
	{
		// Token: 0x06004FF6 RID: 20470 RVA: 0x0010BCDD File Offset: 0x00109EDD
		private NonStreamingFuzzyJoinRequest(DataTable inputDataTable, DataTable referenceDataTable, DataTable transformationDataTable, FuzzyLookupEntry.FuzzyLookupParameters fuzzyJoinParameters, FuzzyLookupEntry.JoinType fuzzyJoinType)
			: base(referenceDataTable, transformationDataTable, fuzzyJoinParameters, fuzzyJoinType)
		{
			this.inputDataTable = inputDataTable;
			this.completeEvent = new ManualResetEvent(false);
		}

		// Token: 0x06004FF7 RID: 20471 RVA: 0x0010BD00 File Offset: 0x00109F00
		public static NonStreamingFuzzyJoinRequest New(IThreadPoolService threadPool, DataTable inputDataTable, DataTable referenceDataTable, DataTable transformationDataTable, FuzzyLookupEntry.FuzzyLookupParameters fuzzyJoinParameters, FuzzyLookupEntry.JoinType fuzzyJoinType)
		{
			NonStreamingFuzzyJoinRequest nonStreamingFuzzyJoinRequest = new NonStreamingFuzzyJoinRequest(inputDataTable, referenceDataTable, transformationDataTable, fuzzyJoinParameters, fuzzyJoinType);
			threadPool.QueueUserWorkItem(new WaitCallback(NonStreamingFuzzyJoinRequest.ExecuteInternal), nonStreamingFuzzyJoinRequest);
			return nonStreamingFuzzyJoinRequest;
		}

		// Token: 0x06004FF8 RID: 20472 RVA: 0x0010BD2E File Offset: 0x00109F2E
		void IDisposable.Dispose()
		{
			this.completeEvent.Close();
		}

		// Token: 0x170018EC RID: 6380
		// (get) Token: 0x06004FF9 RID: 20473 RVA: 0x0010BD3B File Offset: 0x00109F3B
		public EventWaitHandle Complete
		{
			get
			{
				return this.completeEvent;
			}
		}

		// Token: 0x170018ED RID: 6381
		// (get) Token: 0x06004FFA RID: 20474 RVA: 0x0010BD43 File Offset: 0x00109F43
		public DataTable OutputTable
		{
			get
			{
				return this.outputTable;
			}
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x0010BD4C File Offset: 0x00109F4C
		private void Execute()
		{
			this.outputTable = ExternalFuzzyMatcher.ExecuteJoin(this.inputDataTable, this.referenceDataTable, this.transformationDataTable, this.fuzzyJoinParameters, this.fuzzyJoinType, default(Guid), false);
			this.Complete.Set();
		}

		// Token: 0x06004FFC RID: 20476 RVA: 0x0010BD98 File Offset: 0x00109F98
		private static void ExecuteInternal(object request)
		{
			((NonStreamingFuzzyJoinRequest)request).Execute();
		}

		// Token: 0x04002AEA RID: 10986
		private readonly ManualResetEvent completeEvent;

		// Token: 0x04002AEB RID: 10987
		private readonly DataTable inputDataTable;

		// Token: 0x04002AEC RID: 10988
		private DataTable outputTable;
	}
}
