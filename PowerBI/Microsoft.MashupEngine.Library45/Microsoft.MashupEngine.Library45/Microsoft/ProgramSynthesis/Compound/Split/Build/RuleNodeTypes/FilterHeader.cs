using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x02000956 RID: 2390
	public struct FilterHeader : IProgramNodeBuilder, IEquatable<FilterHeader>
	{
		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x060037F8 RID: 14328 RVA: 0x000AEA86 File Offset: 0x000ACC86
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060037F9 RID: 14329 RVA: 0x000AEA8E File Offset: 0x000ACC8E
		private FilterHeader(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060037FA RID: 14330 RVA: 0x000AEA97 File Offset: 0x000ACC97
		public static FilterHeader CreateUnsafe(ProgramNode node)
		{
			return new FilterHeader(node);
		}

		// Token: 0x060037FB RID: 14331 RVA: 0x000AEAA0 File Offset: 0x000ACCA0
		public static FilterHeader? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FilterHeader)
			{
				return null;
			}
			return new FilterHeader?(FilterHeader.CreateUnsafe(node));
		}

		// Token: 0x060037FC RID: 14332 RVA: 0x000AEAD5 File Offset: 0x000ACCD5
		public FilterHeader(GrammarBuilders g, basicLinePredicate value0, skippedRecords value1)
		{
			this._node = g.Rule.FilterHeader.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x060037FD RID: 14333 RVA: 0x000AEB07 File Offset: 0x000ACD07
		public static implicit operator dataLines(FilterHeader arg)
		{
			return dataLines.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x060037FE RID: 14334 RVA: 0x000AEB15 File Offset: 0x000ACD15
		public basicLinePredicate basicLinePredicate
		{
			get
			{
				return basicLinePredicate.CreateUnsafe(this.Node.Children[0].Children[0]);
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x060037FF RID: 14335 RVA: 0x000AEB30 File Offset: 0x000ACD30
		public skippedRecords skippedRecords
		{
			get
			{
				return skippedRecords.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x000AEB44 File Offset: 0x000ACD44
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003801 RID: 14337 RVA: 0x000AEB58 File Offset: 0x000ACD58
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003802 RID: 14338 RVA: 0x000AEB82 File Offset: 0x000ACD82
		public bool Equals(FilterHeader other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A76 RID: 6774
		private ProgramNode _node;
	}
}
