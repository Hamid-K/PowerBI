using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001056 RID: 4182
	public struct LeafAnd : IProgramNodeBuilder, IEquatable<LeafAnd>
	{
		// Token: 0x17001636 RID: 5686
		// (get) Token: 0x06007C1F RID: 31775 RVA: 0x001A44C6 File Offset: 0x001A26C6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007C20 RID: 31776 RVA: 0x001A44CE File Offset: 0x001A26CE
		private LeafAnd(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007C21 RID: 31777 RVA: 0x001A44D7 File Offset: 0x001A26D7
		public static LeafAnd CreateUnsafe(ProgramNode node)
		{
			return new LeafAnd(node);
		}

		// Token: 0x06007C22 RID: 31778 RVA: 0x001A44E0 File Offset: 0x001A26E0
		public static LeafAnd? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LeafAnd)
			{
				return null;
			}
			return new LeafAnd?(LeafAnd.CreateUnsafe(node));
		}

		// Token: 0x06007C23 RID: 31779 RVA: 0x001A4515 File Offset: 0x001A2715
		public LeafAnd(GrammarBuilders g, leafFExpr value0, leafAtom value1)
		{
			this._node = g.Rule.LeafAnd.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x06007C24 RID: 31780 RVA: 0x001A4547 File Offset: 0x001A2747
		public static implicit operator leafFExpr(LeafAnd arg)
		{
			return leafFExpr.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001637 RID: 5687
		// (get) Token: 0x06007C25 RID: 31781 RVA: 0x001A4555 File Offset: 0x001A2755
		public leafFExpr leafFExpr
		{
			get
			{
				return leafFExpr.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001638 RID: 5688
		// (get) Token: 0x06007C26 RID: 31782 RVA: 0x001A4569 File Offset: 0x001A2769
		public leafAtom leafAtom
		{
			get
			{
				return leafAtom.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007C27 RID: 31783 RVA: 0x001A457D File Offset: 0x001A277D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007C28 RID: 31784 RVA: 0x001A4590 File Offset: 0x001A2790
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007C29 RID: 31785 RVA: 0x001A45BA File Offset: 0x001A27BA
		public bool Equals(LeafAnd other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400336F RID: 13167
		private ProgramNode _node;
	}
}
