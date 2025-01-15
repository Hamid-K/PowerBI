using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x02000957 RID: 2391
	public struct SelectData : IProgramNodeBuilder, IEquatable<SelectData>
	{
		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06003803 RID: 14339 RVA: 0x000AEB96 File Offset: 0x000ACD96
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x000AEB9E File Offset: 0x000ACD9E
		private SelectData(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x000AEBA7 File Offset: 0x000ACDA7
		public static SelectData CreateUnsafe(ProgramNode node)
		{
			return new SelectData(node);
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x000AEBB0 File Offset: 0x000ACDB0
		public static SelectData? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SelectData)
			{
				return null;
			}
			return new SelectData?(SelectData.CreateUnsafe(node));
		}

		// Token: 0x06003807 RID: 14343 RVA: 0x000AEBE5 File Offset: 0x000ACDE5
		public SelectData(GrammarBuilders g, basicLinePredicate value0, skippedRecords value1)
		{
			this._node = g.Rule.SelectData.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x06003808 RID: 14344 RVA: 0x000AEC17 File Offset: 0x000ACE17
		public static implicit operator dataLines(SelectData arg)
		{
			return dataLines.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06003809 RID: 14345 RVA: 0x000AEC25 File Offset: 0x000ACE25
		public basicLinePredicate basicLinePredicate
		{
			get
			{
				return basicLinePredicate.CreateUnsafe(this.Node.Children[0].Children[0]);
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x0600380A RID: 14346 RVA: 0x000AEC40 File Offset: 0x000ACE40
		public skippedRecords skippedRecords
		{
			get
			{
				return skippedRecords.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600380B RID: 14347 RVA: 0x000AEC54 File Offset: 0x000ACE54
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x000AEC68 File Offset: 0x000ACE68
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x000AEC92 File Offset: 0x000ACE92
		public bool Equals(SelectData other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A77 RID: 6775
		private ProgramNode _node;
	}
}
