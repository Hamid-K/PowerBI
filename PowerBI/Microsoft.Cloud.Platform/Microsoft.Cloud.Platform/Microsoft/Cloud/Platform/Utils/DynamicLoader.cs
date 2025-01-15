using System;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001ED RID: 493
	public static class DynamicLoader
	{
		// Token: 0x06000CCF RID: 3279 RVA: 0x0002CC64 File Offset: 0x0002AE64
		public static Type[] GetTypes(Assembly assembly)
		{
			Type[] types;
			try
			{
				types = assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				Exception[] loaderExceptions = ex.LoaderExceptions;
				if (loaderExceptions == null || loaderExceptions.Length == 0)
				{
					throw;
				}
				string text = string.Format(CultureInfo.CurrentCulture, string.Concat(new string[]
				{
					"Unable to load one of more of the requested types.",
					Environment.NewLine,
					"  First loader exception is: '{0}'.",
					Environment.NewLine,
					"  Inner exception is: '{1}'."
				}), new object[]
				{
					loaderExceptions[0].ToString(),
					ex.ToString()
				});
				throw new ReflectionTypeLoadException(ex.Types, loaderExceptions, text);
			}
			return types;
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0002CD04 File Offset: 0x0002AF04
		public static Type Load([NotNull] string sourceAssembly, [NotNull] string type, [NotNull] Predicate<Type> checker, LoadOptions options)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(sourceAssembly, "sourceAssembly");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(type, "type");
			ExtendedDiagnostics.EnsureArgumentNotNull<Predicate<Type>>(checker, "checker");
			Assembly assembly = DynamicLoader.LoadAssembly(sourceAssembly, options);
			Type type3;
			try
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Getting type {0} from assembly", new object[] { type });
				Type type2 = assembly.GetType(type);
				if (type2 == null)
				{
					type2 = FreeFunctions.FindIf<Type>(assembly.GetTypes(), (Type x) => x.Name == type);
				}
				if (type2 == null)
				{
					throw new LoadableObjectNotFoundException(sourceAssembly, type);
				}
				if (!checker(type2))
				{
					throw new LoadableObjectNotFoundException(type);
				}
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Returning type {0}", new object[] { type2 });
				type3 = type2;
			}
			catch (ReflectionTypeLoadException ex)
			{
				throw new LoadableObjectNotFoundException(sourceAssembly, type, ex);
			}
			return type3;
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0002CE08 File Offset: 0x0002B008
		public static Assembly LoadAssembly([NotNull] string assembly, LoadOptions options)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(assembly, "assembly");
			Assembly a = null;
			DynamicLoader.HandleAssemblyLoadExceptions(assembly, delegate
			{
				try
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Loading assembly {0}", new object[] { assembly });
					a = Assembly.Load(assembly);
				}
				catch (FileNotFoundException)
				{
					if ((options & LoadOptions.Explicit) == LoadOptions.None)
					{
						throw;
					}
					string text = Path.Combine(CurrentProcess.MainModuleDirectory, assembly);
					a = Assembly.LoadFrom(text);
				}
			});
			return a;
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0002CE60 File Offset: 0x0002B060
		public static Assembly LoadAssemblyFrom([NotNull] string path, LoadOptions options)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(path, "path");
			Assembly a = null;
			DynamicLoader.HandleAssemblyLoadExceptions(path, delegate
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Loading assembly from {0}", new object[] { path });
				a = Assembly.LoadFrom(path);
			});
			return a;
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0002CEAE File Offset: 0x0002B0AE
		public static object Instantiate(string sourceAssembly, string type, Predicate<Type> checker, LoadOptions options, params object[] args)
		{
			return DynamicLoader.Instantiate(DynamicLoader.Load(sourceAssembly, type, checker, options), checker, args);
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0002CEC4 File Offset: 0x0002B0C4
		public static object Instantiate([NotNull] Type type, [NotNull] Predicate<Type> checker, params object[] args)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Type>(type, "type");
			ExtendedDiagnostics.EnsureArgumentNotNull<Predicate<Type>>(checker, "checker");
			object obj2;
			try
			{
				if (!checker(type))
				{
					throw new LoadableObjectNotFoundException(type);
				}
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Instantiating type {0}", new object[] { type });
				object obj = Activator.CreateInstance(type, args);
				if (obj == null)
				{
					throw new LoadableObjectNotInstantiatedException(type);
				}
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Instantiated object {0}", new object[] { obj });
				obj2 = obj;
			}
			catch (TargetInvocationException ex)
			{
				throw new LoadableObjectNotInstantiatedException(type, ex);
			}
			catch (ReflectionTypeLoadException ex2)
			{
				throw new LoadableObjectNotInstantiatedException(type, ex2);
			}
			catch (MissingMethodException ex3)
			{
				throw new LoadableObjectNotInstantiatedException(type, ex3);
			}
			return obj2;
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0002CF8C File Offset: 0x0002B18C
		public static bool ImplementsInterface<T>(Type type)
		{
			return type.GetInterface(typeof(T).Name) != null;
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0002CFA9 File Offset: 0x0002B1A9
		public static bool IsPod([NotNull] Type type)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Type>(type, "type");
			return type.IsLayoutSequential && type.IsValueType;
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x000034FD File Offset: 0x000016FD
		public static bool IsValidType(Type type)
		{
			return true;
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0002CFC6 File Offset: 0x0002B1C6
		public static T FastCreateInstance<T>() where T : new()
		{
			return DynamicLoader.GetCompiledConstructor<T>()();
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0002CFD2 File Offset: 0x0002B1D2
		public static object FastCreateInstance(Type type)
		{
			return DynamicLoader.GetCompiledConstructor(type)();
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0002CFDF File Offset: 0x0002B1DF
		public static Func<T> GetCompiledConstructor<T>() where T : new()
		{
			return Expression.Lambda<Func<T>>(Expression.New(typeof(T).GetConstructor(new Type[0])), new ParameterExpression[0]).Compile();
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0002D00B File Offset: 0x0002B20B
		public static Func<object> GetCompiledConstructor(Type type)
		{
			return Expression.Lambda<Func<object>>(Expression.New(type.GetConstructor(new Type[0])), new ParameterExpression[0]).Compile();
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0002D030 File Offset: 0x0002B230
		private static void HandleAssemblyLoadExceptions(string assembly, Action loadingAction)
		{
			try
			{
				loadingAction();
			}
			catch (BadImageFormatException ex)
			{
				throw new LoadableObjectNotFoundException(assembly, ex);
			}
			catch (FileLoadException ex2)
			{
				throw new LoadableObjectNotFoundException(assembly, ex2);
			}
			catch (FileNotFoundException ex3)
			{
				throw new LoadableObjectNotFoundException(assembly, ex3);
			}
		}
	}
}
