using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Semantics;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes
{
	// Token: 0x020011FC RID: 4604
	public struct token : IProgramNodeBuilder, IEquatable<token>
	{
		// Token: 0x170017CA RID: 6090
		// (get) Token: 0x06008AC8 RID: 35528 RVA: 0x001D1916 File Offset: 0x001CFB16
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008AC9 RID: 35529 RVA: 0x001D191E File Offset: 0x001CFB1E
		private token(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008ACA RID: 35530 RVA: 0x001D1927 File Offset: 0x001CFB27
		public static token CreateUnsafe(ProgramNode node)
		{
			return new token(node);
		}

		// Token: 0x06008ACB RID: 35531 RVA: 0x001D1930 File Offset: 0x001CFB30
		public static token? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.token)
			{
				return null;
			}
			return new token?(token.CreateUnsafe(node));
		}

		// Token: 0x06008ACC RID: 35532 RVA: 0x001D196A File Offset: 0x001CFB6A
		public static token CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new token(new Hole(g.Symbol.token, holeId));
		}

		// Token: 0x06008ACD RID: 35533 RVA: 0x001D1982 File Offset: 0x001CFB82
		public token(GrammarBuilders g, IToken value)
		{
			this = new token(new LiteralNode(g.Symbol.token, value));
		}

		// Token: 0x170017CB RID: 6091
		// (get) Token: 0x06008ACE RID: 35534 RVA: 0x001D199B File Offset: 0x001CFB9B
		public IToken Value
		{
			get
			{
				return (IToken)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06008ACF RID: 35535 RVA: 0x001D19B2 File Offset: 0x001CFBB2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008AD0 RID: 35536 RVA: 0x001D19C8 File Offset: 0x001CFBC8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008AD1 RID: 35537 RVA: 0x001D19F2 File Offset: 0x001CFBF2
		public bool Equals(token other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040038B0 RID: 14512
		private ProgramNode _node;
	}
}
