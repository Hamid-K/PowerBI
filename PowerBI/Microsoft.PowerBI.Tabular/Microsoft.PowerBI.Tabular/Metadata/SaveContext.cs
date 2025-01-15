using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Hosting;
using Microsoft.AnalysisServices.Tabular.DataRefresh;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x02000207 RID: 519
	internal sealed class SaveContext
	{
		// Token: 0x06001D77 RID: 7543 RVA: 0x000C89FB File Offset: 0x000C6BFB
		public SaveContext(SaveFlags flags, int maxParallelism, TxSavepoint savepoint)
		{
			this.Flags = flags;
			this.MaxParallelism = maxParallelism;
			this.Savepoint = savepoint;
			this.UnblockingDatabase = false;
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001D78 RID: 7544 RVA: 0x000C8A1F File Offset: 0x000C6C1F
		public SaveFlags Flags { get; }

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001D79 RID: 7545 RVA: 0x000C8A27 File Offset: 0x000C6C27
		public int MaxParallelism { get; }

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001D7A RID: 7546 RVA: 0x000C8A2F File Offset: 0x000C6C2F
		// (set) Token: 0x06001D7B RID: 7547 RVA: 0x000C8A37 File Offset: 0x000C6C37
		public TxSavepoint Savepoint { get; private set; }

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x000C8A40 File Offset: 0x000C6C40
		// (set) Token: 0x06001D7D RID: 7549 RVA: 0x000C8A48 File Offset: 0x000C6C48
		public bool MultiPhaseSave { get; internal set; }

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x000C8A51 File Offset: 0x000C6C51
		// (set) Token: 0x06001D7F RID: 7551 RVA: 0x000C8A59 File Offset: 0x000C6C59
		public bool TransactionCreated { get; internal set; }

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001D80 RID: 7552 RVA: 0x000C8A62 File Offset: 0x000C6C62
		// (set) Token: 0x06001D81 RID: 7553 RVA: 0x000C8A6A File Offset: 0x000C6C6A
		public bool CRUDOperationsExecuted { get; internal set; }

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001D82 RID: 7554 RVA: 0x000C8A73 File Offset: 0x000C6C73
		// (set) Token: 0x06001D83 RID: 7555 RVA: 0x000C8A7B File Offset: 0x000C6C7B
		public bool UnblockingDatabase { get; internal set; }

		// Token: 0x06001D84 RID: 7556 RVA: 0x000C8A84 File Offset: 0x000C6C84
		internal PartitionPolicyRangeMap GeneratePartitionPolicyRangesMap(Table table, DateTime effectiveTime, bool refreshPartitions)
		{
			PartitionPolicyRangeMap partitionPolicyRangeMap = new PartitionPolicyRangeMap(table, effectiveTime, refreshPartitions || this.UnblockingDatabase, this.UnblockingDatabase);
			if (this.irTablesRangeMapCollection == null)
			{
				this.irTablesRangeMapCollection = new List<PartitionPolicyRangeMap>();
			}
			this.irTablesRangeMapCollection.Add(partitionPolicyRangeMap);
			return partitionPolicyRangeMap;
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x000C8ACC File Offset: 0x000C6CCC
		internal PartitionPolicyRangeMap GetPartitionPolicyRangesMap(Table table)
		{
			PartitionPolicyRangeMap partitionPolicyRangeMap = this.irTablesRangeMapCollection.Find((PartitionPolicyRangeMap rm) => rm.Table == table);
			if (partitionPolicyRangeMap == null)
			{
				throw TomInternalException.CreateWithRestrictedInfo("Could not find a range-map for {0} in the save-context", new KeyValuePair<InfoRestrictionType, object>[]
				{
					new KeyValuePair<InfoRestrictionType, object>(InfoRestrictionType.CCON, table.Name)
				});
			}
			return partitionPolicyRangeMap;
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x000C8B2B File Offset: 0x000C6D2B
		internal void PrepareForPostCRUDOperations()
		{
			this.Savepoint.TxManager.HandlePartialSave(true);
			this.Savepoint = this.Savepoint.TxManager.CurrentSavepoint;
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x000C8B54 File Offset: 0x000C6D54
		internal void QueueRefreshPolicyRelatedPartitionsMerge(IEnumerable<Partition> sources, Partition target)
		{
			if (!this.Savepoint.AllMergePartitionsRequestedTables.Contains(target.Table))
			{
				target.RequestMergeImpl(sources.ToList<Partition>());
				return;
			}
			if (this.deferredMergeRequests == null)
			{
				this.deferredMergeRequests = new List<SaveContext.PartitionsMergeRequest>();
			}
			this.deferredMergeRequests.Add(new SaveContext.PartitionsMergeRequest(sources, target));
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x000C8BAB File Offset: 0x000C6DAB
		internal bool HasDeferredMergeRequests()
		{
			return this.deferredMergeRequests != null && this.deferredMergeRequests.Count > 0;
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x000C8BC8 File Offset: 0x000C6DC8
		internal void RequestNextBatchOfDeferredMergeRequests()
		{
			if (!this.HasDeferredMergeRequests())
			{
				return;
			}
			this.Savepoint.TxManager.HandlePartialSave(false);
			this.Savepoint = this.Savepoint.TxManager.CurrentSavepoint;
			int i = 0;
			while (i < this.deferredMergeRequests.Count)
			{
				SaveContext.PartitionsMergeRequest partitionsMergeRequest = this.deferredMergeRequests[i];
				if (!this.Savepoint.AllMergePartitionsRequestedTables.Contains(partitionsMergeRequest.Target.Table))
				{
					partitionsMergeRequest.Target.RequestMergeImpl(partitionsMergeRequest.Sources);
					this.deferredMergeRequests.RemoveAt(i);
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x040006C3 RID: 1731
		private List<PartitionPolicyRangeMap> irTablesRangeMapCollection;

		// Token: 0x040006C4 RID: 1732
		private List<SaveContext.PartitionsMergeRequest> deferredMergeRequests;

		// Token: 0x02000438 RID: 1080
		private struct PartitionsMergeRequest
		{
			// Token: 0x060028C6 RID: 10438 RVA: 0x000F014F File Offset: 0x000EE34F
			public PartitionsMergeRequest(IEnumerable<Partition> sources, Partition target)
			{
				this.target = target;
				this.sources = sources.ToList<Partition>();
			}

			// Token: 0x170007E7 RID: 2023
			// (get) Token: 0x060028C7 RID: 10439 RVA: 0x000F0164 File Offset: 0x000EE364
			public Partition Target
			{
				get
				{
					return this.target;
				}
			}

			// Token: 0x170007E8 RID: 2024
			// (get) Token: 0x060028C8 RID: 10440 RVA: 0x000F016C File Offset: 0x000EE36C
			public IEnumerable<Partition> Sources
			{
				get
				{
					return this.sources;
				}
			}

			// Token: 0x040013FB RID: 5115
			private readonly Partition target;

			// Token: 0x040013FC RID: 5116
			private readonly IEnumerable<Partition> sources;
		}
	}
}
