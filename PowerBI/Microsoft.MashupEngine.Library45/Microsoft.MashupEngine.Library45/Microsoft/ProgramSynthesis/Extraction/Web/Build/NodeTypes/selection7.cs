using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001070 RID: 4208
	public struct selection7 : IProgramNodeBuilder, IEquatable<selection7>
	{
		// Token: 0x17001659 RID: 5721
		// (get) Token: 0x06007D9B RID: 32155 RVA: 0x001A774A File Offset: 0x001A594A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007D9C RID: 32156 RVA: 0x001A7752 File Offset: 0x001A5952
		private selection7(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007D9D RID: 32157 RVA: 0x001A775B File Offset: 0x001A595B
		public static selection7 CreateUnsafe(ProgramNode node)
		{
			return new selection7(node);
		}

		// Token: 0x06007D9E RID: 32158 RVA: 0x001A7764 File Offset: 0x001A5964
		public static selection7? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.selection7)
			{
				return null;
			}
			return new selection7?(selection7.CreateUnsafe(node));
		}

		// Token: 0x06007D9F RID: 32159 RVA: 0x001A779E File Offset: 0x001A599E
		public static selection7 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new selection7(new Hole(g.Symbol.selection7, holeId));
		}

		// Token: 0x06007DA0 RID: 32160 RVA: 0x001A77B6 File Offset: 0x001A59B6
		public bool Is_SingleSelection4(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SingleSelection4;
		}

		// Token: 0x06007DA1 RID: 32161 RVA: 0x001A77D0 File Offset: 0x001A59D0
		public bool Is_SingleSelection4(GrammarBuilders g, out SingleSelection4 value)
		{
			if (this.Node.GrammarRule == g.Rule.SingleSelection4)
			{
				value = SingleSelection4.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SingleSelection4);
			return false;
		}

		// Token: 0x06007DA2 RID: 32162 RVA: 0x001A7808 File Offset: 0x001A5A08
		public SingleSelection4? As_SingleSelection4(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SingleSelection4)
			{
				return null;
			}
			return new SingleSelection4?(SingleSelection4.CreateUnsafe(this.Node));
		}

		// Token: 0x06007DA3 RID: 32163 RVA: 0x001A7848 File Offset: 0x001A5A48
		public SingleSelection4 Cast_SingleSelection4(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SingleSelection4)
			{
				return SingleSelection4.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SingleSelection4 is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007DA4 RID: 32164 RVA: 0x001A789D File Offset: 0x001A5A9D
		public bool Is_DisjSelection4(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.DisjSelection4;
		}

		// Token: 0x06007DA5 RID: 32165 RVA: 0x001A78B7 File Offset: 0x001A5AB7
		public bool Is_DisjSelection4(GrammarBuilders g, out DisjSelection4 value)
		{
			if (this.Node.GrammarRule == g.Rule.DisjSelection4)
			{
				value = DisjSelection4.CreateUnsafe(this.Node);
				return true;
			}
			value = default(DisjSelection4);
			return false;
		}

		// Token: 0x06007DA6 RID: 32166 RVA: 0x001A78EC File Offset: 0x001A5AEC
		public DisjSelection4? As_DisjSelection4(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.DisjSelection4)
			{
				return null;
			}
			return new DisjSelection4?(DisjSelection4.CreateUnsafe(this.Node));
		}

		// Token: 0x06007DA7 RID: 32167 RVA: 0x001A792C File Offset: 0x001A5B2C
		public DisjSelection4 Cast_DisjSelection4(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.DisjSelection4)
			{
				return DisjSelection4.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_DisjSelection4 is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007DA8 RID: 32168 RVA: 0x001A7984 File Offset: 0x001A5B84
		public T Switch<T>(GrammarBuilders g, Func<SingleSelection4, T> func0, Func<DisjSelection4, T> func1)
		{
			SingleSelection4 singleSelection;
			if (this.Is_SingleSelection4(g, out singleSelection))
			{
				return func0(singleSelection);
			}
			DisjSelection4 disjSelection;
			if (this.Is_DisjSelection4(g, out disjSelection))
			{
				return func1(disjSelection);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selection7");
		}

		// Token: 0x06007DA9 RID: 32169 RVA: 0x001A79DC File Offset: 0x001A5BDC
		public void Switch(GrammarBuilders g, Action<SingleSelection4> func0, Action<DisjSelection4> func1)
		{
			SingleSelection4 singleSelection;
			if (this.Is_SingleSelection4(g, out singleSelection))
			{
				func0(singleSelection);
				return;
			}
			DisjSelection4 disjSelection;
			if (this.Is_DisjSelection4(g, out disjSelection))
			{
				func1(disjSelection);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selection7");
		}

		// Token: 0x06007DAA RID: 32170 RVA: 0x001A7A33 File Offset: 0x001A5C33
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007DAB RID: 32171 RVA: 0x001A7A48 File Offset: 0x001A5C48
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007DAC RID: 32172 RVA: 0x001A7A72 File Offset: 0x001A5C72
		public bool Equals(selection7 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003389 RID: 13193
		private ProgramNode _node;
	}
}
