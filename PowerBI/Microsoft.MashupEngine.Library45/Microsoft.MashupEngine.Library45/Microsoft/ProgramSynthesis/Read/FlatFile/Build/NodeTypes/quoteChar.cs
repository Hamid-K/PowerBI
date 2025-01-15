using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes
{
	// Token: 0x0200128A RID: 4746
	public struct quoteChar : IProgramNodeBuilder, IEquatable<quoteChar>
	{
		// Token: 0x170018B6 RID: 6326
		// (get) Token: 0x06008F9A RID: 36762 RVA: 0x001E2E1A File Offset: 0x001E101A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008F9B RID: 36763 RVA: 0x001E2E22 File Offset: 0x001E1022
		private quoteChar(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008F9C RID: 36764 RVA: 0x001E2E2B File Offset: 0x001E102B
		public static quoteChar CreateUnsafe(ProgramNode node)
		{
			return new quoteChar(node);
		}

		// Token: 0x06008F9D RID: 36765 RVA: 0x001E2E34 File Offset: 0x001E1034
		public static quoteChar? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.quoteChar)
			{
				return null;
			}
			return new quoteChar?(quoteChar.CreateUnsafe(node));
		}

		// Token: 0x06008F9E RID: 36766 RVA: 0x001E2E6E File Offset: 0x001E106E
		public static quoteChar CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new quoteChar(new Hole(g.Symbol.quoteChar, holeId));
		}

		// Token: 0x06008F9F RID: 36767 RVA: 0x001E2E86 File Offset: 0x001E1086
		public quoteChar(GrammarBuilders g, Optional<char> value)
		{
			this = new quoteChar(new LiteralNode(g.Symbol.quoteChar, value));
		}

		// Token: 0x170018B7 RID: 6327
		// (get) Token: 0x06008FA0 RID: 36768 RVA: 0x001E2EA4 File Offset: 0x001E10A4
		public Optional<char> Value
		{
			get
			{
				return (Optional<char>)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06008FA1 RID: 36769 RVA: 0x001E2EBB File Offset: 0x001E10BB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008FA2 RID: 36770 RVA: 0x001E2ED0 File Offset: 0x001E10D0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008FA3 RID: 36771 RVA: 0x001E2EFA File Offset: 0x001E10FA
		public bool Equals(quoteChar other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003A7B RID: 14971
		private ProgramNode _node;
	}
}
