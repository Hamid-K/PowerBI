using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes
{
	// Token: 0x02000F2C RID: 3884
	public struct SplitDelimiter : IProgramNodeBuilder, IEquatable<SplitDelimiter>
	{
		// Token: 0x17001339 RID: 4921
		// (get) Token: 0x06006B87 RID: 27527 RVA: 0x00161326 File Offset: 0x0015F526
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006B88 RID: 27528 RVA: 0x0016132E File Offset: 0x0015F52E
		private SplitDelimiter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006B89 RID: 27529 RVA: 0x00161337 File Offset: 0x0015F537
		public static SplitDelimiter CreateUnsafe(ProgramNode node)
		{
			return new SplitDelimiter(node);
		}

		// Token: 0x06006B8A RID: 27530 RVA: 0x00161340 File Offset: 0x0015F540
		public static SplitDelimiter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SplitDelimiter)
			{
				return null;
			}
			return new SplitDelimiter?(SplitDelimiter.CreateUnsafe(node));
		}

		// Token: 0x06006B8B RID: 27531 RVA: 0x00161375 File Offset: 0x0015F575
		public SplitDelimiter(GrammarBuilders g, row value0, str value1, k value2)
		{
			this._node = g.Rule.SplitDelimiter.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x06006B8C RID: 27532 RVA: 0x001613A2 File Offset: 0x0015F5A2
		public static implicit operator split(SplitDelimiter arg)
		{
			return split.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700133A RID: 4922
		// (get) Token: 0x06006B8D RID: 27533 RVA: 0x001613B0 File Offset: 0x0015F5B0
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700133B RID: 4923
		// (get) Token: 0x06006B8E RID: 27534 RVA: 0x001613C4 File Offset: 0x0015F5C4
		public str str
		{
			get
			{
				return str.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x1700133C RID: 4924
		// (get) Token: 0x06006B8F RID: 27535 RVA: 0x001613D8 File Offset: 0x0015F5D8
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x06006B90 RID: 27536 RVA: 0x001613EC File Offset: 0x0015F5EC
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006B91 RID: 27537 RVA: 0x00161400 File Offset: 0x0015F600
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006B92 RID: 27538 RVA: 0x0016142A File Offset: 0x0015F62A
		public bool Equals(SplitDelimiter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F17 RID: 12055
		private ProgramNode _node;
	}
}
