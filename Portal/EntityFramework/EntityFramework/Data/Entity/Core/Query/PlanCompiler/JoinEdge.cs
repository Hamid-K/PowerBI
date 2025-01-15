using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000349 RID: 841
	internal class JoinEdge
	{
		// Token: 0x06002848 RID: 10312 RVA: 0x00079AEC File Offset: 0x00077CEC
		private JoinEdge(AugmentedTableNode left, AugmentedTableNode right, AugmentedJoinNode joinNode, JoinKind joinKind, List<ColumnVar> leftVars, List<ColumnVar> rightVars)
		{
			this.m_left = left;
			this.m_right = right;
			this.JoinKind = joinKind;
			this.m_joinNode = joinNode;
			this.m_leftVars = leftVars;
			this.m_rightVars = rightVars;
			PlanCompiler.Assert(this.m_leftVars.Count == this.m_rightVars.Count, "Count mismatch: " + this.m_leftVars.Count.ToString() + "," + this.m_rightVars.Count.ToString());
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06002849 RID: 10313 RVA: 0x00079B7E File Offset: 0x00077D7E
		internal AugmentedTableNode Left
		{
			get
			{
				return this.m_left;
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x0600284A RID: 10314 RVA: 0x00079B86 File Offset: 0x00077D86
		internal AugmentedTableNode Right
		{
			get
			{
				return this.m_right;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x0600284B RID: 10315 RVA: 0x00079B8E File Offset: 0x00077D8E
		internal AugmentedJoinNode JoinNode
		{
			get
			{
				return this.m_joinNode;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x0600284C RID: 10316 RVA: 0x00079B96 File Offset: 0x00077D96
		// (set) Token: 0x0600284D RID: 10317 RVA: 0x00079B9E File Offset: 0x00077D9E
		internal JoinKind JoinKind { get; set; }

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x0600284E RID: 10318 RVA: 0x00079BA7 File Offset: 0x00077DA7
		internal List<ColumnVar> LeftVars
		{
			get
			{
				return this.m_leftVars;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x0600284F RID: 10319 RVA: 0x00079BAF File Offset: 0x00077DAF
		internal List<ColumnVar> RightVars
		{
			get
			{
				return this.m_rightVars;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06002850 RID: 10320 RVA: 0x00079BB7 File Offset: 0x00077DB7
		internal bool IsEliminated
		{
			get
			{
				return this.Left.IsEliminated || this.Right.IsEliminated;
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06002851 RID: 10321 RVA: 0x00079BD4 File Offset: 0x00077DD4
		internal bool RestrictedElimination
		{
			get
			{
				return this.m_joinNode != null && (this.m_joinNode.OtherPredicate != null || this.m_left.LastVisibleId < this.m_joinNode.Id || this.m_right.LastVisibleId < this.m_joinNode.Id);
			}
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x00079C2C File Offset: 0x00077E2C
		internal static JoinEdge CreateJoinEdge(AugmentedTableNode left, AugmentedTableNode right, AugmentedJoinNode joinNode, ColumnVar leftVar, ColumnVar rightVar)
		{
			List<ColumnVar> list = new List<ColumnVar>();
			List<ColumnVar> list2 = new List<ColumnVar>();
			list.Add(leftVar);
			list2.Add(rightVar);
			OpType opType = joinNode.Node.Op.OpType;
			PlanCompiler.Assert(opType == OpType.LeftOuterJoin || opType == OpType.InnerJoin, "Unexpected join type for join edge: " + opType.ToString());
			JoinKind joinKind = ((opType == OpType.LeftOuterJoin) ? JoinKind.LeftOuter : JoinKind.Inner);
			return new JoinEdge(left, right, joinNode, joinKind, list, list2);
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x00079CA3 File Offset: 0x00077EA3
		internal static JoinEdge CreateTransitiveJoinEdge(AugmentedTableNode left, AugmentedTableNode right, JoinKind joinKind, List<ColumnVar> leftVars, List<ColumnVar> rightVars)
		{
			return new JoinEdge(left, right, null, joinKind, leftVars, rightVars);
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x00079CB1 File Offset: 0x00077EB1
		internal bool AddCondition(AugmentedJoinNode joinNode, ColumnVar leftVar, ColumnVar rightVar)
		{
			if (joinNode != this.m_joinNode)
			{
				return false;
			}
			this.m_leftVars.Add(leftVar);
			this.m_rightVars.Add(rightVar);
			return true;
		}

		// Token: 0x04000E01 RID: 3585
		private readonly AugmentedTableNode m_left;

		// Token: 0x04000E02 RID: 3586
		private readonly AugmentedTableNode m_right;

		// Token: 0x04000E03 RID: 3587
		private readonly AugmentedJoinNode m_joinNode;

		// Token: 0x04000E04 RID: 3588
		private readonly List<ColumnVar> m_leftVars;

		// Token: 0x04000E05 RID: 3589
		private readonly List<ColumnVar> m_rightVars;
	}
}
