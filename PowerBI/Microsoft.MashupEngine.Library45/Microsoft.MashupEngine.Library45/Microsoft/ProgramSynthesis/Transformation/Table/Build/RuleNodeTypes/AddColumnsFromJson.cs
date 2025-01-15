using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes
{
	// Token: 0x02001AB0 RID: 6832
	public struct AddColumnsFromJson : IProgramNodeBuilder, IEquatable<AddColumnsFromJson>
	{
		// Token: 0x170025C6 RID: 9670
		// (get) Token: 0x0600E1B3 RID: 57779 RVA: 0x003002F6 File Offset: 0x002FE4F6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E1B4 RID: 57780 RVA: 0x003002FE File Offset: 0x002FE4FE
		private AddColumnsFromJson(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E1B5 RID: 57781 RVA: 0x00300307 File Offset: 0x002FE507
		public static AddColumnsFromJson CreateUnsafe(ProgramNode node)
		{
			return new AddColumnsFromJson(node);
		}

		// Token: 0x0600E1B6 RID: 57782 RVA: 0x00300310 File Offset: 0x002FE510
		public static AddColumnsFromJson? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.AddColumnsFromJson)
			{
				return null;
			}
			return new AddColumnsFromJson?(AddColumnsFromJson.CreateUnsafe(node));
		}

		// Token: 0x0600E1B7 RID: 57783 RVA: 0x00300345 File Offset: 0x002FE545
		public AddColumnsFromJson(GrammarBuilders g, table value0, sourceColumnName value1, ejsonProgram value2)
		{
			this._node = g.Rule.AddColumnsFromJson.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600E1B8 RID: 57784 RVA: 0x00300372 File Offset: 0x002FE572
		public static implicit operator table(AddColumnsFromJson arg)
		{
			return table.CreateUnsafe(arg.Node);
		}

		// Token: 0x170025C7 RID: 9671
		// (get) Token: 0x0600E1B9 RID: 57785 RVA: 0x00300380 File Offset: 0x002FE580
		public table table
		{
			get
			{
				return table.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170025C8 RID: 9672
		// (get) Token: 0x0600E1BA RID: 57786 RVA: 0x00300394 File Offset: 0x002FE594
		public sourceColumnName sourceColumnName
		{
			get
			{
				return sourceColumnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x170025C9 RID: 9673
		// (get) Token: 0x0600E1BB RID: 57787 RVA: 0x003003A8 File Offset: 0x002FE5A8
		public ejsonProgram ejsonProgram
		{
			get
			{
				return ejsonProgram.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600E1BC RID: 57788 RVA: 0x003003BC File Offset: 0x002FE5BC
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E1BD RID: 57789 RVA: 0x003003D0 File Offset: 0x002FE5D0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E1BE RID: 57790 RVA: 0x003003FA File Offset: 0x002FE5FA
		public bool Equals(AddColumnsFromJson other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400556F RID: 21871
		private ProgramNode _node;
	}
}
