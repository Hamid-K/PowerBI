using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015E2 RID: 5602
	public struct sliceBetweenEndText : IProgramNodeBuilder, IEquatable<sliceBetweenEndText>
	{
		// Token: 0x1700201B RID: 8219
		// (get) Token: 0x0600B9F8 RID: 47608 RVA: 0x00281EA6 File Offset: 0x002800A6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B9F9 RID: 47609 RVA: 0x00281EAE File Offset: 0x002800AE
		private sliceBetweenEndText(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B9FA RID: 47610 RVA: 0x00281EB7 File Offset: 0x002800B7
		public static sliceBetweenEndText CreateUnsafe(ProgramNode node)
		{
			return new sliceBetweenEndText(node);
		}

		// Token: 0x0600B9FB RID: 47611 RVA: 0x00281EC0 File Offset: 0x002800C0
		public static sliceBetweenEndText? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.sliceBetweenEndText)
			{
				return null;
			}
			return new sliceBetweenEndText?(sliceBetweenEndText.CreateUnsafe(node));
		}

		// Token: 0x0600B9FC RID: 47612 RVA: 0x00281EFA File Offset: 0x002800FA
		public static sliceBetweenEndText CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new sliceBetweenEndText(new Hole(g.Symbol.sliceBetweenEndText, holeId));
		}

		// Token: 0x0600B9FD RID: 47613 RVA: 0x00281F12 File Offset: 0x00280112
		public sliceBetweenEndText(GrammarBuilders g, string value)
		{
			this = new sliceBetweenEndText(new LiteralNode(g.Symbol.sliceBetweenEndText, value));
		}

		// Token: 0x1700201C RID: 8220
		// (get) Token: 0x0600B9FE RID: 47614 RVA: 0x00281F2B File Offset: 0x0028012B
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600B9FF RID: 47615 RVA: 0x00281F42 File Offset: 0x00280142
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600BA00 RID: 47616 RVA: 0x00281F58 File Offset: 0x00280158
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600BA01 RID: 47617 RVA: 0x00281F82 File Offset: 0x00280182
		public bool Equals(sliceBetweenEndText other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004690 RID: 18064
		private ProgramNode _node;
	}
}
