using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000763 RID: 1891
	[Serializable]
	internal abstract class TablixHeading : ReportHierarchyNode
	{
		// Token: 0x060068D5 RID: 26837 RVA: 0x00198699 File Offset: 0x00196899
		internal TablixHeading()
		{
		}

		// Token: 0x060068D6 RID: 26838 RVA: 0x001986A8 File Offset: 0x001968A8
		internal TablixHeading(int id, DataRegion dataRegionDef)
			: base(id, dataRegionDef)
		{
			this.m_aggregates = new DataAggregateInfoList();
			this.m_postSortAggregates = new DataAggregateInfoList();
			this.m_recursiveAggregates = new DataAggregateInfoList();
		}

		// Token: 0x17002503 RID: 9475
		// (get) Token: 0x060068D7 RID: 26839 RVA: 0x001986DA File Offset: 0x001968DA
		// (set) Token: 0x060068D8 RID: 26840 RVA: 0x001986E1 File Offset: 0x001968E1
		internal new ReportHierarchyNode InnerHierarchy
		{
			get
			{
				throw new InvalidOperationException();
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17002504 RID: 9476
		// (get) Token: 0x060068D9 RID: 26841 RVA: 0x001986E8 File Offset: 0x001968E8
		// (set) Token: 0x060068DA RID: 26842 RVA: 0x001986F0 File Offset: 0x001968F0
		internal bool Subtotal
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

		// Token: 0x17002505 RID: 9477
		// (get) Token: 0x060068DB RID: 26843 RVA: 0x001986F9 File Offset: 0x001968F9
		// (set) Token: 0x060068DC RID: 26844 RVA: 0x00198701 File Offset: 0x00196901
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

		// Token: 0x17002506 RID: 9478
		// (get) Token: 0x060068DD RID: 26845 RVA: 0x0019870A File Offset: 0x0019690A
		// (set) Token: 0x060068DE RID: 26846 RVA: 0x00198712 File Offset: 0x00196912
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

		// Token: 0x17002507 RID: 9479
		// (get) Token: 0x060068DF RID: 26847 RVA: 0x0019871B File Offset: 0x0019691B
		// (set) Token: 0x060068E0 RID: 26848 RVA: 0x00198723 File Offset: 0x00196923
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

		// Token: 0x17002508 RID: 9480
		// (get) Token: 0x060068E1 RID: 26849 RVA: 0x0019872C File Offset: 0x0019692C
		// (set) Token: 0x060068E2 RID: 26850 RVA: 0x00198734 File Offset: 0x00196934
		internal int HeadingSpan
		{
			get
			{
				return this.m_headingSpan;
			}
			set
			{
				this.m_headingSpan = value;
			}
		}

		// Token: 0x17002509 RID: 9481
		// (get) Token: 0x060068E3 RID: 26851 RVA: 0x0019873D File Offset: 0x0019693D
		// (set) Token: 0x060068E4 RID: 26852 RVA: 0x00198745 File Offset: 0x00196945
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

		// Token: 0x1700250A RID: 9482
		// (get) Token: 0x060068E5 RID: 26853 RVA: 0x0019874E File Offset: 0x0019694E
		// (set) Token: 0x060068E6 RID: 26854 RVA: 0x00198756 File Offset: 0x00196956
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

		// Token: 0x1700250B RID: 9483
		// (get) Token: 0x060068E7 RID: 26855 RVA: 0x0019875F File Offset: 0x0019695F
		// (set) Token: 0x060068E8 RID: 26856 RVA: 0x00198767 File Offset: 0x00196967
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

		// Token: 0x1700250C RID: 9484
		// (get) Token: 0x060068E9 RID: 26857 RVA: 0x00198770 File Offset: 0x00196970
		// (set) Token: 0x060068EA RID: 26858 RVA: 0x00198778 File Offset: 0x00196978
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

		// Token: 0x1700250D RID: 9485
		// (get) Token: 0x060068EB RID: 26859 RVA: 0x00198781 File Offset: 0x00196981
		// (set) Token: 0x060068EC RID: 26860 RVA: 0x00198789 File Offset: 0x00196989
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

		// Token: 0x1700250E RID: 9486
		// (get) Token: 0x060068ED RID: 26861 RVA: 0x00198792 File Offset: 0x00196992
		// (set) Token: 0x060068EE RID: 26862 RVA: 0x0019879A File Offset: 0x0019699A
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

		// Token: 0x1700250F RID: 9487
		// (get) Token: 0x060068EF RID: 26863 RVA: 0x001987A3 File Offset: 0x001969A3
		// (set) Token: 0x060068F0 RID: 26864 RVA: 0x001987AB File Offset: 0x001969AB
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

		// Token: 0x17002510 RID: 9488
		// (get) Token: 0x060068F1 RID: 26865 RVA: 0x001987B4 File Offset: 0x001969B4
		// (set) Token: 0x060068F2 RID: 26866 RVA: 0x001987BC File Offset: 0x001969BC
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

		// Token: 0x060068F3 RID: 26867 RVA: 0x001987C8 File Offset: 0x001969C8
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.ReportHierarchyNode, new MemberInfoList
			{
				new MemberInfo(MemberName.Subtotal, Token.Boolean),
				new MemberInfo(MemberName.IsColumn, Token.Boolean),
				new MemberInfo(MemberName.Level, Token.Int32),
				new MemberInfo(MemberName.HasExprHost, Token.Boolean),
				new MemberInfo(MemberName.HeadingSpan, Token.Int32)
			});
		}

		// Token: 0x040033B1 RID: 13233
		protected bool m_subtotal;

		// Token: 0x040033B2 RID: 13234
		protected bool m_isColumn;

		// Token: 0x040033B3 RID: 13235
		protected int m_level;

		// Token: 0x040033B4 RID: 13236
		protected bool m_hasExprHost;

		// Token: 0x040033B5 RID: 13237
		protected int m_headingSpan = 1;

		// Token: 0x040033B6 RID: 13238
		[NonSerialized]
		protected int m_numberOfStatics;

		// Token: 0x040033B7 RID: 13239
		[NonSerialized]
		protected DataAggregateInfoList m_aggregates;

		// Token: 0x040033B8 RID: 13240
		[NonSerialized]
		protected DataAggregateInfoList m_postSortAggregates;

		// Token: 0x040033B9 RID: 13241
		[NonSerialized]
		protected DataAggregateInfoList m_recursiveAggregates;

		// Token: 0x040033BA RID: 13242
		[NonSerialized]
		protected AggregatesImpl m_outermostSTCellRVCol;

		// Token: 0x040033BB RID: 13243
		[NonSerialized]
		protected AggregatesImpl m_cellRVCol;

		// Token: 0x040033BC RID: 13244
		[NonSerialized]
		protected AggregatesImpl[] m_outermostSTCellScopedRVCollections;

		// Token: 0x040033BD RID: 13245
		[NonSerialized]
		protected AggregatesImpl[] m_cellScopedRVCollections;

		// Token: 0x040033BE RID: 13246
		[NonSerialized]
		protected Hashtable[] m_cellScopeNames;
	}
}
