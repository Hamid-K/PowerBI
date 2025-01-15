using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200077A RID: 1914
	internal sealed class RuntimeUserSortTargetInfo
	{
		// Token: 0x06006AB0 RID: 27312 RVA: 0x001AEA9D File Offset: 0x001ACC9D
		internal RuntimeUserSortTargetInfo(ReportProcessing.IHierarchyObj owner, int sortInfoIndex, RuntimeSortFilterEventInfo sortInfo)
		{
			this.AddSortInfo(owner, sortInfoIndex, sortInfo);
		}

		// Token: 0x17002549 RID: 9545
		// (get) Token: 0x06006AB1 RID: 27313 RVA: 0x001AEAAE File Offset: 0x001ACCAE
		// (set) Token: 0x06006AB2 RID: 27314 RVA: 0x001AEAB6 File Offset: 0x001ACCB6
		internal ReportProcessing.BTreeNode SortTree
		{
			get
			{
				return this.m_sortTree;
			}
			set
			{
				this.m_sortTree = value;
			}
		}

		// Token: 0x1700254A RID: 9546
		// (get) Token: 0x06006AB3 RID: 27315 RVA: 0x001AEABF File Offset: 0x001ACCBF
		// (set) Token: 0x06006AB4 RID: 27316 RVA: 0x001AEAC7 File Offset: 0x001ACCC7
		internal ReportProcessing.AggregateRowList AggregateRows
		{
			get
			{
				return this.m_aggregateRows;
			}
			set
			{
				this.m_aggregateRows = value;
			}
		}

		// Token: 0x1700254B RID: 9547
		// (get) Token: 0x06006AB5 RID: 27317 RVA: 0x001AEAD0 File Offset: 0x001ACCD0
		// (set) Token: 0x06006AB6 RID: 27318 RVA: 0x001AEAD8 File Offset: 0x001ACCD8
		internal IntList SortFilterInfoIndices
		{
			get
			{
				return this.m_sortFilterInfoIndices;
			}
			set
			{
				this.m_sortFilterInfoIndices = value;
			}
		}

		// Token: 0x1700254C RID: 9548
		// (get) Token: 0x06006AB7 RID: 27319 RVA: 0x001AEAE1 File Offset: 0x001ACCE1
		internal bool TargetForNonDetailSort
		{
			get
			{
				return this.m_targetForNonDetailSort != null;
			}
		}

		// Token: 0x06006AB8 RID: 27320 RVA: 0x001AEAEC File Offset: 0x001ACCEC
		internal void AddSortInfo(ReportProcessing.IHierarchyObj owner, int sortInfoIndex, RuntimeSortFilterEventInfo sortInfo)
		{
			if (sortInfo.EventSource.UserSort.SortExpressionScope != null || owner.IsDetail)
			{
				if (sortInfo.EventSource.UserSort.SortExpressionScope == null)
				{
					this.AddSortInfoIndex(sortInfoIndex, sortInfo);
				}
				if (this.m_sortTree == null)
				{
					this.m_sortTree = new ReportProcessing.BTreeNode(owner);
				}
			}
			if (sortInfo.EventSource.UserSort.SortExpressionScope != null)
			{
				if (this.m_targetForNonDetailSort == null)
				{
					this.m_targetForNonDetailSort = new Hashtable();
				}
				this.m_targetForNonDetailSort.Add(sortInfoIndex, null);
				return;
			}
			if (this.m_targetForDetailSort == null)
			{
				this.m_targetForDetailSort = new Hashtable();
			}
			this.m_targetForDetailSort.Add(sortInfoIndex, null);
		}

		// Token: 0x06006AB9 RID: 27321 RVA: 0x001AEBA0 File Offset: 0x001ACDA0
		internal void AddSortInfoIndex(int sortInfoIndex, RuntimeSortFilterEventInfo sortInfo)
		{
			Global.Tracer.Assert(sortInfo.EventSource.UserSort.SortExpressionScope == null || !sortInfo.TargetSortFilterInfoAdded);
			if (this.m_sortFilterInfoIndices == null)
			{
				this.m_sortFilterInfoIndices = new IntList();
			}
			this.m_sortFilterInfoIndices.Add(sortInfoIndex);
			sortInfo.TargetSortFilterInfoAdded = true;
		}

		// Token: 0x06006ABA RID: 27322 RVA: 0x001AEC01 File Offset: 0x001ACE01
		internal void ResetTargetForNonDetailSort()
		{
			this.m_targetForNonDetailSort = null;
		}

		// Token: 0x06006ABB RID: 27323 RVA: 0x001AEC0C File Offset: 0x001ACE0C
		internal bool IsTargetForSort(int index, bool detailSort)
		{
			Hashtable hashtable = this.m_targetForNonDetailSort;
			if (detailSort)
			{
				hashtable = this.m_targetForDetailSort;
			}
			return hashtable != null && hashtable.Contains(index);
		}

		// Token: 0x06006ABC RID: 27324 RVA: 0x001AEC40 File Offset: 0x001ACE40
		internal void MarkSortInfoProcessed(RuntimeSortFilterEventInfoList runtimeSortFilterInfo, ReportProcessing.IHierarchyObj sortTarget)
		{
			if (this.m_targetForNonDetailSort != null)
			{
				foreach (object obj in this.m_targetForNonDetailSort.Keys)
				{
					int num = (int)obj;
					if (runtimeSortFilterInfo[num].EventTarget == sortTarget)
					{
						Global.Tracer.Assert(!runtimeSortFilterInfo[num].Processed, "(!runtimeSortFilterInfo[index].Processed)");
						runtimeSortFilterInfo[num].Processed = true;
					}
				}
			}
			if (this.m_targetForDetailSort != null)
			{
				foreach (object obj2 in this.m_targetForDetailSort.Keys)
				{
					int num2 = (int)obj2;
					if (runtimeSortFilterInfo[num2].EventTarget == sortTarget)
					{
						Global.Tracer.Assert(!runtimeSortFilterInfo[num2].Processed, "(!runtimeSortFilterInfo[index].Processed)");
						runtimeSortFilterInfo[num2].Processed = true;
					}
				}
			}
		}

		// Token: 0x06006ABD RID: 27325 RVA: 0x001AED64 File Offset: 0x001ACF64
		internal void EnterProcessUserSortPhase(ReportProcessing.ProcessingContext pc)
		{
			if (this.m_sortFilterInfoIndices != null)
			{
				for (int i = 0; i < this.m_sortFilterInfoIndices.Count; i++)
				{
					pc.UserSortFilterContext.EnterProcessUserSortPhase(i);
				}
			}
		}

		// Token: 0x06006ABE RID: 27326 RVA: 0x001AED9C File Offset: 0x001ACF9C
		internal void LeaveProcessUserSortPhase(ReportProcessing.ProcessingContext pc)
		{
			if (this.m_sortFilterInfoIndices != null)
			{
				for (int i = 0; i < this.m_sortFilterInfoIndices.Count; i++)
				{
					pc.UserSortFilterContext.LeaveProcessUserSortPhase(i);
				}
			}
		}

		// Token: 0x040035D8 RID: 13784
		private ReportProcessing.BTreeNode m_sortTree;

		// Token: 0x040035D9 RID: 13785
		private ReportProcessing.AggregateRowList m_aggregateRows;

		// Token: 0x040035DA RID: 13786
		private IntList m_sortFilterInfoIndices;

		// Token: 0x040035DB RID: 13787
		private Hashtable m_targetForNonDetailSort;

		// Token: 0x040035DC RID: 13788
		private Hashtable m_targetForDetailSort;
	}
}
