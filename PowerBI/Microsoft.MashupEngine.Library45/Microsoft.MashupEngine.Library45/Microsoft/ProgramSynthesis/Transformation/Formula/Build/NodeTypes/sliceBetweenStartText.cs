using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015E1 RID: 5601
	public struct sliceBetweenStartText : IProgramNodeBuilder, IEquatable<sliceBetweenStartText>
	{
		// Token: 0x17002019 RID: 8217
		// (get) Token: 0x0600B9EE RID: 47598 RVA: 0x00281DB6 File Offset: 0x0027FFB6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B9EF RID: 47599 RVA: 0x00281DBE File Offset: 0x0027FFBE
		private sliceBetweenStartText(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B9F0 RID: 47600 RVA: 0x00281DC7 File Offset: 0x0027FFC7
		public static sliceBetweenStartText CreateUnsafe(ProgramNode node)
		{
			return new sliceBetweenStartText(node);
		}

		// Token: 0x0600B9F1 RID: 47601 RVA: 0x00281DD0 File Offset: 0x0027FFD0
		public static sliceBetweenStartText? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.sliceBetweenStartText)
			{
				return null;
			}
			return new sliceBetweenStartText?(sliceBetweenStartText.CreateUnsafe(node));
		}

		// Token: 0x0600B9F2 RID: 47602 RVA: 0x00281E0A File Offset: 0x0028000A
		public static sliceBetweenStartText CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new sliceBetweenStartText(new Hole(g.Symbol.sliceBetweenStartText, holeId));
		}

		// Token: 0x0600B9F3 RID: 47603 RVA: 0x00281E22 File Offset: 0x00280022
		public sliceBetweenStartText(GrammarBuilders g, string value)
		{
			this = new sliceBetweenStartText(new LiteralNode(g.Symbol.sliceBetweenStartText, value));
		}

		// Token: 0x1700201A RID: 8218
		// (get) Token: 0x0600B9F4 RID: 47604 RVA: 0x00281E3B File Offset: 0x0028003B
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600B9F5 RID: 47605 RVA: 0x00281E52 File Offset: 0x00280052
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B9F6 RID: 47606 RVA: 0x00281E68 File Offset: 0x00280068
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B9F7 RID: 47607 RVA: 0x00281E92 File Offset: 0x00280092
		public bool Equals(sliceBetweenStartText other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400468F RID: 18063
		private ProgramNode _node;
	}
}
