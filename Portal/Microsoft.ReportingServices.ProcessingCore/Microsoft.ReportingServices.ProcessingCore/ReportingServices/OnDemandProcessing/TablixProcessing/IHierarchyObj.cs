using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x02000901 RID: 2305
	public interface IHierarchyObj : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x17002939 RID: 10553
		// (get) Token: 0x06007EDC RID: 32476
		IReference<IHierarchyObj> HierarchyRoot { get; }

		// Token: 0x1700293A RID: 10554
		// (get) Token: 0x06007EDD RID: 32477
		OnDemandProcessingContext OdpContext { get; }

		// Token: 0x1700293B RID: 10555
		// (get) Token: 0x06007EDE RID: 32478
		BTree SortTree { get; }

		// Token: 0x1700293C RID: 10556
		// (get) Token: 0x06007EDF RID: 32479
		int ExpressionIndex { get; }

		// Token: 0x1700293D RID: 10557
		// (get) Token: 0x06007EE0 RID: 32480
		int Depth { get; }

		// Token: 0x1700293E RID: 10558
		// (get) Token: 0x06007EE1 RID: 32481
		List<int> SortFilterInfoIndices { get; }

		// Token: 0x1700293F RID: 10559
		// (get) Token: 0x06007EE2 RID: 32482
		bool IsDetail { get; }

		// Token: 0x17002940 RID: 10560
		// (get) Token: 0x06007EE3 RID: 32483
		bool InDataRowSortPhase { get; }

		// Token: 0x06007EE4 RID: 32484
		IHierarchyObj CreateHierarchyObjForSortTree();

		// Token: 0x06007EE5 RID: 32485
		ProcessingMessageList RegisterComparisonError(string propertyName);

		// Token: 0x06007EE6 RID: 32486
		void NextRow(IHierarchyObj owner);

		// Token: 0x06007EE7 RID: 32487
		void Traverse(ProcessingStages operation, ITraversalContext traversalContext);

		// Token: 0x06007EE8 RID: 32488
		void ReadRow();

		// Token: 0x06007EE9 RID: 32489
		void ProcessUserSort();

		// Token: 0x06007EEA RID: 32490
		void MarkSortInfoProcessed(List<IReference<RuntimeSortFilterEventInfo>> runtimeSortFilterInfo);

		// Token: 0x06007EEB RID: 32491
		void AddSortInfoIndex(int sortInfoIndex, IReference<RuntimeSortFilterEventInfo> sortInfo);
	}
}
