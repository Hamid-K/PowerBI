using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001037 RID: 4151
	public struct DisjSubstring : IProgramNodeBuilder, IEquatable<DisjSubstring>
	{
		// Token: 0x170015DE RID: 5598
		// (get) Token: 0x06007ACF RID: 31439 RVA: 0x001A25EE File Offset: 0x001A07EE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007AD0 RID: 31440 RVA: 0x001A25F6 File Offset: 0x001A07F6
		private DisjSubstring(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007AD1 RID: 31441 RVA: 0x001A25FF File Offset: 0x001A07FF
		public static DisjSubstring CreateUnsafe(ProgramNode node)
		{
			return new DisjSubstring(node);
		}

		// Token: 0x06007AD2 RID: 31442 RVA: 0x001A2608 File Offset: 0x001A0808
		public static DisjSubstring? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.DisjSubstring)
			{
				return null;
			}
			return new DisjSubstring?(DisjSubstring.CreateUnsafe(node));
		}

		// Token: 0x06007AD3 RID: 31443 RVA: 0x001A263D File Offset: 0x001A083D
		public DisjSubstring(GrammarBuilders g, substringDisj value0, substring value1)
		{
			this._node = g.Rule.DisjSubstring.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007AD4 RID: 31444 RVA: 0x001A2663 File Offset: 0x001A0863
		public static implicit operator substringDisj(DisjSubstring arg)
		{
			return substringDisj.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015DF RID: 5599
		// (get) Token: 0x06007AD5 RID: 31445 RVA: 0x001A2671 File Offset: 0x001A0871
		public substringDisj substringDisj
		{
			get
			{
				return substringDisj.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170015E0 RID: 5600
		// (get) Token: 0x06007AD6 RID: 31446 RVA: 0x001A2685 File Offset: 0x001A0885
		public substring substring
		{
			get
			{
				return substring.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007AD7 RID: 31447 RVA: 0x001A2699 File Offset: 0x001A0899
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007AD8 RID: 31448 RVA: 0x001A26AC File Offset: 0x001A08AC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007AD9 RID: 31449 RVA: 0x001A26D6 File Offset: 0x001A08D6
		public bool Equals(DisjSubstring other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003350 RID: 13136
		private ProgramNode _node;
	}
}
