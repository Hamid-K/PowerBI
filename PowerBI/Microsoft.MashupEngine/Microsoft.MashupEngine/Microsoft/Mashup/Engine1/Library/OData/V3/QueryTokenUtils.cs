using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Experimental.OData.Query;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008E9 RID: 2281
	internal static class QueryTokenUtils
	{
		// Token: 0x0600410B RID: 16651 RVA: 0x000D98B4 File Offset: 0x000D7AB4
		public static bool Equals(QueryToken a, QueryToken b)
		{
			if (a == null && b == null)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (a.Kind != b.Kind)
			{
				return false;
			}
			switch (a.Kind)
			{
			case QueryTokenKind.Extension:
				return false;
			case QueryTokenKind.QueryDescriptor:
				return QueryTokenUtils.EqualsQueryDescriptor((QueryDescriptorQueryToken)a, (QueryDescriptorQueryToken)b);
			case QueryTokenKind.Segment:
				return QueryTokenUtils.EqualsSegment((SegmentQueryToken)a, (SegmentQueryToken)b);
			case QueryTokenKind.BinaryOperator:
				return QueryTokenUtils.EqualsBinaryOperator((BinaryOperatorQueryToken)a, (BinaryOperatorQueryToken)b);
			case QueryTokenKind.UnaryOperator:
				return QueryTokenUtils.EqualsUnary((UnaryOperatorQueryToken)a, (UnaryOperatorQueryToken)b);
			case QueryTokenKind.Literal:
				return QueryTokenUtils.EqualsLiteral((LiteralQueryToken)a, (LiteralQueryToken)b);
			case QueryTokenKind.FunctionCall:
				return QueryTokenUtils.EqualsFunctionCall((FunctionCallQueryToken)a, (FunctionCallQueryToken)b);
			case QueryTokenKind.PropertyAccess:
				return QueryTokenUtils.EqualsPropertyAccess((PropertyAccessQueryToken)a, (PropertyAccessQueryToken)b);
			case QueryTokenKind.OrderBy:
				return QueryTokenUtils.EqualsOrderBy((OrderByQueryToken)a, (OrderByQueryToken)b);
			case QueryTokenKind.QueryOption:
				return QueryTokenUtils.EqualsQueryOption((QueryOptionQueryToken)a, (QueryOptionQueryToken)b);
			case QueryTokenKind.Select:
				return QueryTokenUtils.EqualsSelect((SelectQueryToken)a, (SelectQueryToken)b);
			case QueryTokenKind.Star:
				return QueryTokenUtils.EqualsStar((StarQueryToken)a, (StarQueryToken)b);
			case QueryTokenKind.KeywordSegment:
				return QueryTokenUtils.EqualsKeywordSegment((KeywordSegmentQueryToken)a, (KeywordSegmentQueryToken)b);
			case QueryTokenKind.Expand:
				return QueryTokenUtils.EqualsExpand((ExpandQueryToken)a, (ExpandQueryToken)b);
			default:
				throw new InvalidOperationException("Should not reach here with QueryToken kind: " + a.Kind.ToString());
			}
		}

		// Token: 0x0600410C RID: 16652 RVA: 0x000D9A40 File Offset: 0x000D7C40
		private static bool EqualsBinaryOperator(BinaryOperatorQueryToken a, BinaryOperatorQueryToken b)
		{
			return a.OperatorKind == b.OperatorKind && QueryTokenUtils.Equals(a.Left, b.Left) && QueryTokenUtils.Equals(a.Right, b.Right);
		}

		// Token: 0x0600410D RID: 16653 RVA: 0x000D9A76 File Offset: 0x000D7C76
		private static bool EqualsExpand(ExpandQueryToken a, ExpandQueryToken b)
		{
			return QueryTokenUtils.EqualsTokensUnordered(a.Properties, b.Properties);
		}

		// Token: 0x0600410E RID: 16654 RVA: 0x000D9A89 File Offset: 0x000D7C89
		private static bool EqualsFunctionCall(FunctionCallQueryToken a, FunctionCallQueryToken b)
		{
			return !(a.Name != b.Name) && QueryTokenUtils.EqualsTokensOrdered(a.Arguments, b.Arguments);
		}

		// Token: 0x0600410F RID: 16655 RVA: 0x000D9AB1 File Offset: 0x000D7CB1
		private static bool EqualsKeywordSegment(KeywordSegmentQueryToken a, KeywordSegmentQueryToken b)
		{
			return a.Keyword == b.Keyword && QueryTokenUtils.EqualsSegment(a, b);
		}

		// Token: 0x06004110 RID: 16656 RVA: 0x000D9ACC File Offset: 0x000D7CCC
		private static bool EqualsLiteral(LiteralQueryToken a, LiteralQueryToken b)
		{
			if (a.Value == b.Value)
			{
				return true;
			}
			if (a.Value.Equals(b.Value))
			{
				return true;
			}
			TypeCode typeCode = Type.GetTypeCode(a.Value.GetType());
			TypeCode typeCode2 = Type.GetTypeCode(b.Value.GetType());
			if (typeCode == typeCode2)
			{
				return false;
			}
			if (typeCode - TypeCode.SByte <= 9)
			{
				double num;
				double num2;
				return double.TryParse(a.Value.ToString(), out num) && double.TryParse(b.Value.ToString(), out num2) && num == num2;
			}
			byte[] array = a.Value as byte[];
			byte[] array2 = b.Value as byte[];
			if (array != null && array2 != null && array.Length == array2.Length)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != array2[i])
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06004111 RID: 16657 RVA: 0x000D9BA5 File Offset: 0x000D7DA5
		private static bool EqualsNamedValue(NamedValue a, NamedValue b)
		{
			return a.Name == b.Name && QueryTokenUtils.Equals(a.Value, b.Value);
		}

		// Token: 0x06004112 RID: 16658 RVA: 0x000D9BCD File Offset: 0x000D7DCD
		private static bool EqualsOrderBy(OrderByQueryToken a, OrderByQueryToken b)
		{
			return a.Direction == b.Direction && QueryTokenUtils.Equals(a.Expression, b.Expression);
		}

		// Token: 0x06004113 RID: 16659 RVA: 0x000D9BF0 File Offset: 0x000D7DF0
		private static bool EqualsPropertyAccess(PropertyAccessQueryToken a, PropertyAccessQueryToken b)
		{
			return !(a.Name != b.Name) && QueryTokenUtils.Equals(a.Parent, b.Parent);
		}

		// Token: 0x06004114 RID: 16660 RVA: 0x000D9C18 File Offset: 0x000D7E18
		private static bool EqualsQueryDescriptor(QueryDescriptorQueryToken a, QueryDescriptorQueryToken b)
		{
			int? num = a.Skip;
			int? num2 = b.Skip;
			if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
			{
				num2 = a.Top;
				num = b.Top;
				if ((num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null)))
				{
					InlineCountKind? inlineCount = a.InlineCount;
					InlineCountKind? inlineCount2 = b.InlineCount;
					if (((inlineCount.GetValueOrDefault() == inlineCount2.GetValueOrDefault()) & (inlineCount != null == (inlineCount2 != null))) && string.Equals(a.Format, b.Format, StringComparison.OrdinalIgnoreCase) && QueryTokenUtils.Equals(a.Path, b.Path) && QueryTokenUtils.Equals(a.Filter, b.Filter) && QueryTokenUtils.Equals(a.Select, b.Select) && QueryTokenUtils.Equals(a.Expand, b.Expand) && QueryTokenUtils.EqualsTokensOrdered(a.OrderByTokens.OfType<QueryToken>(), b.OrderByTokens.OfType<QueryToken>()))
					{
						return QueryTokenUtils.EqualsTokensUnordered(a.QueryOptions.OfType<QueryToken>(), b.QueryOptions.OfType<QueryToken>());
					}
				}
			}
			return false;
		}

		// Token: 0x06004115 RID: 16661 RVA: 0x000D9D5E File Offset: 0x000D7F5E
		private static bool EqualsQueryOption(QueryOptionQueryToken a, QueryOptionQueryToken b)
		{
			return a.Name == b.Name && a.Value == b.Value;
		}

		// Token: 0x06004116 RID: 16662 RVA: 0x000D9D88 File Offset: 0x000D7F88
		private static bool EqualsSegment(SegmentQueryToken a, SegmentQueryToken b)
		{
			if (a.Name != b.Name)
			{
				return false;
			}
			if (!QueryTokenUtils.Equals(a.Parent, b.Parent))
			{
				return false;
			}
			if (a.NamedValues == null && b.NamedValues == null)
			{
				return true;
			}
			if (a.NamedValues == null || b.NamedValues == null)
			{
				return false;
			}
			List<NamedValue> list = new List<NamedValue>(a.NamedValues);
			List<NamedValue> list2 = new List<NamedValue>(b.NamedValues);
			if (list.Count != list2.Count)
			{
				return false;
			}
			for (int i = 0; i < list.Count; i++)
			{
				for (int j = 0; j < list2.Count; j++)
				{
					if (QueryTokenUtils.EqualsNamedValue(list[i], list2[j]))
					{
						list2.RemoveAt(j);
						break;
					}
				}
			}
			return list2.Count == 0;
		}

		// Token: 0x06004117 RID: 16663 RVA: 0x000D9E54 File Offset: 0x000D8054
		private static bool EqualsSelect(SelectQueryToken a, SelectQueryToken b)
		{
			return QueryTokenUtils.EqualsTokensOrdered(a.Properties, b.Properties);
		}

		// Token: 0x06004118 RID: 16664 RVA: 0x000D9E67 File Offset: 0x000D8067
		private static bool EqualsStar(StarQueryToken a, StarQueryToken b)
		{
			return QueryTokenUtils.Equals(a.Parent, b.Parent);
		}

		// Token: 0x06004119 RID: 16665 RVA: 0x000D9E7C File Offset: 0x000D807C
		private static bool EqualsTokensOrdered(IEnumerable<QueryToken> a, IEnumerable<QueryToken> b)
		{
			List<QueryToken> list = new List<QueryToken>(a);
			List<QueryToken> list2 = new List<QueryToken>(b);
			if (list.Count != list2.Count)
			{
				return false;
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (!QueryTokenUtils.Equals(list[i], list2[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600411A RID: 16666 RVA: 0x000D9ED0 File Offset: 0x000D80D0
		private static bool EqualsTokensUnordered(IEnumerable<QueryToken> a, IEnumerable<QueryToken> b)
		{
			List<QueryToken> list = new List<QueryToken>(a);
			List<QueryToken> list2 = new List<QueryToken>(b);
			if (list.Count != list2.Count)
			{
				return false;
			}
			for (int i = 0; i < list.Count; i++)
			{
				for (int j = 0; j < list2.Count; j++)
				{
					if (QueryTokenUtils.Equals(list[i], list2[j]))
					{
						list2.RemoveAt(j);
						break;
					}
				}
			}
			return list2.Count == 0;
		}

		// Token: 0x0600411B RID: 16667 RVA: 0x000D9F44 File Offset: 0x000D8144
		private static bool EqualsUnary(UnaryOperatorQueryToken a, UnaryOperatorQueryToken b)
		{
			return a.OperatorKind == b.OperatorKind && QueryTokenUtils.Equals(a.Operand, b.Operand);
		}
	}
}
