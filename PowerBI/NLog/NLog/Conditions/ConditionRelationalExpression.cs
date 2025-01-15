using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using NLog.Common;

namespace NLog.Conditions
{
	// Token: 0x020001B2 RID: 434
	internal sealed class ConditionRelationalExpression : ConditionExpression
	{
		// Token: 0x06001341 RID: 4929 RVA: 0x00034508 File Offset: 0x00032708
		public ConditionRelationalExpression(ConditionExpression leftExpression, ConditionExpression rightExpression, ConditionRelationalOperator relationalOperator)
		{
			this.LeftExpression = leftExpression;
			this.RightExpression = rightExpression;
			this.RelationalOperator = relationalOperator;
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x00034525 File Offset: 0x00032725
		// (set) Token: 0x06001343 RID: 4931 RVA: 0x0003452D File Offset: 0x0003272D
		public ConditionExpression LeftExpression { get; private set; }

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x00034536 File Offset: 0x00032736
		// (set) Token: 0x06001345 RID: 4933 RVA: 0x0003453E File Offset: 0x0003273E
		public ConditionExpression RightExpression { get; private set; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x00034547 File Offset: 0x00032747
		// (set) Token: 0x06001347 RID: 4935 RVA: 0x0003454F File Offset: 0x0003274F
		public ConditionRelationalOperator RelationalOperator { get; private set; }

		// Token: 0x06001348 RID: 4936 RVA: 0x00034558 File Offset: 0x00032758
		public override string ToString()
		{
			return string.Format("({0} {1} {2})", this.LeftExpression, this.GetOperatorString(), this.RightExpression);
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00034578 File Offset: 0x00032778
		protected override object EvaluateNode(LogEventInfo context)
		{
			object obj = this.LeftExpression.Evaluate(context);
			object obj2 = this.RightExpression.Evaluate(context);
			return ConditionRelationalExpression.Compare(obj, obj2, this.RelationalOperator);
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x000345AC File Offset: 0x000327AC
		private static object Compare(object leftValue, object rightValue, ConditionRelationalOperator relationalOperator)
		{
			IComparer invariantCulture = StringComparer.InvariantCulture;
			ConditionRelationalExpression.PromoteTypes(ref leftValue, ref rightValue);
			switch (relationalOperator)
			{
			case ConditionRelationalOperator.Equal:
				return invariantCulture.Compare(leftValue, rightValue) == 0;
			case ConditionRelationalOperator.NotEqual:
				return invariantCulture.Compare(leftValue, rightValue) != 0;
			case ConditionRelationalOperator.Less:
				return invariantCulture.Compare(leftValue, rightValue) < 0;
			case ConditionRelationalOperator.Greater:
				return invariantCulture.Compare(leftValue, rightValue) > 0;
			case ConditionRelationalOperator.LessOrEqual:
				return invariantCulture.Compare(leftValue, rightValue) <= 0;
			case ConditionRelationalOperator.GreaterOrEqual:
				return invariantCulture.Compare(leftValue, rightValue) >= 0;
			default:
				throw new NotSupportedException(string.Format("Relational operator {0} is not supported.", relationalOperator));
			}
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0003466C File Offset: 0x0003286C
		private static void PromoteTypes(ref object leftValue, ref object rightValue)
		{
			if (leftValue == null || rightValue == null)
			{
				return;
			}
			Type type = leftValue.GetType();
			Type type2 = rightValue.GetType();
			if (type == type2)
			{
				return;
			}
			int order = ConditionRelationalExpression.GetOrder(type);
			int order2 = ConditionRelationalExpression.GetOrder(type2);
			if (order < order2)
			{
				if (ConditionRelationalExpression.TryPromoteTypes(ref rightValue, type, ref leftValue, type2))
				{
					return;
				}
			}
			else if (ConditionRelationalExpression.TryPromoteTypes(ref leftValue, type2, ref rightValue, type))
			{
				return;
			}
			throw new ConditionEvaluationException(string.Concat(new string[] { "Cannot find common type for '", type.Name, "' and '", type2.Name, "'." }));
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00034700 File Offset: 0x00032900
		private static bool TryPromoteType(ref object val, Type type1)
		{
			try
			{
				if (type1 == typeof(DateTime))
				{
					val = Convert.ToDateTime(val, CultureInfo.InvariantCulture);
					return true;
				}
				if (type1 == typeof(double))
				{
					val = Convert.ToDouble(val, CultureInfo.InvariantCulture);
					return true;
				}
				if (type1 == typeof(float))
				{
					val = Convert.ToSingle(val, CultureInfo.InvariantCulture);
					return true;
				}
				if (type1 == typeof(decimal))
				{
					val = Convert.ToDecimal(val, CultureInfo.InvariantCulture);
					return true;
				}
				if (type1 == typeof(long))
				{
					val = Convert.ToInt64(val, CultureInfo.InvariantCulture);
					return true;
				}
				if (type1 == typeof(int))
				{
					val = Convert.ToInt32(val, CultureInfo.InvariantCulture);
					return true;
				}
				if (type1 == typeof(bool))
				{
					val = Convert.ToBoolean(val, CultureInfo.InvariantCulture);
					return true;
				}
				if (type1 == typeof(string))
				{
					val = Convert.ToString(val, CultureInfo.InvariantCulture);
					InternalLogger.Debug("Using string comparision");
					return true;
				}
			}
			catch (Exception)
			{
				InternalLogger.Debug<object, string>("conversion of {0} to {1} failed", val, type1.Name);
			}
			return false;
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x000348A0 File Offset: 0x00032AA0
		private static bool TryPromoteTypes(ref object val1, Type type1, ref object val2, Type type2)
		{
			return ConditionRelationalExpression.TryPromoteType(ref val1, type1) || ConditionRelationalExpression.TryPromoteType(ref val2, type2);
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x000348B4 File Offset: 0x00032AB4
		private static int GetOrder(Type type1)
		{
			int num;
			if (ConditionRelationalExpression.TypePromoteOrder.TryGetValue(type1, out num))
			{
				return num;
			}
			return int.MaxValue;
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x000348D8 File Offset: 0x00032AD8
		private static Dictionary<Type, int> BuildTypeOrderDictionary()
		{
			List<Type> list = new List<Type>
			{
				typeof(DateTime),
				typeof(double),
				typeof(float),
				typeof(decimal),
				typeof(long),
				typeof(int),
				typeof(bool),
				typeof(string)
			};
			Dictionary<Type, int> dictionary = new Dictionary<Type, int>(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				dictionary.Add(list[i], i);
			}
			return dictionary;
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00034998 File Offset: 0x00032B98
		private string GetOperatorString()
		{
			switch (this.RelationalOperator)
			{
			case ConditionRelationalOperator.Equal:
				return "==";
			case ConditionRelationalOperator.NotEqual:
				return "!=";
			case ConditionRelationalOperator.Less:
				return "<";
			case ConditionRelationalOperator.Greater:
				return ">";
			case ConditionRelationalOperator.LessOrEqual:
				return "<=";
			case ConditionRelationalOperator.GreaterOrEqual:
				return ">=";
			default:
				throw new NotSupportedException(string.Format("Relational operator {0} is not supported.", this.RelationalOperator));
			}
		}

		// Token: 0x04000524 RID: 1316
		private static Dictionary<Type, int> TypePromoteOrder = ConditionRelationalExpression.BuildTypeOrderDictionary();
	}
}
