using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200158D RID: 5517
	public struct FromNumberStr : IProgramNodeBuilder, IEquatable<FromNumberStr>
	{
		// Token: 0x17001F9E RID: 8094
		// (get) Token: 0x0600B4B7 RID: 46263 RVA: 0x0027537E File Offset: 0x0027357E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B4B8 RID: 46264 RVA: 0x00275386 File Offset: 0x00273586
		private FromNumberStr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B4B9 RID: 46265 RVA: 0x0027538F File Offset: 0x0027358F
		public static FromNumberStr CreateUnsafe(ProgramNode node)
		{
			return new FromNumberStr(node);
		}

		// Token: 0x0600B4BA RID: 46266 RVA: 0x00275398 File Offset: 0x00273598
		public static FromNumberStr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FromNumberStr)
			{
				return null;
			}
			return new FromNumberStr?(FromNumberStr.CreateUnsafe(node));
		}

		// Token: 0x0600B4BB RID: 46267 RVA: 0x002753CD File Offset: 0x002735CD
		public FromNumberStr(GrammarBuilders g, row value0, columnName value1)
		{
			this._node = g.Rule.FromNumberStr.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B4BC RID: 46268 RVA: 0x002753F3 File Offset: 0x002735F3
		public static implicit operator fromNumberStr(FromNumberStr arg)
		{
			return fromNumberStr.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F9F RID: 8095
		// (get) Token: 0x0600B4BD RID: 46269 RVA: 0x00275401 File Offset: 0x00273601
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001FA0 RID: 8096
		// (get) Token: 0x0600B4BE RID: 46270 RVA: 0x00275415 File Offset: 0x00273615
		public columnName columnName
		{
			get
			{
				return columnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B4BF RID: 46271 RVA: 0x00275429 File Offset: 0x00273629
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B4C0 RID: 46272 RVA: 0x0027543C File Offset: 0x0027363C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B4C1 RID: 46273 RVA: 0x00275466 File Offset: 0x00273666
		public bool Equals(FromNumberStr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400463B RID: 17979
		private ProgramNode _node;
	}
}
