using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015B3 RID: 5555
	public struct divideRight : IProgramNodeBuilder, IEquatable<divideRight>
	{
		// Token: 0x17001FD9 RID: 8153
		// (get) Token: 0x0600B778 RID: 46968 RVA: 0x0027CC9E File Offset: 0x0027AE9E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B779 RID: 46969 RVA: 0x0027CCA6 File Offset: 0x0027AEA6
		private divideRight(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B77A RID: 46970 RVA: 0x0027CCAF File Offset: 0x0027AEAF
		public static divideRight CreateUnsafe(ProgramNode node)
		{
			return new divideRight(node);
		}

		// Token: 0x0600B77B RID: 46971 RVA: 0x0027CCB8 File Offset: 0x0027AEB8
		public static divideRight? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.divideRight)
			{
				return null;
			}
			return new divideRight?(divideRight.CreateUnsafe(node));
		}

		// Token: 0x0600B77C RID: 46972 RVA: 0x0027CCF2 File Offset: 0x0027AEF2
		public static divideRight CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new divideRight(new Hole(g.Symbol.divideRight, holeId));
		}

		// Token: 0x0600B77D RID: 46973 RVA: 0x0027CD0A File Offset: 0x0027AF0A
		public bool Is_divideRight_arithmeticLeft(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.divideRight_arithmeticLeft;
		}

		// Token: 0x0600B77E RID: 46974 RVA: 0x0027CD24 File Offset: 0x0027AF24
		public bool Is_divideRight_arithmeticLeft(GrammarBuilders g, out divideRight_arithmeticLeft value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.divideRight_arithmeticLeft)
			{
				value = divideRight_arithmeticLeft.CreateUnsafe(this.Node);
				return true;
			}
			value = default(divideRight_arithmeticLeft);
			return false;
		}

		// Token: 0x0600B77F RID: 46975 RVA: 0x0027CD5C File Offset: 0x0027AF5C
		public divideRight_arithmeticLeft? As_divideRight_arithmeticLeft(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.divideRight_arithmeticLeft)
			{
				return null;
			}
			return new divideRight_arithmeticLeft?(divideRight_arithmeticLeft.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B780 RID: 46976 RVA: 0x0027CD9C File Offset: 0x0027AF9C
		public divideRight_arithmeticLeft Cast_divideRight_arithmeticLeft(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.divideRight_arithmeticLeft)
			{
				return divideRight_arithmeticLeft.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_divideRight_arithmeticLeft is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B781 RID: 46977 RVA: 0x0027CDF1 File Offset: 0x0027AFF1
		public bool Is_DivideRightNumber(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.DivideRightNumber;
		}

		// Token: 0x0600B782 RID: 46978 RVA: 0x0027CE0B File Offset: 0x0027B00B
		public bool Is_DivideRightNumber(GrammarBuilders g, out DivideRightNumber value)
		{
			if (this.Node.GrammarRule == g.Rule.DivideRightNumber)
			{
				value = DivideRightNumber.CreateUnsafe(this.Node);
				return true;
			}
			value = default(DivideRightNumber);
			return false;
		}

		// Token: 0x0600B783 RID: 46979 RVA: 0x0027CE40 File Offset: 0x0027B040
		public DivideRightNumber? As_DivideRightNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.DivideRightNumber)
			{
				return null;
			}
			return new DivideRightNumber?(DivideRightNumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B784 RID: 46980 RVA: 0x0027CE80 File Offset: 0x0027B080
		public DivideRightNumber Cast_DivideRightNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.DivideRightNumber)
			{
				return DivideRightNumber.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_DivideRightNumber is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B785 RID: 46981 RVA: 0x0027CED8 File Offset: 0x0027B0D8
		public T Switch<T>(GrammarBuilders g, Func<divideRight_arithmeticLeft, T> func0, Func<DivideRightNumber, T> func1)
		{
			divideRight_arithmeticLeft divideRight_arithmeticLeft;
			if (this.Is_divideRight_arithmeticLeft(g, out divideRight_arithmeticLeft))
			{
				return func0(divideRight_arithmeticLeft);
			}
			DivideRightNumber divideRightNumber;
			if (this.Is_DivideRightNumber(g, out divideRightNumber))
			{
				return func1(divideRightNumber);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol divideRight");
		}

		// Token: 0x0600B786 RID: 46982 RVA: 0x0027CF30 File Offset: 0x0027B130
		public void Switch(GrammarBuilders g, Action<divideRight_arithmeticLeft> func0, Action<DivideRightNumber> func1)
		{
			divideRight_arithmeticLeft divideRight_arithmeticLeft;
			if (this.Is_divideRight_arithmeticLeft(g, out divideRight_arithmeticLeft))
			{
				func0(divideRight_arithmeticLeft);
				return;
			}
			DivideRightNumber divideRightNumber;
			if (this.Is_DivideRightNumber(g, out divideRightNumber))
			{
				func1(divideRightNumber);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol divideRight");
		}

		// Token: 0x0600B787 RID: 46983 RVA: 0x0027CF87 File Offset: 0x0027B187
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B788 RID: 46984 RVA: 0x0027CF9C File Offset: 0x0027B19C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B789 RID: 46985 RVA: 0x0027CFC6 File Offset: 0x0027B1C6
		public bool Equals(divideRight other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004661 RID: 18017
		private ProgramNode _node;
	}
}
