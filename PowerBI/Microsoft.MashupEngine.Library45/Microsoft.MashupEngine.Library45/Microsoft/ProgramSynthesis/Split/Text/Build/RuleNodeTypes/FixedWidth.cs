using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x02001353 RID: 4947
	public struct FixedWidth : IProgramNodeBuilder, IEquatable<FixedWidth>
	{
		// Token: 0x17001A45 RID: 6725
		// (get) Token: 0x060098A0 RID: 39072 RVA: 0x002070A2 File Offset: 0x002052A2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060098A1 RID: 39073 RVA: 0x002070AA File Offset: 0x002052AA
		private FixedWidth(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060098A2 RID: 39074 RVA: 0x002070B3 File Offset: 0x002052B3
		public static FixedWidth CreateUnsafe(ProgramNode node)
		{
			return new FixedWidth(node);
		}

		// Token: 0x060098A3 RID: 39075 RVA: 0x002070BC File Offset: 0x002052BC
		public static FixedWidth? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FixedWidth)
			{
				return null;
			}
			return new FixedWidth?(FixedWidth.CreateUnsafe(node));
		}

		// Token: 0x060098A4 RID: 39076 RVA: 0x002070F1 File Offset: 0x002052F1
		public FixedWidth(GrammarBuilders g, v value0, fieldStartPositions value1)
		{
			this._node = g.Rule.FixedWidth.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x060098A5 RID: 39077 RVA: 0x00207117 File Offset: 0x00205317
		public static implicit operator fixedWidthMatches(FixedWidth arg)
		{
			return fixedWidthMatches.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A46 RID: 6726
		// (get) Token: 0x060098A6 RID: 39078 RVA: 0x00207125 File Offset: 0x00205325
		public v v
		{
			get
			{
				return v.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A47 RID: 6727
		// (get) Token: 0x060098A7 RID: 39079 RVA: 0x00207139 File Offset: 0x00205339
		public fieldStartPositions fieldStartPositions
		{
			get
			{
				return fieldStartPositions.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060098A8 RID: 39080 RVA: 0x0020714D File Offset: 0x0020534D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060098A9 RID: 39081 RVA: 0x00207160 File Offset: 0x00205360
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060098AA RID: 39082 RVA: 0x0020718A File Offset: 0x0020538A
		public bool Equals(FixedWidth other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DCA RID: 15818
		private ProgramNode _node;
	}
}
