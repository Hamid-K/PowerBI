using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes
{
	// Token: 0x02000F35 RID: 3893
	public struct LetExtractTup : IProgramNodeBuilder, IEquatable<LetExtractTup>
	{
		// Token: 0x17001354 RID: 4948
		// (get) Token: 0x06006BEA RID: 27626 RVA: 0x00161C1A File Offset: 0x0015FE1A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006BEB RID: 27627 RVA: 0x00161C22 File Offset: 0x0015FE22
		private LetExtractTup(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006BEC RID: 27628 RVA: 0x00161C2B File Offset: 0x0015FE2B
		public static LetExtractTup CreateUnsafe(ProgramNode node)
		{
			return new LetExtractTup(node);
		}

		// Token: 0x06006BED RID: 27629 RVA: 0x00161C34 File Offset: 0x0015FE34
		public static LetExtractTup? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetExtractTup)
			{
				return null;
			}
			return new LetExtractTup?(LetExtractTup.CreateUnsafe(node));
		}

		// Token: 0x06006BEE RID: 27630 RVA: 0x00161C69 File Offset: 0x0015FE69
		public LetExtractTup(GrammarBuilders g, _LetB3 value0, trimExtract value1)
		{
			this._node = new LetNode(g.Rule.LetExtractTup, value0.Node, value1.Node);
		}

		// Token: 0x06006BEF RID: 27631 RVA: 0x00161C8F File Offset: 0x0015FE8F
		public static implicit operator extractTup(LetExtractTup arg)
		{
			return extractTup.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001355 RID: 4949
		// (get) Token: 0x06006BF0 RID: 27632 RVA: 0x00161C9D File Offset: 0x0015FE9D
		public _LetB3 _LetB3
		{
			get
			{
				return _LetB3.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001356 RID: 4950
		// (get) Token: 0x06006BF1 RID: 27633 RVA: 0x00161CB1 File Offset: 0x0015FEB1
		public trimExtract trimExtract
		{
			get
			{
				return trimExtract.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06006BF2 RID: 27634 RVA: 0x00161CC5 File Offset: 0x0015FEC5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006BF3 RID: 27635 RVA: 0x00161CD8 File Offset: 0x0015FED8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006BF4 RID: 27636 RVA: 0x00161D02 File Offset: 0x0015FF02
		public bool Equals(LetExtractTup other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F20 RID: 12064
		private ProgramNode _node;
	}
}
