using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes
{
	// Token: 0x02000BF6 RID: 3062
	public struct Between : IProgramNodeBuilder, IEquatable<Between>
	{
		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06004E9C RID: 20124 RVA: 0x000F8F7A File Offset: 0x000F717A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004E9D RID: 20125 RVA: 0x000F8F82 File Offset: 0x000F7182
		private Between(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004E9E RID: 20126 RVA: 0x000F8F8B File Offset: 0x000F718B
		public static Between CreateUnsafe(ProgramNode node)
		{
			return new Between(node);
		}

		// Token: 0x06004E9F RID: 20127 RVA: 0x000F8F94 File Offset: 0x000F7194
		public static Between? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Between)
			{
				return null;
			}
			return new Between?(Between.CreateUnsafe(node));
		}

		// Token: 0x06004EA0 RID: 20128 RVA: 0x000F8FC9 File Offset: 0x000F71C9
		public Between(GrammarBuilders g, betweenAxis value0, before value1, beforeRelativeBounds value2)
		{
			this._node = g.Rule.Between.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x06004EA1 RID: 20129 RVA: 0x000F8FF6 File Offset: 0x000F71F6
		public static implicit operator _LetB0(Between arg)
		{
			return _LetB0.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06004EA2 RID: 20130 RVA: 0x000F9004 File Offset: 0x000F7204
		public betweenAxis betweenAxis
		{
			get
			{
				return betweenAxis.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06004EA3 RID: 20131 RVA: 0x000F9018 File Offset: 0x000F7218
		public before before
		{
			get
			{
				return before.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06004EA4 RID: 20132 RVA: 0x000F902C File Offset: 0x000F722C
		public beforeRelativeBounds beforeRelativeBounds
		{
			get
			{
				return beforeRelativeBounds.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x06004EA5 RID: 20133 RVA: 0x000F9040 File Offset: 0x000F7240
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004EA6 RID: 20134 RVA: 0x000F9054 File Offset: 0x000F7254
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004EA7 RID: 20135 RVA: 0x000F907E File Offset: 0x000F727E
		public bool Equals(Between other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400231E RID: 8990
		private ProgramNode _node;
	}
}
