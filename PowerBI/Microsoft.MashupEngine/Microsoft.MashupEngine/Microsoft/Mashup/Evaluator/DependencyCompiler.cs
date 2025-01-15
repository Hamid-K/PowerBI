using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Evaluator.Services;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001C6C RID: 7276
	public class DependencyCompiler : IDisposable
	{
		// Token: 0x0600B545 RID: 46405 RVA: 0x0024CB74 File Offset: 0x0024AD74
		public DependencyCompiler(IEngine engine, IEngineHost engineHost, IPackageSectionConfig preferredConfig = null, params IModule[] legacyDependencies)
		{
			this.engine = engine;
			this.preferredConfig = new DependencyCompiler.DocumentConfig(preferredConfig);
			this.libraryService = engineHost.QueryService<ILibraryService>();
			this.tracingService = TracingService.GetService(engineHost);
			this.evaluationConstants = engineHost.GetEvaluationConstants();
			this.loading = new HashSet<string>();
			this.legacyDependencies = legacyDependencies;
			this.legacyDependenciesKey = new DependencyCompiler.LegacyLibraryKey(this.legacyDependencies);
			this.engineHostByConfig = new Dictionary<DependencyCompiler.DocumentConfig, IEngineHost>();
			this.dataSourceKinds = new HashSet<string>();
			this.moduleVersions = new Dictionary<string, string>();
			ArrayBuilder<IEngineHost> arrayBuilder = new ArrayBuilder<IEngineHost>(5);
			arrayBuilder.Add(new SimpleEngineHost<DependencyCompiler>(this));
			arrayBuilder.Add(new SimpleEngineHost<ICurrentEnvironmentService>(new CurrentEnvironmentService()));
			this.preferredConfig.AddServices(this, engineHost, ref arrayBuilder);
			arrayBuilder.Add(engineHost);
			this.engineHost = new CompositeEngineHost(arrayBuilder.ToArray());
		}

		// Token: 0x17002D4C RID: 11596
		// (get) Token: 0x0600B546 RID: 46406 RVA: 0x0024CC52 File Offset: 0x0024AE52
		public IEngineHost EngineHost
		{
			get
			{
				return this.engineHost;
			}
		}

		// Token: 0x17002D4D RID: 11597
		// (get) Token: 0x0600B547 RID: 46407 RVA: 0x0024CC5C File Offset: 0x0024AE5C
		public IPackageSectionConfig PreferredConfig
		{
			get
			{
				return new PackageSectionConfig(this.preferredConfig.Culture, this.preferredConfig.TimeZone, null, null, null);
			}
		}

		// Token: 0x0600B548 RID: 46408 RVA: 0x0024CC8D File Offset: 0x0024AE8D
		public IModule Compile(IDocumentHost documentHost, SegmentedString text, IPackageSectionConfig config, CompileOptions compileOptions, List<IError> errors, out IModule library)
		{
			return this.CompileQuery(documentHost, text, config, compileOptions, errors, out library);
		}

		// Token: 0x0600B549 RID: 46409 RVA: 0x0024CCA0 File Offset: 0x0024AEA0
		public IModule Compile(IDocumentHost documentHost, string text, IPackageSectionConfig config, CompileOptions compileOptions, List<IError> errors)
		{
			IModule module;
			return this.CompileQuery(documentHost, SegmentedString.New(text), config, compileOptions, errors, out module);
		}

		// Token: 0x0600B54A RID: 46410 RVA: 0x0024CCC4 File Offset: 0x0024AEC4
		public void RemoveChangedDependenciesFromCaches()
		{
			KeyValuePair<string, DependencyCompiler.ModuleReference>[] array = DependencyCompiler.cachedVersions.ToArray<KeyValuePair<string, DependencyCompiler.ModuleReference>>();
			string[] array2 = new string[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = array[i].Key;
			}
			string[] loadedVersions = this.libraryService.GetLoadedVersions(array2);
			HashSet<string> hashSet = new HashSet<string>();
			for (int j = 0; j < loadedVersions.Length; j++)
			{
				if (array[j].Value.LoadVersion != loadedVersions[j])
				{
					DependencyCompiler.libraryCache.Remove(array2[j]);
					hashSet.Add(array2[j]);
				}
			}
			if (this.legacyDependenciesKey.Equals(DependencyCompiler.mostRecentLegacyDependenciesKey))
			{
				DependencyCompiler.RemoveDependents<DependencyCompiler.CachedDocumentKey, DependencyCompiler.CachedDocument>(DependencyCompiler.queryCache, hashSet);
			}
			else
			{
				DependencyCompiler.queryCache.Clear();
				DependencyCompiler.mostRecentLegacyDependenciesKey = this.legacyDependenciesKey;
			}
			DependencyCompiler.RemoveDependents<string, DependencyCompiler.CachedDocument>(DependencyCompiler.libraryCache, hashSet);
			DependencyCompiler.RemoveDependents<DependencyCompiler.LegacyLibraryKey, DependencyCompiler.LegacyLibrary>(DependencyCompiler.legacyLibraryCache, hashSet);
		}

		// Token: 0x0600B54B RID: 46411 RVA: 0x0024CDB0 File Offset: 0x0024AFB0
		internal void UpdateLoadVersionsForDependencies()
		{
			List<string> list = new List<string>(DependencyCompiler.cachedVersions.Count);
			foreach (KeyValuePair<string, DependencyCompiler.ModuleReference> keyValuePair in DependencyCompiler.cachedVersions)
			{
				if (keyValuePair.Value.LoadVersion == null)
				{
					list.Add(keyValuePair.Key);
				}
			}
			string[] loadedVersions = this.libraryService.GetLoadedVersions(list.ToArray());
			for (int i = 0; i < list.Count; i++)
			{
				DependencyCompiler.ModuleReference moduleReference;
				if (DependencyCompiler.cachedVersions.TryGetValue(list[i], out moduleReference))
				{
					moduleReference.LoadVersion = loadedVersions[i];
				}
			}
		}

		// Token: 0x0600B54C RID: 46412 RVA: 0x0024CE70 File Offset: 0x0024B070
		public static void ClearCaches()
		{
			DependencyCompiler.queryCache.Clear();
			DependencyCompiler.libraryCache.Clear();
			DependencyCompiler.legacyLibraryCache.Clear();
			DependencyCompiler.builtinModuleCache.Clear();
			DependencyCompiler.cachedVersions.Clear();
		}

		// Token: 0x0600B54D RID: 46413 RVA: 0x0024CEA4 File Offset: 0x0024B0A4
		void IDisposable.Dispose()
		{
			this.UpdateLoadVersionsForDependencies();
			using (IHostTrace hostTrace = this.tracingService.CreateTrace("DependencyCompiler/Dispose/DataSources", this.evaluationConstants, TraceEventType.Information, null))
			{
				foreach (string text in this.dataSourceKinds)
				{
					hostTrace.Add(text, true, false);
				}
			}
			using (IHostTrace hostTrace2 = this.tracingService.CreateTrace("DependencyCompiler/Dispose/Modules", this.evaluationConstants, TraceEventType.Information, null))
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.moduleVersions)
				{
					hostTrace2.Add("Module_" + keyValuePair.Key, keyValuePair.Value, false);
				}
			}
		}

		// Token: 0x0600B54E RID: 46414 RVA: 0x0024CFC0 File Offset: 0x0024B1C0
		private IModule CompileQuery(IDocumentHost documentHost, SegmentedString text, IPackageSectionConfig config, CompileOptions compileOptions, List<IError> errors, out IModule library)
		{
			DependencyCompiler.CachedDocumentKey cachedDocumentKey = new DependencyCompiler.CachedDocumentKey(text, compileOptions);
			DependencyCompiler.CachedDocument cachedDocument;
			if (!DependencyCompiler.queryCache.TryGetValue(cachedDocumentKey, out cachedDocument))
			{
				cachedDocument = DependencyCompiler.DocumentCompiler.CompileSource(this, false, text, compileOptions);
				if (cachedDocument.Document.Kind == DocumentKind.Section && !cachedDocument.HasDynamicDependencies)
				{
					DependencyCompiler.queryCache.Add(cachedDocumentKey, cachedDocument);
					cachedDocument.AddRefcounts();
				}
			}
			cachedDocument.Host.Host = documentHost;
			errors.AddRange(cachedDocument.Errors);
			library = cachedDocument.Library;
			DependencyCompiler.DocumentConfig documentConfig;
			if (this.preferredConfig.RequiresOverride(config, out documentConfig))
			{
				return this.BindConfiguration(cachedDocument.Module, documentConfig);
			}
			return cachedDocument.Module;
		}

		// Token: 0x0600B54F RID: 46415 RVA: 0x0024D060 File Offset: 0x0024B260
		private IModule GetLibrary(string sectionName, IEnumerable<string> moduleNames, CompileOptions compileOptions, List<IError> errors, HashSet<string> dependencies, out bool hasDynamicDependencies)
		{
			hasDynamicDependencies = false;
			List<IModule> list = new List<IModule>();
			foreach (string text in moduleNames)
			{
				dependencies.Add(text);
				DependencyCompiler.CachedDocument module = this.GetModule(sectionName, text, compileOptions, errors);
				if (module != null && module.Module != null)
				{
					list.Add(module.Module);
					if (module.HasDynamicDependencies || module.IsDynamicModule)
					{
						hasDynamicDependencies = true;
					}
				}
			}
			return this.engine.Link(list, delegate(IError error)
			{
				errors.Add(error);
			}, LinkOptions.ExplicitEnvironment);
		}

		// Token: 0x0600B550 RID: 46416 RVA: 0x0024D11C File Offset: 0x0024B31C
		private IModule GetLegacyLibrary(string sectionName, CompileOptions compileOptions, List<IError> errors)
		{
			DependencyCompiler.LegacyLibrary legacyLibrary;
			if (!DependencyCompiler.legacyLibraryCache.TryGetValue(this.legacyDependenciesKey, out legacyLibrary))
			{
				IModule[] array = (from module in this.legacyDependencies.Concat(DependencyCompiler.legacyModule)
					select this.GetLegacyModule(sectionName, module, compileOptions)).ToArray<IModule>();
				legacyLibrary = new DependencyCompiler.LegacyLibrary(this.engine.Link(array, delegate(IError error)
				{
					errors.Add(error);
				}, LinkOptions.None), this.legacyDependenciesKey.Components);
				DependencyCompiler.legacyLibraryCache.Add(this.legacyDependenciesKey, legacyLibrary);
			}
			return legacyLibrary.Library;
		}

		// Token: 0x0600B551 RID: 46417 RVA: 0x0024D1C8 File Offset: 0x0024B3C8
		private IModule GetLegacyModule(string sectionName, IModule module, CompileOptions compileOptions)
		{
			IModule module2;
			if (module.Name == "Legacy" && this.TryGetBuiltinModule(module.Name, out module2))
			{
				return module2;
			}
			if (this.TryGetBuiltinModule(module, out module2))
			{
				module = module2;
			}
			return this.engine.DelayLoadingModule(module, (IEngineHost engineHost) => DependencyCompiler.GetDelayedModule(engineHost, sectionName, module.Name, compileOptions));
		}

		// Token: 0x0600B552 RID: 46418 RVA: 0x0024D252 File Offset: 0x0024B452
		private bool TryGetBuiltinModule(IModule module, out IModule tmp)
		{
			if (module.Version == null && this.engine.TryGetModule(module.Name, out tmp))
			{
				return true;
			}
			tmp = null;
			return false;
		}

		// Token: 0x0600B553 RID: 46419 RVA: 0x0024D278 File Offset: 0x0024B478
		private static IModule GetDelayedModule(IEngineHost engineHost, string sectionName, string moduleName, CompileOptions compileOptions)
		{
			DependencyCompiler dependencyCompiler = engineHost.QueryService<DependencyCompiler>();
			List<IError> list = new List<IError>();
			if (dependencyCompiler != null)
			{
				return dependencyCompiler.GetModule(sectionName, moduleName, compileOptions, list).Module;
			}
			return null;
		}

		// Token: 0x0600B554 RID: 46420 RVA: 0x0024D2A8 File Offset: 0x0024B4A8
		private DependencyCompiler.CachedDocument GetModule(string referringModule, string name, CompileOptions options, List<IError> errors)
		{
			DependencyCompiler.CachedDocument cacheEntry;
			if (!DependencyCompiler.libraryCache.TryGetValue(name, out cacheEntry))
			{
				cacheEntry = this.LoadModule(referringModule, name, options, errors);
				if (cacheEntry == null)
				{
					return null;
				}
				if (!cacheEntry.HasDynamicDependencies)
				{
					DependencyCompiler.libraryCache.Add(name, cacheEntry);
					cacheEntry.AddRefcounts();
				}
			}
			errors.AddRange(cacheEntry.Errors);
			if (cacheEntry.Module != null)
			{
				this.RegisterDataSources(cacheEntry.Module);
			}
			if (cacheEntry.IsDynamicModule)
			{
				IPersistentObjectCache persistentObjectCache = this.EngineHost.QueryService<ICacheSets>().Data.PersistentObjectCache;
				string text = "DependencyCompiler/" + cacheEntry.LoadVersion + "/" + name;
				object obj;
				if (persistentObjectCache.TryGetValue(text, DateTime.MinValue, null, (Stream s) => DependencyCompiler.DynamicMemberCompiler.Deserialize(s, this, options, cacheEntry), out obj))
				{
					cacheEntry = (DependencyCompiler.CachedDocument)obj;
				}
				else
				{
					DependencyCompiler.DynamicWrapper wrapper = cacheEntry.GeneratorData.GenerateDynamicWrapper(this.engine, this.EngineHost, cacheEntry, errors);
					cacheEntry = DependencyCompiler.DynamicMemberCompiler.Compile(this, options, cacheEntry, wrapper);
					persistentObjectCache.CommitValue(text, null, delegate(Stream s, object o)
					{
						DependencyCompiler.DynamicWrapper.Serialize(s, wrapper);
					}, cacheEntry);
				}
			}
			return cacheEntry;
		}

		// Token: 0x0600B555 RID: 46421 RVA: 0x0024D438 File Offset: 0x0024B638
		private DependencyCompiler.CachedDocument LoadModule(string referringModule, string name, CompileOptions options, List<IError> errors)
		{
			if (!this.loading.Add(name))
			{
				errors.Add(new DependencyCompiler.GenericError(Strings.Recursive_Section_Dependency(referringModule, name)));
				return null;
			}
			DependencyCompiler.CachedDocument cachedDocument2;
			try
			{
				string source = this.libraryService.GetSource(name);
				DependencyCompiler.CachedDocument cachedDocument;
				if (source == null)
				{
					List<IError> list = new List<IError>();
					IModule module;
					if (!this.TryGetBuiltinModule(name, out module))
					{
						module = null;
						list.Add(new DependencyCompiler.GenericError(Strings.Missing_Section_Dependency(referringModule, name)));
					}
					bool flag = false;
					cachedDocument = new DependencyCompiler.CachedDocument(null, null, module, null, list, null, flag, null);
				}
				else
				{
					cachedDocument = DependencyCompiler.DocumentCompiler.CompileSource(this, true, SegmentedString.New(source), options);
					if (cachedDocument.Module != null && cachedDocument.Module.Name != name)
					{
						cachedDocument.Errors.Add(new DependencyCompiler.GenericError(Strings.Section_Name_Mismatch(name, cachedDocument.Module.Name)));
						cachedDocument = new DependencyCompiler.CachedDocument(cachedDocument.Host, cachedDocument.Document, null, null, cachedDocument.Errors, cachedDocument.Dependencies, cachedDocument.HasDynamicDependencies, new DependencyCompiler.GeneratorData?(cachedDocument.GeneratorData));
					}
					cachedDocument.Host.Host = new TextDocumentHost(source);
				}
				cachedDocument2 = cachedDocument;
			}
			finally
			{
				this.loading.Remove(name);
			}
			return cachedDocument2;
		}

		// Token: 0x0600B556 RID: 46422 RVA: 0x0024D570 File Offset: 0x0024B770
		private bool TryGetBuiltinModule(string name, out IModule found)
		{
			Dictionary<string, IModule> dictionary = DependencyCompiler.builtinModuleCache;
			bool flag2;
			lock (dictionary)
			{
				if (DependencyCompiler.builtinModuleCache.TryGetValue(name, out found))
				{
					flag2 = true;
				}
				else if (!this.engine.TryGetModule(name, out found))
				{
					flag2 = false;
				}
				else
				{
					found = this.engine.LibraryCachingModule(found);
					DependencyCompiler.builtinModuleCache[name] = found;
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x0600B557 RID: 46423 RVA: 0x0024D5F0 File Offset: 0x0024B7F0
		private void RegisterDataSources(IModule module)
		{
			foreach (ResourceKindInfo resourceKindInfo in module.DataSources)
			{
				using (IHostTrace hostTrace = this.tracingService.CreateTrace("DependencyCompiler/RegisterDataSources", this.evaluationConstants, TraceEventType.Information, null))
				{
					hostTrace.Add("Kind", resourceKindInfo.Kind, false);
					bool flag = this.engine.UnregisterResourceKind(resourceKindInfo.Kind);
					hostTrace.Add("WasLoaded", flag, false);
					Exception ex;
					if (this.engine.TryRegisterResourceKind(resourceKindInfo, module.Name, out ex))
					{
						this.dataSourceKinds.Add(resourceKindInfo.Kind);
					}
					else
					{
						hostTrace.Add(ex, true);
					}
				}
			}
			if (module.Name != null)
			{
				this.moduleVersions[module.Name] = module.Version;
			}
		}

		// Token: 0x0600B558 RID: 46424 RVA: 0x0024D6DC File Offset: 0x0024B8DC
		private IModule BindConfiguration(IModule module, DependencyCompiler.DocumentConfig config)
		{
			return this.engine.OverrideEngineHostModule(module, (IEngineHost engineHost) => this.GetConfigurationSpecificEngineHost(engineHost, config));
		}

		// Token: 0x0600B559 RID: 46425 RVA: 0x0024D718 File Offset: 0x0024B918
		private IEngineHost GetConfigurationSpecificEngineHost(DependencyCompiler.DocumentConfig config)
		{
			IEngineHost engineHost;
			if (!this.engineHostByConfig.TryGetValue(config, out engineHost))
			{
				engineHost = this.MakeConfigurationSpecificEngineHost(this.engineHost, config);
				this.engineHostByConfig[config] = engineHost;
			}
			return engineHost;
		}

		// Token: 0x0600B55A RID: 46426 RVA: 0x0024D754 File Offset: 0x0024B954
		private IEngineHost MakeConfigurationSpecificEngineHost(IEngineHost engineHost, DependencyCompiler.DocumentConfig config)
		{
			ArrayBuilder<IEngineHost> arrayBuilder = new ArrayBuilder<IEngineHost>(3);
			config.AddServices(this, engineHost, ref arrayBuilder);
			arrayBuilder.Add(engineHost);
			return new CompositeEngineHost(arrayBuilder.ToArray());
		}

		// Token: 0x0600B55B RID: 46427 RVA: 0x0024D788 File Offset: 0x0024B988
		private static IEngineHost OverrideCultureService(IEngineHost engineHost, string culture)
		{
			return new SimpleEngineHost<ICultureService>(new OverrideDefaultCultureService(engineHost.QueryService<ICultureService>(), culture));
		}

		// Token: 0x0600B55C RID: 46428 RVA: 0x0024D79C File Offset: 0x0024B99C
		private IEngineHost OverrideTimeZoneService(IEngineHost engineHost, string timeZone)
		{
			ITimeZoneService timeZoneService = engineHost.QueryService<ITimeZoneService>();
			ITimeZone timeZone2;
			if (!timeZoneService.TryGetTimeZone(timeZone, out timeZone2))
			{
				throw this.engine.Exception(this.engine.ExceptionRecord(this.engine.Text("Expression.Error"), this.engine.Text(Strings.InvalidTimeZone(timeZone)), this.engine.Text(timeZone)));
			}
			return new SimpleEngineHost<ITimeZoneService>(new OverrideDefaultTimeZoneService(timeZoneService, timeZone2));
		}

		// Token: 0x0600B55D RID: 46429 RVA: 0x0024D80C File Offset: 0x0024BA0C
		private IEngineHost GetConfigurationSpecificEngineHost(IEngineHost engineHost, DependencyCompiler.DocumentConfig config)
		{
			DependencyCompiler dependencyCompiler = engineHost.QueryService<DependencyCompiler>();
			if (dependencyCompiler == null)
			{
				return this.MakeConfigurationSpecificEngineHost(engineHost, config);
			}
			return dependencyCompiler.GetConfigurationSpecificEngineHost(config);
		}

		// Token: 0x0600B55E RID: 46430 RVA: 0x0024D833 File Offset: 0x0024BA33
		private static void ReduceRefcounts<K, V>(K cacheKey, V cacheEntry) where V : DependencyCompiler.ItemWithDependencies
		{
			cacheEntry.RemoveRefcounts();
		}

		// Token: 0x0600B55F RID: 46431 RVA: 0x0024D840 File Offset: 0x0024BA40
		private static void RemoveDependents<K, V>(LruCache<K, V> cache, HashSet<string> changed) where K : IEquatable<K> where V : DependencyCompiler.ItemWithDependencies
		{
			Func<string, bool> <>9__0;
			foreach (KeyValuePair<K, V> keyValuePair in cache.Contents)
			{
				IEnumerable<string> dependencies = keyValuePair.Value.Dependencies;
				Func<string, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (string m) => changed.Contains(m));
				}
				if (dependencies.Any(func))
				{
					cache.Remove(keyValuePair.Key);
				}
			}
		}

		// Token: 0x0600B560 RID: 46432 RVA: 0x0024D8C0 File Offset: 0x0024BAC0
		// Note: this type is marked as 'beforefieldinit'.
		static DependencyCompiler()
		{
			IModule[] array = new ModuleStub[]
			{
				new ModuleStub("Legacy", null, Array.Empty<string>())
			};
			DependencyCompiler.legacyModule = array;
			DependencyCompiler.mostRecentLegacyDependenciesKey = new DependencyCompiler.LegacyLibraryKey(new IModule[0]);
		}

		// Token: 0x04005C93 RID: 23699
		private static readonly LruCache<DependencyCompiler.CachedDocumentKey, DependencyCompiler.CachedDocument> queryCache = new LruCache<DependencyCompiler.CachedDocumentKey, DependencyCompiler.CachedDocument>(16, new Action<DependencyCompiler.CachedDocumentKey, DependencyCompiler.CachedDocument>(DependencyCompiler.ReduceRefcounts<DependencyCompiler.CachedDocumentKey, DependencyCompiler.CachedDocument>));

		// Token: 0x04005C94 RID: 23700
		private static readonly LruCache<string, DependencyCompiler.CachedDocument> libraryCache = new LruCache<string, DependencyCompiler.CachedDocument>(50, new Action<string, DependencyCompiler.CachedDocument>(DependencyCompiler.ReduceRefcounts<string, DependencyCompiler.CachedDocument>));

		// Token: 0x04005C95 RID: 23701
		private static readonly LruCache<DependencyCompiler.LegacyLibraryKey, DependencyCompiler.LegacyLibrary> legacyLibraryCache = new LruCache<DependencyCompiler.LegacyLibraryKey, DependencyCompiler.LegacyLibrary>(4, new Action<DependencyCompiler.LegacyLibraryKey, DependencyCompiler.LegacyLibrary>(DependencyCompiler.ReduceRefcounts<DependencyCompiler.LegacyLibraryKey, DependencyCompiler.LegacyLibrary>));

		// Token: 0x04005C96 RID: 23702
		private static readonly Dictionary<string, IModule> builtinModuleCache = new Dictionary<string, IModule>();

		// Token: 0x04005C97 RID: 23703
		private static readonly Dictionary<string, DependencyCompiler.ModuleReference> cachedVersions = new Dictionary<string, DependencyCompiler.ModuleReference>();

		// Token: 0x04005C98 RID: 23704
		private static readonly IModule[] legacyModule;

		// Token: 0x04005C99 RID: 23705
		private static DependencyCompiler.LegacyLibraryKey mostRecentLegacyDependenciesKey;

		// Token: 0x04005C9A RID: 23706
		private readonly IEngine engine;

		// Token: 0x04005C9B RID: 23707
		private readonly IEngineHost engineHost;

		// Token: 0x04005C9C RID: 23708
		private readonly DependencyCompiler.DocumentConfig preferredConfig;

		// Token: 0x04005C9D RID: 23709
		private readonly ILibraryService libraryService;

		// Token: 0x04005C9E RID: 23710
		private readonly ITracingService tracingService;

		// Token: 0x04005C9F RID: 23711
		private readonly IEvaluationConstants evaluationConstants;

		// Token: 0x04005CA0 RID: 23712
		private readonly HashSet<string> loading;

		// Token: 0x04005CA1 RID: 23713
		private readonly IModule[] legacyDependencies;

		// Token: 0x04005CA2 RID: 23714
		private readonly DependencyCompiler.LegacyLibraryKey legacyDependenciesKey;

		// Token: 0x04005CA3 RID: 23715
		private readonly Dictionary<DependencyCompiler.DocumentConfig, IEngineHost> engineHostByConfig;

		// Token: 0x04005CA4 RID: 23716
		private readonly HashSet<string> dataSourceKinds;

		// Token: 0x04005CA5 RID: 23717
		private readonly Dictionary<string, string> moduleVersions;

		// Token: 0x02001C6D RID: 7277
		private struct DocumentConfig : IEquatable<DependencyCompiler.DocumentConfig>
		{
			// Token: 0x0600B561 RID: 46433 RVA: 0x0024D958 File Offset: 0x0024BB58
			public DocumentConfig(IPackageSectionConfig config)
			{
				this.culture = ((config != null) ? config.Culture : null);
				this.timeZone = ((config != null) ? config.TimeZone : null);
			}

			// Token: 0x17002D4E RID: 11598
			// (get) Token: 0x0600B562 RID: 46434 RVA: 0x0024D97E File Offset: 0x0024BB7E
			public string Culture
			{
				get
				{
					return this.culture;
				}
			}

			// Token: 0x17002D4F RID: 11599
			// (get) Token: 0x0600B563 RID: 46435 RVA: 0x0024D986 File Offset: 0x0024BB86
			public string TimeZone
			{
				get
				{
					return this.timeZone;
				}
			}

			// Token: 0x0600B564 RID: 46436 RVA: 0x0024D990 File Offset: 0x0024BB90
			public override int GetHashCode()
			{
				string text = this.culture;
				int num = ((text != null) ? text.GetHashCode() : 0);
				string text2 = this.timeZone;
				int num2 = ((text2 != null) ? text2.GetHashCode() : 0);
				return num ^ num2;
			}

			// Token: 0x0600B565 RID: 46437 RVA: 0x0024D9C4 File Offset: 0x0024BBC4
			public override bool Equals(object obj)
			{
				DependencyCompiler.DocumentConfig? documentConfig = obj as DependencyCompiler.DocumentConfig?;
				return documentConfig != null && documentConfig.Value.Equals(this);
			}

			// Token: 0x0600B566 RID: 46438 RVA: 0x0024D9FD File Offset: 0x0024BBFD
			public bool Equals(DependencyCompiler.DocumentConfig other)
			{
				return this.culture == other.culture && this.timeZone == other.timeZone;
			}

			// Token: 0x0600B567 RID: 46439 RVA: 0x0024DA25 File Offset: 0x0024BC25
			public void AddServices(DependencyCompiler compiler, IEngineHost engineHost, ref ArrayBuilder<IEngineHost> builder)
			{
				if (!string.IsNullOrEmpty(this.culture))
				{
					builder.Add(DependencyCompiler.OverrideCultureService(engineHost, this.culture));
				}
				if (this.timeZone != null)
				{
					builder.Add(compiler.OverrideTimeZoneService(engineHost, this.timeZone));
				}
			}

			// Token: 0x0600B568 RID: 46440 RVA: 0x0024DA64 File Offset: 0x0024BC64
			public bool RequiresOverride(IPackageSectionConfig config, out DependencyCompiler.DocumentConfig documentConfig)
			{
				documentConfig = new DependencyCompiler.DocumentConfig(config);
				return (documentConfig.culture != null && documentConfig.culture != this.culture) || (documentConfig.timeZone != null && documentConfig.timeZone != this.timeZone);
			}

			// Token: 0x04005CA6 RID: 23718
			private readonly string culture;

			// Token: 0x04005CA7 RID: 23719
			private readonly string timeZone;
		}

		// Token: 0x02001C6E RID: 7278
		private abstract class DocumentCompiler
		{
			// Token: 0x0600B569 RID: 46441 RVA: 0x0024DAB5 File Offset: 0x0024BCB5
			protected DocumentCompiler(DependencyCompiler compiler, CompileOptions compileOptions)
			{
				this.compiler = compiler;
				this.compileOptions = compileOptions;
				this.errors = new List<IError>();
			}

			// Token: 0x0600B56A RID: 46442 RVA: 0x0024DAD8 File Offset: 0x0024BCD8
			public static DependencyCompiler.CachedDocument CompileSource(DependencyCompiler compiler, bool isLibrary, SegmentedString text, CompileOptions compileOptions)
			{
				DependencyCompiler.CachedDocumentHost cachedDocumentHost = new DependencyCompiler.CachedDocumentHost();
				List<IError> list = new List<IError>();
				IDocument document = DependencyCompiler.DocumentCompiler.Parse(compiler.engine, cachedDocumentHost, text, list);
				DependencyCompiler.DocumentCompiler documentCompiler;
				if (document.Kind == DocumentKind.Expression)
				{
					documentCompiler = (isLibrary ? DependencyCompiler.DocumentCompiler.SectionLibraryCompiler.NewExpressionDocument(compiler, compileOptions, (IExpressionDocument)document) : new DependencyCompiler.DocumentCompiler.ExpressionQuery(compiler, compileOptions, (IExpressionDocument)document));
				}
				else
				{
					ISectionDocument sectionDocument = (ISectionDocument)document;
					if (isLibrary)
					{
						ISectionDocument sectionDocument2;
						DependencyCompiler.GeneratorData generatorData;
						if (DependencyCompiler.GeneratorData.TryCreateCompiler(compiler.engine, sectionDocument, list, out sectionDocument2, out generatorData))
						{
							sectionDocument = sectionDocument2;
						}
						documentCompiler = new DependencyCompiler.DocumentCompiler.SectionLibraryCompiler(compiler, compileOptions, sectionDocument, generatorData);
					}
					else
					{
						documentCompiler = new DependencyCompiler.DocumentCompiler.SectionQueryCompiler(compiler, compileOptions, sectionDocument);
					}
				}
				documentCompiler.errors.AddRange(list);
				return documentCompiler.Compile(cachedDocumentHost);
			}

			// Token: 0x17002D50 RID: 11600
			// (get) Token: 0x0600B56B RID: 46443 RVA: 0x0024DB79 File Offset: 0x0024BD79
			protected ILibraryService LibraryService
			{
				get
				{
					return this.compiler.libraryService;
				}
			}

			// Token: 0x17002D51 RID: 11601
			// (get) Token: 0x0600B56C RID: 46444 RVA: 0x0024DB86 File Offset: 0x0024BD86
			protected IEngine Engine
			{
				get
				{
					return this.compiler.engine;
				}
			}

			// Token: 0x0600B56D RID: 46445
			protected abstract DependencyCompiler.CachedDocument Compile(DependencyCompiler.CachedDocumentHost documentHost);

			// Token: 0x0600B56E RID: 46446 RVA: 0x0024DB93 File Offset: 0x0024BD93
			protected void LogError(IError error)
			{
				this.errors.Add(error);
			}

			// Token: 0x0600B56F RID: 46447 RVA: 0x0024DBA1 File Offset: 0x0024BDA1
			protected IModule Compile(IDocument document)
			{
				return this.Engine.Compile(document, this.Engine.EmptyRecord, this.compileOptions, new Action<IError>(this.LogError));
			}

			// Token: 0x0600B570 RID: 46448 RVA: 0x0024DBCC File Offset: 0x0024BDCC
			protected static IDocument Parse(IEngine engine, IDocumentHost documentHost, SegmentedString text, List<IError> log)
			{
				return engine.Parse(engine.Tokenize(text), documentHost, delegate(IError error)
				{
					log.Add(error);
				});
			}

			// Token: 0x04005CA8 RID: 23720
			protected readonly DependencyCompiler compiler;

			// Token: 0x04005CA9 RID: 23721
			protected readonly CompileOptions compileOptions;

			// Token: 0x04005CAA RID: 23722
			protected readonly List<IError> errors;

			// Token: 0x02001C6F RID: 7279
			private sealed class ExpressionQuery : DependencyCompiler.DocumentCompiler
			{
				// Token: 0x0600B571 RID: 46449 RVA: 0x0024DC00 File Offset: 0x0024BE00
				public ExpressionQuery(DependencyCompiler compiler, CompileOptions compileOptions, IExpressionDocument document)
					: base(compiler, compileOptions)
				{
					this.document = document;
				}

				// Token: 0x0600B572 RID: 46450 RVA: 0x0024DC14 File Offset: 0x0024BE14
				protected override DependencyCompiler.CachedDocument Compile(DependencyCompiler.CachedDocumentHost documentHost)
				{
					return new DependencyCompiler.CachedDocument(documentHost, this.document, base.Compile(this.document), null, this.errors, null, false, null);
				}

				// Token: 0x04005CAB RID: 23723
				private readonly IExpressionDocument document;
			}

			// Token: 0x02001C70 RID: 7280
			private abstract class SectionCompiler : DependencyCompiler.DocumentCompiler
			{
				// Token: 0x0600B573 RID: 46451 RVA: 0x0024DC4B File Offset: 0x0024BE4B
				public SectionCompiler(DependencyCompiler compiler, CompileOptions compileOptions, ISectionDocument document, DependencyCompiler.GeneratorData? generatorData = null)
					: base(compiler, compileOptions)
				{
					this.document = document;
					this.dependencies = new HashSet<string>();
					this.generatorData = generatorData;
				}

				// Token: 0x17002D52 RID: 11602
				// (get) Token: 0x0600B574 RID: 46452
				protected abstract LinkOptions LinkOptions { get; }

				// Token: 0x17002D53 RID: 11603
				// (get) Token: 0x0600B575 RID: 46453 RVA: 0x0024DC70 File Offset: 0x0024BE70
				private bool DeclaresDependencies
				{
					get
					{
						if (this.document.Section.Attribute != null)
						{
							return this.document.Section.Attribute.Members.Any((VariableInitializer member) => member.Name == "Requires");
						}
						return false;
					}
				}

				// Token: 0x0600B576 RID: 46454 RVA: 0x0024DCCC File Offset: 0x0024BECC
				protected override DependencyCompiler.CachedDocument Compile(DependencyCompiler.CachedDocumentHost documentHost)
				{
					IPackageSectionConfig sectionMetadata = this.document.Section.GetSectionMetadata(null);
					bool flag = false;
					IModule module = (this.DeclaresDependencies ? this.GetDependenciesLibrary(sectionMetadata, out flag) : this.GetLegacyLibrary());
					IModule module2 = this.CompileAndLink(module);
					DependencyCompiler.DocumentConfig documentConfig;
					if (this.compiler.preferredConfig.RequiresOverride(sectionMetadata, out documentConfig))
					{
						module2 = this.compiler.BindConfiguration(module2, documentConfig);
					}
					return new DependencyCompiler.CachedDocument(documentHost, this.document, module2, module, this.errors, this.dependencies, flag, this.generatorData);
				}

				// Token: 0x0600B577 RID: 46455
				protected abstract IModule GetLegacyLibrary();

				// Token: 0x0600B578 RID: 46456 RVA: 0x0024DD58 File Offset: 0x0024BF58
				protected virtual IModule GetDependenciesLibrary(IPackageSectionConfig config, out bool hasDynamicDependencies)
				{
					return this.compiler.GetLibrary(this.document.Section.SectionName, config.Dependencies.Select((KeyValuePair<string, VersionRange> kvp) => kvp.Key), this.compileOptions, this.errors, this.dependencies, out hasDynamicDependencies);
				}

				// Token: 0x0600B579 RID: 46457 RVA: 0x0024DDC2 File Offset: 0x0024BFC2
				protected virtual IModule CompileAndLink(IModule library)
				{
					return base.Engine.Link(new IModule[]
					{
						base.Compile(this.document),
						library
					}, new Action<IError>(base.LogError), this.LinkOptions);
				}

				// Token: 0x04005CAC RID: 23724
				protected readonly ISectionDocument document;

				// Token: 0x04005CAD RID: 23725
				protected readonly HashSet<string> dependencies;

				// Token: 0x04005CAE RID: 23726
				private readonly DependencyCompiler.GeneratorData? generatorData;
			}

			// Token: 0x02001C72 RID: 7282
			private sealed class SectionLibraryCompiler : DependencyCompiler.DocumentCompiler.SectionCompiler
			{
				// Token: 0x0600B57E RID: 46462 RVA: 0x0024DE27 File Offset: 0x0024C027
				public SectionLibraryCompiler(DependencyCompiler compiler, CompileOptions compileOptions, ISectionDocument document, DependencyCompiler.GeneratorData generatorData)
					: base(compiler, compileOptions, document, new DependencyCompiler.GeneratorData?(generatorData))
				{
				}

				// Token: 0x0600B57F RID: 46463 RVA: 0x0024DE3C File Offset: 0x0024C03C
				public static DependencyCompiler.DocumentCompiler NewExpressionDocument(DependencyCompiler compiler, CompileOptions compileOptions, IExpressionDocument document)
				{
					ISectionDocument sectionDocument;
					Exception ex;
					if (!compiler.engine.TryWrapExpressionDataSource(document, compiler.libraryService, out sectionDocument, out ex))
					{
						DependencyCompiler.DocumentCompiler.ExpressionQuery expressionQuery = new DependencyCompiler.DocumentCompiler.ExpressionQuery(compiler, compileOptions, document);
						expressionQuery.LogError(new DependencyCompiler.GenericError(ex.Message));
						return expressionQuery;
					}
					return new DependencyCompiler.DocumentCompiler.SectionLibraryCompiler(compiler, compileOptions, sectionDocument, default(DependencyCompiler.GeneratorData));
				}

				// Token: 0x17002D54 RID: 11604
				// (get) Token: 0x0600B580 RID: 46464 RVA: 0x00075E2C File Offset: 0x0007402C
				protected override LinkOptions LinkOptions
				{
					get
					{
						return LinkOptions.ExportFirstModule | LinkOptions.ExplicitEnvironment;
					}
				}

				// Token: 0x0600B581 RID: 46465 RVA: 0x0024DE8C File Offset: 0x0024C08C
				protected override IModule GetLegacyLibrary()
				{
					this.loadAsDataSource = true;
					bool flag;
					return this.compiler.GetLibrary(this.document.Section.SectionName, Extension.Modules.Concat(new string[] { "Legacy" }), this.compileOptions, this.errors, this.dependencies, out flag);
				}

				// Token: 0x0600B582 RID: 46466 RVA: 0x0024DEEC File Offset: 0x0024C0EC
				protected override IModule GetDependenciesLibrary(IPackageSectionConfig config, out bool hasDynamicDependencies)
				{
					string key = config.Dependencies.FirstOrDefault((KeyValuePair<string, VersionRange> kvp) => Extension.IsExtensionOnly(kvp.Key)).Key;
					this.loadAsDataSource = key != null;
					return base.GetDependenciesLibrary(config, out hasDynamicDependencies);
				}

				// Token: 0x0600B583 RID: 46467 RVA: 0x0024DF40 File Offset: 0x0024C140
				protected override IModule CompileAndLink(IModule library)
				{
					if (!this.loadAsDataSource)
					{
						return base.Engine.LibraryCachingModule(base.CompileAndLink(library));
					}
					IModule module;
					if (base.Engine.TryCompileDataSource(this.document, library, base.LibraryService, this.compileOptions, new Action<IError>(base.LogError), out module))
					{
						return module;
					}
					return library;
				}

				// Token: 0x04005CB2 RID: 23730
				private bool loadAsDataSource;
			}

			// Token: 0x02001C74 RID: 7284
			private sealed class SectionQueryCompiler : DependencyCompiler.DocumentCompiler.SectionCompiler
			{
				// Token: 0x0600B587 RID: 46471 RVA: 0x0024DFB4 File Offset: 0x0024C1B4
				public SectionQueryCompiler(DependencyCompiler compiler, CompileOptions compileOptions, ISectionDocument document)
					: base(compiler, compileOptions, document, null)
				{
				}

				// Token: 0x17002D55 RID: 11605
				// (get) Token: 0x0600B588 RID: 46472 RVA: 0x0000240C File Offset: 0x0000060C
				protected override LinkOptions LinkOptions
				{
					get
					{
						return LinkOptions.ExportFirstModule | LinkOptions.IgnoreUnresolvedImports;
					}
				}

				// Token: 0x0600B589 RID: 46473 RVA: 0x0024DFD4 File Offset: 0x0024C1D4
				protected override IModule GetLegacyLibrary()
				{
					this.dependencies.UnionWith(this.compiler.legacyDependencies.Select((IModule m) => m.Name));
					return this.compiler.GetLegacyLibrary(this.document.Section.SectionName, this.compileOptions, this.errors);
				}

				// Token: 0x0600B58A RID: 46474 RVA: 0x0024E048 File Offset: 0x0024C248
				protected override IModule GetDependenciesLibrary(IPackageSectionConfig config, out bool hasDynamicDependencies)
				{
					string key = config.Dependencies.FirstOrDefault((KeyValuePair<string, VersionRange> kvp) => Extension.IsExtensionOnly(kvp.Key)).Key;
					if (key != null)
					{
						this.errors.Add(new DependencyCompiler.GenericError(Strings.Illegal_Module_Reference(key)));
					}
					return base.GetDependenciesLibrary(config, out hasDynamicDependencies);
				}
			}
		}

		// Token: 0x02001C77 RID: 7287
		private sealed class DynamicMemberCompiler : DependencyCompiler.DocumentCompiler
		{
			// Token: 0x0600B591 RID: 46481 RVA: 0x0024E0CC File Offset: 0x0024C2CC
			private DynamicMemberCompiler(DependencyCompiler compiler, CompileOptions compileOptions, DependencyCompiler.CachedDocument innerModule, DependencyCompiler.DynamicWrapper dynamicWrapper)
				: base(compiler, compileOptions)
			{
				this.innerModule = innerModule;
				SegmentedString segmentedString = SegmentedString.New(dynamicWrapper.OuterCode);
				this.document = DependencyCompiler.DocumentCompiler.Parse(base.Engine, new TextDocumentHost(segmentedString), segmentedString, this.errors);
				this.innerModuleName = dynamicWrapper.InnerModuleName;
			}

			// Token: 0x0600B592 RID: 46482 RVA: 0x0024E120 File Offset: 0x0024C320
			public static DependencyCompiler.CachedDocument Compile(DependencyCompiler compiler, CompileOptions compileOptions, DependencyCompiler.CachedDocument innerModule, DependencyCompiler.DynamicWrapper dynamicWrapper)
			{
				if (dynamicWrapper.OuterCode == null)
				{
					return innerModule;
				}
				return new DependencyCompiler.DynamicMemberCompiler(compiler, compileOptions, innerModule, dynamicWrapper).Compile(new DependencyCompiler.CachedDocumentHost
				{
					Host = new TextDocumentHost(dynamicWrapper.OuterCode)
				});
			}

			// Token: 0x0600B593 RID: 46483 RVA: 0x0024E150 File Offset: 0x0024C350
			public static DependencyCompiler.CachedDocument Deserialize(Stream stream, DependencyCompiler compiler, CompileOptions compileOptions, DependencyCompiler.CachedDocument innerModule)
			{
				return DependencyCompiler.DynamicMemberCompiler.Compile(compiler, compileOptions, innerModule, DependencyCompiler.DynamicWrapper.Deserialize(stream));
			}

			// Token: 0x0600B594 RID: 46484 RVA: 0x0024E160 File Offset: 0x0024C360
			protected override DependencyCompiler.CachedDocument Compile(DependencyCompiler.CachedDocumentHost documentHost)
			{
				IModule module = base.Engine.LibraryCachingModule(base.Engine.Link(new IModule[]
				{
					base.Compile(this.document),
					base.Engine.InternalizeModule(this.innerModule.Module, this.innerModuleName),
					this.innerModule.Library
				}, new Action<IError>(base.LogError), LinkOptions.ExportFirstModule | LinkOptions.ExplicitEnvironment));
				return new DependencyCompiler.CachedDocument(documentHost, this.document, module, this.innerModule.Library, this.errors, this.innerModule.Dependencies, true, null);
			}

			// Token: 0x04005CB9 RID: 23737
			private readonly DependencyCompiler.CachedDocument innerModule;

			// Token: 0x04005CBA RID: 23738
			private readonly IDocument document;

			// Token: 0x04005CBB RID: 23739
			private readonly string innerModuleName;
		}

		// Token: 0x02001C78 RID: 7288
		private struct GeneratorData
		{
			// Token: 0x0600B595 RID: 46485 RVA: 0x0024E206 File Offset: 0x0024C406
			private GeneratorData(string generator, bool publicGenerator)
			{
				this.generator = generator;
				this.publicGenerator = publicGenerator;
			}

			// Token: 0x0600B596 RID: 46486 RVA: 0x0024E218 File Offset: 0x0024C418
			public static bool TryCreateCompiler(IEngine engine, ISectionDocument document, List<IError> errors, out ISectionDocument newDocument, out DependencyCompiler.GeneratorData generatorData)
			{
				if (document.Section.Attribute != null)
				{
					IExpression value = document.Section.Attribute.Members.FirstOrDefault((VariableInitializer vi) => vi.Name == "DynamicMemberGenerator").Value;
					if (value != null && value.Kind == ExpressionKind.Constant)
					{
						IValue value2 = ((IConstantExpression2)value).Value;
						if (value2.IsText)
						{
							string asString = value2.AsString;
							newDocument = DependencyCompiler.GeneratorData.MakePublic(engine, document, asString, errors);
							generatorData = new DependencyCompiler.GeneratorData(asString, document == newDocument);
							return true;
						}
					}
					if (value != null)
					{
						errors.Add(new DependencyCompiler.GenericError(Strings.Invalid_Dynamic_Module(document.Section.SectionName)));
					}
				}
				newDocument = null;
				generatorData = default(DependencyCompiler.GeneratorData);
				return false;
			}

			// Token: 0x17002D56 RID: 11606
			// (get) Token: 0x0600B597 RID: 46487 RVA: 0x0024E2E2 File Offset: 0x0024C4E2
			public bool IsDynamicModule
			{
				get
				{
					return this.generator != null;
				}
			}

			// Token: 0x0600B598 RID: 46488 RVA: 0x0024E2F0 File Offset: 0x0024C4F0
			public DependencyCompiler.DynamicWrapper GenerateDynamicWrapper(IEngine engine, IEngineHost engineHost, DependencyCompiler.CachedDocument innerModule, List<IError> errors)
			{
				Action<IError> action = delegate(IError error)
				{
					errors.Add(error);
				};
				ISectionDocument sectionDocument = (ISectionDocument)innerModule.Document;
				string text = string.Format(CultureInfo.InvariantCulture, "let table = {0}() in [Keys = List.First(List.Select(Table.Keys(table), each [Primary] = true)), Table = table]", engine.EscapeIdentifier(this.generator));
				IDocument document = engine.Parse(engine.Tokenize(text), new TextDocumentHost(text), action);
				IModule module = engine.Compile(document, engine.EmptyRecord, CompileOptions.None, action);
				IAssembly assembly = engine.Assemble(new IModule[] { module, innerModule.Module, innerModule.Library }, engine.EmptyRecord, engineHost, action);
				DependencyCompiler.DynamicWrapper dynamicWrapper;
				try
				{
					IRecordValue asRecord = engine.Invoke(assembly.Function, Array.Empty<IValue>()).AsRecord;
					IRecordValue asRecord2 = asRecord["Keys"].AsRecord;
					ITableValue asTable = asRecord["Table"].AsTable;
					IValue @null;
					if (!asTable.Type.TryGetMetaField("NavigationTable.NameColumn", out @null))
					{
						@null = engine.Null;
					}
					string asString = @null.AsString;
					IValue null2;
					if (!asTable.Type.TryGetMetaField("NavigationTable.DataColumn", out null2))
					{
						null2 = engine.Null;
					}
					string asString2 = null2.AsString;
					string text2 = Guid.NewGuid().ToString();
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					foreach (ISectionMember sectionMember in sectionDocument.Section.Members.Where((ISectionMember m) => m.Export))
					{
						if (this.publicGenerator || !(sectionMember.Name == this.generator))
						{
							dictionary[sectionMember.Name] = string.Format(CultureInfo.InvariantCulture, "{0}!{1}", engine.EscapeIdentifier(text2), engine.EscapeIdentifier(sectionMember.Name));
						}
					}
					string[] array = (from i in asRecord2["Columns"].AsList.GetEnumerable()
						select i.Value.AsString).ToArray<string>();
					foreach (IValueReference2 valueReference in asTable)
					{
						string asString3 = valueReference.Value.AsRecord[asString].AsString;
						string text3;
						if (dictionary.TryGetValue(asString3, out text3))
						{
							dictionary[asString3] = string.Format(CultureInfo.InvariantCulture, "error Error.Record(\"Expression.Error\", {0}, {1})", engine.EscapeString(Strings.Duplicate_Member_Definition), engine.EscapeString(asString3));
						}
						else
						{
							StringBuilder stringBuilder = new StringBuilder();
							foreach (string text4 in array)
							{
								if (stringBuilder.Length > 0)
								{
									stringBuilder.Append(", ");
								}
								stringBuilder.Append(engine.EscapeFieldIdentifier(text4));
								stringBuilder.Append("=");
								stringBuilder.Append(valueReference.Value.AsRecord[text4].ToSource());
							}
							dictionary[asString3] = string.Format(CultureInfo.InvariantCulture, "{0}!{1}(){{[{2}]}}[{3}]", new object[]
							{
								engine.EscapeIdentifier(text2),
								engine.EscapeIdentifier(this.generator),
								stringBuilder.ToString(),
								engine.EscapeFieldIdentifier(asString2)
							});
						}
					}
					StringBuilder stringBuilder2 = new StringBuilder();
					stringBuilder2.AppendFormat(CultureInfo.InvariantCulture, "section {0};\r\n\r\n", engine.EscapeIdentifier(sectionDocument.Section.SectionName));
					foreach (KeyValuePair<string, string> keyValuePair in dictionary)
					{
						stringBuilder2.AppendFormat(CultureInfo.InvariantCulture, "shared {0} = {1};\r\n", engine.EscapeIdentifier(keyValuePair.Key), keyValuePair.Value);
					}
					dynamicWrapper = default(DependencyCompiler.DynamicWrapper);
					dynamicWrapper.InnerModuleName = text2;
					dynamicWrapper.OuterCode = stringBuilder2.ToString();
					dynamicWrapper = dynamicWrapper;
				}
				catch (ValueException2 valueException)
				{
					action(new DependencyCompiler.GenericError(Strings.Invalid_Dynamic_Generator(sectionDocument.Section.SectionName, valueException.Message)));
					dynamicWrapper = default(DependencyCompiler.DynamicWrapper);
				}
				return dynamicWrapper;
			}

			// Token: 0x0600B599 RID: 46489 RVA: 0x0024E7B8 File Offset: 0x0024C9B8
			private static ISectionDocument MakePublic(IEngine engine, ISectionDocument document, string memberName, List<IError> errors)
			{
				ISectionMember sectionMember = document.Section.Members.FirstOrDefault((ISectionMember m) => m.Name == memberName);
				if (sectionMember == null || sectionMember.Export)
				{
					return document;
				}
				TokenRange range = sectionMember.Name.Range;
				SegmentedStringBuilder segmentedStringBuilder = SegmentedStringBuilder.New();
				SegmentedString segmentedText = document.Tokens.GetSegmentedText();
				segmentedStringBuilder.Append(segmentedText, 0, document.Tokens.GetOffset(range.Start));
				segmentedStringBuilder.Append("shared ");
				segmentedStringBuilder.Append(engine.EscapeIdentifier(memberName));
				segmentedStringBuilder.Append(segmentedText, document.Tokens.GetOffset(range.End) + document.Tokens.GetLength(range.End));
				SegmentedString segmentedString = segmentedStringBuilder.ToSegmentedString();
				return (ISectionDocument)engine.Parse(engine.Tokenize(segmentedString), new TextDocumentHost(segmentedString), delegate(IError e)
				{
					errors.Add(e);
				});
			}

			// Token: 0x04005CBC RID: 23740
			private readonly string generator;

			// Token: 0x04005CBD RID: 23741
			private readonly bool publicGenerator;
		}

		// Token: 0x02001C7C RID: 7292
		private abstract class ItemWithDependencies
		{
			// Token: 0x0600B5A4 RID: 46500 RVA: 0x0024E913 File Offset: 0x0024CB13
			protected ItemWithDependencies(HashSet<string> dependencies)
			{
				this.dependencies = dependencies ?? DependencyCompiler.ItemWithDependencies.noDependencies;
			}

			// Token: 0x17002D57 RID: 11607
			// (get) Token: 0x0600B5A5 RID: 46501 RVA: 0x0024E92B File Offset: 0x0024CB2B
			public HashSet<string> Dependencies
			{
				get
				{
					return this.dependencies;
				}
			}

			// Token: 0x0600B5A6 RID: 46502 RVA: 0x0024E934 File Offset: 0x0024CB34
			public void AddRefcounts()
			{
				foreach (string text in this.dependencies)
				{
					DependencyCompiler.ItemWithDependencies.AddRefcount(text);
				}
			}

			// Token: 0x0600B5A7 RID: 46503 RVA: 0x0024E984 File Offset: 0x0024CB84
			public void RemoveRefcounts()
			{
				foreach (string text in this.dependencies)
				{
					DependencyCompiler.ItemWithDependencies.RemoveRefcount(text);
				}
			}

			// Token: 0x0600B5A8 RID: 46504 RVA: 0x0024E9D4 File Offset: 0x0024CBD4
			private static void AddRefcount(string module)
			{
				DependencyCompiler.ModuleReference moduleReference;
				if (DependencyCompiler.cachedVersions.TryGetValue(module, out moduleReference))
				{
					DependencyCompiler.ModuleReference moduleReference2 = moduleReference;
					int referenceCount = moduleReference2.ReferenceCount;
					moduleReference2.ReferenceCount = referenceCount + 1;
					return;
				}
				DependencyCompiler.cachedVersions.Add(module, new DependencyCompiler.ModuleReference
				{
					ReferenceCount = 1
				});
			}

			// Token: 0x0600B5A9 RID: 46505 RVA: 0x0024EA18 File Offset: 0x0024CC18
			private static void RemoveRefcount(string module)
			{
				DependencyCompiler.ModuleReference moduleReference;
				if (DependencyCompiler.cachedVersions.TryGetValue(module, out moduleReference))
				{
					DependencyCompiler.ModuleReference moduleReference2 = moduleReference;
					int num = moduleReference2.ReferenceCount - 1;
					moduleReference2.ReferenceCount = num;
					if (num == 0)
					{
						DependencyCompiler.cachedVersions.Remove(module);
					}
				}
			}

			// Token: 0x04005CC5 RID: 23749
			private static readonly HashSet<string> noDependencies = new HashSet<string>();

			// Token: 0x04005CC6 RID: 23750
			private readonly HashSet<string> dependencies;
		}

		// Token: 0x02001C7D RID: 7293
		private sealed class CachedDocumentKey : IEquatable<DependencyCompiler.CachedDocumentKey>
		{
			// Token: 0x0600B5AB RID: 46507 RVA: 0x0024EA5F File Offset: 0x0024CC5F
			public CachedDocumentKey(SegmentedString text, CompileOptions options)
			{
				this.text = text;
				this.options = options;
			}

			// Token: 0x0600B5AC RID: 46508 RVA: 0x0024EA78 File Offset: 0x0024CC78
			public override int GetHashCode()
			{
				return this.text.GetHashCode();
			}

			// Token: 0x0600B5AD RID: 46509 RVA: 0x0024EA99 File Offset: 0x0024CC99
			public override bool Equals(object other)
			{
				return this.Equals(other as DependencyCompiler.CachedDocumentKey);
			}

			// Token: 0x0600B5AE RID: 46510 RVA: 0x0024EAA7 File Offset: 0x0024CCA7
			public bool Equals(DependencyCompiler.CachedDocumentKey other)
			{
				return other != null && this.text == other.text && this.options == other.options;
			}

			// Token: 0x04005CC7 RID: 23751
			private readonly SegmentedString text;

			// Token: 0x04005CC8 RID: 23752
			private readonly CompileOptions options;
		}

		// Token: 0x02001C7E RID: 7294
		private class CachedDocument : DependencyCompiler.ItemWithDependencies
		{
			// Token: 0x0600B5AF RID: 46511 RVA: 0x0024EAD0 File Offset: 0x0024CCD0
			public CachedDocument(DependencyCompiler.CachedDocumentHost host, IDocument document, IModule module, IModule library, List<IError> errors, HashSet<string> dependencies, bool hasDynamicDependencies, DependencyCompiler.GeneratorData? generatorData = null)
				: base(dependencies)
			{
				this.host = host;
				this.document = document;
				this.module = module;
				this.library = library;
				this.errors = errors;
				this.hasDynamicDependencies = hasDynamicDependencies;
				this.generatorData = generatorData.GetValueOrDefault();
			}

			// Token: 0x17002D58 RID: 11608
			// (get) Token: 0x0600B5B0 RID: 46512 RVA: 0x0024EB1F File Offset: 0x0024CD1F
			public DependencyCompiler.CachedDocumentHost Host
			{
				get
				{
					return this.host;
				}
			}

			// Token: 0x17002D59 RID: 11609
			// (get) Token: 0x0600B5B1 RID: 46513 RVA: 0x0024EB27 File Offset: 0x0024CD27
			public IDocument Document
			{
				get
				{
					return this.document;
				}
			}

			// Token: 0x17002D5A RID: 11610
			// (get) Token: 0x0600B5B2 RID: 46514 RVA: 0x0024EB2F File Offset: 0x0024CD2F
			public IModule Module
			{
				get
				{
					return this.module;
				}
			}

			// Token: 0x17002D5B RID: 11611
			// (get) Token: 0x0600B5B3 RID: 46515 RVA: 0x0024EB37 File Offset: 0x0024CD37
			public IModule Library
			{
				get
				{
					return this.library;
				}
			}

			// Token: 0x17002D5C RID: 11612
			// (get) Token: 0x0600B5B4 RID: 46516 RVA: 0x0024EB3F File Offset: 0x0024CD3F
			public string LoadVersion
			{
				get
				{
					return this.module.Version;
				}
			}

			// Token: 0x17002D5D RID: 11613
			// (get) Token: 0x0600B5B5 RID: 46517 RVA: 0x0024EB4C File Offset: 0x0024CD4C
			public List<IError> Errors
			{
				get
				{
					return this.errors;
				}
			}

			// Token: 0x17002D5E RID: 11614
			// (get) Token: 0x0600B5B6 RID: 46518 RVA: 0x0024EB54 File Offset: 0x0024CD54
			public bool IsDynamicModule
			{
				get
				{
					return this.generatorData.IsDynamicModule;
				}
			}

			// Token: 0x17002D5F RID: 11615
			// (get) Token: 0x0600B5B7 RID: 46519 RVA: 0x0024EB6F File Offset: 0x0024CD6F
			public bool HasDynamicDependencies
			{
				get
				{
					return this.hasDynamicDependencies;
				}
			}

			// Token: 0x17002D60 RID: 11616
			// (get) Token: 0x0600B5B8 RID: 46520 RVA: 0x0024EB77 File Offset: 0x0024CD77
			public DependencyCompiler.GeneratorData GeneratorData
			{
				get
				{
					return this.generatorData;
				}
			}

			// Token: 0x04005CC9 RID: 23753
			private readonly DependencyCompiler.CachedDocumentHost host;

			// Token: 0x04005CCA RID: 23754
			private readonly IDocument document;

			// Token: 0x04005CCB RID: 23755
			private readonly IModule module;

			// Token: 0x04005CCC RID: 23756
			private readonly IModule library;

			// Token: 0x04005CCD RID: 23757
			private readonly List<IError> errors;

			// Token: 0x04005CCE RID: 23758
			private readonly bool hasDynamicDependencies;

			// Token: 0x04005CCF RID: 23759
			private readonly DependencyCompiler.GeneratorData generatorData;
		}

		// Token: 0x02001C7F RID: 7295
		private sealed class CachedDocumentHost : ICacheableDocumentHost, IDocumentHost, ITranslateSourceLocation
		{
			// Token: 0x17002D61 RID: 11617
			// (set) Token: 0x0600B5B9 RID: 46521 RVA: 0x0024EB7F File Offset: 0x0024CD7F
			public IDocumentHost Host
			{
				set
				{
					this.host = value;
				}
			}

			// Token: 0x17002D62 RID: 11618
			// (get) Token: 0x0600B5BA RID: 46522 RVA: 0x0024EB88 File Offset: 0x0024CD88
			public string UniqueID
			{
				get
				{
					return this.host.UniqueID;
				}
			}

			// Token: 0x17002D63 RID: 11619
			// (get) Token: 0x0600B5BB RID: 46523 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public object CacheIdentity
			{
				get
				{
					return this;
				}
			}

			// Token: 0x0600B5BC RID: 46524 RVA: 0x0024EB98 File Offset: 0x0024CD98
			public SourceLocation TranslateSourceLocation(SourceLocation location)
			{
				ITranslateSourceLocation translateSourceLocation = this.host as ITranslateSourceLocation;
				if (translateSourceLocation != null)
				{
					location = translateSourceLocation.TranslateSourceLocation(location);
				}
				return location;
			}

			// Token: 0x04005CD0 RID: 23760
			private IDocumentHost host;
		}

		// Token: 0x02001C80 RID: 7296
		private sealed class LegacyLibraryKey : IEquatable<DependencyCompiler.LegacyLibraryKey>
		{
			// Token: 0x0600B5BE RID: 46526 RVA: 0x0024EBC0 File Offset: 0x0024CDC0
			public LegacyLibraryKey(IModule[] modules)
			{
				this.components = new HashSet<string>();
				foreach (IModule module in modules)
				{
					this.components.Add(module.Name);
					this.hashCode ^= module.Name.GetHashCode();
				}
			}

			// Token: 0x17002D64 RID: 11620
			// (get) Token: 0x0600B5BF RID: 46527 RVA: 0x0024EC1C File Offset: 0x0024CE1C
			public HashSet<string> Components
			{
				get
				{
					return this.components;
				}
			}

			// Token: 0x0600B5C0 RID: 46528 RVA: 0x0024EC24 File Offset: 0x0024CE24
			public override int GetHashCode()
			{
				return this.hashCode;
			}

			// Token: 0x0600B5C1 RID: 46529 RVA: 0x0024EC2C File Offset: 0x0024CE2C
			public override bool Equals(object other)
			{
				return this.Equals(other as DependencyCompiler.LegacyLibraryKey);
			}

			// Token: 0x0600B5C2 RID: 46530 RVA: 0x0024EC3A File Offset: 0x0024CE3A
			public bool Equals(DependencyCompiler.LegacyLibraryKey other)
			{
				return this.hashCode == other.hashCode && this.components.SetEquals(other.components);
			}

			// Token: 0x04005CD1 RID: 23761
			private readonly HashSet<string> components;

			// Token: 0x04005CD2 RID: 23762
			private readonly int hashCode;
		}

		// Token: 0x02001C81 RID: 7297
		private sealed class LegacyLibrary : DependencyCompiler.ItemWithDependencies
		{
			// Token: 0x0600B5C3 RID: 46531 RVA: 0x0024EC5D File Offset: 0x0024CE5D
			public LegacyLibrary(IModule library, HashSet<string> dependencies)
				: base(dependencies)
			{
				this.library = library;
			}

			// Token: 0x17002D65 RID: 11621
			// (get) Token: 0x0600B5C4 RID: 46532 RVA: 0x0024EC6D File Offset: 0x0024CE6D
			public IModule Library
			{
				get
				{
					return this.library;
				}
			}

			// Token: 0x04005CD3 RID: 23763
			private readonly IModule library;
		}

		// Token: 0x02001C82 RID: 7298
		private sealed class GenericError : IError
		{
			// Token: 0x0600B5C5 RID: 46533 RVA: 0x0024EC75 File Offset: 0x0024CE75
			public GenericError(string message)
			{
				this.message = message;
			}

			// Token: 0x17002D66 RID: 11622
			// (get) Token: 0x0600B5C6 RID: 46534 RVA: 0x0024EC84 File Offset: 0x0024CE84
			public ErrorRange ErrorRange
			{
				get
				{
					return new ErrorRange(0, 0);
				}
			}

			// Token: 0x17002D67 RID: 11623
			// (get) Token: 0x0600B5C7 RID: 46535 RVA: 0x00142610 File Offset: 0x00140810
			public ErrorKind Kind
			{
				get
				{
					return ErrorKind.Generic;
				}
			}

			// Token: 0x17002D68 RID: 11624
			// (get) Token: 0x0600B5C8 RID: 46536 RVA: 0x001D08D8 File Offset: 0x001CEAD8
			public SourceLocation Location
			{
				get
				{
					return SourceLocation.None;
				}
			}

			// Token: 0x17002D69 RID: 11625
			// (get) Token: 0x0600B5C9 RID: 46537 RVA: 0x0024EC8D File Offset: 0x0024CE8D
			public string Message
			{
				get
				{
					return this.message;
				}
			}

			// Token: 0x04005CD4 RID: 23764
			private readonly string message;
		}

		// Token: 0x02001C83 RID: 7299
		private sealed class ModuleReference
		{
			// Token: 0x17002D6A RID: 11626
			// (get) Token: 0x0600B5CA RID: 46538 RVA: 0x0024EC95 File Offset: 0x0024CE95
			// (set) Token: 0x0600B5CB RID: 46539 RVA: 0x0024EC9D File Offset: 0x0024CE9D
			public string LoadVersion { get; set; }

			// Token: 0x17002D6B RID: 11627
			// (get) Token: 0x0600B5CC RID: 46540 RVA: 0x0024ECA6 File Offset: 0x0024CEA6
			// (set) Token: 0x0600B5CD RID: 46541 RVA: 0x0024ECAE File Offset: 0x0024CEAE
			public int ReferenceCount { get; set; }
		}

		// Token: 0x02001C84 RID: 7300
		private struct DynamicWrapper
		{
			// Token: 0x0600B5CF RID: 46543 RVA: 0x0024ECB7 File Offset: 0x0024CEB7
			public static void Serialize(Stream stream, DependencyCompiler.DynamicWrapper wrapper)
			{
				BinaryWriter binaryWriter = new BinaryWriter(stream);
				binaryWriter.WriteNullableString(wrapper.InnerModuleName);
				binaryWriter.WriteNullableString(wrapper.OuterCode);
			}

			// Token: 0x0600B5D0 RID: 46544 RVA: 0x0024ECD8 File Offset: 0x0024CED8
			public static DependencyCompiler.DynamicWrapper Deserialize(Stream stream)
			{
				BinaryReader binaryReader = new BinaryReader(stream);
				return new DependencyCompiler.DynamicWrapper
				{
					InnerModuleName = binaryReader.ReadNullableString(),
					OuterCode = binaryReader.ReadNullableString()
				};
			}

			// Token: 0x04005CD7 RID: 23767
			public string InnerModuleName;

			// Token: 0x04005CD8 RID: 23768
			public string OuterCode;
		}
	}
}
