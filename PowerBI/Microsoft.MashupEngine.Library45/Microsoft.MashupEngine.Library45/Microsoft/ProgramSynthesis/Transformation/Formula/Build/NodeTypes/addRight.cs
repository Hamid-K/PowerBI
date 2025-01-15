using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015B0 RID: 5552
	public struct addRight : IProgramNodeBuilder, IEquatable<addRight>
	{
		// Token: 0x17001FD6 RID: 8150
		// (get) Token: 0x0600B742 RID: 46914 RVA: 0x0027C2EA File Offset: 0x0027A4EA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B743 RID: 46915 RVA: 0x0027C2F2 File Offset: 0x0027A4F2
		private addRight(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B744 RID: 46916 RVA: 0x0027C2FB File Offset: 0x0027A4FB
		public static addRight CreateUnsafe(ProgramNode node)
		{
			return new addRight(node);
		}

		// Token: 0x0600B745 RID: 46917 RVA: 0x0027C304 File Offset: 0x0027A504
		public static addRight? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.addRight)
			{
				return null;
			}
			return new addRight?(addRight.CreateUnsafe(node));
		}

		// Token: 0x0600B746 RID: 46918 RVA: 0x0027C33E File Offset: 0x0027A53E
		public static addRight CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new addRight(new Hole(g.Symbol.addRight, holeId));
		}

		// Token: 0x0600B747 RID: 46919 RVA: 0x0027C356 File Offset: 0x0027A556
		public bool Is_addRight_arithmeticLeft(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.addRight_arithmeticLeft;
		}

		// Token: 0x0600B748 RID: 46920 RVA: 0x0027C370 File Offset: 0x0027A570
		public bool Is_addRight_arithmeticLeft(GrammarBuilders g, out addRight_arithmeticLeft value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.addRight_arithmeticLeft)
			{
				value = addRight_arithmeticLeft.CreateUnsafe(this.Node);
				return true;
			}
			value = default(addRight_arithmeticLeft);
			return false;
		}

		// Token: 0x0600B749 RID: 46921 RVA: 0x0027C3A8 File Offset: 0x0027A5A8
		public addRight_arithmeticLeft? As_addRight_arithmeticLeft(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.addRight_arithmeticLeft)
			{
				return null;
			}
			return new addRight_arithmeticLeft?(addRight_arithmeticLeft.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B74A RID: 46922 RVA: 0x0027C3E8 File Offset: 0x0027A5E8
		public addRight_arithmeticLeft Cast_addRight_arithmeticLeft(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.addRight_arithmeticLeft)
			{
				return addRight_arithmeticLeft.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_addRight_arithmeticLeft is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B74B RID: 46923 RVA: 0x0027C43D File Offset: 0x0027A63D
		public bool Is_AddRightNumber(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.AddRightNumber;
		}

		// Token: 0x0600B74C RID: 46924 RVA: 0x0027C457 File Offset: 0x0027A657
		public bool Is_AddRightNumber(GrammarBuilders g, out AddRightNumber value)
		{
			if (this.Node.GrammarRule == g.Rule.AddRightNumber)
			{
				value = AddRightNumber.CreateUnsafe(this.Node);
				return true;
			}
			value = default(AddRightNumber);
			return false;
		}

		// Token: 0x0600B74D RID: 46925 RVA: 0x0027C48C File Offset: 0x0027A68C
		public AddRightNumber? As_AddRightNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.AddRightNumber)
			{
				return null;
			}
			return new AddRightNumber?(AddRightNumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B74E RID: 46926 RVA: 0x0027C4CC File Offset: 0x0027A6CC
		public AddRightNumber Cast_AddRightNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.AddRightNumber)
			{
				return AddRightNumber.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_AddRightNumber is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B74F RID: 46927 RVA: 0x0027C524 File Offset: 0x0027A724
		public T Switch<T>(GrammarBuilders g, Func<addRight_arithmeticLeft, T> func0, Func<AddRightNumber, T> func1)
		{
			addRight_arithmeticLeft addRight_arithmeticLeft;
			if (this.Is_addRight_arithmeticLeft(g, out addRight_arithmeticLeft))
			{
				return func0(addRight_arithmeticLeft);
			}
			AddRightNumber addRightNumber;
			if (this.Is_AddRightNumber(g, out addRightNumber))
			{
				return func1(addRightNumber);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol addRight");
		}

		// Token: 0x0600B750 RID: 46928 RVA: 0x0027C57C File Offset: 0x0027A77C
		public void Switch(GrammarBuilders g, Action<addRight_arithmeticLeft> func0, Action<AddRightNumber> func1)
		{
			addRight_arithmeticLeft addRight_arithmeticLeft;
			if (this.Is_addRight_arithmeticLeft(g, out addRight_arithmeticLeft))
			{
				func0(addRight_arithmeticLeft);
				return;
			}
			AddRightNumber addRightNumber;
			if (this.Is_AddRightNumber(g, out addRightNumber))
			{
				func1(addRightNumber);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol addRight");
		}

		// Token: 0x0600B751 RID: 46929 RVA: 0x0027C5D3 File Offset: 0x0027A7D3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B752 RID: 46930 RVA: 0x0027C5E8 File Offset: 0x0027A7E8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B753 RID: 46931 RVA: 0x0027C612 File Offset: 0x0027A812
		public bool Equals(addRight other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400465E RID: 18014
		private ProgramNode _node;
	}
}
