using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000974 RID: 2420
	public struct sep : IProgramNodeBuilder, IEquatable<sep>
	{
		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x060039AE RID: 14766 RVA: 0x000B2666 File Offset: 0x000B0866
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060039AF RID: 14767 RVA: 0x000B266E File Offset: 0x000B086E
		private sep(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060039B0 RID: 14768 RVA: 0x000B2677 File Offset: 0x000B0877
		public static sep CreateUnsafe(ProgramNode node)
		{
			return new sep(node);
		}

		// Token: 0x060039B1 RID: 14769 RVA: 0x000B2680 File Offset: 0x000B0880
		public static sep? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.sep)
			{
				return null;
			}
			return new sep?(sep.CreateUnsafe(node));
		}

		// Token: 0x060039B2 RID: 14770 RVA: 0x000B26BA File Offset: 0x000B08BA
		public static sep CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new sep(new Hole(g.Symbol.sep, holeId));
		}

		// Token: 0x060039B3 RID: 14771 RVA: 0x000B26D2 File Offset: 0x000B08D2
		public sep(GrammarBuilders g, string value)
		{
			this = new sep(new LiteralNode(g.Symbol.sep, value));
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x060039B4 RID: 14772 RVA: 0x000B26EB File Offset: 0x000B08EB
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x060039B5 RID: 14773 RVA: 0x000B2702 File Offset: 0x000B0902
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060039B6 RID: 14774 RVA: 0x000B2718 File Offset: 0x000B0918
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x000B2742 File Offset: 0x000B0942
		public bool Equals(sep other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A94 RID: 6804
		private ProgramNode _node;
	}
}
