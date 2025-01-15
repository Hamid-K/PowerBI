using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015B2 RID: 5554
	public struct multiplyRight : IProgramNodeBuilder, IEquatable<multiplyRight>
	{
		// Token: 0x17001FD8 RID: 8152
		// (get) Token: 0x0600B766 RID: 46950 RVA: 0x0027C962 File Offset: 0x0027AB62
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B767 RID: 46951 RVA: 0x0027C96A File Offset: 0x0027AB6A
		private multiplyRight(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B768 RID: 46952 RVA: 0x0027C973 File Offset: 0x0027AB73
		public static multiplyRight CreateUnsafe(ProgramNode node)
		{
			return new multiplyRight(node);
		}

		// Token: 0x0600B769 RID: 46953 RVA: 0x0027C97C File Offset: 0x0027AB7C
		public static multiplyRight? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.multiplyRight)
			{
				return null;
			}
			return new multiplyRight?(multiplyRight.CreateUnsafe(node));
		}

		// Token: 0x0600B76A RID: 46954 RVA: 0x0027C9B6 File Offset: 0x0027ABB6
		public static multiplyRight CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new multiplyRight(new Hole(g.Symbol.multiplyRight, holeId));
		}

		// Token: 0x0600B76B RID: 46955 RVA: 0x0027C9CE File Offset: 0x0027ABCE
		public bool Is_multiplyRight_arithmeticLeft(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.multiplyRight_arithmeticLeft;
		}

		// Token: 0x0600B76C RID: 46956 RVA: 0x0027C9E8 File Offset: 0x0027ABE8
		public bool Is_multiplyRight_arithmeticLeft(GrammarBuilders g, out multiplyRight_arithmeticLeft value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.multiplyRight_arithmeticLeft)
			{
				value = multiplyRight_arithmeticLeft.CreateUnsafe(this.Node);
				return true;
			}
			value = default(multiplyRight_arithmeticLeft);
			return false;
		}

		// Token: 0x0600B76D RID: 46957 RVA: 0x0027CA20 File Offset: 0x0027AC20
		public multiplyRight_arithmeticLeft? As_multiplyRight_arithmeticLeft(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.multiplyRight_arithmeticLeft)
			{
				return null;
			}
			return new multiplyRight_arithmeticLeft?(multiplyRight_arithmeticLeft.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B76E RID: 46958 RVA: 0x0027CA60 File Offset: 0x0027AC60
		public multiplyRight_arithmeticLeft Cast_multiplyRight_arithmeticLeft(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.multiplyRight_arithmeticLeft)
			{
				return multiplyRight_arithmeticLeft.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_multiplyRight_arithmeticLeft is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B76F RID: 46959 RVA: 0x0027CAB5 File Offset: 0x0027ACB5
		public bool Is_MultiplyRightNumber(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.MultiplyRightNumber;
		}

		// Token: 0x0600B770 RID: 46960 RVA: 0x0027CACF File Offset: 0x0027ACCF
		public bool Is_MultiplyRightNumber(GrammarBuilders g, out MultiplyRightNumber value)
		{
			if (this.Node.GrammarRule == g.Rule.MultiplyRightNumber)
			{
				value = MultiplyRightNumber.CreateUnsafe(this.Node);
				return true;
			}
			value = default(MultiplyRightNumber);
			return false;
		}

		// Token: 0x0600B771 RID: 46961 RVA: 0x0027CB04 File Offset: 0x0027AD04
		public MultiplyRightNumber? As_MultiplyRightNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.MultiplyRightNumber)
			{
				return null;
			}
			return new MultiplyRightNumber?(MultiplyRightNumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B772 RID: 46962 RVA: 0x0027CB44 File Offset: 0x0027AD44
		public MultiplyRightNumber Cast_MultiplyRightNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.MultiplyRightNumber)
			{
				return MultiplyRightNumber.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_MultiplyRightNumber is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B773 RID: 46963 RVA: 0x0027CB9C File Offset: 0x0027AD9C
		public T Switch<T>(GrammarBuilders g, Func<multiplyRight_arithmeticLeft, T> func0, Func<MultiplyRightNumber, T> func1)
		{
			multiplyRight_arithmeticLeft multiplyRight_arithmeticLeft;
			if (this.Is_multiplyRight_arithmeticLeft(g, out multiplyRight_arithmeticLeft))
			{
				return func0(multiplyRight_arithmeticLeft);
			}
			MultiplyRightNumber multiplyRightNumber;
			if (this.Is_MultiplyRightNumber(g, out multiplyRightNumber))
			{
				return func1(multiplyRightNumber);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol multiplyRight");
		}

		// Token: 0x0600B774 RID: 46964 RVA: 0x0027CBF4 File Offset: 0x0027ADF4
		public void Switch(GrammarBuilders g, Action<multiplyRight_arithmeticLeft> func0, Action<MultiplyRightNumber> func1)
		{
			multiplyRight_arithmeticLeft multiplyRight_arithmeticLeft;
			if (this.Is_multiplyRight_arithmeticLeft(g, out multiplyRight_arithmeticLeft))
			{
				func0(multiplyRight_arithmeticLeft);
				return;
			}
			MultiplyRightNumber multiplyRightNumber;
			if (this.Is_MultiplyRightNumber(g, out multiplyRightNumber))
			{
				func1(multiplyRightNumber);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol multiplyRight");
		}

		// Token: 0x0600B775 RID: 46965 RVA: 0x0027CC4B File Offset: 0x0027AE4B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B776 RID: 46966 RVA: 0x0027CC60 File Offset: 0x0027AE60
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B777 RID: 46967 RVA: 0x0027CC8A File Offset: 0x0027AE8A
		public bool Equals(multiplyRight other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004660 RID: 18016
		private ProgramNode _node;
	}
}
