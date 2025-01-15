using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C19 RID: 7193
	public struct RegexPositionRelative : IProgramNodeBuilder, IEquatable<RegexPositionRelative>
	{
		// Token: 0x17002875 RID: 10357
		// (get) Token: 0x0600F21C RID: 61980 RVA: 0x003408DE File Offset: 0x0033EADE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F21D RID: 61981 RVA: 0x003408E6 File Offset: 0x0033EAE6
		private RegexPositionRelative(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F21E RID: 61982 RVA: 0x003408EF File Offset: 0x0033EAEF
		public static RegexPositionRelative CreateUnsafe(ProgramNode node)
		{
			return new RegexPositionRelative(node);
		}

		// Token: 0x0600F21F RID: 61983 RVA: 0x003408F8 File Offset: 0x0033EAF8
		public static RegexPositionRelative? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.RegexPositionRelative)
			{
				return null;
			}
			return new RegexPositionRelative?(RegexPositionRelative.CreateUnsafe(node));
		}

		// Token: 0x0600F220 RID: 61984 RVA: 0x0034092D File Offset: 0x0033EB2D
		public RegexPositionRelative(GrammarBuilders g, x value0, regexPair value1, k value2)
		{
			this._node = g.Rule.RegexPositionRelative.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600F221 RID: 61985 RVA: 0x0034095A File Offset: 0x0033EB5A
		public static implicit operator pos(RegexPositionRelative arg)
		{
			return pos.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002876 RID: 10358
		// (get) Token: 0x0600F222 RID: 61986 RVA: 0x00340968 File Offset: 0x0033EB68
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002877 RID: 10359
		// (get) Token: 0x0600F223 RID: 61987 RVA: 0x0034097C File Offset: 0x0033EB7C
		public regexPair regexPair
		{
			get
			{
				return regexPair.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17002878 RID: 10360
		// (get) Token: 0x0600F224 RID: 61988 RVA: 0x00340990 File Offset: 0x0033EB90
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600F225 RID: 61989 RVA: 0x003409A4 File Offset: 0x0033EBA4
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F226 RID: 61990 RVA: 0x003409B8 File Offset: 0x0033EBB8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F227 RID: 61991 RVA: 0x003409E2 File Offset: 0x0033EBE2
		public bool Equals(RegexPositionRelative other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B08 RID: 23304
		private ProgramNode _node;
	}
}
