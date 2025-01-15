using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x0200034A RID: 842
	public sealed class EventsKitExplorer : IEventsKitExplorer
	{
		// Token: 0x06001900 RID: 6400 RVA: 0x0005C8FC File Offset: 0x0005AAFC
		internal EventsKitExplorer([NotNull] string path, bool recursive, [NotNull] Predicate<string> predicate)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(path, "path");
			ExtendedDiagnostics.EnsureArgumentNotNull<Predicate<string>>(predicate, "predicate");
			this.m_loadedAssemblies = new HashSet<Assembly>(from a in AppDomain.CurrentDomain.GetAssemblies()
				where !a.IsDynamic
				select a);
			this.m_eventKits = new Dictionary<long, EventsKitMetadata>();
			this.m_eventsKitMetadataCache = new List<IEventsKitMetadata>();
			this.m_eventsCache = new List<IEventMetadata>();
			this.m_eventsByIdCache = new Dictionary<long, IEventMetadata>();
			this.m_graph = new Dictionary<string, EventsKitExplorer.AssemblyNode>();
			foreach (string text in AssemblyWalker.GetAssemblyFileNames(path, recursive, predicate))
			{
				this.LoadEventKitsFromFile(text);
			}
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x0005C9D8 File Offset: 0x0005ABD8
		internal EventsKitExplorer([NotNull] IEnumerable<string> paths, bool recursive, [NotNull] Predicate<string> predicate)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<string>>(paths, "paths");
			ExtendedDiagnostics.EnsureArgumentNotNull<Predicate<string>>(predicate, "predicate");
			this.m_loadedAssemblies = new HashSet<Assembly>(from a in AppDomain.CurrentDomain.GetAssemblies()
				where !a.IsDynamic
				select a);
			this.m_eventKits = new Dictionary<long, EventsKitMetadata>();
			this.m_eventsKitMetadataCache = new List<IEventsKitMetadata>();
			this.m_eventsCache = new List<IEventMetadata>();
			this.m_eventsByIdCache = new Dictionary<long, IEventMetadata>();
			this.m_graph = new Dictionary<string, EventsKitExplorer.AssemblyNode>();
			foreach (string text in paths)
			{
				foreach (string text2 in AssemblyWalker.GetAssemblyFileNames(text, recursive, predicate))
				{
					this.LoadEventKitsFromFile(text2);
				}
			}
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x0005CAE4 File Offset: 0x0005ACE4
		internal EventsKitExplorer([NotNull] IEnumerable<Type> eventsKitTypes)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<Type>>(eventsKitTypes, "eventsKitTypes");
			this.m_eventKits = new Dictionary<long, EventsKitMetadata>();
			this.m_eventsKitMetadataCache = new List<IEventsKitMetadata>();
			this.m_eventsCache = new List<IEventMetadata>();
			this.m_eventsByIdCache = new Dictionary<long, IEventMetadata>();
			this.m_graph = new Dictionary<string, EventsKitExplorer.AssemblyNode>();
			this.LoadEventKitsFromTypes(eventsKitTypes);
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x0005CB40 File Offset: 0x0005AD40
		public IEnumerable<IEventsKitMetadata> EventKits
		{
			get
			{
				return this.m_eventsKitMetadataCache;
			}
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0005CB48 File Offset: 0x0005AD48
		public IEnumerable<IEventsKitMetadata> GetEventKitsInClosure(string fileName)
		{
			return this.GetEventKitsInClosure(Path.GetFileNameWithoutExtension(fileName), new HashSet<string>()).Cast<IEventsKitMetadata>();
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x0005CB60 File Offset: 0x0005AD60
		public IEnumerable<IEventMetadata> Events
		{
			get
			{
				return this.m_eventsCache;
			}
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x0005CB68 File Offset: 0x0005AD68
		public IEventsKitMetadata GetEventsKit(long id)
		{
			EventsKitMetadata eventsKitMetadata;
			if (!this.m_eventKits.TryGetValue(id, out eventsKitMetadata))
			{
				throw new EventsKitNotFoundException(id);
			}
			return eventsKitMetadata;
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x0005CB8D File Offset: 0x0005AD8D
		public IEventMetadata GetEventMetadata(EventsKitIdentifiers id)
		{
			return this.GetEventsKit(id.EventsKitId).GetEvent(id.EventId);
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x0005CBA6 File Offset: 0x0005ADA6
		public IEventMetadata GetEventMetadata(Guid id)
		{
			return this.GetEventMetadata(new EventsKitIdentifiers(id));
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x0005CBB4 File Offset: 0x0005ADB4
		public bool TryGetEventMetadata(long eventId, out IEventMetadata evm)
		{
			return this.m_eventsByIdCache.TryGetValue(eventId, out evm);
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x0005CBC4 File Offset: 0x0005ADC4
		public IEnumerable<T> GetAttributes<T>(EventsKitIdentifiers id) where T : Attribute
		{
			IEnumerable<T> enumerable = this.GetEventMetadata(id).EventMethod.GetCustomAttributes(typeof(T), false).Cast<T>();
			IEnumerable<T> enumerable2 = this.GetEventsKit(id.EventsKitId).EventsKitType.GetCustomAttributes(typeof(T), false).Cast<T>();
			return enumerable.Concat(enumerable2);
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x0005CC1F File Offset: 0x0005AE1F
		public string GetFullEventsKitSchema()
		{
			return this.GetFullEventsKitSchema(null);
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x0005CC28 File Offset: 0x0005AE28
		public string GetFullEventsKitSchema(Type useOnlyEventsWithAttributeType)
		{
			return (from ek in this.EventKits
				orderby ek.Id.EventsKitId
				select ek.GetSchemaAsString(useOnlyEventsWithAttributeType)).StringJoin(Environment.NewLine);
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x0005CC88 File Offset: 0x0005AE88
		private IEnumerable<Type> GetTypes(Assembly assembly)
		{
			Type[] array;
			try
			{
				array = assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				array = ex.Types;
			}
			return array.Where((Type t) => t != null && t.IsInterface);
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x0005CCDC File Offset: 0x0005AEDC
		private static List<string> GetDependencies(Assembly assembly)
		{
			List<string> list = new List<string>();
			try
			{
				list = (from a in assembly.GetReferencedAssemblies()
					select a.Name).ToList<string>();
			}
			catch (ReflectionTypeLoadException ex)
			{
				TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Warning, "Failed locating dependencies for {0}: {1}", new object[] { assembly, ex });
			}
			return list;
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x0005CD54 File Offset: 0x0005AF54
		private EventsKitExplorer.ReflectionResult Reflect(string filename)
		{
			EventsKitExplorer.ReflectionResult reflectionResult = new EventsKitExplorer.ReflectionResult(Path.GetFileNameWithoutExtension(filename));
			try
			{
				Assembly assembly = this.m_loadedAssemblies.FirstOrDefault((Assembly a) => Path.GetFileName(a.Location).Equals(Path.GetFileName(filename), StringComparison.Ordinal));
				if (assembly != null)
				{
					reflectionResult = new EventsKitExplorer.ReflectionResult(assembly.GetName().Name, EventsKitExplorer.GetDependencies(assembly), assembly);
				}
				else
				{
					assembly = Assembly.ReflectionOnlyLoadFrom(filename);
					reflectionResult = new EventsKitExplorer.ReflectionResult(assembly.GetName().Name, EventsKitExplorer.GetDependencies(assembly), null);
				}
				foreach (Type type in this.GetTypes(assembly).Where(new Func<Type, bool>(EventsKitExplorer.IsTypeAnEventsKit)))
				{
					reflectionResult.EventKitTypes.Add(type);
				}
			}
			catch (BadImageFormatException ex)
			{
				TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Warning, "Failed loading {0}: {1}", new object[] { filename, ex });
			}
			catch (FileLoadException ex2)
			{
				TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Warning, "Failed loading {0}: {1}", new object[] { filename, ex2 });
			}
			return reflectionResult;
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x0005CEA4 File Offset: 0x0005B0A4
		public static bool IsEventsKit([NotNull] MemberInfo type)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<MemberInfo>(type, "type");
			return type.GetCustomAttributes(typeof(EventsKitAttribute), false).Length != 0;
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x0005CEC8 File Offset: 0x0005B0C8
		private static bool IsTypeAnEventsKit(Type type)
		{
			try
			{
				return CustomAttributeData.GetCustomAttributes(type).Any((CustomAttributeData a) => a.Constructor.DeclaringType.FullName.Equals(typeof(EventsKitAttribute).FullName));
			}
			catch (FileLoadException ex)
			{
				TraceSourceBase<EventingTrace>.Tracer.TraceWarning("Failed loading type {0}: {1}", new object[] { type, ex });
			}
			catch (CustomAttributeFormatException ex2)
			{
				TraceSourceBase<EventingTrace>.Tracer.TraceWarning("Failed loading type {0}: {1}", new object[] { type, ex2 });
			}
			catch (TypeLoadException ex3)
			{
				TraceSourceBase<EventingTrace>.Tracer.TraceWarning("Failed loading type {0}: {1}", new object[] { type, ex3 });
			}
			return false;
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x0005CF90 File Offset: 0x0005B190
		private void LoadEventKitsFromFile(string filename)
		{
			TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Analyzing {0}", new object[] { filename });
			Stopwatch stopwatch = Stopwatch.StartNew();
			EventsKitExplorer.ReflectionResult reflectionResult = this.Reflect(filename);
			if (this.m_graph.ContainsKey(reflectionResult.Name))
			{
				stopwatch.Stop();
				TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Skipping loading {0} as it has already appeared before", new object[] { filename });
				return;
			}
			EventsKitExplorer.AssemblyNode assemblyNode = new EventsKitExplorer.AssemblyNode(reflectionResult.Dependencies);
			if (reflectionResult.EventKitTypes.Count<Type>() > 0)
			{
				Assembly assembly = reflectionResult.LocalAssembly ?? Assembly.LoadFrom(filename);
				foreach (Type type in reflectionResult.EventKitTypes)
				{
					EventsKitMetadata eventsKitMetadata = new EventsKitMetadata(assembly.GetType(type.FullName));
					try
					{
						this.m_eventKits.Add(eventsKitMetadata.Id.EventsKitId, eventsKitMetadata);
						assemblyNode.Eventkits.Add(eventsKitMetadata);
					}
					catch (ArgumentException)
					{
						EventsKitMetadata eventsKitMetadata2 = null;
						if (this.m_eventKits.TryGetValue(eventsKitMetadata.Id.EventsKitId, out eventsKitMetadata2))
						{
							throw new EventsAlreadyExistsException(eventsKitMetadata.EventsKitType.Name, eventsKitMetadata.Id.EventId, eventsKitMetadata.EventsKitType.Assembly.CodeBase, eventsKitMetadata2.EventsKitType.Assembly.CodeBase);
						}
						throw new EventsAlreadyExistsException(eventsKitMetadata.EventsKitType.Name, eventsKitMetadata.Id.EventId, eventsKitMetadata.EventsKitType.Assembly.CodeBase, "??? Unknown ???");
					}
					this.m_eventsKitMetadataCache.Add(eventsKitMetadata);
					this.m_eventsCache.AddRange(eventsKitMetadata.Events);
					foreach (IEventMetadata eventMetadata in eventsKitMetadata.Events)
					{
						this.m_eventsByIdCache.Add(eventMetadata.Id.EventId, eventMetadata);
					}
				}
			}
			this.m_graph.Add(reflectionResult.Name, assemblyNode);
			stopwatch.Stop();
			TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Analyzed {0} during {1} ms", new object[] { filename, stopwatch.ElapsedMilliseconds });
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x0005D224 File Offset: 0x0005B424
		private void LoadEventKitsFromTypes(IEnumerable<Type> eventsKitTypes)
		{
			ExtendedDiagnostics.EnsureOperation(eventsKitTypes.All(new Func<Type, bool>(EventsKitExplorer.IsTypeAnEventsKit)), "All type must be events kits");
			TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Analyzing events collection");
			Stopwatch stopwatch = Stopwatch.StartNew();
			foreach (Type type in eventsKitTypes)
			{
				EventsKitMetadata eventsKitMetadata = new EventsKitMetadata(type);
				this.m_eventKits.Add(eventsKitMetadata.Id.EventsKitId, eventsKitMetadata);
				this.m_eventsKitMetadataCache.Add(eventsKitMetadata);
				this.m_eventsCache.AddRange(eventsKitMetadata.Events);
				foreach (IEventMetadata eventMetadata in eventsKitMetadata.Events)
				{
					this.m_eventsByIdCache.Add(eventMetadata.Id.EventId, eventMetadata);
				}
			}
			stopwatch.Stop();
			TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Analyzed collection during {0} ms", new object[] { stopwatch.ElapsedMilliseconds });
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x0005D350 File Offset: 0x0005B550
		private IEnumerable<EventsKitMetadata> GetEventKitsInClosure(string assemblyName, HashSet<string> visited)
		{
			List<EventsKitMetadata> list = new List<EventsKitMetadata>();
			EventsKitExplorer.AssemblyNode assemblyNode = null;
			if (!visited.Contains(assemblyName) && this.m_graph.TryGetValue(assemblyName, out assemblyNode))
			{
				visited.Add(assemblyName);
				list.AddRange(assemblyNode.Eventkits);
				foreach (string text in assemblyNode.Neighbors)
				{
					list.AddRange(this.GetEventKitsInClosure(text, visited));
				}
			}
			return list;
		}

		// Token: 0x04000892 RID: 2194
		private readonly Dictionary<long, EventsKitMetadata> m_eventKits;

		// Token: 0x04000893 RID: 2195
		private readonly List<IEventsKitMetadata> m_eventsKitMetadataCache;

		// Token: 0x04000894 RID: 2196
		private readonly List<IEventMetadata> m_eventsCache;

		// Token: 0x04000895 RID: 2197
		private readonly Dictionary<long, IEventMetadata> m_eventsByIdCache;

		// Token: 0x04000896 RID: 2198
		private readonly Dictionary<string, EventsKitExplorer.AssemblyNode> m_graph;

		// Token: 0x04000897 RID: 2199
		private readonly HashSet<Assembly> m_loadedAssemblies;

		// Token: 0x0200079B RID: 1947
		private class AssemblyNode
		{
			// Token: 0x060030F0 RID: 12528 RVA: 0x000A7015 File Offset: 0x000A5215
			public AssemblyNode(List<string> dependencies)
			{
				this.Neighbors = dependencies;
				this.Eventkits = new List<EventsKitMetadata>();
			}

			// Token: 0x17000761 RID: 1889
			// (get) Token: 0x060030F1 RID: 12529 RVA: 0x000A702F File Offset: 0x000A522F
			// (set) Token: 0x060030F2 RID: 12530 RVA: 0x000A7037 File Offset: 0x000A5237
			public List<EventsKitMetadata> Eventkits { get; private set; }

			// Token: 0x17000762 RID: 1890
			// (get) Token: 0x060030F3 RID: 12531 RVA: 0x000A7040 File Offset: 0x000A5240
			// (set) Token: 0x060030F4 RID: 12532 RVA: 0x000A7048 File Offset: 0x000A5248
			public List<string> Neighbors { get; private set; }
		}

		// Token: 0x0200079C RID: 1948
		private class ReflectionResult
		{
			// Token: 0x060030F5 RID: 12533 RVA: 0x000A7051 File Offset: 0x000A5251
			public ReflectionResult(string filename)
				: this(filename, new List<string>(), null)
			{
			}

			// Token: 0x060030F6 RID: 12534 RVA: 0x000A7060 File Offset: 0x000A5260
			public ReflectionResult(string filename, List<string> dependencies, Assembly localAssembly)
			{
				this.Name = filename;
				this.EventKitTypes = new List<Type>();
				this.Dependencies = dependencies;
				this.LocalAssembly = localAssembly;
			}

			// Token: 0x17000763 RID: 1891
			// (get) Token: 0x060030F7 RID: 12535 RVA: 0x000A7088 File Offset: 0x000A5288
			// (set) Token: 0x060030F8 RID: 12536 RVA: 0x000A7090 File Offset: 0x000A5290
			public string Name { get; private set; }

			// Token: 0x17000764 RID: 1892
			// (get) Token: 0x060030F9 RID: 12537 RVA: 0x000A7099 File Offset: 0x000A5299
			// (set) Token: 0x060030FA RID: 12538 RVA: 0x000A70A1 File Offset: 0x000A52A1
			public List<Type> EventKitTypes { get; private set; }

			// Token: 0x17000765 RID: 1893
			// (get) Token: 0x060030FB RID: 12539 RVA: 0x000A70AA File Offset: 0x000A52AA
			// (set) Token: 0x060030FC RID: 12540 RVA: 0x000A70B2 File Offset: 0x000A52B2
			public List<string> Dependencies { get; private set; }

			// Token: 0x17000766 RID: 1894
			// (get) Token: 0x060030FD RID: 12541 RVA: 0x000A70BB File Offset: 0x000A52BB
			// (set) Token: 0x060030FE RID: 12542 RVA: 0x000A70C3 File Offset: 0x000A52C3
			public Assembly LocalAssembly { get; private set; }
		}
	}
}
