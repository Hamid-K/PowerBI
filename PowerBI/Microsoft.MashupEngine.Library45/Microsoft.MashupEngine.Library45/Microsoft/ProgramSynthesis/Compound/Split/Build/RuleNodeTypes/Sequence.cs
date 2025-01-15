using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x02000953 RID: 2387
	public struct Sequence : IProgramNodeBuilder, IEquatable<Sequence>
	{
		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x060037D8 RID: 14296 RVA: 0x000AE782 File Offset: 0x000AC982
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x000AE78A File Offset: 0x000AC98A
		private Sequence(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x000AE793 File Offset: 0x000AC993
		public static Sequence CreateUnsafe(ProgramNode node)
		{
			return new Sequence(node);
		}

		// Token: 0x060037DB RID: 14299 RVA: 0x000AE79C File Offset: 0x000AC99C
		public static Sequence? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Sequence)
			{
				return null;
			}
			return new Sequence?(Sequence.CreateUnsafe(node));
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x000AE7D1 File Offset: 0x000AC9D1
		public Sequence(GrammarBuilders g, ls value0)
		{
			this._node = g.Rule.Sequence.BuildASTNode(value0.Node);
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x000AE7F0 File Offset: 0x000AC9F0
		public static implicit operator splitSequence(Sequence arg)
		{
			return splitSequence.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x060037DE RID: 14302 RVA: 0x000AE7FE File Offset: 0x000AC9FE
		public ls ls
		{
			get
			{
				return ls.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x000AE812 File Offset: 0x000ACA12
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x000AE828 File Offset: 0x000ACA28
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x000AE852 File Offset: 0x000ACA52
		public bool Equals(Sequence other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A73 RID: 6771
		private ProgramNode _node;
	}
}
