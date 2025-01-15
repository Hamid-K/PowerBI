using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008D8 RID: 2264
	public class AggregateUpdateCollection
	{
		// Token: 0x06007BBD RID: 31677 RVA: 0x001FCA60 File Offset: 0x001FAC60
		public AggregateUpdateCollection(AggregateBucket<DataAggregateObj> bucket)
		{
			this.m_level = bucket.Level;
			this.m_innermostUpdateScopeID = -1;
			this.m_innermostUpdateScopeDepth = -1;
			foreach (DataAggregateObj dataAggregateObj in bucket.Aggregates)
			{
				DataAggregateInfo aggregateDef = dataAggregateObj.AggregateDef;
				if (aggregateDef.UpdatesAtRowScope)
				{
					this.Add<AggregateUpdateCollection.AggregatesByScopeId, DataAggregateObj>(ref this.m_rowAggsByUpdateScope, aggregateDef, dataAggregateObj);
				}
				else
				{
					this.Add<AggregateUpdateCollection.AggregatesByScopeId, DataAggregateObj>(ref this.m_aggsByUpdateScope, aggregateDef, dataAggregateObj);
				}
			}
		}

		// Token: 0x06007BBE RID: 31678 RVA: 0x001FCAFC File Offset: 0x001FACFC
		public AggregateUpdateCollection(List<RunningValueInfo> runningValues)
		{
			this.m_level = 0;
			this.MergeRunningValues(runningValues);
		}

		// Token: 0x06007BBF RID: 31679 RVA: 0x001FCB14 File Offset: 0x001FAD14
		public void MergeRunningValues(List<RunningValueInfo> runningValues)
		{
			foreach (RunningValueInfo runningValueInfo in runningValues)
			{
				if (runningValueInfo.UpdatesAtRowScope)
				{
					this.Add<AggregateUpdateCollection.RunningValuesByScopeId, string>(ref this.m_rowRunningValuesByUpdateScope, runningValueInfo, runningValueInfo.Name);
				}
				else
				{
					this.Add<AggregateUpdateCollection.RunningValuesByScopeId, string>(ref this.m_runningValuesByUpdateScope, runningValueInfo, runningValueInfo.Name);
				}
			}
		}

		// Token: 0x06007BC0 RID: 31680 RVA: 0x001FCB8C File Offset: 0x001FAD8C
		private void Add<T, U>(ref T colByScope, DataAggregateInfo agg, U item) where T : Dictionary<int, List<U>>, new()
		{
			if (colByScope == null)
			{
				colByScope = new T();
			}
			int updateScopeID = agg.UpdateScopeID;
			List<U> list;
			if (!colByScope.TryGetValue(updateScopeID, out list))
			{
				list = new List<U>();
				colByScope.Add(updateScopeID, list);
			}
			list.Add(item);
			if (agg.UpdateScopeDepth > this.m_innermostUpdateScopeDepth)
			{
				this.m_innermostUpdateScopeID = agg.UpdateScopeID;
				this.m_innermostUpdateScopeDepth = agg.UpdateScopeDepth;
			}
		}

		// Token: 0x1700288F RID: 10383
		// (get) Token: 0x06007BC1 RID: 31681 RVA: 0x001FCC0A File Offset: 0x001FAE0A
		// (set) Token: 0x06007BC2 RID: 31682 RVA: 0x001FCC12 File Offset: 0x001FAE12
		public int Level
		{
			get
			{
				return this.m_level;
			}
			set
			{
				this.m_level = value;
			}
		}

		// Token: 0x17002890 RID: 10384
		// (get) Token: 0x06007BC3 RID: 31683 RVA: 0x001FCC1B File Offset: 0x001FAE1B
		public int InnermostUpdateScopeID
		{
			get
			{
				return this.m_innermostUpdateScopeID;
			}
		}

		// Token: 0x17002891 RID: 10385
		// (get) Token: 0x06007BC4 RID: 31684 RVA: 0x001FCC23 File Offset: 0x001FAE23
		public int InnermostUpdateScopeDepth
		{
			get
			{
				return this.m_innermostUpdateScopeDepth;
			}
		}

		// Token: 0x17002892 RID: 10386
		// (get) Token: 0x06007BC5 RID: 31685 RVA: 0x001FCC2B File Offset: 0x001FAE2B
		// (set) Token: 0x06007BC6 RID: 31686 RVA: 0x001FCC34 File Offset: 0x001FAE34
		public AggregateUpdateCollection LinkedCollection
		{
			get
			{
				return this.m_linkedCol;
			}
			set
			{
				this.m_linkedCol = value;
				if (this.m_linkedCol != null && this.m_linkedCol.m_innermostUpdateScopeDepth > this.m_innermostUpdateScopeDepth)
				{
					this.m_innermostUpdateScopeDepth = this.m_linkedCol.m_innermostUpdateScopeDepth;
					this.m_innermostUpdateScopeID = this.m_linkedCol.m_innermostUpdateScopeID;
				}
			}
		}

		// Token: 0x06007BC7 RID: 31687 RVA: 0x001FCC85 File Offset: 0x001FAE85
		public bool GetAggregatesForScope(int scopeId, out List<DataAggregateObj> aggs)
		{
			return this.NullCheckTryGetValue<DataAggregateObj>(this.m_aggsByUpdateScope, scopeId, out aggs);
		}

		// Token: 0x06007BC8 RID: 31688 RVA: 0x001FCC95 File Offset: 0x001FAE95
		public bool GetAggregatesForRowScope(int scopeId, out List<DataAggregateObj> aggs)
		{
			return this.NullCheckTryGetValue<DataAggregateObj>(this.m_rowAggsByUpdateScope, scopeId, out aggs);
		}

		// Token: 0x06007BC9 RID: 31689 RVA: 0x001FCCA5 File Offset: 0x001FAEA5
		public bool GetRunningValuesForScope(int scopeId, out List<string> aggs)
		{
			return this.NullCheckTryGetValue<string>(this.m_runningValuesByUpdateScope, scopeId, out aggs);
		}

		// Token: 0x06007BCA RID: 31690 RVA: 0x001FCCB5 File Offset: 0x001FAEB5
		public bool GetRunningValuesForRowScope(int scopeId, out List<string> aggs)
		{
			return this.NullCheckTryGetValue<string>(this.m_rowRunningValuesByUpdateScope, scopeId, out aggs);
		}

		// Token: 0x06007BCB RID: 31691 RVA: 0x001FCCC5 File Offset: 0x001FAEC5
		private bool NullCheckTryGetValue<T>(Dictionary<int, List<T>> aggsById, int scopeId, out List<T> aggs)
		{
			if (aggsById == null)
			{
				aggs = null;
				return false;
			}
			return aggsById.TryGetValue(scopeId, out aggs);
		}

		// Token: 0x04003D88 RID: 15752
		private int m_level;

		// Token: 0x04003D89 RID: 15753
		private int m_innermostUpdateScopeID;

		// Token: 0x04003D8A RID: 15754
		private int m_innermostUpdateScopeDepth;

		// Token: 0x04003D8B RID: 15755
		private AggregateUpdateCollection m_linkedCol;

		// Token: 0x04003D8C RID: 15756
		private AggregateUpdateCollection.AggregatesByScopeId m_aggsByUpdateScope;

		// Token: 0x04003D8D RID: 15757
		private AggregateUpdateCollection.AggregatesByScopeId m_rowAggsByUpdateScope;

		// Token: 0x04003D8E RID: 15758
		private AggregateUpdateCollection.RunningValuesByScopeId m_runningValuesByUpdateScope;

		// Token: 0x04003D8F RID: 15759
		private AggregateUpdateCollection.RunningValuesByScopeId m_rowRunningValuesByUpdateScope;

		// Token: 0x02000D1E RID: 3358
		private class AggregatesByScopeId : Dictionary<int, List<DataAggregateObj>>
		{
			// Token: 0x06008F09 RID: 36617 RVA: 0x0024658D File Offset: 0x0024478D
			public AggregatesByScopeId()
				: base(5)
			{
			}
		}

		// Token: 0x02000D1F RID: 3359
		private class RunningValuesByScopeId : Dictionary<int, List<string>>
		{
			// Token: 0x06008F0A RID: 36618 RVA: 0x00246596 File Offset: 0x00244796
			public RunningValuesByScopeId()
				: base(5)
			{
			}
		}
	}
}
