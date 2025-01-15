using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200003C RID: 60
	internal sealed class SqlConfigurableRetryLogicLoader
	{
		// Token: 0x06000750 RID: 1872 RVA: 0x0000F24D File Offset: 0x0000D44D
		private void AssignProviders(SqlRetryLogicBaseProvider cnnProvider = null, SqlRetryLogicBaseProvider cmdProvider = null)
		{
			this.ConnectionProvider = cnnProvider ?? SqlConfigurableRetryFactory.CreateNoneRetryProvider();
			this.CommandProvider = cmdProvider ?? SqlConfigurableRetryFactory.CreateNoneRetryProvider();
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x0000F26F File Offset: 0x0000D46F
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x0000F277 File Offset: 0x0000D477
		internal SqlRetryLogicBaseProvider ConnectionProvider { get; private set; }

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0000F280 File Offset: 0x0000D480
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x0000F288 File Offset: 0x0000D488
		internal SqlRetryLogicBaseProvider CommandProvider { get; private set; }

		// Token: 0x06000755 RID: 1877 RVA: 0x0000F291 File Offset: 0x0000D491
		public SqlConfigurableRetryLogicLoader(ISqlConfigurableRetryConnectionSection connectionRetryConfigs, ISqlConfigurableRetryCommandSection commandRetryConfigs, string cnnSectionName = "SqlConfigurableRetryLogicConnection", string cmdSectionName = "SqlConfigurableRetryLogicCommand")
		{
			this.AssignProviders((connectionRetryConfigs == null) ? null : SqlConfigurableRetryLogicLoader.CreateRetryLogicProvider(cnnSectionName, connectionRetryConfigs), (commandRetryConfigs == null) ? null : SqlConfigurableRetryLogicLoader.CreateRetryLogicProvider(cmdSectionName, commandRetryConfigs));
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0000F2BC File Offset: 0x0000D4BC
		private static SqlRetryLogicBaseProvider CreateRetryLogicProvider(string sectionName, ISqlConfigurableRetryConnectionSection configSection)
		{
			string name = MethodBase.GetCurrentMethod().Name;
			SqlClientEventSource.Log.TryTraceEvent<string, string>("<sc.{0}.{1}|INFO> Entry point.", "SqlConfigurableRetryLogicLoader", name);
			try
			{
				SqlRetryLogicOption sqlRetryLogicOption = new SqlRetryLogicOption
				{
					NumberOfTries = configSection.NumberOfTries,
					DeltaTime = configSection.DeltaTime,
					MinTimeInterval = configSection.MinTimeInterval,
					MaxTimeInterval = configSection.MaxTimeInterval
				};
				if (!string.IsNullOrEmpty(configSection.TransientErrors))
				{
					sqlRetryLogicOption.TransientErrors = (from x in configSection.TransientErrors.Split(new char[] { ',' })
						select Convert.ToInt32(x)).ToList<int>();
				}
				ISqlConfigurableRetryCommandSection cmdSection = configSection as ISqlConfigurableRetryCommandSection;
				if (cmdSection != null && !string.IsNullOrEmpty(cmdSection.AuthorizedSqlCondition))
				{
					sqlRetryLogicOption.AuthorizedSqlCondition = (string x) => Regex.IsMatch(x, cmdSection.AuthorizedSqlCondition);
				}
				SqlClientEventSource.Log.TryTraceEvent<string, string, string, string>("<sc.{0}.{1}|INFO> Successfully created a {2} object to use on creating a retry logic provider from the section '{3}'.", "SqlConfigurableRetryLogicLoader", name, "SqlRetryLogicOption", sectionName);
				SqlRetryLogicBaseProvider sqlRetryLogicBaseProvider = SqlConfigurableRetryLogicLoader.ResolveRetryLogicProvider(configSection.RetryLogicType, configSection.RetryMethod, sqlRetryLogicOption);
				SqlClientEventSource.Log.TryTraceEvent<string, string, string, string>("<sc.{0}.{1}|INFO> Successfully created a {2} object from the section '{3}'.", "SqlConfigurableRetryLogicLoader", name, "SqlRetryLogicBaseProvider", sectionName);
				return sqlRetryLogicBaseProvider;
			}
			catch (Exception ex)
			{
				SqlClientEventSource.Log.TryTraceEvent<string, string, Exception>("<sc.{0}.{1}|INFO> {2}", "SqlConfigurableRetryLogicLoader", name, ex);
			}
			SqlClientEventSource.Log.TryTraceEvent<string, string>("<sc.{0}.{1}|INFO> Due to an exception, the default non-retriable logic will be applied.", "SqlConfigurableRetryLogicLoader", name);
			return SqlConfigurableRetryFactory.CreateNoneRetryProvider();
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0000F454 File Offset: 0x0000D654
		private static SqlRetryLogicBaseProvider ResolveRetryLogicProvider(string configurableRetryType, string retryMethod, SqlRetryLogicOption option)
		{
			string name = MethodBase.GetCurrentMethod().Name;
			SqlClientEventSource.Log.TryTraceEvent<string, string>("<sc.{0}.{1}|INFO> Entry point.", "SqlConfigurableRetryLogicLoader", name);
			if (string.IsNullOrEmpty(retryMethod))
			{
				throw new ArgumentNullException("Failed to create SqlRetryLogicBaseProvider object because the retryMethod value is null or empty.");
			}
			Type type = null;
			try
			{
				type = SqlConfigurableRetryLogicLoader.LoadType(configurableRetryType);
			}
			catch (Exception ex)
			{
				type = typeof(SqlConfigurableRetryFactory);
				SqlClientEventSource.Log.TryTraceEvent<string, string, string, string, Exception>("<sc.{0}.{1}|INFO> Unable to load the '{2}' type; Trying to use the internal `{3}` type: {4}", "SqlConfigurableRetryLogicLoader", name, configurableRetryType, type.FullName, ex);
			}
			try
			{
				object obj = SqlConfigurableRetryLogicLoader.CreateInstance(type, retryMethod, option);
				SqlRetryLogicBaseProvider sqlRetryLogicBaseProvider = obj as SqlRetryLogicBaseProvider;
				if (sqlRetryLogicBaseProvider != null)
				{
					SqlClientEventSource.Log.TryTraceEvent<string, string, string>("<sc.{0}.{1}|INFO> The created instace is a {2} type.", "SqlConfigurableRetryLogicLoader", name, typeof(SqlRetryLogicBaseProvider).FullName);
					SqlRetryLogicBaseProvider sqlRetryLogicBaseProvider2 = sqlRetryLogicBaseProvider;
					sqlRetryLogicBaseProvider2.Retrying = (EventHandler<SqlRetryingEventArgs>)Delegate.Combine(sqlRetryLogicBaseProvider2.Retrying, new EventHandler<SqlRetryingEventArgs>(SqlConfigurableRetryLogicLoader.OnRetryingEvent));
					return sqlRetryLogicBaseProvider;
				}
			}
			catch (Exception ex2)
			{
				throw new Exception(string.Concat(new string[] { "Exception occurred when running the `", type.FullName, ".", retryMethod, "()` method." }), ex2);
			}
			SqlClientEventSource.Log.TryTraceEvent<string, string>("<sc.{0}.{1}|INFO> Unable to resolve a valid provider; Returns `null`.", "SqlConfigurableRetryLogicLoader", name);
			return null;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0000F5A0 File Offset: 0x0000D7A0
		private static object CreateInstance(Type type, string retryMethodName, SqlRetryLogicOption option)
		{
			string name = MethodBase.GetCurrentMethod().Name;
			SqlClientEventSource.Log.TryTraceEvent<string, string>("<sc.{0}.{1}|INFO> Entry point.", "SqlConfigurableRetryLogicLoader", name);
			if (type == typeof(SqlConfigurableRetryFactory) || type == null)
			{
				SqlClientEventSource.Log.TryTraceEvent<string, string, string, string>("<sc.{0}.{1}|INFO> The given type `{2}` infers as internal `{3}` type.", "SqlConfigurableRetryLogicLoader", name, (type != null) ? type.Name : null, typeof(SqlConfigurableRetryFactory).FullName);
				MethodInfo method = typeof(SqlConfigurableRetryFactory).GetMethod(retryMethodName);
				if (method == null)
				{
					throw new InvalidOperationException(string.Concat(new string[]
					{
						"Failed to resolve the '",
						retryMethodName,
						"' method from `",
						typeof(SqlConfigurableRetryFactory).FullName,
						"` type."
					}));
				}
				SqlClientEventSource.Log.TryTraceEvent<string, string, string, string, string>("<sc.{0}.{1}|INFO> The `{2}.{3}()` method has been discovered as the `{4}` method name.", "SqlConfigurableRetryLogicLoader", name, method.ReflectedType.FullName, method.Name, retryMethodName);
				object[] array = SqlConfigurableRetryLogicLoader.PrepareParamValues(method.GetParameters(), option, retryMethodName);
				SqlClientEventSource.Log.TryTraceEvent<string, string, string, string>("<sc.{0}.{1}|INFO> Parameters are prepared to invoke the `{2}.{3}()` method.", "SqlConfigurableRetryLogicLoader", name, method.ReflectedType.FullName, method.Name);
				return method.Invoke(null, array);
			}
			else
			{
				MethodInfo method2 = type.GetMethod(retryMethodName);
				if (method2 == null)
				{
					throw new InvalidOperationException(string.Concat(new string[] { "Failed to resolve the '", retryMethodName, "' method from `", type.FullName, "` type." }));
				}
				SqlClientEventSource.Log.TryTraceEvent<string, string, string, string, string>("<sc.{0}.{1}|INFO> The `{2}` method metadata has been extracted from the `{3}` type by using the `{4}` method name.", "SqlConfigurableRetryLogicLoader", name, method2.Name, type.FullName, retryMethodName);
				if (!typeof(SqlRetryLogicBaseProvider).IsAssignableFrom(method2.ReturnType))
				{
					throw new InvalidCastException("Invalid return type; Return type must be of `" + typeof(SqlRetryLogicBaseProvider).FullName + "` type.");
				}
				SqlClientEventSource.Log.TryTraceEvent<string, string, string, string>("<sc.{0}.{1}|INFO> The return type of the `{2}.{3}()` method is valid.", "SqlConfigurableRetryLogicLoader", name, type.FullName, method2.Name);
				object[] array2 = SqlConfigurableRetryLogicLoader.PrepareParamValues(method2.GetParameters(), option, retryMethodName);
				if (method2.IsStatic)
				{
					SqlClientEventSource.Log.TryTraceEvent<string, string, string, string>("<sc.{0}.{1}|INFO> Run the static `{2}` method without object creation of `{3}` type.", "SqlConfigurableRetryLogicLoader", name, method2.Name, type.FullName);
					return method2.Invoke(null, array2);
				}
				object obj = Activator.CreateInstance(type);
				SqlClientEventSource.Log.TryTraceEvent<string, string, string, string>("<sc.{0}.{1}|INFO> An instance of `{2}` type is created to invoke the `{3}` method.", "SqlConfigurableRetryLogicLoader", name, type.FullName, method2.Name);
				return method2.Invoke(obj, array2);
			}
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0000F818 File Offset: 0x0000DA18
		private static object[] PrepareParamValues(ParameterInfo[] parameterInfos, SqlRetryLogicOption option, string retryMethod)
		{
			if (parameterInfos.FirstOrDefault((ParameterInfo x) => x.ParameterType == typeof(SqlRetryLogicOption)) == null)
			{
				string text = string.Concat(new string[]
				{
					"Failed to create SqlRetryLogicBaseProvider object because of invalid ",
					retryMethod,
					"'s parameters.",
					Environment.NewLine,
					"The function must have a paramter of type 'SqlRetryLogicOption'."
				});
				throw new InvalidOperationException(text);
			}
			object[] array = new object[parameterInfos.Length];
			for (int i = 0; i < parameterInfos.Length; i++)
			{
				ParameterInfo paramInfo = parameterInfos[i];
				if (paramInfo.HasDefaultValue && paramInfo.ParameterType != typeof(SqlRetryLogicOption))
				{
					array[i] = paramInfo.DefaultValue;
				}
				else
				{
					if (!(paramInfo.ParameterType == typeof(SqlRetryLogicOption)))
					{
						string text2 = string.Concat(new string[]
						{
							"Failed to create SqlRetryLogicBaseProvider object because of invalid retryMethod's parameters.",
							Environment.NewLine,
							"Parameter '",
							paramInfo.ParameterType.Name,
							" ",
							paramInfo.Name,
							"' doesn't have default value."
						});
						throw new InvalidOperationException(text2);
					}
					if (!paramInfo.HasDefaultValue || (paramInfo.HasDefaultValue && parameterInfos.FirstOrDefault((ParameterInfo x) => x != paramInfo && !x.HasDefaultValue && x.ParameterType == typeof(SqlRetryLogicOption)) == null))
					{
						array[i] = option;
					}
					else
					{
						array[i] = paramInfo.DefaultValue;
					}
				}
			}
			SqlClientEventSource.Log.TryTraceEvent<string, string, string, string>("<sc.{0}.{1}|INFO> Parameters are prepared to invoke the `{2}.{3}()` method.", "SqlConfigurableRetryLogicLoader", MethodBase.GetCurrentMethod().Name, typeof(SqlConfigurableRetryFactory).FullName, retryMethod);
			return array;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0000F9D8 File Offset: 0x0000DBD8
		private static void OnRetryingEvent(object sender, SqlRetryingEventArgs args)
		{
			Exception ex = args.Exceptions[args.Exceptions.Count - 1];
			string text = string.Format("<sc.{0}.OnRetryingEvent|INFO>: Default configurable retry logic for {1} object. attempts count:{2}, upcoming delay:{3}", new object[]
			{
				"SqlConfigurableRetryLogicLoader",
				sender.GetType().Name,
				args.RetryCount,
				args.Delay
			});
			SqlClientEventSource.Log.TryTraceEvent<string, string>("{0}, Last exception:<{1}>", text, ex.Message);
			SqlClientEventSource.Log.TryAdvancedTraceEvent<string, Exception>("<ADV>{0}, Last exception: {1}", text, ex);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0000FA68 File Offset: 0x0000DC68
		private static Type LoadType(string fullyQualifiedName)
		{
			string name = MethodBase.GetCurrentMethod().Name;
			Type type = Type.GetType(fullyQualifiedName);
			SqlClientEventSource.Log.TryTraceEvent<string, string, string>("<sc.{0}.{1}|INFO> The '{2}' type is resolved.", "SqlConfigurableRetryLogicLoader", name, (type != null) ? type.FullName : null);
			if (!(type != null))
			{
				return typeof(SqlConfigurableRetryFactory);
			}
			return type;
		}

		// Token: 0x040000D1 RID: 209
		private const string TypeName = "SqlConfigurableRetryLogicLoader";
	}
}
