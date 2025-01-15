using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200102A RID: 4138
	public struct NthChild : IProgramNodeBuilder, IEquatable<NthChild>
	{
		// Token: 0x170015B7 RID: 5559
		// (get) Token: 0x06007A40 RID: 31296 RVA: 0x001A1912 File Offset: 0x0019FB12
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007A41 RID: 31297 RVA: 0x001A191A File Offset: 0x0019FB1A
		private NthChild(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007A42 RID: 31298 RVA: 0x001A1923 File Offset: 0x0019FB23
		public static NthChild CreateUnsafe(ProgramNode node)
		{
			return new NthChild(node);
		}

		// Token: 0x06007A43 RID: 31299 RVA: 0x001A192C File Offset: 0x0019FB2C
		public static NthChild? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.NthChild)
			{
				return null;
			}
			return new NthChild?(NthChild.CreateUnsafe(node));
		}

		// Token: 0x06007A44 RID: 31300 RVA: 0x001A1961 File Offset: 0x0019FB61
		public NthChild(GrammarBuilders g, idx1 value0, node value1)
		{
			this._node = g.Rule.NthChild.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007A45 RID: 31301 RVA: 0x001A1987 File Offset: 0x0019FB87
		public static implicit operator atomExpr(NthChild arg)
		{
			return atomExpr.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015B8 RID: 5560
		// (get) Token: 0x06007A46 RID: 31302 RVA: 0x001A1995 File Offset: 0x0019FB95
		public idx1 idx1
		{
			get
			{
				return idx1.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170015B9 RID: 5561
		// (get) Token: 0x06007A47 RID: 31303 RVA: 0x001A19A9 File Offset: 0x0019FBA9
		public node node
		{
			get
			{
				return node.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007A48 RID: 31304 RVA: 0x001A19BD File Offset: 0x0019FBBD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007A49 RID: 31305 RVA: 0x001A19D0 File Offset: 0x0019FBD0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007A4A RID: 31306 RVA: 0x001A19FA File Offset: 0x0019FBFA
		public bool Equals(NthChild other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003343 RID: 13123
		private ProgramNode _node;
	}
}
