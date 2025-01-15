using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001076 RID: 4214
	public struct leafFExpr : IProgramNodeBuilder, IEquatable<leafFExpr>
	{
		// Token: 0x1700165F RID: 5727
		// (get) Token: 0x06007DF5 RID: 32245 RVA: 0x001A83CE File Offset: 0x001A65CE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007DF6 RID: 32246 RVA: 0x001A83D6 File Offset: 0x001A65D6
		private leafFExpr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007DF7 RID: 32247 RVA: 0x001A83DF File Offset: 0x001A65DF
		public static leafFExpr CreateUnsafe(ProgramNode node)
		{
			return new leafFExpr(node);
		}

		// Token: 0x06007DF8 RID: 32248 RVA: 0x001A83E8 File Offset: 0x001A65E8
		public static leafFExpr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.leafFExpr)
			{
				return null;
			}
			return new leafFExpr?(leafFExpr.CreateUnsafe(node));
		}

		// Token: 0x06007DF9 RID: 32249 RVA: 0x001A8422 File Offset: 0x001A6622
		public static leafFExpr CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new leafFExpr(new Hole(g.Symbol.leafFExpr, holeId));
		}

		// Token: 0x06007DFA RID: 32250 RVA: 0x001A843A File Offset: 0x001A663A
		public bool Is_leafFExpr_leafAtom(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.leafFExpr_leafAtom;
		}

		// Token: 0x06007DFB RID: 32251 RVA: 0x001A8454 File Offset: 0x001A6654
		public bool Is_leafFExpr_leafAtom(GrammarBuilders g, out leafFExpr_leafAtom value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.leafFExpr_leafAtom)
			{
				value = leafFExpr_leafAtom.CreateUnsafe(this.Node);
				return true;
			}
			value = default(leafFExpr_leafAtom);
			return false;
		}

		// Token: 0x06007DFC RID: 32252 RVA: 0x001A848C File Offset: 0x001A668C
		public leafFExpr_leafAtom? As_leafFExpr_leafAtom(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.leafFExpr_leafAtom)
			{
				return null;
			}
			return new leafFExpr_leafAtom?(leafFExpr_leafAtom.CreateUnsafe(this.Node));
		}

		// Token: 0x06007DFD RID: 32253 RVA: 0x001A84CC File Offset: 0x001A66CC
		public leafFExpr_leafAtom Cast_leafFExpr_leafAtom(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.leafFExpr_leafAtom)
			{
				return leafFExpr_leafAtom.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_leafFExpr_leafAtom is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007DFE RID: 32254 RVA: 0x001A8521 File Offset: 0x001A6721
		public bool Is_LeafAnd(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LeafAnd;
		}

		// Token: 0x06007DFF RID: 32255 RVA: 0x001A853B File Offset: 0x001A673B
		public bool Is_LeafAnd(GrammarBuilders g, out LeafAnd value)
		{
			if (this.Node.GrammarRule == g.Rule.LeafAnd)
			{
				value = LeafAnd.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LeafAnd);
			return false;
		}

		// Token: 0x06007E00 RID: 32256 RVA: 0x001A8570 File Offset: 0x001A6770
		public LeafAnd? As_LeafAnd(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LeafAnd)
			{
				return null;
			}
			return new LeafAnd?(LeafAnd.CreateUnsafe(this.Node));
		}

		// Token: 0x06007E01 RID: 32257 RVA: 0x001A85B0 File Offset: 0x001A67B0
		public LeafAnd Cast_LeafAnd(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LeafAnd)
			{
				return LeafAnd.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LeafAnd is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007E02 RID: 32258 RVA: 0x001A8608 File Offset: 0x001A6808
		public T Switch<T>(GrammarBuilders g, Func<leafFExpr_leafAtom, T> func0, Func<LeafAnd, T> func1)
		{
			leafFExpr_leafAtom leafFExpr_leafAtom;
			if (this.Is_leafFExpr_leafAtom(g, out leafFExpr_leafAtom))
			{
				return func0(leafFExpr_leafAtom);
			}
			LeafAnd leafAnd;
			if (this.Is_LeafAnd(g, out leafAnd))
			{
				return func1(leafAnd);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol leafFExpr");
		}

		// Token: 0x06007E03 RID: 32259 RVA: 0x001A8660 File Offset: 0x001A6860
		public void Switch(GrammarBuilders g, Action<leafFExpr_leafAtom> func0, Action<LeafAnd> func1)
		{
			leafFExpr_leafAtom leafFExpr_leafAtom;
			if (this.Is_leafFExpr_leafAtom(g, out leafFExpr_leafAtom))
			{
				func0(leafFExpr_leafAtom);
				return;
			}
			LeafAnd leafAnd;
			if (this.Is_LeafAnd(g, out leafAnd))
			{
				func1(leafAnd);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol leafFExpr");
		}

		// Token: 0x06007E04 RID: 32260 RVA: 0x001A86B7 File Offset: 0x001A68B7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007E05 RID: 32261 RVA: 0x001A86CC File Offset: 0x001A68CC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007E06 RID: 32262 RVA: 0x001A86F6 File Offset: 0x001A68F6
		public bool Equals(leafFExpr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400338F RID: 13199
		private ProgramNode _node;
	}
}
