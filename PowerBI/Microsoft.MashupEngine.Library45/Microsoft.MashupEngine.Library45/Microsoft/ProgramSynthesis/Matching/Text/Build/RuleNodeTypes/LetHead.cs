using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes
{
	// Token: 0x020011EF RID: 4591
	public struct LetHead : IProgramNodeBuilder, IEquatable<LetHead>
	{
		// Token: 0x170017BB RID: 6075
		// (get) Token: 0x06008A0B RID: 35339 RVA: 0x001D0052 File Offset: 0x001CE252
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008A0C RID: 35340 RVA: 0x001D005A File Offset: 0x001CE25A
		private LetHead(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008A0D RID: 35341 RVA: 0x001D0063 File Offset: 0x001CE263
		public static LetHead CreateUnsafe(ProgramNode node)
		{
			return new LetHead(node);
		}

		// Token: 0x06008A0E RID: 35342 RVA: 0x001D006C File Offset: 0x001CE26C
		public static LetHead? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetHead)
			{
				return null;
			}
			return new LetHead?(LetHead.CreateUnsafe(node));
		}

		// Token: 0x06008A0F RID: 35343 RVA: 0x001D00A1 File Offset: 0x001CE2A1
		public LetHead(GrammarBuilders g, _LetB3 value0, _LetB4 value1)
		{
			this._node = new LetNode(g.Rule.LetHead, value0.Node, value1.Node);
		}

		// Token: 0x06008A10 RID: 35344 RVA: 0x001D00C7 File Offset: 0x001CE2C7
		public static implicit operator multi_result_matches(LetHead arg)
		{
			return multi_result_matches.CreateUnsafe(arg.Node);
		}

		// Token: 0x170017BC RID: 6076
		// (get) Token: 0x06008A11 RID: 35345 RVA: 0x001D00D5 File Offset: 0x001CE2D5
		public _LetB3 _LetB3
		{
			get
			{
				return _LetB3.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170017BD RID: 6077
		// (get) Token: 0x06008A12 RID: 35346 RVA: 0x001D00E9 File Offset: 0x001CE2E9
		public _LetB4 _LetB4
		{
			get
			{
				return _LetB4.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06008A13 RID: 35347 RVA: 0x001D00FD File Offset: 0x001CE2FD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008A14 RID: 35348 RVA: 0x001D0110 File Offset: 0x001CE310
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008A15 RID: 35349 RVA: 0x001D013A File Offset: 0x001CE33A
		public bool Equals(LetHead other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040038A3 RID: 14499
		private ProgramNode _node;
	}
}
