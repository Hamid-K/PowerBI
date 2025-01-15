using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015D7 RID: 5591
	public struct numberGreaterThanValue : IProgramNodeBuilder, IEquatable<numberGreaterThanValue>
	{
		// Token: 0x17002005 RID: 8197
		// (get) Token: 0x0600B98A RID: 47498 RVA: 0x00281446 File Offset: 0x0027F646
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B98B RID: 47499 RVA: 0x0028144E File Offset: 0x0027F64E
		private numberGreaterThanValue(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B98C RID: 47500 RVA: 0x00281457 File Offset: 0x0027F657
		public static numberGreaterThanValue CreateUnsafe(ProgramNode node)
		{
			return new numberGreaterThanValue(node);
		}

		// Token: 0x0600B98D RID: 47501 RVA: 0x00281460 File Offset: 0x0027F660
		public static numberGreaterThanValue? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.numberGreaterThanValue)
			{
				return null;
			}
			return new numberGreaterThanValue?(numberGreaterThanValue.CreateUnsafe(node));
		}

		// Token: 0x0600B98E RID: 47502 RVA: 0x0028149A File Offset: 0x0027F69A
		public static numberGreaterThanValue CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new numberGreaterThanValue(new Hole(g.Symbol.numberGreaterThanValue, holeId));
		}

		// Token: 0x0600B98F RID: 47503 RVA: 0x002814B2 File Offset: 0x0027F6B2
		public numberGreaterThanValue(GrammarBuilders g, decimal value)
		{
			this = new numberGreaterThanValue(new LiteralNode(g.Symbol.numberGreaterThanValue, value));
		}

		// Token: 0x17002006 RID: 8198
		// (get) Token: 0x0600B990 RID: 47504 RVA: 0x002814D0 File Offset: 0x0027F6D0
		public decimal Value
		{
			get
			{
				return (decimal)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600B991 RID: 47505 RVA: 0x002814E7 File Offset: 0x0027F6E7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B992 RID: 47506 RVA: 0x002814FC File Offset: 0x0027F6FC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B993 RID: 47507 RVA: 0x00281526 File Offset: 0x0027F726
		public bool Equals(numberGreaterThanValue other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004685 RID: 18053
		private ProgramNode _node;
	}
}
