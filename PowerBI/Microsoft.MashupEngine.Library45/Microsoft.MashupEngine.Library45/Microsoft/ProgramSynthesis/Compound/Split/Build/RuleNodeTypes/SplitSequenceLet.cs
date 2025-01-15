using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x0200095C RID: 2396
	public struct SplitSequenceLet : IProgramNodeBuilder, IEquatable<SplitSequenceLet>
	{
		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06003839 RID: 14393 RVA: 0x000AF07E File Offset: 0x000AD27E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x000AF086 File Offset: 0x000AD286
		private SplitSequenceLet(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600383B RID: 14395 RVA: 0x000AF08F File Offset: 0x000AD28F
		public static SplitSequenceLet CreateUnsafe(ProgramNode node)
		{
			return new SplitSequenceLet(node);
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x000AF098 File Offset: 0x000AD298
		public static SplitSequenceLet? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SplitSequenceLet)
			{
				return null;
			}
			return new SplitSequenceLet?(SplitSequenceLet.CreateUnsafe(node));
		}

		// Token: 0x0600383D RID: 14397 RVA: 0x000AF0CD File Offset: 0x000AD2CD
		public SplitSequenceLet(GrammarBuilders g, dataLines value0, splitSequence value1)
		{
			this._node = new LetNode(g.Rule.SplitSequenceLet, value0.Node, value1.Node);
		}

		// Token: 0x0600383E RID: 14398 RVA: 0x000AF0F3 File Offset: 0x000AD2F3
		public static implicit operator splitLines(SplitSequenceLet arg)
		{
			return splitLines.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x0600383F RID: 14399 RVA: 0x000AF101 File Offset: 0x000AD301
		public dataLines dataLines
		{
			get
			{
				return dataLines.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06003840 RID: 14400 RVA: 0x000AF115 File Offset: 0x000AD315
		public splitSequence splitSequence
		{
			get
			{
				return splitSequence.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06003841 RID: 14401 RVA: 0x000AF129 File Offset: 0x000AD329
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003842 RID: 14402 RVA: 0x000AF13C File Offset: 0x000AD33C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003843 RID: 14403 RVA: 0x000AF166 File Offset: 0x000AD366
		public bool Equals(SplitSequenceLet other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A7C RID: 6780
		private ProgramNode _node;
	}
}
