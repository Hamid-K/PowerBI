using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015A3 RID: 5539
	public struct concatCase : IProgramNodeBuilder, IEquatable<concatCase>
	{
		// Token: 0x17001FC9 RID: 8137
		// (get) Token: 0x0600B60C RID: 46604 RVA: 0x002787C2 File Offset: 0x002769C2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B60D RID: 46605 RVA: 0x002787CA File Offset: 0x002769CA
		private concatCase(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B60E RID: 46606 RVA: 0x002787D3 File Offset: 0x002769D3
		public static concatCase CreateUnsafe(ProgramNode node)
		{
			return new concatCase(node);
		}

		// Token: 0x0600B60F RID: 46607 RVA: 0x002787DC File Offset: 0x002769DC
		public static concatCase? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.concatCase)
			{
				return null;
			}
			return new concatCase?(concatCase.CreateUnsafe(node));
		}

		// Token: 0x0600B610 RID: 46608 RVA: 0x00278816 File Offset: 0x00276A16
		public static concatCase CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new concatCase(new Hole(g.Symbol.concatCase, holeId));
		}

		// Token: 0x0600B611 RID: 46609 RVA: 0x0027882E File Offset: 0x00276A2E
		public bool Is_concatCase_concat(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.concatCase_concat;
		}

		// Token: 0x0600B612 RID: 46610 RVA: 0x00278848 File Offset: 0x00276A48
		public bool Is_concatCase_concat(GrammarBuilders g, out concatCase_concat value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.concatCase_concat)
			{
				value = concatCase_concat.CreateUnsafe(this.Node);
				return true;
			}
			value = default(concatCase_concat);
			return false;
		}

		// Token: 0x0600B613 RID: 46611 RVA: 0x00278880 File Offset: 0x00276A80
		public concatCase_concat? As_concatCase_concat(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.concatCase_concat)
			{
				return null;
			}
			return new concatCase_concat?(concatCase_concat.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B614 RID: 46612 RVA: 0x002788C0 File Offset: 0x00276AC0
		public concatCase_concat Cast_concatCase_concat(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.concatCase_concat)
			{
				return concatCase_concat.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_concatCase_concat is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B615 RID: 46613 RVA: 0x00278915 File Offset: 0x00276B15
		public bool Is_LowerCaseConcat(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LowerCaseConcat;
		}

		// Token: 0x0600B616 RID: 46614 RVA: 0x0027892F File Offset: 0x00276B2F
		public bool Is_LowerCaseConcat(GrammarBuilders g, out LowerCaseConcat value)
		{
			if (this.Node.GrammarRule == g.Rule.LowerCaseConcat)
			{
				value = LowerCaseConcat.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LowerCaseConcat);
			return false;
		}

		// Token: 0x0600B617 RID: 46615 RVA: 0x00278964 File Offset: 0x00276B64
		public LowerCaseConcat? As_LowerCaseConcat(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LowerCaseConcat)
			{
				return null;
			}
			return new LowerCaseConcat?(LowerCaseConcat.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B618 RID: 46616 RVA: 0x002789A4 File Offset: 0x00276BA4
		public LowerCaseConcat Cast_LowerCaseConcat(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LowerCaseConcat)
			{
				return LowerCaseConcat.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LowerCaseConcat is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B619 RID: 46617 RVA: 0x002789F9 File Offset: 0x00276BF9
		public bool Is_UpperCaseConcat(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.UpperCaseConcat;
		}

		// Token: 0x0600B61A RID: 46618 RVA: 0x00278A13 File Offset: 0x00276C13
		public bool Is_UpperCaseConcat(GrammarBuilders g, out UpperCaseConcat value)
		{
			if (this.Node.GrammarRule == g.Rule.UpperCaseConcat)
			{
				value = UpperCaseConcat.CreateUnsafe(this.Node);
				return true;
			}
			value = default(UpperCaseConcat);
			return false;
		}

		// Token: 0x0600B61B RID: 46619 RVA: 0x00278A48 File Offset: 0x00276C48
		public UpperCaseConcat? As_UpperCaseConcat(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.UpperCaseConcat)
			{
				return null;
			}
			return new UpperCaseConcat?(UpperCaseConcat.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B61C RID: 46620 RVA: 0x00278A88 File Offset: 0x00276C88
		public UpperCaseConcat Cast_UpperCaseConcat(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.UpperCaseConcat)
			{
				return UpperCaseConcat.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_UpperCaseConcat is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B61D RID: 46621 RVA: 0x00278ADD File Offset: 0x00276CDD
		public bool Is_ProperCaseConcat(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ProperCaseConcat;
		}

		// Token: 0x0600B61E RID: 46622 RVA: 0x00278AF7 File Offset: 0x00276CF7
		public bool Is_ProperCaseConcat(GrammarBuilders g, out ProperCaseConcat value)
		{
			if (this.Node.GrammarRule == g.Rule.ProperCaseConcat)
			{
				value = ProperCaseConcat.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ProperCaseConcat);
			return false;
		}

		// Token: 0x0600B61F RID: 46623 RVA: 0x00278B2C File Offset: 0x00276D2C
		public ProperCaseConcat? As_ProperCaseConcat(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ProperCaseConcat)
			{
				return null;
			}
			return new ProperCaseConcat?(ProperCaseConcat.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B620 RID: 46624 RVA: 0x00278B6C File Offset: 0x00276D6C
		public ProperCaseConcat Cast_ProperCaseConcat(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ProperCaseConcat)
			{
				return ProperCaseConcat.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ProperCaseConcat is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B621 RID: 46625 RVA: 0x00278BC4 File Offset: 0x00276DC4
		public T Switch<T>(GrammarBuilders g, Func<concatCase_concat, T> func0, Func<LowerCaseConcat, T> func1, Func<UpperCaseConcat, T> func2, Func<ProperCaseConcat, T> func3)
		{
			concatCase_concat concatCase_concat;
			if (this.Is_concatCase_concat(g, out concatCase_concat))
			{
				return func0(concatCase_concat);
			}
			LowerCaseConcat lowerCaseConcat;
			if (this.Is_LowerCaseConcat(g, out lowerCaseConcat))
			{
				return func1(lowerCaseConcat);
			}
			UpperCaseConcat upperCaseConcat;
			if (this.Is_UpperCaseConcat(g, out upperCaseConcat))
			{
				return func2(upperCaseConcat);
			}
			ProperCaseConcat properCaseConcat;
			if (this.Is_ProperCaseConcat(g, out properCaseConcat))
			{
				return func3(properCaseConcat);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol concatCase");
		}

		// Token: 0x0600B622 RID: 46626 RVA: 0x00278C44 File Offset: 0x00276E44
		public void Switch(GrammarBuilders g, Action<concatCase_concat> func0, Action<LowerCaseConcat> func1, Action<UpperCaseConcat> func2, Action<ProperCaseConcat> func3)
		{
			concatCase_concat concatCase_concat;
			if (this.Is_concatCase_concat(g, out concatCase_concat))
			{
				func0(concatCase_concat);
				return;
			}
			LowerCaseConcat lowerCaseConcat;
			if (this.Is_LowerCaseConcat(g, out lowerCaseConcat))
			{
				func1(lowerCaseConcat);
				return;
			}
			UpperCaseConcat upperCaseConcat;
			if (this.Is_UpperCaseConcat(g, out upperCaseConcat))
			{
				func2(upperCaseConcat);
				return;
			}
			ProperCaseConcat properCaseConcat;
			if (this.Is_ProperCaseConcat(g, out properCaseConcat))
			{
				func3(properCaseConcat);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol concatCase");
		}

		// Token: 0x0600B623 RID: 46627 RVA: 0x00278CC3 File Offset: 0x00276EC3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B624 RID: 46628 RVA: 0x00278CD8 File Offset: 0x00276ED8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B625 RID: 46629 RVA: 0x00278D02 File Offset: 0x00276F02
		public bool Equals(concatCase other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004651 RID: 18001
		private ProgramNode _node;
	}
}
