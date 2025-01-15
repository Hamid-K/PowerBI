using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015AC RID: 5548
	public struct number : IProgramNodeBuilder, IEquatable<number>
	{
		// Token: 0x17001FD2 RID: 8146
		// (get) Token: 0x0600B6D6 RID: 46806 RVA: 0x0027AC86 File Offset: 0x00278E86
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B6D7 RID: 46807 RVA: 0x0027AC8E File Offset: 0x00278E8E
		private number(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B6D8 RID: 46808 RVA: 0x0027AC97 File Offset: 0x00278E97
		public static number CreateUnsafe(ProgramNode node)
		{
			return new number(node);
		}

		// Token: 0x0600B6D9 RID: 46809 RVA: 0x0027ACA0 File Offset: 0x00278EA0
		public static number? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.number)
			{
				return null;
			}
			return new number?(number.CreateUnsafe(node));
		}

		// Token: 0x0600B6DA RID: 46810 RVA: 0x0027ACDA File Offset: 0x00278EDA
		public static number CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new number(new Hole(g.Symbol.number, holeId));
		}

		// Token: 0x0600B6DB RID: 46811 RVA: 0x0027ACF2 File Offset: 0x00278EF2
		public bool Is_number_number1(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.number_number1;
		}

		// Token: 0x0600B6DC RID: 46812 RVA: 0x0027AD0C File Offset: 0x00278F0C
		public bool Is_number_number1(GrammarBuilders g, out number_number1 value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.number_number1)
			{
				value = number_number1.CreateUnsafe(this.Node);
				return true;
			}
			value = default(number_number1);
			return false;
		}

		// Token: 0x0600B6DD RID: 46813 RVA: 0x0027AD44 File Offset: 0x00278F44
		public number_number1? As_number_number1(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.number_number1)
			{
				return null;
			}
			return new number_number1?(number_number1.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B6DE RID: 46814 RVA: 0x0027AD84 File Offset: 0x00278F84
		public number_number1 Cast_number_number1(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.number_number1)
			{
				return number_number1.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_number_number1 is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B6DF RID: 46815 RVA: 0x0027ADD9 File Offset: 0x00278FD9
		public bool Is_number_arithmetic(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.number_arithmetic;
		}

		// Token: 0x0600B6E0 RID: 46816 RVA: 0x0027ADF3 File Offset: 0x00278FF3
		public bool Is_number_arithmetic(GrammarBuilders g, out number_arithmetic value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.number_arithmetic)
			{
				value = number_arithmetic.CreateUnsafe(this.Node);
				return true;
			}
			value = default(number_arithmetic);
			return false;
		}

		// Token: 0x0600B6E1 RID: 46817 RVA: 0x0027AE28 File Offset: 0x00279028
		public number_arithmetic? As_number_arithmetic(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.number_arithmetic)
			{
				return null;
			}
			return new number_arithmetic?(number_arithmetic.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B6E2 RID: 46818 RVA: 0x0027AE68 File Offset: 0x00279068
		public number_arithmetic Cast_number_arithmetic(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.number_arithmetic)
			{
				return number_arithmetic.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_number_arithmetic is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B6E3 RID: 46819 RVA: 0x0027AEBD File Offset: 0x002790BD
		public bool Is_number_rowNumberTransform(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.number_rowNumberTransform;
		}

		// Token: 0x0600B6E4 RID: 46820 RVA: 0x0027AED7 File Offset: 0x002790D7
		public bool Is_number_rowNumberTransform(GrammarBuilders g, out number_rowNumberTransform value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.number_rowNumberTransform)
			{
				value = number_rowNumberTransform.CreateUnsafe(this.Node);
				return true;
			}
			value = default(number_rowNumberTransform);
			return false;
		}

		// Token: 0x0600B6E5 RID: 46821 RVA: 0x0027AF0C File Offset: 0x0027910C
		public number_rowNumberTransform? As_number_rowNumberTransform(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.number_rowNumberTransform)
			{
				return null;
			}
			return new number_rowNumberTransform?(number_rowNumberTransform.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B6E6 RID: 46822 RVA: 0x0027AF4C File Offset: 0x0027914C
		public number_rowNumberTransform Cast_number_rowNumberTransform(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.number_rowNumberTransform)
			{
				return number_rowNumberTransform.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_number_rowNumberTransform is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B6E7 RID: 46823 RVA: 0x0027AFA1 File Offset: 0x002791A1
		public bool Is_Length(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Length;
		}

		// Token: 0x0600B6E8 RID: 46824 RVA: 0x0027AFBB File Offset: 0x002791BB
		public bool Is_Length(GrammarBuilders g, out Length value)
		{
			if (this.Node.GrammarRule == g.Rule.Length)
			{
				value = Length.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Length);
			return false;
		}

		// Token: 0x0600B6E9 RID: 46825 RVA: 0x0027AFF0 File Offset: 0x002791F0
		public Length? As_Length(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Length)
			{
				return null;
			}
			return new Length?(Length.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B6EA RID: 46826 RVA: 0x0027B030 File Offset: 0x00279230
		public Length Cast_Length(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Length)
			{
				return Length.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Length is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B6EB RID: 46827 RVA: 0x0027B088 File Offset: 0x00279288
		public T Switch<T>(GrammarBuilders g, Func<number_number1, T> func0, Func<number_arithmetic, T> func1, Func<number_rowNumberTransform, T> func2, Func<Length, T> func3)
		{
			number_number1 number_number;
			if (this.Is_number_number1(g, out number_number))
			{
				return func0(number_number);
			}
			number_arithmetic number_arithmetic;
			if (this.Is_number_arithmetic(g, out number_arithmetic))
			{
				return func1(number_arithmetic);
			}
			number_rowNumberTransform number_rowNumberTransform;
			if (this.Is_number_rowNumberTransform(g, out number_rowNumberTransform))
			{
				return func2(number_rowNumberTransform);
			}
			Length length;
			if (this.Is_Length(g, out length))
			{
				return func3(length);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol number");
		}

		// Token: 0x0600B6EC RID: 46828 RVA: 0x0027B108 File Offset: 0x00279308
		public void Switch(GrammarBuilders g, Action<number_number1> func0, Action<number_arithmetic> func1, Action<number_rowNumberTransform> func2, Action<Length> func3)
		{
			number_number1 number_number;
			if (this.Is_number_number1(g, out number_number))
			{
				func0(number_number);
				return;
			}
			number_arithmetic number_arithmetic;
			if (this.Is_number_arithmetic(g, out number_arithmetic))
			{
				func1(number_arithmetic);
				return;
			}
			number_rowNumberTransform number_rowNumberTransform;
			if (this.Is_number_rowNumberTransform(g, out number_rowNumberTransform))
			{
				func2(number_rowNumberTransform);
				return;
			}
			Length length;
			if (this.Is_Length(g, out length))
			{
				func3(length);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol number");
		}

		// Token: 0x0600B6ED RID: 46829 RVA: 0x0027B187 File Offset: 0x00279387
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B6EE RID: 46830 RVA: 0x0027B19C File Offset: 0x0027939C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B6EF RID: 46831 RVA: 0x0027B1C6 File Offset: 0x002793C6
		public bool Equals(number other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400465A RID: 18010
		private ProgramNode _node;
	}
}
