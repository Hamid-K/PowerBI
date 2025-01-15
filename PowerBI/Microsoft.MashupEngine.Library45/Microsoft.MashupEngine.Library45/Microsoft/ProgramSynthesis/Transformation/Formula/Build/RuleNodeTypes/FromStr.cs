using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200158C RID: 5516
	public struct FromStr : IProgramNodeBuilder, IEquatable<FromStr>
	{
		// Token: 0x17001F9B RID: 8091
		// (get) Token: 0x0600B4AC RID: 46252 RVA: 0x00275282 File Offset: 0x00273482
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B4AD RID: 46253 RVA: 0x0027528A File Offset: 0x0027348A
		private FromStr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B4AE RID: 46254 RVA: 0x00275293 File Offset: 0x00273493
		public static FromStr CreateUnsafe(ProgramNode node)
		{
			return new FromStr(node);
		}

		// Token: 0x0600B4AF RID: 46255 RVA: 0x0027529C File Offset: 0x0027349C
		public static FromStr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FromStr)
			{
				return null;
			}
			return new FromStr?(FromStr.CreateUnsafe(node));
		}

		// Token: 0x0600B4B0 RID: 46256 RVA: 0x002752D1 File Offset: 0x002734D1
		public FromStr(GrammarBuilders g, row value0, columnName value1)
		{
			this._node = g.Rule.FromStr.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B4B1 RID: 46257 RVA: 0x002752F7 File Offset: 0x002734F7
		public static implicit operator fromStr(FromStr arg)
		{
			return fromStr.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F9C RID: 8092
		// (get) Token: 0x0600B4B2 RID: 46258 RVA: 0x00275305 File Offset: 0x00273505
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F9D RID: 8093
		// (get) Token: 0x0600B4B3 RID: 46259 RVA: 0x00275319 File Offset: 0x00273519
		public columnName columnName
		{
			get
			{
				return columnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B4B4 RID: 46260 RVA: 0x0027532D File Offset: 0x0027352D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B4B5 RID: 46261 RVA: 0x00275340 File Offset: 0x00273540
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B4B6 RID: 46262 RVA: 0x0027536A File Offset: 0x0027356A
		public bool Equals(FromStr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400463A RID: 17978
		private ProgramNode _node;
	}
}
