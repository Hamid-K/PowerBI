using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001080 RID: 4224
	public struct substringDisj : IProgramNodeBuilder, IEquatable<substringDisj>
	{
		// Token: 0x17001669 RID: 5737
		// (get) Token: 0x06007EBB RID: 32443 RVA: 0x001AA54E File Offset: 0x001A874E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007EBC RID: 32444 RVA: 0x001AA556 File Offset: 0x001A8756
		private substringDisj(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007EBD RID: 32445 RVA: 0x001AA55F File Offset: 0x001A875F
		public static substringDisj CreateUnsafe(ProgramNode node)
		{
			return new substringDisj(node);
		}

		// Token: 0x06007EBE RID: 32446 RVA: 0x001AA568 File Offset: 0x001A8768
		public static substringDisj? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.substringDisj)
			{
				return null;
			}
			return new substringDisj?(substringDisj.CreateUnsafe(node));
		}

		// Token: 0x06007EBF RID: 32447 RVA: 0x001AA5A2 File Offset: 0x001A87A2
		public static substringDisj CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new substringDisj(new Hole(g.Symbol.substringDisj, holeId));
		}

		// Token: 0x06007EC0 RID: 32448 RVA: 0x001AA5BA File Offset: 0x001A87BA
		public bool Is_SingleSubstring(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SingleSubstring;
		}

		// Token: 0x06007EC1 RID: 32449 RVA: 0x001AA5D4 File Offset: 0x001A87D4
		public bool Is_SingleSubstring(GrammarBuilders g, out SingleSubstring value)
		{
			if (this.Node.GrammarRule == g.Rule.SingleSubstring)
			{
				value = SingleSubstring.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SingleSubstring);
			return false;
		}

		// Token: 0x06007EC2 RID: 32450 RVA: 0x001AA60C File Offset: 0x001A880C
		public SingleSubstring? As_SingleSubstring(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SingleSubstring)
			{
				return null;
			}
			return new SingleSubstring?(SingleSubstring.CreateUnsafe(this.Node));
		}

		// Token: 0x06007EC3 RID: 32451 RVA: 0x001AA64C File Offset: 0x001A884C
		public SingleSubstring Cast_SingleSubstring(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SingleSubstring)
			{
				return SingleSubstring.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SingleSubstring is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007EC4 RID: 32452 RVA: 0x001AA6A1 File Offset: 0x001A88A1
		public bool Is_DisjSubstring(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.DisjSubstring;
		}

		// Token: 0x06007EC5 RID: 32453 RVA: 0x001AA6BB File Offset: 0x001A88BB
		public bool Is_DisjSubstring(GrammarBuilders g, out DisjSubstring value)
		{
			if (this.Node.GrammarRule == g.Rule.DisjSubstring)
			{
				value = DisjSubstring.CreateUnsafe(this.Node);
				return true;
			}
			value = default(DisjSubstring);
			return false;
		}

		// Token: 0x06007EC6 RID: 32454 RVA: 0x001AA6F0 File Offset: 0x001A88F0
		public DisjSubstring? As_DisjSubstring(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.DisjSubstring)
			{
				return null;
			}
			return new DisjSubstring?(DisjSubstring.CreateUnsafe(this.Node));
		}

		// Token: 0x06007EC7 RID: 32455 RVA: 0x001AA730 File Offset: 0x001A8930
		public DisjSubstring Cast_DisjSubstring(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.DisjSubstring)
			{
				return DisjSubstring.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_DisjSubstring is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007EC8 RID: 32456 RVA: 0x001AA788 File Offset: 0x001A8988
		public T Switch<T>(GrammarBuilders g, Func<SingleSubstring, T> func0, Func<DisjSubstring, T> func1)
		{
			SingleSubstring singleSubstring;
			if (this.Is_SingleSubstring(g, out singleSubstring))
			{
				return func0(singleSubstring);
			}
			DisjSubstring disjSubstring;
			if (this.Is_DisjSubstring(g, out disjSubstring))
			{
				return func1(disjSubstring);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol substringDisj");
		}

		// Token: 0x06007EC9 RID: 32457 RVA: 0x001AA7E0 File Offset: 0x001A89E0
		public void Switch(GrammarBuilders g, Action<SingleSubstring> func0, Action<DisjSubstring> func1)
		{
			SingleSubstring singleSubstring;
			if (this.Is_SingleSubstring(g, out singleSubstring))
			{
				func0(singleSubstring);
				return;
			}
			DisjSubstring disjSubstring;
			if (this.Is_DisjSubstring(g, out disjSubstring))
			{
				func1(disjSubstring);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol substringDisj");
		}

		// Token: 0x06007ECA RID: 32458 RVA: 0x001AA837 File Offset: 0x001A8A37
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007ECB RID: 32459 RVA: 0x001AA84C File Offset: 0x001A8A4C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007ECC RID: 32460 RVA: 0x001AA876 File Offset: 0x001A8A76
		public bool Equals(substringDisj other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003399 RID: 13209
		private ProgramNode _node;
	}
}
