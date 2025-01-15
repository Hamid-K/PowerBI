using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using NLog.Common;
using NLog.Conditions;
using NLog.Filters;
using NLog.Internal;
using NLog.LayoutRenderers;
using NLog.Layouts;
using NLog.MessageTemplates;
using NLog.Targets;
using NLog.Time;

namespace NLog.Config
{
	// Token: 0x02000180 RID: 384
	public class ConfigurationItemFactory
	{
		// Token: 0x14000024 RID: 36
		// (add) Token: 0x0600118A RID: 4490 RVA: 0x0002D648 File Offset: 0x0002B848
		// (remove) Token: 0x0600118B RID: 4491 RVA: 0x0002D67C File Offset: 0x0002B87C
		public static event EventHandler<AssemblyLoadingEventArgs> AssemblyLoading;

		// Token: 0x0600118C RID: 4492 RVA: 0x0002D6B0 File Offset: 0x0002B8B0
		public ConfigurationItemFactory(params Assembly[] assemblies)
		{
			this.CreateInstance = new ConfigurationItemCreator(FactoryHelper.CreateInstance);
			this._targets = new Factory<Target, TargetAttribute>(this);
			this._filters = new Factory<Filter, FilterAttribute>(this);
			this._layoutRenderers = new LayoutRendererFactory(this);
			this._layouts = new Factory<Layout, LayoutAttribute>(this);
			this._conditionMethods = new MethodFactory<ConditionMethodsAttribute, ConditionMethodAttribute>();
			this._ambientProperties = new Factory<LayoutRenderer, AmbientPropertyAttribute>(this);
			this._timeSources = new Factory<TimeSource, TimeSourceAttribute>(this);
			this._allFactories = new List<object> { this._targets, this._filters, this._layoutRenderers, this._layouts, this._conditionMethods, this._ambientProperties, this._timeSources };
			foreach (Assembly assembly in assemblies)
			{
				this.RegisterItemsFromAssembly(assembly);
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x0002D7B8 File Offset: 0x0002B9B8
		// (set) Token: 0x0600118E RID: 4494 RVA: 0x0002D7CE File Offset: 0x0002B9CE
		public static ConfigurationItemFactory Default
		{
			get
			{
				ConfigurationItemFactory configurationItemFactory;
				if ((configurationItemFactory = ConfigurationItemFactory._defaultInstance) == null)
				{
					configurationItemFactory = (ConfigurationItemFactory._defaultInstance = ConfigurationItemFactory.BuildDefaultFactory());
				}
				return configurationItemFactory;
			}
			set
			{
				ConfigurationItemFactory._defaultInstance = value;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x0002D7D6 File Offset: 0x0002B9D6
		// (set) Token: 0x06001190 RID: 4496 RVA: 0x0002D7DE File Offset: 0x0002B9DE
		public ConfigurationItemCreator CreateInstance { get; set; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x0002D7E7 File Offset: 0x0002B9E7
		public INamedItemFactory<Target, Type> Targets
		{
			get
			{
				return this._targets;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x0002D7EF File Offset: 0x0002B9EF
		public INamedItemFactory<Filter, Type> Filters
		{
			get
			{
				return this._filters;
			}
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0002D7F7 File Offset: 0x0002B9F7
		internal LayoutRendererFactory GetLayoutRenderers()
		{
			return this._layoutRenderers;
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x0002D7FF File Offset: 0x0002B9FF
		public INamedItemFactory<LayoutRenderer, Type> LayoutRenderers
		{
			get
			{
				return this._layoutRenderers;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x0002D807 File Offset: 0x0002BA07
		public INamedItemFactory<Layout, Type> Layouts
		{
			get
			{
				return this._layouts;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x0002D80F File Offset: 0x0002BA0F
		public INamedItemFactory<LayoutRenderer, Type> AmbientProperties
		{
			get
			{
				return this._ambientProperties;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x0002D817 File Offset: 0x0002BA17
		// (set) Token: 0x06001198 RID: 4504 RVA: 0x0002D824 File Offset: 0x0002BA24
		[Obsolete("Use JsonConverter property instead. Marked obsolete on NLog 4.5")]
		public IJsonSerializer JsonSerializer
		{
			get
			{
				return this._jsonSerializer as IJsonSerializer;
			}
			set
			{
				IJsonConverter jsonConverter2;
				if (value == null)
				{
					IJsonConverter jsonConverter = DefaultJsonSerializer.Instance;
					jsonConverter2 = jsonConverter;
				}
				else
				{
					IJsonConverter jsonConverter = new JsonConverterLegacy(value);
					jsonConverter2 = jsonConverter;
				}
				this._jsonSerializer = jsonConverter2;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x0002D84B File Offset: 0x0002BA4B
		// (set) Token: 0x0600119A RID: 4506 RVA: 0x0002D853 File Offset: 0x0002BA53
		public IJsonConverter JsonConverter
		{
			get
			{
				return this._jsonSerializer;
			}
			set
			{
				this._jsonSerializer = value ?? DefaultJsonSerializer.Instance;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x0002D865 File Offset: 0x0002BA65
		// (set) Token: 0x0600119C RID: 4508 RVA: 0x0002D86C File Offset: 0x0002BA6C
		public IValueFormatter ValueFormatter
		{
			get
			{
				return NLog.MessageTemplates.ValueFormatter.Instance;
			}
			set
			{
				NLog.MessageTemplates.ValueFormatter.Instance = value;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x0002D874 File Offset: 0x0002BA74
		// (set) Token: 0x0600119E RID: 4510 RVA: 0x0002D87C File Offset: 0x0002BA7C
		public IPropertyTypeConverter PropertyTypeConverter { get; set; } = new PropertyTypeConverter();

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x0002D888 File Offset: 0x0002BA88
		// (set) Token: 0x060011A0 RID: 4512 RVA: 0x0002D8C9 File Offset: 0x0002BAC9
		public bool? ParseMessageTemplates
		{
			get
			{
				if (LogEventInfo.DefaultMessageFormatter == LogEventInfo.StringFormatMessageFormatter)
				{
					return new bool?(false);
				}
				if (LogEventInfo.DefaultMessageFormatter == LogMessageTemplateFormatter.Default.MessageFormatter)
				{
					return new bool?(true);
				}
				return null;
			}
			set
			{
				LogEventInfo.SetDefaultMessageFormatter(value);
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x0002D8D1 File Offset: 0x0002BAD1
		public INamedItemFactory<TimeSource, Type> TimeSources
		{
			get
			{
				return this._timeSources;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x0002D8D9 File Offset: 0x0002BAD9
		public INamedItemFactory<MethodInfo, MethodInfo> ConditionMethods
		{
			get
			{
				return this._conditionMethods;
			}
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0002D8E1 File Offset: 0x0002BAE1
		public void RegisterItemsFromAssembly(Assembly assembly)
		{
			this.RegisterItemsFromAssembly(assembly, string.Empty);
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0002D8F0 File Offset: 0x0002BAF0
		public void RegisterItemsFromAssembly(Assembly assembly, string itemNamePrefix)
		{
			if (ConfigurationItemFactory.AssemblyLoading != null)
			{
				AssemblyLoadingEventArgs assemblyLoadingEventArgs = new AssemblyLoadingEventArgs(assembly);
				ConfigurationItemFactory.AssemblyLoading(null, assemblyLoadingEventArgs);
				if (assemblyLoadingEventArgs.Cancel)
				{
					InternalLogger.Info<string>("Loading assembly '{0}' is canceled", assembly.FullName);
					return;
				}
			}
			InternalLogger.Debug<string>("ScanAssembly('{0}')", assembly.FullName);
			Type[] array = assembly.SafeGetTypes();
			this.PreloadAssembly(array);
			foreach (object obj in this._allFactories)
			{
				((IFactory)obj).ScanTypes(array, itemNamePrefix);
			}
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0002D994 File Offset: 0x0002BB94
		public void PreloadAssembly(Type[] typesToScan)
		{
			foreach (Type type in typesToScan.Where((Type t) => t.Name.Equals("NLogPackageLoader", StringComparison.OrdinalIgnoreCase)))
			{
				this.CallPreload(type);
			}
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0002DA00 File Offset: 0x0002BC00
		private void CallPreload(Type type)
		{
			if (type == null)
			{
				return;
			}
			InternalLogger.Debug<string>("Found for preload'{0}'", type.FullName);
			MethodInfo method = type.GetMethod("Preload");
			if (method != null)
			{
				if (method.IsStatic)
				{
					InternalLogger.Debug("NLogPackageLoader contains Preload method");
					try
					{
						object[] array = ConfigurationItemFactory.CreatePreloadParameters(method, this);
						method.Invoke(null, array);
						InternalLogger.Debug<string>("Preload successfully invoked for '{0}'", type.FullName);
						return;
					}
					catch (Exception ex)
					{
						InternalLogger.Warn(ex, "Invoking Preload for '{0}' failed", new object[] { type.FullName });
						return;
					}
				}
				InternalLogger.Debug("NLogPackageLoader contains a preload method, but isn't static");
				return;
			}
			InternalLogger.Debug<string>("{0} doesn't contain Preload method", type.FullName);
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0002DAB8 File Offset: 0x0002BCB8
		private static object[] CreatePreloadParameters(MethodInfo preloadMethod, ConfigurationItemFactory configurationItemFactory)
		{
			ParameterInfo parameterInfo = preloadMethod.GetParameters().FirstOrDefault<ParameterInfo>();
			object[] array = null;
			if (((parameterInfo != null) ? parameterInfo.ParameterType : null) == typeof(ConfigurationItemFactory))
			{
				array = new object[] { configurationItemFactory };
			}
			return array;
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0002DAFC File Offset: 0x0002BCFC
		public void Clear()
		{
			foreach (object obj in this._allFactories)
			{
				((IFactory)obj).Clear();
			}
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0002DB4C File Offset: 0x0002BD4C
		public void RegisterType(Type type, string itemNamePrefix)
		{
			foreach (object obj in this._allFactories)
			{
				((IFactory)obj).RegisterType(type, itemNamePrefix);
			}
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0002DBA0 File Offset: 0x0002BDA0
		private static ConfigurationItemFactory BuildDefaultFactory()
		{
			Assembly assembly = typeof(ILogger).GetAssembly();
			ConfigurationItemFactory configurationItemFactory = new ConfigurationItemFactory(new Assembly[] { assembly });
			configurationItemFactory.RegisterExtendedItems();
			try
			{
				string text = string.Empty;
				string[] array = ArrayHelper.Empty<string>();
				foreach (KeyValuePair<string, Assembly> keyValuePair in ConfigurationItemFactory.GetAutoLoadingFileLocations())
				{
					if (!string.IsNullOrEmpty(keyValuePair.Key))
					{
						if (string.IsNullOrEmpty(text))
						{
							text = keyValuePair.Key;
						}
						array = ConfigurationItemFactory.GetNLogExtensionFiles(keyValuePair.Key);
						if (array.Length != 0)
						{
							text = keyValuePair.Key;
							break;
						}
					}
				}
				InternalLogger.Debug<string>("Start auto loading, location: {0}", text);
				ConfigurationItemFactory.LoadNLogExtensionAssemblies(configurationItemFactory, assembly, array);
			}
			catch (SecurityException ex)
			{
				InternalLogger.Warn(ex, "Seems that we do not have permission");
				if (ex.MustBeRethrown())
				{
					throw;
				}
			}
			catch (UnauthorizedAccessException ex2)
			{
				InternalLogger.Warn(ex2, "Seems that we do not have permission");
				if (ex2.MustBeRethrown())
				{
					throw;
				}
			}
			InternalLogger.Debug("Auto loading done");
			return configurationItemFactory;
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0002DCBC File Offset: 0x0002BEBC
		private static void LoadNLogExtensionAssemblies(ConfigurationItemFactory factory, Assembly nlogAssembly, string[] extensionDlls)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { nlogAssembly.FullName };
			foreach (string text in extensionDlls)
			{
				InternalLogger.Info<string>("Auto loading assembly file: {0}", text);
				bool flag = false;
				try
				{
					Assembly assembly = AssemblyHelpers.LoadFromPath(text, null);
					InternalLogger.LogAssemblyVersion(assembly);
					factory.RegisterItemsFromAssembly(assembly);
					hashSet.Add(assembly.FullName);
					flag = true;
				}
				catch (Exception ex)
				{
					if (ex.MustBeRethrownImmediately())
					{
						throw;
					}
					InternalLogger.Warn(ex, "Auto loading assembly file: {0} failed! Skipping this file.", new object[] { text });
				}
				if (flag)
				{
					InternalLogger.Info<string>("Auto loading assembly file: {0} succeeded!", text);
				}
			}
			foreach (Assembly assembly2 in LogFactory.CurrentAppDomain.GetAssemblies())
			{
				if (assembly2.FullName.StartsWith("NLog.", StringComparison.OrdinalIgnoreCase) && !hashSet.Contains(assembly2.FullName))
				{
					factory.RegisterItemsFromAssembly(assembly2);
				}
				if (assembly2.FullName.StartsWith("NLog.Extensions.Logging,", StringComparison.OrdinalIgnoreCase) || assembly2.FullName.StartsWith("NLog.Web,", StringComparison.OrdinalIgnoreCase) || assembly2.FullName.StartsWith("NLog.Web.AspNetCore,", StringComparison.OrdinalIgnoreCase) || assembly2.FullName.StartsWith("Microsoft.Extensions.Logging,", StringComparison.OrdinalIgnoreCase) || assembly2.FullName.StartsWith("Microsoft.Extensions.Logging.Abstractions,", StringComparison.OrdinalIgnoreCase) || assembly2.FullName.StartsWith("Microsoft.Extensions.Logging.Filter,", StringComparison.OrdinalIgnoreCase) || assembly2.FullName.StartsWith("Microsoft.Logging,", StringComparison.OrdinalIgnoreCase))
				{
					LogManager.AddHiddenAssembly(assembly2);
				}
			}
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0002DE70 File Offset: 0x0002C070
		internal static IEnumerable<KeyValuePair<string, Assembly>> GetAutoLoadingFileLocations()
		{
			Assembly assembly = typeof(ILogger).GetAssembly();
			string assemblyLocation = PathHelpers.TrimDirectorySeparators(AssemblyHelpers.GetAssemblyFileLocation(assembly));
			if (!string.IsNullOrEmpty(assemblyLocation))
			{
				yield return new KeyValuePair<string, Assembly>(assemblyLocation, assembly);
			}
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			string text = PathHelpers.TrimDirectorySeparators(AssemblyHelpers.GetAssemblyFileLocation(Assembly.GetEntryAssembly()));
			if (!string.IsNullOrEmpty(text) && !string.Equals(text, assemblyLocation, StringComparison.OrdinalIgnoreCase))
			{
				yield return new KeyValuePair<string, Assembly>(text, entryAssembly);
			}
			string text2 = PathHelpers.TrimDirectorySeparators(LogFactory.CurrentAppDomain.BaseDirectory);
			InternalLogger.Debug<string>("Auto loading based on AppDomain-BaseDirectory found location: {0}", text2);
			if (!string.IsNullOrEmpty(text2) && !string.Equals(text2, assemblyLocation, StringComparison.OrdinalIgnoreCase))
			{
				yield return new KeyValuePair<string, Assembly>(text2, null);
			}
			yield break;
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x0002DE7C File Offset: 0x0002C07C
		private static string[] GetNLogExtensionFiles(string assemblyLocation)
		{
			string[] array;
			try
			{
				InternalLogger.Debug<string>("Search for auto loading files in location: {0}", assemblyLocation);
				if (string.IsNullOrEmpty(assemblyLocation))
				{
					array = ArrayHelper.Empty<string>();
				}
				else
				{
					array = (from x in Directory.GetFiles(assemblyLocation, "NLog*.dll").Select(new Func<string, string>(Path.GetFileName))
						where !x.Equals("NLog.dll", StringComparison.OrdinalIgnoreCase)
						where !x.Equals("NLog.UnitTests.dll", StringComparison.OrdinalIgnoreCase)
						where !x.Equals("NLog.Extended.dll", StringComparison.OrdinalIgnoreCase)
						select Path.Combine(assemblyLocation, x)).ToArray<string>();
				}
			}
			catch (DirectoryNotFoundException ex)
			{
				InternalLogger.Warn(ex, "Skipping auto loading location because assembly directory does not exist: {0}", new object[] { assemblyLocation });
				if (ex.MustBeRethrown())
				{
					throw;
				}
				array = ArrayHelper.Empty<string>();
			}
			catch (SecurityException ex2)
			{
				InternalLogger.Warn(ex2, "Skipping auto loading location because access not allowed to assembly directory: {0}", new object[] { assemblyLocation });
				if (ex2.MustBeRethrown())
				{
					throw;
				}
				array = ArrayHelper.Empty<string>();
			}
			catch (UnauthorizedAccessException ex3)
			{
				InternalLogger.Warn(ex3, "Skipping auto loading location because access not allowed to assembly directory: {0}", new object[] { assemblyLocation });
				if (ex3.MustBeRethrown())
				{
					throw;
				}
				array = ArrayHelper.Empty<string>();
			}
			return array;
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0002E01C File Offset: 0x0002C21C
		private void RegisterExtendedItems()
		{
			string assemblyQualifiedName = typeof(ILogger).AssemblyQualifiedName;
			string text = "NLog,";
			int? num = ((assemblyQualifiedName != null) ? new int?(assemblyQualifiedName.IndexOf(text, StringComparison.OrdinalIgnoreCase)) : null);
			int? num2 = num;
			int num3 = 0;
			if ((num2.GetValueOrDefault() >= num3) & (num2 != null))
			{
				string text2 = ", NLog.Extended," + assemblyQualifiedName.Substring(num.Value + text.Length);
				string @namespace = typeof(DebugTarget).Namespace;
				this._targets.RegisterNamedType("MSMQ", @namespace + ".MessageQueueTarget" + text2);
			}
			this._layoutRenderers.RegisterNamedType("configsetting", "NLog.Extensions.Logging.ConfigSettingLayoutRenderer, NLog.Extensions.Logging");
		}

		// Token: 0x040004C0 RID: 1216
		private static ConfigurationItemFactory _defaultInstance;

		// Token: 0x040004C1 RID: 1217
		private readonly IList<object> _allFactories;

		// Token: 0x040004C2 RID: 1218
		private readonly Factory<Target, TargetAttribute> _targets;

		// Token: 0x040004C3 RID: 1219
		private readonly Factory<Filter, FilterAttribute> _filters;

		// Token: 0x040004C4 RID: 1220
		private readonly LayoutRendererFactory _layoutRenderers;

		// Token: 0x040004C5 RID: 1221
		private readonly Factory<Layout, LayoutAttribute> _layouts;

		// Token: 0x040004C6 RID: 1222
		private readonly MethodFactory<ConditionMethodsAttribute, ConditionMethodAttribute> _conditionMethods;

		// Token: 0x040004C7 RID: 1223
		private readonly Factory<LayoutRenderer, AmbientPropertyAttribute> _ambientProperties;

		// Token: 0x040004C8 RID: 1224
		private readonly Factory<TimeSource, TimeSourceAttribute> _timeSources;

		// Token: 0x040004C9 RID: 1225
		private IJsonConverter _jsonSerializer = DefaultJsonSerializer.Instance;
	}
}
