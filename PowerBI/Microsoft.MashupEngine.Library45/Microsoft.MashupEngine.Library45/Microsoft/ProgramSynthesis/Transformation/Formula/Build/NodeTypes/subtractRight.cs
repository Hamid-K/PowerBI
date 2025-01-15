using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015B1 RID: 5553
	public struct subtractRight : IProgramNodeBuilder, IEquatable<subtractRight>
	{
		// Token: 0x17001FD7 RID: 8151
		// (get) Token: 0x0600B754 RID: 46932 RVA: 0x0027C626 File Offset: 0x0027A826
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B755 RID: 46933 RVA: 0x0027C62E File Offset: 0x0027A82E
		private subtractRight(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B756 RID: 46934 RVA: 0x0027C637 File Offset: 0x0027A837
		public static subtractRight CreateUnsafe(ProgramNode node)
		{
			return new subtractRight(node);
		}

		// Token: 0x0600B757 RID: 46935 RVA: 0x0027C640 File Offset: 0x0027A840
		public static subtractRight? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.subtractRight)
			{
				return null;
			}
			return new subtractRight?(subtractRight.CreateUnsafe(node));
		}

		// Token: 0x0600B758 RID: 46936 RVA: 0x0027C67A File Offset: 0x0027A87A
		public static subtractRight CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new subtractRight(new Hole(g.Symbol.subtractRight, holeId));
		}

		// Token: 0x0600B759 RID: 46937 RVA: 0x0027C692 File Offset: 0x0027A892
		public bool Is_subtractRight_arithmeticLeft(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.subtractRight_arithmeticLeft;
		}

		// Token: 0x0600B75A RID: 46938 RVA: 0x0027C6AC File Offset: 0x0027A8AC
		public bool Is_subtractRight_arithmeticLeft(GrammarBuilders g, out subtractRight_arithmeticLeft value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.subtractRight_arithmeticLeft)
			{
				value = subtractRight_arithmeticLeft.CreateUnsafe(this.Node);
				return true;
			}
			value = default(subtractRight_arithmeticLeft);
			return false;
		}

		// Token: 0x0600B75B RID: 46939 RVA: 0x0027C6E4 File Offset: 0x0027A8E4
		public subtractRight_arithmeticLeft? As_subtractRight_arithmeticLeft(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.subtractRight_arithmeticLeft)
			{
				return null;
			}
			return new subtractRight_arithmeticLeft?(subtractRight_arithmeticLeft.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B75C RID: 46940 RVA: 0x0027C724 File Offset: 0x0027A924
		public subtractRight_arithmeticLeft Cast_subtractRight_arithmeticLeft(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.subtractRight_arithmeticLeft)
			{
				return subtractRight_arithmeticLeft.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_subtractRight_arithmeticLeft is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B75D RID: 46941 RVA: 0x0027C779 File Offset: 0x0027A979
		public bool Is_SubtractRightNumber(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SubtractRightNumber;
		}

		// Token: 0x0600B75E RID: 46942 RVA: 0x0027C793 File Offset: 0x0027A993
		public bool Is_SubtractRightNumber(GrammarBuilders g, out SubtractRightNumber value)
		{
			if (this.Node.GrammarRule == g.Rule.SubtractRightNumber)
			{
				value = SubtractRightNumber.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SubtractRightNumber);
			return false;
		}

		// Token: 0x0600B75F RID: 46943 RVA: 0x0027C7C8 File Offset: 0x0027A9C8
		public SubtractRightNumber? As_SubtractRightNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SubtractRightNumber)
			{
				return null;
			}
			return new SubtractRightNumber?(SubtractRightNumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B760 RID: 46944 RVA: 0x0027C808 File Offset: 0x0027AA08
		public SubtractRightNumber Cast_SubtractRightNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SubtractRightNumber)
			{
				return SubtractRightNumber.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SubtractRightNumber is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B761 RID: 46945 RVA: 0x0027C860 File Offset: 0x0027AA60
		public T Switch<T>(GrammarBuilders g, Func<subtractRight_arithmeticLeft, T> func0, Func<SubtractRightNumber, T> func1)
		{
			subtractRight_arithmeticLeft subtractRight_arithmeticLeft;
			if (this.Is_subtractRight_arithmeticLeft(g, out subtractRight_arithmeticLeft))
			{
				return func0(subtractRight_arithmeticLeft);
			}
			SubtractRightNumber subtractRightNumber;
			if (this.Is_SubtractRightNumber(g, out subtractRightNumber))
			{
				return func1(subtractRightNumber);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol subtractRight");
		}

		// Token: 0x0600B762 RID: 46946 RVA: 0x0027C8B8 File Offset: 0x0027AAB8
		public void Switch(GrammarBuilders g, Action<subtractRight_arithmeticLeft> func0, Action<SubtractRightNumber> func1)
		{
			subtractRight_arithmeticLeft subtractRight_arithmeticLeft;
			if (this.Is_subtractRight_arithmeticLeft(g, out subtractRight_arithmeticLeft))
			{
				func0(subtractRight_arithmeticLeft);
				return;
			}
			SubtractRightNumber subtractRightNumber;
			if (this.Is_SubtractRightNumber(g, out subtractRightNumber))
			{
				func1(subtractRightNumber);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol subtractRight");
		}

		// Token: 0x0600B763 RID: 46947 RVA: 0x0027C90F File Offset: 0x0027AB0F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B764 RID: 46948 RVA: 0x0027C924 File Offset: 0x0027AB24
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B765 RID: 46949 RVA: 0x0027C94E File Offset: 0x0027AB4E
		public bool Equals(subtractRight other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400465F RID: 18015
		private ProgramNode _node;
	}
}
