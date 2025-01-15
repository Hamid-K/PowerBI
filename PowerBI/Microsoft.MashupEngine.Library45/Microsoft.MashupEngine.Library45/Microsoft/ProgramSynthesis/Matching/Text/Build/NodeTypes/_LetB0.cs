using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes
{
	// Token: 0x020011F7 RID: 4599
	public struct _LetB0 : IProgramNodeBuilder, IEquatable<_LetB0>
	{
		// Token: 0x170017C5 RID: 6085
		// (get) Token: 0x06008A8C RID: 35468 RVA: 0x001D1466 File Offset: 0x001CF666
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008A8D RID: 35469 RVA: 0x001D146E File Offset: 0x001CF66E
		private _LetB0(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008A8E RID: 35470 RVA: 0x001D1477 File Offset: 0x001CF677
		public static _LetB0 CreateUnsafe(ProgramNode node)
		{
			return new _LetB0(node);
		}

		// Token: 0x06008A8F RID: 35471 RVA: 0x001D1480 File Offset: 0x001CF680
		public static _LetB0? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB0)
			{
				return null;
			}
			return new _LetB0?(_LetB0.CreateUnsafe(node));
		}

		// Token: 0x06008A90 RID: 35472 RVA: 0x001D14BA File Offset: 0x001CF6BA
		public static _LetB0 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB0(new Hole(g.Symbol._LetB0, holeId));
		}

		// Token: 0x06008A91 RID: 35473 RVA: 0x001D14D2 File Offset: 0x001CF6D2
		public SuffixAfterTokenMatch Cast_SuffixAfterTokenMatch()
		{
			return SuffixAfterTokenMatch.CreateUnsafe(this.Node);
		}

		// Token: 0x06008A92 RID: 35474 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_SuffixAfterTokenMatch(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06008A93 RID: 35475 RVA: 0x001D14DF File Offset: 0x001CF6DF
		public bool Is_SuffixAfterTokenMatch(GrammarBuilders g, out SuffixAfterTokenMatch value)
		{
			value = SuffixAfterTokenMatch.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06008A94 RID: 35476 RVA: 0x001D14F3 File Offset: 0x001CF6F3
		public SuffixAfterTokenMatch? As_SuffixAfterTokenMatch(GrammarBuilders g)
		{
			return new SuffixAfterTokenMatch?(SuffixAfterTokenMatch.CreateUnsafe(this.Node));
		}

		// Token: 0x06008A95 RID: 35477 RVA: 0x001D1505 File Offset: 0x001CF705
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008A96 RID: 35478 RVA: 0x001D1518 File Offset: 0x001CF718
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008A97 RID: 35479 RVA: 0x001D1542 File Offset: 0x001CF742
		public bool Equals(_LetB0 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040038AB RID: 14507
		private ProgramNode _node;
	}
}
