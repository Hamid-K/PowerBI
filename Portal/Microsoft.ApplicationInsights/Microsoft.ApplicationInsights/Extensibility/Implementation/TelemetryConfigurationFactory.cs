using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using Microsoft.ApplicationInsights.Extensibility.Implementation.Platform;
using Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000081 RID: 129
	internal class TelemetryConfigurationFactory
	{
		// Token: 0x0600041C RID: 1052 RVA: 0x000122C2 File Offset: 0x000104C2
		protected TelemetryConfigurationFactory()
		{
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x000122CA File Offset: 0x000104CA
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x000122E0 File Offset: 0x000104E0
		public static TelemetryConfigurationFactory Instance
		{
			get
			{
				TelemetryConfigurationFactory telemetryConfigurationFactory;
				if ((telemetryConfigurationFactory = TelemetryConfigurationFactory.instance) == null)
				{
					telemetryConfigurationFactory = (TelemetryConfigurationFactory.instance = new TelemetryConfigurationFactory());
				}
				return telemetryConfigurationFactory;
			}
			set
			{
				TelemetryConfigurationFactory.instance = value;
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x000122E8 File Offset: 0x000104E8
		public virtual void Initialize(TelemetryConfiguration configuration, TelemetryModules modules, string serializedConfiguration)
		{
			try
			{
				SdkInternalOperationsMonitor.Enter();
				if (modules != null)
				{
					if (!modules.Modules.Any((ITelemetryModule module) => module is DiagnosticsTelemetryModule))
					{
						modules.Modules.Add(new DiagnosticsTelemetryModule());
					}
				}
				configuration.TelemetryInitializers.Add(new OperationCorrelationTelemetryInitializer());
				if (!string.IsNullOrEmpty(serializedConfiguration))
				{
					try
					{
						XDocument xdocument = XDocument.Parse(serializedConfiguration);
						TelemetryConfigurationFactory.LoadFromXml(configuration, modules, xdocument);
					}
					catch (XmlException ex)
					{
						CoreEventSource.Log.ConfigurationFileCouldNotBeParsedError(ex.Message, "Incorrect");
					}
				}
				string environmentVariable = PlatformSingleton.Current.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY");
				if (!string.IsNullOrEmpty(environmentVariable))
				{
					configuration.InstrumentationKey = environmentVariable;
				}
				if (configuration.TelemetryProcessors == null)
				{
					configuration.TelemetryProcessorChainBuilder.Build();
				}
				foreach (TelemetrySink telemetrySink in configuration.TelemetrySinks)
				{
					telemetrySink.Initialize(configuration);
					if (telemetrySink.TelemetryProcessorChain == null)
					{
						telemetrySink.TelemetryProcessorChainBuilder.Build();
					}
				}
				TelemetryConfigurationFactory.InitializeComponents(configuration, modules);
			}
			finally
			{
				SdkInternalOperationsMonitor.Exit();
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00012450 File Offset: 0x00010650
		public virtual void Initialize(TelemetryConfiguration configuration, TelemetryModules modules)
		{
			this.Initialize(configuration, modules, PlatformSingleton.Current.ReadConfigurationXml());
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00012464 File Offset: 0x00010664
		protected static object CreateInstance(Type interfaceType, string typeName, object[] constructorArgs = null)
		{
			Type type = TelemetryConfigurationFactory.GetType(typeName);
			if (type == null)
			{
				CoreEventSource.Log.TypeWasNotFoundConfigurationError(typeName, "Incorrect");
				return null;
			}
			object obj;
			try
			{
				obj = ((constructorArgs != null) ? Activator.CreateInstance(type, constructorArgs) : Activator.CreateInstance(type));
			}
			catch (Exception ex)
			{
				CoreEventSource.Log.MissingMethodExceptionConfigurationError(typeName, ex.Message, "Incorrect");
				return null;
			}
			if (!interfaceType.IsAssignableFrom(obj.GetType()))
			{
				CoreEventSource.Log.IncorrectTypeConfigurationError(type.AssemblyQualifiedName, interfaceType.FullName, "Incorrect");
				return null;
			}
			return obj;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00012504 File Offset: 0x00010704
		protected static void LoadFromXml(TelemetryConfiguration configuration, TelemetryModules modules, XDocument xml)
		{
			TelemetryConfigurationFactory.LoadInstance(xml.Element(TelemetryConfigurationFactory.XmlNamespace + "ApplicationInsights"), typeof(TelemetryConfiguration), configuration, null, modules);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00012530 File Offset: 0x00010730
		protected static object LoadInstance(XElement definition, Type expectedType, object instance, object[] constructorArgs, TelemetryModules modules)
		{
			if (definition != null)
			{
				XAttribute xattribute = definition.Attribute("Type");
				if (xattribute != null)
				{
					if (instance == null || instance.GetType() != TelemetryConfigurationFactory.GetType(xattribute.Value))
					{
						instance = TelemetryConfigurationFactory.CreateInstance(expectedType, xattribute.Value, constructorArgs);
					}
				}
				else if (!definition.Elements().Any<XElement>() && !definition.Attributes().Any<XAttribute>() && constructorArgs == null)
				{
					TelemetryConfigurationFactory.LoadInstanceFromValue(definition, expectedType, ref instance);
				}
				else if (instance == null && !expectedType.IsAbstract())
				{
					instance = ((constructorArgs != null) ? Activator.CreateInstance(expectedType, constructorArgs) : Activator.CreateInstance(expectedType));
				}
				else if (instance == null)
				{
					CoreEventSource.Log.IncorrectInstanceAtributesConfigurationError(definition.Name.LocalName, "Incorrect");
				}
				if (instance != null)
				{
					TelemetryConfigurationFactory.LoadProperties(definition, instance, modules);
					Type type;
					if (TelemetryConfigurationFactory.GetCollectionElementType(instance.GetType(), out type))
					{
						TelemetryConfigurationFactory.LoadInstancesDefinition.MakeGenericMethod(new Type[] { type }).Invoke(null, new object[] { definition, instance, modules });
					}
				}
			}
			return instance;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00012634 File Offset: 0x00010834
		protected static void BuildTelemetryProcessorChain(XElement definition, TelemetryProcessorChainBuilder builder)
		{
			if (definition != null)
			{
				using (IEnumerator<XElement> enumerator = definition.Elements(TelemetryConfigurationFactory.XmlNamespace + "Add").GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						XElement addElement = enumerator.Current;
						builder = builder.Use(delegate(ITelemetryProcessor current)
						{
							object[] array = new object[] { current };
							return (ITelemetryProcessor)TelemetryConfigurationFactory.LoadInstance(addElement, typeof(ITelemetryProcessor), null, array, null);
						});
					}
				}
			}
			builder.Build();
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000126B4 File Offset: 0x000108B4
		protected static void LoadTelemetrySinks(XElement definition, TelemetryConfiguration telemetryConfiguration)
		{
			foreach (XElement xelement in definition.Elements(TelemetryConfigurationFactory.XmlNamespace + "Add"))
			{
				XAttribute nameAttribute = xelement.Attribute("Name");
				XAttribute nameAttribute2 = nameAttribute;
				if (!string.IsNullOrWhiteSpace((nameAttribute2 != null) ? nameAttribute2.Value : null))
				{
					if (TelemetrySink.DefaultSinkName.Equals(nameAttribute.Value, StringComparison.OrdinalIgnoreCase))
					{
						TelemetryConfigurationFactory.LoadProperties(xelement, telemetryConfiguration.DefaultTelemetrySink, null);
						continue;
					}
					TelemetrySink telemetrySink = telemetryConfiguration.TelemetrySinks.FirstOrDefault((TelemetrySink s) => string.Equals(s.Name, nameAttribute.Value, StringComparison.OrdinalIgnoreCase));
					if (telemetrySink != null)
					{
						TelemetryConfigurationFactory.LoadProperties(xelement, telemetrySink, null);
						continue;
					}
				}
				XElement xelement2 = xelement;
				Type typeFromHandle = typeof(TelemetrySink);
				object obj = null;
				object[] array = new object[2];
				array[0] = telemetryConfiguration;
				TelemetrySink telemetrySink2 = TelemetryConfigurationFactory.LoadInstance(xelement2, typeFromHandle, obj, array, null) as TelemetrySink;
				if (telemetrySink2 != null)
				{
					telemetryConfiguration.TelemetrySinks.Add(telemetrySink2);
				}
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000127C4 File Offset: 0x000109C4
		protected static void LoadInstances<T>(XElement definition, ICollection<T> instances, TelemetryModules modules)
		{
			if (definition != null)
			{
				foreach (XElement xelement in definition.Elements(TelemetryConfigurationFactory.XmlNamespace + "Add"))
				{
					object obj = null;
					XAttribute xattribute = xelement.Attribute("Type");
					if (xattribute != null)
					{
						Type type = TelemetryConfigurationFactory.GetType(xattribute.Value);
						obj = instances.FirstOrDefault((T i) => i.GetType() == type);
					}
					if (obj == null)
					{
						obj = TelemetryConfigurationFactory.LoadInstance(xelement, typeof(T), obj, null, modules);
						if (obj != null)
						{
							instances.Add((T)((object)obj));
						}
					}
					else
					{
						TelemetryConfigurationFactory.LoadProperties(xelement, obj, null);
					}
				}
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001289C File Offset: 0x00010A9C
		protected static void LoadProperties(XElement instanceDefinition, object instance, TelemetryModules modules)
		{
			List<XElement> list = TelemetryConfigurationFactory.GetPropertyDefinitions(instanceDefinition).ToList<XElement>();
			if (list.Count > 0)
			{
				Type type = instance.GetType();
				Dictionary<string, PropertyInfo> dictionary = type.GetProperties().ToDictionary((PropertyInfo p) => p.Name);
				foreach (XElement xelement in list)
				{
					string localName = xelement.Name.LocalName;
					PropertyInfo propertyInfo;
					if (dictionary.TryGetValue(localName, out propertyInfo))
					{
						if (localName == "TelemetryProcessors")
						{
							TelemetryConfiguration telemetryConfiguration = instance as TelemetryConfiguration;
							TelemetryProcessorChainBuilder telemetryProcessorChainBuilder;
							if ((telemetryProcessorChainBuilder = ((telemetryConfiguration != null) ? telemetryConfiguration.TelemetryProcessorChainBuilder : null)) == null)
							{
								TelemetrySink telemetrySink = instance as TelemetrySink;
								telemetryProcessorChainBuilder = ((telemetrySink != null) ? telemetrySink.TelemetryProcessorChainBuilder : null);
							}
							TelemetryProcessorChainBuilder telemetryProcessorChainBuilder2 = telemetryProcessorChainBuilder;
							if (telemetryProcessorChainBuilder2 != null)
							{
								TelemetryConfigurationFactory.BuildTelemetryProcessorChain(xelement, telemetryProcessorChainBuilder2);
							}
						}
						else if (localName == "TelemetrySinks")
						{
							TelemetryConfigurationFactory.LoadTelemetrySinks(xelement, (TelemetryConfiguration)instance);
						}
						else
						{
							object obj = propertyInfo.GetValue(instance, null);
							obj = TelemetryConfigurationFactory.LoadInstance(xelement, propertyInfo.PropertyType, obj, null, modules);
							if (obj != null && propertyInfo.CanWrite)
							{
								propertyInfo.SetValue(instance, obj, null);
							}
						}
					}
					else if (modules != null && localName == "TelemetryModules")
					{
						TelemetryConfigurationFactory.LoadInstance(xelement, modules.Modules.GetType(), modules.Modules, null, modules);
					}
					else if (!(instance is TelemetryConfiguration))
					{
						CoreEventSource.Log.IncorrectPropertyConfigurationError(type.AssemblyQualifiedName, localName, "Incorrect");
					}
				}
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00012A50 File Offset: 0x00010C50
		private static void InitializeComponents(TelemetryConfiguration configuration, TelemetryModules modules)
		{
			TelemetryConfigurationFactory.InitializeComponent(configuration.TelemetryChannel, configuration);
			TelemetryConfigurationFactory.InitializeComponents(configuration.TelemetryInitializers, configuration);
			TelemetryConfigurationFactory.InitializeComponents(configuration.TelemetryProcessorChain.TelemetryProcessors, configuration);
			if (modules != null)
			{
				TelemetryConfigurationFactory.InitializeComponents(modules.Modules, configuration);
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00012A8C File Offset: 0x00010C8C
		private static void InitializeComponents(IEnumerable components, TelemetryConfiguration configuration)
		{
			foreach (object obj in components)
			{
				TelemetryConfigurationFactory.InitializeComponent(obj, configuration);
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00012ADC File Offset: 0x00010CDC
		private static void InitializeComponent(object component, TelemetryConfiguration configuration)
		{
			ITelemetryModule telemetryModule = component as ITelemetryModule;
			if (telemetryModule != null)
			{
				try
				{
					telemetryModule.Initialize(configuration);
				}
				catch (Exception ex)
				{
					CoreEventSource.Log.ComponentInitializationConfigurationError(component.ToString(), ex.ToInvariantString(), "Incorrect");
				}
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00012B2C File Offset: 0x00010D2C
		private static void LoadInstanceFromValue(XElement definition, Type expectedType, ref object instance)
		{
			if (string.IsNullOrEmpty(definition.Value))
			{
				instance = (typeof(ValueType).IsAssignableFrom(expectedType) ? Activator.CreateInstance(expectedType) : null);
				return;
			}
			try
			{
				string text = definition.Value.Trim();
				expectedType = Nullable.GetUnderlyingType(expectedType) ?? expectedType;
				if (text == "null")
				{
					instance = null;
				}
				else if (expectedType == typeof(TimeSpan))
				{
					instance = TimeSpan.Parse(text, CultureInfo.InvariantCulture);
				}
				else if (expectedType.IsEnum)
				{
					instance = Enum.Parse(expectedType, text);
				}
				else if (text.IndexOf("0x", StringComparison.OrdinalIgnoreCase) == 0)
				{
					CultureInfo invariantCulture = CultureInfo.InvariantCulture;
					instance = int.Parse(text.Remove(0, 2), NumberStyles.AllowHexSpecifier, invariantCulture);
				}
				else
				{
					instance = Convert.ChangeType(text, expectedType, CultureInfo.InvariantCulture);
				}
			}
			catch (InvalidCastException ex)
			{
				CoreEventSource.Log.LoadInstanceFromValueConfigurationError(definition.Name.LocalName, definition.Value, ex.Message, "Incorrect");
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00012C44 File Offset: 0x00010E44
		private static Type GetType(string typeName)
		{
			return TelemetryConfigurationFactory.GetManagedType(typeName);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00012C4C File Offset: 0x00010E4C
		private static Type GetManagedType(string typeName)
		{
			Type type;
			try
			{
				type = Type.GetType(typeName);
			}
			catch (IOException)
			{
				type = null;
			}
			return type;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00012C78 File Offset: 0x00010E78
		private static bool GetCollectionElementType(Type type, out Type elementType)
		{
			Type type2 = type.GetInterfaces().FirstOrDefault((Type i) => i.IsGenericType() && i.GetGenericTypeDefinition() == typeof(ICollection<>));
			elementType = ((type2 != null) ? type2.GetGenericArguments()[0] : null);
			return elementType != null;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00012CD0 File Offset: 0x00010ED0
		private static IEnumerable<XElement> GetPropertyDefinitions(XElement instanceDefinition)
		{
			IEnumerable<XElement> enumerable = from a in instanceDefinition.Attributes()
				where !a.IsNamespaceDeclaration && a.Name.LocalName != "Type"
				select new XElement(a.Name, a.Value);
			IEnumerable<XElement> enumerable2 = from e in instanceDefinition.Elements()
				where e.Name.LocalName != "Add"
				select e;
			return enumerable.Concat(enumerable2);
		}

		// Token: 0x0400019E RID: 414
		private const string AddElementName = "Add";

		// Token: 0x0400019F RID: 415
		private const string TypeAttributeName = "Type";

		// Token: 0x040001A0 RID: 416
		private const string NameAttributeName = "Name";

		// Token: 0x040001A1 RID: 417
		private const string InstrumentationKeyWebSitesEnvironmentVariable = "APPINSIGHTS_INSTRUMENTATIONKEY";

		// Token: 0x040001A2 RID: 418
		private static readonly MethodInfo LoadInstancesDefinition = typeof(TelemetryConfigurationFactory).GetRuntimeMethods().First((MethodInfo m) => m.Name == "LoadInstances");

		// Token: 0x040001A3 RID: 419
		private static readonly XNamespace XmlNamespace = "http://schemas.microsoft.com/ApplicationInsights/2013/Settings";

		// Token: 0x040001A4 RID: 420
		private static TelemetryConfigurationFactory instance;
	}
}
