using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C16 RID: 7190
	public struct RegexPositionPair : IProgramNodeBuilder, IEquatable<RegexPositionPair>
	{
		// Token: 0x1700286A RID: 10346
		// (get) Token: 0x0600F1F9 RID: 61945 RVA: 0x003405B2 File Offset: 0x0033E7B2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F1FA RID: 61946 RVA: 0x003405BA File Offset: 0x0033E7BA
		private RegexPositionPair(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F1FB RID: 61947 RVA: 0x003405C3 File Offset: 0x0033E7C3
		public static RegexPositionPair CreateUnsafe(ProgramNode node)
		{
			return new RegexPositionPair(node);
		}

		// Token: 0x0600F1FC RID: 61948 RVA: 0x003405CC File Offset: 0x0033E7CC
		public static RegexPositionPair? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.RegexPositionPair)
			{
				return null;
			}
			return new RegexPositionPair?(RegexPositionPair.CreateUnsafe(node));
		}

		// Token: 0x0600F1FD RID: 61949 RVA: 0x00340601 File Offset: 0x0033E801
		public RegexPositionPair(GrammarBuilders g, x value0, r value1, k value2)
		{
			this._node = g.Rule.RegexPositionPair.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600F1FE RID: 61950 RVA: 0x0034062E File Offset: 0x0033E82E
		public static implicit operator PP(RegexPositionPair arg)
		{
			return PP.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700286B RID: 10347
		// (get) Token: 0x0600F1FF RID: 61951 RVA: 0x0034063C File Offset: 0x0033E83C
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700286C RID: 10348
		// (get) Token: 0x0600F200 RID: 61952 RVA: 0x00340650 File Offset: 0x0033E850
		public r r
		{
			get
			{
				return r.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x1700286D RID: 10349
		// (get) Token: 0x0600F201 RID: 61953 RVA: 0x00340664 File Offset: 0x0033E864
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600F202 RID: 61954 RVA: 0x00340678 File Offset: 0x0033E878
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F203 RID: 61955 RVA: 0x0034068C File Offset: 0x0033E88C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F204 RID: 61956 RVA: 0x003406B6 File Offset: 0x0033E8B6
		public bool Equals(RegexPositionPair other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B05 RID: 23301
		private ProgramNode _node;
	}
}
