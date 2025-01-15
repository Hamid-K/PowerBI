using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes
{
	// Token: 0x02001A43 RID: 6723
	public struct selectValue : IProgramNodeBuilder, IEquatable<selectValue>
	{
		// Token: 0x17002512 RID: 9490
		// (get) Token: 0x0600DD6E RID: 56686 RVA: 0x002F15F2 File Offset: 0x002EF7F2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DD6F RID: 56687 RVA: 0x002F15FA File Offset: 0x002EF7FA
		private selectValue(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DD70 RID: 56688 RVA: 0x002F1603 File Offset: 0x002EF803
		public static selectValue CreateUnsafe(ProgramNode node)
		{
			return new selectValue(node);
		}

		// Token: 0x0600DD71 RID: 56689 RVA: 0x002F160C File Offset: 0x002EF80C
		public static selectValue? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.selectValue)
			{
				return null;
			}
			return new selectValue?(selectValue.CreateUnsafe(node));
		}

		// Token: 0x0600DD72 RID: 56690 RVA: 0x002F1646 File Offset: 0x002EF846
		public static selectValue CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new selectValue(new Hole(g.Symbol.selectValue, holeId));
		}

		// Token: 0x0600DD73 RID: 56691 RVA: 0x002F165E File Offset: 0x002EF85E
		public bool Is_SelectValue(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SelectValue;
		}

		// Token: 0x0600DD74 RID: 56692 RVA: 0x002F1678 File Offset: 0x002EF878
		public bool Is_SelectValue(GrammarBuilders g, out SelectValue value)
		{
			if (this.Node.GrammarRule == g.Rule.SelectValue)
			{
				value = SelectValue.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SelectValue);
			return false;
		}

		// Token: 0x0600DD75 RID: 56693 RVA: 0x002F16B0 File Offset: 0x002EF8B0
		public SelectValue? As_SelectValue(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SelectValue)
			{
				return null;
			}
			return new SelectValue?(SelectValue.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DD76 RID: 56694 RVA: 0x002F16F0 File Offset: 0x002EF8F0
		public SelectValue Cast_SelectValue(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SelectValue)
			{
				return SelectValue.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SelectValue is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DD77 RID: 56695 RVA: 0x002F1745 File Offset: 0x002EF945
		public bool Is_ValueToString(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ValueToString;
		}

		// Token: 0x0600DD78 RID: 56696 RVA: 0x002F175F File Offset: 0x002EF95F
		public bool Is_ValueToString(GrammarBuilders g, out ValueToString value)
		{
			if (this.Node.GrammarRule == g.Rule.ValueToString)
			{
				value = ValueToString.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ValueToString);
			return false;
		}

		// Token: 0x0600DD79 RID: 56697 RVA: 0x002F1794 File Offset: 0x002EF994
		public ValueToString? As_ValueToString(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ValueToString)
			{
				return null;
			}
			return new ValueToString?(ValueToString.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DD7A RID: 56698 RVA: 0x002F17D4 File Offset: 0x002EF9D4
		public ValueToString Cast_ValueToString(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ValueToString)
			{
				return ValueToString.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ValueToString is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DD7B RID: 56699 RVA: 0x002F1829 File Offset: 0x002EFA29
		public bool Is_ConvertValueTo(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ConvertValueTo;
		}

		// Token: 0x0600DD7C RID: 56700 RVA: 0x002F1843 File Offset: 0x002EFA43
		public bool Is_ConvertValueTo(GrammarBuilders g, out ConvertValueTo value)
		{
			if (this.Node.GrammarRule == g.Rule.ConvertValueTo)
			{
				value = ConvertValueTo.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ConvertValueTo);
			return false;
		}

		// Token: 0x0600DD7D RID: 56701 RVA: 0x002F1878 File Offset: 0x002EFA78
		public ConvertValueTo? As_ConvertValueTo(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ConvertValueTo)
			{
				return null;
			}
			return new ConvertValueTo?(ConvertValueTo.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DD7E RID: 56702 RVA: 0x002F18B8 File Offset: 0x002EFAB8
		public ConvertValueTo Cast_ConvertValueTo(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ConvertValueTo)
			{
				return ConvertValueTo.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ConvertValueTo is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DD7F RID: 56703 RVA: 0x002F1910 File Offset: 0x002EFB10
		public T Switch<T>(GrammarBuilders g, Func<SelectValue, T> func0, Func<ValueToString, T> func1, Func<ConvertValueTo, T> func2)
		{
			SelectValue selectValue;
			if (this.Is_SelectValue(g, out selectValue))
			{
				return func0(selectValue);
			}
			ValueToString valueToString;
			if (this.Is_ValueToString(g, out valueToString))
			{
				return func1(valueToString);
			}
			ConvertValueTo convertValueTo;
			if (this.Is_ConvertValueTo(g, out convertValueTo))
			{
				return func2(convertValueTo);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selectValue");
		}

		// Token: 0x0600DD80 RID: 56704 RVA: 0x002F197C File Offset: 0x002EFB7C
		public void Switch(GrammarBuilders g, Action<SelectValue> func0, Action<ValueToString> func1, Action<ConvertValueTo> func2)
		{
			SelectValue selectValue;
			if (this.Is_SelectValue(g, out selectValue))
			{
				func0(selectValue);
				return;
			}
			ValueToString valueToString;
			if (this.Is_ValueToString(g, out valueToString))
			{
				func1(valueToString);
				return;
			}
			ConvertValueTo convertValueTo;
			if (this.Is_ConvertValueTo(g, out convertValueTo))
			{
				func2(convertValueTo);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selectValue");
		}

		// Token: 0x0600DD81 RID: 56705 RVA: 0x002F19E7 File Offset: 0x002EFBE7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DD82 RID: 56706 RVA: 0x002F19FC File Offset: 0x002EFBFC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DD83 RID: 56707 RVA: 0x002F1A26 File Offset: 0x002EFC26
		public bool Equals(selectValue other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005434 RID: 21556
		private ProgramNode _node;
	}
}
