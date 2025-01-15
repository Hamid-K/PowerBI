using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E70 RID: 3696
	public struct k : IProgramNodeBuilder, IEquatable<k>
	{
		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x060064C5 RID: 25797 RVA: 0x00146F8E File Offset: 0x0014518E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060064C6 RID: 25798 RVA: 0x00146F96 File Offset: 0x00145196
		private k(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060064C7 RID: 25799 RVA: 0x00146F9F File Offset: 0x0014519F
		public static k CreateUnsafe(ProgramNode node)
		{
			return new k(node);
		}

		// Token: 0x060064C8 RID: 25800 RVA: 0x00146FA8 File Offset: 0x001451A8
		public static k? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.k)
			{
				return null;
			}
			return new k?(k.CreateUnsafe(node));
		}

		// Token: 0x060064C9 RID: 25801 RVA: 0x00146FE2 File Offset: 0x001451E2
		public static k CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new k(new Hole(g.Symbol.k, holeId));
		}

		// Token: 0x060064CA RID: 25802 RVA: 0x00146FFA File Offset: 0x001451FA
		public k(GrammarBuilders g, int value)
		{
			this = new k(new LiteralNode(g.Symbol.k, value));
		}

		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x060064CB RID: 25803 RVA: 0x00147018 File Offset: 0x00145218
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x060064CC RID: 25804 RVA: 0x0014702F File Offset: 0x0014522F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060064CD RID: 25805 RVA: 0x00147044 File Offset: 0x00145244
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060064CE RID: 25806 RVA: 0x0014706E File Offset: 0x0014526E
		public bool Equals(k other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C1A RID: 11290
		private ProgramNode _node;
	}
}
