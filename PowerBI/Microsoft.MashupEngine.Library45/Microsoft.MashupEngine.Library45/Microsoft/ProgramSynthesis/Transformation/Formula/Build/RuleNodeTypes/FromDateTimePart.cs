using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001593 RID: 5523
	public struct FromDateTimePart : IProgramNodeBuilder, IEquatable<FromDateTimePart>
	{
		// Token: 0x17001FAF RID: 8111
		// (get) Token: 0x0600B4F8 RID: 46328 RVA: 0x0027594E File Offset: 0x00273B4E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B4F9 RID: 46329 RVA: 0x00275956 File Offset: 0x00273B56
		private FromDateTimePart(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B4FA RID: 46330 RVA: 0x0027595F File Offset: 0x00273B5F
		public static FromDateTimePart CreateUnsafe(ProgramNode node)
		{
			return new FromDateTimePart(node);
		}

		// Token: 0x0600B4FB RID: 46331 RVA: 0x00275968 File Offset: 0x00273B68
		public static FromDateTimePart? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FromDateTimePart)
			{
				return null;
			}
			return new FromDateTimePart?(FromDateTimePart.CreateUnsafe(node));
		}

		// Token: 0x0600B4FC RID: 46332 RVA: 0x0027599D File Offset: 0x00273B9D
		public FromDateTimePart(GrammarBuilders g, row value0, columnName value1, fromDateTimePartKind value2)
		{
			this._node = g.Rule.FromDateTimePart.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600B4FD RID: 46333 RVA: 0x002759CA File Offset: 0x00273BCA
		public static implicit operator fromDateTimePart(FromDateTimePart arg)
		{
			return fromDateTimePart.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001FB0 RID: 8112
		// (get) Token: 0x0600B4FE RID: 46334 RVA: 0x002759D8 File Offset: 0x00273BD8
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001FB1 RID: 8113
		// (get) Token: 0x0600B4FF RID: 46335 RVA: 0x002759EC File Offset: 0x00273BEC
		public columnName columnName
		{
			get
			{
				return columnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001FB2 RID: 8114
		// (get) Token: 0x0600B500 RID: 46336 RVA: 0x00275A00 File Offset: 0x00273C00
		public fromDateTimePartKind fromDateTimePartKind
		{
			get
			{
				return fromDateTimePartKind.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600B501 RID: 46337 RVA: 0x00275A14 File Offset: 0x00273C14
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B502 RID: 46338 RVA: 0x00275A28 File Offset: 0x00273C28
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B503 RID: 46339 RVA: 0x00275A52 File Offset: 0x00273C52
		public bool Equals(FromDateTimePart other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004641 RID: 17985
		private ProgramNode _node;
	}
}
