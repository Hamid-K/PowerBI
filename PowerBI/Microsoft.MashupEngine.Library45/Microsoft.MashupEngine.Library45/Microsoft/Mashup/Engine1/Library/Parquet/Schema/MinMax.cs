using System;
using System.Linq.Expressions;
using ParquetSharp;

namespace Microsoft.Mashup.Engine1.Library.Parquet.Schema
{
	// Token: 0x02001F84 RID: 8068
	internal struct MinMax<T>
	{
		// Token: 0x06010ED1 RID: 69329 RVA: 0x003A573B File Offset: 0x003A393B
		public MinMax(T min, T max)
		{
			this.Min = min;
			this.Max = max;
		}

		// Token: 0x06010ED2 RID: 69330 RVA: 0x003A574B File Offset: 0x003A394B
		public static MinMax<T> GetMinMax(Statistics statistics)
		{
			return MinMax<T>.Getter(statistics);
		}

		// Token: 0x17002CD2 RID: 11474
		// (get) Token: 0x06010ED3 RID: 69331 RVA: 0x003A5758 File Offset: 0x003A3958
		private static Func<Statistics, MinMax<T>> Getter
		{
			get
			{
				if (MinMax<T>.getter == null)
				{
					MinMax<T>.getter = MinMax<T>.MakeGetter();
				}
				return MinMax<T>.getter;
			}
		}

		// Token: 0x06010ED4 RID: 69332 RVA: 0x003A5770 File Offset: 0x003A3970
		private static Func<Statistics, MinMax<T>> MakeGetter()
		{
			Type typeFromHandle = typeof(T);
			Type type = typeof(Statistics<>).MakeGenericType(new Type[] { typeFromHandle });
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Statistics), "statistics");
			UnaryExpression unaryExpression = Expression.Convert(parameterExpression, type);
			Expression expression = Expression.Property(unaryExpression, "Min");
			Expression expression2 = Expression.Property(unaryExpression, "Max");
			return Expression.Lambda<Func<Statistics, MinMax<T>>>(Expression.New(typeof(MinMax<T>).GetConstructor(new Type[] { typeFromHandle, typeFromHandle }), new Expression[] { expression, expression2 }), new ParameterExpression[] { parameterExpression }).Compile();
		}

		// Token: 0x040065F2 RID: 26098
		private static Func<Statistics, MinMax<T>> getter;

		// Token: 0x040065F3 RID: 26099
		public readonly T Min;

		// Token: 0x040065F4 RID: 26100
		public readonly T Max;
	}
}
