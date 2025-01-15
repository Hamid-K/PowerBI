using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes
{
	// Token: 0x02000F2B RID: 3883
	public struct SplitPosition : IProgramNodeBuilder, IEquatable<SplitPosition>
	{
		// Token: 0x17001336 RID: 4918
		// (get) Token: 0x06006B7C RID: 27516 RVA: 0x0016122A File Offset: 0x0015F42A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006B7D RID: 27517 RVA: 0x00161232 File Offset: 0x0015F432
		private SplitPosition(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006B7E RID: 27518 RVA: 0x0016123B File Offset: 0x0015F43B
		public static SplitPosition CreateUnsafe(ProgramNode node)
		{
			return new SplitPosition(node);
		}

		// Token: 0x06006B7F RID: 27519 RVA: 0x00161244 File Offset: 0x0015F444
		public static SplitPosition? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SplitPosition)
			{
				return null;
			}
			return new SplitPosition?(SplitPosition.CreateUnsafe(node));
		}

		// Token: 0x06006B80 RID: 27520 RVA: 0x00161279 File Offset: 0x0015F479
		public SplitPosition(GrammarBuilders g, row value0, k value1)
		{
			this._node = g.Rule.SplitPosition.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06006B81 RID: 27521 RVA: 0x0016129F File Offset: 0x0015F49F
		public static implicit operator split(SplitPosition arg)
		{
			return split.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001337 RID: 4919
		// (get) Token: 0x06006B82 RID: 27522 RVA: 0x001612AD File Offset: 0x0015F4AD
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001338 RID: 4920
		// (get) Token: 0x06006B83 RID: 27523 RVA: 0x001612C1 File Offset: 0x0015F4C1
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06006B84 RID: 27524 RVA: 0x001612D5 File Offset: 0x0015F4D5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006B85 RID: 27525 RVA: 0x001612E8 File Offset: 0x0015F4E8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006B86 RID: 27526 RVA: 0x00161312 File Offset: 0x0015F512
		public bool Equals(SplitPosition other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F16 RID: 12054
		private ProgramNode _node;
	}
}
