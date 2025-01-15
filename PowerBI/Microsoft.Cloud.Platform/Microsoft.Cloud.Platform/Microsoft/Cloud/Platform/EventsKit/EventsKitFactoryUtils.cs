using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Timers;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.ConfigurationClasses.EventKitFactory;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Eventing.Etw;
using Microsoft.Cloud.Platform.Utils;
using Microsoft.Diagnostics.Tracing;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000351 RID: 849
	internal static class EventsKitFactoryUtils
	{
		// Token: 0x0600192B RID: 6443 RVA: 0x0005D935 File Offset: 0x0005BB35
		internal static Assembly Generate<T>(EventsKitFactoryOptions options)
		{
			return EventsKitFactoryUtils.Generate(typeof(T), options, null);
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x0005D948 File Offset: 0x0005BB48
		internal static Assembly Generate(Type type, EventsKitFactoryOptions options, string outputAssembly = null)
		{
			EventsKitMetadata eventsKitMetadata = new EventsKitMetadata(type);
			IEnumerable<string> typeReferencedAssemblyLocationsFromAppDomain = CodeGenerationUtils.GetTypeReferencedAssemblyLocationsFromAppDomain(type);
			List<string> list = new List<string>();
			list.AddRange(typeReferencedAssemblyLocationsFromAppDomain);
			if (!string.Equals(new DirectoryInfo(Path.GetDirectoryName(type.Assembly.Location)).Name, "temp", StringComparison.OrdinalIgnoreCase))
			{
				list.Add(type.Assembly.Location);
			}
			string location = typeof(Timer).Assembly.Location;
			if (!list.Contains(location, StringComparer.OrdinalIgnoreCase))
			{
				list.Add(location);
			}
			string location2 = typeof(HashSet<string>).Assembly.Location;
			if (!list.Contains(location2, StringComparer.OrdinalIgnoreCase))
			{
				list.Add(location2);
			}
			if (options.HasFlag(EventsKitFactoryOptions.EmitEtwEvents))
			{
				string location3 = typeof(EventSource).Assembly.Location;
				if (!list.Contains(location3, StringComparer.OrdinalIgnoreCase))
				{
					list.Add(location3);
				}
			}
			return new ExtendedCSharpCodeProvider(new EventsKitCodeGenerator(options, eventsKitMetadata).CreateGeneratedEventsKitCode(), list.ToArray(), "").BuildAssembly(ExtendedCSharpCodeProviderBuildOptions.None, outputAssembly);
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x0005DA62 File Offset: 0x0005BC62
		internal static string FormatEventSourceClassName(string eventsKitClassName)
		{
			return CodeGenerationUtils.ConvertToValidCSharpClassName("{0}EventSource".FormatWithInvariantCulture(new object[] { eventsKitClassName }));
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x0005DA80 File Offset: 0x0005BC80
		public static GeneratedEventsKitImplementationTypes GetImplementationTypes<T>(EventsKitFactoryOptions options, Assembly assembly, EventsKitType type)
		{
			Type typeFromHandle = typeof(T);
			Type type2 = null;
			Type type3 = null;
			if (assembly == null)
			{
				type2 = EventsKitFactoryUtils.GetEventsKitImplementationType(typeFromHandle.Assembly, typeFromHandle, type);
				assembly = EventsKitFactoryUtils.Generate<T>(options);
			}
			if (options.HasFlag(EventsKitFactoryOptions.EmitEtwEvents))
			{
				type3 = EventsKitFactoryUtils.GetEventSourceImplementationType(assembly, typeFromHandle);
				if (type3 == null)
				{
					throw new EventsKitCreationFailedException(typeFromHandle.Name, assembly.GetName().Name, "Cannot locate event source in the hosting assembly");
				}
			}
			if (type2 == null)
			{
				type2 = EventsKitFactoryUtils.GetEventsKitImplementationType(assembly, typeFromHandle, type);
			}
			if (type2 == null)
			{
				throw new EventsKitCreationFailedException(typeFromHandle.Name, typeFromHandle.Assembly.GetName().Name, "Cannot locate the events kit in the hosting assembly");
			}
			return new GeneratedEventsKitImplementationTypes(type3, type2);
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x0005DB40 File Offset: 0x0005BD40
		public static GeneratedEventsKitInstances<T> CreateGeneratedEventsKitInstances<T>(EventsKitFactoryOptions options, ElementId element, ActivityType activityType, IPackageManager packageMgr, string pcInstanceName, EventsKitType type, IEventingServer eventingServer, IEtwSessionsManager etwSessionsManager, EventKitFactoryConfiguration eventKitFactoryConfiguration)
		{
			Type typeFromHandle = typeof(T);
			Assembly assembly = EventsKitFactoryUtils.LoadPreBuiltEventsKit(options, typeFromHandle);
			GeneratedEventsKitImplementationTypes implementationTypes = EventsKitFactoryUtils.GetImplementationTypes<T>(options, assembly, type);
			EventSource eventSource = null;
			if (implementationTypes.EventSourceType != null)
			{
				eventSource = EventsKitFactoryUtils.CreateEventSourceInstance(implementationTypes.EventSourceType, etwSessionsManager);
			}
			return new GeneratedEventsKitInstances<T>(EventsKitFactoryUtils.CreateEventsKitInstance<T>(element, activityType, packageMgr, pcInstanceName, implementationTypes.EventsKitType, eventingServer, eventKitFactoryConfiguration, eventSource), eventSource);
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x0005DBA8 File Offset: 0x0005BDA8
		private static Assembly LoadPreBuiltEventsKit(EventsKitFactoryOptions options, Type interfaceType)
		{
			if (options == EventsKitFactoryOptions.All)
			{
				string location = interfaceType.Assembly.Location;
				string text = Path.Combine(Path.GetDirectoryName(location), Path.GetFileNameWithoutExtension(location) + ".EventsKit.dll");
				if (File.Exists(text))
				{
					return DynamicLoader.LoadAssemblyFrom(text, LoadOptions.Explicit);
				}
				text = Path.Combine(Path.GetDirectoryName(location), EventsKitFactoryUtils.GetEventsKitsImplementationLibraryHashedName(interfaceType));
				if (File.Exists(text))
				{
					return DynamicLoader.LoadAssemblyFrom(text, LoadOptions.Explicit);
				}
			}
			return null;
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x0005DC15 File Offset: 0x0005BE15
		internal static string GetEventsKitsImplementationLibraryHashedName(Type type)
		{
			return "EK.{0}.{1}.dll".FormatWithInvariantCulture(new object[]
			{
				type.Name,
				Obfuscation.ShortObfuscate(type.FullName, true)
			});
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x0005DC40 File Offset: 0x0005BE40
		public static T CreateEventsKitInstance<T>(Assembly assembly, ElementId element, ActivityType activityType, IPackageManager packageMgr, string pcInstanceName, EventsKitType type, IEventingServer eventingServer, EventKitFactoryConfiguration eventKitFactoryConfiguration, EventSource eventSource)
		{
			Type eventsKitImplementationType = EventsKitFactoryUtils.GetEventsKitImplementationType(assembly, typeof(T), type);
			return EventsKitFactoryUtils.CreateEventsKitInstance<T>(element, activityType, packageMgr, pcInstanceName, eventsKitImplementationType, eventingServer, eventKitFactoryConfiguration, eventSource);
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x0005DC74 File Offset: 0x0005BE74
		public static T CreateEventsKitInstance<T>(ElementId element, ActivityType activityType, IPackageManager packageMgr, string pcInstanceName, Type implType, IEventingServer eventingServer, EventKitFactoryConfiguration eventKitFactoryConfiguration, EventSource eventSourceInstance)
		{
			object[] array = new object[]
			{
				element,
				activityType,
				packageMgr,
				pcInstanceName,
				eventingServer,
				(eventKitFactoryConfiguration == null) ? null : eventKitFactoryConfiguration.EventLogSourceName
			};
			if (eventSourceInstance != null)
			{
				array = array.Concat(new EventSource[] { eventSourceInstance }).ToArray<object>();
			}
			return (T)((object)Activator.CreateInstance(implType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, array, null));
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x0005DCDD File Offset: 0x0005BEDD
		private static EventSource CreateEventSourceInstance(Type implType, IEtwSessionsManager etwSessionsManager)
		{
			return (EventSource)Activator.CreateInstance(implType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new object[] { etwSessionsManager }, null);
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x0005DCFC File Offset: 0x0005BEFC
		private static Type GetEventsKitImplementationType(Assembly assembly, Type interfaceType, EventsKitType type)
		{
			return (from t in DynamicLoader.GetTypes(assembly)
				from a in t.GetCustomAttributes(typeof(EventsKitImplementationAttribute), false)
				let attr = (EventsKitImplementationAttribute)a
				where attr.EventsKit.Equals(interfaceType) && attr.EventsKitType == type
				select t).FirstOrDefault<Type>();
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x0005DDC4 File Offset: 0x0005BFC4
		internal static Type GetEventSourceImplementationType(Assembly assembly, Type interfaceType)
		{
			return (from t in DynamicLoader.GetTypes(assembly)
				where t.Name.Equals(EventsKitFactoryUtils.FormatEventSourceClassName(EventsKitMetadata.GetImplementationClassName(interfaceType.Name, false)), StringComparison.Ordinal)
				select t).FirstOrDefault<Type>();
		}
	}
}
