using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000761 RID: 1889
	[Serializable]
	internal abstract class Tablix : DataRegion, IAggregateHolder, IRunningValueHolder
	{
		// Token: 0x06006863 RID: 26723 RVA: 0x00196102 File Offset: 0x00194302
		internal Tablix(ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x06006864 RID: 26724 RVA: 0x00196119 File Offset: 0x00194319
		internal Tablix(int id, ReportItem parent)
			: base(id, parent)
		{
			this.m_runningValues = new RunningValueInfoList();
			this.m_cellAggregates = new DataAggregateInfoList();
			this.m_cellPostSortAggregates = new DataAggregateInfoList();
		}

		// Token: 0x170024DF RID: 9439
		// (get) Token: 0x06006865 RID: 26725 RVA: 0x00196152 File Offset: 0x00194352
		// (set) Token: 0x06006866 RID: 26726 RVA: 0x0019615A File Offset: 0x0019435A
		internal int ColumnCount
		{
			get
			{
				return this.m_columnCount;
			}
			set
			{
				this.m_columnCount = value;
			}
		}

		// Token: 0x170024E0 RID: 9440
		// (get) Token: 0x06006867 RID: 26727 RVA: 0x00196163 File Offset: 0x00194363
		// (set) Token: 0x06006868 RID: 26728 RVA: 0x0019616B File Offset: 0x0019436B
		internal int RowCount
		{
			get
			{
				return this.m_rowCount;
			}
			set
			{
				this.m_rowCount = value;
			}
		}

		// Token: 0x170024E1 RID: 9441
		// (get) Token: 0x06006869 RID: 26729 RVA: 0x00196174 File Offset: 0x00194374
		// (set) Token: 0x0600686A RID: 26730 RVA: 0x0019617C File Offset: 0x0019437C
		internal DataAggregateInfoList CellAggregates
		{
			get
			{
				return this.m_cellAggregates;
			}
			set
			{
				this.m_cellAggregates = value;
			}
		}

		// Token: 0x170024E2 RID: 9442
		// (get) Token: 0x0600686B RID: 26731 RVA: 0x00196185 File Offset: 0x00194385
		// (set) Token: 0x0600686C RID: 26732 RVA: 0x0019618D File Offset: 0x0019438D
		internal DataAggregateInfoList CellPostSortAggregates
		{
			get
			{
				return this.m_cellPostSortAggregates;
			}
			set
			{
				this.m_cellPostSortAggregates = value;
			}
		}

		// Token: 0x170024E3 RID: 9443
		// (get) Token: 0x0600686D RID: 26733 RVA: 0x00196196 File Offset: 0x00194396
		// (set) Token: 0x0600686E RID: 26734 RVA: 0x0019619E File Offset: 0x0019439E
		internal Pivot.ProcessingInnerGroupings ProcessingInnerGrouping
		{
			get
			{
				return this.m_processingInnerGrouping;
			}
			set
			{
				this.m_processingInnerGrouping = value;
			}
		}

		// Token: 0x170024E4 RID: 9444
		// (get) Token: 0x0600686F RID: 26735 RVA: 0x001961A7 File Offset: 0x001943A7
		// (set) Token: 0x06006870 RID: 26736 RVA: 0x001961AF File Offset: 0x001943AF
		internal RunningValueInfoList RunningValues
		{
			get
			{
				return this.m_runningValues;
			}
			set
			{
				this.m_runningValues = value;
			}
		}

		// Token: 0x170024E5 RID: 9445
		// (get) Token: 0x06006871 RID: 26737
		internal abstract TablixHeadingList TablixColumns { get; }

		// Token: 0x170024E6 RID: 9446
		// (get) Token: 0x06006872 RID: 26738
		internal abstract TablixHeadingList TablixRows { get; }

		// Token: 0x170024E7 RID: 9447
		// (get) Token: 0x06006873 RID: 26739
		internal abstract RunningValueInfoList TablixCellRunningValues { get; }

		// Token: 0x170024E8 RID: 9448
		// (get) Token: 0x06006874 RID: 26740 RVA: 0x001961B8 File Offset: 0x001943B8
		// (set) Token: 0x06006875 RID: 26741 RVA: 0x001961C0 File Offset: 0x001943C0
		internal ReportProcessing.RuntimeTablixGroupRootObj CurrentOuterHeadingGroupRoot
		{
			get
			{
				return this.m_currentOuterHeadingGroupRoot;
			}
			set
			{
				this.m_currentOuterHeadingGroupRoot = value;
			}
		}

		// Token: 0x170024E9 RID: 9449
		// (get) Token: 0x06006876 RID: 26742 RVA: 0x001961C9 File Offset: 0x001943C9
		// (set) Token: 0x06006877 RID: 26743 RVA: 0x001961D1 File Offset: 0x001943D1
		internal int InnermostRowFilterLevel
		{
			get
			{
				return this.m_innermostRowFilterLevel;
			}
			set
			{
				this.m_innermostRowFilterLevel = value;
			}
		}

		// Token: 0x170024EA RID: 9450
		// (get) Token: 0x06006878 RID: 26744 RVA: 0x001961DA File Offset: 0x001943DA
		// (set) Token: 0x06006879 RID: 26745 RVA: 0x001961E2 File Offset: 0x001943E2
		internal int InnermostColumnFilterLevel
		{
			get
			{
				return this.m_innermostColumnFilterLevel;
			}
			set
			{
				this.m_innermostColumnFilterLevel = value;
			}
		}

		// Token: 0x170024EB RID: 9451
		// (get) Token: 0x0600687A RID: 26746 RVA: 0x001961EB File Offset: 0x001943EB
		internal int[] OuterGroupingIndexes
		{
			get
			{
				return this.m_outerGroupingIndexes;
			}
		}

		// Token: 0x170024EC RID: 9452
		// (get) Token: 0x0600687B RID: 26747 RVA: 0x001961F3 File Offset: 0x001943F3
		// (set) Token: 0x0600687C RID: 26748 RVA: 0x001961FB File Offset: 0x001943FB
		internal bool ProcessCellRunningValues
		{
			get
			{
				return this.m_processCellRunningValues;
			}
			set
			{
				this.m_processCellRunningValues = value;
			}
		}

		// Token: 0x170024ED RID: 9453
		// (get) Token: 0x0600687D RID: 26749 RVA: 0x00196204 File Offset: 0x00194404
		// (set) Token: 0x0600687E RID: 26750 RVA: 0x0019620C File Offset: 0x0019440C
		internal bool ProcessOutermostSTCellRunningValues
		{
			get
			{
				return this.m_processOutermostSTCellRunningValues;
			}
			set
			{
				this.m_processOutermostSTCellRunningValues = value;
			}
		}

		// Token: 0x0600687F RID: 26751 RVA: 0x00196218 File Offset: 0x00194418
		internal static void CopyAggregates(DataAggregateInfoList srcAggregates, DataAggregateInfoList targetAggregates)
		{
			for (int i = 0; i < srcAggregates.Count; i++)
			{
				DataAggregateInfo dataAggregateInfo = srcAggregates[i];
				targetAggregates.Add(dataAggregateInfo);
				dataAggregateInfo.IsCopied = true;
			}
		}

		// Token: 0x06006880 RID: 26752 RVA: 0x0019624D File Offset: 0x0019444D
		RunningValueInfoList IRunningValueHolder.GetRunningValueList()
		{
			return this.m_runningValues;
		}

		// Token: 0x06006881 RID: 26753 RVA: 0x00196255 File Offset: 0x00194455
		void IRunningValueHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(this.m_runningValues != null);
			if (this.m_runningValues.Count == 0)
			{
				this.m_runningValues = null;
			}
		}

		// Token: 0x06006882 RID: 26754 RVA: 0x0019627E File Offset: 0x0019447E
		DataAggregateInfoList[] IAggregateHolder.GetAggregateLists()
		{
			return new DataAggregateInfoList[] { this.m_aggregates, this.m_cellAggregates };
		}

		// Token: 0x06006883 RID: 26755 RVA: 0x00196298 File Offset: 0x00194498
		DataAggregateInfoList[] IAggregateHolder.GetPostSortAggregateLists()
		{
			return new DataAggregateInfoList[] { this.m_postSortAggregates, this.m_cellPostSortAggregates };
		}

		// Token: 0x06006884 RID: 26756 RVA: 0x001962B4 File Offset: 0x001944B4
		void IAggregateHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(this.m_aggregates != null);
			if (this.m_aggregates.Count == 0)
			{
				this.m_aggregates = null;
			}
			Global.Tracer.Assert(this.m_postSortAggregates != null);
			if (this.m_postSortAggregates.Count == 0)
			{
				this.m_postSortAggregates = null;
			}
			Global.Tracer.Assert(this.m_cellAggregates != null);
			if (this.m_cellAggregates.Count == 0)
			{
				this.m_cellAggregates = null;
			}
			Global.Tracer.Assert(this.m_cellPostSortAggregates != null);
			if (this.m_cellPostSortAggregates.Count == 0)
			{
				this.m_cellPostSortAggregates = null;
			}
		}

		// Token: 0x06006885 RID: 26757 RVA: 0x0019635D File Offset: 0x0019455D
		internal void SkipStaticHeading(ref TablixHeadingList tablixHeading, ref TablixHeadingList staticHeading)
		{
			if (tablixHeading != null && tablixHeading[0].Grouping == null)
			{
				staticHeading = tablixHeading;
				tablixHeading = tablixHeading.InnerHeadings();
				return;
			}
			staticHeading = null;
		}

		// Token: 0x06006886 RID: 26758 RVA: 0x00196383 File Offset: 0x00194583
		internal TablixHeadingList GetOuterHeading()
		{
			if (this.m_processingInnerGrouping == Pivot.ProcessingInnerGroupings.Column)
			{
				return this.TablixRows;
			}
			return this.TablixColumns;
		}

		// Token: 0x06006887 RID: 26759
		internal abstract TablixHeadingList SkipStatics(TablixHeadingList headings);

		// Token: 0x06006888 RID: 26760
		internal abstract int GetDynamicHeadingCount(bool outerGroupings);

		// Token: 0x06006889 RID: 26761 RVA: 0x0019639A File Offset: 0x0019459A
		internal void GetHeadingDefState(out TablixHeadingList outermostColumns, out TablixHeadingList outermostRows, out TablixHeadingList staticColumns, out TablixHeadingList staticRows)
		{
			outermostColumns = this.TablixColumns;
			outermostRows = this.TablixRows;
			staticColumns = null;
			staticRows = null;
			this.SkipStaticHeading(ref outermostColumns, ref staticColumns);
			this.SkipStaticHeading(ref outermostRows, ref staticRows);
		}

		// Token: 0x0600688A RID: 26762 RVA: 0x001963C4 File Offset: 0x001945C4
		internal int CreateOuterGroupingIndexList()
		{
			int dynamicHeadingCount = this.GetDynamicHeadingCount(true);
			if (this.m_outerGroupingIndexes == null)
			{
				this.m_outerGroupingIndexes = new int[dynamicHeadingCount];
				this.m_outerGroupingAggregateRowInfo = new ReportProcessing.AggregateRowInfo[dynamicHeadingCount];
			}
			return dynamicHeadingCount;
		}

		// Token: 0x0600688B RID: 26763
		internal abstract Hashtable GetOuterScopeNames(int dynamicLevel);

		// Token: 0x0600688C RID: 26764 RVA: 0x001963FA File Offset: 0x001945FA
		internal void SaveTablixAggregateRowInfo(ReportProcessing.ProcessingContext pc)
		{
			if (this.m_tablixAggregateRowInfo == null)
			{
				this.m_tablixAggregateRowInfo = new ReportProcessing.AggregateRowInfo();
			}
			this.m_tablixAggregateRowInfo.SaveAggregateInfo(pc);
		}

		// Token: 0x0600688D RID: 26765 RVA: 0x0019641B File Offset: 0x0019461B
		internal void RestoreTablixAggregateRowInfo(ReportProcessing.ProcessingContext pc)
		{
			if (this.m_tablixAggregateRowInfo != null)
			{
				this.m_tablixAggregateRowInfo.RestoreAggregateInfo(pc);
			}
		}

		// Token: 0x0600688E RID: 26766 RVA: 0x00196431 File Offset: 0x00194631
		internal void SaveOuterGroupingAggregateRowInfo(int headingLevel, ReportProcessing.ProcessingContext pc)
		{
			Global.Tracer.Assert(this.m_outerGroupingAggregateRowInfo != null);
			if (this.m_outerGroupingAggregateRowInfo[headingLevel] == null)
			{
				this.m_outerGroupingAggregateRowInfo[headingLevel] = new ReportProcessing.AggregateRowInfo();
			}
			this.m_outerGroupingAggregateRowInfo[headingLevel].SaveAggregateInfo(pc);
		}

		// Token: 0x0600688F RID: 26767 RVA: 0x0019646B File Offset: 0x0019466B
		internal void SetCellAggregateRowInfo(int headingLevel, ReportProcessing.ProcessingContext pc)
		{
			Global.Tracer.Assert(this.m_outerGroupingAggregateRowInfo != null && this.m_tablixAggregateRowInfo != null);
			this.m_tablixAggregateRowInfo.CombineAggregateInfo(pc, this.m_outerGroupingAggregateRowInfo[headingLevel]);
		}

		// Token: 0x06006890 RID: 26768 RVA: 0x001964A0 File Offset: 0x001946A0
		internal void ResetOutergGroupingAggregateRowInfo()
		{
			Global.Tracer.Assert(this.m_outerGroupingAggregateRowInfo != null);
			for (int i = 0; i < this.m_outerGroupingAggregateRowInfo.Length; i++)
			{
				this.m_outerGroupingAggregateRowInfo[i] = null;
			}
		}

		// Token: 0x06006891 RID: 26769 RVA: 0x001964DC File Offset: 0x001946DC
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.DataRegion, new MemberInfoList
			{
				new MemberInfo(MemberName.ColumnCount, Token.Int32),
				new MemberInfo(MemberName.RowCount, Token.Int32),
				new MemberInfo(MemberName.CellAggregates, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.DataAggregateInfoList),
				new MemberInfo(MemberName.ProcessingInnerGrouping, Token.Enum),
				new MemberInfo(MemberName.RunningValues, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.RunningValueInfoList),
				new MemberInfo(MemberName.CellPostSortAggregates, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.DataAggregateInfoList)
			});
		}

		// Token: 0x0400338B RID: 13195
		private int m_columnCount;

		// Token: 0x0400338C RID: 13196
		private int m_rowCount;

		// Token: 0x0400338D RID: 13197
		protected DataAggregateInfoList m_cellAggregates;

		// Token: 0x0400338E RID: 13198
		protected Pivot.ProcessingInnerGroupings m_processingInnerGrouping;

		// Token: 0x0400338F RID: 13199
		protected RunningValueInfoList m_runningValues;

		// Token: 0x04003390 RID: 13200
		protected DataAggregateInfoList m_cellPostSortAggregates;

		// Token: 0x04003391 RID: 13201
		[NonSerialized]
		protected ReportProcessing.RuntimeTablixGroupRootObj m_currentOuterHeadingGroupRoot;

		// Token: 0x04003392 RID: 13202
		[NonSerialized]
		protected int m_innermostRowFilterLevel = -1;

		// Token: 0x04003393 RID: 13203
		[NonSerialized]
		protected int m_innermostColumnFilterLevel = -1;

		// Token: 0x04003394 RID: 13204
		[NonSerialized]
		protected int[] m_outerGroupingIndexes;

		// Token: 0x04003395 RID: 13205
		[NonSerialized]
		protected ReportProcessing.AggregateRowInfo[] m_outerGroupingAggregateRowInfo;

		// Token: 0x04003396 RID: 13206
		[NonSerialized]
		protected ReportProcessing.AggregateRowInfo m_tablixAggregateRowInfo;

		// Token: 0x04003397 RID: 13207
		[NonSerialized]
		protected bool m_processCellRunningValues;

		// Token: 0x04003398 RID: 13208
		[NonSerialized]
		protected bool m_processOutermostSTCellRunningValues;
	}
}
