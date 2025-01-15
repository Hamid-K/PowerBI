using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using SharedResourceNamespace;

namespace Owin.Loader
{
	// Token: 0x02000003 RID: 3
	internal class DefaultLoader
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public DefaultLoader()
			: this(null, null, null)
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205B File Offset: 0x0000025B
		public DefaultLoader(IEnumerable<Assembly> referencedAssemblies)
			: this(null, null, referencedAssemblies)
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002066 File Offset: 0x00000266
		public DefaultLoader(Func<string, IList<string>, Action<IAppBuilder>> next)
			: this(next, null, null)
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002071 File Offset: 0x00000271
		public DefaultLoader(Func<string, IList<string>, Action<IAppBuilder>> next, Func<Type, object> activator)
			: this(next, activator, null)
		{
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000207C File Offset: 0x0000027C
		public DefaultLoader(Func<string, IList<string>, Action<IAppBuilder>> next, Func<Type, object> activator, IEnumerable<Assembly> referencedAssemblies)
		{
			this._next = next ?? NullLoader.Instance;
			this._activator = activator ?? new Func<Type, object>(Activator.CreateInstance);
			this._referencedAssemblies = referencedAssemblies ?? new DefaultLoader.AssemblyDirScanner();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020BB File Offset: 0x000002BB
		public Action<IAppBuilder> Load(string startupName, IList<string> errorDetails)
		{
			return this.LoadImplementation(startupName, errorDetails) ?? this._next(startupName, errorDetails);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020D8 File Offset: 0x000002D8
		private Action<IAppBuilder> LoadImplementation(string startupName, IList<string> errorDetails)
		{
			Tuple<Type, string> typeAndMethod = null;
			startupName = startupName ?? string.Empty;
			if (!startupName.Contains(','))
			{
				typeAndMethod = this.GetDefaultConfiguration(startupName, errorDetails);
			}
			if (typeAndMethod == null && !string.IsNullOrWhiteSpace(startupName))
			{
				typeAndMethod = this.GetTypeAndMethodNameForConfigurationString(startupName, errorDetails);
			}
			if (typeAndMethod == null)
			{
				return null;
			}
			Type type = typeAndMethod.Item1;
			string methodName = ((!string.IsNullOrWhiteSpace(typeAndMethod.Item2)) ? typeAndMethod.Item2 : "Configuration");
			Action<IAppBuilder> startup = this.MakeDelegate(type, methodName, errorDetails);
			if (startup == null)
			{
				return null;
			}
			return delegate(IAppBuilder builder)
			{
				if (builder == null)
				{
					throw new ArgumentNullException("builder");
				}
				object value;
				if (!builder.Properties.TryGetValue("host.AppName", out value) || string.IsNullOrWhiteSpace(Convert.ToString(value, CultureInfo.InvariantCulture)))
				{
					builder.Properties["host.AppName"] = type.FullName;
				}
				startup(builder);
			};
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002178 File Offset: 0x00000378
		private Tuple<Type, string> GetTypeAndMethodNameForConfigurationString(string configuration, IList<string> errors)
		{
			Tuple<string, Assembly> typePair = this.HuntForAssembly(configuration, errors);
			if (typePair == null)
			{
				return null;
			}
			string longestPossibleName = typePair.Item1;
			Assembly assembly = typePair.Item2;
			foreach (string typeName in DefaultLoader.DotByDot(longestPossibleName).Take(2))
			{
				Type type = assembly.GetType(typeName, false);
				if (!(type == null))
				{
					string methodName = ((typeName == longestPossibleName) ? null : longestPossibleName.Substring(typeName.Length + 1));
					return new Tuple<Type, string>(type, methodName);
				}
				errors.Add(string.Format(CultureInfo.CurrentCulture, LoaderResources.ClassNotFoundInAssembly, new object[] { configuration, typeName, assembly.FullName }));
			}
			return null;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002254 File Offset: 0x00000454
		private Tuple<Type, string> GetDefaultConfiguration(string friendlyName, IList<string> errors)
		{
			friendlyName = friendlyName ?? string.Empty;
			bool conflict = false;
			Tuple<Type, string> result = this.SearchForStartupAttribute(friendlyName, errors, ref conflict);
			if (result == null && !conflict && string.IsNullOrEmpty(friendlyName))
			{
				result = this.SearchForStartupConvention(errors);
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002294 File Offset: 0x00000494
		private Tuple<Type, string> SearchForStartupAttribute(string friendlyName, IList<string> errors, ref bool conflict)
		{
			friendlyName = friendlyName ?? string.Empty;
			bool foundAnyInstances = false;
			Tuple<Type, string> fullMatch = null;
			Assembly matchedAssembly = null;
			using (IEnumerator<Assembly> enumerator = this._referencedAssemblies.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Assembly assembly = enumerator.Current;
					Attribute[] attributes;
					try
					{
						attributes = (from data in assembly.GetCustomAttributesData()
							where DefaultLoader.MatchesStartupAttribute(data.AttributeType)
							select data.AttributeType).SelectMany((Type type) => assembly.GetCustomAttributes(type)).Distinct<Attribute>().ToArray<Attribute>();
					}
					catch (CustomAttributeFormatException)
					{
						continue;
					}
					foreach (Attribute owinStartupAttribute in attributes.Where((Attribute attribute) => DefaultLoader.MatchesStartupAttribute(attribute.GetType())))
					{
						Type attributeType = owinStartupAttribute.GetType();
						foundAnyInstances = true;
						PropertyInfo startupTypeProperty = attributeType.GetProperty("StartupType", typeof(Type));
						if (startupTypeProperty == null)
						{
							errors.Add(string.Format(CultureInfo.CurrentCulture, LoaderResources.StartupTypePropertyMissing, new object[] { attributeType.AssemblyQualifiedName, assembly.FullName }));
						}
						else
						{
							Type startupType = startupTypeProperty.GetValue(owinStartupAttribute, null) as Type;
							if (startupType == null)
							{
								errors.Add(string.Format(CultureInfo.CurrentCulture, LoaderResources.StartupTypePropertyEmpty, new object[] { assembly.FullName }));
							}
							else
							{
								string friendlyNameValue = string.Empty;
								PropertyInfo friendlyNameProperty = attributeType.GetProperty("FriendlyName", typeof(string));
								if (friendlyNameProperty != null)
								{
									friendlyNameValue = (friendlyNameProperty.GetValue(owinStartupAttribute, null) as string) ?? string.Empty;
								}
								if (!string.Equals(friendlyName, friendlyNameValue, StringComparison.OrdinalIgnoreCase))
								{
									errors.Add(string.Format(CultureInfo.CurrentCulture, LoaderResources.FriendlyNameMismatch, new object[] { friendlyNameValue, friendlyName, assembly.FullName }));
								}
								else
								{
									string methodName = string.Empty;
									PropertyInfo methodNameProperty = attributeType.GetProperty("MethodName", typeof(string));
									if (methodNameProperty != null)
									{
										methodName = (methodNameProperty.GetValue(owinStartupAttribute, null) as string) ?? string.Empty;
									}
									if (fullMatch != null)
									{
										conflict = true;
										errors.Add(string.Format(CultureInfo.CurrentCulture, LoaderResources.Exception_AttributeNameConflict, new object[]
										{
											matchedAssembly.GetName().Name,
											fullMatch.Item1,
											assembly.GetName().Name,
											startupType,
											friendlyName
										}));
									}
									else
									{
										fullMatch = new Tuple<Type, string>(startupType, methodName);
										matchedAssembly = assembly;
									}
								}
							}
						}
					}
				}
			}
			if (!foundAnyInstances)
			{
				errors.Add(LoaderResources.NoOwinStartupAttribute);
			}
			if (conflict)
			{
				return null;
			}
			return fullMatch;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002604 File Offset: 0x00000804
		private static bool MatchesStartupAttribute(Type type)
		{
			return type.Name.Equals("OwinStartupAttribute", StringComparison.Ordinal);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002618 File Offset: 0x00000818
		private Tuple<Type, string> SearchForStartupConvention(IList<string> errors)
		{
			Type matchedType = null;
			bool conflict = false;
			foreach (Assembly assembly in this._referencedAssemblies)
			{
				DefaultLoader.CheckForStartupType("Startup", assembly, ref matchedType, ref conflict, errors);
				DefaultLoader.CheckForStartupType(assembly.GetName().Name + ".Startup", assembly, ref matchedType, ref conflict, errors);
			}
			if (matchedType == null)
			{
				errors.Add(LoaderResources.NoAssemblyWithStartupClass);
				return null;
			}
			if (conflict)
			{
				return null;
			}
			if (!matchedType.GetMethods().Any((MethodInfo methodInfo) => methodInfo.Name.Equals("Configuration")))
			{
				errors.Add(string.Format(CultureInfo.CurrentCulture, LoaderResources.MethodNotFoundInClass, new object[] { "Configuration", matchedType.AssemblyQualifiedName }));
				return null;
			}
			return new Tuple<Type, string>(matchedType, "Configuration");
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002714 File Offset: 0x00000914
		private static void CheckForStartupType(string startupName, Assembly assembly, ref Type matchedType, ref bool conflict, IList<string> errors)
		{
			Type startupType = assembly.GetType(startupName, false);
			if (startupType != null)
			{
				if (matchedType != null)
				{
					conflict = true;
					errors.Add(string.Format(CultureInfo.CurrentCulture, LoaderResources.Exception_StartupTypeConflict, new object[] { matchedType.AssemblyQualifiedName, startupType.AssemblyQualifiedName }));
					return;
				}
				matchedType = startupType;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002774 File Offset: 0x00000974
		private Tuple<string, Assembly> HuntForAssembly(string configuration, IList<string> errors)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			int commaIndex = configuration.IndexOf(',');
			if (commaIndex < 0)
			{
				foreach (Assembly assembly in this._referencedAssemblies)
				{
					foreach (string typeName in DefaultLoader.DotByDot(configuration).Take(2))
					{
						if (assembly.GetType(typeName, false) != null)
						{
							return Tuple.Create<string, Assembly>(configuration, assembly);
						}
					}
				}
				errors.Add(string.Format(CultureInfo.CurrentCulture, LoaderResources.TypeOrMethodNotFound, new object[] { configuration }));
				return null;
			}
			string methodOrTypeName = DefaultLoader.DotByDot(configuration.Substring(0, commaIndex)).FirstOrDefault<string>();
			string assemblyName = configuration.Substring(commaIndex + 1).Trim();
			Assembly assembly2 = DefaultLoader.TryAssemblyLoad(assemblyName);
			if (assembly2 == null)
			{
				errors.Add(string.Format(CultureInfo.CurrentCulture, LoaderResources.AssemblyNotFound, new object[] { configuration, assemblyName }));
				return null;
			}
			return Tuple.Create<string, Assembly>(methodOrTypeName, assembly2);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000028BC File Offset: 0x00000ABC
		private static Assembly TryAssemblyLoad(string assemblyName)
		{
			Assembly assembly;
			try
			{
				assembly = Assembly.Load(assemblyName);
			}
			catch (FileNotFoundException)
			{
				assembly = null;
			}
			catch (FileLoadException)
			{
				assembly = null;
			}
			return assembly;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000028FC File Offset: 0x00000AFC
		public static IEnumerable<string> DotByDot(string text)
		{
			if (text == null)
			{
				yield break;
			}
			text = text.Trim(new char[] { '.' });
			for (int length = text.Length; length > 0; length = text.LastIndexOf('.', length - 1, length - 1))
			{
				yield return text.Substring(0, length);
			}
			yield break;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000290C File Offset: 0x00000B0C
		private Action<IAppBuilder> MakeDelegate(Type type, string methodName, IList<string> errors)
		{
			MethodInfo partialMatch = null;
			MethodInfo[] methods = type.GetMethods();
			for (int i = 0; i < methods.Length; i++)
			{
				MethodInfo methodInfo = methods[i];
				if (methodInfo.Name.Equals(methodName))
				{
					if (DefaultLoader.Matches(methodInfo, false, new Type[] { typeof(IAppBuilder) }))
					{
						object instance3 = (methodInfo.IsStatic ? null : this._activator(type));
						return delegate(IAppBuilder builder)
						{
							MethodBase methodInfo2 = methodInfo;
							object instance2 = instance3;
							object[] array = new IAppBuilder[] { builder };
							methodInfo2.Invoke(instance2, array);
						};
					}
					if (DefaultLoader.Matches(methodInfo, true, new Type[] { typeof(IDictionary<string, object>) }))
					{
						object instance4 = (methodInfo.IsStatic ? null : this._activator(type));
						return delegate(IAppBuilder builder)
						{
							builder.Use(new Func<object, object>((object _) => methodInfo.Invoke(instance4, new object[] { builder.Properties })), new object[0]);
						};
					}
					if (DefaultLoader.Matches(methodInfo, true, new Type[0]))
					{
						object instance = (methodInfo.IsStatic ? null : this._activator(type));
						Func<object, object> <>9__4;
						return delegate(IAppBuilder builder)
						{
							Func<object, object> func;
							if ((func = <>9__4) == null)
							{
								func = (<>9__4 = (object _) => methodInfo.Invoke(instance, new object[0]));
							}
							builder.Use(func, new object[0]);
						};
					}
					partialMatch = partialMatch ?? methodInfo;
				}
			}
			if (partialMatch == null)
			{
				errors.Add(string.Format(CultureInfo.CurrentCulture, LoaderResources.MethodNotFoundInClass, new object[] { methodName, type.AssemblyQualifiedName }));
			}
			else
			{
				errors.Add(string.Format(CultureInfo.CurrentCulture, LoaderResources.UnexpectedMethodSignature, new object[] { methodName, type.AssemblyQualifiedName }));
			}
			return null;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002AEC File Offset: 0x00000CEC
		private static bool Matches(MethodInfo methodInfo, bool hasReturnValue, params Type[] parameterTypes)
		{
			bool methodHadReturnValue = methodInfo.ReturnType != typeof(void);
			if (hasReturnValue != methodHadReturnValue)
			{
				return false;
			}
			ParameterInfo[] parameters = methodInfo.GetParameters();
			if (parameters.Length != parameterTypes.Length)
			{
				return false;
			}
			return parameters.Zip(parameterTypes, (ParameterInfo pi, Type t) => pi.ParameterType == t).All((bool b) => b);
		}

		// Token: 0x04000008 RID: 8
		private readonly Func<string, IList<string>, Action<IAppBuilder>> _next;

		// Token: 0x04000009 RID: 9
		private readonly Func<Type, object> _activator;

		// Token: 0x0400000A RID: 10
		private readonly IEnumerable<Assembly> _referencedAssemblies;

		// Token: 0x0200002F RID: 47
		private class AssemblyDirScanner : IEnumerable<Assembly>, IEnumerable
		{
			// Token: 0x060000DD RID: 221 RVA: 0x00004C44 File Offset: 0x00002E44
			public IEnumerator<Assembly> GetEnumerator()
			{
				AppDomainSetup info = AppDomain.CurrentDomain.SetupInformation;
				IEnumerable<string> searchPaths = new string[0];
				if (info.PrivateBinPathProbe == null || string.IsNullOrWhiteSpace(info.PrivateBinPath))
				{
					searchPaths = searchPaths.Concat(new string[] { string.Empty });
				}
				if (!string.IsNullOrWhiteSpace(info.PrivateBinPath))
				{
					searchPaths = searchPaths.Concat(info.PrivateBinPath.Split(new char[] { ';' }));
				}
				foreach (string searchPath in searchPaths)
				{
					string assembliesPath = Path.Combine(info.ApplicationBase, searchPath);
					if (Directory.Exists(assembliesPath))
					{
						IEnumerable<string> files = Directory.GetFiles(assembliesPath, "*.dll").Concat(Directory.GetFiles(assembliesPath, "*.exe"));
						foreach (string file in files)
						{
							Assembly assembly = null;
							try
							{
								assembly = Assembly.Load(AssemblyName.GetAssemblyName(file));
							}
							catch (BadImageFormatException)
							{
								continue;
							}
							yield return assembly;
						}
						IEnumerator<string> enumerator2 = null;
					}
				}
				IEnumerator<string> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x060000DE RID: 222 RVA: 0x00004C4C File Offset: 0x00002E4C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}
		}
	}
}
