using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200104F RID: 4175
	public struct LeafFilter1 : IProgramNodeBuilder, IEquatable<LeafFilter1>
	{
		// Token: 0x17001621 RID: 5665
		// (get) Token: 0x06007BD2 RID: 31698 RVA: 0x001A3D56 File Offset: 0x001A1F56
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007BD3 RID: 31699 RVA: 0x001A3D5E File Offset: 0x001A1F5E
		private LeafFilter1(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007BD4 RID: 31700 RVA: 0x001A3D67 File Offset: 0x001A1F67
		public static LeafFilter1 CreateUnsafe(ProgramNode node)
		{
			return new LeafFilter1(node);
		}

		// Token: 0x06007BD5 RID: 31701 RVA: 0x001A3D70 File Offset: 0x001A1F70
		public static LeafFilter1? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LeafFilter1)
			{
				return null;
			}
			return new LeafFilter1?(LeafFilter1.CreateUnsafe(node));
		}

		// Token: 0x06007BD6 RID: 31702 RVA: 0x001A3DA5 File Offset: 0x001A1FA5
		public LeafFilter1(GrammarBuilders g, leafFExpr value0, selection2 value1)
		{
			this._node = g.Rule.LeafFilter1.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x06007BD7 RID: 31703 RVA: 0x001A3DD7 File Offset: 0x001A1FD7
		public static implicit operator filterSelection(LeafFilter1 arg)
		{
			return filterSelection.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001622 RID: 5666
		// (get) Token: 0x06007BD8 RID: 31704 RVA: 0x001A3DE5 File Offset: 0x001A1FE5
		public leafFExpr leafFExpr
		{
			get
			{
				return leafFExpr.CreateUnsafe(this.Node.Children[0].Children[0]);
			}
		}

		// Token: 0x17001623 RID: 5667
		// (get) Token: 0x06007BD9 RID: 31705 RVA: 0x001A3E00 File Offset: 0x001A2000
		public selection2 selection2
		{
			get
			{
				return selection2.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007BDA RID: 31706 RVA: 0x001A3E14 File Offset: 0x001A2014
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007BDB RID: 31707 RVA: 0x001A3E28 File Offset: 0x001A2028
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007BDC RID: 31708 RVA: 0x001A3E52 File Offset: 0x001A2052
		public bool Equals(LeafFilter1 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003368 RID: 13160
		private ProgramNode _node;
	}
}
