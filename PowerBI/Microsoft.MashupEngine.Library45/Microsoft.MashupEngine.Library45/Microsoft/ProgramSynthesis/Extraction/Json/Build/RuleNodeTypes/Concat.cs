using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes
{
	// Token: 0x02000B5C RID: 2908
	public struct Concat : IProgramNodeBuilder, IEquatable<Concat>
	{
		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x0600495C RID: 18780 RVA: 0x000E7C32 File Offset: 0x000E5E32
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600495D RID: 18781 RVA: 0x000E7C3A File Offset: 0x000E5E3A
		private Concat(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600495E RID: 18782 RVA: 0x000E7C43 File Offset: 0x000E5E43
		public static Concat CreateUnsafe(ProgramNode node)
		{
			return new Concat(node);
		}

		// Token: 0x0600495F RID: 18783 RVA: 0x000E7C4C File Offset: 0x000E5E4C
		public static Concat? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Concat)
			{
				return null;
			}
			return new Concat?(Concat.CreateUnsafe(node));
		}

		// Token: 0x06004960 RID: 18784 RVA: 0x000E7C81 File Offset: 0x000E5E81
		public Concat(GrammarBuilders g, output value0, structBodyRec value1)
		{
			this._node = g.Rule.Concat.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06004961 RID: 18785 RVA: 0x000E7CA7 File Offset: 0x000E5EA7
		public static implicit operator structBodyRec(Concat arg)
		{
			return structBodyRec.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06004962 RID: 18786 RVA: 0x000E7CB5 File Offset: 0x000E5EB5
		public output output
		{
			get
			{
				return output.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06004963 RID: 18787 RVA: 0x000E7CC9 File Offset: 0x000E5EC9
		public structBodyRec structBodyRec
		{
			get
			{
				return structBodyRec.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06004964 RID: 18788 RVA: 0x000E7CDD File Offset: 0x000E5EDD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004965 RID: 18789 RVA: 0x000E7CF0 File Offset: 0x000E5EF0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004966 RID: 18790 RVA: 0x000E7D1A File Offset: 0x000E5F1A
		public bool Equals(Concat other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002157 RID: 8535
		private ProgramNode _node;
	}
}
