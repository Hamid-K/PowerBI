using System;
using System.Globalization;
using System.Security;
using Microsoft.Fabric.Common;
using Microsoft.Win32;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000122 RID: 290
	internal class CustomProviderFactory
	{
		// Token: 0x06000858 RID: 2136 RVA: 0x0001E154 File Offset: 0x0001C354
		private static string GetTypeFromRegistry(string invariantName)
		{
			string text = string.Empty;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\AppFabric\\V1.0\\Providers\\AppFabricCaching"))
				{
					if (registryKey == null)
					{
						throw CustomProviderFactory.CustomProviderLoadException(invariantName, string.Format(CultureInfo.CurrentCulture, "{0} -> {1}", new object[]
						{
							Registry.LocalMachine.Name,
							"SOFTWARE\\Microsoft\\AppFabric\\V1.0\\Providers\\AppFabricCaching"
						}));
					}
					using (RegistryKey registryKey2 = registryKey.OpenSubKey(invariantName))
					{
						if (registryKey2 == null)
						{
							throw CustomProviderFactory.CustomProviderLoadException(invariantName, string.Format(CultureInfo.CurrentCulture, "{0} -> {1} -> {2}", new object[]
							{
								Registry.LocalMachine.Name,
								"SOFTWARE\\Microsoft\\AppFabric\\V1.0\\Providers\\AppFabricCaching",
								invariantName
							}));
						}
						text = registryKey2.GetValue("Type", null) as string;
						if (text == null)
						{
							throw CustomProviderFactory.CustomProviderLoadException(invariantName, string.Format(CultureInfo.CurrentCulture, "{0} -> {1} -> {2} -> {3}", new object[]
							{
								Registry.LocalMachine.Name,
								"SOFTWARE\\Microsoft\\AppFabric\\V1.0\\Providers\\AppFabricCaching",
								invariantName,
								"Type"
							}));
						}
					}
				}
			}
			catch (SecurityException ex)
			{
				throw CustomProviderFactory.ProviderException(invariantName, ex);
			}
			return text;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001E2C4 File Offset: 0x0001C4C4
		public static ICustomProvider CreateInstance(string invariantName)
		{
			string text = null;
			ICustomProvider customProvider = null;
			if (CloudUtility.IsVASDeployment)
			{
				if (invariantName.Equals("System.Data.SqlClient"))
				{
					text = "ExternalStore.SqlServer";
					customProvider = CustomProviderFactory.CreateInstance(invariantName, "Microsoft.ApplicationServer.Caching.SqlServerCustomProvider, Microsoft.ApplicationServer.Caching.SqlProvider, Culture=neutral");
				}
				else if (invariantName.Equals("SqlAzure"))
				{
					text = "ExternalStore.SqlAzure";
					customProvider = CustomProviderFactory.CreateInstance(invariantName, "Microsoft.ApplicationServer.Caching.SqlAzureCustomProvider, Microsoft.ApplicationServer.Caching.SqlAzureProvider, Culture=neutral");
				}
			}
			else if (invariantName.Equals("SqlAzure"))
			{
				text = "ExternalStore.SqlAzure";
				customProvider = CustomProviderFactory.CreateInstance(invariantName, "Microsoft.ApplicationServer.Caching.SqlAzureCustomProvider, Microsoft.ApplicationServer.Caching.SqlAzureProvider, Culture=neutral");
			}
			else if (invariantName.Equals("WindowsAzureBlobProvider"))
			{
				text = "ExternalStore.WindowsAzureBlob";
				customProvider = CustomProviderFactory.CreateInstance(invariantName, "Microsoft.ApplicationServer.Caching.WindowsAzureBlobProvider, Microsoft.ApplicationServer.Caching.WindowsAzureBlobProvider, Culture=neutral");
			}
			else
			{
				text = invariantName;
				customProvider = CustomProviderFactory.CreateInstance(invariantName, CustomProviderFactory.GetTypeFromRegistry(invariantName));
			}
			if (customProvider != null)
			{
				return new CustomProviderProxy(text, customProvider);
			}
			return null;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001E37C File Offset: 0x0001C57C
		public static void RegisterProvider(string invariant, string type, string displayName)
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\AppFabric\\V1.0\\Providers\\AppFabricCaching"))
				{
					using (RegistryKey registryKey2 = registryKey.CreateSubKey(invariant))
					{
						registryKey2.SetValue("Type", type);
						registryKey2.SetValue("DisplayName", displayName);
					}
				}
			}
			catch (Exception ex)
			{
				if (ex is UnauthorizedAccessException || ex is SecurityException)
				{
					throw new DataCacheException("CustomProviderFactory", 9008, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 9008, invariant, ex.Message), true);
				}
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001E438 File Offset: 0x0001C638
		internal static ICustomProvider CreateInstance(string invariantName, string typeStr)
		{
			ICustomProvider customProvider;
			try
			{
				Type type = Type.GetType(typeStr, true);
				customProvider = CustomProviderFactory.Instantiate(invariantName, type);
			}
			catch (Exception ex)
			{
				EventLogWriter.WriteWarning("CustomProviderFactory", "Error while loading assembly = {0}, Exception= {1}", new object[] { typeStr, ex });
				if (Utility.IsExpectedDuringTypeLoad(ex))
				{
					throw CustomProviderFactory.ProviderException(invariantName, ex);
				}
				throw;
			}
			return customProvider;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001E49C File Offset: 0x0001C69C
		private static ICustomProvider Instantiate(string provider, Type type)
		{
			ICustomProvider customProvider2;
			try
			{
				ICustomProvider customProvider = (ICustomProvider)Activator.CreateInstance(type);
				customProvider2 = customProvider;
			}
			catch (Exception ex)
			{
				if (Utility.IsExpectedDuringReflection(ex) || ex is InvalidCastException)
				{
					throw CustomProviderFactory.ProviderException(string.Format(CultureInfo.CurrentCulture, "{0}:{1}", new object[] { provider, type.AssemblyQualifiedName }), ex);
				}
				throw;
			}
			return customProvider2;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001E508 File Offset: 0x0001C708
		internal static DataCacheException ProviderException(string invariantName, Exception e)
		{
			return new DataCacheException("CustomProviderFactory", 9007, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 9007, invariantName, e.Message), e, true);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001E53E File Offset: 0x0001C73E
		internal static DataCacheException CustomProviderLoadException(string provider, string parameter)
		{
			return new DataCacheException("CustomProviderFactory", 9006, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 9006, provider, parameter), true);
		}

		// Token: 0x04000678 RID: 1656
		private const string LogSource = "CustomProviderFactory";
	}
}
