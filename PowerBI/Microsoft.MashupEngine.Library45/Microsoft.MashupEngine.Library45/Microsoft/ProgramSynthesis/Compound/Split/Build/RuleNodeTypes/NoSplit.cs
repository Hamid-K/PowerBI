using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x0200093D RID: 2365
	public struct NoSplit : IProgramNodeBuilder, IEquatable<NoSplit>
	{
		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x060036DC RID: 14044 RVA: 0x000AD00A File Offset: 0x000AB20A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060036DD RID: 14045 RVA: 0x000AD012 File Offset: 0x000AB212
		private NoSplit(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060036DE RID: 14046 RVA: 0x000AD01B File Offset: 0x000AB21B
		public static NoSplit CreateUnsafe(ProgramNode node)
		{
			return new NoSplit(node);
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x000AD024 File Offset: 0x000AB224
		public static NoSplit? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.NoSplit)
			{
				return null;
			}
			return new NoSplit?(NoSplit.CreateUnsafe(node));
		}

		// Token: 0x060036E0 RID: 14048 RVA: 0x000AD059 File Offset: 0x000AB259
		public NoSplit(GrammarBuilders g, records value0, hasHeader value1)
		{
			this._node = g.Rule.NoSplit.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x060036E1 RID: 14049 RVA: 0x000AD07F File Offset: 0x000AB27F
		public static implicit operator splitRecords(NoSplit arg)
		{
			return splitRecords.CreateUnsafe(arg.Node);
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x060036E2 RID: 14050 RVA: 0x000AD08D File Offset: 0x000AB28D
		public records records
		{
			get
			{
				return records.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x060036E3 RID: 14051 RVA: 0x000AD0A1 File Offset: 0x000AB2A1
		public hasHeader hasHeader
		{
			get
			{
				return hasHeader.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060036E4 RID: 14052 RVA: 0x000AD0B5 File Offset: 0x000AB2B5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060036E5 RID: 14053 RVA: 0x000AD0C8 File Offset: 0x000AB2C8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x000AD0F2 File Offset: 0x000AB2F2
		public bool Equals(NoSplit other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A5D RID: 6749
		private ProgramNode _node;
	}
}
