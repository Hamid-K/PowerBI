using System;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015D2 RID: 5586
	public struct isMatchRegex : IProgramNodeBuilder, IEquatable<isMatchRegex>
	{
		// Token: 0x17001FFB RID: 8187
		// (get) Token: 0x0600B958 RID: 47448 RVA: 0x00280F8A File Offset: 0x0027F18A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B959 RID: 47449 RVA: 0x00280F92 File Offset: 0x0027F192
		private isMatchRegex(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B95A RID: 47450 RVA: 0x00280F9B File Offset: 0x0027F19B
		public static isMatchRegex CreateUnsafe(ProgramNode node)
		{
			return new isMatchRegex(node);
		}

		// Token: 0x0600B95B RID: 47451 RVA: 0x00280FA4 File Offset: 0x0027F1A4
		public static isMatchRegex? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.isMatchRegex)
			{
				return null;
			}
			return new isMatchRegex?(isMatchRegex.CreateUnsafe(node));
		}

		// Token: 0x0600B95C RID: 47452 RVA: 0x00280FDE File Offset: 0x0027F1DE
		public static isMatchRegex CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new isMatchRegex(new Hole(g.Symbol.isMatchRegex, holeId));
		}

		// Token: 0x0600B95D RID: 47453 RVA: 0x00280FF6 File Offset: 0x0027F1F6
		public isMatchRegex(GrammarBuilders g, Regex value)
		{
			this = new isMatchRegex(new LiteralNode(g.Symbol.isMatchRegex, value));
		}

		// Token: 0x17001FFC RID: 8188
		// (get) Token: 0x0600B95E RID: 47454 RVA: 0x0028100F File Offset: 0x0027F20F
		public Regex Value
		{
			get
			{
				return (Regex)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600B95F RID: 47455 RVA: 0x00281026 File Offset: 0x0027F226
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B960 RID: 47456 RVA: 0x0028103C File Offset: 0x0027F23C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B961 RID: 47457 RVA: 0x00281066 File Offset: 0x0027F266
		public bool Equals(isMatchRegex other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004680 RID: 18048
		private ProgramNode _node;
	}
}
