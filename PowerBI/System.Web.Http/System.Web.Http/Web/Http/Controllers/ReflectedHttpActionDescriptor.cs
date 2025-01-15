using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Internal;
using System.Web.Http.Properties;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000FF RID: 255
	public class ReflectedHttpActionDescriptor : HttpActionDescriptor
	{
		// Token: 0x060006A4 RID: 1700 RVA: 0x00010998 File Offset: 0x0000EB98
		public ReflectedHttpActionDescriptor()
		{
			this._parameters = new Lazy<Collection<HttpParameterDescriptor>>(() => this.InitializeParameterDescriptors());
			this._supportedHttpMethods = new Collection<HttpMethod>();
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x000109C2 File Offset: 0x0000EBC2
		public ReflectedHttpActionDescriptor(HttpControllerDescriptor controllerDescriptor, MethodInfo methodInfo)
			: base(controllerDescriptor)
		{
			if (methodInfo == null)
			{
				throw Error.ArgumentNull("methodInfo");
			}
			this.InitializeProperties(methodInfo);
			this._parameters = new Lazy<Collection<HttpParameterDescriptor>>(() => this.InitializeParameterDescriptors());
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x000109FD File Offset: 0x0000EBFD
		public override string ActionName
		{
			get
			{
				return this._actionName;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x00010A05 File Offset: 0x0000EC05
		public override Collection<HttpMethod> SupportedHttpMethods
		{
			get
			{
				return this._supportedHttpMethods;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x00010A0D File Offset: 0x0000EC0D
		// (set) Token: 0x060006A9 RID: 1705 RVA: 0x00010A15 File Offset: 0x0000EC15
		public MethodInfo MethodInfo
		{
			get
			{
				return this._methodInfo;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this.InitializeProperties(value);
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x00010A2D File Offset: 0x0000EC2D
		private ParameterInfo[] ParameterInfos
		{
			get
			{
				if (this._parameterInfos == null)
				{
					this._parameterInfos = this._methodInfo.GetParameters();
				}
				return this._parameterInfos;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00010A4E File Offset: 0x0000EC4E
		public override Type ReturnType
		{
			get
			{
				return this._returnType;
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00010A56 File Offset: 0x0000EC56
		public override Collection<T> GetCustomAttributes<T>(bool inherit)
		{
			return new Collection<T>(TypeHelper.OfType<T>(inherit ? this._attributeCache : this._declaredOnlyAttributeCache));
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00010A74 File Offset: 0x0000EC74
		public override Task<object> ExecuteAsync(HttpControllerContext controllerContext, IDictionary<string, object> arguments, CancellationToken cancellationToken)
		{
			if (controllerContext == null)
			{
				throw Error.ArgumentNull("controllerContext");
			}
			if (arguments == null)
			{
				throw Error.ArgumentNull("arguments");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return TaskHelpers.Canceled<object>();
			}
			Task<object> task;
			try
			{
				object[] array = this.PrepareParameters(arguments, controllerContext);
				task = this._actionExecutor.Value.Execute(controllerContext.Controller, array);
			}
			catch (Exception ex)
			{
				task = TaskHelpers.FromError<object>(ex);
			}
			return task;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00010AEC File Offset: 0x0000ECEC
		public override Collection<IFilter> GetFilters()
		{
			return new Collection<IFilter>(this.GetCustomAttributes<IFilter>().Concat(base.GetFilters()).ToList<IFilter>());
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00010B09 File Offset: 0x0000ED09
		public override Collection<HttpParameterDescriptor> GetParameters()
		{
			return this._parameters.Value;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00010B18 File Offset: 0x0000ED18
		private void InitializeProperties(MethodInfo methodInfo)
		{
			this._methodInfo = methodInfo;
			this._parameterInfos = null;
			this._returnType = ReflectedHttpActionDescriptor.GetReturnType(methodInfo);
			this._actionExecutor = new Lazy<ReflectedHttpActionDescriptor.ActionExecutor>(() => ReflectedHttpActionDescriptor.InitializeActionExecutor(this._methodInfo));
			this._declaredOnlyAttributeCache = this._methodInfo.GetCustomAttributes(false);
			this._attributeCache = this._methodInfo.GetCustomAttributes(true);
			this._actionName = ReflectedHttpActionDescriptor.GetActionName(this._methodInfo, this._attributeCache);
			this._supportedHttpMethods = ReflectedHttpActionDescriptor.GetSupportedHttpMethods(this._methodInfo, this._attributeCache);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00010BA8 File Offset: 0x0000EDA8
		internal static Type GetReturnType(MethodInfo methodInfo)
		{
			Type type = methodInfo.ReturnType;
			if (typeof(Task).IsAssignableFrom(type))
			{
				type = TypeHelper.GetTaskInnerTypeOrNull(methodInfo.ReturnType);
			}
			if (type == typeof(void))
			{
				type = null;
			}
			return type;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00010BEF File Offset: 0x0000EDEF
		private Collection<HttpParameterDescriptor> InitializeParameterDescriptors()
		{
			return new Collection<HttpParameterDescriptor>(this.ParameterInfos.Select((ParameterInfo item) => new ReflectedHttpParameterDescriptor(this, item)).ToList<HttpParameterDescriptor>());
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00010C14 File Offset: 0x0000EE14
		private object[] PrepareParameters(IDictionary<string, object> parameters, HttpControllerContext controllerContext)
		{
			if (this._parameters.Value.Count == 0)
			{
				return ReflectedHttpActionDescriptor._empty;
			}
			ParameterInfo[] parameterInfos = this.ParameterInfos;
			int num = parameterInfos.Length;
			object[] array = new object[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = this.ExtractParameterFromDictionary(parameterInfos[i], parameters, controllerContext);
			}
			return array;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00010C68 File Offset: 0x0000EE68
		private object ExtractParameterFromDictionary(ParameterInfo parameterInfo, IDictionary<string, object> parameters, HttpControllerContext controllerContext)
		{
			object obj;
			if (!parameters.TryGetValue(parameterInfo.Name, out obj))
			{
				throw new HttpResponseException(controllerContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, SRResources.BadRequest, Error.Format(SRResources.ReflectedActionDescriptor_ParameterNotInDictionary, new object[]
				{
					parameterInfo.Name,
					parameterInfo.ParameterType,
					this.MethodInfo,
					this.MethodInfo.DeclaringType
				})));
			}
			if (obj == null && !TypeHelper.TypeAllowsNullValue(parameterInfo.ParameterType))
			{
				throw new HttpResponseException(controllerContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, SRResources.BadRequest, Error.Format(SRResources.ReflectedActionDescriptor_ParameterCannotBeNull, new object[]
				{
					parameterInfo.Name,
					parameterInfo.ParameterType,
					this.MethodInfo,
					this.MethodInfo.DeclaringType
				})));
			}
			if (obj != null && !parameterInfo.ParameterType.IsInstanceOfType(obj))
			{
				throw new HttpResponseException(controllerContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, SRResources.BadRequest, Error.Format(SRResources.ReflectedActionDescriptor_ParameterValueHasWrongType, new object[]
				{
					parameterInfo.Name,
					this.MethodInfo,
					this.MethodInfo.DeclaringType,
					obj.GetType(),
					parameterInfo.ParameterType
				})));
			}
			return obj;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00010DAC File Offset: 0x0000EFAC
		private static string GetActionName(MethodInfo methodInfo, object[] actionAttributes)
		{
			ActionNameAttribute actionNameAttribute = TypeHelper.OfType<ActionNameAttribute>(actionAttributes).FirstOrDefault<ActionNameAttribute>();
			if (actionNameAttribute == null)
			{
				return methodInfo.Name;
			}
			return actionNameAttribute.Name;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00010DD8 File Offset: 0x0000EFD8
		private static Collection<HttpMethod> GetSupportedHttpMethods(MethodInfo methodInfo, object[] actionAttributes)
		{
			Collection<HttpMethod> collection = new Collection<HttpMethod>();
			ICollection<IActionHttpMethodProvider> collection2 = TypeHelper.OfType<IActionHttpMethodProvider>(actionAttributes);
			if (collection2.Count > 0)
			{
				using (IEnumerator<IActionHttpMethodProvider> enumerator = collection2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IActionHttpMethodProvider actionHttpMethodProvider = enumerator.Current;
						foreach (HttpMethod httpMethod in actionHttpMethodProvider.HttpMethods)
						{
							collection.Add(httpMethod);
						}
					}
					goto IL_00AB;
				}
			}
			for (int i = 0; i < ReflectedHttpActionDescriptor._supportedHttpMethodsByConvention.Length; i++)
			{
				if (methodInfo.Name.StartsWith(ReflectedHttpActionDescriptor._supportedHttpMethodsByConvention[i].Method, StringComparison.OrdinalIgnoreCase))
				{
					collection.Add(ReflectedHttpActionDescriptor._supportedHttpMethodsByConvention[i]);
					break;
				}
			}
			IL_00AB:
			if (collection.Count == 0)
			{
				collection.Add(HttpMethod.Post);
			}
			return collection;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00010EC0 File Offset: 0x0000F0C0
		public override int GetHashCode()
		{
			if (this._methodInfo != null)
			{
				return this._methodInfo.GetHashCode();
			}
			return base.GetHashCode();
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00010EE4 File Offset: 0x0000F0E4
		public override bool Equals(object obj)
		{
			if (this._methodInfo != null)
			{
				ReflectedHttpActionDescriptor reflectedHttpActionDescriptor = obj as ReflectedHttpActionDescriptor;
				return reflectedHttpActionDescriptor != null && this._methodInfo.Equals(reflectedHttpActionDescriptor._methodInfo);
			}
			return base.Equals(obj);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00010F24 File Offset: 0x0000F124
		private static ReflectedHttpActionDescriptor.ActionExecutor InitializeActionExecutor(MethodInfo methodInfo)
		{
			if (methodInfo.ContainsGenericParameters)
			{
				throw Error.InvalidOperation(SRResources.ReflectedHttpActionDescriptor_CannotCallOpenGenericMethods, new object[]
				{
					methodInfo,
					methodInfo.ReflectedType.FullName
				});
			}
			return new ReflectedHttpActionDescriptor.ActionExecutor(methodInfo);
		}

		// Token: 0x040001B0 RID: 432
		private static readonly object[] _empty = new object[0];

		// Token: 0x040001B1 RID: 433
		private readonly Lazy<Collection<HttpParameterDescriptor>> _parameters;

		// Token: 0x040001B2 RID: 434
		private ParameterInfo[] _parameterInfos;

		// Token: 0x040001B3 RID: 435
		private Lazy<ReflectedHttpActionDescriptor.ActionExecutor> _actionExecutor;

		// Token: 0x040001B4 RID: 436
		private MethodInfo _methodInfo;

		// Token: 0x040001B5 RID: 437
		private Type _returnType;

		// Token: 0x040001B6 RID: 438
		private string _actionName;

		// Token: 0x040001B7 RID: 439
		private Collection<HttpMethod> _supportedHttpMethods;

		// Token: 0x040001B8 RID: 440
		private object[] _attributeCache;

		// Token: 0x040001B9 RID: 441
		private object[] _declaredOnlyAttributeCache;

		// Token: 0x040001BA RID: 442
		private static readonly HttpMethod[] _supportedHttpMethodsByConvention = new HttpMethod[]
		{
			HttpMethod.Get,
			HttpMethod.Post,
			HttpMethod.Put,
			HttpMethod.Delete,
			HttpMethod.Head,
			HttpMethod.Options,
			new HttpMethod("PATCH")
		};

		// Token: 0x020001FD RID: 509
		private sealed class ActionExecutor
		{
			// Token: 0x06000BB2 RID: 2994 RVA: 0x0001E70F File Offset: 0x0001C90F
			public ActionExecutor(MethodInfo methodInfo)
			{
				this._executor = ReflectedHttpActionDescriptor.ActionExecutor.GetExecutor(methodInfo);
			}

			// Token: 0x06000BB3 RID: 2995 RVA: 0x0001E723 File Offset: 0x0001C923
			public Task<object> Execute(object instance, object[] arguments)
			{
				return this._executor(instance, arguments);
			}

			// Token: 0x06000BB4 RID: 2996 RVA: 0x0001E732 File Offset: 0x0001C932
			private static Task<object> Convert<T>(object taskAsObject)
			{
				return ((Task<T>)taskAsObject).CastToObject<T>();
			}

			// Token: 0x06000BB5 RID: 2997 RVA: 0x0001E73F File Offset: 0x0001C93F
			[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
			private static Func<object, Task<object>> CompileGenericTaskConversionDelegate(Type taskValueType)
			{
				return (Func<object, Task<object>>)Delegate.CreateDelegate(typeof(Func<object, Task<object>>), ReflectedHttpActionDescriptor.ActionExecutor._convertOfTMethod.MakeGenericMethod(new Type[] { taskValueType }));
			}

			// Token: 0x06000BB6 RID: 2998 RVA: 0x0001E76C File Offset: 0x0001C96C
			private static Func<object, object[], Task<object>> GetExecutor(MethodInfo methodInfo)
			{
				ReflectedHttpActionDescriptor.ActionExecutor.<>c__DisplayClass6_0 CS$<>8__locals1 = new ReflectedHttpActionDescriptor.ActionExecutor.<>c__DisplayClass6_0();
				CS$<>8__locals1.methodInfo = methodInfo;
				ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "instance");
				ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object[]), "parameters");
				List<Expression> list = new List<Expression>();
				ParameterInfo[] parameters = CS$<>8__locals1.methodInfo.GetParameters();
				for (int i = 0; i < parameters.Length; i++)
				{
					ParameterInfo parameterInfo = parameters[i];
					UnaryExpression unaryExpression = Expression.Convert(Expression.ArrayIndex(parameterExpression2, Expression.Constant(i)), parameterInfo.ParameterType);
					list.Add(unaryExpression);
				}
				MethodCallExpression methodCallExpression = Expression.Call((!CS$<>8__locals1.methodInfo.IsStatic) ? Expression.Convert(parameterExpression, CS$<>8__locals1.methodInfo.ReflectedType) : null, CS$<>8__locals1.methodInfo, list);
				if (methodCallExpression.Type == typeof(void))
				{
					Expression<Action<object, object[]>> expression = Expression.Lambda<Action<object, object[]>>(methodCallExpression, new ParameterExpression[] { parameterExpression, parameterExpression2 });
					Action<object, object[]> voidExecutor = expression.Compile();
					return delegate(object instance, object[] methodParameters)
					{
						voidExecutor(instance, methodParameters);
						return TaskHelpers.NullResult();
					};
				}
				Expression<Func<object, object[], object>> expression2 = Expression.Lambda<Func<object, object[], object>>(Expression.Convert(methodCallExpression, typeof(object)), new ParameterExpression[] { parameterExpression, parameterExpression2 });
				Func<object, object[], object> compiled = expression2.Compile();
				if (methodCallExpression.Type == typeof(Task))
				{
					return delegate(object instance, object[] methodParameters)
					{
						Task task = (Task)compiled(instance, methodParameters);
						ReflectedHttpActionDescriptor.ActionExecutor.ThrowIfWrappedTaskInstance(CS$<>8__locals1.methodInfo, task.GetType());
						return task.CastToObject();
					};
				}
				if (typeof(Task).IsAssignableFrom(methodCallExpression.Type))
				{
					Type taskInnerTypeOrNull = TypeHelper.GetTaskInnerTypeOrNull(methodCallExpression.Type);
					Func<object, Task<object>> compiledConversion = ReflectedHttpActionDescriptor.ActionExecutor.CompileGenericTaskConversionDelegate(taskInnerTypeOrNull);
					return delegate(object instance, object[] methodParameters)
					{
						object obj = compiled(instance, methodParameters);
						return compiledConversion(obj);
					};
				}
				return delegate(object instance, object[] methodParameters)
				{
					object obj2 = compiled(instance, methodParameters);
					if (obj2 is Task)
					{
						throw Error.InvalidOperation(SRResources.ActionExecutor_UnexpectedTaskInstance, new object[]
						{
							CS$<>8__locals1.methodInfo.Name,
							CS$<>8__locals1.methodInfo.DeclaringType.Name
						});
					}
					return Task.FromResult<object>(obj2);
				};
			}

			// Token: 0x06000BB7 RID: 2999 RVA: 0x0001E94C File Offset: 0x0001CB4C
			private static void ThrowIfWrappedTaskInstance(MethodInfo method, Type type)
			{
				if (type != typeof(Task))
				{
					Type taskInnerTypeOrNull = TypeHelper.GetTaskInnerTypeOrNull(type);
					if (taskInnerTypeOrNull != null && typeof(Task).IsAssignableFrom(taskInnerTypeOrNull))
					{
						throw Error.InvalidOperation(SRResources.ActionExecutor_WrappedTaskInstance, new object[]
						{
							method.Name,
							method.DeclaringType.Name,
							type.FullName
						});
					}
				}
			}

			// Token: 0x0400042F RID: 1071
			private readonly Func<object, object[], Task<object>> _executor;

			// Token: 0x04000430 RID: 1072
			private static MethodInfo _convertOfTMethod = typeof(ReflectedHttpActionDescriptor.ActionExecutor).GetMethod("Convert", BindingFlags.Static | BindingFlags.NonPublic);
		}
	}
}
