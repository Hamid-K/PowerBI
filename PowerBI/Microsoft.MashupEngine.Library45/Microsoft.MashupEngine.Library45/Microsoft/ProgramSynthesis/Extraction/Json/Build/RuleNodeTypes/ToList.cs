using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes
{
	// Token: 0x02000B5D RID: 2909
	public struct ToList : IProgramNodeBuilder, IEquatable<ToList>
	{
		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06004967 RID: 18791 RVA: 0x000E7D2E File Offset: 0x000E5F2E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004968 RID: 18792 RVA: 0x000E7D36 File Offset: 0x000E5F36
		private ToList(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004969 RID: 18793 RVA: 0x000E7D3F File Offset: 0x000E5F3F
		public static ToList CreateUnsafe(ProgramNode node)
		{
			return new ToList(node);
		}

		// Token: 0x0600496A RID: 18794 RVA: 0x000E7D48 File Offset: 0x000E5F48
		public static ToList? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ToList)
			{
				return null;
			}
			return new ToList?(ToList.CreateUnsafe(node));
		}

		// Token: 0x0600496B RID: 18795 RVA: 0x000E7D7D File Offset: 0x000E5F7D
		public ToList(GrammarBuilders g, output value0)
		{
			this._node = g.Rule.ToList.BuildASTNode(value0.Node);
		}

		// Token: 0x0600496C RID: 18796 RVA: 0x000E7D9C File Offset: 0x000E5F9C
		public static implicit operator structBodyRec(ToList arg)
		{
			return structBodyRec.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x0600496D RID: 18797 RVA: 0x000E7DAA File Offset: 0x000E5FAA
		public output output
		{
			get
			{
				return output.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600496E RID: 18798 RVA: 0x000E7DBE File Offset: 0x000E5FBE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600496F RID: 18799 RVA: 0x000E7DD4 File Offset: 0x000E5FD4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004970 RID: 18800 RVA: 0x000E7DFE File Offset: 0x000E5FFE
		public bool Equals(ToList other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002158 RID: 8536
		private ProgramNode _node;
	}
}
