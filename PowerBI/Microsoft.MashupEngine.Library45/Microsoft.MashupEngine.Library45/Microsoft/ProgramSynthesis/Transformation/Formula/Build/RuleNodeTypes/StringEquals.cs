using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001556 RID: 5462
	public struct StringEquals : IProgramNodeBuilder, IEquatable<StringEquals>
	{
		// Token: 0x17001EF7 RID: 7927
		// (get) Token: 0x0600B258 RID: 45656 RVA: 0x00271C82 File Offset: 0x0026FE82
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B259 RID: 45657 RVA: 0x00271C8A File Offset: 0x0026FE8A
		private StringEquals(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B25A RID: 45658 RVA: 0x00271C93 File Offset: 0x0026FE93
		public static StringEquals CreateUnsafe(ProgramNode node)
		{
			return new StringEquals(node);
		}

		// Token: 0x0600B25B RID: 45659 RVA: 0x00271C9C File Offset: 0x0026FE9C
		public static StringEquals? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.StringEquals)
			{
				return null;
			}
			return new StringEquals?(StringEquals.CreateUnsafe(node));
		}

		// Token: 0x0600B25C RID: 45660 RVA: 0x00271CD1 File Offset: 0x0026FED1
		public StringEquals(GrammarBuilders g, row value0, columnName value1, equalsText value2)
		{
			this._node = g.Rule.StringEquals.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600B25D RID: 45661 RVA: 0x00271CFE File Offset: 0x0026FEFE
		public static implicit operator condition(StringEquals arg)
		{
			return condition.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EF8 RID: 7928
		// (get) Token: 0x0600B25E RID: 45662 RVA: 0x00271D0C File Offset: 0x0026FF0C
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001EF9 RID: 7929
		// (get) Token: 0x0600B25F RID: 45663 RVA: 0x00271D20 File Offset: 0x0026FF20
		public columnName columnName
		{
			get
			{
				return columnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001EFA RID: 7930
		// (get) Token: 0x0600B260 RID: 45664 RVA: 0x00271D34 File Offset: 0x0026FF34
		public equalsText equalsText
		{
			get
			{
				return equalsText.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600B261 RID: 45665 RVA: 0x00271D48 File Offset: 0x0026FF48
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B262 RID: 45666 RVA: 0x00271D5C File Offset: 0x0026FF5C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B263 RID: 45667 RVA: 0x00271D86 File Offset: 0x0026FF86
		public bool Equals(StringEquals other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004604 RID: 17924
		private ProgramNode _node;
	}
}
