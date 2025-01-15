using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008D7 RID: 2263
	public class AggregateUpdateContext : ITraversalContext
	{
		// Token: 0x06007BB2 RID: 31666 RVA: 0x001FC6B6 File Offset: 0x001FA8B6
		public AggregateUpdateContext(OnDemandProcessingContext odpContext, AggregateMode mode)
		{
			this.m_mode = mode;
			this.m_odpContext = odpContext;
			this.m_activeAggregates = null;
		}

		// Token: 0x1700288E RID: 10382
		// (get) Token: 0x06007BB3 RID: 31667 RVA: 0x001FC6D3 File Offset: 0x001FA8D3
		public AggregateMode Mode
		{
			get
			{
				return this.m_mode;
			}
		}

		// Token: 0x06007BB4 RID: 31668 RVA: 0x001FC6DB File Offset: 0x001FA8DB
		public AggregateUpdateQueue ReplaceAggregatesToUpdate(BucketedDataAggregateObjs aggBuckets)
		{
			return this.HandleNewBuckets(aggBuckets, false);
		}

		// Token: 0x06007BB5 RID: 31669 RVA: 0x001FC6E5 File Offset: 0x001FA8E5
		public AggregateUpdateQueue RegisterAggregatesToUpdate(BucketedDataAggregateObjs aggBuckets)
		{
			return this.HandleNewBuckets(aggBuckets, true);
		}

		// Token: 0x06007BB6 RID: 31670 RVA: 0x001FC6F0 File Offset: 0x001FA8F0
		public AggregateUpdateQueue RegisterRunningValuesToUpdate(AggregateUpdateQueue workQueue, List<Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo> runningValues)
		{
			if (runningValues == null || runningValues.Count == 0)
			{
				return workQueue;
			}
			if (workQueue == null)
			{
				workQueue = new AggregateUpdateQueue(this.m_activeAggregates);
				this.m_activeAggregates = new AggregateUpdateCollection(runningValues)
				{
					LinkedCollection = this.m_activeAggregates
				};
			}
			else
			{
				this.m_activeAggregates.MergeRunningValues(runningValues);
			}
			return workQueue;
		}

		// Token: 0x06007BB7 RID: 31671 RVA: 0x001FC744 File Offset: 0x001FA944
		private AggregateUpdateQueue HandleNewBuckets(BucketedDataAggregateObjs aggBuckets, bool canMergeActiveAggs)
		{
			bool flag = aggBuckets == null || aggBuckets.Buckets.Count == 0;
			if (canMergeActiveAggs && flag)
			{
				return null;
			}
			AggregateUpdateQueue aggregateUpdateQueue = new AggregateUpdateQueue(this.m_activeAggregates);
			AggregateUpdateCollection aggregateUpdateCollection = null;
			if (canMergeActiveAggs)
			{
				aggregateUpdateCollection = this.m_activeAggregates;
			}
			this.m_activeAggregates = null;
			if (flag)
			{
				return aggregateUpdateQueue;
			}
			for (int i = 0; i < aggBuckets.Buckets.Count; i++)
			{
				AggregateBucket<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> aggregateBucket = aggBuckets.Buckets[i];
				AggregateUpdateCollection aggregateUpdateCollection2 = new AggregateUpdateCollection(aggregateBucket);
				if (aggregateUpdateCollection != null)
				{
					if (aggregateUpdateCollection.Level == aggregateBucket.Level)
					{
						aggregateUpdateCollection2.LinkedCollection = aggregateUpdateCollection;
						aggregateUpdateCollection = null;
					}
					else if (aggregateUpdateCollection.Level < aggregateBucket.Level)
					{
						aggregateUpdateCollection2 = aggregateUpdateCollection;
						i--;
						aggregateUpdateCollection = null;
					}
				}
				if (this.m_activeAggregates == null)
				{
					this.m_activeAggregates = aggregateUpdateCollection2;
				}
				else
				{
					aggregateUpdateQueue.Enqueue(aggregateUpdateCollection2);
				}
			}
			if (aggregateUpdateCollection != null)
			{
				aggregateUpdateQueue.Enqueue(aggregateUpdateCollection);
			}
			return aggregateUpdateQueue;
		}

		// Token: 0x06007BB8 RID: 31672 RVA: 0x001FC819 File Offset: 0x001FAA19
		public bool AdvanceQueue(AggregateUpdateQueue queue)
		{
			if (queue == null)
			{
				return false;
			}
			if (queue.Count == 0)
			{
				this.RestoreOriginalState(queue);
				return false;
			}
			this.m_activeAggregates = queue.Dequeue();
			return true;
		}

		// Token: 0x06007BB9 RID: 31673 RVA: 0x001FC83E File Offset: 0x001FAA3E
		public void RestoreOriginalState(AggregateUpdateQueue queue)
		{
			if (queue == null)
			{
				return;
			}
			this.m_activeAggregates = queue.OriginalState;
		}

		// Token: 0x06007BBA RID: 31674 RVA: 0x001FC850 File Offset: 0x001FAA50
		public bool UpdateAggregates(DataScopeInfo scopeInfo, IDataRowHolder scopeInst, AggregateUpdateFlags updateFlags, bool needsSetupEnvironment)
		{
			this.m_aggsForUpdateAtRowScope = null;
			this.m_runningValuesForUpdateAtRow = null;
			if (this.m_activeAggregates == null)
			{
				return false;
			}
			for (AggregateUpdateCollection aggregateUpdateCollection = this.m_activeAggregates; aggregateUpdateCollection != null; aggregateUpdateCollection = aggregateUpdateCollection.LinkedCollection)
			{
				List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> list;
				if (aggregateUpdateCollection.GetAggregatesForScope(scopeInfo.ScopeID, out list))
				{
					if (needsSetupEnvironment)
					{
						scopeInst.SetupEnvironment();
						needsSetupEnvironment = false;
					}
					foreach (Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj dataAggregateObj in list)
					{
						dataAggregateObj.Update();
					}
				}
				if (aggregateUpdateCollection.GetAggregatesForRowScope(scopeInfo.ScopeID, out list))
				{
					if (this.m_aggsForUpdateAtRowScope == null)
					{
						this.m_aggsForUpdateAtRowScope = new List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>();
					}
					this.m_aggsForUpdateAtRowScope.AddRange(list);
				}
				List<string> list2;
				if (aggregateUpdateCollection.GetRunningValuesForScope(scopeInfo.ScopeID, out list2))
				{
					if (needsSetupEnvironment)
					{
						scopeInst.SetupEnvironment();
						needsSetupEnvironment = false;
					}
					RuntimeDataTablixObj.UpdateRunningValues(this.m_odpContext, list2);
				}
				if (aggregateUpdateCollection.GetRunningValuesForRowScope(scopeInfo.ScopeID, out list2))
				{
					if (this.m_runningValuesForUpdateAtRow == null)
					{
						this.m_runningValuesForUpdateAtRow = new List<string>();
					}
					this.m_runningValuesForUpdateAtRow.AddRange(list2);
				}
			}
			if (this.m_aggsForUpdateAtRowScope != null || this.m_runningValuesForUpdateAtRow != null)
			{
				if (needsSetupEnvironment)
				{
					scopeInst.SetupEnvironment();
				}
				if (FlagUtils.HasFlag(updateFlags, AggregateUpdateFlags.RowAggregates))
				{
					scopeInst.ReadRows(DataActions.AggregatesOfAggregates, this);
				}
			}
			return scopeInfo.ScopeID != this.m_activeAggregates.InnermostUpdateScopeID;
		}

		// Token: 0x06007BBB RID: 31675 RVA: 0x001FC9B4 File Offset: 0x001FABB4
		public void UpdateAggregatesForRow()
		{
			Global.Tracer.Assert(this.m_aggsForUpdateAtRowScope != null || this.m_runningValuesForUpdateAtRow != null, "UpdateAggregatesForRow must be driven by a call to UpdateAggregates.");
			if (this.m_aggsForUpdateAtRowScope != null)
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj dataAggregateObj in this.m_aggsForUpdateAtRowScope)
				{
					dataAggregateObj.Update();
				}
			}
			if (this.m_runningValuesForUpdateAtRow != null)
			{
				RuntimeDataTablixObj.UpdateRunningValues(this.m_odpContext, this.m_runningValuesForUpdateAtRow);
			}
		}

		// Token: 0x06007BBC RID: 31676 RVA: 0x001FCA48 File Offset: 0x001FAC48
		public bool LastScopeNeedsRowAggregateProcessing()
		{
			return this.m_aggsForUpdateAtRowScope != null || this.m_runningValuesForUpdateAtRow != null;
		}

		// Token: 0x04003D83 RID: 15747
		private AggregateMode m_mode;

		// Token: 0x04003D84 RID: 15748
		private OnDemandProcessingContext m_odpContext;

		// Token: 0x04003D85 RID: 15749
		private AggregateUpdateCollection m_activeAggregates;

		// Token: 0x04003D86 RID: 15750
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> m_aggsForUpdateAtRowScope;

		// Token: 0x04003D87 RID: 15751
		private List<string> m_runningValuesForUpdateAtRow;
	}
}
