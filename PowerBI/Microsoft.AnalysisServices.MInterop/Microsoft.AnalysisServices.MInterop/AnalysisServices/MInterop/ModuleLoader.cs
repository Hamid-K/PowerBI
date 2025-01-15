using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.AnalysisServices.PlatformHost;
using Microsoft.Data.Mashup;
using Microsoft.Data.Mashup.Preview;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x02000027 RID: 39
	internal static class ModuleLoader
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00004300 File Offset: 0x00002500
		internal static void LoadModules(string additionalExtensionsString, string disabledExtensionsString, string customExtensionDirectoriesString, bool includeSubdirectories = false, bool watchForChanges = false, string certifiedExtensionsAllowlist = "", string certifiedExtensionDirectories = "", bool certifiedExtensionDirectoryWatchForChanges = false, bool certifiedExtensionDirectoryIncludeSubdirectories = false)
		{
			ModuleLoader.ApplyInitializationWithRetry("EnableOptionalModule", delegate
			{
				MashupProviderFactory.EnableOptionalModule("Resource");
			});
			List<MashupLibraryProvider> mashupProviders = new List<MashupLibraryProvider>();
			mashupProviders.Add(ModuleLoader.PowerBIExtensionsAssemblyProvider(additionalExtensionsString, disabledExtensionsString));
			mashupProviders.AddRange(ModuleLoader.CustomExtensionDirectoryProviders(customExtensionDirectoriesString, includeSubdirectories, watchForChanges));
			mashupProviders.AddRange(ModuleLoader.CertifiedExtensionDirectoryProviders(certifiedExtensionsAllowlist, certifiedExtensionDirectories, certifiedExtensionDirectoryIncludeSubdirectories, certifiedExtensionDirectoryWatchForChanges));
			ModuleLoader.ApplyInitializationWithRetry("MashupLibraryProvider::SetProviders", delegate
			{
				MashupLibraryProvider.SetProviders(mashupProviders.ToArray());
			});
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000439C File Offset: 0x0000259C
		private static void ApplyInitializationWithRetry(string stepName, Action func)
		{
			bool flag = true;
			int num = 0;
			while (flag)
			{
				try
				{
					flag = false;
					num++;
					func();
				}
				catch (Exception ex)
				{
					if (num <= ModuleLoader.MaxInitializationAttempts)
					{
						flag = true;
					}
					string text = string.Format("Error '{0}' during initialization step '{1}'. Attempt: {2}, Retry: {3}, BackOff for retry: {4} milliseconds.", new object[]
					{
						ex.Message,
						stepName,
						num,
						flag,
						ModuleLoader.InitializationBackOff.TotalMilliseconds
					});
					if (ModuleLoader.InitializationErrors.Count < ModuleLoader.MaxInitializationErrorBufferCount)
					{
						ModuleLoader.InitializationErrors.Add(text);
						ModuleLoader.InitializationErrorCallStacks.Add(ex.StackTrace);
					}
					IEngineTracer engineTracer = MInteropHelperImpl.EngineTracer;
					if (engineTracer != null)
					{
						engineTracer.LogMessage(text);
					}
					Thread.Sleep(ModuleLoader.InitializationBackOff);
				}
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004474 File Offset: 0x00002674
		private static IEnumerable<string> SplitSemicolonDelimitedString(string moduleList)
		{
			return from s in moduleList.Split(new char[] { ';' })
				select s.Trim() into s
				where s.Length != 0
				select s;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000044DA File Offset: 0x000026DA
		private static IEnumerable<MashupLibraryProvider> CustomExtensionDirectoryProviders(string customExtensionDirectoriesString, bool includeSubdirectories, bool watchForChanges)
		{
			return ModuleLoader.FilesystemDirectoryProviders(customExtensionDirectoriesString, includeSubdirectories, watchForChanges);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000044E4 File Offset: 0x000026E4
		private static IEnumerable<MashupLibraryProvider> FilesystemDirectoryProviders(string customExtensionDirectoriesString, bool includeSubdirectories, bool watchForChanges)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
			if (!string.IsNullOrEmpty(customExtensionDirectoriesString))
			{
				hashSet.UnionWith(ModuleLoader.SplitSemicolonDelimitedString(customExtensionDirectoriesString));
			}
			List<MashupLibraryProvider> list = new List<MashupLibraryProvider>();
			foreach (string text in hashSet)
			{
				list.Add(MashupLibraryProvider.Filesystem(text, includeSubdirectories, watchForChanges));
			}
			return list;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004560 File Offset: 0x00002760
		private static IEnumerable<MashupLibraryProvider> CertifiedExtensionDirectoryProviders(string certifiedExtensionsAllowlistString, string certifiedExtensionDirectoriesString, bool includeSubdirectories, bool watchForChanges)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
			if (!string.IsNullOrEmpty(certifiedExtensionDirectoriesString))
			{
				hashSet.UnionWith(ModuleLoader.SplitSemicolonDelimitedString(certifiedExtensionDirectoriesString));
			}
			HashSet<string> hashSet2 = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
			if (!string.IsNullOrEmpty(certifiedExtensionsAllowlistString))
			{
				hashSet2.UnionWith(ModuleLoader.SplitSemicolonDelimitedString(certifiedExtensionsAllowlistString));
			}
			if (hashSet2.Count == 0)
			{
				return ModuleLoader.FilesystemDirectoryProviders(certifiedExtensionDirectoriesString, includeSubdirectories, watchForChanges);
			}
			List<byte[]> list = new List<byte[]>();
			foreach (string text in hashSet)
			{
				try
				{
					foreach (string text2 in from path in Directory.GetFiles(text)
						where ModuleLoader.MashupExtensionExtensions.Contains(Path.GetExtension(path), StringComparer.InvariantCultureIgnoreCase)
						select path)
					{
						string text3 = text2.Substring(text.Length + 1);
						if (hashSet2.Contains(text3))
						{
							try
							{
								list.Add(File.ReadAllBytes(text2));
							}
							catch (IOException)
							{
								IEngineTracer engineTracer = MInteropHelperImpl.EngineTracer;
								if (engineTracer != null)
								{
									engineTracer.LogMessage("Unable to load mashup extension '" + text3 + "'. Skipping.");
								}
							}
						}
					}
				}
				catch (Exception)
				{
					IEngineTracer engineTracer2 = MInteropHelperImpl.EngineTracer;
					if (engineTracer2 != null)
					{
						engineTracer2.LogMessage("Error loading mashup extensions from '" + text + "'. Skipping.");
					}
				}
			}
			if (list.Count == 0)
			{
				return new List<MashupLibraryProvider>();
			}
			return new List<MashupLibraryProvider> { MashupLibraryProvider.Memory("CertifiedExtensions", list.ToArray()) };
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000471C File Offset: 0x0000291C
		private static MashupLibraryProvider PowerBIExtensionsAssemblyProvider(string additionalExtensionsString, string disabledExtensionsString)
		{
			HashSet<string> hashSet = new HashSet<string>(ModuleLoader.BuiltinAllowlist, StringComparer.InvariantCultureIgnoreCase);
			if (!string.IsNullOrEmpty(additionalExtensionsString))
			{
				hashSet.UnionWith(ModuleLoader.SplitSemicolonDelimitedString(additionalExtensionsString));
			}
			if (!string.IsNullOrEmpty(disabledExtensionsString))
			{
				foreach (string text in ModuleLoader.SplitSemicolonDelimitedString(disabledExtensionsString))
				{
					hashSet.Remove(text);
				}
			}
			return ModuleLoader.PowerBIExtensionsAssemblyProvider(hashSet);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000479C File Offset: 0x0000299C
		private static MashupLibraryProvider PowerBIExtensionsAssemblyProvider(HashSet<string> allowlist)
		{
			AssemblyName assemblyName = new AssemblyName(Path.GetFileNameWithoutExtension(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "PowerBIExtensions.dll")));
			Assembly assembly = Assembly.Load(assemblyName);
			HashSet<string> resourceNames = new HashSet<string>((from name in assembly.GetManifestResourceNames()
				where allowlist.Contains(name)
				select name).ToArray<string>());
			return MashupLibraryProvider.Assembly(assemblyName, (string resourceName) => resourceNames.Contains(resourceName));
		}

		// Token: 0x040000C4 RID: 196
		private static readonly string[] MashupExtensionExtensions = new string[] { ".m", ".mez", ".pq", ".pqx" };

		// Token: 0x040000C5 RID: 197
		private static readonly int MaxInitializationAttempts = 10;

		// Token: 0x040000C6 RID: 198
		private static readonly int MaxInitializationErrorBufferCount = 2 * ModuleLoader.MaxInitializationAttempts;

		// Token: 0x040000C7 RID: 199
		private static readonly TimeSpan InitializationBackOff = TimeSpan.FromMilliseconds(100.0);

		// Token: 0x040000C8 RID: 200
		private static List<string> InitializationErrors = new List<string>();

		// Token: 0x040000C9 RID: 201
		private static List<string> InitializationErrorCallStacks = new List<string>();

		// Token: 0x040000CA RID: 202
		private static readonly HashSet<string> BuiltinAllowlist = new HashSet<string>(new string[]
		{
			"PowerBIExtensions.appFigures.pqx", "PowerBIExtensions.AzureEnterprise.mez", "PowerBIExtensions.DataLake.pqx", "PowerBIExtensions.DocumentDB.pqx", "PowerBIExtensions.GitHub.pqx", "PowerBIExtensions.Impala.pqx", "PowerBIExtensions.Kusto.pqx", "PowerBIExtensions.MailChimp.pqx", "PowerBIExtensions.Marketo.pqx", "PowerBIExtensions.Mixpanel.pqx",
			"PowerBIExtensions.QuickBooks.pqx", "PowerBIExtensions.Redshift.pqx", "PowerBIExtensions.Smartsheet.pqx", "PowerBIExtensions.Snowflake.pqx", "PowerBIExtensions.Spark.pqx", "PowerBIExtensions.Stripe.pqx", "PowerBIExtensions.SweetIQ.pqx", "PowerBIExtensions.twilio.pqx", "PowerBIExtensions.VSTS.pqx", "PowerBIExtensions.zendesk.pqx"
		}, StringComparer.InvariantCultureIgnoreCase);
	}
}
