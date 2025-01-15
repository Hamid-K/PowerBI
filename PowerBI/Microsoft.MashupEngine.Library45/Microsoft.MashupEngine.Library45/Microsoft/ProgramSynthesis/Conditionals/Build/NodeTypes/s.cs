using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes
{
	// Token: 0x02000A54 RID: 2644
	public struct s : IProgramNodeBuilder, IEquatable<s>
	{
		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06004193 RID: 16787 RVA: 0x000CD562 File Offset: 0x000CB762
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004194 RID: 16788 RVA: 0x000CD56A File Offset: 0x000CB76A
		private s(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004195 RID: 16789 RVA: 0x000CD573 File Offset: 0x000CB773
		public static s CreateUnsafe(ProgramNode node)
		{
			return new s(node);
		}

		// Token: 0x06004196 RID: 16790 RVA: 0x000CD57C File Offset: 0x000CB77C
		public static s? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.s)
			{
				return null;
			}
			return new s?(s.CreateUnsafe(node));
		}

		// Token: 0x06004197 RID: 16791 RVA: 0x000CD5B6 File Offset: 0x000CB7B6
		public static s CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new s(new Hole(g.Symbol.s, holeId));
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x000CD5CE File Offset: 0x000CB7CE
		public s(GrammarBuilders g)
		{
			this = new s(new VariableNode(g.Symbol.s));
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06004199 RID: 16793 RVA: 0x000CD5E6 File Offset: 0x000CB7E6
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x0600419A RID: 16794 RVA: 0x000CD5F3 File Offset: 0x000CB7F3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600419B RID: 16795 RVA: 0x000CD608 File Offset: 0x000CB808
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600419C RID: 16796 RVA: 0x000CD632 File Offset: 0x000CB832
		public bool Equals(s other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D8F RID: 7567
		private ProgramNode _node;
	}
}
