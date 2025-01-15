using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200106A RID: 4202
	public struct selection3 : IProgramNodeBuilder, IEquatable<selection3>
	{
		// Token: 0x17001653 RID: 5715
		// (get) Token: 0x06007D3B RID: 32059 RVA: 0x001A687A File Offset: 0x001A4A7A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007D3C RID: 32060 RVA: 0x001A6882 File Offset: 0x001A4A82
		private selection3(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007D3D RID: 32061 RVA: 0x001A688B File Offset: 0x001A4A8B
		public static selection3 CreateUnsafe(ProgramNode node)
		{
			return new selection3(node);
		}

		// Token: 0x06007D3E RID: 32062 RVA: 0x001A6894 File Offset: 0x001A4A94
		public static selection3? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.selection3)
			{
				return null;
			}
			return new selection3?(selection3.CreateUnsafe(node));
		}

		// Token: 0x06007D3F RID: 32063 RVA: 0x001A68CE File Offset: 0x001A4ACE
		public static selection3 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new selection3(new Hole(g.Symbol.selection3, holeId));
		}

		// Token: 0x06007D40 RID: 32064 RVA: 0x001A68E6 File Offset: 0x001A4AE6
		public bool Is_SingleSelection2(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SingleSelection2;
		}

		// Token: 0x06007D41 RID: 32065 RVA: 0x001A6900 File Offset: 0x001A4B00
		public bool Is_SingleSelection2(GrammarBuilders g, out SingleSelection2 value)
		{
			if (this.Node.GrammarRule == g.Rule.SingleSelection2)
			{
				value = SingleSelection2.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SingleSelection2);
			return false;
		}

		// Token: 0x06007D42 RID: 32066 RVA: 0x001A6938 File Offset: 0x001A4B38
		public SingleSelection2? As_SingleSelection2(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SingleSelection2)
			{
				return null;
			}
			return new SingleSelection2?(SingleSelection2.CreateUnsafe(this.Node));
		}

		// Token: 0x06007D43 RID: 32067 RVA: 0x001A6978 File Offset: 0x001A4B78
		public SingleSelection2 Cast_SingleSelection2(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SingleSelection2)
			{
				return SingleSelection2.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SingleSelection2 is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007D44 RID: 32068 RVA: 0x001A69CD File Offset: 0x001A4BCD
		public bool Is_DisjSelection2(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.DisjSelection2;
		}

		// Token: 0x06007D45 RID: 32069 RVA: 0x001A69E7 File Offset: 0x001A4BE7
		public bool Is_DisjSelection2(GrammarBuilders g, out DisjSelection2 value)
		{
			if (this.Node.GrammarRule == g.Rule.DisjSelection2)
			{
				value = DisjSelection2.CreateUnsafe(this.Node);
				return true;
			}
			value = default(DisjSelection2);
			return false;
		}

		// Token: 0x06007D46 RID: 32070 RVA: 0x001A6A1C File Offset: 0x001A4C1C
		public DisjSelection2? As_DisjSelection2(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.DisjSelection2)
			{
				return null;
			}
			return new DisjSelection2?(DisjSelection2.CreateUnsafe(this.Node));
		}

		// Token: 0x06007D47 RID: 32071 RVA: 0x001A6A5C File Offset: 0x001A4C5C
		public DisjSelection2 Cast_DisjSelection2(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.DisjSelection2)
			{
				return DisjSelection2.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_DisjSelection2 is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007D48 RID: 32072 RVA: 0x001A6AB4 File Offset: 0x001A4CB4
		public T Switch<T>(GrammarBuilders g, Func<SingleSelection2, T> func0, Func<DisjSelection2, T> func1)
		{
			SingleSelection2 singleSelection;
			if (this.Is_SingleSelection2(g, out singleSelection))
			{
				return func0(singleSelection);
			}
			DisjSelection2 disjSelection;
			if (this.Is_DisjSelection2(g, out disjSelection))
			{
				return func1(disjSelection);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selection3");
		}

		// Token: 0x06007D49 RID: 32073 RVA: 0x001A6B0C File Offset: 0x001A4D0C
		public void Switch(GrammarBuilders g, Action<SingleSelection2> func0, Action<DisjSelection2> func1)
		{
			SingleSelection2 singleSelection;
			if (this.Is_SingleSelection2(g, out singleSelection))
			{
				func0(singleSelection);
				return;
			}
			DisjSelection2 disjSelection;
			if (this.Is_DisjSelection2(g, out disjSelection))
			{
				func1(disjSelection);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selection3");
		}

		// Token: 0x06007D4A RID: 32074 RVA: 0x001A6B63 File Offset: 0x001A4D63
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007D4B RID: 32075 RVA: 0x001A6B78 File Offset: 0x001A4D78
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007D4C RID: 32076 RVA: 0x001A6BA2 File Offset: 0x001A4DA2
		public bool Equals(selection3 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003383 RID: 13187
		private ProgramNode _node;
	}
}
