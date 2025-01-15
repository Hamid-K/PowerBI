using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x0200094D RID: 2381
	public struct FilterRecords : IProgramNodeBuilder, IEquatable<FilterRecords>
	{
		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06003791 RID: 14225 RVA: 0x000AE0EE File Offset: 0x000AC2EE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x000AE0F6 File Offset: 0x000AC2F6
		private FilterRecords(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003793 RID: 14227 RVA: 0x000AE0FF File Offset: 0x000AC2FF
		public static FilterRecords CreateUnsafe(ProgramNode node)
		{
			return new FilterRecords(node);
		}

		// Token: 0x06003794 RID: 14228 RVA: 0x000AE108 File Offset: 0x000AC308
		public static FilterRecords? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FilterRecords)
			{
				return null;
			}
			return new FilterRecords?(FilterRecords.CreateUnsafe(node));
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x000AE140 File Offset: 0x000AC340
		public FilterRecords(GrammarBuilders g, skipEmpty value0, delimiter value1, commentStr value2, hasCommentHeader value3, skippedRecords value4)
		{
			this._node = g.Rule.FilterRecords.BuildASTNode(new ProgramNode[] { value0.Node, value1.Node, value2.Node, value3.Node, value4.Node });
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x000AE19B File Offset: 0x000AC39B
		public static implicit operator dataLines(FilterRecords arg)
		{
			return dataLines.CreateUnsafe(arg.Node);
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06003797 RID: 14231 RVA: 0x000AE1A9 File Offset: 0x000AC3A9
		public skipEmpty skipEmpty
		{
			get
			{
				return skipEmpty.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06003798 RID: 14232 RVA: 0x000AE1BD File Offset: 0x000AC3BD
		public delimiter delimiter
		{
			get
			{
				return delimiter.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06003799 RID: 14233 RVA: 0x000AE1D1 File Offset: 0x000AC3D1
		public commentStr commentStr
		{
			get
			{
				return commentStr.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x0600379A RID: 14234 RVA: 0x000AE1E5 File Offset: 0x000AC3E5
		public hasCommentHeader hasCommentHeader
		{
			get
			{
				return hasCommentHeader.CreateUnsafe(this.Node.Children[3]);
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x0600379B RID: 14235 RVA: 0x000AE1F9 File Offset: 0x000AC3F9
		public skippedRecords skippedRecords
		{
			get
			{
				return skippedRecords.CreateUnsafe(this.Node.Children[4]);
			}
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x000AE20D File Offset: 0x000AC40D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600379D RID: 14237 RVA: 0x000AE220 File Offset: 0x000AC420
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600379E RID: 14238 RVA: 0x000AE24A File Offset: 0x000AC44A
		public bool Equals(FilterRecords other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A6D RID: 6765
		private ProgramNode _node;
	}
}
