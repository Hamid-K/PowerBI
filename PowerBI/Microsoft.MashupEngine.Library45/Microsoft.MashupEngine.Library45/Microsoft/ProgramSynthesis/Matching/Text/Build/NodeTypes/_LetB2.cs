using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes
{
	// Token: 0x020011F9 RID: 4601
	public struct _LetB2 : IProgramNodeBuilder, IEquatable<_LetB2>
	{
		// Token: 0x170017C7 RID: 6087
		// (get) Token: 0x06008AA4 RID: 35492 RVA: 0x001D1646 File Offset: 0x001CF846
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008AA5 RID: 35493 RVA: 0x001D164E File Offset: 0x001CF84E
		private _LetB2(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008AA6 RID: 35494 RVA: 0x001D1657 File Offset: 0x001CF857
		public static _LetB2 CreateUnsafe(ProgramNode node)
		{
			return new _LetB2(node);
		}

		// Token: 0x06008AA7 RID: 35495 RVA: 0x001D1660 File Offset: 0x001CF860
		public static _LetB2? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB2)
			{
				return null;
			}
			return new _LetB2?(_LetB2.CreateUnsafe(node));
		}

		// Token: 0x06008AA8 RID: 35496 RVA: 0x001D169A File Offset: 0x001CF89A
		public static _LetB2 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB2(new Hole(g.Symbol._LetB2, holeId));
		}

		// Token: 0x06008AA9 RID: 35497 RVA: 0x001D16B2 File Offset: 0x001CF8B2
		public MatchColumns Cast_MatchColumns()
		{
			return MatchColumns.CreateUnsafe(this.Node);
		}

		// Token: 0x06008AAA RID: 35498 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_MatchColumns(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06008AAB RID: 35499 RVA: 0x001D16BF File Offset: 0x001CF8BF
		public bool Is_MatchColumns(GrammarBuilders g, out MatchColumns value)
		{
			value = MatchColumns.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06008AAC RID: 35500 RVA: 0x001D16D3 File Offset: 0x001CF8D3
		public MatchColumns? As_MatchColumns(GrammarBuilders g)
		{
			return new MatchColumns?(MatchColumns.CreateUnsafe(this.Node));
		}

		// Token: 0x06008AAD RID: 35501 RVA: 0x001D16E5 File Offset: 0x001CF8E5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008AAE RID: 35502 RVA: 0x001D16F8 File Offset: 0x001CF8F8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008AAF RID: 35503 RVA: 0x001D1722 File Offset: 0x001CF922
		public bool Equals(_LetB2 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040038AD RID: 14509
		private ProgramNode _node;
	}
}
