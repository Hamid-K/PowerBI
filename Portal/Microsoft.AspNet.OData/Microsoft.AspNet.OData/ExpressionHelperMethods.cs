using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200004E RID: 78
	internal class ExpressionHelperMethods
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00007E0F File Offset: 0x0000600F
		public static MethodInfo EnumerableWhereGeneric
		{
			get
			{
				return ExpressionHelperMethods._enumerableWhereMethod;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00007E16 File Offset: 0x00006016
		public static MethodInfo QueryableToList
		{
			get
			{
				return ExpressionHelperMethods._queryableToListMethod;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00007E1D File Offset: 0x0000601D
		public static MethodInfo QueryableOrderByGeneric
		{
			get
			{
				return ExpressionHelperMethods._orderByMethod;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00007E24 File Offset: 0x00006024
		public static MethodInfo EnumerableOrderByGeneric
		{
			get
			{
				return ExpressionHelperMethods._enumerableOrderByMethod;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00007E2B File Offset: 0x0000602B
		public static MethodInfo QueryableOrderByDescendingGeneric
		{
			get
			{
				return ExpressionHelperMethods._orderByDescendingMethod;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00007E32 File Offset: 0x00006032
		public static MethodInfo EnumerableOrderByDescendingGeneric
		{
			get
			{
				return ExpressionHelperMethods._enumerableOrderByDescendingMethod;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00007E39 File Offset: 0x00006039
		public static MethodInfo QueryableThenByGeneric
		{
			get
			{
				return ExpressionHelperMethods._thenByMethod;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00007E40 File Offset: 0x00006040
		public static MethodInfo EnumerableThenByGeneric
		{
			get
			{
				return ExpressionHelperMethods._enumerableThenByMethod;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00007E47 File Offset: 0x00006047
		public static MethodInfo QueryableThenByDescendingGeneric
		{
			get
			{
				return ExpressionHelperMethods._thenByDescendingMethod;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00007E4E File Offset: 0x0000604E
		public static MethodInfo EnumerableThenByDescendingGeneric
		{
			get
			{
				return ExpressionHelperMethods._enumerableThenByDescendingMethod;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00007E55 File Offset: 0x00006055
		public static MethodInfo QueryableCountGeneric
		{
			get
			{
				return ExpressionHelperMethods._countMethod;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00007E5C File Offset: 0x0000605C
		public static Dictionary<Type, MethodInfo> QueryableSumGenerics
		{
			get
			{
				return ExpressionHelperMethods._queryableSumMethods;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00007E63 File Offset: 0x00006063
		public static Dictionary<Type, MethodInfo> EnumerableSumGenerics
		{
			get
			{
				return ExpressionHelperMethods._enumerableSumMethods;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00007E6A File Offset: 0x0000606A
		public static MethodInfo QueryableMin
		{
			get
			{
				return ExpressionHelperMethods._queryableMinMethod;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00007E71 File Offset: 0x00006071
		public static MethodInfo EnumerableMin
		{
			get
			{
				return ExpressionHelperMethods._enumerableMinMethod;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00007E78 File Offset: 0x00006078
		public static MethodInfo QueryableMax
		{
			get
			{
				return ExpressionHelperMethods._queryableMaxMethod;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00007E7F File Offset: 0x0000607F
		public static MethodInfo EnumerableMax
		{
			get
			{
				return ExpressionHelperMethods._enumerableMaxMethod;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00007E86 File Offset: 0x00006086
		public static Dictionary<Type, MethodInfo> QueryableAverageGenerics
		{
			get
			{
				return ExpressionHelperMethods._queryableAverageMethods;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00007E8D File Offset: 0x0000608D
		public static Dictionary<Type, MethodInfo> EnumerableAverageGenerics
		{
			get
			{
				return ExpressionHelperMethods._enumerableAverageMethods;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00007E94 File Offset: 0x00006094
		public static MethodInfo QueryableDistinct
		{
			get
			{
				return ExpressionHelperMethods._queryableDistinctMethod;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00007E9B File Offset: 0x0000609B
		public static MethodInfo EnumerableDistinct
		{
			get
			{
				return ExpressionHelperMethods._enumerableDistinctMethod;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00007EA2 File Offset: 0x000060A2
		public static MethodInfo QueryableGroupByGeneric
		{
			get
			{
				return ExpressionHelperMethods._groupByMethod;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00007EA9 File Offset: 0x000060A9
		public static MethodInfo EnumerableGroupByGeneric
		{
			get
			{
				return ExpressionHelperMethods._enumerableGroupByMethod;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00007EB0 File Offset: 0x000060B0
		public static MethodInfo QueryableAggregateGeneric
		{
			get
			{
				return ExpressionHelperMethods._aggregateMethod;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00007EB7 File Offset: 0x000060B7
		public static MethodInfo QueryableTakeGeneric
		{
			get
			{
				return ExpressionHelperMethods._queryableTakeMethod;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00007EBE File Offset: 0x000060BE
		public static MethodInfo EnumerableTakeGeneric
		{
			get
			{
				return ExpressionHelperMethods._enumerableTakeMethod;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00007EC5 File Offset: 0x000060C5
		public static MethodInfo QueryableSkipGeneric
		{
			get
			{
				return ExpressionHelperMethods._skipMethod;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00007ECC File Offset: 0x000060CC
		public static MethodInfo EnumerableSkipGeneric
		{
			get
			{
				return ExpressionHelperMethods._enumerableSkipMethod;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00007ED3 File Offset: 0x000060D3
		public static MethodInfo QueryableWhereGeneric
		{
			get
			{
				return ExpressionHelperMethods._whereMethod;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00007EDA File Offset: 0x000060DA
		public static MethodInfo QueryableContainsGeneric
		{
			get
			{
				return ExpressionHelperMethods._queryableContainsMethod;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00007EE1 File Offset: 0x000060E1
		public static MethodInfo EnumerableContainsGeneric
		{
			get
			{
				return ExpressionHelperMethods._enumerableContainsMethod;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00007EE8 File Offset: 0x000060E8
		public static MethodInfo QueryableSelectGeneric
		{
			get
			{
				return ExpressionHelperMethods._queryableSelectMethod;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00007EEF File Offset: 0x000060EF
		public static MethodInfo EnumerableSelectGeneric
		{
			get
			{
				return ExpressionHelperMethods._enumerableSelectMethod;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00007EF6 File Offset: 0x000060F6
		public static MethodInfo QueryableSelectManyGeneric
		{
			get
			{
				return ExpressionHelperMethods._queryableSelectManyMethod;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00007EFD File Offset: 0x000060FD
		public static MethodInfo EnumerableSelectManyGeneric
		{
			get
			{
				return ExpressionHelperMethods._enumerableSelectManyMethod;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00007F04 File Offset: 0x00006104
		public static MethodInfo QueryableEmptyAnyGeneric
		{
			get
			{
				return ExpressionHelperMethods._queryableEmptyAnyMethod;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00007F0B File Offset: 0x0000610B
		public static MethodInfo QueryableNonEmptyAnyGeneric
		{
			get
			{
				return ExpressionHelperMethods._queryableNonEmptyAnyMethod;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00007F12 File Offset: 0x00006112
		public static MethodInfo QueryableAllGeneric
		{
			get
			{
				return ExpressionHelperMethods._queryableAllMethod;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00007F19 File Offset: 0x00006119
		public static MethodInfo EnumerableEmptyAnyGeneric
		{
			get
			{
				return ExpressionHelperMethods._enumerableEmptyAnyMethod;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00007F20 File Offset: 0x00006120
		public static MethodInfo EnumerableNonEmptyAnyGeneric
		{
			get
			{
				return ExpressionHelperMethods._enumerableNonEmptyAnyMethod;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00007F27 File Offset: 0x00006127
		public static MethodInfo EnumerableAllGeneric
		{
			get
			{
				return ExpressionHelperMethods._enumerableAllMethod;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00007F2E File Offset: 0x0000612E
		public static MethodInfo EnumerableOfType
		{
			get
			{
				return ExpressionHelperMethods._enumerableOfTypeMethod;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00007F35 File Offset: 0x00006135
		public static MethodInfo QueryableOfType
		{
			get
			{
				return ExpressionHelperMethods._queryableOfTypeMethod;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00007F3C File Offset: 0x0000613C
		public static MethodInfo QueryableAsQueryable
		{
			get
			{
				return ExpressionHelperMethods._queryableAsQueryableMethod;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00007F43 File Offset: 0x00006143
		public static MethodInfo EntityAsQueryable
		{
			get
			{
				return ExpressionHelperMethods._toQueryableMethod;
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00007F4A File Offset: 0x0000614A
		public static IQueryable ToQueryable<T>(T value)
		{
			return new List<T> { value }.AsQueryable<T>();
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00007F5D File Offset: 0x0000615D
		public static MethodInfo EnumerableCountGeneric
		{
			get
			{
				return ExpressionHelperMethods._enumerableCountMethod;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00007F64 File Offset: 0x00006164
		public static MethodInfo ConvertToDecimal
		{
			get
			{
				return ExpressionHelperMethods._safeConvertToDecimalMethod;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00007F6B File Offset: 0x0000616B
		public static MethodInfo CreateQueryGeneric
		{
			get
			{
				return ExpressionHelperMethods._createQueryGenericMethod;
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00007F74 File Offset: 0x00006174
		public static decimal? SafeConvertToDecimal(object value)
		{
			if (value == null || value == DBNull.Value)
			{
				return null;
			}
			Type type = value.GetType();
			type = Nullable.GetUnderlyingType(type) ?? type;
			if (type == typeof(short) || type == typeof(int) || type == typeof(long) || type == typeof(decimal) || type == typeof(double) || type == typeof(float))
			{
				return (decimal?)Convert.ChangeType(value, typeof(decimal), CultureInfo.InvariantCulture);
			}
			return null;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00008039 File Offset: 0x00006239
		private static MethodInfo GenericMethodOf<TReturn>(Expression<Func<object, TReturn>> expression)
		{
			return ExpressionHelperMethods.GenericMethodOf(expression);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00008041 File Offset: 0x00006241
		private static MethodInfo GenericMethodOf(Expression expression)
		{
			return ((expression as LambdaExpression).Body as MethodCallExpression).Method.GetGenericMethodDefinition();
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00008060 File Offset: 0x00006260
		private static Dictionary<Type, MethodInfo> GetQueryableAggregationMethods(string methodName)
		{
			return (from m in typeof(Queryable).GetMethods()
				where m.Name == methodName
				where m.GetParameters().Count<ParameterInfo>() == 2
				select m).ToDictionary((MethodInfo m) => m.ReturnType);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000080E4 File Offset: 0x000062E4
		private static Dictionary<Type, MethodInfo> GetEnumerableAggregationMethods(string methodName)
		{
			return (from m in typeof(Enumerable).GetMethods()
				where m.Name == methodName
				where m.GetParameters().Count<ParameterInfo>() == 2
				select m).ToDictionary((MethodInfo m) => m.ReturnType);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00008168 File Offset: 0x00006368
		private static MethodInfo GetCreateQueryGenericMethod()
		{
			return (from m in typeof(IQueryProvider).GetTypeInfo().GetDeclaredMethods("CreateQuery")
				where m.IsGenericMethod
				select m).FirstOrDefault<MethodInfo>();
		}

		// Token: 0x0400007C RID: 124
		private static MethodInfo _enumerableWhereMethod = ExpressionHelperMethods.GenericMethodOf<IEnumerable<int>>((object _) => Enumerable.Where((IEnumerable<TSource>)null, null));

		// Token: 0x0400007D RID: 125
		private static MethodInfo _queryableToListMethod = ExpressionHelperMethods.GenericMethodOf<List<int>>((object _) => Enumerable.ToList((IEnumerable<TSource>)null));

		// Token: 0x0400007E RID: 126
		private static MethodInfo _orderByMethod = ExpressionHelperMethods.GenericMethodOf<IOrderedQueryable<int>>((object _) => Queryable.OrderBy((IQueryable<TSource>)null, null));

		// Token: 0x0400007F RID: 127
		private static MethodInfo _enumerableOrderByMethod = ExpressionHelperMethods.GenericMethodOf<IOrderedEnumerable<int>>((object _) => Enumerable.OrderBy((IEnumerable<TSource>)null, null));

		// Token: 0x04000080 RID: 128
		private static MethodInfo _orderByDescendingMethod = ExpressionHelperMethods.GenericMethodOf<IOrderedQueryable<int>>((object _) => Queryable.OrderByDescending((IQueryable<TSource>)null, null));

		// Token: 0x04000081 RID: 129
		private static MethodInfo _enumerableOrderByDescendingMethod = ExpressionHelperMethods.GenericMethodOf<IOrderedEnumerable<int>>((object _) => Enumerable.OrderByDescending((IEnumerable<TSource>)null, null));

		// Token: 0x04000082 RID: 130
		private static MethodInfo _thenByMethod = ExpressionHelperMethods.GenericMethodOf<IOrderedQueryable<int>>((object _) => Queryable.ThenBy((IOrderedQueryable<TSource>)null, null));

		// Token: 0x04000083 RID: 131
		private static MethodInfo _enumerableThenByMethod = ExpressionHelperMethods.GenericMethodOf<IOrderedEnumerable<int>>((object _) => Enumerable.ThenBy((IOrderedEnumerable<TSource>)null, null));

		// Token: 0x04000084 RID: 132
		private static MethodInfo _thenByDescendingMethod = ExpressionHelperMethods.GenericMethodOf<IOrderedQueryable<int>>((object _) => Queryable.ThenByDescending((IOrderedQueryable<TSource>)null, null));

		// Token: 0x04000085 RID: 133
		private static MethodInfo _enumerableThenByDescendingMethod = ExpressionHelperMethods.GenericMethodOf<IOrderedEnumerable<int>>((object _) => Enumerable.ThenByDescending((IOrderedEnumerable<TSource>)null, null));

		// Token: 0x04000086 RID: 134
		private static MethodInfo _countMethod = ExpressionHelperMethods.GenericMethodOf<long>((object _) => Queryable.LongCount((IQueryable<TSource>)null));

		// Token: 0x04000087 RID: 135
		private static MethodInfo _enumerableGroupByMethod = ExpressionHelperMethods.GenericMethodOf<IEnumerable<IGrouping<int, int>>>((object _) => Enumerable.GroupBy((IEnumerable<TSource>)null, null));

		// Token: 0x04000088 RID: 136
		private static MethodInfo _groupByMethod = ExpressionHelperMethods.GenericMethodOf<IQueryable<IGrouping<int, int>>>((object _) => Queryable.GroupBy((IQueryable<TSource>)null, null));

		// Token: 0x04000089 RID: 137
		private static MethodInfo _aggregateMethod = ExpressionHelperMethods.GenericMethodOf<int>((object _) => Queryable.Aggregate((IQueryable<TSource>)null, 0, null));

		// Token: 0x0400008A RID: 138
		private static MethodInfo _skipMethod = ExpressionHelperMethods.GenericMethodOf<IQueryable<int>>((object _) => Queryable.Skip((IQueryable<TSource>)null, 0));

		// Token: 0x0400008B RID: 139
		private static MethodInfo _enumerableSkipMethod = ExpressionHelperMethods.GenericMethodOf<IEnumerable<int>>((object _) => Enumerable.Skip((IEnumerable<TSource>)null, 0));

		// Token: 0x0400008C RID: 140
		private static MethodInfo _whereMethod = ExpressionHelperMethods.GenericMethodOf<IQueryable<int>>((object _) => Queryable.Where((IQueryable<TSource>)null, null));

		// Token: 0x0400008D RID: 141
		private static MethodInfo _queryableContainsMethod = ExpressionHelperMethods.GenericMethodOf<bool>((object _) => Queryable.Contains((IQueryable<TSource>)null, 0));

		// Token: 0x0400008E RID: 142
		private static MethodInfo _enumerableContainsMethod = ExpressionHelperMethods.GenericMethodOf<bool>((object _) => Enumerable.Contains((IEnumerable<TSource>)null, 0));

		// Token: 0x0400008F RID: 143
		private static MethodInfo _queryableEmptyAnyMethod = ExpressionHelperMethods.GenericMethodOf<bool>((object _) => Queryable.Any((IQueryable<TSource>)null));

		// Token: 0x04000090 RID: 144
		private static MethodInfo _queryableNonEmptyAnyMethod = ExpressionHelperMethods.GenericMethodOf<bool>((object _) => Queryable.Any((IQueryable<TSource>)null, null));

		// Token: 0x04000091 RID: 145
		private static MethodInfo _queryableAllMethod = ExpressionHelperMethods.GenericMethodOf<bool>((object _) => Queryable.All((IQueryable<TSource>)null, null));

		// Token: 0x04000092 RID: 146
		private static MethodInfo _enumerableEmptyAnyMethod = ExpressionHelperMethods.GenericMethodOf<bool>((object _) => Enumerable.Any((IEnumerable<TSource>)null));

		// Token: 0x04000093 RID: 147
		private static MethodInfo _enumerableNonEmptyAnyMethod = ExpressionHelperMethods.GenericMethodOf<bool>((object _) => Enumerable.Any((IEnumerable<TSource>)null, null));

		// Token: 0x04000094 RID: 148
		private static MethodInfo _enumerableAllMethod = ExpressionHelperMethods.GenericMethodOf<bool>((object _) => Enumerable.All((IEnumerable<TSource>)null, null));

		// Token: 0x04000095 RID: 149
		private static MethodInfo _enumerableOfTypeMethod = ExpressionHelperMethods.GenericMethodOf<IEnumerable<int>>((object _) => Enumerable.OfType((IEnumerable)null));

		// Token: 0x04000096 RID: 150
		private static MethodInfo _queryableOfTypeMethod = ExpressionHelperMethods.GenericMethodOf<IQueryable<int>>((object _) => Queryable.OfType((IQueryable)null));

		// Token: 0x04000097 RID: 151
		private static MethodInfo _enumerableSelectManyMethod = ExpressionHelperMethods.GenericMethodOf<IEnumerable<int>>((object _) => Enumerable.SelectMany((IEnumerable<TSource>)null, null));

		// Token: 0x04000098 RID: 152
		private static MethodInfo _queryableSelectManyMethod = ExpressionHelperMethods.GenericMethodOf<IQueryable<int>>((object _) => Queryable.SelectMany((IQueryable<TSource>)null, null));

		// Token: 0x04000099 RID: 153
		private static MethodInfo _enumerableSelectMethod = ExpressionHelperMethods.GenericMethodOf<IEnumerable<int>>((object _) => Enumerable.Select((IEnumerable<TSource>)null, (int i) => i));

		// Token: 0x0400009A RID: 154
		private static MethodInfo _queryableSelectMethod = ExpressionHelperMethods.GenericMethodOf<IQueryable<int>>((object _) => Queryable.Select((IQueryable<TSource>)null, (int i) => i));

		// Token: 0x0400009B RID: 155
		private static MethodInfo _queryableTakeMethod = ExpressionHelperMethods.GenericMethodOf<IQueryable<int>>((object _) => Queryable.Take((IQueryable<TSource>)null, 0));

		// Token: 0x0400009C RID: 156
		private static MethodInfo _enumerableTakeMethod = ExpressionHelperMethods.GenericMethodOf<IEnumerable<int>>((object _) => Enumerable.Take((IEnumerable<TSource>)null, 0));

		// Token: 0x0400009D RID: 157
		private static MethodInfo _queryableAsQueryableMethod = ExpressionHelperMethods.GenericMethodOf<IQueryable<int>>((object _) => Queryable.AsQueryable((IEnumerable<TElement>)null));

		// Token: 0x0400009E RID: 158
		private static MethodInfo _toQueryableMethod = ExpressionHelperMethods.GenericMethodOf<IQueryable>((object _) => ExpressionHelperMethods.ToQueryable<int>(0));

		// Token: 0x0400009F RID: 159
		private static Dictionary<Type, MethodInfo> _queryableSumMethods = ExpressionHelperMethods.GetQueryableAggregationMethods("Sum");

		// Token: 0x040000A0 RID: 160
		private static Dictionary<Type, MethodInfo> _enumerableSumMethods = ExpressionHelperMethods.GetEnumerableAggregationMethods("Sum");

		// Token: 0x040000A1 RID: 161
		private static MethodInfo _enumerableMinMethod = ExpressionHelperMethods.GenericMethodOf<int>((object _) => Enumerable.Min((IEnumerable<TSource>)null, null));

		// Token: 0x040000A2 RID: 162
		private static MethodInfo _enumerableMaxMethod = ExpressionHelperMethods.GenericMethodOf<int>((object _) => Enumerable.Max((IEnumerable<TSource>)null, null));

		// Token: 0x040000A3 RID: 163
		private static MethodInfo _enumerableDistinctMethod = ExpressionHelperMethods.GenericMethodOf<IEnumerable<int>>((object _) => Enumerable.Distinct((IEnumerable<TSource>)null));

		// Token: 0x040000A4 RID: 164
		private static MethodInfo _queryableMinMethod = ExpressionHelperMethods.GenericMethodOf<int>((object _) => Queryable.Min((IQueryable<TSource>)null, null));

		// Token: 0x040000A5 RID: 165
		private static MethodInfo _queryableMaxMethod = ExpressionHelperMethods.GenericMethodOf<int>((object _) => Queryable.Max((IQueryable<TSource>)null, null));

		// Token: 0x040000A6 RID: 166
		private static MethodInfo _queryableDistinctMethod = ExpressionHelperMethods.GenericMethodOf<IQueryable<int>>((object _) => Queryable.Distinct((IQueryable<TSource>)null));

		// Token: 0x040000A7 RID: 167
		private static MethodInfo _createQueryGenericMethod = ExpressionHelperMethods.GetCreateQueryGenericMethod();

		// Token: 0x040000A8 RID: 168
		private static Dictionary<Type, MethodInfo> _enumerableAverageMethods = new Dictionary<Type, MethodInfo>
		{
			{
				typeof(int),
				ExpressionHelperMethods.GenericMethodOf<double>((object _) => Enumerable.Average((IEnumerable<TSource>)null, null))
			},
			{
				typeof(int?),
				ExpressionHelperMethods.GenericMethodOf<double?>((object _) => Enumerable.Average((IEnumerable<TSource>)null, null))
			},
			{
				typeof(long),
				ExpressionHelperMethods.GenericMethodOf<double>((object _) => Enumerable.Average((IEnumerable<TSource>)null, null))
			},
			{
				typeof(long?),
				ExpressionHelperMethods.GenericMethodOf<double?>((object _) => Enumerable.Average((IEnumerable<TSource>)null, null))
			},
			{
				typeof(float),
				ExpressionHelperMethods.GenericMethodOf<float>((object _) => Enumerable.Average((IEnumerable<TSource>)null, null))
			},
			{
				typeof(float?),
				ExpressionHelperMethods.GenericMethodOf<float?>((object _) => Enumerable.Average((IEnumerable<TSource>)null, null))
			},
			{
				typeof(decimal),
				ExpressionHelperMethods.GenericMethodOf<decimal>((object _) => Enumerable.Average((IEnumerable<TSource>)null, null))
			},
			{
				typeof(decimal?),
				ExpressionHelperMethods.GenericMethodOf<decimal?>((object _) => Enumerable.Average((IEnumerable<TSource>)null, null))
			},
			{
				typeof(double),
				ExpressionHelperMethods.GenericMethodOf<double>((object _) => Enumerable.Average((IEnumerable<TSource>)null, null))
			},
			{
				typeof(double?),
				ExpressionHelperMethods.GenericMethodOf<double?>((object _) => Enumerable.Average((IEnumerable<TSource>)null, null))
			}
		};

		// Token: 0x040000A9 RID: 169
		private static Dictionary<Type, MethodInfo> _queryableAverageMethods = new Dictionary<Type, MethodInfo>
		{
			{
				typeof(int),
				ExpressionHelperMethods.GenericMethodOf<double>((object _) => Queryable.Average((IQueryable<TSource>)null, null))
			},
			{
				typeof(int?),
				ExpressionHelperMethods.GenericMethodOf<double?>((object _) => Queryable.Average((IQueryable<TSource>)null, null))
			},
			{
				typeof(long),
				ExpressionHelperMethods.GenericMethodOf<double>((object _) => Queryable.Average((IQueryable<TSource>)null, null))
			},
			{
				typeof(long?),
				ExpressionHelperMethods.GenericMethodOf<double?>((object _) => Queryable.Average((IQueryable<TSource>)null, null))
			},
			{
				typeof(float),
				ExpressionHelperMethods.GenericMethodOf<float>((object _) => Queryable.Average((IQueryable<TSource>)null, null))
			},
			{
				typeof(float?),
				ExpressionHelperMethods.GenericMethodOf<float?>((object _) => Queryable.Average((IQueryable<TSource>)null, null))
			},
			{
				typeof(decimal),
				ExpressionHelperMethods.GenericMethodOf<decimal>((object _) => Queryable.Average((IQueryable<TSource>)null, null))
			},
			{
				typeof(decimal?),
				ExpressionHelperMethods.GenericMethodOf<decimal?>((object _) => Queryable.Average((IQueryable<TSource>)null, null))
			},
			{
				typeof(double),
				ExpressionHelperMethods.GenericMethodOf<double>((object _) => Queryable.Average((IQueryable<TSource>)null, null))
			},
			{
				typeof(double?),
				ExpressionHelperMethods.GenericMethodOf<double?>((object _) => Queryable.Average((IQueryable<TSource>)null, null))
			}
		};

		// Token: 0x040000AA RID: 170
		private static MethodInfo _enumerableCountMethod = ExpressionHelperMethods.GenericMethodOf<long>((object _) => Enumerable.LongCount((IEnumerable<TSource>)null));

		// Token: 0x040000AB RID: 171
		private static MethodInfo _safeConvertToDecimalMethod = typeof(ExpressionHelperMethods).GetMethod("SafeConvertToDecimal");
	}
}
