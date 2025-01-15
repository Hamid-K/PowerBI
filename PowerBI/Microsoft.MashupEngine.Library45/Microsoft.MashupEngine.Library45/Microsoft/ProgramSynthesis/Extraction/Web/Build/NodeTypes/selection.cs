using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001065 RID: 4197
	public struct selection : IProgramNodeBuilder, IEquatable<selection>
	{
		// Token: 0x1700164E RID: 5710
		// (get) Token: 0x06007CE5 RID: 31973 RVA: 0x001A5ACE File Offset: 0x001A3CCE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007CE6 RID: 31974 RVA: 0x001A5AD6 File Offset: 0x001A3CD6
		private selection(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007CE7 RID: 31975 RVA: 0x001A5ADF File Offset: 0x001A3CDF
		public static selection CreateUnsafe(ProgramNode node)
		{
			return new selection(node);
		}

		// Token: 0x06007CE8 RID: 31976 RVA: 0x001A5AE8 File Offset: 0x001A3CE8
		public static selection? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.selection)
			{
				return null;
			}
			return new selection?(selection.CreateUnsafe(node));
		}

		// Token: 0x06007CE9 RID: 31977 RVA: 0x001A5B22 File Offset: 0x001A3D22
		public static selection CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new selection(new Hole(g.Symbol.selection, holeId));
		}

		// Token: 0x06007CEA RID: 31978 RVA: 0x001A5B3A File Offset: 0x001A3D3A
		public bool Is_SingleSelection1(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SingleSelection1;
		}

		// Token: 0x06007CEB RID: 31979 RVA: 0x001A5B54 File Offset: 0x001A3D54
		public bool Is_SingleSelection1(GrammarBuilders g, out SingleSelection1 value)
		{
			if (this.Node.GrammarRule == g.Rule.SingleSelection1)
			{
				value = SingleSelection1.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SingleSelection1);
			return false;
		}

		// Token: 0x06007CEC RID: 31980 RVA: 0x001A5B8C File Offset: 0x001A3D8C
		public SingleSelection1? As_SingleSelection1(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SingleSelection1)
			{
				return null;
			}
			return new SingleSelection1?(SingleSelection1.CreateUnsafe(this.Node));
		}

		// Token: 0x06007CED RID: 31981 RVA: 0x001A5BCC File Offset: 0x001A3DCC
		public SingleSelection1 Cast_SingleSelection1(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SingleSelection1)
			{
				return SingleSelection1.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SingleSelection1 is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007CEE RID: 31982 RVA: 0x001A5C21 File Offset: 0x001A3E21
		public bool Is_DisjSelection1(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.DisjSelection1;
		}

		// Token: 0x06007CEF RID: 31983 RVA: 0x001A5C3B File Offset: 0x001A3E3B
		public bool Is_DisjSelection1(GrammarBuilders g, out DisjSelection1 value)
		{
			if (this.Node.GrammarRule == g.Rule.DisjSelection1)
			{
				value = DisjSelection1.CreateUnsafe(this.Node);
				return true;
			}
			value = default(DisjSelection1);
			return false;
		}

		// Token: 0x06007CF0 RID: 31984 RVA: 0x001A5C70 File Offset: 0x001A3E70
		public DisjSelection1? As_DisjSelection1(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.DisjSelection1)
			{
				return null;
			}
			return new DisjSelection1?(DisjSelection1.CreateUnsafe(this.Node));
		}

		// Token: 0x06007CF1 RID: 31985 RVA: 0x001A5CB0 File Offset: 0x001A3EB0
		public DisjSelection1 Cast_DisjSelection1(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.DisjSelection1)
			{
				return DisjSelection1.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_DisjSelection1 is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007CF2 RID: 31986 RVA: 0x001A5D05 File Offset: 0x001A3F05
		public bool Is_CSSSelection(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.CSSSelection;
		}

		// Token: 0x06007CF3 RID: 31987 RVA: 0x001A5D1F File Offset: 0x001A3F1F
		public bool Is_CSSSelection(GrammarBuilders g, out CSSSelection value)
		{
			if (this.Node.GrammarRule == g.Rule.CSSSelection)
			{
				value = CSSSelection.CreateUnsafe(this.Node);
				return true;
			}
			value = default(CSSSelection);
			return false;
		}

		// Token: 0x06007CF4 RID: 31988 RVA: 0x001A5D54 File Offset: 0x001A3F54
		public CSSSelection? As_CSSSelection(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.CSSSelection)
			{
				return null;
			}
			return new CSSSelection?(CSSSelection.CreateUnsafe(this.Node));
		}

		// Token: 0x06007CF5 RID: 31989 RVA: 0x001A5D94 File Offset: 0x001A3F94
		public CSSSelection Cast_CSSSelection(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.CSSSelection)
			{
				return CSSSelection.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_CSSSelection is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007CF6 RID: 31990 RVA: 0x001A5DEC File Offset: 0x001A3FEC
		public T Switch<T>(GrammarBuilders g, Func<SingleSelection1, T> func0, Func<DisjSelection1, T> func1, Func<CSSSelection, T> func2)
		{
			SingleSelection1 singleSelection;
			if (this.Is_SingleSelection1(g, out singleSelection))
			{
				return func0(singleSelection);
			}
			DisjSelection1 disjSelection;
			if (this.Is_DisjSelection1(g, out disjSelection))
			{
				return func1(disjSelection);
			}
			CSSSelection cssselection;
			if (this.Is_CSSSelection(g, out cssselection))
			{
				return func2(cssselection);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selection");
		}

		// Token: 0x06007CF7 RID: 31991 RVA: 0x001A5E58 File Offset: 0x001A4058
		public void Switch(GrammarBuilders g, Action<SingleSelection1> func0, Action<DisjSelection1> func1, Action<CSSSelection> func2)
		{
			SingleSelection1 singleSelection;
			if (this.Is_SingleSelection1(g, out singleSelection))
			{
				func0(singleSelection);
				return;
			}
			DisjSelection1 disjSelection;
			if (this.Is_DisjSelection1(g, out disjSelection))
			{
				func1(disjSelection);
				return;
			}
			CSSSelection cssselection;
			if (this.Is_CSSSelection(g, out cssselection))
			{
				func2(cssselection);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selection");
		}

		// Token: 0x06007CF8 RID: 31992 RVA: 0x001A5EC3 File Offset: 0x001A40C3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007CF9 RID: 31993 RVA: 0x001A5ED8 File Offset: 0x001A40D8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007CFA RID: 31994 RVA: 0x001A5F02 File Offset: 0x001A4102
		public bool Equals(selection other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400337E RID: 13182
		private ProgramNode _node;
	}
}
