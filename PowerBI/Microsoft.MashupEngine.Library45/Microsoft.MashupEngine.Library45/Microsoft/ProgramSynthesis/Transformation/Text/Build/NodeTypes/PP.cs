using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C4C RID: 7244
	public struct PP : IProgramNodeBuilder, IEquatable<PP>
	{
		// Token: 0x170028E2 RID: 10466
		// (get) Token: 0x0600F4D8 RID: 62680 RVA: 0x003463EA File Offset: 0x003445EA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F4D9 RID: 62681 RVA: 0x003463F2 File Offset: 0x003445F2
		private PP(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F4DA RID: 62682 RVA: 0x003463FB File Offset: 0x003445FB
		public static PP CreateUnsafe(ProgramNode node)
		{
			return new PP(node);
		}

		// Token: 0x0600F4DB RID: 62683 RVA: 0x00346404 File Offset: 0x00344604
		public static PP? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.PP)
			{
				return null;
			}
			return new PP?(PP.CreateUnsafe(node));
		}

		// Token: 0x0600F4DC RID: 62684 RVA: 0x0034643E File Offset: 0x0034463E
		public static PP CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new PP(new Hole(g.Symbol.PP, holeId));
		}

		// Token: 0x0600F4DD RID: 62685 RVA: 0x00346456 File Offset: 0x00344656
		public bool Is_PosPair(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.PosPair;
		}

		// Token: 0x0600F4DE RID: 62686 RVA: 0x00346470 File Offset: 0x00344670
		public bool Is_PosPair(GrammarBuilders g, out PosPair value)
		{
			if (this.Node.GrammarRule == g.Rule.PosPair)
			{
				value = PosPair.CreateUnsafe(this.Node);
				return true;
			}
			value = default(PosPair);
			return false;
		}

		// Token: 0x0600F4DF RID: 62687 RVA: 0x003464A8 File Offset: 0x003446A8
		public PosPair? As_PosPair(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.PosPair)
			{
				return null;
			}
			return new PosPair?(PosPair.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F4E0 RID: 62688 RVA: 0x003464E8 File Offset: 0x003446E8
		public PosPair Cast_PosPair(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.PosPair)
			{
				return PosPair.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_PosPair is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F4E1 RID: 62689 RVA: 0x0034653D File Offset: 0x0034473D
		public bool Is_LetPL1(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LetPL1;
		}

		// Token: 0x0600F4E2 RID: 62690 RVA: 0x00346557 File Offset: 0x00344757
		public bool Is_LetPL1(GrammarBuilders g, out LetPL1 value)
		{
			if (this.Node.GrammarRule == g.Rule.LetPL1)
			{
				value = LetPL1.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LetPL1);
			return false;
		}

		// Token: 0x0600F4E3 RID: 62691 RVA: 0x0034658C File Offset: 0x0034478C
		public LetPL1? As_LetPL1(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LetPL1)
			{
				return null;
			}
			return new LetPL1?(LetPL1.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F4E4 RID: 62692 RVA: 0x003465CC File Offset: 0x003447CC
		public LetPL1 Cast_LetPL1(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LetPL1)
			{
				return LetPL1.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LetPL1 is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F4E5 RID: 62693 RVA: 0x00346621 File Offset: 0x00344821
		public bool Is_RegexPositionPair(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.RegexPositionPair;
		}

		// Token: 0x0600F4E6 RID: 62694 RVA: 0x0034663B File Offset: 0x0034483B
		public bool Is_RegexPositionPair(GrammarBuilders g, out RegexPositionPair value)
		{
			if (this.Node.GrammarRule == g.Rule.RegexPositionPair)
			{
				value = RegexPositionPair.CreateUnsafe(this.Node);
				return true;
			}
			value = default(RegexPositionPair);
			return false;
		}

		// Token: 0x0600F4E7 RID: 62695 RVA: 0x00346670 File Offset: 0x00344870
		public RegexPositionPair? As_RegexPositionPair(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.RegexPositionPair)
			{
				return null;
			}
			return new RegexPositionPair?(RegexPositionPair.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F4E8 RID: 62696 RVA: 0x003466B0 File Offset: 0x003448B0
		public RegexPositionPair Cast_RegexPositionPair(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.RegexPositionPair)
			{
				return RegexPositionPair.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_RegexPositionPair is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F4E9 RID: 62697 RVA: 0x00346705 File Offset: 0x00344905
		public bool Is_ExternalExtractorPositionPair(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ExternalExtractorPositionPair;
		}

		// Token: 0x0600F4EA RID: 62698 RVA: 0x0034671F File Offset: 0x0034491F
		public bool Is_ExternalExtractorPositionPair(GrammarBuilders g, out ExternalExtractorPositionPair value)
		{
			if (this.Node.GrammarRule == g.Rule.ExternalExtractorPositionPair)
			{
				value = ExternalExtractorPositionPair.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ExternalExtractorPositionPair);
			return false;
		}

		// Token: 0x0600F4EB RID: 62699 RVA: 0x00346754 File Offset: 0x00344954
		public ExternalExtractorPositionPair? As_ExternalExtractorPositionPair(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ExternalExtractorPositionPair)
			{
				return null;
			}
			return new ExternalExtractorPositionPair?(ExternalExtractorPositionPair.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F4EC RID: 62700 RVA: 0x00346794 File Offset: 0x00344994
		public ExternalExtractorPositionPair Cast_ExternalExtractorPositionPair(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ExternalExtractorPositionPair)
			{
				return ExternalExtractorPositionPair.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ExternalExtractorPositionPair is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F4ED RID: 62701 RVA: 0x003467EC File Offset: 0x003449EC
		public T Switch<T>(GrammarBuilders g, Func<PosPair, T> func0, Func<LetPL1, T> func1, Func<RegexPositionPair, T> func2, Func<ExternalExtractorPositionPair, T> func3)
		{
			PosPair posPair;
			if (this.Is_PosPair(g, out posPair))
			{
				return func0(posPair);
			}
			LetPL1 letPL;
			if (this.Is_LetPL1(g, out letPL))
			{
				return func1(letPL);
			}
			RegexPositionPair regexPositionPair;
			if (this.Is_RegexPositionPair(g, out regexPositionPair))
			{
				return func2(regexPositionPair);
			}
			ExternalExtractorPositionPair externalExtractorPositionPair;
			if (this.Is_ExternalExtractorPositionPair(g, out externalExtractorPositionPair))
			{
				return func3(externalExtractorPositionPair);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol PP");
		}

		// Token: 0x0600F4EE RID: 62702 RVA: 0x0034686C File Offset: 0x00344A6C
		public void Switch(GrammarBuilders g, Action<PosPair> func0, Action<LetPL1> func1, Action<RegexPositionPair> func2, Action<ExternalExtractorPositionPair> func3)
		{
			PosPair posPair;
			if (this.Is_PosPair(g, out posPair))
			{
				func0(posPair);
				return;
			}
			LetPL1 letPL;
			if (this.Is_LetPL1(g, out letPL))
			{
				func1(letPL);
				return;
			}
			RegexPositionPair regexPositionPair;
			if (this.Is_RegexPositionPair(g, out regexPositionPair))
			{
				func2(regexPositionPair);
				return;
			}
			ExternalExtractorPositionPair externalExtractorPositionPair;
			if (this.Is_ExternalExtractorPositionPair(g, out externalExtractorPositionPair))
			{
				func3(externalExtractorPositionPair);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol PP");
		}

		// Token: 0x0600F4EF RID: 62703 RVA: 0x003468EB File Offset: 0x00344AEB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F4F0 RID: 62704 RVA: 0x00346900 File Offset: 0x00344B00
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F4F1 RID: 62705 RVA: 0x0034692A File Offset: 0x00344B2A
		public bool Equals(PP other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B3B RID: 23355
		private ProgramNode _node;
	}
}
