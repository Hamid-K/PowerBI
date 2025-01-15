using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015D1 RID: 5585
	public struct startsWithFindText : IProgramNodeBuilder, IEquatable<startsWithFindText>
	{
		// Token: 0x17001FF9 RID: 8185
		// (get) Token: 0x0600B94E RID: 47438 RVA: 0x00280E9A File Offset: 0x0027F09A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B94F RID: 47439 RVA: 0x00280EA2 File Offset: 0x0027F0A2
		private startsWithFindText(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B950 RID: 47440 RVA: 0x00280EAB File Offset: 0x0027F0AB
		public static startsWithFindText CreateUnsafe(ProgramNode node)
		{
			return new startsWithFindText(node);
		}

		// Token: 0x0600B951 RID: 47441 RVA: 0x00280EB4 File Offset: 0x0027F0B4
		public static startsWithFindText? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.startsWithFindText)
			{
				return null;
			}
			return new startsWithFindText?(startsWithFindText.CreateUnsafe(node));
		}

		// Token: 0x0600B952 RID: 47442 RVA: 0x00280EEE File Offset: 0x0027F0EE
		public static startsWithFindText CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new startsWithFindText(new Hole(g.Symbol.startsWithFindText, holeId));
		}

		// Token: 0x0600B953 RID: 47443 RVA: 0x00280F06 File Offset: 0x0027F106
		public startsWithFindText(GrammarBuilders g, string value)
		{
			this = new startsWithFindText(new LiteralNode(g.Symbol.startsWithFindText, value));
		}

		// Token: 0x17001FFA RID: 8186
		// (get) Token: 0x0600B954 RID: 47444 RVA: 0x00280F1F File Offset: 0x0027F11F
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600B955 RID: 47445 RVA: 0x00280F36 File Offset: 0x0027F136
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B956 RID: 47446 RVA: 0x00280F4C File Offset: 0x0027F14C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B957 RID: 47447 RVA: 0x00280F76 File Offset: 0x0027F176
		public bool Equals(startsWithFindText other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400467F RID: 18047
		private ProgramNode _node;
	}
}
