using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006E7 RID: 1767
	[Serializable]
	internal sealed class EndUserSort
	{
		// Token: 0x170021F9 RID: 8697
		// (get) Token: 0x06006089 RID: 24713 RVA: 0x00184F40 File Offset: 0x00183140
		// (set) Token: 0x0600608A RID: 24714 RVA: 0x00184F48 File Offset: 0x00183148
		internal int DataSetID
		{
			get
			{
				return this.m_dataSetID;
			}
			set
			{
				this.m_dataSetID = value;
			}
		}

		// Token: 0x170021FA RID: 8698
		// (get) Token: 0x0600608B RID: 24715 RVA: 0x00184F51 File Offset: 0x00183151
		// (set) Token: 0x0600608C RID: 24716 RVA: 0x00184F59 File Offset: 0x00183159
		internal ISortFilterScope SortExpressionScope
		{
			get
			{
				return this.m_sortExpressionScope;
			}
			set
			{
				this.m_sortExpressionScope = value;
			}
		}

		// Token: 0x170021FB RID: 8699
		// (get) Token: 0x0600608D RID: 24717 RVA: 0x00184F62 File Offset: 0x00183162
		// (set) Token: 0x0600608E RID: 24718 RVA: 0x00184F6A File Offset: 0x0018316A
		internal GroupingList GroupsInSortTarget
		{
			get
			{
				return this.m_groupsInSortTarget;
			}
			set
			{
				this.m_groupsInSortTarget = value;
			}
		}

		// Token: 0x170021FC RID: 8700
		// (get) Token: 0x0600608F RID: 24719 RVA: 0x00184F73 File Offset: 0x00183173
		// (set) Token: 0x06006090 RID: 24720 RVA: 0x00184F7B File Offset: 0x0018317B
		internal ISortFilterScope SortTarget
		{
			get
			{
				return this.m_sortTarget;
			}
			set
			{
				this.m_sortTarget = value;
			}
		}

		// Token: 0x170021FD RID: 8701
		// (get) Token: 0x06006091 RID: 24721 RVA: 0x00184F84 File Offset: 0x00183184
		// (set) Token: 0x06006092 RID: 24722 RVA: 0x00184F8C File Offset: 0x0018318C
		internal int SortExpressionIndex
		{
			get
			{
				return this.m_sortExpressionIndex;
			}
			set
			{
				this.m_sortExpressionIndex = value;
			}
		}

		// Token: 0x170021FE RID: 8702
		// (get) Token: 0x06006093 RID: 24723 RVA: 0x00184F95 File Offset: 0x00183195
		// (set) Token: 0x06006094 RID: 24724 RVA: 0x00184F9D File Offset: 0x0018319D
		internal SubReportList DetailScopeSubReports
		{
			get
			{
				return this.m_detailScopeSubReports;
			}
			set
			{
				this.m_detailScopeSubReports = value;
			}
		}

		// Token: 0x170021FF RID: 8703
		// (get) Token: 0x06006095 RID: 24725 RVA: 0x00184FA6 File Offset: 0x001831A6
		// (set) Token: 0x06006096 RID: 24726 RVA: 0x00184FAE File Offset: 0x001831AE
		internal ExpressionInfo SortExpression
		{
			get
			{
				return this.m_sortExpression;
			}
			set
			{
				this.m_sortExpression = value;
			}
		}

		// Token: 0x17002200 RID: 8704
		// (get) Token: 0x06006097 RID: 24727 RVA: 0x00184FB7 File Offset: 0x001831B7
		// (set) Token: 0x06006098 RID: 24728 RVA: 0x00184FBF File Offset: 0x001831BF
		internal int SortExpressionScopeID
		{
			get
			{
				return this.m_sortExpressionScopeID;
			}
			set
			{
				this.m_sortExpressionScopeID = value;
			}
		}

		// Token: 0x17002201 RID: 8705
		// (get) Token: 0x06006099 RID: 24729 RVA: 0x00184FC8 File Offset: 0x001831C8
		// (set) Token: 0x0600609A RID: 24730 RVA: 0x00184FD0 File Offset: 0x001831D0
		internal IntList GroupInSortTargetIDs
		{
			get
			{
				return this.m_groupInSortTargetIDs;
			}
			set
			{
				this.m_groupInSortTargetIDs = value;
			}
		}

		// Token: 0x17002202 RID: 8706
		// (get) Token: 0x0600609B RID: 24731 RVA: 0x00184FD9 File Offset: 0x001831D9
		// (set) Token: 0x0600609C RID: 24732 RVA: 0x00184FE1 File Offset: 0x001831E1
		internal int SortTargetID
		{
			get
			{
				return this.m_sortTargetID;
			}
			set
			{
				this.m_sortTargetID = value;
			}
		}

		// Token: 0x17002203 RID: 8707
		// (get) Token: 0x0600609D RID: 24733 RVA: 0x00184FEA File Offset: 0x001831EA
		// (set) Token: 0x0600609E RID: 24734 RVA: 0x00184FF2 File Offset: 0x001831F2
		internal string SortExpressionScopeString
		{
			get
			{
				return this.m_sortExpressionScopeString;
			}
			set
			{
				this.m_sortExpressionScopeString = value;
			}
		}

		// Token: 0x17002204 RID: 8708
		// (get) Token: 0x0600609F RID: 24735 RVA: 0x00184FFB File Offset: 0x001831FB
		// (set) Token: 0x060060A0 RID: 24736 RVA: 0x00185003 File Offset: 0x00183203
		internal string SortTargetString
		{
			get
			{
				return this.m_sortTargetString;
			}
			set
			{
				this.m_sortTargetString = value;
			}
		}

		// Token: 0x17002205 RID: 8709
		// (get) Token: 0x060060A1 RID: 24737 RVA: 0x0018500C File Offset: 0x0018320C
		// (set) Token: 0x060060A2 RID: 24738 RVA: 0x00185014 File Offset: 0x00183214
		internal bool FoundSortExpressionScope
		{
			get
			{
				return this.m_foundSortExpressionScope;
			}
			set
			{
				this.m_foundSortExpressionScope = value;
			}
		}

		// Token: 0x060060A3 RID: 24739 RVA: 0x00185020 File Offset: 0x00183220
		internal void SetSortTarget(ISortFilterScope target)
		{
			Global.Tracer.Assert(target != null);
			this.m_sortTarget = target;
			if (target.UserSortExpressions == null)
			{
				target.UserSortExpressions = new ExpressionInfoList();
			}
			this.m_sortExpressionIndex = target.UserSortExpressions.Count;
			target.UserSortExpressions.Add(this.m_sortExpression);
		}

		// Token: 0x060060A4 RID: 24740 RVA: 0x00185078 File Offset: 0x00183278
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.DataSetID, Token.Int32),
				new MemberInfo(MemberName.SortExpressionScope, Token.Reference, ObjectType.ISortFilterScope),
				new MemberInfo(MemberName.GroupsInSortTarget, Token.Reference, ObjectType.GroupingList),
				new MemberInfo(MemberName.SortTarget, Token.Reference, ObjectType.ISortFilterScope),
				new MemberInfo(MemberName.SortExpressionIndex, Token.Int32),
				new MemberInfo(MemberName.DetailScopeSubReports, Token.Reference, ObjectType.SubReportList)
			});
		}

		// Token: 0x040030FC RID: 12540
		private int m_dataSetID = -1;

		// Token: 0x040030FD RID: 12541
		[Reference]
		private ISortFilterScope m_sortExpressionScope;

		// Token: 0x040030FE RID: 12542
		[Reference]
		private GroupingList m_groupsInSortTarget;

		// Token: 0x040030FF RID: 12543
		[Reference]
		private ISortFilterScope m_sortTarget;

		// Token: 0x04003100 RID: 12544
		private int m_sortExpressionIndex = -1;

		// Token: 0x04003101 RID: 12545
		private SubReportList m_detailScopeSubReports;

		// Token: 0x04003102 RID: 12546
		[NonSerialized]
		private ExpressionInfo m_sortExpression;

		// Token: 0x04003103 RID: 12547
		[NonSerialized]
		private int m_sortExpressionScopeID = -1;

		// Token: 0x04003104 RID: 12548
		[NonSerialized]
		private IntList m_groupInSortTargetIDs;

		// Token: 0x04003105 RID: 12549
		[NonSerialized]
		private int m_sortTargetID = -1;

		// Token: 0x04003106 RID: 12550
		[NonSerialized]
		private string m_sortExpressionScopeString;

		// Token: 0x04003107 RID: 12551
		[NonSerialized]
		private string m_sortTargetString;

		// Token: 0x04003108 RID: 12552
		[NonSerialized]
		private bool m_foundSortExpressionScope;
	}
}
