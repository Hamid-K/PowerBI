using System;
using System.Linq.Expressions;

namespace System.Web.Http.Internal
{
	// Token: 0x02000181 RID: 385
	internal static class TypeActivator
	{
		// Token: 0x060009FC RID: 2556 RVA: 0x00019C8A File Offset: 0x00017E8A
		public static Func<TBase> Create<TBase>(Type instanceType) where TBase : class
		{
			return Expression.Lambda<Func<TBase>>(Expression.New(instanceType), new ParameterExpression[0]).Compile();
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00019CA2 File Offset: 0x00017EA2
		public static Func<TInstance> Create<TInstance>() where TInstance : class
		{
			return TypeActivator.Create<TInstance>(typeof(TInstance));
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00019CB3 File Offset: 0x00017EB3
		public static Func<object> Create(Type instanceType)
		{
			return TypeActivator.Create<object>(instanceType);
		}
	}
}
