using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x02000950 RID: 2384
	public struct QuoteRecords : IProgramNodeBuilder, IEquatable<QuoteRecords>
	{
		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x060037B6 RID: 14262 RVA: 0x000AE472 File Offset: 0x000AC672
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x000AE47A File Offset: 0x000AC67A
		private QuoteRecords(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060037B8 RID: 14264 RVA: 0x000AE483 File Offset: 0x000AC683
		public static QuoteRecords CreateUnsafe(ProgramNode node)
		{
			return new QuoteRecords(node);
		}

		// Token: 0x060037B9 RID: 14265 RVA: 0x000AE48C File Offset: 0x000AC68C
		public static QuoteRecords? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.QuoteRecords)
			{
				return null;
			}
			return new QuoteRecords?(QuoteRecords.CreateUnsafe(node));
		}

		// Token: 0x060037BA RID: 14266 RVA: 0x000AE4C1 File Offset: 0x000AC6C1
		public QuoteRecords(GrammarBuilders g, quotingConfig value0, delimiter value1, allLines value2)
		{
			this._node = g.Rule.QuoteRecords.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x060037BB RID: 14267 RVA: 0x000AE4EE File Offset: 0x000AC6EE
		public static implicit operator allRecords(QuoteRecords arg)
		{
			return allRecords.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x060037BC RID: 14268 RVA: 0x000AE4FC File Offset: 0x000AC6FC
		public quotingConfig quotingConfig
		{
			get
			{
				return quotingConfig.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x060037BD RID: 14269 RVA: 0x000AE510 File Offset: 0x000AC710
		public delimiter delimiter
		{
			get
			{
				return delimiter.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x060037BE RID: 14270 RVA: 0x000AE524 File Offset: 0x000AC724
		public allLines allLines
		{
			get
			{
				return allLines.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x060037BF RID: 14271 RVA: 0x000AE538 File Offset: 0x000AC738
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060037C0 RID: 14272 RVA: 0x000AE54C File Offset: 0x000AC74C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060037C1 RID: 14273 RVA: 0x000AE576 File Offset: 0x000AC776
		public bool Equals(QuoteRecords other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A70 RID: 6768
		private ProgramNode _node;
	}
}
