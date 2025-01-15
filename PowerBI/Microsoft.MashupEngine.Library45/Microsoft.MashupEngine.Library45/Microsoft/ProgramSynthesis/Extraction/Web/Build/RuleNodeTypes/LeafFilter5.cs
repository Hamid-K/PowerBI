using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001055 RID: 4181
	public struct LeafFilter5 : IProgramNodeBuilder, IEquatable<LeafFilter5>
	{
		// Token: 0x17001633 RID: 5683
		// (get) Token: 0x06007C14 RID: 31764 RVA: 0x001A43B6 File Offset: 0x001A25B6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007C15 RID: 31765 RVA: 0x001A43BE File Offset: 0x001A25BE
		private LeafFilter5(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007C16 RID: 31766 RVA: 0x001A43C7 File Offset: 0x001A25C7
		public static LeafFilter5 CreateUnsafe(ProgramNode node)
		{
			return new LeafFilter5(node);
		}

		// Token: 0x06007C17 RID: 31767 RVA: 0x001A43D0 File Offset: 0x001A25D0
		public static LeafFilter5? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LeafFilter5)
			{
				return null;
			}
			return new LeafFilter5?(LeafFilter5.CreateUnsafe(node));
		}

		// Token: 0x06007C18 RID: 31768 RVA: 0x001A4405 File Offset: 0x001A2605
		public LeafFilter5(GrammarBuilders g, leafFExpr value0, selection10 value1)
		{
			this._node = g.Rule.LeafFilter5.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x06007C19 RID: 31769 RVA: 0x001A4437 File Offset: 0x001A2637
		public static implicit operator filterSelection5(LeafFilter5 arg)
		{
			return filterSelection5.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001634 RID: 5684
		// (get) Token: 0x06007C1A RID: 31770 RVA: 0x001A4445 File Offset: 0x001A2645
		public leafFExpr leafFExpr
		{
			get
			{
				return leafFExpr.CreateUnsafe(this.Node.Children[0].Children[0]);
			}
		}

		// Token: 0x17001635 RID: 5685
		// (get) Token: 0x06007C1B RID: 31771 RVA: 0x001A4460 File Offset: 0x001A2660
		public selection10 selection10
		{
			get
			{
				return selection10.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007C1C RID: 31772 RVA: 0x001A4474 File Offset: 0x001A2674
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007C1D RID: 31773 RVA: 0x001A4488 File Offset: 0x001A2688
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007C1E RID: 31774 RVA: 0x001A44B2 File Offset: 0x001A26B2
		public bool Equals(LeafFilter5 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400336E RID: 13166
		private ProgramNode _node;
	}
}
