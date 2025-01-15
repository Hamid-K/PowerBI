using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing.EventsKit
{
	// Token: 0x020003AE RID: 942
	public static class PerformanceCountersManifestUtilities
	{
		// Token: 0x06001D18 RID: 7448 RVA: 0x0006E93D File Offset: 0x0006CB3D
		public static XElement GeneratePerformanceCountersManifest(IEnumerable<string> paths, Guid providerGuid, string providerName, string dynamicLibraryFileName, string counterSerNamePrefix)
		{
			return PerformanceCountersManifestUtilities.GeneratePerformanceCountersManifest(PerformanceCountersManifestUtilities.GetCategoriesFromEventsKits(paths), providerGuid, providerName, dynamicLibraryFileName, counterSerNamePrefix);
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x0006E94F File Offset: 0x0006CB4F
		public static Dictionary<string, IEnumerable<string>> GetPerformanceCountersFromManifest(string manifestPath)
		{
			return PerformanceCountersManifestUtilities.GetPerformanceCountersInformationFromManifest(XElement.Load(manifestPath));
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x0006E95C File Offset: 0x0006CB5C
		public static void CompilePerformanceCountersManifestToResourceFile(string performanceCountersToolsPath, string manifestFilePath, string resourceFilePath)
		{
			TraceSourceBase<EventingTrace>.Tracer.TraceInformation("Compiling the performance counters manifest into a resource file using the CTRPP tool. ({0}->{1})", new object[]
			{
				Path.GetFileName(manifestFilePath),
				Path.GetFileName(resourceFilePath)
			});
			string text = Path.Combine(performanceCountersToolsPath, "ctrpp.exe");
			string text2 = "-rc {0} {1}".FormatWithInvariantCulture(new object[] { resourceFilePath, manifestFilePath });
			string text3;
			string text4;
			int num = ExtendedProcess.Run(text, text2, PerformanceCountersManifestUtilities.c_compilePerformanceCountersManifestToResourceFileTimeout, ExtendedProcessOptions.KillProcessOnTimeout, out text3, out text4);
			if (num != 0)
			{
				throw new CompilePerformanceCountersManifestToResourceFileException(text, text2, num, text3, text4);
			}
			TraceSourceBase<EventingTrace>.Tracer.TraceInformation("Compiling the performance counters manifest into a resource file has succeeded: {0}", new object[] { text3 });
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x0006E9F4 File Offset: 0x0006CBF4
		public static void CompilePerformanceCountersResourceFileToCompiledResourceFile(string performanceCountersToolsPath, string resourceFilePath, string compiledResourceFilePath)
		{
			TraceSourceBase<EventingTrace>.Tracer.TraceInformation("Compiling the resource file into a compiled resource file using the RC tool. ({0}->{1})", new object[]
			{
				Path.GetFileName(resourceFilePath),
				Path.GetFileName(compiledResourceFilePath)
			});
			string text = Path.Combine(performanceCountersToolsPath, "rc.exe");
			string text2 = "/r /fo {0} {1}".FormatWithInvariantCulture(new object[] { compiledResourceFilePath, resourceFilePath });
			string text3;
			string text4;
			int num = ExtendedProcess.Run(text, text2, PerformanceCountersManifestUtilities.c_compilePerformanceCountersResourceFileToCompiledResourceFileTimeout, ExtendedProcessOptions.KillProcessOnTimeout, out text3, out text4);
			if (num != 0)
			{
				throw new CompilePerformanceCountersResourceFileToCompiledResourceFileException(text, text2, num, text3, text4);
			}
			TraceSourceBase<EventingTrace>.Tracer.TraceInformation("Compiling the resource file into a compiled resource file has succeeded: {0}", new object[] { text3 });
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x0006EA8C File Offset: 0x0006CC8C
		public static void LinkCompiledResourceFileToDynamicLibrary(string performanceCountersToolsPath, string compiledResourceFilePath, string dynamicLibraryPath)
		{
			TraceSourceBase<EventingTrace>.Tracer.TraceInformation("Linking the compiled resource file into a dynamic library. ({0}->{1})", new object[]
			{
				Path.GetFileName(compiledResourceFilePath),
				Path.GetFileName(dynamicLibraryPath)
			});
			string text = Path.Combine(performanceCountersToolsPath, "link.exe");
			string text2 = "/dll /noentry /machine:x64 /OUT:{0} {1}".FormatWithInvariantCulture(new object[] { dynamicLibraryPath, compiledResourceFilePath });
			string text3;
			string text4;
			int num = ExtendedProcess.Run(text, text2, PerformanceCountersManifestUtilities.c_linkCompiledResourceFileToDynamicLibraryTimeout, ExtendedProcessOptions.KillProcessOnTimeout, out text3, out text4);
			if (num != 0)
			{
				throw new LinkCompiledResourceFileToDynamicLibraryException(text, text2, num, text3, text4);
			}
			TraceSourceBase<EventingTrace>.Tracer.TraceInformation("Linking the compiled resource file into a dynamic library has succeeded: {0}", new object[] { text3 });
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x0006EB24 File Offset: 0x0006CD24
		private static Dictionary<PerformanceCounterCategoryMetadata, HashSet<PerformanceCounterMetadata>> GetCategoriesFromEventsKits(IEnumerable<string> paths)
		{
			ConcurrentBag<Dictionary<PerformanceCounterCategoryMetadata, HashSet<PerformanceCounterMetadata>>> counterSetDictionaries = new ConcurrentBag<Dictionary<PerformanceCounterCategoryMetadata, HashSet<PerformanceCounterMetadata>>>();
			Parallel.ForEach<string>(paths, delegate(string path)
			{
				counterSetDictionaries.Add(PerformanceCountersManifestUtilities.GetCategoriesFromEventsKits(path, new Predicate<string>(EventsKitExplorerFactory.AssemblyHasEventKits)));
			});
			return (from kvp in counterSetDictionaries.SelectMany((Dictionary<PerformanceCounterCategoryMetadata, HashSet<PerformanceCounterMetadata>> d) => d)
				group kvp by kvp.Key).ToDictionary((IGrouping<PerformanceCounterCategoryMetadata, KeyValuePair<PerformanceCounterCategoryMetadata, HashSet<PerformanceCounterMetadata>>> g) => g.Key, (IGrouping<PerformanceCounterCategoryMetadata, KeyValuePair<PerformanceCounterCategoryMetadata, HashSet<PerformanceCounterMetadata>>> g) => g.SelectMany((KeyValuePair<PerformanceCounterCategoryMetadata, HashSet<PerformanceCounterMetadata>> kvp) => kvp.Value).ToHashSet<PerformanceCounterMetadata>());
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x0006EBE6 File Offset: 0x0006CDE6
		private static Dictionary<PerformanceCounterCategoryMetadata, HashSet<PerformanceCounterMetadata>> GetCategoriesFromEventsKits(string path, Predicate<string> assemblyLoadPredicate)
		{
			Dictionary<PerformanceCounterCategoryMetadata, HashSet<PerformanceCounterMetadata>> counterSetupCollection = null;
			ExtendedAppDomain.RunWithPrivateAppDomainWorker<PerformanceCountersManifestUtilities.BuildCategoryCollectionWorker>(delegate(PerformanceCountersManifestUtilities.BuildCategoryCollectionWorker worker)
			{
				counterSetupCollection = worker.GetEventsKits(path, false, assemblyLoadPredicate);
			});
			return counterSetupCollection;
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x0006EC18 File Offset: 0x0006CE18
		private static XElement GeneratePerformanceCountersManifest(Dictionary<PerformanceCounterCategoryMetadata, HashSet<PerformanceCounterMetadata>> counterSets, Guid providerGuid, string providerName, string dynamicLibraryFileName, string counterSerNamePrefix)
		{
			return new XElement(PerformanceCountersManifestUtilities.c_eventsXNamespace + "instrumentationManifest", new object[]
			{
				new XAttribute("xmlns", "http://schemas.microsoft.com/win/2004/08/events"),
				new XAttribute(XNamespace.Xmlns + "win", "http://manifests.microsoft.com/win/2004/08/windows/events"),
				new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
				new XAttribute(XNamespace.Xmlns + "xs", "http://www.w3.org/2001/XMLSchema"),
				new XAttribute(XNamespace.Xmlns + "trace", "http://schemas.microsoft.com/win/2004/08/events/trace"),
				new XAttribute(PerformanceCountersManifestUtilities.c_eventsXNamespaceXsi + "schemaLocation", "http://schemas.microsoft.com/win/2004/08/events eventman.xsd"),
				new XElement(PerformanceCountersManifestUtilities.c_eventsXNamespace + "instrumentation", new XElement(PerformanceCountersManifestUtilities.c_countersXNamespace + "counters", new object[]
				{
					new XAttribute("schemaVersion", "1.1"),
					new XAttribute("xmlns", "http://schemas.microsoft.com/win/2005/12/counters"),
					new XAttribute(XNamespace.Xmlns + "auto-ns1", "http://schemas.microsoft.com/win/2004/08/events"),
					PerformanceCountersManifestUtilities.GenerateProviderXml(counterSets, providerGuid, providerName, dynamicLibraryFileName, counterSerNamePrefix)
				}))
			});
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x0006ED6C File Offset: 0x0006CF6C
		private static XElement GenerateProviderXml(Dictionary<PerformanceCounterCategoryMetadata, HashSet<PerformanceCounterMetadata>> counterSets, Guid providerGuid, string providerName, string dynamicLibraryFileName, string counterSerNamePrefix)
		{
			return new XElement(PerformanceCountersManifestUtilities.c_countersXNamespace + "provider", new object[]
			{
				new XAttribute("providerName", providerName),
				new XAttribute("applicationIdentity", dynamicLibraryFileName),
				new XAttribute("providerType", "userMode"),
				new XAttribute("providerGuid", providerGuid.ToString("B")),
				new XAttribute("symbol", providerName),
				counterSets.Select((KeyValuePair<PerformanceCounterCategoryMetadata, HashSet<PerformanceCounterMetadata>> kvp) => PerformanceCountersManifestUtilities.GenerateCounterSetXml(kvp.Key, kvp.Value, counterSerNamePrefix)).ToArray<XElement>()
			});
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x0006EE2C File Offset: 0x0006D02C
		private static XElement GenerateCounterSetXml(PerformanceCounterCategoryMetadata counterSet, IEnumerable<PerformanceCounterMetadata> counters, string counterSetNamePrefix)
		{
			return new XElement(PerformanceCountersManifestUtilities.c_countersXNamespace + "counterSet", new object[]
			{
				new XAttribute("name", "{0}.{1}".FormatWithInvariantCulture(new object[] { counterSetNamePrefix, counterSet.CategoryName })),
				new XAttribute("guid", counterSet.CategoryId.ToString("B")),
				new XAttribute("uri", "Microsoft.Windows.System.PerfCounters.{0}".FormatWithInvariantCulture(new object[] { counterSet.CategorySymbol })),
				new XAttribute("description", counterSet.CategoryName),
				new XAttribute("instances", "multiple"),
				new XAttribute("symbol", counterSet.CategorySymbol),
				counters.SelectMany((PerformanceCounterMetadata counter) => PerformanceCountersManifestUtilities.GenerateCountersXml(counterSet, counter))
			});
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x0006EF54 File Offset: 0x0006D154
		private static IEnumerable<XElement> GenerateCountersXml(PerformanceCounterCategoryMetadata counterSet, PerformanceCounterMetadata counter)
		{
			List<XElement> list = new List<XElement>();
			list.Add(PerformanceCountersManifestUtilities.GenerateCounterXml(counterSet, counter));
			if (counter.Base != null)
			{
				list.Add(PerformanceCountersManifestUtilities.GenerateBaseCounterXml(counterSet, counter.Base));
			}
			return list;
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x0006EF90 File Offset: 0x0006D190
		private static XElement GenerateCounterXml(PerformanceCounterCategoryMetadata counterSet, PerformanceCounterMetadata counter)
		{
			XElement xelement = new XElement(PerformanceCountersManifestUtilities.c_countersXNamespace + "counter", new object[]
			{
				new XAttribute("name", counter.CounterName),
				new XAttribute("id", counter.CounterId),
				new XAttribute("symbol", "{0}_{1}".FormatWithInvariantCulture(new object[] { counterSet.CategorySymbol, counter.CounterSymbol })),
				new XAttribute("uri", "Microsoft.Cloud.Platform.PerfCounters.{0}.{1}".FormatWithInvariantCulture(new object[] { counterSet.CategorySymbol, counter.CounterSymbol })),
				new XAttribute("description", counter.CounterName),
				new XAttribute("type", PerformanceCountersManifestUtilities.GetCounterTypeString(counter.CounterType)),
				new XAttribute("detailLevel", "standard"),
				new XElement(PerformanceCountersManifestUtilities.c_countersXNamespace + "counterAttributes", new XElement(PerformanceCountersManifestUtilities.c_countersXNamespace + "counterAttribute", new XAttribute("name", "reference")))
			});
			if (counter.Base != null)
			{
				xelement.Add(new XAttribute("baseID", counter.Base.CounterId));
			}
			return xelement;
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x0006F110 File Offset: 0x0006D310
		private static XElement GenerateBaseCounterXml(PerformanceCounterCategoryMetadata counterSet, PerformanceCounterMetadata counter)
		{
			XElement xelement = PerformanceCountersManifestUtilities.GenerateCounterXml(counterSet, counter);
			xelement.Descendants(PerformanceCountersManifestUtilities.c_countersXNamespace + "counterAttributes").First<XElement>().Add(new XElement(PerformanceCountersManifestUtilities.c_countersXNamespace + "counterAttribute", new XAttribute("name", "noDisplay")));
			return xelement;
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x0006F16B File Offset: 0x0006D36B
		private static string GetCounterTypeString(PerformanceCounterType counterType)
		{
			return PerformanceCountersManifestUtilities.c_counterTypetoString[counterType];
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x0006F178 File Offset: 0x0006D378
		private static Dictionary<string, IEnumerable<string>> GetPerformanceCountersInformationFromManifest(XElement manifestRoot)
		{
			return manifestRoot.Descendants(PerformanceCountersManifestUtilities.c_countersXNamespace + "counterSet").ToDictionary((XElement cs) => cs.Attribute("name").Value, (XElement cs) => from c in cs.Descendants(PerformanceCountersManifestUtilities.c_countersXNamespace + "counter")
				where c.Descendants(PerformanceCountersManifestUtilities.c_countersXNamespace + "counterAttribute").None((XElement ca) => ca.Attributes("name").Any((XAttribute name) => name.Value.Equals("noDisplay")))
				select c.Attribute("name").Value);
		}

		// Token: 0x040009C4 RID: 2500
		private const string c_ctrppTool = "ctrpp.exe";

		// Token: 0x040009C5 RID: 2501
		private const string c_rcTool = "rc.exe";

		// Token: 0x040009C6 RID: 2502
		private const string c_linkTool = "link.exe";

		// Token: 0x040009C7 RID: 2503
		private static readonly int c_compilePerformanceCountersManifestToResourceFileTimeout = (int)TimeSpan.FromSeconds(10.0).TotalMilliseconds;

		// Token: 0x040009C8 RID: 2504
		private static readonly int c_compilePerformanceCountersResourceFileToCompiledResourceFileTimeout = (int)TimeSpan.FromSeconds(10.0).TotalMilliseconds;

		// Token: 0x040009C9 RID: 2505
		private static readonly int c_linkCompiledResourceFileToDynamicLibraryTimeout = (int)TimeSpan.FromSeconds(30.0).TotalMilliseconds;

		// Token: 0x040009CA RID: 2506
		private const string c_eventsNamespace = "http://schemas.microsoft.com/win/2004/08/events";

		// Token: 0x040009CB RID: 2507
		private const string c_eventsNamespaceXsi = "http://www.w3.org/2001/XMLSchema-instance";

		// Token: 0x040009CC RID: 2508
		private const string c_countersNamespace = "http://schemas.microsoft.com/win/2005/12/counters";

		// Token: 0x040009CD RID: 2509
		private static readonly XNamespace c_eventsXNamespace = "http://schemas.microsoft.com/win/2004/08/events";

		// Token: 0x040009CE RID: 2510
		private static readonly XNamespace c_eventsXNamespaceXsi = "http://www.w3.org/2001/XMLSchema-instance";

		// Token: 0x040009CF RID: 2511
		private static readonly XNamespace c_countersXNamespace = "http://schemas.microsoft.com/win/2005/12/counters";

		// Token: 0x040009D0 RID: 2512
		private static readonly Dictionary<PerformanceCounterType, string> c_counterTypetoString = new Dictionary<PerformanceCounterType, string>
		{
			{
				PerformanceCounterType.CounterDelta64,
				"perf_counter_large_delta"
			},
			{
				PerformanceCounterType.NumberOfItems64,
				"perf_counter_large_rawcount"
			},
			{
				PerformanceCounterType.AverageCount64,
				"perf_average_bulk"
			},
			{
				PerformanceCounterType.CountPerTimeInterval64,
				"perf_counter_large_queuelen_type"
			},
			{
				PerformanceCounterType.RateOfCountsPerSecond64,
				"perf_counter_bulk_count"
			},
			{
				PerformanceCounterType.AverageBase,
				"perf_average_base"
			}
		};

		// Token: 0x020007C9 RID: 1993
		private sealed class BuildCategoryCollectionWorker : MarshalByRefObject
		{
			// Token: 0x060031C7 RID: 12743 RVA: 0x000A88AC File Offset: 0x000A6AAC
			public Dictionary<PerformanceCounterCategoryMetadata, HashSet<PerformanceCounterMetadata>> GetEventsKits(string path, bool recursive, Predicate<string> assemblyLoadPredicate)
			{
				return (from EventsKitMetadata ekmd in new EventsKitExplorerFactory(path, assemblyLoadPredicate).Create(path, recursive, assemblyLoadPredicate).EventKits
					from emd in ekmd.Events.Cast<EventsKitEventMetadata>()
					from pcm in emd.PerformanceCounters.Cast<PerformanceCounterMetadata>()
					group pcm by ekmd.PerformanceCountersCategory).ToDictionary((IGrouping<PerformanceCounterCategoryMetadata, PerformanceCounterMetadata> g) => g.Key, (IGrouping<PerformanceCounterCategoryMetadata, PerformanceCounterMetadata> g) => g.ToHashSet<PerformanceCounterMetadata>());
			}
		}
	}
}
