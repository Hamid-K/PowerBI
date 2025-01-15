using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015D5 RID: 5589
	public struct matchCount : IProgramNodeBuilder, IEquatable<matchCount>
	{
		// Token: 0x17002001 RID: 8193
		// (get) Token: 0x0600B976 RID: 47478 RVA: 0x0028125E File Offset: 0x0027F45E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B977 RID: 47479 RVA: 0x00281266 File Offset: 0x0027F466
		private matchCount(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B978 RID: 47480 RVA: 0x0028126F File Offset: 0x0027F46F
		public static matchCount CreateUnsafe(ProgramNode node)
		{
			return new matchCount(node);
		}

		// Token: 0x0600B979 RID: 47481 RVA: 0x00281278 File Offset: 0x0027F478
		public static matchCount? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.matchCount)
			{
				return null;
			}
			return new matchCount?(matchCount.CreateUnsafe(node));
		}

		// Token: 0x0600B97A RID: 47482 RVA: 0x002812B2 File Offset: 0x0027F4B2
		public static matchCount CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new matchCount(new Hole(g.Symbol.matchCount, holeId));
		}

		// Token: 0x0600B97B RID: 47483 RVA: 0x002812CA File Offset: 0x0027F4CA
		public matchCount(GrammarBuilders g, int value)
		{
			this = new matchCount(new LiteralNode(g.Symbol.matchCount, value));
		}

		// Token: 0x17002002 RID: 8194
		// (get) Token: 0x0600B97C RID: 47484 RVA: 0x002812E8 File Offset: 0x0027F4E8
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600B97D RID: 47485 RVA: 0x002812FF File Offset: 0x0027F4FF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B97E RID: 47486 RVA: 0x00281314 File Offset: 0x0027F514
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B97F RID: 47487 RVA: 0x0028133E File Offset: 0x0027F53E
		public bool Equals(matchCount other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004683 RID: 18051
		private ProgramNode _node;
	}
}
