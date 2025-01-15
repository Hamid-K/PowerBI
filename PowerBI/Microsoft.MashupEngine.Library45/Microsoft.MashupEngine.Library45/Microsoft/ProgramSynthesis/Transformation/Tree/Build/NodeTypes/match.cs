using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E7B RID: 7803
	public struct match : IProgramNodeBuilder, IEquatable<match>
	{
		// Token: 0x17002BD4 RID: 11220
		// (get) Token: 0x0601073C RID: 67388 RVA: 0x0038ADB6 File Offset: 0x00388FB6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0601073D RID: 67389 RVA: 0x0038ADBE File Offset: 0x00388FBE
		private match(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0601073E RID: 67390 RVA: 0x0038ADC7 File Offset: 0x00388FC7
		public static match CreateUnsafe(ProgramNode node)
		{
			return new match(node);
		}

		// Token: 0x0601073F RID: 67391 RVA: 0x0038ADD0 File Offset: 0x00388FD0
		public static match? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.match)
			{
				return null;
			}
			return new match?(match.CreateUnsafe(node));
		}

		// Token: 0x06010740 RID: 67392 RVA: 0x0038AE0A File Offset: 0x0038900A
		public static match CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new match(new Hole(g.Symbol.match, holeId));
		}

		// Token: 0x06010741 RID: 67393 RVA: 0x0038AE22 File Offset: 0x00389022
		public bool Is_match_pred(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.match_pred;
		}

		// Token: 0x06010742 RID: 67394 RVA: 0x0038AE3C File Offset: 0x0038903C
		public bool Is_match_pred(GrammarBuilders g, out match_pred value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.match_pred)
			{
				value = match_pred.CreateUnsafe(this.Node);
				return true;
			}
			value = default(match_pred);
			return false;
		}

		// Token: 0x06010743 RID: 67395 RVA: 0x0038AE74 File Offset: 0x00389074
		public match_pred? As_match_pred(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.match_pred)
			{
				return null;
			}
			return new match_pred?(match_pred.CreateUnsafe(this.Node));
		}

		// Token: 0x06010744 RID: 67396 RVA: 0x0038AEB4 File Offset: 0x003890B4
		public match_pred Cast_match_pred(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.match_pred)
			{
				return match_pred.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_match_pred is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06010745 RID: 67397 RVA: 0x0038AF09 File Offset: 0x00389109
		public bool Is_Conj(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Conj;
		}

		// Token: 0x06010746 RID: 67398 RVA: 0x0038AF23 File Offset: 0x00389123
		public bool Is_Conj(GrammarBuilders g, out Conj value)
		{
			if (this.Node.GrammarRule == g.Rule.Conj)
			{
				value = Conj.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Conj);
			return false;
		}

		// Token: 0x06010747 RID: 67399 RVA: 0x0038AF58 File Offset: 0x00389158
		public Conj? As_Conj(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Conj)
			{
				return null;
			}
			return new Conj?(Conj.CreateUnsafe(this.Node));
		}

		// Token: 0x06010748 RID: 67400 RVA: 0x0038AF98 File Offset: 0x00389198
		public Conj Cast_Conj(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Conj)
			{
				return Conj.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Conj is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06010749 RID: 67401 RVA: 0x0038AFF0 File Offset: 0x003891F0
		public T Switch<T>(GrammarBuilders g, Func<match_pred, T> func0, Func<Conj, T> func1)
		{
			match_pred match_pred;
			if (this.Is_match_pred(g, out match_pred))
			{
				return func0(match_pred);
			}
			Conj conj;
			if (this.Is_Conj(g, out conj))
			{
				return func1(conj);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol match");
		}

		// Token: 0x0601074A RID: 67402 RVA: 0x0038B048 File Offset: 0x00389248
		public void Switch(GrammarBuilders g, Action<match_pred> func0, Action<Conj> func1)
		{
			match_pred match_pred;
			if (this.Is_match_pred(g, out match_pred))
			{
				func0(match_pred);
				return;
			}
			Conj conj;
			if (this.Is_Conj(g, out conj))
			{
				func1(conj);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol match");
		}

		// Token: 0x0601074B RID: 67403 RVA: 0x0038B09F File Offset: 0x0038929F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0601074C RID: 67404 RVA: 0x0038B0B4 File Offset: 0x003892B4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0601074D RID: 67405 RVA: 0x0038B0DE File Offset: 0x003892DE
		public bool Equals(match other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062BA RID: 25274
		private ProgramNode _node;
	}
}
