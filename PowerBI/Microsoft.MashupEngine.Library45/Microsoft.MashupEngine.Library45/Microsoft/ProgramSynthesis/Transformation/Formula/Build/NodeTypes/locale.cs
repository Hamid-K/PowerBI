using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015DE RID: 5598
	public struct locale : IProgramNodeBuilder, IEquatable<locale>
	{
		// Token: 0x17002013 RID: 8211
		// (get) Token: 0x0600B9D0 RID: 47568 RVA: 0x00281AE6 File Offset: 0x0027FCE6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B9D1 RID: 47569 RVA: 0x00281AEE File Offset: 0x0027FCEE
		private locale(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B9D2 RID: 47570 RVA: 0x00281AF7 File Offset: 0x0027FCF7
		public static locale CreateUnsafe(ProgramNode node)
		{
			return new locale(node);
		}

		// Token: 0x0600B9D3 RID: 47571 RVA: 0x00281B00 File Offset: 0x0027FD00
		public static locale? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.locale)
			{
				return null;
			}
			return new locale?(locale.CreateUnsafe(node));
		}

		// Token: 0x0600B9D4 RID: 47572 RVA: 0x00281B3A File Offset: 0x0027FD3A
		public static locale CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new locale(new Hole(g.Symbol.locale, holeId));
		}

		// Token: 0x0600B9D5 RID: 47573 RVA: 0x00281B52 File Offset: 0x0027FD52
		public locale(GrammarBuilders g, string value)
		{
			this = new locale(new LiteralNode(g.Symbol.locale, value));
		}

		// Token: 0x17002014 RID: 8212
		// (get) Token: 0x0600B9D6 RID: 47574 RVA: 0x00281B6B File Offset: 0x0027FD6B
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600B9D7 RID: 47575 RVA: 0x00281B82 File Offset: 0x0027FD82
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B9D8 RID: 47576 RVA: 0x00281B98 File Offset: 0x0027FD98
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B9D9 RID: 47577 RVA: 0x00281BC2 File Offset: 0x0027FDC2
		public bool Equals(locale other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400468C RID: 18060
		private ProgramNode _node;
	}
}
