using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E6F RID: 3695
	public struct rangeName : IProgramNodeBuilder, IEquatable<rangeName>
	{
		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x060064BB RID: 25787 RVA: 0x00146E9E File Offset: 0x0014509E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060064BC RID: 25788 RVA: 0x00146EA6 File Offset: 0x001450A6
		private rangeName(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060064BD RID: 25789 RVA: 0x00146EAF File Offset: 0x001450AF
		public static rangeName CreateUnsafe(ProgramNode node)
		{
			return new rangeName(node);
		}

		// Token: 0x060064BE RID: 25790 RVA: 0x00146EB8 File Offset: 0x001450B8
		public static rangeName? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.rangeName)
			{
				return null;
			}
			return new rangeName?(rangeName.CreateUnsafe(node));
		}

		// Token: 0x060064BF RID: 25791 RVA: 0x00146EF2 File Offset: 0x001450F2
		public static rangeName CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new rangeName(new Hole(g.Symbol.rangeName, holeId));
		}

		// Token: 0x060064C0 RID: 25792 RVA: 0x00146F0A File Offset: 0x0014510A
		public rangeName(GrammarBuilders g, string value)
		{
			this = new rangeName(new LiteralNode(g.Symbol.rangeName, value));
		}

		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x060064C1 RID: 25793 RVA: 0x00146F23 File Offset: 0x00145123
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x060064C2 RID: 25794 RVA: 0x00146F3A File Offset: 0x0014513A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060064C3 RID: 25795 RVA: 0x00146F50 File Offset: 0x00145150
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060064C4 RID: 25796 RVA: 0x00146F7A File Offset: 0x0014517A
		public bool Equals(rangeName other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C19 RID: 11289
		private ProgramNode _node;
	}
}
