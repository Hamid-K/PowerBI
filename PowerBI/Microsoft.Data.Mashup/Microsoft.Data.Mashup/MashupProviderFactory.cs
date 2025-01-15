using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.IO;
using Microsoft.Data.Mashup.ProviderCommon;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.EngineHost;
using Microsoft.Mashup.EngineHost.Services;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Libraries;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200003E RID: 62
	public sealed class MashupProviderFactory : DbProviderFactory
	{
		// Token: 0x060002EE RID: 750 RVA: 0x0000B8E0 File Offset: 0x00009AE0
		public static void InitializeDiskCacheRoot(string directory, TimeSpan? garbageCollectionTimeout)
		{
			DynamicDiskCacheRoot.SetDirectory(directory);
			if (garbageCollectionTimeout != null)
			{
				CacheGroupManager.SetGarbageCollectionTimeout(garbageCollectionTimeout.Value);
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000B900 File Offset: 0x00009B00
		public static string LoadExtension(byte[] moduleBytes)
		{
			if (moduleBytes == null || moduleBytes.Length == 0)
			{
				throw new ArgumentNullException("moduleBytes");
			}
			IModule module;
			Exception ex;
			if (!LibraryService.TryCompileLibrary(moduleBytes, out module, out ex))
			{
				throw new MashupException(ex.Message, ex);
			}
			ILibrary library;
			if (MashupProviderFactory.LegacyProvider.TryGetLibrary(module.Name, out library))
			{
				throw new MashupException(ProviderErrorStrings.ModuleNameDuplicate(module.Name));
			}
			if (!MashupProviderFactory.LegacyProvider.TryAddLibrary(module.Name, moduleBytes, out ex))
			{
				throw new MashupException(ex.Message);
			}
			return module.Name;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000B984 File Offset: 0x00009B84
		public static string LoadExtension(string filename)
		{
			if (string.IsNullOrEmpty(filename))
			{
				throw new ArgumentNullException("filename");
			}
			if (!File.Exists(filename))
			{
				throw new FileNotFoundException(ProviderErrorStrings.ExtensionFileNotFound(filename), filename);
			}
			string extension = Path.GetExtension(filename);
			if (!string.Equals(extension, ".dll", StringComparison.OrdinalIgnoreCase) && !string.Equals(extension, ".exe", StringComparison.OrdinalIgnoreCase))
			{
				return MashupProviderFactory.LoadExtension(File.ReadAllBytes(filename));
			}
			string text = null;
			if (MashupProviderFactory.standardExtensions.TryGetValue(filename, out text))
			{
				return text;
			}
			Exception ex;
			if (!MashupProviderFactory.engine.TryLoadDllExtension(filename, out text, out ex))
			{
				throw new MashupException(ex.Message, ex);
			}
			HashSet<string> hashSet = MashupProviderFactory.dllExtensions;
			lock (hashSet)
			{
				MashupProviderFactory.dllExtensions.Add(text);
			}
			return text;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000BA54 File Offset: 0x00009C54
		public static bool EnableOptionalModule(string moduleName)
		{
			if (string.IsNullOrEmpty(moduleName))
			{
				throw new ArgumentNullException("moduleName");
			}
			IModule module;
			if (!LibraryService.LegacyLibrary.HasModule(moduleName) && !MashupProviderFactory.engine.TryGetModule(moduleName, out module))
			{
				throw new MashupException(ProviderErrorStrings.ModuleNameNotFound(moduleName));
			}
			LibraryService.LegacyLibrary.AddModule(moduleName);
			return true;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000BAA8 File Offset: 0x00009CA8
		public static void ReplaceExtension(string moduleName, byte[] moduleBytes)
		{
			if (string.IsNullOrEmpty(moduleName))
			{
				throw new ArgumentNullException("moduleName");
			}
			if (moduleBytes == null || moduleBytes.Length == 0)
			{
				throw new ArgumentNullException("moduleBytes");
			}
			IModule module;
			Exception ex;
			if (!LibraryService.TryCompileLibrary(moduleBytes, out module, out ex))
			{
				throw new MashupException(ex.Message, ex);
			}
			if (module.Name != moduleName)
			{
				throw new MashupException(ProviderErrorStrings.ModuleNameChanged(moduleName, module.Name));
			}
			object obj = MashupProviderFactory.lockObject;
			lock (obj)
			{
				bool flag2 = LibraryService.LegacyLibrary.HasModule(moduleName);
				if (!MashupProviderFactory.LegacyProvider.TryReplaceLibrary(module.Name, moduleBytes, out ex))
				{
					IModule module2;
					if (MashupProviderFactory.engine.TryGetModule(module.Name, out module2))
					{
						MashupProviderFactory.engine.DisabledModules.Add(module.Name);
						flag2 = true;
					}
					else if (!MashupProviderFactory.LegacyProvider.TryAddLibrary(module.Name, moduleBytes, out ex))
					{
						throw new MashupException(ex.Message, ex);
					}
				}
				if (flag2)
				{
					LibraryService.LegacyLibrary.Reset();
				}
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000BBC0 File Offset: 0x00009DC0
		public static bool UnloadExtension(string moduleName)
		{
			if (string.IsNullOrEmpty(moduleName))
			{
				throw new ArgumentNullException("moduleName");
			}
			LibraryService.LegacyLibrary.RemoveModule(moduleName);
			HashSet<string> hashSet = MashupProviderFactory.dllExtensions;
			lock (hashSet)
			{
				if (MashupProviderFactory.dllExtensions.Remove(moduleName))
				{
					return MashupProviderFactory.engine.UnloadExtension(moduleName);
				}
			}
			return MashupProviderFactory.LegacyProvider.RemoveLibrary(moduleName);
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000BC40 File Offset: 0x00009E40
		internal static MemoryLibraryProvider LegacyProvider
		{
			get
			{
				if (MashupProviderFactory.legacyProvider == null)
				{
					object obj = MashupProviderFactory.lockObject;
					lock (obj)
					{
						if (MashupProviderFactory.legacyProvider == null)
						{
							if (MashupProviderFactory.libraryProviders != null)
							{
								throw new InvalidOperationException("must pick either legacy or modern library loading");
							}
							MashupProviderFactory.legacyProvider = new MemoryLibraryProvider("$Legacy", true);
							LibraryService.Reinitialize(new ILibraryProvider[] { MashupProviderFactory.legacyProvider });
						}
					}
				}
				return MashupProviderFactory.legacyProvider;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000BCC4 File Offset: 0x00009EC4
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x0000BD04 File Offset: 0x00009F04
		internal static ILibraryProvider[] LibraryProviders
		{
			get
			{
				object obj = MashupProviderFactory.lockObject;
				ILibraryProvider[] array;
				lock (obj)
				{
					array = MashupProviderFactory.libraryProviders;
				}
				return array;
			}
			set
			{
				object obj = MashupProviderFactory.lockObject;
				lock (obj)
				{
					if (MashupProviderFactory.legacyProvider != null)
					{
						throw new InvalidOperationException("must pick either legacy or modern library loading");
					}
					MashupProviderFactory.libraryProviders = value;
					LibraryService.Reinitialize(MashupProviderFactory.libraryProviders ?? new ILibraryProvider[0]);
				}
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000BD6C File Offset: 0x00009F6C
		internal static void ResetLibraryProviders()
		{
			object obj = MashupProviderFactory.lockObject;
			lock (obj)
			{
				LibraryService.Reinitialize(Array.Empty<ILibraryProvider>());
				MashupProviderFactory.legacyProvider = null;
				MashupProviderFactory.libraryProviders = null;
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000BDBC File Offset: 0x00009FBC
		internal static IEngineHost MakeEngineHost()
		{
			return new CompositeEngineHost(new IEngineHost[]
			{
				new SimpleEngineHost<ICultureService>(new CultureService(MinimalEngineHost.Instance, CultureInfo.InvariantCulture.Name)),
				new SimpleEngineHost<ICurrentTimeService>(new CurrentTimeService(null)),
				new SimpleEngineHost<ITimeZoneService>(MinimalEngineHost.LocalTimeZoneService)
			});
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000BE13 File Offset: 0x0000A013
		public override DbCommand CreateCommand()
		{
			return new MashupCommand();
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000BE1A File Offset: 0x0000A01A
		public override DbCommandBuilder CreateCommandBuilder()
		{
			throw new NotSupportedException(ProviderErrorStrings.CommandBuilderNotSupported);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000BE26 File Offset: 0x0000A026
		public override DbConnection CreateConnection()
		{
			return new MashupConnection();
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000BE2D File Offset: 0x0000A02D
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new MashupConnectionStringBuilder();
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000BE34 File Offset: 0x0000A034
		public override DbDataAdapter CreateDataAdapter()
		{
			throw new NotSupportedException(ProviderErrorStrings.DataAdapterNotSupported);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000BE40 File Offset: 0x0000A040
		public override DbDataSourceEnumerator CreateDataSourceEnumerator()
		{
			throw new NotSupportedException(ProviderErrorStrings.CreateDataSourceEnumeratorNotSupported);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000BE4C File Offset: 0x0000A04C
		public override DbParameter CreateParameter()
		{
			return new MashupParameter();
		}

		// Token: 0x04000187 RID: 391
		public static readonly MashupProviderFactory Instance = new MashupProviderFactory();

		// Token: 0x04000188 RID: 392
		public const string InvariantName = "Microsoft.Data.Mashup";

		// Token: 0x04000189 RID: 393
		private static readonly object lockObject = new object();

		// Token: 0x0400018A RID: 394
		private static readonly IEngine engine = MashupEngines.Version1;

		// Token: 0x0400018B RID: 395
		private static MemoryLibraryProvider legacyProvider;

		// Token: 0x0400018C RID: 396
		private static ILibraryProvider[] libraryProviders;

		// Token: 0x0400018D RID: 397
		private static readonly HashSet<string> dllExtensions = new HashSet<string>();

		// Token: 0x0400018E RID: 398
		private static Dictionary<string, string> standardExtensions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
		{
			{ "Microsoft.Mashup.HtmlTable.dll", "HtmlTable" },
			{ "Microsoft.Mashup.Pdf.dll", "Pdf" },
			{ "Microsoft.Mashup.Parquet.dll", "Parquet" },
			{ "Microsoft.Mashup.WebBrowserContents.dll", "WebBrowserContents" }
		};

		// Token: 0x0400018F RID: 399
		private static HashSet<string> standardModules = new HashSet<string>(MashupProviderFactory.standardExtensions.Values);
	}
}
