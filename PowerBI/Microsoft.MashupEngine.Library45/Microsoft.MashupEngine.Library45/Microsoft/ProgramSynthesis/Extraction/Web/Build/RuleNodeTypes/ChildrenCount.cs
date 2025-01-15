using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200102D RID: 4141
	public struct ChildrenCount : IProgramNodeBuilder, IEquatable<ChildrenCount>
	{
		// Token: 0x170015C0 RID: 5568
		// (get) Token: 0x06007A61 RID: 31329 RVA: 0x001A1C06 File Offset: 0x0019FE06
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007A62 RID: 31330 RVA: 0x001A1C0E File Offset: 0x0019FE0E
		private ChildrenCount(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007A63 RID: 31331 RVA: 0x001A1C17 File Offset: 0x0019FE17
		public static ChildrenCount CreateUnsafe(ProgramNode node)
		{
			return new ChildrenCount(node);
		}

		// Token: 0x06007A64 RID: 31332 RVA: 0x001A1C20 File Offset: 0x0019FE20
		public static ChildrenCount? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ChildrenCount)
			{
				return null;
			}
			return new ChildrenCount?(ChildrenCount.CreateUnsafe(node));
		}

		// Token: 0x06007A65 RID: 31333 RVA: 0x001A1C55 File Offset: 0x0019FE55
		public ChildrenCount(GrammarBuilders g, count value0, node value1)
		{
			this._node = g.Rule.ChildrenCount.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007A66 RID: 31334 RVA: 0x001A1C7B File Offset: 0x0019FE7B
		public static implicit operator atomExpr(ChildrenCount arg)
		{
			return atomExpr.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015C1 RID: 5569
		// (get) Token: 0x06007A67 RID: 31335 RVA: 0x001A1C89 File Offset: 0x0019FE89
		public count count
		{
			get
			{
				return count.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170015C2 RID: 5570
		// (get) Token: 0x06007A68 RID: 31336 RVA: 0x001A1C9D File Offset: 0x0019FE9D
		public node node
		{
			get
			{
				return node.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007A69 RID: 31337 RVA: 0x001A1CB1 File Offset: 0x0019FEB1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007A6A RID: 31338 RVA: 0x001A1CC4 File Offset: 0x0019FEC4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007A6B RID: 31339 RVA: 0x001A1CEE File Offset: 0x0019FEEE
		public bool Equals(ChildrenCount other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003346 RID: 13126
		private ProgramNode _node;
	}
}
