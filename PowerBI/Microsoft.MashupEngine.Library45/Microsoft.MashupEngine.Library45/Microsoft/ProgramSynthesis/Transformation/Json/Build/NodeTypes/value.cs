using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes
{
	// Token: 0x02001A3B RID: 6715
	public struct value : IProgramNodeBuilder, IEquatable<value>
	{
		// Token: 0x1700250A RID: 9482
		// (get) Token: 0x0600DCD0 RID: 56528 RVA: 0x002EF922 File Offset: 0x002EDB22
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DCD1 RID: 56529 RVA: 0x002EF92A File Offset: 0x002EDB2A
		private value(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DCD2 RID: 56530 RVA: 0x002EF933 File Offset: 0x002EDB33
		public static value CreateUnsafe(ProgramNode node)
		{
			return new value(node);
		}

		// Token: 0x0600DCD3 RID: 56531 RVA: 0x002EF93C File Offset: 0x002EDB3C
		public static value? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.value)
			{
				return null;
			}
			return new value?(value.CreateUnsafe(node));
		}

		// Token: 0x0600DCD4 RID: 56532 RVA: 0x002EF976 File Offset: 0x002EDB76
		public static value CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new value(new Hole(g.Symbol.value, holeId));
		}

		// Token: 0x0600DCD5 RID: 56533 RVA: 0x002EF98E File Offset: 0x002EDB8E
		public bool Is_value_object(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.value_object;
		}

		// Token: 0x0600DCD6 RID: 56534 RVA: 0x002EF9A8 File Offset: 0x002EDBA8
		public bool Is_value_object(GrammarBuilders g, out value_object value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.value_object)
			{
				value = value_object.CreateUnsafe(this.Node);
				return true;
			}
			value = default(value_object);
			return false;
		}

		// Token: 0x0600DCD7 RID: 56535 RVA: 0x002EF9E0 File Offset: 0x002EDBE0
		public value_object? As_value_object(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.value_object)
			{
				return null;
			}
			return new value_object?(value_object.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DCD8 RID: 56536 RVA: 0x002EFA20 File Offset: 0x002EDC20
		public value_object Cast_value_object(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.value_object)
			{
				return value_object.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_value_object is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DCD9 RID: 56537 RVA: 0x002EFA75 File Offset: 0x002EDC75
		public bool Is_value_array(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.value_array;
		}

		// Token: 0x0600DCDA RID: 56538 RVA: 0x002EFA8F File Offset: 0x002EDC8F
		public bool Is_value_array(GrammarBuilders g, out value_array value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.value_array)
			{
				value = value_array.CreateUnsafe(this.Node);
				return true;
			}
			value = default(value_array);
			return false;
		}

		// Token: 0x0600DCDB RID: 56539 RVA: 0x002EFAC4 File Offset: 0x002EDCC4
		public value_array? As_value_array(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.value_array)
			{
				return null;
			}
			return new value_array?(value_array.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DCDC RID: 56540 RVA: 0x002EFB04 File Offset: 0x002EDD04
		public value_array Cast_value_array(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.value_array)
			{
				return value_array.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_value_array is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DCDD RID: 56541 RVA: 0x002EFB59 File Offset: 0x002EDD59
		public bool Is_SelectOrTransformValue(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SelectOrTransformValue;
		}

		// Token: 0x0600DCDE RID: 56542 RVA: 0x002EFB73 File Offset: 0x002EDD73
		public bool Is_SelectOrTransformValue(GrammarBuilders g, out SelectOrTransformValue value)
		{
			if (this.Node.GrammarRule == g.Rule.SelectOrTransformValue)
			{
				value = SelectOrTransformValue.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SelectOrTransformValue);
			return false;
		}

		// Token: 0x0600DCDF RID: 56543 RVA: 0x002EFBA8 File Offset: 0x002EDDA8
		public SelectOrTransformValue? As_SelectOrTransformValue(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SelectOrTransformValue)
			{
				return null;
			}
			return new SelectOrTransformValue?(SelectOrTransformValue.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DCE0 RID: 56544 RVA: 0x002EFBE8 File Offset: 0x002EDDE8
		public SelectOrTransformValue Cast_SelectOrTransformValue(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SelectOrTransformValue)
			{
				return SelectOrTransformValue.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SelectOrTransformValue is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DCE1 RID: 56545 RVA: 0x002EFC3D File Offset: 0x002EDE3D
		public bool Is__Value(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule._Value;
		}

		// Token: 0x0600DCE2 RID: 56546 RVA: 0x002EFC57 File Offset: 0x002EDE57
		public bool Is__Value(GrammarBuilders g, out _Value value)
		{
			if (this.Node.GrammarRule == g.Rule._Value)
			{
				value = _Value.CreateUnsafe(this.Node);
				return true;
			}
			value = default(_Value);
			return false;
		}

		// Token: 0x0600DCE3 RID: 56547 RVA: 0x002EFC8C File Offset: 0x002EDE8C
		public _Value? As__Value(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule._Value)
			{
				return null;
			}
			return new _Value?(_Value.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DCE4 RID: 56548 RVA: 0x002EFCCC File Offset: 0x002EDECC
		public _Value Cast__Value(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule._Value)
			{
				return _Value.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast__Value is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DCE5 RID: 56549 RVA: 0x002EFD21 File Offset: 0x002EDF21
		public bool Is_ConstValue(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ConstValue;
		}

		// Token: 0x0600DCE6 RID: 56550 RVA: 0x002EFD3B File Offset: 0x002EDF3B
		public bool Is_ConstValue(GrammarBuilders g, out ConstValue value)
		{
			if (this.Node.GrammarRule == g.Rule.ConstValue)
			{
				value = ConstValue.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ConstValue);
			return false;
		}

		// Token: 0x0600DCE7 RID: 56551 RVA: 0x002EFD70 File Offset: 0x002EDF70
		public ConstValue? As_ConstValue(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ConstValue)
			{
				return null;
			}
			return new ConstValue?(ConstValue.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DCE8 RID: 56552 RVA: 0x002EFDB0 File Offset: 0x002EDFB0
		public ConstValue Cast_ConstValue(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ConstValue)
			{
				return ConstValue.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ConstValue is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DCE9 RID: 56553 RVA: 0x002EFE08 File Offset: 0x002EE008
		public T Switch<T>(GrammarBuilders g, Func<value_object, T> func0, Func<value_array, T> func1, Func<SelectOrTransformValue, T> func2, Func<_Value, T> func3, Func<ConstValue, T> func4)
		{
			value_object value_object;
			if (this.Is_value_object(g, out value_object))
			{
				return func0(value_object);
			}
			value_array value_array;
			if (this.Is_value_array(g, out value_array))
			{
				return func1(value_array);
			}
			SelectOrTransformValue selectOrTransformValue;
			if (this.Is_SelectOrTransformValue(g, out selectOrTransformValue))
			{
				return func2(selectOrTransformValue);
			}
			_Value value;
			if (this.Is__Value(g, out value))
			{
				return func3(value);
			}
			ConstValue constValue;
			if (this.Is_ConstValue(g, out constValue))
			{
				return func4(constValue);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol value");
		}

		// Token: 0x0600DCEA RID: 56554 RVA: 0x002EFE9C File Offset: 0x002EE09C
		public void Switch(GrammarBuilders g, Action<value_object> func0, Action<value_array> func1, Action<SelectOrTransformValue> func2, Action<_Value> func3, Action<ConstValue> func4)
		{
			value_object value_object;
			if (this.Is_value_object(g, out value_object))
			{
				func0(value_object);
				return;
			}
			value_array value_array;
			if (this.Is_value_array(g, out value_array))
			{
				func1(value_array);
				return;
			}
			SelectOrTransformValue selectOrTransformValue;
			if (this.Is_SelectOrTransformValue(g, out selectOrTransformValue))
			{
				func2(selectOrTransformValue);
				return;
			}
			_Value value;
			if (this.Is__Value(g, out value))
			{
				func3(value);
				return;
			}
			ConstValue constValue;
			if (this.Is_ConstValue(g, out constValue))
			{
				func4(constValue);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol value");
		}

		// Token: 0x0600DCEB RID: 56555 RVA: 0x002EFF30 File Offset: 0x002EE130
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DCEC RID: 56556 RVA: 0x002EFF44 File Offset: 0x002EE144
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DCED RID: 56557 RVA: 0x002EFF6E File Offset: 0x002EE16E
		public bool Equals(value other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400542C RID: 21548
		private ProgramNode _node;
	}
}
