using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C6A RID: 7274
	public struct numberFormatSeparatorChar : IProgramNodeBuilder, IEquatable<numberFormatSeparatorChar>
	{
		// Token: 0x1700290C RID: 10508
		// (get) Token: 0x0600F65C RID: 63068 RVA: 0x0034921A File Offset: 0x0034741A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F65D RID: 63069 RVA: 0x00349222 File Offset: 0x00347422
		private numberFormatSeparatorChar(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F65E RID: 63070 RVA: 0x0034922B File Offset: 0x0034742B
		public static numberFormatSeparatorChar CreateUnsafe(ProgramNode node)
		{
			return new numberFormatSeparatorChar(node);
		}

		// Token: 0x0600F65F RID: 63071 RVA: 0x00349234 File Offset: 0x00347434
		public static numberFormatSeparatorChar? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.numberFormatSeparatorChar)
			{
				return null;
			}
			return new numberFormatSeparatorChar?(numberFormatSeparatorChar.CreateUnsafe(node));
		}

		// Token: 0x0600F660 RID: 63072 RVA: 0x0034926E File Offset: 0x0034746E
		public static numberFormatSeparatorChar CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new numberFormatSeparatorChar(new Hole(g.Symbol.numberFormatSeparatorChar, holeId));
		}

		// Token: 0x0600F661 RID: 63073 RVA: 0x00349286 File Offset: 0x00347486
		public numberFormatSeparatorChar(GrammarBuilders g, char? value)
		{
			this = new numberFormatSeparatorChar(new LiteralNode(g.Symbol.numberFormatSeparatorChar, value));
		}

		// Token: 0x1700290D RID: 10509
		// (get) Token: 0x0600F662 RID: 63074 RVA: 0x003492A4 File Offset: 0x003474A4
		public char? Value
		{
			get
			{
				return (char?)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600F663 RID: 63075 RVA: 0x003492BB File Offset: 0x003474BB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F664 RID: 63076 RVA: 0x003492D0 File Offset: 0x003474D0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F665 RID: 63077 RVA: 0x003492FA File Offset: 0x003474FA
		public bool Equals(numberFormatSeparatorChar other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B59 RID: 23385
		private ProgramNode _node;
	}
}
