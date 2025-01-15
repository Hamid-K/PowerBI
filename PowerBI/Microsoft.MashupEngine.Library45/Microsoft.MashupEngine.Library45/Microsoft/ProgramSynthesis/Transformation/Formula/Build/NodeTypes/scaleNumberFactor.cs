using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015F4 RID: 5620
	public struct scaleNumberFactor : IProgramNodeBuilder, IEquatable<scaleNumberFactor>
	{
		// Token: 0x1700203F RID: 8255
		// (get) Token: 0x0600BAAC RID: 47788 RVA: 0x00282FA6 File Offset: 0x002811A6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600BAAD RID: 47789 RVA: 0x00282FAE File Offset: 0x002811AE
		private scaleNumberFactor(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600BAAE RID: 47790 RVA: 0x00282FB7 File Offset: 0x002811B7
		public static scaleNumberFactor CreateUnsafe(ProgramNode node)
		{
			return new scaleNumberFactor(node);
		}

		// Token: 0x0600BAAF RID: 47791 RVA: 0x00282FC0 File Offset: 0x002811C0
		public static scaleNumberFactor? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.scaleNumberFactor)
			{
				return null;
			}
			return new scaleNumberFactor?(scaleNumberFactor.CreateUnsafe(node));
		}

		// Token: 0x0600BAB0 RID: 47792 RVA: 0x00282FFA File Offset: 0x002811FA
		public static scaleNumberFactor CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new scaleNumberFactor(new Hole(g.Symbol.scaleNumberFactor, holeId));
		}

		// Token: 0x0600BAB1 RID: 47793 RVA: 0x00283012 File Offset: 0x00281212
		public scaleNumberFactor(GrammarBuilders g, int value)
		{
			this = new scaleNumberFactor(new LiteralNode(g.Symbol.scaleNumberFactor, value));
		}

		// Token: 0x17002040 RID: 8256
		// (get) Token: 0x0600BAB2 RID: 47794 RVA: 0x00283030 File Offset: 0x00281230
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600BAB3 RID: 47795 RVA: 0x00283047 File Offset: 0x00281247
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600BAB4 RID: 47796 RVA: 0x0028305C File Offset: 0x0028125C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600BAB5 RID: 47797 RVA: 0x00283086 File Offset: 0x00281286
		public bool Equals(scaleNumberFactor other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040046A2 RID: 18082
		private ProgramNode _node;
	}
}
