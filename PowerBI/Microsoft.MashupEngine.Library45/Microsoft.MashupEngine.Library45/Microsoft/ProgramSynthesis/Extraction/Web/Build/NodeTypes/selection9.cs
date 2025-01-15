using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001073 RID: 4211
	public struct selection9 : IProgramNodeBuilder, IEquatable<selection9>
	{
		// Token: 0x1700165C RID: 5724
		// (get) Token: 0x06007DCB RID: 32203 RVA: 0x001A7EB2 File Offset: 0x001A60B2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007DCC RID: 32204 RVA: 0x001A7EBA File Offset: 0x001A60BA
		private selection9(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007DCD RID: 32205 RVA: 0x001A7EC3 File Offset: 0x001A60C3
		public static selection9 CreateUnsafe(ProgramNode node)
		{
			return new selection9(node);
		}

		// Token: 0x06007DCE RID: 32206 RVA: 0x001A7ECC File Offset: 0x001A60CC
		public static selection9? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.selection9)
			{
				return null;
			}
			return new selection9?(selection9.CreateUnsafe(node));
		}

		// Token: 0x06007DCF RID: 32207 RVA: 0x001A7F06 File Offset: 0x001A6106
		public static selection9 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new selection9(new Hole(g.Symbol.selection9, holeId));
		}

		// Token: 0x06007DD0 RID: 32208 RVA: 0x001A7F1E File Offset: 0x001A611E
		public bool Is_SingleSelection5(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SingleSelection5;
		}

		// Token: 0x06007DD1 RID: 32209 RVA: 0x001A7F38 File Offset: 0x001A6138
		public bool Is_SingleSelection5(GrammarBuilders g, out SingleSelection5 value)
		{
			if (this.Node.GrammarRule == g.Rule.SingleSelection5)
			{
				value = SingleSelection5.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SingleSelection5);
			return false;
		}

		// Token: 0x06007DD2 RID: 32210 RVA: 0x001A7F70 File Offset: 0x001A6170
		public SingleSelection5? As_SingleSelection5(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SingleSelection5)
			{
				return null;
			}
			return new SingleSelection5?(SingleSelection5.CreateUnsafe(this.Node));
		}

		// Token: 0x06007DD3 RID: 32211 RVA: 0x001A7FB0 File Offset: 0x001A61B0
		public SingleSelection5 Cast_SingleSelection5(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SingleSelection5)
			{
				return SingleSelection5.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SingleSelection5 is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007DD4 RID: 32212 RVA: 0x001A8005 File Offset: 0x001A6205
		public bool Is_DisjSelection5(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.DisjSelection5;
		}

		// Token: 0x06007DD5 RID: 32213 RVA: 0x001A801F File Offset: 0x001A621F
		public bool Is_DisjSelection5(GrammarBuilders g, out DisjSelection5 value)
		{
			if (this.Node.GrammarRule == g.Rule.DisjSelection5)
			{
				value = DisjSelection5.CreateUnsafe(this.Node);
				return true;
			}
			value = default(DisjSelection5);
			return false;
		}

		// Token: 0x06007DD6 RID: 32214 RVA: 0x001A8054 File Offset: 0x001A6254
		public DisjSelection5? As_DisjSelection5(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.DisjSelection5)
			{
				return null;
			}
			return new DisjSelection5?(DisjSelection5.CreateUnsafe(this.Node));
		}

		// Token: 0x06007DD7 RID: 32215 RVA: 0x001A8094 File Offset: 0x001A6294
		public DisjSelection5 Cast_DisjSelection5(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.DisjSelection5)
			{
				return DisjSelection5.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_DisjSelection5 is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007DD8 RID: 32216 RVA: 0x001A80EC File Offset: 0x001A62EC
		public T Switch<T>(GrammarBuilders g, Func<SingleSelection5, T> func0, Func<DisjSelection5, T> func1)
		{
			SingleSelection5 singleSelection;
			if (this.Is_SingleSelection5(g, out singleSelection))
			{
				return func0(singleSelection);
			}
			DisjSelection5 disjSelection;
			if (this.Is_DisjSelection5(g, out disjSelection))
			{
				return func1(disjSelection);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selection9");
		}

		// Token: 0x06007DD9 RID: 32217 RVA: 0x001A8144 File Offset: 0x001A6344
		public void Switch(GrammarBuilders g, Action<SingleSelection5> func0, Action<DisjSelection5> func1)
		{
			SingleSelection5 singleSelection;
			if (this.Is_SingleSelection5(g, out singleSelection))
			{
				func0(singleSelection);
				return;
			}
			DisjSelection5 disjSelection;
			if (this.Is_DisjSelection5(g, out disjSelection))
			{
				func1(disjSelection);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selection9");
		}

		// Token: 0x06007DDA RID: 32218 RVA: 0x001A819B File Offset: 0x001A639B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007DDB RID: 32219 RVA: 0x001A81B0 File Offset: 0x001A63B0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007DDC RID: 32220 RVA: 0x001A81DA File Offset: 0x001A63DA
		public bool Equals(selection9 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400338C RID: 13196
		private ProgramNode _node;
	}
}
