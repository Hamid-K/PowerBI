using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015DB RID: 5595
	public struct constStr : IProgramNodeBuilder, IEquatable<constStr>
	{
		// Token: 0x1700200D RID: 8205
		// (get) Token: 0x0600B9B2 RID: 47538 RVA: 0x0028180E File Offset: 0x0027FA0E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B9B3 RID: 47539 RVA: 0x00281816 File Offset: 0x0027FA16
		private constStr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B9B4 RID: 47540 RVA: 0x0028181F File Offset: 0x0027FA1F
		public static constStr CreateUnsafe(ProgramNode node)
		{
			return new constStr(node);
		}

		// Token: 0x0600B9B5 RID: 47541 RVA: 0x00281828 File Offset: 0x0027FA28
		public static constStr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.constStr)
			{
				return null;
			}
			return new constStr?(constStr.CreateUnsafe(node));
		}

		// Token: 0x0600B9B6 RID: 47542 RVA: 0x00281862 File Offset: 0x0027FA62
		public static constStr CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new constStr(new Hole(g.Symbol.constStr, holeId));
		}

		// Token: 0x0600B9B7 RID: 47543 RVA: 0x0028187A File Offset: 0x0027FA7A
		public constStr(GrammarBuilders g, string value)
		{
			this = new constStr(new LiteralNode(g.Symbol.constStr, value));
		}

		// Token: 0x1700200E RID: 8206
		// (get) Token: 0x0600B9B8 RID: 47544 RVA: 0x00281893 File Offset: 0x0027FA93
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600B9B9 RID: 47545 RVA: 0x002818AA File Offset: 0x0027FAAA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B9BA RID: 47546 RVA: 0x002818C0 File Offset: 0x0027FAC0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B9BB RID: 47547 RVA: 0x002818EA File Offset: 0x0027FAEA
		public bool Equals(constStr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004689 RID: 18057
		private ProgramNode _node;
	}
}
