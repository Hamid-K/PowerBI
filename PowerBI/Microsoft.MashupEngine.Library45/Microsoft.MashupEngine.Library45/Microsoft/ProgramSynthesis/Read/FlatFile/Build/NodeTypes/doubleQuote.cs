using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes
{
	// Token: 0x0200128C RID: 4748
	public struct doubleQuote : IProgramNodeBuilder, IEquatable<doubleQuote>
	{
		// Token: 0x170018BA RID: 6330
		// (get) Token: 0x06008FAE RID: 36782 RVA: 0x001E3002 File Offset: 0x001E1202
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008FAF RID: 36783 RVA: 0x001E300A File Offset: 0x001E120A
		private doubleQuote(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008FB0 RID: 36784 RVA: 0x001E3013 File Offset: 0x001E1213
		public static doubleQuote CreateUnsafe(ProgramNode node)
		{
			return new doubleQuote(node);
		}

		// Token: 0x06008FB1 RID: 36785 RVA: 0x001E301C File Offset: 0x001E121C
		public static doubleQuote? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.doubleQuote)
			{
				return null;
			}
			return new doubleQuote?(doubleQuote.CreateUnsafe(node));
		}

		// Token: 0x06008FB2 RID: 36786 RVA: 0x001E3056 File Offset: 0x001E1256
		public static doubleQuote CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new doubleQuote(new Hole(g.Symbol.doubleQuote, holeId));
		}

		// Token: 0x06008FB3 RID: 36787 RVA: 0x001E306E File Offset: 0x001E126E
		public doubleQuote(GrammarBuilders g, bool value)
		{
			this = new doubleQuote(new LiteralNode(g.Symbol.doubleQuote, value));
		}

		// Token: 0x170018BB RID: 6331
		// (get) Token: 0x06008FB4 RID: 36788 RVA: 0x001E308C File Offset: 0x001E128C
		public bool Value
		{
			get
			{
				return (bool)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06008FB5 RID: 36789 RVA: 0x001E30A3 File Offset: 0x001E12A3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008FB6 RID: 36790 RVA: 0x001E30B8 File Offset: 0x001E12B8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008FB7 RID: 36791 RVA: 0x001E30E2 File Offset: 0x001E12E2
		public bool Equals(doubleQuote other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003A7D RID: 14973
		private ProgramNode _node;
	}
}
