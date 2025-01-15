using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes
{
	// Token: 0x02001285 RID: 4741
	public struct skipFooter : IProgramNodeBuilder, IEquatable<skipFooter>
	{
		// Token: 0x170018AC RID: 6316
		// (get) Token: 0x06008F68 RID: 36712 RVA: 0x001E295E File Offset: 0x001E0B5E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008F69 RID: 36713 RVA: 0x001E2966 File Offset: 0x001E0B66
		private skipFooter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008F6A RID: 36714 RVA: 0x001E296F File Offset: 0x001E0B6F
		public static skipFooter CreateUnsafe(ProgramNode node)
		{
			return new skipFooter(node);
		}

		// Token: 0x06008F6B RID: 36715 RVA: 0x001E2978 File Offset: 0x001E0B78
		public static skipFooter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.skipFooter)
			{
				return null;
			}
			return new skipFooter?(skipFooter.CreateUnsafe(node));
		}

		// Token: 0x06008F6C RID: 36716 RVA: 0x001E29B2 File Offset: 0x001E0BB2
		public static skipFooter CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new skipFooter(new Hole(g.Symbol.skipFooter, holeId));
		}

		// Token: 0x06008F6D RID: 36717 RVA: 0x001E29CA File Offset: 0x001E0BCA
		public skipFooter(GrammarBuilders g, int value)
		{
			this = new skipFooter(new LiteralNode(g.Symbol.skipFooter, value));
		}

		// Token: 0x170018AD RID: 6317
		// (get) Token: 0x06008F6E RID: 36718 RVA: 0x001E29E8 File Offset: 0x001E0BE8
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06008F6F RID: 36719 RVA: 0x001E29FF File Offset: 0x001E0BFF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008F70 RID: 36720 RVA: 0x001E2A14 File Offset: 0x001E0C14
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008F71 RID: 36721 RVA: 0x001E2A3E File Offset: 0x001E0C3E
		public bool Equals(skipFooter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003A76 RID: 14966
		private ProgramNode _node;
	}
}
