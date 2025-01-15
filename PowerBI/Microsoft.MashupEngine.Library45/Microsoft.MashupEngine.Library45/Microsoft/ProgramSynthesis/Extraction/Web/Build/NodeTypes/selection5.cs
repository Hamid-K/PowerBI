using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200106D RID: 4205
	public struct selection5 : IProgramNodeBuilder, IEquatable<selection5>
	{
		// Token: 0x17001656 RID: 5718
		// (get) Token: 0x06007D6B RID: 32107 RVA: 0x001A6FE2 File Offset: 0x001A51E2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007D6C RID: 32108 RVA: 0x001A6FEA File Offset: 0x001A51EA
		private selection5(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007D6D RID: 32109 RVA: 0x001A6FF3 File Offset: 0x001A51F3
		public static selection5 CreateUnsafe(ProgramNode node)
		{
			return new selection5(node);
		}

		// Token: 0x06007D6E RID: 32110 RVA: 0x001A6FFC File Offset: 0x001A51FC
		public static selection5? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.selection5)
			{
				return null;
			}
			return new selection5?(selection5.CreateUnsafe(node));
		}

		// Token: 0x06007D6F RID: 32111 RVA: 0x001A7036 File Offset: 0x001A5236
		public static selection5 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new selection5(new Hole(g.Symbol.selection5, holeId));
		}

		// Token: 0x06007D70 RID: 32112 RVA: 0x001A704E File Offset: 0x001A524E
		public bool Is_SingleSelection3(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SingleSelection3;
		}

		// Token: 0x06007D71 RID: 32113 RVA: 0x001A7068 File Offset: 0x001A5268
		public bool Is_SingleSelection3(GrammarBuilders g, out SingleSelection3 value)
		{
			if (this.Node.GrammarRule == g.Rule.SingleSelection3)
			{
				value = SingleSelection3.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SingleSelection3);
			return false;
		}

		// Token: 0x06007D72 RID: 32114 RVA: 0x001A70A0 File Offset: 0x001A52A0
		public SingleSelection3? As_SingleSelection3(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SingleSelection3)
			{
				return null;
			}
			return new SingleSelection3?(SingleSelection3.CreateUnsafe(this.Node));
		}

		// Token: 0x06007D73 RID: 32115 RVA: 0x001A70E0 File Offset: 0x001A52E0
		public SingleSelection3 Cast_SingleSelection3(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SingleSelection3)
			{
				return SingleSelection3.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SingleSelection3 is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007D74 RID: 32116 RVA: 0x001A7135 File Offset: 0x001A5335
		public bool Is_DisjSelection3(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.DisjSelection3;
		}

		// Token: 0x06007D75 RID: 32117 RVA: 0x001A714F File Offset: 0x001A534F
		public bool Is_DisjSelection3(GrammarBuilders g, out DisjSelection3 value)
		{
			if (this.Node.GrammarRule == g.Rule.DisjSelection3)
			{
				value = DisjSelection3.CreateUnsafe(this.Node);
				return true;
			}
			value = default(DisjSelection3);
			return false;
		}

		// Token: 0x06007D76 RID: 32118 RVA: 0x001A7184 File Offset: 0x001A5384
		public DisjSelection3? As_DisjSelection3(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.DisjSelection3)
			{
				return null;
			}
			return new DisjSelection3?(DisjSelection3.CreateUnsafe(this.Node));
		}

		// Token: 0x06007D77 RID: 32119 RVA: 0x001A71C4 File Offset: 0x001A53C4
		public DisjSelection3 Cast_DisjSelection3(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.DisjSelection3)
			{
				return DisjSelection3.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_DisjSelection3 is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007D78 RID: 32120 RVA: 0x001A721C File Offset: 0x001A541C
		public T Switch<T>(GrammarBuilders g, Func<SingleSelection3, T> func0, Func<DisjSelection3, T> func1)
		{
			SingleSelection3 singleSelection;
			if (this.Is_SingleSelection3(g, out singleSelection))
			{
				return func0(singleSelection);
			}
			DisjSelection3 disjSelection;
			if (this.Is_DisjSelection3(g, out disjSelection))
			{
				return func1(disjSelection);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selection5");
		}

		// Token: 0x06007D79 RID: 32121 RVA: 0x001A7274 File Offset: 0x001A5474
		public void Switch(GrammarBuilders g, Action<SingleSelection3> func0, Action<DisjSelection3> func1)
		{
			SingleSelection3 singleSelection;
			if (this.Is_SingleSelection3(g, out singleSelection))
			{
				func0(singleSelection);
				return;
			}
			DisjSelection3 disjSelection;
			if (this.Is_DisjSelection3(g, out disjSelection))
			{
				func1(disjSelection);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selection5");
		}

		// Token: 0x06007D7A RID: 32122 RVA: 0x001A72CB File Offset: 0x001A54CB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007D7B RID: 32123 RVA: 0x001A72E0 File Offset: 0x001A54E0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007D7C RID: 32124 RVA: 0x001A730A File Offset: 0x001A550A
		public bool Equals(selection5 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003386 RID: 13190
		private ProgramNode _node;
	}
}
