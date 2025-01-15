using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001384 RID: 4996
	public struct regex : IProgramNodeBuilder, IEquatable<regex>
	{
		// Token: 0x17001A9B RID: 6811
		// (get) Token: 0x06009B1B RID: 39707 RVA: 0x0020BEEE File Offset: 0x0020A0EE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009B1C RID: 39708 RVA: 0x0020BEF6 File Offset: 0x0020A0F6
		private regex(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009B1D RID: 39709 RVA: 0x0020BEFF File Offset: 0x0020A0FF
		public static regex CreateUnsafe(ProgramNode node)
		{
			return new regex(node);
		}

		// Token: 0x06009B1E RID: 39710 RVA: 0x0020BF08 File Offset: 0x0020A108
		public static regex? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.regex)
			{
				return null;
			}
			return new regex?(regex.CreateUnsafe(node));
		}

		// Token: 0x06009B1F RID: 39711 RVA: 0x0020BF42 File Offset: 0x0020A142
		public static regex CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new regex(new Hole(g.Symbol.regex, holeId));
		}

		// Token: 0x06009B20 RID: 39712 RVA: 0x0020BF5A File Offset: 0x0020A15A
		public regex(GrammarBuilders g, RegularExpression value)
		{
			this = new regex(new LiteralNode(g.Symbol.regex, value));
		}

		// Token: 0x17001A9C RID: 6812
		// (get) Token: 0x06009B21 RID: 39713 RVA: 0x0020BF73 File Offset: 0x0020A173
		public RegularExpression Value
		{
			get
			{
				return (RegularExpression)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06009B22 RID: 39714 RVA: 0x0020BF8A File Offset: 0x0020A18A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009B23 RID: 39715 RVA: 0x0020BFA0 File Offset: 0x0020A1A0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009B24 RID: 39716 RVA: 0x0020BFCA File Offset: 0x0020A1CA
		public bool Equals(regex other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DFB RID: 15867
		private ProgramNode _node;
	}
}
