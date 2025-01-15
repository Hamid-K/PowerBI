using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000330 RID: 816
	internal sealed class AugmentedJoinNode : AugmentedNode
	{
		// Token: 0x060026F5 RID: 9973 RVA: 0x00071108 File Offset: 0x0006F308
		internal AugmentedJoinNode(int id, Node node, AugmentedNode leftChild, AugmentedNode rightChild, List<ColumnVar> leftVars, List<ColumnVar> rightVars, Node otherPredicate)
			: this(id, node, new List<AugmentedNode>(new AugmentedNode[] { leftChild, rightChild }))
		{
			this.m_otherPredicate = otherPredicate;
			this.m_rightVars = rightVars;
			this.m_leftVars = leftVars;
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x0007113E File Offset: 0x0006F33E
		internal AugmentedJoinNode(int id, Node node, List<AugmentedNode> children)
			: base(id, node, children)
		{
			this.m_leftVars = new List<ColumnVar>();
			this.m_rightVars = new List<ColumnVar>();
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x060026F7 RID: 9975 RVA: 0x0007115F File Offset: 0x0006F35F
		internal Node OtherPredicate
		{
			get
			{
				return this.m_otherPredicate;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x060026F8 RID: 9976 RVA: 0x00071167 File Offset: 0x0006F367
		internal List<ColumnVar> LeftVars
		{
			get
			{
				return this.m_leftVars;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x060026F9 RID: 9977 RVA: 0x0007116F File Offset: 0x0006F36F
		internal List<ColumnVar> RightVars
		{
			get
			{
				return this.m_rightVars;
			}
		}

		// Token: 0x04000D94 RID: 3476
		private readonly List<ColumnVar> m_leftVars;

		// Token: 0x04000D95 RID: 3477
		private readonly List<ColumnVar> m_rightVars;

		// Token: 0x04000D96 RID: 3478
		private readonly Node m_otherPredicate;
	}
}
