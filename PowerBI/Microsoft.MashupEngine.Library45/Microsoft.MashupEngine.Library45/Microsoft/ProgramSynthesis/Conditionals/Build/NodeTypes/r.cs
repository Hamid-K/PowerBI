using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes
{
	// Token: 0x02000A51 RID: 2641
	public struct r : IProgramNodeBuilder, IEquatable<r>
	{
		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06004175 RID: 16757 RVA: 0x000CD28E File Offset: 0x000CB48E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004176 RID: 16758 RVA: 0x000CD296 File Offset: 0x000CB496
		private r(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004177 RID: 16759 RVA: 0x000CD29F File Offset: 0x000CB49F
		public static r CreateUnsafe(ProgramNode node)
		{
			return new r(node);
		}

		// Token: 0x06004178 RID: 16760 RVA: 0x000CD2A8 File Offset: 0x000CB4A8
		public static r? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.r)
			{
				return null;
			}
			return new r?(r.CreateUnsafe(node));
		}

		// Token: 0x06004179 RID: 16761 RVA: 0x000CD2E2 File Offset: 0x000CB4E2
		public static r CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new r(new Hole(g.Symbol.r, holeId));
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x000CD2FA File Offset: 0x000CB4FA
		public r(GrammarBuilders g, RegularExpression value)
		{
			this = new r(new LiteralNode(g.Symbol.r, value));
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x0600417B RID: 16763 RVA: 0x000CD313 File Offset: 0x000CB513
		public RegularExpression Value
		{
			get
			{
				return (RegularExpression)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600417C RID: 16764 RVA: 0x000CD32A File Offset: 0x000CB52A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x000CD340 File Offset: 0x000CB540
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600417E RID: 16766 RVA: 0x000CD36A File Offset: 0x000CB56A
		public bool Equals(r other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D8C RID: 7564
		private ProgramNode _node;
	}
}
