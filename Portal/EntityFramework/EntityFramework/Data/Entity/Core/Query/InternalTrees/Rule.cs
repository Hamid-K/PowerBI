using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003D9 RID: 985
	internal abstract class Rule
	{
		// Token: 0x06002ECF RID: 11983 RVA: 0x00095021 File Offset: 0x00093221
		protected Rule(OpType opType, Rule.ProcessNodeDelegate nodeProcessDelegate)
		{
			this.m_opType = opType;
			this.m_nodeDelegate = nodeProcessDelegate;
		}

		// Token: 0x06002ED0 RID: 11984
		internal abstract bool Match(Node node);

		// Token: 0x06002ED1 RID: 11985 RVA: 0x00095037 File Offset: 0x00093237
		internal bool Apply(RuleProcessingContext ruleProcessingContext, Node node, out Node newNode)
		{
			return this.m_nodeDelegate(ruleProcessingContext, node, out newNode);
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06002ED2 RID: 11986 RVA: 0x00095047 File Offset: 0x00093247
		internal OpType RuleOpType
		{
			get
			{
				return this.m_opType;
			}
		}

		// Token: 0x04000FCC RID: 4044
		private readonly Rule.ProcessNodeDelegate m_nodeDelegate;

		// Token: 0x04000FCD RID: 4045
		private readonly OpType m_opType;

		// Token: 0x02000A09 RID: 2569
		// (Invoke) Token: 0x060060A1 RID: 24737
		internal delegate bool ProcessNodeDelegate(RuleProcessingContext context, Node subTree, out Node newSubTree);
	}
}
