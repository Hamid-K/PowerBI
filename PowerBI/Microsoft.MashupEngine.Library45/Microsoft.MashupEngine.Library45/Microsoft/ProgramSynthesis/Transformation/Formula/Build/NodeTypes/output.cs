using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x0200159A RID: 5530
	public struct output : IProgramNodeBuilder, IEquatable<output>
	{
		// Token: 0x17001FC0 RID: 8128
		// (get) Token: 0x0600B54E RID: 46414 RVA: 0x00276352 File Offset: 0x00274552
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B54F RID: 46415 RVA: 0x0027635A File Offset: 0x0027455A
		private output(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B550 RID: 46416 RVA: 0x00276363 File Offset: 0x00274563
		public static output CreateUnsafe(ProgramNode node)
		{
			return new output(node);
		}

		// Token: 0x0600B551 RID: 46417 RVA: 0x0027636C File Offset: 0x0027456C
		public static output? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.output)
			{
				return null;
			}
			return new output?(output.CreateUnsafe(node));
		}

		// Token: 0x0600B552 RID: 46418 RVA: 0x002763A6 File Offset: 0x002745A6
		public static output CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new output(new Hole(g.Symbol.output, holeId));
		}

		// Token: 0x0600B553 RID: 46419 RVA: 0x002763BE File Offset: 0x002745BE
		public bool Is_ToInt(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ToInt;
		}

		// Token: 0x0600B554 RID: 46420 RVA: 0x002763D8 File Offset: 0x002745D8
		public bool Is_ToInt(GrammarBuilders g, out ToInt value)
		{
			if (this.Node.GrammarRule == g.Rule.ToInt)
			{
				value = ToInt.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ToInt);
			return false;
		}

		// Token: 0x0600B555 RID: 46421 RVA: 0x00276410 File Offset: 0x00274610
		public ToInt? As_ToInt(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ToInt)
			{
				return null;
			}
			return new ToInt?(ToInt.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B556 RID: 46422 RVA: 0x00276450 File Offset: 0x00274650
		public ToInt Cast_ToInt(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ToInt)
			{
				return ToInt.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ToInt is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B557 RID: 46423 RVA: 0x002764A5 File Offset: 0x002746A5
		public bool Is_ToDouble(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ToDouble;
		}

		// Token: 0x0600B558 RID: 46424 RVA: 0x002764BF File Offset: 0x002746BF
		public bool Is_ToDouble(GrammarBuilders g, out ToDouble value)
		{
			if (this.Node.GrammarRule == g.Rule.ToDouble)
			{
				value = ToDouble.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ToDouble);
			return false;
		}

		// Token: 0x0600B559 RID: 46425 RVA: 0x002764F4 File Offset: 0x002746F4
		public ToDouble? As_ToDouble(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ToDouble)
			{
				return null;
			}
			return new ToDouble?(ToDouble.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B55A RID: 46426 RVA: 0x00276534 File Offset: 0x00274734
		public ToDouble Cast_ToDouble(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ToDouble)
			{
				return ToDouble.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ToDouble is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B55B RID: 46427 RVA: 0x00276589 File Offset: 0x00274789
		public bool Is_ToDecimal(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ToDecimal;
		}

		// Token: 0x0600B55C RID: 46428 RVA: 0x002765A3 File Offset: 0x002747A3
		public bool Is_ToDecimal(GrammarBuilders g, out ToDecimal value)
		{
			if (this.Node.GrammarRule == g.Rule.ToDecimal)
			{
				value = ToDecimal.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ToDecimal);
			return false;
		}

		// Token: 0x0600B55D RID: 46429 RVA: 0x002765D8 File Offset: 0x002747D8
		public ToDecimal? As_ToDecimal(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ToDecimal)
			{
				return null;
			}
			return new ToDecimal?(ToDecimal.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B55E RID: 46430 RVA: 0x00276618 File Offset: 0x00274818
		public ToDecimal Cast_ToDecimal(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ToDecimal)
			{
				return ToDecimal.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ToDecimal is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B55F RID: 46431 RVA: 0x0027666D File Offset: 0x0027486D
		public bool Is_ToDateTime(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ToDateTime;
		}

		// Token: 0x0600B560 RID: 46432 RVA: 0x00276687 File Offset: 0x00274887
		public bool Is_ToDateTime(GrammarBuilders g, out ToDateTime value)
		{
			if (this.Node.GrammarRule == g.Rule.ToDateTime)
			{
				value = ToDateTime.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ToDateTime);
			return false;
		}

		// Token: 0x0600B561 RID: 46433 RVA: 0x002766BC File Offset: 0x002748BC
		public ToDateTime? As_ToDateTime(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ToDateTime)
			{
				return null;
			}
			return new ToDateTime?(ToDateTime.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B562 RID: 46434 RVA: 0x002766FC File Offset: 0x002748FC
		public ToDateTime Cast_ToDateTime(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ToDateTime)
			{
				return ToDateTime.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ToDateTime is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B563 RID: 46435 RVA: 0x00276751 File Offset: 0x00274951
		public bool Is_ToStr(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ToStr;
		}

		// Token: 0x0600B564 RID: 46436 RVA: 0x0027676B File Offset: 0x0027496B
		public bool Is_ToStr(GrammarBuilders g, out ToStr value)
		{
			if (this.Node.GrammarRule == g.Rule.ToStr)
			{
				value = ToStr.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ToStr);
			return false;
		}

		// Token: 0x0600B565 RID: 46437 RVA: 0x002767A0 File Offset: 0x002749A0
		public ToStr? As_ToStr(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ToStr)
			{
				return null;
			}
			return new ToStr?(ToStr.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B566 RID: 46438 RVA: 0x002767E0 File Offset: 0x002749E0
		public ToStr Cast_ToStr(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ToStr)
			{
				return ToStr.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ToStr is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B567 RID: 46439 RVA: 0x00276838 File Offset: 0x00274A38
		public T Switch<T>(GrammarBuilders g, Func<ToInt, T> func0, Func<ToDouble, T> func1, Func<ToDecimal, T> func2, Func<ToDateTime, T> func3, Func<ToStr, T> func4)
		{
			ToInt toInt;
			if (this.Is_ToInt(g, out toInt))
			{
				return func0(toInt);
			}
			ToDouble toDouble;
			if (this.Is_ToDouble(g, out toDouble))
			{
				return func1(toDouble);
			}
			ToDecimal toDecimal;
			if (this.Is_ToDecimal(g, out toDecimal))
			{
				return func2(toDecimal);
			}
			ToDateTime toDateTime;
			if (this.Is_ToDateTime(g, out toDateTime))
			{
				return func3(toDateTime);
			}
			ToStr toStr;
			if (this.Is_ToStr(g, out toStr))
			{
				return func4(toStr);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol output");
		}

		// Token: 0x0600B568 RID: 46440 RVA: 0x002768CC File Offset: 0x00274ACC
		public void Switch(GrammarBuilders g, Action<ToInt> func0, Action<ToDouble> func1, Action<ToDecimal> func2, Action<ToDateTime> func3, Action<ToStr> func4)
		{
			ToInt toInt;
			if (this.Is_ToInt(g, out toInt))
			{
				func0(toInt);
				return;
			}
			ToDouble toDouble;
			if (this.Is_ToDouble(g, out toDouble))
			{
				func1(toDouble);
				return;
			}
			ToDecimal toDecimal;
			if (this.Is_ToDecimal(g, out toDecimal))
			{
				func2(toDecimal);
				return;
			}
			ToDateTime toDateTime;
			if (this.Is_ToDateTime(g, out toDateTime))
			{
				func3(toDateTime);
				return;
			}
			ToStr toStr;
			if (this.Is_ToStr(g, out toStr))
			{
				func4(toStr);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol output");
		}

		// Token: 0x0600B569 RID: 46441 RVA: 0x00276960 File Offset: 0x00274B60
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B56A RID: 46442 RVA: 0x00276974 File Offset: 0x00274B74
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B56B RID: 46443 RVA: 0x0027699E File Offset: 0x00274B9E
		public bool Equals(output other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004648 RID: 17992
		private ProgramNode _node;
	}
}
