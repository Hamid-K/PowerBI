using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.RuleNodeTypes
{
	// Token: 0x02001AAC RID: 6828
	public struct FillMissingValues : IProgramNodeBuilder, IEquatable<FillMissingValues>
	{
		// Token: 0x170025B5 RID: 9653
		// (get) Token: 0x0600E182 RID: 57730 RVA: 0x002FFE5A File Offset: 0x002FE05A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E183 RID: 57731 RVA: 0x002FFE62 File Offset: 0x002FE062
		private FillMissingValues(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E184 RID: 57732 RVA: 0x002FFE6B File Offset: 0x002FE06B
		public static FillMissingValues CreateUnsafe(ProgramNode node)
		{
			return new FillMissingValues(node);
		}

		// Token: 0x0600E185 RID: 57733 RVA: 0x002FFE74 File Offset: 0x002FE074
		public static FillMissingValues? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FillMissingValues)
			{
				return null;
			}
			return new FillMissingValues?(FillMissingValues.CreateUnsafe(node));
		}

		// Token: 0x0600E186 RID: 57734 RVA: 0x002FFEAC File Offset: 0x002FE0AC
		public FillMissingValues(GrammarBuilders g, table value0, sourceColumnName value1, fillValue value2, missingValueMarkers value3, fillMethod value4)
		{
			this._node = g.Rule.FillMissingValues.BuildASTNode(new ProgramNode[] { value0.Node, value1.Node, value2.Node, value3.Node, value4.Node });
		}

		// Token: 0x0600E187 RID: 57735 RVA: 0x002FFF07 File Offset: 0x002FE107
		public static implicit operator table(FillMissingValues arg)
		{
			return table.CreateUnsafe(arg.Node);
		}

		// Token: 0x170025B6 RID: 9654
		// (get) Token: 0x0600E188 RID: 57736 RVA: 0x002FFF15 File Offset: 0x002FE115
		public table table
		{
			get
			{
				return table.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170025B7 RID: 9655
		// (get) Token: 0x0600E189 RID: 57737 RVA: 0x002FFF29 File Offset: 0x002FE129
		public sourceColumnName sourceColumnName
		{
			get
			{
				return sourceColumnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x170025B8 RID: 9656
		// (get) Token: 0x0600E18A RID: 57738 RVA: 0x002FFF3D File Offset: 0x002FE13D
		public fillValue fillValue
		{
			get
			{
				return fillValue.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x170025B9 RID: 9657
		// (get) Token: 0x0600E18B RID: 57739 RVA: 0x002FFF51 File Offset: 0x002FE151
		public missingValueMarkers missingValueMarkers
		{
			get
			{
				return missingValueMarkers.CreateUnsafe(this.Node.Children[3]);
			}
		}

		// Token: 0x170025BA RID: 9658
		// (get) Token: 0x0600E18C RID: 57740 RVA: 0x002FFF65 File Offset: 0x002FE165
		public fillMethod fillMethod
		{
			get
			{
				return fillMethod.CreateUnsafe(this.Node.Children[4]);
			}
		}

		// Token: 0x0600E18D RID: 57741 RVA: 0x002FFF79 File Offset: 0x002FE179
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E18E RID: 57742 RVA: 0x002FFF8C File Offset: 0x002FE18C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E18F RID: 57743 RVA: 0x002FFFB6 File Offset: 0x002FE1B6
		public bool Equals(FillMissingValues other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400556B RID: 21867
		private ProgramNode _node;
	}
}
