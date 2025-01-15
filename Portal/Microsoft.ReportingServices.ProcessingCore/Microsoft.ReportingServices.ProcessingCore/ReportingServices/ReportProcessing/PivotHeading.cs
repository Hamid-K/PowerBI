using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006EC RID: 1772
	[Serializable]
	internal abstract class PivotHeading : ReportHierarchyNode
	{
		// Token: 0x0600614F RID: 24911 RVA: 0x00186394 File Offset: 0x00184594
		internal PivotHeading()
		{
		}

		// Token: 0x06006150 RID: 24912 RVA: 0x0018639C File Offset: 0x0018459C
		internal PivotHeading(int id, DataRegion matrixDef)
			: base(id, matrixDef)
		{
			this.m_aggregates = new DataAggregateInfoList();
			this.m_postSortAggregates = new DataAggregateInfoList();
			this.m_recursiveAggregates = new DataAggregateInfoList();
		}

		// Token: 0x1700224A RID: 8778
		// (get) Token: 0x06006151 RID: 24913 RVA: 0x001863C7 File Offset: 0x001845C7
		// (set) Token: 0x06006152 RID: 24914 RVA: 0x001863D4 File Offset: 0x001845D4
		internal PivotHeading SubHeading
		{
			get
			{
				return (PivotHeading)this.m_innerHierarchy;
			}
			set
			{
				this.m_innerHierarchy = value;
			}
		}

		// Token: 0x1700224B RID: 8779
		// (get) Token: 0x06006153 RID: 24915 RVA: 0x001863DD File Offset: 0x001845DD
		// (set) Token: 0x06006154 RID: 24916 RVA: 0x001863E5 File Offset: 0x001845E5
		internal Visibility Visibility
		{
			get
			{
				return this.m_visibility;
			}
			set
			{
				this.m_visibility = value;
			}
		}

		// Token: 0x1700224C RID: 8780
		// (get) Token: 0x06006155 RID: 24917 RVA: 0x001863EE File Offset: 0x001845EE
		// (set) Token: 0x06006156 RID: 24918 RVA: 0x001863F6 File Offset: 0x001845F6
		internal Subtotal Subtotal
		{
			get
			{
				return this.m_subtotal;
			}
			set
			{
				this.m_subtotal = value;
			}
		}

		// Token: 0x1700224D RID: 8781
		// (get) Token: 0x06006157 RID: 24919 RVA: 0x001863FF File Offset: 0x001845FF
		// (set) Token: 0x06006158 RID: 24920 RVA: 0x00186407 File Offset: 0x00184607
		internal int Level
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

		// Token: 0x1700224E RID: 8782
		// (get) Token: 0x06006159 RID: 24921 RVA: 0x00186410 File Offset: 0x00184610
		// (set) Token: 0x0600615A RID: 24922 RVA: 0x00186418 File Offset: 0x00184618
		internal bool IsColumn
		{
			get
			{
				return this.m_isColumn;
			}
			set
			{
				this.m_isColumn = value;
			}
		}

		// Token: 0x1700224F RID: 8783
		// (get) Token: 0x0600615B RID: 24923 RVA: 0x00186421 File Offset: 0x00184621
		// (set) Token: 0x0600615C RID: 24924 RVA: 0x00186429 File Offset: 0x00184629
		internal bool HasExprHost
		{
			get
			{
				return this.m_hasExprHost;
			}
			set
			{
				this.m_hasExprHost = value;
			}
		}

		// Token: 0x17002250 RID: 8784
		// (get) Token: 0x0600615D RID: 24925 RVA: 0x00186432 File Offset: 0x00184632
		// (set) Token: 0x0600615E RID: 24926 RVA: 0x0018643A File Offset: 0x0018463A
		internal int SubtotalSpan
		{
			get
			{
				return this.m_subtotalSpan;
			}
			set
			{
				this.m_subtotalSpan = value;
			}
		}

		// Token: 0x17002251 RID: 8785
		// (get) Token: 0x0600615F RID: 24927 RVA: 0x00186443 File Offset: 0x00184643
		// (set) Token: 0x06006160 RID: 24928 RVA: 0x0018644B File Offset: 0x0018464B
		internal IntList IDs
		{
			get
			{
				return this.m_IDs;
			}
			set
			{
				this.m_IDs = value;
			}
		}

		// Token: 0x17002252 RID: 8786
		// (get) Token: 0x06006161 RID: 24929 RVA: 0x00186454 File Offset: 0x00184654
		// (set) Token: 0x06006162 RID: 24930 RVA: 0x0018645C File Offset: 0x0018465C
		internal DataAggregateInfoList Aggregates
		{
			get
			{
				return this.m_aggregates;
			}
			set
			{
				this.m_aggregates = value;
			}
		}

		// Token: 0x17002253 RID: 8787
		// (get) Token: 0x06006163 RID: 24931 RVA: 0x00186465 File Offset: 0x00184665
		// (set) Token: 0x06006164 RID: 24932 RVA: 0x0018646D File Offset: 0x0018466D
		internal DataAggregateInfoList PostSortAggregates
		{
			get
			{
				return this.m_postSortAggregates;
			}
			set
			{
				this.m_postSortAggregates = value;
			}
		}

		// Token: 0x17002254 RID: 8788
		// (get) Token: 0x06006165 RID: 24933 RVA: 0x00186476 File Offset: 0x00184676
		// (set) Token: 0x06006166 RID: 24934 RVA: 0x0018647E File Offset: 0x0018467E
		internal DataAggregateInfoList RecursiveAggregates
		{
			get
			{
				return this.m_recursiveAggregates;
			}
			set
			{
				this.m_recursiveAggregates = value;
			}
		}

		// Token: 0x17002255 RID: 8789
		// (get) Token: 0x06006167 RID: 24935 RVA: 0x00186487 File Offset: 0x00184687
		// (set) Token: 0x06006168 RID: 24936 RVA: 0x0018648F File Offset: 0x0018468F
		internal int NumberOfStatics
		{
			get
			{
				return this.m_numberOfStatics;
			}
			set
			{
				this.m_numberOfStatics = value;
			}
		}

		// Token: 0x17002256 RID: 8790
		// (get) Token: 0x06006169 RID: 24937 RVA: 0x00186498 File Offset: 0x00184698
		// (set) Token: 0x0600616A RID: 24938 RVA: 0x001864A0 File Offset: 0x001846A0
		internal AggregatesImpl OutermostSTCellRVCol
		{
			get
			{
				return this.m_outermostSTCellRVCol;
			}
			set
			{
				this.m_outermostSTCellRVCol = value;
			}
		}

		// Token: 0x17002257 RID: 8791
		// (get) Token: 0x0600616B RID: 24939 RVA: 0x001864A9 File Offset: 0x001846A9
		// (set) Token: 0x0600616C RID: 24940 RVA: 0x001864B1 File Offset: 0x001846B1
		internal AggregatesImpl CellRVCol
		{
			get
			{
				return this.m_cellRVCol;
			}
			set
			{
				this.m_cellRVCol = value;
			}
		}

		// Token: 0x17002258 RID: 8792
		// (get) Token: 0x0600616D RID: 24941 RVA: 0x001864BA File Offset: 0x001846BA
		// (set) Token: 0x0600616E RID: 24942 RVA: 0x001864C2 File Offset: 0x001846C2
		internal AggregatesImpl[] OutermostSTCellScopedRVCollections
		{
			get
			{
				return this.m_outermostSTCellScopedRVCollections;
			}
			set
			{
				this.m_outermostSTCellScopedRVCollections = value;
			}
		}

		// Token: 0x17002259 RID: 8793
		// (get) Token: 0x0600616F RID: 24943 RVA: 0x001864CB File Offset: 0x001846CB
		// (set) Token: 0x06006170 RID: 24944 RVA: 0x001864D3 File Offset: 0x001846D3
		internal AggregatesImpl[] CellScopedRVCollections
		{
			get
			{
				return this.m_cellScopedRVCollections;
			}
			set
			{
				this.m_cellScopedRVCollections = value;
			}
		}

		// Token: 0x1700225A RID: 8794
		// (get) Token: 0x06006171 RID: 24945 RVA: 0x001864DC File Offset: 0x001846DC
		// (set) Token: 0x06006172 RID: 24946 RVA: 0x001864E4 File Offset: 0x001846E4
		internal Hashtable[] CellScopeNames
		{
			get
			{
				return this.m_cellScopeNames;
			}
			set
			{
				this.m_cellScopeNames = value;
			}
		}

		// Token: 0x06006173 RID: 24947 RVA: 0x001864F0 File Offset: 0x001846F0
		internal void CopySubHeadingAggregates()
		{
			if (this.SubHeading != null)
			{
				this.SubHeading.CopySubHeadingAggregates();
				Pivot.CopyAggregates(this.SubHeading.Aggregates, this.m_aggregates);
				Pivot.CopyAggregates(this.SubHeading.PostSortAggregates, this.m_postSortAggregates);
				Pivot.CopyAggregates(this.SubHeading.RecursiveAggregates, this.m_aggregates);
			}
		}

		// Token: 0x06006174 RID: 24948 RVA: 0x00186554 File Offset: 0x00184754
		internal void TransferHeadingAggregates()
		{
			if (this.SubHeading != null)
			{
				this.SubHeading.TransferHeadingAggregates();
			}
			if (this.m_grouping != null)
			{
				for (int i = 0; i < this.m_aggregates.Count; i++)
				{
					this.m_grouping.Aggregates.Add(this.m_aggregates[i]);
				}
			}
			this.m_aggregates = null;
			if (this.m_grouping != null)
			{
				for (int j = 0; j < this.m_postSortAggregates.Count; j++)
				{
					this.m_grouping.PostSortAggregates.Add(this.m_postSortAggregates[j]);
				}
			}
			this.m_postSortAggregates = null;
			if (this.m_grouping != null)
			{
				for (int k = 0; k < this.m_recursiveAggregates.Count; k++)
				{
					this.m_grouping.RecursiveAggregates.Add(this.m_recursiveAggregates[k]);
				}
			}
			this.m_recursiveAggregates = null;
		}

		// Token: 0x06006175 RID: 24949 RVA: 0x0018663C File Offset: 0x0018483C
		internal PivotHeading GetInnerStaticHeading()
		{
			Pivot pivot = (Pivot)this.m_dataRegionDef;
			PivotHeading pivotHeading;
			if (this.m_isColumn)
			{
				pivotHeading = pivot.PivotStaticColumns;
			}
			else
			{
				pivotHeading = pivot.PivotStaticRows;
			}
			if (pivotHeading != null && pivotHeading.Level > this.m_level)
			{
				return pivotHeading;
			}
			return null;
		}

		// Token: 0x06006176 RID: 24950 RVA: 0x00186684 File Offset: 0x00184884
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.ReportHierarchyNode, new MemberInfoList
			{
				new MemberInfo(MemberName.Visibility, ObjectType.Visibility),
				new MemberInfo(MemberName.Subtotal, ObjectType.Subtotal),
				new MemberInfo(MemberName.Level, Token.Int32),
				new MemberInfo(MemberName.IsColumn, Token.Boolean),
				new MemberInfo(MemberName.HasExprHost, Token.Boolean),
				new MemberInfo(MemberName.SubtotalSpan, Token.Int32),
				new MemberInfo(MemberName.IDs, ObjectType.IntList)
			});
		}

		// Token: 0x04003145 RID: 12613
		protected Visibility m_visibility;

		// Token: 0x04003146 RID: 12614
		protected Subtotal m_subtotal;

		// Token: 0x04003147 RID: 12615
		protected int m_level;

		// Token: 0x04003148 RID: 12616
		protected bool m_isColumn;

		// Token: 0x04003149 RID: 12617
		protected bool m_hasExprHost;

		// Token: 0x0400314A RID: 12618
		protected int m_subtotalSpan;

		// Token: 0x0400314B RID: 12619
		private IntList m_IDs;

		// Token: 0x0400314C RID: 12620
		[NonSerialized]
		protected int m_numberOfStatics;

		// Token: 0x0400314D RID: 12621
		[NonSerialized]
		protected DataAggregateInfoList m_aggregates;

		// Token: 0x0400314E RID: 12622
		[NonSerialized]
		protected DataAggregateInfoList m_postSortAggregates;

		// Token: 0x0400314F RID: 12623
		[NonSerialized]
		protected DataAggregateInfoList m_recursiveAggregates;

		// Token: 0x04003150 RID: 12624
		[NonSerialized]
		protected AggregatesImpl m_outermostSTCellRVCol;

		// Token: 0x04003151 RID: 12625
		[NonSerialized]
		protected AggregatesImpl m_cellRVCol;

		// Token: 0x04003152 RID: 12626
		[NonSerialized]
		protected AggregatesImpl[] m_outermostSTCellScopedRVCollections;

		// Token: 0x04003153 RID: 12627
		[NonSerialized]
		protected AggregatesImpl[] m_cellScopedRVCollections;

		// Token: 0x04003154 RID: 12628
		[NonSerialized]
		protected Hashtable[] m_cellScopeNames;
	}
}
