using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015D0 RID: 5584
	public struct containsFindText : IProgramNodeBuilder, IEquatable<containsFindText>
	{
		// Token: 0x17001FF7 RID: 8183
		// (get) Token: 0x0600B944 RID: 47428 RVA: 0x00280DAA File Offset: 0x0027EFAA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B945 RID: 47429 RVA: 0x00280DB2 File Offset: 0x0027EFB2
		private containsFindText(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B946 RID: 47430 RVA: 0x00280DBB File Offset: 0x0027EFBB
		public static containsFindText CreateUnsafe(ProgramNode node)
		{
			return new containsFindText(node);
		}

		// Token: 0x0600B947 RID: 47431 RVA: 0x00280DC4 File Offset: 0x0027EFC4
		public static containsFindText? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.containsFindText)
			{
				return null;
			}
			return new containsFindText?(containsFindText.CreateUnsafe(node));
		}

		// Token: 0x0600B948 RID: 47432 RVA: 0x00280DFE File Offset: 0x0027EFFE
		public static containsFindText CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new containsFindText(new Hole(g.Symbol.containsFindText, holeId));
		}

		// Token: 0x0600B949 RID: 47433 RVA: 0x00280E16 File Offset: 0x0027F016
		public containsFindText(GrammarBuilders g, string value)
		{
			this = new containsFindText(new LiteralNode(g.Symbol.containsFindText, value));
		}

		// Token: 0x17001FF8 RID: 8184
		// (get) Token: 0x0600B94A RID: 47434 RVA: 0x00280E2F File Offset: 0x0027F02F
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600B94B RID: 47435 RVA: 0x00280E46 File Offset: 0x0027F046
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B94C RID: 47436 RVA: 0x00280E5C File Offset: 0x0027F05C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B94D RID: 47437 RVA: 0x00280E86 File Offset: 0x0027F086
		public bool Equals(containsFindText other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400467E RID: 18046
		private ProgramNode _node;
	}
}
