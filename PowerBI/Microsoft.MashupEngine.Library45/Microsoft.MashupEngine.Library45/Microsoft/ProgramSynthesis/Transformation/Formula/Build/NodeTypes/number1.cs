using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015AD RID: 5549
	public struct number1 : IProgramNodeBuilder, IEquatable<number1>
	{
		// Token: 0x17001FD3 RID: 8147
		// (get) Token: 0x0600B6F0 RID: 46832 RVA: 0x0027B1DA File Offset: 0x002793DA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B6F1 RID: 46833 RVA: 0x0027B1E2 File Offset: 0x002793E2
		private number1(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B6F2 RID: 46834 RVA: 0x0027B1EB File Offset: 0x002793EB
		public static number1 CreateUnsafe(ProgramNode node)
		{
			return new number1(node);
		}

		// Token: 0x0600B6F3 RID: 46835 RVA: 0x0027B1F4 File Offset: 0x002793F4
		public static number1? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.number1)
			{
				return null;
			}
			return new number1?(number1.CreateUnsafe(node));
		}

		// Token: 0x0600B6F4 RID: 46836 RVA: 0x0027B22E File Offset: 0x0027942E
		public static number1 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new number1(new Hole(g.Symbol.number1, holeId));
		}

		// Token: 0x0600B6F5 RID: 46837 RVA: 0x0027B246 File Offset: 0x00279446
		public bool Is_number1_inumber(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.number1_inumber;
		}

		// Token: 0x0600B6F6 RID: 46838 RVA: 0x0027B260 File Offset: 0x00279460
		public bool Is_number1_inumber(GrammarBuilders g, out number1_inumber value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.number1_inumber)
			{
				value = number1_inumber.CreateUnsafe(this.Node);
				return true;
			}
			value = default(number1_inumber);
			return false;
		}

		// Token: 0x0600B6F7 RID: 46839 RVA: 0x0027B298 File Offset: 0x00279498
		public number1_inumber? As_number1_inumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.number1_inumber)
			{
				return null;
			}
			return new number1_inumber?(number1_inumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B6F8 RID: 46840 RVA: 0x0027B2D8 File Offset: 0x002794D8
		public number1_inumber Cast_number1_inumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.number1_inumber)
			{
				return number1_inumber.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_number1_inumber is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B6F9 RID: 46841 RVA: 0x0027B32D File Offset: 0x0027952D
		public bool Is_DateTimePart(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.DateTimePart;
		}

		// Token: 0x0600B6FA RID: 46842 RVA: 0x0027B347 File Offset: 0x00279547
		public bool Is_DateTimePart(GrammarBuilders g, out DateTimePart value)
		{
			if (this.Node.GrammarRule == g.Rule.DateTimePart)
			{
				value = DateTimePart.CreateUnsafe(this.Node);
				return true;
			}
			value = default(DateTimePart);
			return false;
		}

		// Token: 0x0600B6FB RID: 46843 RVA: 0x0027B37C File Offset: 0x0027957C
		public DateTimePart? As_DateTimePart(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.DateTimePart)
			{
				return null;
			}
			return new DateTimePart?(DateTimePart.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B6FC RID: 46844 RVA: 0x0027B3BC File Offset: 0x002795BC
		public DateTimePart Cast_DateTimePart(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.DateTimePart)
			{
				return DateTimePart.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_DateTimePart is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B6FD RID: 46845 RVA: 0x0027B411 File Offset: 0x00279611
		public bool Is_TimePart(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TimePart;
		}

		// Token: 0x0600B6FE RID: 46846 RVA: 0x0027B42B File Offset: 0x0027962B
		public bool Is_TimePart(GrammarBuilders g, out TimePart value)
		{
			if (this.Node.GrammarRule == g.Rule.TimePart)
			{
				value = TimePart.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TimePart);
			return false;
		}

		// Token: 0x0600B6FF RID: 46847 RVA: 0x0027B460 File Offset: 0x00279660
		public TimePart? As_TimePart(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TimePart)
			{
				return null;
			}
			return new TimePart?(TimePart.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B700 RID: 46848 RVA: 0x0027B4A0 File Offset: 0x002796A0
		public TimePart Cast_TimePart(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TimePart)
			{
				return TimePart.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TimePart is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B701 RID: 46849 RVA: 0x0027B4F5 File Offset: 0x002796F5
		public bool Is_RoundNumber(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.RoundNumber;
		}

		// Token: 0x0600B702 RID: 46850 RVA: 0x0027B50F File Offset: 0x0027970F
		public bool Is_RoundNumber(GrammarBuilders g, out RoundNumber value)
		{
			if (this.Node.GrammarRule == g.Rule.RoundNumber)
			{
				value = RoundNumber.CreateUnsafe(this.Node);
				return true;
			}
			value = default(RoundNumber);
			return false;
		}

		// Token: 0x0600B703 RID: 46851 RVA: 0x0027B544 File Offset: 0x00279744
		public RoundNumber? As_RoundNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.RoundNumber)
			{
				return null;
			}
			return new RoundNumber?(RoundNumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B704 RID: 46852 RVA: 0x0027B584 File Offset: 0x00279784
		public RoundNumber Cast_RoundNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.RoundNumber)
			{
				return RoundNumber.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_RoundNumber is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B705 RID: 46853 RVA: 0x0027B5DC File Offset: 0x002797DC
		public T Switch<T>(GrammarBuilders g, Func<number1_inumber, T> func0, Func<DateTimePart, T> func1, Func<TimePart, T> func2, Func<RoundNumber, T> func3)
		{
			number1_inumber number1_inumber;
			if (this.Is_number1_inumber(g, out number1_inumber))
			{
				return func0(number1_inumber);
			}
			DateTimePart dateTimePart;
			if (this.Is_DateTimePart(g, out dateTimePart))
			{
				return func1(dateTimePart);
			}
			TimePart timePart;
			if (this.Is_TimePart(g, out timePart))
			{
				return func2(timePart);
			}
			RoundNumber roundNumber;
			if (this.Is_RoundNumber(g, out roundNumber))
			{
				return func3(roundNumber);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol number1");
		}

		// Token: 0x0600B706 RID: 46854 RVA: 0x0027B65C File Offset: 0x0027985C
		public void Switch(GrammarBuilders g, Action<number1_inumber> func0, Action<DateTimePart> func1, Action<TimePart> func2, Action<RoundNumber> func3)
		{
			number1_inumber number1_inumber;
			if (this.Is_number1_inumber(g, out number1_inumber))
			{
				func0(number1_inumber);
				return;
			}
			DateTimePart dateTimePart;
			if (this.Is_DateTimePart(g, out dateTimePart))
			{
				func1(dateTimePart);
				return;
			}
			TimePart timePart;
			if (this.Is_TimePart(g, out timePart))
			{
				func2(timePart);
				return;
			}
			RoundNumber roundNumber;
			if (this.Is_RoundNumber(g, out roundNumber))
			{
				func3(roundNumber);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol number1");
		}

		// Token: 0x0600B707 RID: 46855 RVA: 0x0027B6DB File Offset: 0x002798DB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B708 RID: 46856 RVA: 0x0027B6F0 File Offset: 0x002798F0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B709 RID: 46857 RVA: 0x0027B71A File Offset: 0x0027991A
		public bool Equals(number1 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400465B RID: 18011
		private ProgramNode _node;
	}
}
