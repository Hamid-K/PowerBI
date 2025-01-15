using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006EB RID: 1771
	[Serializable]
	internal abstract class Pivot : DataRegion, IAggregateHolder, IRunningValueHolder
	{
		// Token: 0x0600611A RID: 24858 RVA: 0x00185D95 File Offset: 0x00183F95
		internal Pivot(ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x0600611B RID: 24859 RVA: 0x00185DAC File Offset: 0x00183FAC
		internal Pivot(int id, ReportItem parent)
			: base(id, parent)
		{
			this.m_runningValues = new RunningValueInfoList();
			this.m_cellAggregates = new DataAggregateInfoList();
			this.m_cellPostSortAggregates = new DataAggregateInfoList();
		}

		// Token: 0x17002238 RID: 8760
		// (get) Token: 0x0600611C RID: 24860
		internal abstract PivotHeading PivotColumns { get; }

		// Token: 0x17002239 RID: 8761
		// (get) Token: 0x0600611D RID: 24861
		internal abstract PivotHeading PivotRows { get; }

		// Token: 0x1700223A RID: 8762
		// (get) Token: 0x0600611E RID: 24862 RVA: 0x00185DE5 File Offset: 0x00183FE5
		// (set) Token: 0x0600611F RID: 24863 RVA: 0x00185DED File Offset: 0x00183FED
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

		// Token: 0x1700223B RID: 8763
		// (get) Token: 0x06006120 RID: 24864 RVA: 0x00185DF6 File Offset: 0x00183FF6
		// (set) Token: 0x06006121 RID: 24865 RVA: 0x00185DFE File Offset: 0x00183FFE
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

		// Token: 0x1700223C RID: 8764
		// (get) Token: 0x06006122 RID: 24866 RVA: 0x00185E07 File Offset: 0x00184007
		// (set) Token: 0x06006123 RID: 24867 RVA: 0x00185E0F File Offset: 0x0018400F
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

		// Token: 0x1700223D RID: 8765
		// (get) Token: 0x06006124 RID: 24868 RVA: 0x00185E18 File Offset: 0x00184018
		// (set) Token: 0x06006125 RID: 24869 RVA: 0x00185E20 File Offset: 0x00184020
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

		// Token: 0x1700223E RID: 8766
		// (get) Token: 0x06006126 RID: 24870
		internal abstract RunningValueInfoList PivotCellRunningValues { get; }

		// Token: 0x1700223F RID: 8767
		// (get) Token: 0x06006127 RID: 24871 RVA: 0x00185E29 File Offset: 0x00184029
		// (set) Token: 0x06006128 RID: 24872 RVA: 0x00185E31 File Offset: 0x00184031
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

		// Token: 0x17002240 RID: 8768
		// (get) Token: 0x06006129 RID: 24873
		internal abstract PivotHeading PivotStaticColumns { get; }

		// Token: 0x17002241 RID: 8769
		// (get) Token: 0x0600612A RID: 24874
		internal abstract PivotHeading PivotStaticRows { get; }

		// Token: 0x17002242 RID: 8770
		// (get) Token: 0x0600612B RID: 24875 RVA: 0x00185E3A File Offset: 0x0018403A
		// (set) Token: 0x0600612C RID: 24876 RVA: 0x00185E42 File Offset: 0x00184042
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

		// Token: 0x17002243 RID: 8771
		// (get) Token: 0x0600612D RID: 24877 RVA: 0x00185E4B File Offset: 0x0018404B
		// (set) Token: 0x0600612E RID: 24878 RVA: 0x00185E53 File Offset: 0x00184053
		internal DataElementOutputTypes CellDataElementOutput
		{
			get
			{
				return this.m_cellDataElementOutput;
			}
			set
			{
				this.m_cellDataElementOutput = value;
			}
		}

		// Token: 0x17002244 RID: 8772
		// (get) Token: 0x0600612F RID: 24879 RVA: 0x00185E5C File Offset: 0x0018405C
		// (set) Token: 0x06006130 RID: 24880 RVA: 0x00185E64 File Offset: 0x00184064
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

		// Token: 0x17002245 RID: 8773
		// (get) Token: 0x06006131 RID: 24881 RVA: 0x00185E6D File Offset: 0x0018406D
		// (set) Token: 0x06006132 RID: 24882 RVA: 0x00185E75 File Offset: 0x00184075
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

		// Token: 0x17002246 RID: 8774
		// (get) Token: 0x06006133 RID: 24883 RVA: 0x00185E7E File Offset: 0x0018407E
		internal int[] OuterGroupingIndexes
		{
			get
			{
				return this.m_outerGroupingIndexes;
			}
		}

		// Token: 0x17002247 RID: 8775
		// (get) Token: 0x06006134 RID: 24884 RVA: 0x00185E86 File Offset: 0x00184086
		// (set) Token: 0x06006135 RID: 24885 RVA: 0x00185E8E File Offset: 0x0018408E
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

		// Token: 0x17002248 RID: 8776
		// (get) Token: 0x06006136 RID: 24886 RVA: 0x00185E97 File Offset: 0x00184097
		// (set) Token: 0x06006137 RID: 24887 RVA: 0x00185E9F File Offset: 0x0018409F
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

		// Token: 0x17002249 RID: 8777
		// (get) Token: 0x06006138 RID: 24888 RVA: 0x00185EA8 File Offset: 0x001840A8
		// (set) Token: 0x06006139 RID: 24889 RVA: 0x00185EB0 File Offset: 0x001840B0
		internal ReportProcessing.RuntimePivotGroupRootObj CurrentOuterHeadingGroupRoot
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

		// Token: 0x0600613A RID: 24890 RVA: 0x00185EB9 File Offset: 0x001840B9
		internal void CopyHeadingAggregates(PivotHeading heading)
		{
			if (heading != null)
			{
				heading.CopySubHeadingAggregates();
				Pivot.CopyAggregates(heading.Aggregates, this.m_aggregates);
				Pivot.CopyAggregates(heading.PostSortAggregates, this.m_postSortAggregates);
				Pivot.CopyAggregates(heading.RecursiveAggregates, this.m_aggregates);
			}
		}

		// Token: 0x0600613B RID: 24891 RVA: 0x00185EF8 File Offset: 0x001840F8
		internal static void CopyAggregates(DataAggregateInfoList srcAggregates, DataAggregateInfoList targetAggregates)
		{
			for (int i = 0; i < srcAggregates.Count; i++)
			{
				DataAggregateInfo dataAggregateInfo = srcAggregates[i];
				targetAggregates.Add(dataAggregateInfo);
				dataAggregateInfo.IsCopied = true;
			}
		}

		// Token: 0x0600613C RID: 24892 RVA: 0x00185F2D File Offset: 0x0018412D
		RunningValueInfoList IRunningValueHolder.GetRunningValueList()
		{
			return this.m_runningValues;
		}

		// Token: 0x0600613D RID: 24893 RVA: 0x00185F35 File Offset: 0x00184135
		void IRunningValueHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(this.m_runningValues != null);
			if (this.m_runningValues.Count == 0)
			{
				this.m_runningValues = null;
			}
		}

		// Token: 0x0600613E RID: 24894 RVA: 0x00185F5E File Offset: 0x0018415E
		DataAggregateInfoList[] IAggregateHolder.GetAggregateLists()
		{
			return new DataAggregateInfoList[] { this.m_aggregates, this.m_cellAggregates };
		}

		// Token: 0x0600613F RID: 24895 RVA: 0x00185F78 File Offset: 0x00184178
		DataAggregateInfoList[] IAggregateHolder.GetPostSortAggregateLists()
		{
			return new DataAggregateInfoList[] { this.m_postSortAggregates, this.m_cellPostSortAggregates };
		}

		// Token: 0x06006140 RID: 24896 RVA: 0x00185F94 File Offset: 0x00184194
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

		// Token: 0x06006141 RID: 24897 RVA: 0x0018603D File Offset: 0x0018423D
		internal bool SubtotalInInnerHeading(ref PivotHeading innerHeading, ref PivotHeading staticHeading)
		{
			this.SkipStaticHeading(ref innerHeading, ref staticHeading);
			return innerHeading != null && innerHeading.Subtotal != null;
		}

		// Token: 0x06006142 RID: 24898 RVA: 0x00186057 File Offset: 0x00184257
		internal void SkipStaticHeading(ref PivotHeading pivotHeading, ref PivotHeading staticHeading)
		{
			if (pivotHeading != null && pivotHeading.Grouping == null)
			{
				staticHeading = pivotHeading;
				pivotHeading = (PivotHeading)pivotHeading.InnerHierarchy;
				return;
			}
			staticHeading = null;
		}

		// Token: 0x06006143 RID: 24899 RVA: 0x0018607C File Offset: 0x0018427C
		internal void GetHeadingDefState(out PivotHeading outermostColumn, out bool outermostColumnSubtotal, out PivotHeading staticColumn, out PivotHeading outermostRow, out bool outermostRowSubtotal, out PivotHeading staticRow)
		{
			outermostRowSubtotal = false;
			outermostColumnSubtotal = false;
			staticRow = null;
			outermostRow = this.PivotRows;
			outermostRowSubtotal = this.SubtotalInInnerHeading(ref outermostRow, ref staticRow);
			if (outermostRow == null)
			{
				outermostRowSubtotal = true;
			}
			staticColumn = null;
			outermostColumn = this.PivotColumns;
			outermostColumnSubtotal = this.SubtotalInInnerHeading(ref outermostColumn, ref staticColumn);
			if (outermostColumn == null)
			{
				outermostColumnSubtotal = true;
			}
		}

		// Token: 0x06006144 RID: 24900 RVA: 0x001860CF File Offset: 0x001842CF
		internal PivotHeading GetPivotHeading(bool outerHeading)
		{
			if ((outerHeading && this.m_processingInnerGrouping == Pivot.ProcessingInnerGroupings.Column) || (!outerHeading && this.m_processingInnerGrouping == Pivot.ProcessingInnerGroupings.Row))
			{
				return this.PivotRows;
			}
			return this.PivotColumns;
		}

		// Token: 0x06006145 RID: 24901 RVA: 0x001860F8 File Offset: 0x001842F8
		internal PivotHeading GetOuterHeading(int level)
		{
			PivotHeading pivotHeading = this.GetPivotHeading(true);
			PivotHeading pivotHeading2 = null;
			for (int i = 0; i <= level; i++)
			{
				this.SkipStaticHeading(ref pivotHeading, ref pivotHeading2);
				if (pivotHeading != null && i < level)
				{
					pivotHeading = (PivotHeading)pivotHeading.InnerHierarchy;
				}
			}
			return pivotHeading;
		}

		// Token: 0x06006146 RID: 24902 RVA: 0x0018613C File Offset: 0x0018433C
		internal int GetDynamicHeadingCount(bool outerGroupings)
		{
			int num;
			if ((outerGroupings && this.m_processingInnerGrouping == Pivot.ProcessingInnerGroupings.Column) || (!outerGroupings && this.m_processingInnerGrouping == Pivot.ProcessingInnerGroupings.Row))
			{
				num = this.m_rowCount;
				if (this.PivotStaticRows != null)
				{
					num--;
				}
			}
			else
			{
				num = this.m_columnCount;
				if (this.PivotStaticColumns != null)
				{
					num--;
				}
			}
			return num;
		}

		// Token: 0x06006147 RID: 24903 RVA: 0x0018618C File Offset: 0x0018438C
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

		// Token: 0x06006148 RID: 24904 RVA: 0x001861C4 File Offset: 0x001843C4
		internal Hashtable GetOuterScopeNames(int dynamicLevel)
		{
			Hashtable hashtable = new Hashtable();
			PivotHeading pivotHeading = this.GetPivotHeading(true);
			int num = 0;
			while (num <= dynamicLevel && pivotHeading != null)
			{
				if (pivotHeading.Grouping != null)
				{
					hashtable.Add(pivotHeading.Grouping.Name, pivotHeading.Grouping);
					num++;
				}
				pivotHeading = (PivotHeading)pivotHeading.InnerHierarchy;
			}
			return hashtable;
		}

		// Token: 0x06006149 RID: 24905 RVA: 0x0018621A File Offset: 0x0018441A
		internal void SavePivotAggregateRowInfo(ReportProcessing.ProcessingContext pc)
		{
			if (this.m_pivotAggregateRowInfo == null)
			{
				this.m_pivotAggregateRowInfo = new ReportProcessing.AggregateRowInfo();
			}
			this.m_pivotAggregateRowInfo.SaveAggregateInfo(pc);
		}

		// Token: 0x0600614A RID: 24906 RVA: 0x0018623B File Offset: 0x0018443B
		internal void RestorePivotAggregateRowInfo(ReportProcessing.ProcessingContext pc)
		{
			if (this.m_pivotAggregateRowInfo != null)
			{
				this.m_pivotAggregateRowInfo.RestoreAggregateInfo(pc);
			}
		}

		// Token: 0x0600614B RID: 24907 RVA: 0x00186251 File Offset: 0x00184451
		internal void SaveOuterGroupingAggregateRowInfo(int headingLevel, ReportProcessing.ProcessingContext pc)
		{
			Global.Tracer.Assert(this.m_outerGroupingAggregateRowInfo != null);
			if (this.m_outerGroupingAggregateRowInfo[headingLevel] == null)
			{
				this.m_outerGroupingAggregateRowInfo[headingLevel] = new ReportProcessing.AggregateRowInfo();
			}
			this.m_outerGroupingAggregateRowInfo[headingLevel].SaveAggregateInfo(pc);
		}

		// Token: 0x0600614C RID: 24908 RVA: 0x0018628B File Offset: 0x0018448B
		internal void SetCellAggregateRowInfo(int headingLevel, ReportProcessing.ProcessingContext pc)
		{
			Global.Tracer.Assert(this.m_outerGroupingAggregateRowInfo != null && this.m_pivotAggregateRowInfo != null);
			this.m_pivotAggregateRowInfo.CombineAggregateInfo(pc, this.m_outerGroupingAggregateRowInfo[headingLevel]);
		}

		// Token: 0x0600614D RID: 24909 RVA: 0x001862C0 File Offset: 0x001844C0
		internal void ResetOutergGroupingAggregateRowInfo()
		{
			Global.Tracer.Assert(this.m_outerGroupingAggregateRowInfo != null);
			for (int i = 0; i < this.m_outerGroupingAggregateRowInfo.Length; i++)
			{
				this.m_outerGroupingAggregateRowInfo[i] = null;
			}
		}

		// Token: 0x0600614E RID: 24910 RVA: 0x001862FC File Offset: 0x001844FC
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.DataRegion, new MemberInfoList
			{
				new MemberInfo(MemberName.ColumnCount, Token.Int32),
				new MemberInfo(MemberName.RowCount, Token.Int32),
				new MemberInfo(MemberName.CellAggregates, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.DataAggregateInfoList),
				new MemberInfo(MemberName.ProcessingInnerGrouping, Token.Enum),
				new MemberInfo(MemberName.RunningValues, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.RunningValueInfoList),
				new MemberInfo(MemberName.CellPostSortAggregates, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.DataAggregateInfoList),
				new MemberInfo(MemberName.CellDataElementOutput, Token.Enum)
			});
		}

		// Token: 0x04003136 RID: 12598
		private int m_columnCount;

		// Token: 0x04003137 RID: 12599
		private int m_rowCount;

		// Token: 0x04003138 RID: 12600
		protected DataAggregateInfoList m_cellAggregates;

		// Token: 0x04003139 RID: 12601
		protected Pivot.ProcessingInnerGroupings m_processingInnerGrouping;

		// Token: 0x0400313A RID: 12602
		protected RunningValueInfoList m_runningValues;

		// Token: 0x0400313B RID: 12603
		protected DataAggregateInfoList m_cellPostSortAggregates;

		// Token: 0x0400313C RID: 12604
		private DataElementOutputTypes m_cellDataElementOutput;

		// Token: 0x0400313D RID: 12605
		[NonSerialized]
		protected ReportProcessing.RuntimePivotGroupRootObj m_currentOuterHeadingGroupRoot;

		// Token: 0x0400313E RID: 12606
		[NonSerialized]
		protected int m_innermostRowFilterLevel = -1;

		// Token: 0x0400313F RID: 12607
		[NonSerialized]
		protected int m_innermostColumnFilterLevel = -1;

		// Token: 0x04003140 RID: 12608
		[NonSerialized]
		protected int[] m_outerGroupingIndexes;

		// Token: 0x04003141 RID: 12609
		[NonSerialized]
		protected ReportProcessing.AggregateRowInfo[] m_outerGroupingAggregateRowInfo;

		// Token: 0x04003142 RID: 12610
		[NonSerialized]
		protected ReportProcessing.AggregateRowInfo m_pivotAggregateRowInfo;

		// Token: 0x04003143 RID: 12611
		[NonSerialized]
		protected bool m_processCellRunningValues;

		// Token: 0x04003144 RID: 12612
		[NonSerialized]
		protected bool m_processOutermostSTCellRunningValues;

		// Token: 0x02000CC5 RID: 3269
		internal enum ProcessingInnerGroupings
		{
			// Token: 0x04004E93 RID: 20115
			Column,
			// Token: 0x04004E94 RID: 20116
			Row
		}
	}
}
