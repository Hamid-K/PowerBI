using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Cloud.Platform.Eventing;
using Microsoft.Cloud.Platform.Utils;
using Microsoft.Diagnostics.Tracing;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000350 RID: 848
	public static class EventSourceUtils
	{
		// Token: 0x06001927 RID: 6439 RVA: 0x0005D884 File Offset: 0x0005BA84
		public static EtwProvidersManifests GenerateEtwProvidersManifestsFromFile(string manifestsFolderPath)
		{
			IEnumerable<string> enumerable = Directory.EnumerateFiles(manifestsFolderPath);
			EtwEventsReader etwEventsReader = new EtwEventsReader();
			return etwEventsReader.EndReadProvidersManifests(etwEventsReader.BeginReadProvidersManifests(enumerable, null, null));
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x0005D8AB File Offset: 0x0005BAAB
		public static List<string> GenerateEventSourceManifests(IEnumerable<string> assembliesPaths, Predicate<string> assemblyLoadPredicate, string assemblyPathToStartAt)
		{
			List<string> res = null;
			ExtendedAppDomain.RunWithPrivateAppDomainWorker<EventSourceUtils.EventsKitExplorerWorker>(delegate(EventSourceUtils.EventsKitExplorerWorker worker)
			{
				res = worker.GenerateSerializedProvidersManifests(assembliesPaths, false, assemblyLoadPredicate);
			}, assemblyPathToStartAt);
			return res;
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x0005D8DE File Offset: 0x0005BADE
		public static void GenerateEventsKitsImplementationLibraries(string assembliesPath, Predicate<string> assemblyLoadPredicate)
		{
			ExtendedAppDomain.RunWithPrivateAppDomainWorker<EventSourceUtils.EventsKitExplorerWorker>(delegate(EventSourceUtils.EventsKitExplorerWorker worker)
			{
				worker.GenerateEventsKitsImplementationLibraries(assembliesPath, true, assemblyLoadPredicate);
			});
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x0005D903 File Offset: 0x0005BB03
		public static List<string> GenerateEventSourceManifests(IEnumerable<string> assembliesPaths, Predicate<string> assemblyLoadPredicate)
		{
			List<string> res = null;
			ExtendedAppDomain.RunWithPrivateAppDomainWorker<EventSourceUtils.EventsKitExplorerWorker>(delegate(EventSourceUtils.EventsKitExplorerWorker worker)
			{
				res = worker.GenerateSerializedProvidersManifests(assembliesPaths, false, assemblyLoadPredicate);
			});
			return res;
		}

		// Token: 0x020007A0 RID: 1952
		private sealed class EventsKitExplorerWorker : MarshalByRefObject
		{
			// Token: 0x0600310B RID: 12555 RVA: 0x000A7160 File Offset: 0x000A5360
			public void GenerateEventsKitsImplementationLibraries(string assembliesPath, bool recursive, Predicate<string> assemblyLoadPredicate)
			{
				IEnumerable<Type> enumerable = this.ExtractEventKitsTypes(new List<string> { assembliesPath }, recursive, assemblyLoadPredicate, true);
				this.CheckEventKitsTypesLibraryFileNameConflict(enumerable);
				foreach (Type type in enumerable)
				{
					string eventsKitsImplementationLibraryHashedName = EventsKitFactoryUtils.GetEventsKitsImplementationLibraryHashedName(type);
					string text = Path.Combine(assembliesPath, eventsKitsImplementationLibraryHashedName);
					if (File.Exists(text))
					{
						break;
					}
					EventsKitFactoryUtils.Generate(type, EventsKitFactoryOptions.All, text);
				}
			}

			// Token: 0x0600310C RID: 12556 RVA: 0x000A71E4 File Offset: 0x000A53E4
			public List<string> GenerateSerializedProvidersManifests(IEnumerable<string> paths, bool recursive, Predicate<string> assemblyLoadPredicate)
			{
				return (from ekType in this.ExtractEventKitsTypes(paths, recursive, assemblyLoadPredicate, false).AsParallel<Type>()
					select EventSource.GenerateManifest(EventSourceUtils.EventsKitExplorerWorker.GetEventSourceType(ekType), null)).ToList<string>();
			}

			// Token: 0x0600310D RID: 12557 RVA: 0x000A7220 File Offset: 0x000A5420
			private void CheckEventKitsTypesLibraryFileNameConflict(IEnumerable<Type> eventKitsTypes)
			{
				var enumerable = from ek in eventKitsTypes
					group ek by EventsKitFactoryUtils.GetEventsKitsImplementationLibraryHashedName(ek) into @group
					where @group.Count<Type>() > 1
					select new
					{
						first = @group.First<Type>(),
						last = @group.Last<Type>()
					};
				if (enumerable.Count() > 0)
				{
					Type first = enumerable.ElementAt(0).first;
					string text = string.Join(", ", enumerable.Select(ekPair => "between {0} and {1}".FormatWithCurrentCulture(new object[]
					{
						ekPair.first.FullName,
						ekPair.last.FullName
					})));
					throw new EventsKitCreationFailedException(first.Name, first.Assembly.GetName().Name, "EventsKit library file name conflict detected. Please rename one of event kits for each of following event kit pair: {0}".FormatWithCurrentCulture(new object[] { text }));
				}
			}

			// Token: 0x0600310E RID: 12558 RVA: 0x000A7311 File Offset: 0x000A5511
			private bool AssemblyHasEventsKitPrebuilt(string path)
			{
				return File.Exists(Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path) + ".EventsKit.dll"));
			}

			// Token: 0x0600310F RID: 12559 RVA: 0x000A7334 File Offset: 0x000A5534
			private IEnumerable<Type> ExtractEventKitsTypes(IEnumerable<string> paths, bool recursive, Predicate<string> assemblyLoadPredicate, bool ignoreBuildGeneratedTypes = false)
			{
				ConcurrentBag<Type> eventKitsTypes = new ConcurrentBag<Type>();
				Dictionary<string, Exception> exceptions = new Dictionary<string, Exception>();
				Parallel.ForEach<string>(paths, delegate(string path)
				{
					Exception ex = TopLevelHandler.Run(this, TopLevelHandlerOption.SwallowNonfatal, delegate
					{
						IEnumerable<Type> enumerable;
						if (ignoreBuildGeneratedTypes)
						{
							enumerable = this.GetEventsKitsNotGeneratedInBuild(path, recursive, assemblyLoadPredicate).Distinct((Type t) => t.FullName).Materialize<Type>();
						}
						else
						{
							enumerable = this.GetEventsKits(path, recursive, assemblyLoadPredicate).Distinct((Type t) => t.FullName).Materialize<Type>();
						}
						foreach (Type type in enumerable)
						{
							eventKitsTypes.Add(type);
						}
					});
					if (ex != null)
					{
						exceptions["{0}{1}".FormatWithInvariantCulture(new object[]
						{
							ex.StackTrace,
							ex.GetType().Name
						})] = ex;
					}
				});
				if (exceptions.Any<KeyValuePair<string, Exception>>())
				{
					throw new EventsKitsRetrievalFailedException(paths.StringJoin(","), exceptions.Values.StringJoin(Environment.NewLine));
				}
				return eventKitsTypes.Distinct((Type t) => t.FullName);
			}

			// Token: 0x06003110 RID: 12560 RVA: 0x000A73EA File Offset: 0x000A55EA
			private static Type GetEventSourceType(Type eventsKit)
			{
				return EventsKitFactoryUtils.GetEventSourceImplementationType(EventsKitFactoryUtils.Generate(eventsKit, EventsKitFactoryOptions.EmitEtwEvents, null), eventsKit);
			}

			// Token: 0x06003111 RID: 12561 RVA: 0x000A73FC File Offset: 0x000A55FC
			private IEnumerable<Type> GetEventsKits(string path, bool recursive, Predicate<string> assemblyLoadPredicate)
			{
				IEventsKitExplorer eventsKitExplorer = new EventsKitExplorerFactory(path, assemblyLoadPredicate).Create(path, recursive, assemblyLoadPredicate);
				List<Type> list = new List<Type>();
				foreach (IEventsKitMetadata eventsKitMetadata in eventsKitExplorer.EventKits)
				{
					list.Add(eventsKitMetadata.EventsKitType);
				}
				return list;
			}

			// Token: 0x06003112 RID: 12562 RVA: 0x000A7464 File Offset: 0x000A5664
			private IEnumerable<Type> GetEventsKitsNotGeneratedInBuild(string path, bool recursive, Predicate<string> assemblyLoadPredicate)
			{
				return (from t in this.GetEventsKits(path, recursive, assemblyLoadPredicate)
					where !this.AssemblyHasEventsKitPrebuilt(Path.Combine(path, Path.GetFileName(t.Assembly.Location)))
					select t).ToArray<Type>();
			}
		}
	}
}
