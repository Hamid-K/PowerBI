using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Owin.Infrastructure;
using Owin;

namespace Microsoft.Owin.Builder
{
	// Token: 0x02000042 RID: 66
	public class AppBuilder : IAppBuilder
	{
		// Token: 0x06000250 RID: 592 RVA: 0x0000676C File Offset: 0x0000496C
		public AppBuilder()
		{
			this._properties = new Dictionary<string, object>();
			this._conversions = new Dictionary<Tuple<Type, Type>, Delegate>();
			this._middleware = new List<Tuple<Type, Delegate, object[]>>();
			this._properties["builder.AddSignatureConversion"] = new Action<Delegate>(this.AddSignatureConversion);
			this._properties["builder.DefaultApp"] = AppBuilder.NotFound;
			SignatureConversions.AddConversions(this);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000067D7 File Offset: 0x000049D7
		internal AppBuilder(IDictionary<Tuple<Type, Type>, Delegate> conversions, IDictionary<string, object> properties)
		{
			this._properties = properties;
			this._conversions = conversions;
			this._middleware = new List<Tuple<Type, Delegate, object[]>>();
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000252 RID: 594 RVA: 0x000067F8 File Offset: 0x000049F8
		public IDictionary<string, object> Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00006800 File Offset: 0x00004A00
		public IAppBuilder Use(object middleware, params object[] args)
		{
			this._middleware.Add(AppBuilder.ToMiddlewareFactory(middleware, args));
			return this;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00006815 File Offset: 0x00004A15
		public IAppBuilder New()
		{
			return new AppBuilder(this._conversions, this._properties);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00006828 File Offset: 0x00004A28
		public object Build(Type returnType)
		{
			return this.BuildInternal(returnType);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00006834 File Offset: 0x00004A34
		private void AddSignatureConversion(Delegate conversion)
		{
			if (conversion == null)
			{
				throw new ArgumentNullException("conversion");
			}
			Type parameterType = AppBuilder.GetParameterType(conversion);
			if (parameterType == null)
			{
				throw new ArgumentException(Resources.Exception_ConversionTakesOneParameter, "conversion");
			}
			Tuple<Type, Type> key = Tuple.Create<Type, Type>(conversion.Method.ReturnType, parameterType);
			this._conversions[key] = conversion;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00006890 File Offset: 0x00004A90
		private static Type GetParameterType(Delegate function)
		{
			ParameterInfo[] parameters = function.Method.GetParameters();
			if (parameters.Length < 1)
			{
				return null;
			}
			return parameters[0].ParameterType;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000068BC File Offset: 0x00004ABC
		private object BuildInternal(Type signature)
		{
			object app;
			if (!this._properties.TryGetValue("builder.DefaultApp", out app))
			{
				app = AppBuilder.NotFound;
			}
			foreach (Tuple<Type, Delegate, object[]> middleware in this._middleware.Reverse<Tuple<Type, Delegate, object[]>>())
			{
				Type neededSignature = middleware.Item1;
				Delegate middlewareDelegate = middleware.Item2;
				object[] middlewareArgs = middleware.Item3;
				app = this.Convert(neededSignature, app);
				object[] invokeParameters = new object[] { app }.Concat(middlewareArgs).ToArray<object>();
				app = middlewareDelegate.DynamicInvoke(invokeParameters);
				app = this.Convert(neededSignature, app);
			}
			return this.Convert(signature, app);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00006974 File Offset: 0x00004B74
		private object Convert(Type signature, object app)
		{
			if (app == null)
			{
				return null;
			}
			object oneHop = this.ConvertOneHop(signature, app);
			if (oneHop != null)
			{
				return oneHop;
			}
			object multiHop = this.ConvertMultiHop(signature, app);
			if (multiHop != null)
			{
				return multiHop;
			}
			throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_NoConversionExists, new object[]
			{
				app.GetType(),
				signature
			}), "signature");
		}

		// Token: 0x0600025A RID: 602 RVA: 0x000069D0 File Offset: 0x00004BD0
		private object ConvertMultiHop(Type signature, object app)
		{
			foreach (KeyValuePair<Tuple<Type, Type>, Delegate> conversion in this._conversions)
			{
				object preConversion = this.ConvertOneHop(conversion.Key.Item2, app);
				if (preConversion != null)
				{
					object intermediate = conversion.Value.DynamicInvoke(new object[] { preConversion });
					if (intermediate != null)
					{
						object postConversion = this.ConvertOneHop(signature, intermediate);
						if (postConversion != null)
						{
							return postConversion;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00006A60 File Offset: 0x00004C60
		private object ConvertOneHop(Type signature, object app)
		{
			if (signature.IsInstanceOfType(app))
			{
				return app;
			}
			if (typeof(Delegate).IsAssignableFrom(signature))
			{
				Delegate memberDelegate = AppBuilder.ToMemberDelegate(signature, app);
				if (memberDelegate != null)
				{
					return memberDelegate;
				}
			}
			foreach (KeyValuePair<Tuple<Type, Type>, Delegate> conversion in this._conversions)
			{
				Type returnType = conversion.Key.Item1;
				Type parameterType = conversion.Key.Item2;
				if (parameterType.IsInstanceOfType(app) && signature.IsAssignableFrom(returnType))
				{
					return conversion.Value.DynamicInvoke(new object[] { app });
				}
			}
			return null;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00006B1C File Offset: 0x00004D1C
		private static Delegate ToMemberDelegate(Type signature, object app)
		{
			MethodInfo signatureMethod = signature.GetMethod("Invoke");
			ParameterInfo[] signatureParameters = signatureMethod.GetParameters();
			MethodInfo[] methods = app.GetType().GetMethods();
			foreach (MethodInfo method in methods)
			{
				if (!(method.Name != "Invoke"))
				{
					ParameterInfo[] methodParameters = method.GetParameters();
					if (methodParameters.Length == signatureParameters.Length)
					{
						if (!methodParameters.Zip(signatureParameters, (ParameterInfo methodParameter, ParameterInfo signatureParameter) => methodParameter.ParameterType.IsAssignableFrom(signatureParameter.ParameterType)).Any((bool compatible) => !compatible) && signatureMethod.ReturnType.IsAssignableFrom(method.ReturnType))
						{
							return Delegate.CreateDelegate(signature, app, method);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00006BFC File Offset: 0x00004DFC
		private static Tuple<Type, Delegate, object[]> ToMiddlewareFactory(object middlewareObject, object[] args)
		{
			if (middlewareObject == null)
			{
				throw new ArgumentNullException("middlewareObject");
			}
			Delegate middlewareDelegate = middlewareObject as Delegate;
			if (middlewareDelegate != null)
			{
				return Tuple.Create<Type, Delegate, object[]>(AppBuilder.GetParameterType(middlewareDelegate), middlewareDelegate, args);
			}
			Tuple<Type, Delegate, object[]> factory = AppBuilder.ToInstanceMiddlewareFactory(middlewareObject, args);
			if (factory != null)
			{
				return factory;
			}
			factory = AppBuilder.ToGeneratorMiddlewareFactory(middlewareObject, args);
			if (factory != null)
			{
				return factory;
			}
			if (middlewareObject is Type)
			{
				return AppBuilder.ToConstructorMiddlewareFactory(middlewareObject, args, ref middlewareDelegate);
			}
			throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_MiddlewareNotSupported, new object[] { middlewareObject.GetType().FullName }));
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00006C84 File Offset: 0x00004E84
		private static Tuple<Type, Delegate, object[]> ToInstanceMiddlewareFactory(object middlewareObject, object[] args)
		{
			MethodInfo[] methods = middlewareObject.GetType().GetMethods();
			MethodInfo[] array = methods;
			for (int i = 0; i < array.Length; i++)
			{
				MethodInfo method = array[i];
				if (!(method.Name != "Initialize"))
				{
					ParameterInfo[] parameters = method.GetParameters();
					Type[] parameterTypes = parameters.Select((ParameterInfo p) => p.ParameterType).ToArray<Type>();
					if (parameterTypes.Length == args.Length + 1)
					{
						if (parameterTypes.Skip(1).Zip(args, new Func<Type, object, bool>(AppBuilder.TestArgForParameter)).All((bool x) => x))
						{
							Func<object, object> func = delegate(object app)
							{
								object[] invokeParameters = new object[] { app }.Concat(args).ToArray<object>();
								method.Invoke(middlewareObject, invokeParameters);
								return middlewareObject;
							};
							return Tuple.Create<Type, Delegate, object[]>(parameters[0].ParameterType, func, new object[0]);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00006DC4 File Offset: 0x00004FC4
		private static Tuple<Type, Delegate, object[]> ToGeneratorMiddlewareFactory(object middlewareObject, object[] args)
		{
			MethodInfo[] methods = middlewareObject.GetType().GetMethods();
			foreach (MethodInfo method in methods)
			{
				if (!(method.Name != "Invoke"))
				{
					ParameterInfo[] parameters = method.GetParameters();
					Type[] parameterTypes = parameters.Select((ParameterInfo p) => p.ParameterType).ToArray<Type>();
					if (parameterTypes.Length == args.Length + 1)
					{
						if (parameterTypes.Skip(1).Zip(args, new Func<Type, object, bool>(AppBuilder.TestArgForParameter)).All((bool x) => x))
						{
							IEnumerable<Type> genericFuncTypes = parameterTypes.Concat(new Type[] { method.ReturnType });
							Type funcType = Expression.GetFuncType(genericFuncTypes.ToArray<Type>());
							Delegate middlewareDelegate = Delegate.CreateDelegate(funcType, middlewareObject, method);
							return Tuple.Create<Type, Delegate, object[]>(parameters[0].ParameterType, middlewareDelegate, args);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00006ED4 File Offset: 0x000050D4
		private static Tuple<Type, Delegate, object[]> ToConstructorMiddlewareFactory(object middlewareObject, object[] args, ref Delegate middlewareDelegate)
		{
			Type middlewareType = middlewareObject as Type;
			ConstructorInfo[] constructors = middlewareType.GetConstructors();
			foreach (ConstructorInfo constructor in constructors)
			{
				ParameterInfo[] parameters = constructor.GetParameters();
				Type[] parameterTypes = parameters.Select((ParameterInfo p) => p.ParameterType).ToArray<Type>();
				if (parameterTypes.Length == args.Length + 1)
				{
					if (parameterTypes.Skip(1).Zip(args, new Func<Type, object, bool>(AppBuilder.TestArgForParameter)).All((bool x) => x))
					{
						ParameterExpression[] parameterExpressions = parameters.Select((ParameterInfo p) => Expression.Parameter(p.ParameterType, p.Name)).ToArray<ParameterExpression>();
						ConstructorInfo constructorInfo = constructor;
						Expression[] array2 = parameterExpressions;
						NewExpression callConstructor = Expression.New(constructorInfo, array2);
						middlewareDelegate = Expression.Lambda(callConstructor, parameterExpressions).Compile();
						return Tuple.Create<Type, Delegate, object[]>(parameters[0].ParameterType, middlewareDelegate, args);
					}
				}
			}
			throw new MissingMethodException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_NoConstructorFound, new object[]
			{
				middlewareType.FullName,
				args.Length + 1
			}));
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000701C File Offset: 0x0000521C
		private static bool TestArgForParameter(Type parameterType, object arg)
		{
			return (arg == null && !parameterType.IsValueType) || parameterType.IsInstanceOfType(arg);
		}

		// Token: 0x04000075 RID: 117
		private static readonly Func<IDictionary<string, object>, Task> NotFound = new Func<IDictionary<string, object>, Task>(new NotFound().Invoke);

		// Token: 0x04000076 RID: 118
		private readonly IList<Tuple<Type, Delegate, object[]>> _middleware;

		// Token: 0x04000077 RID: 119
		private readonly IDictionary<Tuple<Type, Type>, Delegate> _conversions;

		// Token: 0x04000078 RID: 120
		private readonly IDictionary<string, object> _properties;
	}
}
