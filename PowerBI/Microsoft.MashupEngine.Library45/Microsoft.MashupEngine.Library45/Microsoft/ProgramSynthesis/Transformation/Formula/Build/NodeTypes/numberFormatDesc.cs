using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015E3 RID: 5603
	public struct numberFormatDesc : IProgramNodeBuilder, IEquatable<numberFormatDesc>
	{
		// Token: 0x1700201D RID: 8221
		// (get) Token: 0x0600BA02 RID: 47618 RVA: 0x00281F96 File Offset: 0x00280196
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600BA03 RID: 47619 RVA: 0x00281F9E File Offset: 0x0028019E
		private numberFormatDesc(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600BA04 RID: 47620 RVA: 0x00281FA7 File Offset: 0x002801A7
		public static numberFormatDesc CreateUnsafe(ProgramNode node)
		{
			return new numberFormatDesc(node);
		}

		// Token: 0x0600BA05 RID: 47621 RVA: 0x00281FB0 File Offset: 0x002801B0
		public static numberFormatDesc? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.numberFormatDesc)
			{
				return null;
			}
			return new numberFormatDesc?(numberFormatDesc.CreateUnsafe(node));
		}

		// Token: 0x0600BA06 RID: 47622 RVA: 0x00281FEA File Offset: 0x002801EA
		public static numberFormatDesc CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new numberFormatDesc(new Hole(g.Symbol.numberFormatDesc, holeId));
		}

		// Token: 0x0600BA07 RID: 47623 RVA: 0x00282002 File Offset: 0x00280202
		public numberFormatDesc(GrammarBuilders g, FormatNumberDescriptor value)
		{
			this = new numberFormatDesc(new LiteralNode(g.Symbol.numberFormatDesc, value));
		}

		// Token: 0x1700201E RID: 8222
		// (get) Token: 0x0600BA08 RID: 47624 RVA: 0x0028201B File Offset: 0x0028021B
		public FormatNumberDescriptor Value
		{
			get
			{
				return (FormatNumberDescriptor)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600BA09 RID: 47625 RVA: 0x00282032 File Offset: 0x00280232
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600BA0A RID: 47626 RVA: 0x00282048 File Offset: 0x00280248
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600BA0B RID: 47627 RVA: 0x00282072 File Offset: 0x00280272
		public bool Equals(numberFormatDesc other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004691 RID: 18065
		private ProgramNode _node;
	}
}
