using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x0200159F RID: 5535
	public struct segmentCase : IProgramNodeBuilder, IEquatable<segmentCase>
	{
		// Token: 0x17001FC5 RID: 8133
		// (get) Token: 0x0600B5BC RID: 46524 RVA: 0x002778BA File Offset: 0x00275ABA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B5BD RID: 46525 RVA: 0x002778C2 File Offset: 0x00275AC2
		private segmentCase(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B5BE RID: 46526 RVA: 0x002778CB File Offset: 0x00275ACB
		public static segmentCase CreateUnsafe(ProgramNode node)
		{
			return new segmentCase(node);
		}

		// Token: 0x0600B5BF RID: 46527 RVA: 0x002778D4 File Offset: 0x00275AD4
		public static segmentCase? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.segmentCase)
			{
				return null;
			}
			return new segmentCase?(segmentCase.CreateUnsafe(node));
		}

		// Token: 0x0600B5C0 RID: 46528 RVA: 0x0027790E File Offset: 0x00275B0E
		public static segmentCase CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new segmentCase(new Hole(g.Symbol.segmentCase, holeId));
		}

		// Token: 0x0600B5C1 RID: 46529 RVA: 0x00277926 File Offset: 0x00275B26
		public bool Is_segmentCase_segment(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.segmentCase_segment;
		}

		// Token: 0x0600B5C2 RID: 46530 RVA: 0x00277940 File Offset: 0x00275B40
		public bool Is_segmentCase_segment(GrammarBuilders g, out segmentCase_segment value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.segmentCase_segment)
			{
				value = segmentCase_segment.CreateUnsafe(this.Node);
				return true;
			}
			value = default(segmentCase_segment);
			return false;
		}

		// Token: 0x0600B5C3 RID: 46531 RVA: 0x00277978 File Offset: 0x00275B78
		public segmentCase_segment? As_segmentCase_segment(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.segmentCase_segment)
			{
				return null;
			}
			return new segmentCase_segment?(segmentCase_segment.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B5C4 RID: 46532 RVA: 0x002779B8 File Offset: 0x00275BB8
		public segmentCase_segment Cast_segmentCase_segment(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.segmentCase_segment)
			{
				return segmentCase_segment.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_segmentCase_segment is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B5C5 RID: 46533 RVA: 0x00277A0D File Offset: 0x00275C0D
		public bool Is_LowerCase(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LowerCase;
		}

		// Token: 0x0600B5C6 RID: 46534 RVA: 0x00277A27 File Offset: 0x00275C27
		public bool Is_LowerCase(GrammarBuilders g, out LowerCase value)
		{
			if (this.Node.GrammarRule == g.Rule.LowerCase)
			{
				value = LowerCase.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LowerCase);
			return false;
		}

		// Token: 0x0600B5C7 RID: 46535 RVA: 0x00277A5C File Offset: 0x00275C5C
		public LowerCase? As_LowerCase(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LowerCase)
			{
				return null;
			}
			return new LowerCase?(LowerCase.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B5C8 RID: 46536 RVA: 0x00277A9C File Offset: 0x00275C9C
		public LowerCase Cast_LowerCase(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LowerCase)
			{
				return LowerCase.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LowerCase is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B5C9 RID: 46537 RVA: 0x00277AF1 File Offset: 0x00275CF1
		public bool Is_UpperCase(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.UpperCase;
		}

		// Token: 0x0600B5CA RID: 46538 RVA: 0x00277B0B File Offset: 0x00275D0B
		public bool Is_UpperCase(GrammarBuilders g, out UpperCase value)
		{
			if (this.Node.GrammarRule == g.Rule.UpperCase)
			{
				value = UpperCase.CreateUnsafe(this.Node);
				return true;
			}
			value = default(UpperCase);
			return false;
		}

		// Token: 0x0600B5CB RID: 46539 RVA: 0x00277B40 File Offset: 0x00275D40
		public UpperCase? As_UpperCase(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.UpperCase)
			{
				return null;
			}
			return new UpperCase?(UpperCase.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B5CC RID: 46540 RVA: 0x00277B80 File Offset: 0x00275D80
		public UpperCase Cast_UpperCase(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.UpperCase)
			{
				return UpperCase.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_UpperCase is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B5CD RID: 46541 RVA: 0x00277BD5 File Offset: 0x00275DD5
		public bool Is_ProperCase(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ProperCase;
		}

		// Token: 0x0600B5CE RID: 46542 RVA: 0x00277BEF File Offset: 0x00275DEF
		public bool Is_ProperCase(GrammarBuilders g, out ProperCase value)
		{
			if (this.Node.GrammarRule == g.Rule.ProperCase)
			{
				value = ProperCase.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ProperCase);
			return false;
		}

		// Token: 0x0600B5CF RID: 46543 RVA: 0x00277C24 File Offset: 0x00275E24
		public ProperCase? As_ProperCase(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ProperCase)
			{
				return null;
			}
			return new ProperCase?(ProperCase.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B5D0 RID: 46544 RVA: 0x00277C64 File Offset: 0x00275E64
		public ProperCase Cast_ProperCase(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ProperCase)
			{
				return ProperCase.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ProperCase is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B5D1 RID: 46545 RVA: 0x00277CBC File Offset: 0x00275EBC
		public T Switch<T>(GrammarBuilders g, Func<segmentCase_segment, T> func0, Func<LowerCase, T> func1, Func<UpperCase, T> func2, Func<ProperCase, T> func3)
		{
			segmentCase_segment segmentCase_segment;
			if (this.Is_segmentCase_segment(g, out segmentCase_segment))
			{
				return func0(segmentCase_segment);
			}
			LowerCase lowerCase;
			if (this.Is_LowerCase(g, out lowerCase))
			{
				return func1(lowerCase);
			}
			UpperCase upperCase;
			if (this.Is_UpperCase(g, out upperCase))
			{
				return func2(upperCase);
			}
			ProperCase properCase;
			if (this.Is_ProperCase(g, out properCase))
			{
				return func3(properCase);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol segmentCase");
		}

		// Token: 0x0600B5D2 RID: 46546 RVA: 0x00277D3C File Offset: 0x00275F3C
		public void Switch(GrammarBuilders g, Action<segmentCase_segment> func0, Action<LowerCase> func1, Action<UpperCase> func2, Action<ProperCase> func3)
		{
			segmentCase_segment segmentCase_segment;
			if (this.Is_segmentCase_segment(g, out segmentCase_segment))
			{
				func0(segmentCase_segment);
				return;
			}
			LowerCase lowerCase;
			if (this.Is_LowerCase(g, out lowerCase))
			{
				func1(lowerCase);
				return;
			}
			UpperCase upperCase;
			if (this.Is_UpperCase(g, out upperCase))
			{
				func2(upperCase);
				return;
			}
			ProperCase properCase;
			if (this.Is_ProperCase(g, out properCase))
			{
				func3(properCase);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol segmentCase");
		}

		// Token: 0x0600B5D3 RID: 46547 RVA: 0x00277DBB File Offset: 0x00275FBB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B5D4 RID: 46548 RVA: 0x00277DD0 File Offset: 0x00275FD0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B5D5 RID: 46549 RVA: 0x00277DFA File Offset: 0x00275FFA
		public bool Equals(segmentCase other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400464D RID: 17997
		private ProgramNode _node;
	}
}
