using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015AE RID: 5550
	public struct arithmetic : IProgramNodeBuilder, IEquatable<arithmetic>
	{
		// Token: 0x17001FD4 RID: 8148
		// (get) Token: 0x0600B70A RID: 46858 RVA: 0x0027B72E File Offset: 0x0027992E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B70B RID: 46859 RVA: 0x0027B736 File Offset: 0x00279936
		private arithmetic(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B70C RID: 46860 RVA: 0x0027B73F File Offset: 0x0027993F
		public static arithmetic CreateUnsafe(ProgramNode node)
		{
			return new arithmetic(node);
		}

		// Token: 0x0600B70D RID: 46861 RVA: 0x0027B748 File Offset: 0x00279948
		public static arithmetic? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.arithmetic)
			{
				return null;
			}
			return new arithmetic?(arithmetic.CreateUnsafe(node));
		}

		// Token: 0x0600B70E RID: 46862 RVA: 0x0027B782 File Offset: 0x00279982
		public static arithmetic CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new arithmetic(new Hole(g.Symbol.arithmetic, holeId));
		}

		// Token: 0x0600B70F RID: 46863 RVA: 0x0027B79A File Offset: 0x0027999A
		public bool Is_Add(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Add;
		}

		// Token: 0x0600B710 RID: 46864 RVA: 0x0027B7B4 File Offset: 0x002799B4
		public bool Is_Add(GrammarBuilders g, out Add value)
		{
			if (this.Node.GrammarRule == g.Rule.Add)
			{
				value = Add.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Add);
			return false;
		}

		// Token: 0x0600B711 RID: 46865 RVA: 0x0027B7EC File Offset: 0x002799EC
		public Add? As_Add(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Add)
			{
				return null;
			}
			return new Add?(Add.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B712 RID: 46866 RVA: 0x0027B82C File Offset: 0x00279A2C
		public Add Cast_Add(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Add)
			{
				return Add.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Add is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B713 RID: 46867 RVA: 0x0027B881 File Offset: 0x00279A81
		public bool Is_Subtract(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Subtract;
		}

		// Token: 0x0600B714 RID: 46868 RVA: 0x0027B89B File Offset: 0x00279A9B
		public bool Is_Subtract(GrammarBuilders g, out Subtract value)
		{
			if (this.Node.GrammarRule == g.Rule.Subtract)
			{
				value = Subtract.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Subtract);
			return false;
		}

		// Token: 0x0600B715 RID: 46869 RVA: 0x0027B8D0 File Offset: 0x00279AD0
		public Subtract? As_Subtract(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Subtract)
			{
				return null;
			}
			return new Subtract?(Subtract.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B716 RID: 46870 RVA: 0x0027B910 File Offset: 0x00279B10
		public Subtract Cast_Subtract(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Subtract)
			{
				return Subtract.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Subtract is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B717 RID: 46871 RVA: 0x0027B965 File Offset: 0x00279B65
		public bool Is_Multiply(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Multiply;
		}

		// Token: 0x0600B718 RID: 46872 RVA: 0x0027B97F File Offset: 0x00279B7F
		public bool Is_Multiply(GrammarBuilders g, out Multiply value)
		{
			if (this.Node.GrammarRule == g.Rule.Multiply)
			{
				value = Multiply.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Multiply);
			return false;
		}

		// Token: 0x0600B719 RID: 46873 RVA: 0x0027B9B4 File Offset: 0x00279BB4
		public Multiply? As_Multiply(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Multiply)
			{
				return null;
			}
			return new Multiply?(Multiply.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B71A RID: 46874 RVA: 0x0027B9F4 File Offset: 0x00279BF4
		public Multiply Cast_Multiply(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Multiply)
			{
				return Multiply.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Multiply is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B71B RID: 46875 RVA: 0x0027BA49 File Offset: 0x00279C49
		public bool Is_Divide(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Divide;
		}

		// Token: 0x0600B71C RID: 46876 RVA: 0x0027BA63 File Offset: 0x00279C63
		public bool Is_Divide(GrammarBuilders g, out Divide value)
		{
			if (this.Node.GrammarRule == g.Rule.Divide)
			{
				value = Divide.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Divide);
			return false;
		}

		// Token: 0x0600B71D RID: 46877 RVA: 0x0027BA98 File Offset: 0x00279C98
		public Divide? As_Divide(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Divide)
			{
				return null;
			}
			return new Divide?(Divide.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B71E RID: 46878 RVA: 0x0027BAD8 File Offset: 0x00279CD8
		public Divide Cast_Divide(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Divide)
			{
				return Divide.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Divide is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B71F RID: 46879 RVA: 0x0027BB2D File Offset: 0x00279D2D
		public bool Is_Sum(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Sum;
		}

		// Token: 0x0600B720 RID: 46880 RVA: 0x0027BB47 File Offset: 0x00279D47
		public bool Is_Sum(GrammarBuilders g, out Sum value)
		{
			if (this.Node.GrammarRule == g.Rule.Sum)
			{
				value = Sum.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Sum);
			return false;
		}

		// Token: 0x0600B721 RID: 46881 RVA: 0x0027BB7C File Offset: 0x00279D7C
		public Sum? As_Sum(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Sum)
			{
				return null;
			}
			return new Sum?(Sum.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B722 RID: 46882 RVA: 0x0027BBBC File Offset: 0x00279DBC
		public Sum Cast_Sum(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Sum)
			{
				return Sum.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Sum is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B723 RID: 46883 RVA: 0x0027BC11 File Offset: 0x00279E11
		public bool Is_Product(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Product;
		}

		// Token: 0x0600B724 RID: 46884 RVA: 0x0027BC2B File Offset: 0x00279E2B
		public bool Is_Product(GrammarBuilders g, out Product value)
		{
			if (this.Node.GrammarRule == g.Rule.Product)
			{
				value = Product.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Product);
			return false;
		}

		// Token: 0x0600B725 RID: 46885 RVA: 0x0027BC60 File Offset: 0x00279E60
		public Product? As_Product(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Product)
			{
				return null;
			}
			return new Product?(Product.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B726 RID: 46886 RVA: 0x0027BCA0 File Offset: 0x00279EA0
		public Product Cast_Product(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Product)
			{
				return Product.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Product is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B727 RID: 46887 RVA: 0x0027BCF5 File Offset: 0x00279EF5
		public bool Is_Average(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Average;
		}

		// Token: 0x0600B728 RID: 46888 RVA: 0x0027BD0F File Offset: 0x00279F0F
		public bool Is_Average(GrammarBuilders g, out Average value)
		{
			if (this.Node.GrammarRule == g.Rule.Average)
			{
				value = Average.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Average);
			return false;
		}

		// Token: 0x0600B729 RID: 46889 RVA: 0x0027BD44 File Offset: 0x00279F44
		public Average? As_Average(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Average)
			{
				return null;
			}
			return new Average?(Average.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B72A RID: 46890 RVA: 0x0027BD84 File Offset: 0x00279F84
		public Average Cast_Average(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Average)
			{
				return Average.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Average is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B72B RID: 46891 RVA: 0x0027BDDC File Offset: 0x00279FDC
		public T Switch<T>(GrammarBuilders g, Func<Add, T> func0, Func<Subtract, T> func1, Func<Multiply, T> func2, Func<Divide, T> func3, Func<Sum, T> func4, Func<Product, T> func5, Func<Average, T> func6)
		{
			Add add;
			if (this.Is_Add(g, out add))
			{
				return func0(add);
			}
			Subtract subtract;
			if (this.Is_Subtract(g, out subtract))
			{
				return func1(subtract);
			}
			Multiply multiply;
			if (this.Is_Multiply(g, out multiply))
			{
				return func2(multiply);
			}
			Divide divide;
			if (this.Is_Divide(g, out divide))
			{
				return func3(divide);
			}
			Sum sum;
			if (this.Is_Sum(g, out sum))
			{
				return func4(sum);
			}
			Product product;
			if (this.Is_Product(g, out product))
			{
				return func5(product);
			}
			Average average;
			if (this.Is_Average(g, out average))
			{
				return func6(average);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol arithmetic");
		}

		// Token: 0x0600B72C RID: 46892 RVA: 0x0027BE9C File Offset: 0x0027A09C
		public void Switch(GrammarBuilders g, Action<Add> func0, Action<Subtract> func1, Action<Multiply> func2, Action<Divide> func3, Action<Sum> func4, Action<Product> func5, Action<Average> func6)
		{
			Add add;
			if (this.Is_Add(g, out add))
			{
				func0(add);
				return;
			}
			Subtract subtract;
			if (this.Is_Subtract(g, out subtract))
			{
				func1(subtract);
				return;
			}
			Multiply multiply;
			if (this.Is_Multiply(g, out multiply))
			{
				func2(multiply);
				return;
			}
			Divide divide;
			if (this.Is_Divide(g, out divide))
			{
				func3(divide);
				return;
			}
			Sum sum;
			if (this.Is_Sum(g, out sum))
			{
				func4(sum);
				return;
			}
			Product product;
			if (this.Is_Product(g, out product))
			{
				func5(product);
				return;
			}
			Average average;
			if (this.Is_Average(g, out average))
			{
				func6(average);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol arithmetic");
		}

		// Token: 0x0600B72D RID: 46893 RVA: 0x0027BF5A File Offset: 0x0027A15A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B72E RID: 46894 RVA: 0x0027BF70 File Offset: 0x0027A170
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B72F RID: 46895 RVA: 0x0027BF9A File Offset: 0x0027A19A
		public bool Equals(arithmetic other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400465C RID: 18012
		private ProgramNode _node;
	}
}
