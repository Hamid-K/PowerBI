using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015D8 RID: 5592
	public struct numberLessThanValue : IProgramNodeBuilder, IEquatable<numberLessThanValue>
	{
		// Token: 0x17002007 RID: 8199
		// (get) Token: 0x0600B994 RID: 47508 RVA: 0x0028153A File Offset: 0x0027F73A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B995 RID: 47509 RVA: 0x00281542 File Offset: 0x0027F742
		private numberLessThanValue(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B996 RID: 47510 RVA: 0x0028154B File Offset: 0x0027F74B
		public static numberLessThanValue CreateUnsafe(ProgramNode node)
		{
			return new numberLessThanValue(node);
		}

		// Token: 0x0600B997 RID: 47511 RVA: 0x00281554 File Offset: 0x0027F754
		public static numberLessThanValue? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.numberLessThanValue)
			{
				return null;
			}
			return new numberLessThanValue?(numberLessThanValue.CreateUnsafe(node));
		}

		// Token: 0x0600B998 RID: 47512 RVA: 0x0028158E File Offset: 0x0027F78E
		public static numberLessThanValue CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new numberLessThanValue(new Hole(g.Symbol.numberLessThanValue, holeId));
		}

		// Token: 0x0600B999 RID: 47513 RVA: 0x002815A6 File Offset: 0x0027F7A6
		public numberLessThanValue(GrammarBuilders g, decimal value)
		{
			this = new numberLessThanValue(new LiteralNode(g.Symbol.numberLessThanValue, value));
		}

		// Token: 0x17002008 RID: 8200
		// (get) Token: 0x0600B99A RID: 47514 RVA: 0x002815C4 File Offset: 0x0027F7C4
		public decimal Value
		{
			get
			{
				return (decimal)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600B99B RID: 47515 RVA: 0x002815DB File Offset: 0x0027F7DB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B99C RID: 47516 RVA: 0x002815F0 File Offset: 0x0027F7F0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B99D RID: 47517 RVA: 0x0028161A File Offset: 0x0027F81A
		public bool Equals(numberLessThanValue other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004686 RID: 18054
		private ProgramNode _node;
	}
}
