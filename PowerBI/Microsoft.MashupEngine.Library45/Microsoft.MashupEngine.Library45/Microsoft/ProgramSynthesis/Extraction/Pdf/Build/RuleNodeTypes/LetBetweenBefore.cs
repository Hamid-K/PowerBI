using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes
{
	// Token: 0x02000BFD RID: 3069
	public struct LetBetweenBefore : IProgramNodeBuilder, IEquatable<LetBetweenBefore>
	{
		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06004EED RID: 20205 RVA: 0x000F96F2 File Offset: 0x000F78F2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004EEE RID: 20206 RVA: 0x000F96FA File Offset: 0x000F78FA
		private LetBetweenBefore(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004EEF RID: 20207 RVA: 0x000F9703 File Offset: 0x000F7903
		public static LetBetweenBefore CreateUnsafe(ProgramNode node)
		{
			return new LetBetweenBefore(node);
		}

		// Token: 0x06004EF0 RID: 20208 RVA: 0x000F970C File Offset: 0x000F790C
		public static LetBetweenBefore? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetBetweenBefore)
			{
				return null;
			}
			return new LetBetweenBefore?(LetBetweenBefore.CreateUnsafe(node));
		}

		// Token: 0x06004EF1 RID: 20209 RVA: 0x000F9741 File Offset: 0x000F7941
		public LetBetweenBefore(GrammarBuilders g, selectedBounds value0, _LetB0 value1)
		{
			this._node = new LetNode(g.Rule.LetBetweenBefore, value0.Node, value1.Node);
		}

		// Token: 0x06004EF2 RID: 20210 RVA: 0x000F9767 File Offset: 0x000F7967
		public static implicit operator _LetB1(LetBetweenBefore arg)
		{
			return _LetB1.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06004EF3 RID: 20211 RVA: 0x000F9775 File Offset: 0x000F7975
		public selectedBounds selectedBounds
		{
			get
			{
				return selectedBounds.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x06004EF4 RID: 20212 RVA: 0x000F9789 File Offset: 0x000F7989
		public _LetB0 _LetB0
		{
			get
			{
				return _LetB0.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06004EF5 RID: 20213 RVA: 0x000F979D File Offset: 0x000F799D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004EF6 RID: 20214 RVA: 0x000F97B0 File Offset: 0x000F79B0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004EF7 RID: 20215 RVA: 0x000F97DA File Offset: 0x000F79DA
		public bool Equals(LetBetweenBefore other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002325 RID: 8997
		private ProgramNode _node;
	}
}
