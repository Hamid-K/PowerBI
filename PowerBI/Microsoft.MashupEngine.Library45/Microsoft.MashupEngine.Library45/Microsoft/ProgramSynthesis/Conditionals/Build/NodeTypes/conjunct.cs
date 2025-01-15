using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes
{
	// Token: 0x02000A4D RID: 2637
	public struct conjunct : IProgramNodeBuilder, IEquatable<conjunct>
	{
		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x060040FF RID: 16639 RVA: 0x000CBA36 File Offset: 0x000C9C36
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004100 RID: 16640 RVA: 0x000CBA3E File Offset: 0x000C9C3E
		private conjunct(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004101 RID: 16641 RVA: 0x000CBA47 File Offset: 0x000C9C47
		public static conjunct CreateUnsafe(ProgramNode node)
		{
			return new conjunct(node);
		}

		// Token: 0x06004102 RID: 16642 RVA: 0x000CBA50 File Offset: 0x000C9C50
		public static conjunct? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.conjunct)
			{
				return null;
			}
			return new conjunct?(conjunct.CreateUnsafe(node));
		}

		// Token: 0x06004103 RID: 16643 RVA: 0x000CBA8A File Offset: 0x000C9C8A
		public static conjunct CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new conjunct(new Hole(g.Symbol.conjunct, holeId));
		}

		// Token: 0x06004104 RID: 16644 RVA: 0x000CBAA2 File Offset: 0x000C9CA2
		public Conjunct Cast_Conjunct()
		{
			return Conjunct.CreateUnsafe(this.Node);
		}

		// Token: 0x06004105 RID: 16645 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Conjunct(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06004106 RID: 16646 RVA: 0x000CBAAF File Offset: 0x000C9CAF
		public bool Is_Conjunct(GrammarBuilders g, out Conjunct value)
		{
			value = Conjunct.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06004107 RID: 16647 RVA: 0x000CBAC3 File Offset: 0x000C9CC3
		public Conjunct? As_Conjunct(GrammarBuilders g)
		{
			return new Conjunct?(Conjunct.CreateUnsafe(this.Node));
		}

		// Token: 0x06004108 RID: 16648 RVA: 0x000CBAD5 File Offset: 0x000C9CD5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004109 RID: 16649 RVA: 0x000CBAE8 File Offset: 0x000C9CE8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600410A RID: 16650 RVA: 0x000CBB12 File Offset: 0x000C9D12
		public bool Equals(conjunct other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D88 RID: 7560
		private ProgramNode _node;
	}
}
