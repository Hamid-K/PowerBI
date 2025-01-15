using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes
{
	// Token: 0x02001ABE RID: 6846
	public struct fillValue : IProgramNodeBuilder, IEquatable<fillValue>
	{
		// Token: 0x170025E1 RID: 9697
		// (get) Token: 0x0600E273 RID: 57971 RVA: 0x00301B0E File Offset: 0x002FFD0E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E274 RID: 57972 RVA: 0x00301B16 File Offset: 0x002FFD16
		private fillValue(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E275 RID: 57973 RVA: 0x00301B1F File Offset: 0x002FFD1F
		public static fillValue CreateUnsafe(ProgramNode node)
		{
			return new fillValue(node);
		}

		// Token: 0x0600E276 RID: 57974 RVA: 0x00301B28 File Offset: 0x002FFD28
		public static fillValue? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.fillValue)
			{
				return null;
			}
			return new fillValue?(fillValue.CreateUnsafe(node));
		}

		// Token: 0x0600E277 RID: 57975 RVA: 0x00301B62 File Offset: 0x002FFD62
		public static fillValue CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new fillValue(new Hole(g.Symbol.fillValue, holeId));
		}

		// Token: 0x0600E278 RID: 57976 RVA: 0x00301B7A File Offset: 0x002FFD7A
		public fillValue(GrammarBuilders g, object value)
		{
			this = new fillValue(new LiteralNode(g.Symbol.fillValue, value));
		}

		// Token: 0x170025E2 RID: 9698
		// (get) Token: 0x0600E279 RID: 57977 RVA: 0x00301B93 File Offset: 0x002FFD93
		public object Value
		{
			get
			{
				return ((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600E27A RID: 57978 RVA: 0x00301BA5 File Offset: 0x002FFDA5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E27B RID: 57979 RVA: 0x00301BB8 File Offset: 0x002FFDB8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E27C RID: 57980 RVA: 0x00301BE2 File Offset: 0x002FFDE2
		public bool Equals(fillValue other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400557D RID: 21885
		private ProgramNode _node;
	}
}
