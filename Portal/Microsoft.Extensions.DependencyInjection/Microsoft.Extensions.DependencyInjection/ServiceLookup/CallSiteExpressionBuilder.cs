using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x02000008 RID: 8
	internal class CallSiteExpressionBuilder : CallSiteVisitor<ParameterExpression, Expression>
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002866 File Offset: 0x00000A66
		public CallSiteExpressionBuilder(CallSiteRuntimeResolver runtimeResolver)
		{
			if (runtimeResolver == null)
			{
				throw new ArgumentNullException("runtimeResolver");
			}
			this._runtimeResolver = runtimeResolver;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002884 File Offset: 0x00000A84
		public Func<ServiceProvider, object> Build(IServiceCallSite callSite)
		{
			if (callSite is SingletonCallSite)
			{
				return (ServiceProvider provider) => this._runtimeResolver.Resolve(callSite, provider);
			}
			return this.BuildExpression(callSite).Compile();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000028D0 File Offset: 0x00000AD0
		private Expression<Func<ServiceProvider, object>> BuildExpression(IServiceCallSite callSite)
		{
			Expression expression = this.VisitCallSite(callSite, CallSiteExpressionBuilder.ProviderParameter);
			List<Expression> list = new List<Expression>();
			if (this._requiresResolvedServices)
			{
				list.Add(CallSiteExpressionBuilder.ResolvedServicesVariableAssignment);
				expression = CallSiteExpressionBuilder.Lock(expression, CallSiteExpressionBuilder.ResolvedServices);
			}
			list.Add(expression);
			IEnumerable<ParameterExpression> enumerable;
			if (!this._requiresResolvedServices)
			{
				enumerable = Enumerable.Empty<ParameterExpression>();
			}
			else
			{
				IEnumerable<ParameterExpression> enumerable2 = new ParameterExpression[] { CallSiteExpressionBuilder.ResolvedServices };
				enumerable = enumerable2;
			}
			return Expression.Lambda<Func<ServiceProvider, object>>(Expression.Block(enumerable, list), new ParameterExpression[] { CallSiteExpressionBuilder.ProviderParameter });
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000294E File Offset: 0x00000B4E
		protected override Expression VisitSingleton(SingletonCallSite singletonCallSite, ParameterExpression provider)
		{
			return Expression.Call(Expression.Constant(this._runtimeResolver), CallSiteExpressionBuilder.CallSiteRuntimeResolverResolve, Expression.Constant(singletonCallSite, typeof(IServiceCallSite)), provider);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002976 File Offset: 0x00000B76
		protected override Expression VisitConstant(ConstantCallSite constantCallSite, ParameterExpression provider)
		{
			return Expression.Constant(constantCallSite.DefaultValue);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002983 File Offset: 0x00000B83
		protected override Expression VisitCreateInstance(CreateInstanceCallSite createInstanceCallSite, ParameterExpression provider)
		{
			return Expression.New(createInstanceCallSite.Descriptor.ImplementationType);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002995 File Offset: 0x00000B95
		protected override Expression VisitInstanceService(InstanceService instanceCallSite, ParameterExpression provider)
		{
			return Expression.Constant(instanceCallSite.Descriptor.ImplementationInstance, instanceCallSite.Descriptor.ServiceType);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000029B2 File Offset: 0x00000BB2
		protected override Expression VisitServiceProviderService(ServiceProviderService serviceProviderService, ParameterExpression provider)
		{
			return provider;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000029B5 File Offset: 0x00000BB5
		protected override Expression VisitEmptyIEnumerable(EmptyIEnumerableCallSite emptyIEnumerableCallSite, ParameterExpression provider)
		{
			return Expression.Constant(emptyIEnumerableCallSite.ServiceInstance, emptyIEnumerableCallSite.ServiceType);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000029C8 File Offset: 0x00000BC8
		protected override Expression VisitServiceScopeService(ServiceScopeService serviceScopeService, ParameterExpression provider)
		{
			return Expression.New(typeof(ServiceScopeFactory).GetTypeInfo().DeclaredConstructors.Single<ConstructorInfo>(), new Expression[] { provider });
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000029F2 File Offset: 0x00000BF2
		protected override Expression VisitFactoryService(FactoryService factoryService, ParameterExpression provider)
		{
			return Expression.Invoke(Expression.Constant(factoryService.Descriptor.ImplementationFactory), new Expression[] { provider });
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A14 File Offset: 0x00000C14
		protected override Expression VisitClosedIEnumerable(ClosedIEnumerableCallSite callSite, ParameterExpression provider)
		{
			return Expression.NewArrayInit(callSite.ItemType, callSite.ServiceCallSites.Select((IServiceCallSite cs) => Expression.Convert(this.VisitCallSite(cs, provider), callSite.ItemType)));
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002A68 File Offset: 0x00000C68
		protected override Expression VisitTransient(TransientCallSite callSite, ParameterExpression provider)
		{
			return Expression.Invoke(this.GetCaptureDisposable(provider), new Expression[] { this.VisitCallSite(callSite.Service, provider) });
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002A8C File Offset: 0x00000C8C
		protected override Expression VisitConstructor(ConstructorCallSite callSite, ParameterExpression provider)
		{
			ParameterInfo[] parameters = callSite.ConstructorInfo.GetParameters();
			return Expression.New(callSite.ConstructorInfo, callSite.ParameterCallSites.Select((IServiceCallSite c, int index) => Expression.Convert(this.VisitCallSite(c, provider), parameters[index].ParameterType)));
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002AE0 File Offset: 0x00000CE0
		protected override Expression VisitScoped(ScopedCallSite callSite, ParameterExpression provider)
		{
			ConstantExpression constantExpression = Expression.Constant(callSite.Key, typeof(object));
			ParameterExpression parameterExpression = Expression.Variable(typeof(object), "resolved");
			Expression resolvedServices = this.GetResolvedServices(provider);
			MethodCallExpression methodCallExpression = Expression.Call(resolvedServices, CallSiteExpressionBuilder.TryGetValueMethodInfo, constantExpression, parameterExpression);
			BinaryExpression binaryExpression = Expression.Assign(parameterExpression, this.VisitCallSite(callSite.ServiceCallSite, provider));
			MethodCallExpression methodCallExpression2 = Expression.Call(resolvedServices, CallSiteExpressionBuilder.AddMethodInfo, constantExpression, parameterExpression);
			return Expression.Block(typeof(object), new ParameterExpression[] { parameterExpression }, new Expression[]
			{
				Expression.IfThen(Expression.Not(methodCallExpression), Expression.Block(binaryExpression, methodCallExpression2)),
				parameterExpression
			});
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B88 File Offset: 0x00000D88
		private static MethodInfo GetMethodInfo<T>(Expression<T> expr)
		{
			return ((MethodCallExpression)expr.Body).Method;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B9A File Offset: 0x00000D9A
		public Expression GetCaptureDisposable(ParameterExpression provider)
		{
			if (provider != CallSiteExpressionBuilder.ProviderParameter)
			{
				throw new NotSupportedException("GetCaptureDisposable call is supported only for main provider");
			}
			return CallSiteExpressionBuilder.CaptureDisposable;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002BB4 File Offset: 0x00000DB4
		public Expression GetResolvedServices(ParameterExpression provider)
		{
			if (provider != CallSiteExpressionBuilder.ProviderParameter)
			{
				throw new NotSupportedException("GetResolvedServices call is supported only for main provider");
			}
			this._requiresResolvedServices = true;
			return CallSiteExpressionBuilder.ResolvedServices;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002BD8 File Offset: 0x00000DD8
		private static Expression Lock(Expression body, Expression syncVariable)
		{
			ParameterExpression parameterExpression = Expression.Variable(typeof(bool), "lockWasTaken");
			Expression expression = Expression.Call(CallSiteExpressionBuilder.MonitorEnterMethodInfo, syncVariable, parameterExpression);
			MethodCallExpression methodCallExpression = Expression.Call(CallSiteExpressionBuilder.MonitorExitMethodInfo, syncVariable);
			BlockExpression blockExpression = Expression.Block(expression, body);
			ConditionalExpression conditionalExpression = Expression.IfThen(parameterExpression, methodCallExpression);
			return Expression.Block(typeof(object), new ParameterExpression[] { parameterExpression }, new Expression[] { Expression.TryFinally(blockExpression, conditionalExpression) });
		}

		// Token: 0x0400000B RID: 11
		private static readonly MethodInfo CaptureDisposableMethodInfo = CallSiteExpressionBuilder.GetMethodInfo<Func<ServiceProvider, object, object>>((ServiceProvider a, object b) => a.CaptureDisposable(b));

		// Token: 0x0400000C RID: 12
		private static readonly MethodInfo TryGetValueMethodInfo = CallSiteExpressionBuilder.GetMethodInfo<Func<IDictionary<object, object>, object, object, bool>>((IDictionary<object, object> a, object b, object c) => a.TryGetValue(b, c));

		// Token: 0x0400000D RID: 13
		private static readonly MethodInfo AddMethodInfo = CallSiteExpressionBuilder.GetMethodInfo<Action<IDictionary<object, object>, object, object>>((IDictionary<object, object> a, object b, object c) => a.Add(b, c));

		// Token: 0x0400000E RID: 14
		private static readonly MethodInfo MonitorEnterMethodInfo = CallSiteExpressionBuilder.GetMethodInfo<Action<object, bool>>((object lockObj, bool lockTaken) => Monitor.Enter(lockObj, lockTaken));

		// Token: 0x0400000F RID: 15
		private static readonly MethodInfo MonitorExitMethodInfo = CallSiteExpressionBuilder.GetMethodInfo<Action<object>>((object lockObj) => Monitor.Exit(lockObj));

		// Token: 0x04000010 RID: 16
		private static readonly MethodInfo CallSiteRuntimeResolverResolve = CallSiteExpressionBuilder.GetMethodInfo<Func<CallSiteRuntimeResolver, IServiceCallSite, ServiceProvider, object>>((CallSiteRuntimeResolver r, IServiceCallSite c, ServiceProvider p) => r.Resolve(c, p));

		// Token: 0x04000011 RID: 17
		private static readonly ParameterExpression ProviderParameter = Expression.Parameter(typeof(ServiceProvider));

		// Token: 0x04000012 RID: 18
		private static readonly ParameterExpression ResolvedServices = Expression.Variable(typeof(IDictionary<object, object>), CallSiteExpressionBuilder.ProviderParameter.Name + "resolvedServices");

		// Token: 0x04000013 RID: 19
		private static readonly BinaryExpression ResolvedServicesVariableAssignment = Expression.Assign(CallSiteExpressionBuilder.ResolvedServices, Expression.Property(CallSiteExpressionBuilder.ProviderParameter, "ResolvedServices"));

		// Token: 0x04000014 RID: 20
		private static readonly ParameterExpression CaptureDisposableParameter = Expression.Parameter(typeof(object));

		// Token: 0x04000015 RID: 21
		private static readonly LambdaExpression CaptureDisposable = Expression.Lambda(Expression.Call(CallSiteExpressionBuilder.ProviderParameter, CallSiteExpressionBuilder.CaptureDisposableMethodInfo, new Expression[] { CallSiteExpressionBuilder.CaptureDisposableParameter }), new ParameterExpression[] { CallSiteExpressionBuilder.CaptureDisposableParameter });

		// Token: 0x04000016 RID: 22
		private readonly CallSiteRuntimeResolver _runtimeResolver;

		// Token: 0x04000017 RID: 23
		private bool _requiresResolvedServices;
	}
}
