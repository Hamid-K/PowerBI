using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015D6 RID: 5590
	public struct numberEqualsValue : IProgramNodeBuilder, IEquatable<numberEqualsValue>
	{
		// Token: 0x17002003 RID: 8195
		// (get) Token: 0x0600B980 RID: 47488 RVA: 0x00281352 File Offset: 0x0027F552
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B981 RID: 47489 RVA: 0x0028135A File Offset: 0x0027F55A
		private numberEqualsValue(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B982 RID: 47490 RVA: 0x00281363 File Offset: 0x0027F563
		public static numberEqualsValue CreateUnsafe(ProgramNode node)
		{
			return new numberEqualsValue(node);
		}

		// Token: 0x0600B983 RID: 47491 RVA: 0x0028136C File Offset: 0x0027F56C
		public static numberEqualsValue? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.numberEqualsValue)
			{
				return null;
			}
			return new numberEqualsValue?(numberEqualsValue.CreateUnsafe(node));
		}

		// Token: 0x0600B984 RID: 47492 RVA: 0x002813A6 File Offset: 0x0027F5A6
		public static numberEqualsValue CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new numberEqualsValue(new Hole(g.Symbol.numberEqualsValue, holeId));
		}

		// Token: 0x0600B985 RID: 47493 RVA: 0x002813BE File Offset: 0x0027F5BE
		public numberEqualsValue(GrammarBuilders g, decimal value)
		{
			this = new numberEqualsValue(new LiteralNode(g.Symbol.numberEqualsValue, value));
		}

		// Token: 0x17002004 RID: 8196
		// (get) Token: 0x0600B986 RID: 47494 RVA: 0x002813DC File Offset: 0x0027F5DC
		public decimal Value
		{
			get
			{
				return (decimal)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600B987 RID: 47495 RVA: 0x002813F3 File Offset: 0x0027F5F3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B988 RID: 47496 RVA: 0x00281408 File Offset: 0x0027F608
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B989 RID: 47497 RVA: 0x00281432 File Offset: 0x0027F632
		public bool Equals(numberEqualsValue other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004684 RID: 18052
		private ProgramNode _node;
	}
}
