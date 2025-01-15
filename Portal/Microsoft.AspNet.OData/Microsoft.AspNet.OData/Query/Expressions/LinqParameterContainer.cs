using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace Microsoft.AspNet.OData.Query.Expressions
{
	// Token: 0x020000F6 RID: 246
	internal abstract class LinqParameterContainer
	{
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x0600085B RID: 2139
		public abstract object Property { get; }

		// Token: 0x0600085C RID: 2140 RVA: 0x0002092C File Offset: 0x0001EB2C
		public static Expression Parameterize(Type type, object value)
		{
			return Expression.Property(Expression.Constant(LinqParameterContainer.Create(type, value)), "TypedProperty");
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00020944 File Offset: 0x0001EB44
		private static LinqParameterContainer Create(Type type, object value)
		{
			return LinqParameterContainer._ctors.GetOrAdd(type, delegate(Type t)
			{
				MethodInfo methodInfo = typeof(LinqParameterContainer).GetMethod("CreateInternal").MakeGenericMethod(new Type[] { t });
				ParameterExpression parameterExpression = Expression.Parameter(typeof(object));
				return Expression.Lambda<Func<object, LinqParameterContainer>>(Expression.Call(methodInfo, Expression.Convert(parameterExpression, t)), new ParameterExpression[] { parameterExpression }).Compile();
			})(value);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00020976 File Offset: 0x0001EB76
		public static LinqParameterContainer CreateInternal<T>(T value)
		{
			return new LinqParameterContainer.TypedLinqParameterContainer<T>(value);
		}

		// Token: 0x0400027B RID: 635
		private static ConcurrentDictionary<Type, Func<object, LinqParameterContainer>> _ctors = new ConcurrentDictionary<Type, Func<object, LinqParameterContainer>>();

		// Token: 0x020002C0 RID: 704
		internal class TypedLinqParameterContainer<T> : LinqParameterContainer
		{
			// Token: 0x060012F5 RID: 4853 RVA: 0x00042B5B File Offset: 0x00040D5B
			public TypedLinqParameterContainer(T value)
			{
				this.TypedProperty = value;
			}

			// Token: 0x17000497 RID: 1175
			// (get) Token: 0x060012F6 RID: 4854 RVA: 0x00042B6A File Offset: 0x00040D6A
			// (set) Token: 0x060012F7 RID: 4855 RVA: 0x00042B72 File Offset: 0x00040D72
			public T TypedProperty { get; set; }

			// Token: 0x17000498 RID: 1176
			// (get) Token: 0x060012F8 RID: 4856 RVA: 0x00042B7B File Offset: 0x00040D7B
			public override object Property
			{
				get
				{
					return this.TypedProperty;
				}
			}
		}
	}
}
