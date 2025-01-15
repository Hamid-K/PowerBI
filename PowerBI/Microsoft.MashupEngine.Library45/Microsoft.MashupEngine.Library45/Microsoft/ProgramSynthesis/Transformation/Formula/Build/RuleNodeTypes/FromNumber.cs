using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200158E RID: 5518
	public struct FromNumber : IProgramNodeBuilder, IEquatable<FromNumber>
	{
		// Token: 0x17001FA1 RID: 8097
		// (get) Token: 0x0600B4C2 RID: 46274 RVA: 0x0027547A File Offset: 0x0027367A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B4C3 RID: 46275 RVA: 0x00275482 File Offset: 0x00273682
		private FromNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B4C4 RID: 46276 RVA: 0x0027548B File Offset: 0x0027368B
		public static FromNumber CreateUnsafe(ProgramNode node)
		{
			return new FromNumber(node);
		}

		// Token: 0x0600B4C5 RID: 46277 RVA: 0x00275494 File Offset: 0x00273694
		public static FromNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FromNumber)
			{
				return null;
			}
			return new FromNumber?(FromNumber.CreateUnsafe(node));
		}

		// Token: 0x0600B4C6 RID: 46278 RVA: 0x002754C9 File Offset: 0x002736C9
		public FromNumber(GrammarBuilders g, row value0, columnName value1)
		{
			this._node = g.Rule.FromNumber.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B4C7 RID: 46279 RVA: 0x002754EF File Offset: 0x002736EF
		public static implicit operator fromNumber(FromNumber arg)
		{
			return fromNumber.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001FA2 RID: 8098
		// (get) Token: 0x0600B4C8 RID: 46280 RVA: 0x002754FD File Offset: 0x002736FD
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001FA3 RID: 8099
		// (get) Token: 0x0600B4C9 RID: 46281 RVA: 0x00275511 File Offset: 0x00273711
		public columnName columnName
		{
			get
			{
				return columnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B4CA RID: 46282 RVA: 0x00275525 File Offset: 0x00273725
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B4CB RID: 46283 RVA: 0x00275538 File Offset: 0x00273738
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B4CC RID: 46284 RVA: 0x00275562 File Offset: 0x00273762
		public bool Equals(FromNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400463C RID: 17980
		private ProgramNode _node;
	}
}
