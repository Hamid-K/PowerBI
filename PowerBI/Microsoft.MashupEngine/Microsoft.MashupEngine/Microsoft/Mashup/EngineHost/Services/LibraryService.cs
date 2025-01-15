using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Evaluator.Services;
using Microsoft.Mashup.Libraries;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019FF RID: 6655
	public class LibraryService : ILibraryService
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600A86F RID: 43119 RVA: 0x0022C80C File Offset: 0x0022AA0C
		// (remove) Token: 0x0600A870 RID: 43120 RVA: 0x0022C840 File Offset: 0x0022AA40
		public static event EventHandler<LibraryChangedEventArgs> ModulesChanged;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600A871 RID: 43121 RVA: 0x0022C874 File Offset: 0x0022AA74
		// (remove) Token: 0x0600A872 RID: 43122 RVA: 0x0022C8A8 File Offset: 0x0022AAA8
		public static event EventHandler<ValidateLibraryEventArgs> ValidateLibrary;

		// Token: 0x0600A873 RID: 43123 RVA: 0x0022C8DC File Offset: 0x0022AADC
		private LibraryService(ILibraryProvider[] providers)
		{
			this.instanceLockObject = new object();
			this.providers = providers;
			this.knownLibraries = new List<string>[providers.Length];
			this.validLibraries = new Dictionary<string, LibraryService.LibraryInfo>();
			this.loadedModules = new Dictionary<string, LibraryService.LibraryInfo>();
			this.validModules = new Dictionary<string, List<LibraryService.LibraryInfo>>();
			this.loadedSymbols = new HashSet<string>();
			this.invalidLibraries = new Dictionary<string, Exception>();
		}

		// Token: 0x0600A874 RID: 43124 RVA: 0x0022C948 File Offset: 0x0022AB48
		private LibraryService(LibraryService original)
		{
			this.instanceLockObject = new object();
			this.providers = original.providers;
			object obj = original.instanceLockObject;
			lock (obj)
			{
				this.knownLibraries = new List<string>[original.providers.Length];
				for (int i = 0; i < original.providers.Length; i++)
				{
					this.knownLibraries[i] = new List<string>(original.knownLibraries[i]);
				}
				this.validLibraries = new Dictionary<string, LibraryService.LibraryInfo>(original.validLibraries);
				this.loadedModules = new Dictionary<string, LibraryService.LibraryInfo>(original.loadedModules);
				this.validModules = new Dictionary<string, List<LibraryService.LibraryInfo>>(original.validModules);
				this.loadedSymbols = new HashSet<string>(original.loadedSymbols);
				this.invalidLibraries = new Dictionary<string, Exception>(original.invalidLibraries);
			}
		}

		// Token: 0x17002AE6 RID: 10982
		// (get) Token: 0x0600A875 RID: 43125 RVA: 0x0022CA30 File Offset: 0x0022AC30
		public static LibraryInstance LegacyLibrary
		{
			get
			{
				return LibraryService.legacyLibrary;
			}
		}

		// Token: 0x0600A876 RID: 43126 RVA: 0x0022CA38 File Offset: 0x0022AC38
		public static LibraryService Reinitialize(params ILibraryProvider[] providers)
		{
			object obj = LibraryService.legacyLibrary.lockObject;
			LibraryService libraryService;
			lock (obj)
			{
				object obj2 = LibraryService.lockObject;
				lock (obj2)
				{
					string[] array = EmptyArray<string>.Instance;
					if (LibraryService.activeInstance != null)
					{
						array = LibraryService.activeInstance.Uninitialize();
					}
					LibraryService.activeInstance = new LibraryService(providers);
					object obj3 = LibraryService.activeInstance.instanceLockObject;
					lock (obj3)
					{
						for (int i = 0; i < providers.Length; i++)
						{
							providers[i].Changed += LibraryService.activeInstance.OnLibraryChange;
						}
						string[] array2 = LibraryService.activeInstance.EnumerateAndLoadLibraries();
						EventHandler<LibraryChangedEventArgs> modulesChanged = LibraryService.ModulesChanged;
						if (modulesChanged != null)
						{
							modulesChanged(LibraryService.activeInstance, new LibraryChangedEventArgs(array2, EmptyArray<string>.Instance, array));
						}
					}
					LibraryService.passiveInstance = null;
					libraryService = LibraryService.activeInstance;
				}
			}
			return libraryService;
		}

		// Token: 0x0600A877 RID: 43127 RVA: 0x0022CB64 File Offset: 0x0022AD64
		public static void SetMetadataExtractor(string name, ILibraryMetadataExtractor extractor)
		{
			object obj = LibraryService.lockObject;
			lock (obj)
			{
				LibraryService.extractors[name] = extractor;
			}
		}

		// Token: 0x0600A878 RID: 43128 RVA: 0x0022CBAC File Offset: 0x0022ADAC
		public static void AddTrustedThumbprint(string thumbprint)
		{
			ValidateLibraryEventArgs.AddTrustedThumbprint(thumbprint);
		}

		// Token: 0x0600A879 RID: 43129 RVA: 0x0022CBB4 File Offset: 0x0022ADB4
		public static LibraryService GetInstance()
		{
			object obj = LibraryService.lockObject;
			LibraryService libraryService;
			lock (obj)
			{
				if (LibraryService.activeInstance == null)
				{
					LibraryService.activeInstance = new LibraryService(EmptyArray<ILibraryProvider>.Instance);
				}
				if (LibraryService.passiveInstance == null)
				{
					LibraryService.passiveInstance = new LibraryService(LibraryService.activeInstance);
				}
				libraryService = LibraryService.passiveInstance;
			}
			return libraryService;
		}

		// Token: 0x0600A87A RID: 43130 RVA: 0x0022CC20 File Offset: 0x0022AE20
		public static bool TryCompileLibrary(byte[] libraryBytes, out IModule module, out Exception error)
		{
			IEngine version = MashupEngines.Version1;
			LibraryFile libraryFile;
			if (!LibraryFile.New(libraryBytes, out libraryFile, out error))
			{
				module = null;
				return false;
			}
			module = LibraryService.TryCompileLibrary(version, libraryFile.Source, libraryFile, out error);
			return module != null;
		}

		// Token: 0x0600A87B RID: 43131 RVA: 0x0022CC58 File Offset: 0x0022AE58
		public string[] GetLoadedVersions(string[] moduleNames)
		{
			object obj = this.instanceLockObject;
			string[] array2;
			lock (obj)
			{
				string[] array = new string[moduleNames.Length];
				for (int i = 0; i < moduleNames.Length; i++)
				{
					LibraryService.LibraryInfo libraryInfo;
					if (this.loadedModules.TryGetValue(moduleNames[i], out libraryInfo))
					{
						array[i] = libraryInfo.Library.Version;
					}
				}
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600A87C RID: 43132 RVA: 0x0022CCD0 File Offset: 0x0022AED0
		public string GetSource(string moduleName)
		{
			object obj = this.instanceLockObject;
			string text;
			lock (obj)
			{
				LibraryService.LibraryInfo libraryInfo;
				if (!this.loadedModules.TryGetValue(moduleName, out libraryInfo))
				{
					text = null;
				}
				else
				{
					text = libraryInfo.GetSource();
				}
			}
			return text;
		}

		// Token: 0x0600A87D RID: 43133 RVA: 0x0022CD28 File Offset: 0x0022AF28
		public string GetResourceString(string moduleName, string cultureName, string stringName)
		{
			object obj = this.instanceLockObject;
			string text;
			lock (obj)
			{
				LibraryService.LibraryInfo libraryInfo;
				if (!this.loadedModules.TryGetValue(moduleName, out libraryInfo))
				{
					text = null;
				}
				else
				{
					text = libraryInfo.GetResourceString(cultureName, stringName);
				}
			}
			return text;
		}

		// Token: 0x0600A87E RID: 43134 RVA: 0x0022CD80 File Offset: 0x0022AF80
		public byte[] GetResourceFile(string moduleName, string filename)
		{
			object obj = this.instanceLockObject;
			byte[] array;
			lock (obj)
			{
				LibraryService.LibraryInfo libraryInfo;
				if (!this.loadedModules.TryGetValue(moduleName, out libraryInfo))
				{
					array = null;
				}
				else
				{
					array = libraryInfo.GetResourceFile(filename);
				}
			}
			return array;
		}

		// Token: 0x0600A87F RID: 43135 RVA: 0x0022CDD8 File Offset: 0x0022AFD8
		public ModuleTrustLevel GetTrustLevel(string moduleName)
		{
			object obj = this.instanceLockObject;
			ModuleTrustLevel moduleTrustLevel;
			lock (obj)
			{
				LibraryService.LibraryInfo libraryInfo;
				if (!this.loadedModules.TryGetValue(moduleName, out libraryInfo))
				{
					moduleTrustLevel = ModuleTrustLevel.Unknown;
				}
				else
				{
					moduleTrustLevel = libraryInfo.TrustLevel;
				}
			}
			return moduleTrustLevel;
		}

		// Token: 0x0600A880 RID: 43136 RVA: 0x0022CE30 File Offset: 0x0022B030
		public ILibraryService GetLibrary(string moduleName)
		{
			object obj = this.instanceLockObject;
			ILibraryService libraryService;
			lock (obj)
			{
				LibraryService.LibraryInfo libraryInfo;
				if (!this.loadedModules.TryGetValue(moduleName, out libraryInfo))
				{
					libraryService = null;
				}
				else
				{
					libraryService = libraryInfo.File;
				}
			}
			return libraryService;
		}

		// Token: 0x0600A881 RID: 43137 RVA: 0x0022CE88 File Offset: 0x0022B088
		public ILibrary GetLoadedModule(string moduleName)
		{
			object obj = this.instanceLockObject;
			ILibrary library;
			lock (obj)
			{
				LibraryService.LibraryInfo libraryInfo;
				if (!this.loadedModules.TryGetValue(moduleName, out libraryInfo))
				{
					library = null;
				}
				else
				{
					library = libraryInfo.Library;
				}
			}
			return library;
		}

		// Token: 0x0600A882 RID: 43138 RVA: 0x0022CEE0 File Offset: 0x0022B0E0
		public ILibrary[] GetLoadedLibraries()
		{
			object obj = this.instanceLockObject;
			ILibrary[] array;
			lock (obj)
			{
				array = this.loadedModules.Values.Select((LibraryService.LibraryInfo info) => info.Library).ToArray<ILibrary>();
			}
			return array;
		}

		// Token: 0x0600A883 RID: 43139 RVA: 0x0022CF50 File Offset: 0x0022B150
		public ISerializableValue GetLibraries(string cultureName)
		{
			IEngine version = MashupEngines.Version1;
			int num = 0;
			object obj = this.instanceLockObject;
			ISerializableValue serializableValue;
			lock (obj)
			{
				List<IValue> list = new List<IValue>();
				list.Add(version.Record(LibraryService.LibrariesKeys, new IValue[]
				{
					version.Text("Builtin"),
					version.Number((double)num++),
					version.Null,
					version.Null,
					version.Null,
					version.Null,
					version.Null,
					version.Null,
					version.Null
				}));
				for (int i = 0; i < this.knownLibraries.Length; i++)
				{
					foreach (string text in this.knownLibraries[i])
					{
						string text2 = this.providers[i].FullIdentifier(text);
						IValue value = version.Null;
						LibraryService.LibraryInfo libraryInfo;
						Exception ex;
						if (!this.validLibraries.TryGetValue(text2, out libraryInfo) && this.invalidLibraries.TryGetValue(text2, out ex))
						{
							value = version.Text(ex.Message);
						}
						LibraryService.LibraryInfo libraryInfo2;
						if (libraryInfo != null && this.loadedModules.TryGetValue(libraryInfo.ModuleName, out libraryInfo2) && libraryInfo != libraryInfo2)
						{
							value = version.Text(Strings.ShadowedLibrary);
						}
						if (libraryInfo != null && cultureName != null)
						{
							libraryInfo = libraryInfo.Localize(cultureName);
						}
						IValue @null;
						if (libraryInfo == null || libraryInfo.ModuleMetadata == null || !libraryInfo.ModuleMetadata.TryGetValue("Dependencies", out @null) || !@null.IsRecord)
						{
							@null = version.Null;
						}
						List<IValue> list2 = list;
						IEngine engine = version;
						IKeys librariesKeys = LibraryService.LibrariesKeys;
						IValue[] array = new IValue[9];
						array[0] = version.Text(this.providers[i].Identifier);
						array[1] = version.Number((double)num);
						array[2] = version.Text(text);
						array[3] = version.Text(text2);
						array[4] = value;
						int num2 = 5;
						IValue value3;
						if (libraryInfo != null)
						{
							IValue value2 = version.Text(libraryInfo.ModuleName);
							value3 = value2;
						}
						else
						{
							value3 = version.Null;
						}
						array[num2] = value3;
						int num3 = 6;
						IValue value4;
						if (libraryInfo != null)
						{
							IValue value2 = version.Text(libraryInfo.ModuleVersion);
							value4 = value2;
						}
						else
						{
							value4 = version.Null;
						}
						array[num3] = value4;
						array[7] = @null;
						int num4 = 8;
						IValue value5;
						if (libraryInfo != null && libraryInfo.ModuleMetadata != null)
						{
							IValue value2 = libraryInfo.ModuleMetadata;
							value5 = value2;
						}
						else
						{
							value5 = version.Null;
						}
						array[num4] = value5;
						list2.Add(engine.Record(librariesKeys, array));
					}
					num++;
				}
				serializableValue = new SerializableValue(version.Table(LibraryService.LibrariesKeys, list.ToArray()), 131072, this.instanceLockObject);
			}
			return serializableValue;
		}

		// Token: 0x0600A884 RID: 43140 RVA: 0x0022D230 File Offset: 0x0022B430
		public ISerializableValue GetLibraryExports(string cultureName, string libraryIdentifier)
		{
			IEngine version = MashupEngines.Version1;
			object obj = this.instanceLockObject;
			ISerializableValue serializableValue;
			lock (obj)
			{
				IValue[] array = EmptyArray<IValue>.Instance;
				LibraryService.LibraryInfo libraryInfo;
				if (this.validLibraries.TryGetValue(libraryIdentifier, out libraryInfo))
				{
					if (cultureName != null)
					{
						libraryInfo = libraryInfo.Localize(cultureName);
					}
					IRecordValue recordValue = libraryInfo.Exports();
					array = new IValue[recordValue.Keys.Length];
					for (int i = 0; i < array.Length; i++)
					{
						IValue value = recordValue[i];
						IValue value2 = version.Null;
						IValue value3 = version.Null;
						if (value.IsFunction)
						{
							if (!value.TryGetMetaField("Publish", out value2) || !value2.IsRecord)
							{
								value2 = version.Null;
							}
							string text = value.AsFunction.PrimaryResourceKind;
							if (text == null && value.TryGetMetaField("DataSource.Kind", out value3) && value3.IsText)
							{
								text = value3.AsString;
							}
							IValue value5;
							if (text != null)
							{
								IValue value4 = version.Text(text);
								value5 = value4;
							}
							else
							{
								value5 = version.Null;
							}
							value3 = value5;
						}
						array[i] = version.Record(LibraryService.ExportKeys, new IValue[]
						{
							version.Text(recordValue.Keys[i]),
							value.Type,
							value3,
							value2
						});
					}
				}
				serializableValue = new SerializableValue(version.Table(LibraryService.ExportKeys, array), 131072, this.instanceLockObject);
			}
			return serializableValue;
		}

		// Token: 0x0600A885 RID: 43141 RVA: 0x0022D3C8 File Offset: 0x0022B5C8
		public ISerializableValue GetLibraryDataSources(string cultureName, string libraryIdentifier)
		{
			IEngine version = MashupEngines.Version1;
			object obj = this.instanceLockObject;
			ISerializableValue serializableValue;
			lock (obj)
			{
				IValue[] array = EmptyArray<IValue>.Instance;
				LibraryService.LibraryInfo libraryInfo;
				if (this.validLibraries.TryGetValue(libraryIdentifier, out libraryInfo))
				{
					if (cultureName != null)
					{
						libraryInfo = libraryInfo.Localize(cultureName);
					}
					array = LibraryService.MakeDataSources(version, libraryInfo.Module);
				}
				serializableValue = new SerializableValue(version.Table(LibraryService.DataSourcesKeys, array), 131072, this.instanceLockObject);
			}
			return serializableValue;
		}

		// Token: 0x0600A886 RID: 43142 RVA: 0x0022D45C File Offset: 0x0022B65C
		public IModule[] GetModules(string[] moduleNames)
		{
			IModule[] array = new IModule[moduleNames.Length];
			object obj = this.instanceLockObject;
			lock (obj)
			{
				for (int i = 0; i < array.Length; i++)
				{
					LibraryService.LibraryInfo libraryInfo;
					if (this.loadedModules.TryGetValue(moduleNames[i], out libraryInfo))
					{
						array[i] = libraryInfo.Module;
					}
					else
					{
						array[i] = new ModuleStub(moduleNames[i], null, Array.Empty<string>());
					}
				}
			}
			return array;
		}

		// Token: 0x0600A887 RID: 43143 RVA: 0x0022D4E0 File Offset: 0x0022B6E0
		public Dictionary<string, VersionRange> GetDependencies(HashSet<string> references, bool returnAllModules, IEnumerable<string> moduleNames)
		{
			Dictionary<string, VersionRange> dictionary = new Dictionary<string, VersionRange>();
			foreach (string text in moduleNames)
			{
				IModule module;
				if (MashupEngines.Version1.TryGetModule(text, out module))
				{
					if (returnAllModules || text == "Core")
					{
						LibraryService.AddDependency(dictionary, text, module.Version);
					}
					else
					{
						foreach (string text2 in module.Exports)
						{
							if (references.Contains(text2))
							{
								LibraryService.AddDependency(dictionary, text, module.Version);
								break;
							}
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600A888 RID: 43144 RVA: 0x0022D5B0 File Offset: 0x0022B7B0
		internal IModule[] GetModules()
		{
			return this.loadedModules.Select((KeyValuePair<string, LibraryService.LibraryInfo> module) => module.Value.Module).ToArray<IModule>();
		}

		// Token: 0x0600A889 RID: 43145 RVA: 0x0022D5E4 File Offset: 0x0022B7E4
		internal static void ClearValidationEvent()
		{
			if (LibraryService.ValidateLibrary != null)
			{
				Delegate[] invocationList = LibraryService.ValidateLibrary.GetInvocationList();
				for (int i = 0; i < invocationList.Length; i++)
				{
					LibraryService.ValidateLibrary -= (EventHandler<ValidateLibraryEventArgs>)invocationList[i];
				}
			}
		}

		// Token: 0x0600A88A RID: 43146 RVA: 0x0022D620 File Offset: 0x0022B820
		private static void AddDependency(IDictionary<string, VersionRange> moduleDependencies, string moduleName, string moduleVersion)
		{
			VersionRange versionRange;
			if (moduleDependencies.TryGetValue(moduleName, out versionRange))
			{
				moduleDependencies[moduleName] = LibraryService.MergeDependencyRange(versionRange, LibraryService.GetDependencyRange(moduleVersion, moduleName));
				return;
			}
			moduleDependencies[moduleName] = LibraryService.GetDependencyRange(moduleVersion, moduleName);
		}

		// Token: 0x0600A88B RID: 43147 RVA: 0x0022D65C File Offset: 0x0022B85C
		private static VersionRange MergeDependencyRange(VersionRange range1, VersionRange range2)
		{
			Version version = ((range1.MinVersion < range2.MinVersion) ? range1.MinVersion : range2.MinVersion);
			Version version2 = ((range1.MaxVersion > range2.MaxVersion) ? range1.MaxVersion : range2.MaxVersion);
			return new VersionRange(version, version2);
		}

		// Token: 0x0600A88C RID: 43148 RVA: 0x0022D6B4 File Offset: 0x0022B8B4
		private static VersionRange GetDependencyRange(string versionText, string moduleName)
		{
			if (moduleName == "Core")
			{
				versionText = MashupEngines.Version1Version;
			}
			Version version;
			if (Versioning.TryParseVersion(versionText, out version))
			{
				return new VersionRange(version);
			}
			return new VersionRange(new Version(1, 0));
		}

		// Token: 0x0600A88D RID: 43149 RVA: 0x0022D6F4 File Offset: 0x0022B8F4
		private string[] EnumerateAndLoadLibraries()
		{
			for (int i = 0; i < this.providers.Length; i++)
			{
				this.knownLibraries[i] = new List<string>();
				foreach (ILibrary library in this.providers[i].GetLibraries())
				{
					this.knownLibraries[i].Add(library.Identifier);
					this.GetLibraryInfo(library);
				}
			}
			foreach (List<LibraryService.LibraryInfo> list in this.validModules.Values)
			{
				LibraryService.LibraryInfo libraryInfo = list[0];
				Exception ex;
				if (libraryInfo.Register(this.loadedSymbols, out ex))
				{
					this.loadedModules.Add(libraryInfo.ModuleName, libraryInfo);
				}
				else
				{
					this.invalidLibraries.Add(libraryInfo.Library.FullIdentifier(), ex);
					this.validLibraries.Remove(libraryInfo.Library.FullIdentifier());
				}
			}
			return this.loadedModules.Keys.ToArray<string>();
		}

		// Token: 0x0600A88E RID: 43150 RVA: 0x0022D82C File Offset: 0x0022BA2C
		private string[] Uninitialize()
		{
			foreach (LibraryService.LibraryInfo libraryInfo in this.loadedModules.Values)
			{
				libraryInfo.Unregister(this.loadedSymbols);
			}
			return this.loadedModules.Keys.ToArray<string>();
		}

		// Token: 0x0600A88F RID: 43151 RVA: 0x0022D898 File Offset: 0x0022BA98
		private LibraryService.LibraryInfo GetLibraryInfo(ILibrary library)
		{
			LibraryService.LibraryInfo libraryInfo;
			if (!LibraryService.LibraryInfo.TryGetInfo(MashupEngines.Version1, this, library, out libraryInfo))
			{
				libraryInfo = new LibraryService.LibraryInfo(this, library);
				if (libraryInfo.IsValid)
				{
					libraryInfo.TryPersistInfo(this);
				}
			}
			if (libraryInfo.IsValid)
			{
				this.validLibraries.Add(library.FullIdentifier(), libraryInfo);
				this.AddLoadedLibrary(libraryInfo);
			}
			else
			{
				this.invalidLibraries.Add(library.FullIdentifier(), libraryInfo.LoadError);
			}
			return libraryInfo;
		}

		// Token: 0x0600A890 RID: 43152 RVA: 0x0022D908 File Offset: 0x0022BB08
		private void OnLibraryChange(object sender, LibraryChangedEventArgs e)
		{
			ILibraryProvider provider = sender as ILibraryProvider;
			if (provider == null)
			{
				return;
			}
			List<string> list = null;
			for (int i = 0; i < this.providers.Length; i++)
			{
				if (provider == this.providers[i])
				{
					list = this.knownLibraries[i];
					break;
				}
			}
			if (list == null)
			{
				return;
			}
			object obj = LibraryService.legacyLibrary.lockObject;
			lock (obj)
			{
				object obj2 = this.instanceLockObject;
				lock (obj2)
				{
					string[] array = e.Added ?? EmptyArray<string>.Instance;
					string[] array2 = e.Changed ?? EmptyArray<string>.Instance;
					string[] array3 = e.Removed ?? EmptyArray<string>.Instance;
					list.AddRange(array);
					foreach (string text in array3)
					{
						list.Remove(text);
					}
					HashSet<string> hashSet = new HashSet<string>();
					IList<string> list2 = new List<string>(array.Length + array2.Length);
					IList<string> list3 = new List<string>(array3.Length + array2.Length);
					IEnumerable<string> enumerable = array3.Concat(array2);
					Func<string, string> <>9__0;
					Func<string, string> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (string library) => provider.FullIdentifier(library));
					}
					foreach (string text2 in enumerable.Select(func))
					{
						LibraryService.LibraryInfo libraryInfo;
						if (this.validLibraries.TryGetValue(text2, out libraryInfo))
						{
							if (libraryInfo.IsRegistered)
							{
								libraryInfo.Unregister(this.loadedSymbols);
								this.loadedModules.Remove(libraryInfo.ModuleName);
								list3.Add(libraryInfo.ModuleName);
							}
							this.validLibraries.Remove(text2);
							this.RemoveLoadedLibrary(libraryInfo);
							hashSet.Add(libraryInfo.ModuleName);
						}
						this.invalidLibraries.Remove(text2);
					}
					foreach (string text3 in array.Concat(array2))
					{
						ILibrary library2;
						if (provider.TryGetLibrary(text3, out library2))
						{
							LibraryService.LibraryInfo libraryInfo2 = this.GetLibraryInfo(library2);
							if (libraryInfo2.IsValid)
							{
								hashSet.Add(libraryInfo2.ModuleName);
							}
							else
							{
								e.Failures[library2.Identifier] = libraryInfo2.LoadError;
							}
						}
					}
					foreach (string text4 in hashSet)
					{
						List<LibraryService.LibraryInfo> list4;
						if (this.validModules.TryGetValue(text4, out list4))
						{
							LibraryService.LibraryInfo libraryInfo3 = list4[0];
							if (!libraryInfo3.IsRegistered)
							{
								foreach (LibraryService.LibraryInfo libraryInfo4 in list4)
								{
									if (libraryInfo4.IsRegistered)
									{
										this.loadedModules.Remove(libraryInfo4.ModuleName);
										libraryInfo4.Unregister(this.loadedSymbols);
										list3.Add(libraryInfo4.ModuleName);
										break;
									}
								}
								Exception ex;
								if (libraryInfo3.Register(this.loadedSymbols, out ex))
								{
									this.loadedModules.Add(libraryInfo3.ModuleName, libraryInfo3);
									list2.Add(libraryInfo3.ModuleName);
								}
								else
								{
									this.invalidLibraries[libraryInfo3.Library.FullIdentifier()] = ex;
									e.Failures[libraryInfo3.Library.Identifier] = ex;
								}
							}
						}
					}
					EventHandler<LibraryChangedEventArgs> modulesChanged = LibraryService.ModulesChanged;
					if (modulesChanged != null)
					{
						modulesChanged(sender, new LibraryChangedEventArgs(list2.ToArray<string>(), EmptyArray<string>.Instance, list3.ToArray<string>()));
					}
				}
			}
			obj = LibraryService.lockObject;
			lock (obj)
			{
				LibraryService.passiveInstance = null;
			}
		}

		// Token: 0x0600A891 RID: 43153 RVA: 0x0022DDB0 File Offset: 0x0022BFB0
		private void AddLoadedLibrary(LibraryService.LibraryInfo info)
		{
			List<LibraryService.LibraryInfo> list;
			if (!this.validModules.TryGetValue(info.ModuleName, out list))
			{
				list = new List<LibraryService.LibraryInfo>();
				this.validModules.Add(info.ModuleName, list);
			}
			if (list.Count > 0)
			{
				int num = 0;
				while (num < this.providers.Length && this.providers[num] != info.Library.Provider)
				{
					num++;
				}
				int num2 = 0;
				while (num2 < list.Count && this.providers.Take(num).Contains(list[num2].Library.Provider))
				{
					num2++;
				}
				list.Insert(num2, info);
				return;
			}
			list.Add(info);
		}

		// Token: 0x0600A892 RID: 43154 RVA: 0x0022DE60 File Offset: 0x0022C060
		private void RemoveLoadedLibrary(LibraryService.LibraryInfo info)
		{
			List<LibraryService.LibraryInfo> list;
			if (this.validModules.TryGetValue(info.ModuleName, out list))
			{
				list.Remove(info);
				if (list.Count == 0)
				{
					this.validModules.Remove(info.ModuleName);
				}
			}
		}

		// Token: 0x0600A893 RID: 43155 RVA: 0x0022DEA4 File Offset: 0x0022C0A4
		private static IModule TryCompileLibrary(IEngine engine, string source, ILibraryService libraryService, out Exception error)
		{
			List<IError> errors = new List<IError>();
			Action<IError> action = delegate(IError err)
			{
				errors.Add(err);
			};
			IDocument document = engine.Parse(engine.Tokenize(source), new TextDocumentHost(source), action);
			ISectionDocument sectionDocument;
			if (document.Kind == DocumentKind.Expression)
			{
				if (!engine.TryWrapExpressionDataSource((IExpressionDocument)document, libraryService, out sectionDocument, out error))
				{
					return null;
				}
			}
			else
			{
				sectionDocument = (ISectionDocument)document;
			}
			IModule module;
			if (errors.Count > 0 || !engine.TryCompileDataSource(sectionDocument, null, libraryService, CompileOptions.None, action, out module))
			{
				IError error2 = errors[0];
				string text = error2.Message;
				if (error2.Location != null)
				{
					text = error2.Location.Range.ToString() + " " + text;
				}
				error = new InvalidOperationException(text);
				return null;
			}
			error = null;
			return module;
		}

		// Token: 0x0600A894 RID: 43156 RVA: 0x0022DF80 File Offset: 0x0022C180
		private static ITableValue MakeAuthenticationInfos(IEngine engine, ResourceKindInfo resourceKindInfo)
		{
			IEngine engine2 = engine;
			IKeys authenticationKeys = LibraryService.AuthenticationKeys;
			IValue[] array = (from info in resourceKindInfo.AuthenticationInfo
				select LibraryService.MakeAuthenticationInfo(engine, info) into record
				where record != null
				select record).ToArray<IRecordValue>();
			return engine2.Table(authenticationKeys, array);
		}

		// Token: 0x0600A895 RID: 43157 RVA: 0x0022DFEC File Offset: 0x0022C1EC
		private static IRecordValue MakeAuthenticationInfo(IEngine engine, AuthenticationInfo info)
		{
			string text;
			switch (info.AuthenticationKind)
			{
			case AuthenticationKind.Implicit:
				text = "Implicit";
				goto IL_008A;
			case AuthenticationKind.UsernamePassword:
				text = "UsernamePassword";
				goto IL_008A;
			case AuthenticationKind.Windows:
				text = "Windows";
				goto IL_008A;
			case AuthenticationKind.WebApi:
				text = "WebApi";
				goto IL_008A;
			case AuthenticationKind.OAuth2:
				text = ((info is AadAuthenticationInfo) ? "AAD" : "OAuth");
				goto IL_008A;
			case AuthenticationKind.Exchange:
				text = "UsernamePassword";
				goto IL_008A;
			case AuthenticationKind.Key:
				text = "Key";
				goto IL_008A;
			case AuthenticationKind.Parameterized:
				text = ((ParameterizedAuthenticationInfo)info).Name;
				goto IL_008A;
			}
			return null;
			IL_008A:
			return engine.Record(LibraryService.AuthenticationKeys, new IValue[]
			{
				engine.Text(text),
				LibraryService.MakeProperties(engine, DataSourceProperties.GetAuthenticationProperties(info, true)),
				LibraryService.MakeProperties(engine, info.ApplicationProperties)
			});
		}

		// Token: 0x0600A896 RID: 43158 RVA: 0x0022E0BD File Offset: 0x0022C2BD
		private static ITableValue MakeProperties(IEngine engine, ResourceKindInfo resourceKindInfo)
		{
			return LibraryService.MakeProperties(engine, DataSourceProperties.GetConnectionProperties(resourceKindInfo));
		}

		// Token: 0x0600A897 RID: 43159 RVA: 0x0022E0CB File Offset: 0x0022C2CB
		private static ITableValue MakeApplicationProperties(IEngine engine, ResourceKindInfo resourceKindInfo)
		{
			return LibraryService.MakeProperties(engine, resourceKindInfo.ApplicationProperties);
		}

		// Token: 0x0600A898 RID: 43160 RVA: 0x0022E0DC File Offset: 0x0022C2DC
		private static ITableValue MakeProperties(IEngine engine, IList<CredentialProperty> properties)
		{
			if (properties == null)
			{
				return engine.Table(LibraryService.PropertyKeys, Array.Empty<IValue>());
			}
			IEngine engine2 = engine;
			IKeys propertyKeys = LibraryService.PropertyKeys;
			IValue[] array = properties.Select((CredentialProperty p) => engine.Record(LibraryService.PropertyKeys, new IValue[]
			{
				engine.Text(p.Name),
				engine.Text(p.Name ?? p.Label),
				LibraryService.ConvertPropertyType(engine, p)
			})).ToArray<IRecordValue>();
			return engine2.Table(propertyKeys, array);
		}

		// Token: 0x0600A899 RID: 43161 RVA: 0x0022E138 File Offset: 0x0022C338
		private static IValue ConvertPropertyType(IEngine engine, CredentialProperty property)
		{
			TypeHandle typeHandle;
			if (property.PropertyType == typeof(string))
			{
				typeHandle = (property.IsSecret ? TypeHandle.Password : TypeHandle.Text);
			}
			else if (property.PropertyType == typeof(bool))
			{
				typeHandle = TypeHandle.Logical;
			}
			else
			{
				typeHandle = TypeHandle.Any;
			}
			ITypeValue typeValue = engine.Type(typeHandle);
			if (!property.IsRequired)
			{
				typeValue = typeValue.Nullable;
			}
			return typeValue;
		}

		// Token: 0x0600A89A RID: 43162 RVA: 0x0022E1A4 File Offset: 0x0022C3A4
		private static IListValue MakePermissionKinds(IEngine engine, ResourceKindInfo resource)
		{
			IEngine engine2 = engine;
			IValue[] array = resource.PermissionKinds.Select((QueryPermissionChallengeType p) => engine.Text(DataSourceProperties.FromQueryPermission(p))).ToArray<ITextValue>();
			return engine2.List(array);
		}

		// Token: 0x0600A89B RID: 43163 RVA: 0x0022E1E8 File Offset: 0x0022C3E8
		private static IValue[] MakeDataSources(IEngine engine, IModule module)
		{
			ResourceKindInfo[] dataSources = module.DataSources;
			IValue[] array = new IValue[dataSources.Length];
			for (int i = 0; i < array.Length; i++)
			{
				ResourceKindInfo resourceKindInfo = dataSources[i];
				IValue[] array2 = array;
				int num = i;
				IKeys dataSourcesKeys = LibraryService.DataSourcesKeys;
				IValue[] array3 = new IValue[7];
				array3[0] = engine.Text(resourceKindInfo.Kind);
				array3[1] = engine.Text(resourceKindInfo.Label ?? resourceKindInfo.Kind);
				array3[2] = LibraryService.MakeAuthenticationInfos(engine, resourceKindInfo);
				array3[3] = LibraryService.MakeProperties(engine, resourceKindInfo);
				array3[4] = LibraryService.MakeApplicationProperties(engine, resourceKindInfo);
				array3[5] = LibraryService.MakePermissionKinds(engine, resourceKindInfo);
				int num2 = 6;
				IValue resourceRecord = resourceKindInfo.ResourceRecord;
				array3[num2] = resourceRecord ?? engine.Null;
				array2[num] = engine.Record(dataSourcesKeys, array3);
			}
			return array;
		}

		// Token: 0x040057A1 RID: 22433
		public static readonly IKeys LibrariesKeys = MashupEngines.Version1.Keys(new string[] { "Id", "Priority", "LibraryId", "FullIdentifier", "Status", "Name", "Version", "Dependencies", "Metadata" });

		// Token: 0x040057A2 RID: 22434
		public static readonly IKeys DataSourcesKeys = MashupEngines.Version1.Keys(new string[] { "DataSourceKind", "Label", "AuthenticationInfos", "Properties", "ApplicationProperties", "PermissionKinds", "DataSourceRecord" });

		// Token: 0x040057A3 RID: 22435
		public static readonly IKeys ExportKeys = MashupEngines.Version1.Keys(new string[] { "Name", "Type", "DataSourceKind", "Publish" });

		// Token: 0x040057A4 RID: 22436
		private static readonly IKeys AuthenticationKeys = MashupEngines.Version1.Keys(new string[] { "Kind", "Properties", "ApplicationProperties" });

		// Token: 0x040057A5 RID: 22437
		private static readonly IKeys PropertyKeys = MashupEngines.Version1.Keys(new string[] { "Name", "Label", "PropertyType" });

		// Token: 0x040057A6 RID: 22438
		private const string BuiltinProviderName = "Builtin";

		// Token: 0x040057A7 RID: 22439
		private static readonly Dictionary<string, ILibraryMetadataExtractor> extractors = new Dictionary<string, ILibraryMetadataExtractor>();

		// Token: 0x040057A8 RID: 22440
		private const int maxImageLength = 131072;

		// Token: 0x040057A9 RID: 22441
		private static readonly object lockObject = new object();

		// Token: 0x040057AA RID: 22442
		private static LibraryService activeInstance = null;

		// Token: 0x040057AB RID: 22443
		private static LibraryService passiveInstance = null;

		// Token: 0x040057AC RID: 22444
		private static readonly LibraryService.LegacyLibraryInstance legacyLibrary = new LibraryService.LegacyLibraryInstance();

		// Token: 0x040057AD RID: 22445
		private readonly object instanceLockObject;

		// Token: 0x040057AE RID: 22446
		private readonly ILibraryProvider[] providers;

		// Token: 0x040057AF RID: 22447
		private readonly List<string>[] knownLibraries;

		// Token: 0x040057B0 RID: 22448
		private readonly Dictionary<string, LibraryService.LibraryInfo> validLibraries;

		// Token: 0x040057B1 RID: 22449
		private readonly Dictionary<string, LibraryService.LibraryInfo> loadedModules;

		// Token: 0x040057B2 RID: 22450
		private readonly Dictionary<string, List<LibraryService.LibraryInfo>> validModules;

		// Token: 0x040057B3 RID: 22451
		private readonly HashSet<string> loadedSymbols;

		// Token: 0x040057B4 RID: 22452
		private readonly Dictionary<string, Exception> invalidLibraries;

		// Token: 0x02001A00 RID: 6656
		private class LibraryInfo
		{
			// Token: 0x0600A89D RID: 43165 RVA: 0x0022E410 File Offset: 0x0022C610
			public LibraryInfo(LibraryService service, ILibrary library)
				: this(MashupEngines.Version1, service, library)
			{
				if (!LibraryFile.New(library.Contents, out this.file, out this.loadError))
				{
					return;
				}
				this.module = LibraryService.TryCompileLibrary(this.engine, this.file.Source, this.file, out this.loadError);
				if (this.module == null)
				{
					return;
				}
				if (!this.TryValidate(out this.loadError))
				{
					return;
				}
				IError firstError = null;
				Action<IError> action = delegate(IError ierror)
				{
					firstError = firstError ?? ierror;
				};
				if (this.module.Exports.Length <= 0)
				{
					this.exports = this.engine.EmptyRecord;
					this.fullyLoaded = true;
					return;
				}
				string text = string.Format(CultureInfo.InvariantCulture, "Record.SelectFields(#sections[{0}], {{{1}}})", this.engine.EscapeFieldIdentifier(this.module.Name), this.module.Exports.Select((string s) => this.engine.EscapeString(s)).Aggregate((string list, string s) => list + ", " + s));
				IModule module = this.engine.Compile(this.engine.Parse(this.engine.Tokenize(text), new TextDocumentHost(text), action), this.engine.EmptyRecord, CompileOptions.None, action);
				IAssembly assembly = this.engine.Assemble(new IModule[] { module, this.module }, this.engine.GetLibrary(MinimalEngineHost.Instance, Extension.Modules), MinimalEngineHost.Instance, action);
				if (firstError != null)
				{
					this.loadError = new InvalidOperationException(firstError.Message);
					return;
				}
				this.exports = this.engine.Invoke(assembly.Function, Array.Empty<IValue>()).AsRecord;
				this.fullyLoaded = true;
			}

			// Token: 0x0600A89E RID: 43166 RVA: 0x0022E5F0 File Offset: 0x0022C7F0
			private LibraryInfo(LibraryService service, ILibrary library, IModule module, IRecordValue exports)
				: this(MashupEngines.Version1, service, library)
			{
				this.exports = exports;
				if (!LibraryFile.New(library.Contents, out this.file, out this.loadError))
				{
					return;
				}
				this.module = module;
				this.TryValidate(out this.loadError);
			}

			// Token: 0x0600A89F RID: 43167 RVA: 0x0022E640 File Offset: 0x0022C840
			private LibraryInfo(IEngine engine, LibraryService service, ILibrary library)
			{
				this.engine = MashupEngines.Version1;
				this.service = service;
				this.library = library;
				this.localizedVersions = new LruCache<string, byte[]>(3, null);
			}

			// Token: 0x17002AE7 RID: 10983
			// (get) Token: 0x0600A8A0 RID: 43168 RVA: 0x0022E66E File Offset: 0x0022C86E
			public ILibrary Library
			{
				get
				{
					return this.library;
				}
			}

			// Token: 0x17002AE8 RID: 10984
			// (get) Token: 0x0600A8A1 RID: 43169 RVA: 0x0022E676 File Offset: 0x0022C876
			public IModule Module
			{
				get
				{
					return this.module;
				}
			}

			// Token: 0x17002AE9 RID: 10985
			// (get) Token: 0x0600A8A2 RID: 43170 RVA: 0x0022E67E File Offset: 0x0022C87E
			public string ModuleName
			{
				get
				{
					if (this.module != null)
					{
						return this.module.Name;
					}
					return null;
				}
			}

			// Token: 0x17002AEA RID: 10986
			// (get) Token: 0x0600A8A3 RID: 43171 RVA: 0x0022E695 File Offset: 0x0022C895
			public IRecordValue ModuleMetadata
			{
				get
				{
					if (this.module != null)
					{
						return this.module.Metadata;
					}
					return null;
				}
			}

			// Token: 0x17002AEB RID: 10987
			// (get) Token: 0x0600A8A4 RID: 43172 RVA: 0x0022E6AC File Offset: 0x0022C8AC
			public string ModuleVersion
			{
				get
				{
					IModule module = this.module;
					return ((module != null) ? module.Version : null) ?? "1.0";
				}
			}

			// Token: 0x17002AEC RID: 10988
			// (get) Token: 0x0600A8A5 RID: 43173 RVA: 0x0022E6C9 File Offset: 0x0022C8C9
			public ILibraryService File
			{
				get
				{
					return this.file;
				}
			}

			// Token: 0x17002AED RID: 10989
			// (get) Token: 0x0600A8A6 RID: 43174 RVA: 0x0022E6D1 File Offset: 0x0022C8D1
			public bool IsRegistered
			{
				get
				{
					return this.isRegistered;
				}
			}

			// Token: 0x17002AEE RID: 10990
			// (get) Token: 0x0600A8A7 RID: 43175 RVA: 0x0022E6D9 File Offset: 0x0022C8D9
			public bool IsValid
			{
				get
				{
					return this.loadError == null;
				}
			}

			// Token: 0x17002AEF RID: 10991
			// (get) Token: 0x0600A8A8 RID: 43176 RVA: 0x0022E6E4 File Offset: 0x0022C8E4
			public Exception LoadError
			{
				get
				{
					return this.loadError;
				}
			}

			// Token: 0x17002AF0 RID: 10992
			// (get) Token: 0x0600A8A9 RID: 43177 RVA: 0x0022E6EC File Offset: 0x0022C8EC
			public ModuleTrustLevel TrustLevel
			{
				get
				{
					return this.trustLevel;
				}
			}

			// Token: 0x0600A8AA RID: 43178 RVA: 0x0022E6F4 File Offset: 0x0022C8F4
			public static bool TryGetInfo(IEngine engine, LibraryService service, ILibrary library, out LibraryService.LibraryInfo info)
			{
				byte[] array;
				if (library.TryGetMetadata("Module", out array))
				{
					byte[] tmp;
					if (!LibraryService.extractors.SelectMany((KeyValuePair<string, ILibraryMetadataExtractor> pair) => pair.Value.RequiredMetadata).Any((string key) => !library.TryGetMetadata(key, out tmp)))
					{
						bool flag;
						try
						{
							info = LibraryService.LibraryInfo.Deserialize(engine, service, library, array);
							flag = true;
						}
						catch (Exception ex)
						{
							if (!SafeExceptions.IsSafeException(ex))
							{
								throw;
							}
							info = null;
							flag = false;
						}
						return flag;
					}
				}
				info = null;
				return false;
			}

			// Token: 0x0600A8AB RID: 43179 RVA: 0x0022E79C File Offset: 0x0022C99C
			public bool TryPersistInfo(LibraryService libraryService)
			{
				bool flag;
				try
				{
					byte[] array = this.Serialize();
					flag = this.library.TrySetMetadata("Module", array) && LibraryService.extractors.SelectMany((KeyValuePair<string, ILibraryMetadataExtractor> pair) => pair.Value.ExtractMetadata(libraryService, this.library, this.module, this.exports)).All((KeyValuePair<string, byte[]> metadata) => this.library.TrySetMetadata(metadata.Key, metadata.Value));
				}
				catch (Exception ex)
				{
					if (!SafeExceptions.IsSafeException(ex))
					{
						throw;
					}
					this.loadError = ex;
					flag = false;
				}
				return flag;
			}

			// Token: 0x0600A8AC RID: 43180 RVA: 0x0022E82C File Offset: 0x0022CA2C
			public string GetSource()
			{
				return this.file.GetSource(this.module.Name);
			}

			// Token: 0x0600A8AD RID: 43181 RVA: 0x0022E844 File Offset: 0x0022CA44
			public string GetResourceString(string cultureName, string stringName)
			{
				return this.file.GetResourceString(this.module.Name, cultureName, stringName);
			}

			// Token: 0x0600A8AE RID: 43182 RVA: 0x0022E85E File Offset: 0x0022CA5E
			public byte[] GetResourceFile(string filename)
			{
				return this.file.GetResourceFile(this.module.Name, filename);
			}

			// Token: 0x0600A8AF RID: 43183 RVA: 0x0022E878 File Offset: 0x0022CA78
			public bool TryValidate(out Exception error)
			{
				bool flag2;
				try
				{
					error = null;
					LibraryDescription libraryDescription = new LibraryDescription();
					libraryDescription.LibraryProvider = this.library.Provider.Identifier;
					libraryDescription.LibraryIdentifier = this.library.Identifier;
					libraryDescription.LibraryVersion = this.library.Version;
					libraryDescription.ModuleName = this.module.Name;
					libraryDescription.ModuleVersion = this.module.Version;
					libraryDescription.DataSourceKinds = this.module.DataSources.Select((ResourceKindInfo ds) => ds.Kind).ToArray<string>();
					libraryDescription.Verification = this.file.Verification;
					libraryDescription.Signers = this.file.Signers;
					libraryDescription.LibraryContents = this.library.Contents;
					ValidateLibraryEventArgs validateLibraryEventArgs = new ValidateLibraryEventArgs(libraryDescription);
					bool flag = true;
					if (validateLibraryEventArgs.IsFirstParty)
					{
						this.trustLevel = ModuleTrustLevel.FirstParty;
						flag = false;
					}
					else if (validateLibraryEventArgs.IsCertified)
					{
						this.trustLevel = ModuleTrustLevel.Certified;
						flag = false;
					}
					EventHandler<ValidateLibraryEventArgs> validateLibrary = LibraryService.ValidateLibrary;
					if (validateLibrary != null)
					{
						validateLibrary(this.service, validateLibraryEventArgs);
						if (!validateLibraryEventArgs.Validated)
						{
							error = new InvalidOperationException(Strings.LibraryValidationFailed(this.module.Name));
							return false;
						}
					}
					using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("LibraryService/LibraryInfo/TryValidate", null, TraceEventType.Information, null))
					{
						hostTrace.Add("Provider", this.library.Provider.Identifier, true);
						hostTrace.Add("Library", this.library.Identifier, true);
						hostTrace.Add("Module", this.module.Name, flag);
						hostTrace.Add("Version", this.module.Version, false);
						hostTrace.Add("TrustLevel", this.trustLevel, false);
					}
					flag2 = true;
				}
				catch (Exception ex)
				{
					if (!SafeExceptions.IsSafeException(ex))
					{
						throw;
					}
					error = ex;
					flag2 = false;
				}
				return flag2;
			}

			// Token: 0x0600A8B0 RID: 43184 RVA: 0x0022EAA4 File Offset: 0x0022CCA4
			public bool Register(HashSet<string> symbols, out Exception error)
			{
				if (this.isRegistered || this.loadError != null)
				{
					error = this.loadError;
					return this.isRegistered;
				}
				string text;
				IModule module;
				if (!this.fullyLoaded)
				{
					if (!this.TryDelayedLoadExtension(this.module, this.file, out error))
					{
						return false;
					}
				}
				else if (!this.TryLoadExtension(this.file.Source, this.file, out text, out error) || !this.engine.TryGetModule(text, out module))
				{
					error = error ?? new InvalidOperationException("Unreachable");
					return false;
				}
				IKeys keys = this.module.Exports;
				for (int i = 0; i < keys.Length; i++)
				{
					if (!symbols.Add(keys[i]))
					{
						for (int j = 0; j < i; j++)
						{
							symbols.Remove(keys[j]);
						}
						error = new InvalidOperationException(Strings.ShadowedLibrary);
						return false;
					}
				}
				this.isRegistered = true;
				error = null;
				return true;
			}

			// Token: 0x0600A8B1 RID: 43185 RVA: 0x0022EB94 File Offset: 0x0022CD94
			public bool Unregister(HashSet<string> symbols)
			{
				if (!this.isRegistered)
				{
					return false;
				}
				this.isRegistered = false;
				IKeys keys = this.module.Exports;
				for (int i = 0; i < keys.Length; i++)
				{
					symbols.Remove(keys[i]);
				}
				return this.engine.UnloadExtension(this.ModuleName);
			}

			// Token: 0x0600A8B2 RID: 43186 RVA: 0x0022EBF0 File Offset: 0x0022CDF0
			public IRecordValue Exports()
			{
				IKeys keys = this.module.Exports;
				List<IValue> list = new List<IValue>(keys.Length);
				foreach (string text in keys)
				{
					try
					{
						IValue value;
						if (this.exports.TryGetValue(text, out value))
						{
							list.Add(value);
							continue;
						}
					}
					catch (ValueException2)
					{
					}
					list.Add(this.engine.Null);
				}
				return this.engine.Record(this.engine.Keys(keys.ToArray<string>()), list.ToArray());
			}

			// Token: 0x0600A8B3 RID: 43187 RVA: 0x0022ECA8 File Offset: 0x0022CEA8
			public LibraryService.LibraryInfo Localize(string cultureName)
			{
				CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
				try
				{
					CultureInfo cultureInfo;
					string nearestSupportedCulture = this.file.GetNearestSupportedCulture(cultureName, out cultureInfo);
					if (nearestSupportedCulture != null)
					{
						byte[] array;
						if (!this.localizedVersions.TryGetValue(nearestSupportedCulture, out array))
						{
							Thread.CurrentThread.CurrentUICulture = cultureInfo;
							array = new LibraryService.LibraryInfo(this.service, this.library).Serialize();
							this.localizedVersions.Add(nearestSupportedCulture, array);
						}
						return LibraryService.LibraryInfo.Deserialize(this.engine, this.service, this.library, array);
					}
				}
				catch (Exception ex)
				{
					if (!SafeExceptions.IsSafeException(ex))
					{
						throw;
					}
				}
				finally
				{
					Thread.CurrentThread.CurrentUICulture = currentUICulture;
				}
				return this;
			}

			// Token: 0x0600A8B4 RID: 43188 RVA: 0x0022ED68 File Offset: 0x0022CF68
			private bool TryLoadExtension(string moduleSource, ILibraryService libraryService, out string moduleName, out Exception error)
			{
				if (this.IsBuiltinModule(this.module.Name))
				{
					moduleName = this.module.Name;
					return this.engine.TryReplaceExtension(this.module, libraryService, false, out error);
				}
				return this.engine.TryLoadExtension(moduleSource, libraryService, out moduleName, out error);
			}

			// Token: 0x0600A8B5 RID: 43189 RVA: 0x0022EDBB File Offset: 0x0022CFBB
			private bool TryDelayedLoadExtension(IModule moduleInfo, ILibraryService libraryService, out Exception error)
			{
				if (this.IsBuiltinModule(moduleInfo.Name))
				{
					return this.engine.TryReplaceExtension(moduleInfo, libraryService, true, out error);
				}
				return this.engine.TryDelayedLoadExtension(moduleInfo, libraryService, out error);
			}

			// Token: 0x0600A8B6 RID: 43190 RVA: 0x0022EDEC File Offset: 0x0022CFEC
			private bool IsBuiltinModule(string moduleName)
			{
				LibraryService.LibraryInfo libraryInfo;
				IModule module;
				return !this.service.loadedModules.TryGetValue(moduleName, out libraryInfo) && this.engine.TryGetModule(moduleName, out module);
			}

			// Token: 0x0600A8B7 RID: 43191 RVA: 0x0022EE20 File Offset: 0x0022D020
			private byte[] Serialize()
			{
				byte[] array;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
					{
						IRecordValue recordValue = this.exports ?? this.engine.EmptyRecord;
						ModuleStub.Write(binaryWriter, this.module);
						binaryWriter.WriteArray(this.module.DataSources ?? EmptyArray<ResourceKindInfo>.Instance, delegate(BinaryWriter w, ResourceKindInfo resource)
						{
							SerializedResourceKindInfo.Serialize(this.engine, w, resource);
						});
						binaryWriter.Write(ValueSerializer.SerializePreviewValue(this.engine, this.module.Metadata ?? this.engine.EmptyRecord, null, new ValueSerializerOptions?(LibraryService.LibraryInfo.serializationOptions)));
						binaryWriter.Write(ValueSerializer.SerializePreviewValue(this.engine, recordValue, null, new ValueSerializerOptions?(LibraryService.LibraryInfo.serializationOptions)));
						binaryWriter.WriteNullableString((this.module.DynamicModuleDataSource == null) ? null : this.module.DynamicModuleDataSource.Kind);
						array = memoryStream.ToArray();
					}
				}
				return array;
			}

			// Token: 0x0600A8B8 RID: 43192 RVA: 0x0022EF38 File Offset: 0x0022D138
			private static LibraryService.LibraryInfo Deserialize(IEngine engine, LibraryService service, ILibrary library, byte[] bytes)
			{
				LibraryService.LibraryInfo libraryInfo;
				using (MemoryStream memoryStream = new MemoryStream(bytes))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						ModuleStub moduleStub = new ModuleStub(binaryReader);
						ModuleStub moduleStub2 = moduleStub;
						ResourceKindInfo[] array = binaryReader.ReadArray((BinaryReader r) => SerializedResourceKindInfo.Deserialize(engine, r));
						moduleStub2.DataSources = array;
						moduleStub.Metadata = ValueDeserializer.Deserialize(engine, binaryReader.ReadString()).AsRecord;
						IRecordValue asRecord = ValueDeserializer.Deserialize(engine, binaryReader.ReadString()).AsRecord;
						if (memoryStream.Position < (long)bytes.Length)
						{
							string dynamicDataSourceKind = binaryReader.ReadNullableString();
							moduleStub.DynamicModuleDataSource = moduleStub.DataSources.FirstOrDefault((ResourceKindInfo ds) => ds.Kind == dynamicDataSourceKind);
						}
						libraryInfo = new LibraryService.LibraryInfo(service, library, moduleStub, asRecord);
					}
				}
				return libraryInfo;
			}

			// Token: 0x040057B7 RID: 22455
			private static readonly ValueSerializerOptions serializationOptions = new ValueSerializerOptions
			{
				MaxValueDepth = 6,
				NestedRecords = true,
				TruncatedBinaryLength = 131072,
				StripFieldDescriptions = false
			};

			// Token: 0x040057B8 RID: 22456
			private readonly IEngine engine;

			// Token: 0x040057B9 RID: 22457
			private readonly LibraryService service;

			// Token: 0x040057BA RID: 22458
			private readonly ILibrary library;

			// Token: 0x040057BB RID: 22459
			private readonly LibraryFile file;

			// Token: 0x040057BC RID: 22460
			private readonly IModule module;

			// Token: 0x040057BD RID: 22461
			private readonly IRecordValue exports;

			// Token: 0x040057BE RID: 22462
			private readonly bool fullyLoaded;

			// Token: 0x040057BF RID: 22463
			private readonly LruCache<string, byte[]> localizedVersions;

			// Token: 0x040057C0 RID: 22464
			private bool isRegistered;

			// Token: 0x040057C1 RID: 22465
			private ModuleTrustLevel trustLevel;

			// Token: 0x040057C2 RID: 22466
			private Exception loadError;
		}

		// Token: 0x02001A07 RID: 6663
		private class LegacyLibraryInstance : LibraryInstance
		{
			// Token: 0x0600A8CC RID: 43212 RVA: 0x0022F164 File Offset: 0x0022D364
			public LegacyLibraryInstance()
			{
				this.lockObject = new object();
				this.modules = new HashSet<string>();
				this.modules.Add("Action");
				LibraryService.ModulesChanged += this.OnModulesChanged;
			}

			// Token: 0x17002AF1 RID: 10993
			// (get) Token: 0x0600A8CD RID: 43213 RVA: 0x0022F1A4 File Offset: 0x0022D3A4
			public override IRecordValue CurrentLibrary
			{
				get
				{
					if (this.currentLibrary == null)
					{
						object obj = this.lockObject;
						lock (obj)
						{
							if (this.currentLibrary == null)
							{
								this.currentLibrary = base.MakeLibrary(this.modules);
							}
						}
					}
					return this.currentLibrary;
				}
			}

			// Token: 0x0600A8CE RID: 43214 RVA: 0x0022F208 File Offset: 0x0022D408
			public override void Reset()
			{
				object obj = this.lockObject;
				lock (obj)
				{
					this.currentLibrary = null;
				}
			}

			// Token: 0x0600A8CF RID: 43215 RVA: 0x0022F24C File Offset: 0x0022D44C
			public override void AddModule(string module)
			{
				object obj = this.lockObject;
				lock (obj)
				{
					if (this.modules.Add(module))
					{
						this.currentLibrary = null;
					}
				}
			}

			// Token: 0x0600A8D0 RID: 43216 RVA: 0x0022F29C File Offset: 0x0022D49C
			public override void RemoveModule(string module)
			{
				object obj = this.lockObject;
				lock (obj)
				{
					if (this.modules.Remove(module))
					{
						this.currentLibrary = null;
					}
				}
			}

			// Token: 0x0600A8D1 RID: 43217 RVA: 0x0022F2EC File Offset: 0x0022D4EC
			public override bool HasModule(string module)
			{
				object obj = this.lockObject;
				bool flag2;
				lock (obj)
				{
					flag2 = this.modules.Contains(module);
				}
				return flag2;
			}

			// Token: 0x0600A8D2 RID: 43218 RVA: 0x0022F334 File Offset: 0x0022D534
			public override HashSet<string> GetModules()
			{
				object obj = this.lockObject;
				HashSet<string> hashSet;
				lock (obj)
				{
					hashSet = new HashSet<string>(this.modules);
				}
				return hashSet;
			}

			// Token: 0x0600A8D3 RID: 43219 RVA: 0x0022F37C File Offset: 0x0022D57C
			private void OnModulesChanged(object source, LibraryChangedEventArgs e)
			{
				this.modules.ExceptWith(e.Removed);
				this.modules.UnionWith(e.Added);
				this.currentLibrary = null;
			}

			// Token: 0x040057CE RID: 22478
			public readonly object lockObject;

			// Token: 0x040057CF RID: 22479
			private readonly HashSet<string> modules;

			// Token: 0x040057D0 RID: 22480
			private IRecordValue currentLibrary;
		}
	}
}
