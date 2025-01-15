using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes
{
	// Token: 0x02000B5E RID: 2910
	public struct Empty : IProgramNodeBuilder, IEquatable<Empty>
	{
		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06004971 RID: 18801 RVA: 0x000E7E12 File Offset: 0x000E6012
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004972 RID: 18802 RVA: 0x000E7E1A File Offset: 0x000E601A
		private Empty(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004973 RID: 18803 RVA: 0x000E7E23 File Offset: 0x000E6023
		public static Empty CreateUnsafe(ProgramNode node)
		{
			return new Empty(node);
		}

		// Token: 0x06004974 RID: 18804 RVA: 0x000E7E2C File Offset: 0x000E602C
		public static Empty? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Empty)
			{
				return null;
			}
			return new Empty?(Empty.CreateUnsafe(node));
		}

		// Token: 0x06004975 RID: 18805 RVA: 0x000E7E61 File Offset: 0x000E6061
		public Empty(GrammarBuilders g)
		{
			this._node = g.Rule.Empty.BuildASTNode(Array.Empty<ProgramNode>());
		}

		// Token: 0x06004976 RID: 18806 RVA: 0x000E7E7E File Offset: 0x000E607E
		public static implicit operator structBodyRec(Empty arg)
		{
			return structBodyRec.CreateUnsafe(arg.Node);
		}

		// Token: 0x06004977 RID: 18807 RVA: 0x000E7E8C File Offset: 0x000E608C
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004978 RID: 18808 RVA: 0x000E7EA0 File Offset: 0x000E60A0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004979 RID: 18809 RVA: 0x000E7ECA File Offset: 0x000E60CA
		public bool Equals(Empty other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002159 RID: 8537
		private ProgramNode _node;
	}
}
