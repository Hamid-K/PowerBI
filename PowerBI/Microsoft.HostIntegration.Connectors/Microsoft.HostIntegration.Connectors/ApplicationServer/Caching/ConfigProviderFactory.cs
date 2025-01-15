using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000124 RID: 292
	internal class ConfigProviderFactory
	{
		// Token: 0x06000865 RID: 2149 RVA: 0x0001E59C File Offset: 0x0001C79C
		private ConfigProviderFactory()
		{
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0001E5E4 File Offset: 0x0001C7E4
		internal IClusterPropertiesReader CreateClusterPropertiesReaderInstance(string providerName)
		{
			ConfigProviderTypes configProviderTypes;
			if (this.GetProviderTypes(providerName, out configProviderTypes))
			{
				return ConfigProviderFactory.CreateInstance<IClusterPropertiesReader>(providerName, configProviderTypes.CacheConfigReaderType);
			}
			throw ConfigProviderFactory.ProviderException(providerName, new DataCacheException("No provider with the given name was found"));
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0001E61C File Offset: 0x0001C81C
		internal IClusterHostConfigurationReader CreateHostConfigReaderInstance(string providerName)
		{
			ConfigProviderTypes configProviderTypes;
			if (this.GetProviderTypes(providerName, out configProviderTypes))
			{
				return ConfigProviderFactory.CreateInstance<IClusterHostConfigurationReader>(providerName, configProviderTypes.HostConfigReaderType);
			}
			throw ConfigProviderFactory.ProviderException(providerName, new DataCacheException("No provider with the given name was found"));
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001E654 File Offset: 0x0001C854
		private bool GetProviderTypes(string providerName, out ConfigProviderTypes providerType)
		{
			bool flag2;
			lock (this.lockObject)
			{
				flag2 = this.KnownProviderTypes.TryGetValue(providerName, out providerType);
			}
			return flag2;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0001E6A0 File Offset: 0x0001C8A0
		internal void RegisterConfigReaders(string providerName, string cacheConfigReaderType, string hostConfigReaderType)
		{
			if (string.IsNullOrEmpty(providerName))
			{
				throw new ArgumentNullException("providerName");
			}
			if (string.IsNullOrEmpty(cacheConfigReaderType))
			{
				throw new ArgumentNullException("cacheConfigReaderType");
			}
			if (string.IsNullOrEmpty(hostConfigReaderType))
			{
				throw new ArgumentNullException("hostConfigReaderType");
			}
			ConfigProviderTypes configProviderTypes = new ConfigProviderTypes(cacheConfigReaderType, hostConfigReaderType);
			lock (this.lockObject)
			{
				this.KnownProviderTypes.Add(providerName, configProviderTypes);
			}
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001E728 File Offset: 0x0001C928
		private static T CreateInstance<T>(string invariantName, string typeStr)
		{
			T t;
			try
			{
				Type type = Type.GetType(typeStr, true);
				t = ConfigProviderFactory.Instantiate<T>(invariantName, type);
			}
			catch (Exception ex)
			{
				EventLogWriter.WriteWarning(ConfigProviderFactory.LogSource, "Error while loading assembly = {0}, Exception= {1}", new object[] { typeStr, ex });
				if (Utility.IsExpectedDuringTypeLoad(ex))
				{
					throw ConfigProviderFactory.ProviderException(invariantName, ex);
				}
				throw;
			}
			return t;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001E78C File Offset: 0x0001C98C
		private static T Instantiate<T>(string provider, Type type)
		{
			T t2;
			try
			{
				T t = (T)((object)Activator.CreateInstance(type));
				t2 = t;
			}
			catch (Exception ex)
			{
				if (Utility.IsExpectedDuringReflection(ex) || ex is InvalidCastException)
				{
					throw ConfigProviderFactory.ProviderException(string.Format(CultureInfo.CurrentCulture, "{0}:{1}", new object[] { provider, type.AssemblyQualifiedName }), ex);
				}
				throw;
			}
			return t2;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001E7F8 File Offset: 0x0001C9F8
		private static DataCacheException ProviderException(string invariantName, Exception e)
		{
			return new DataCacheException(ConfigProviderFactory.LogSource, 9007, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 9007, invariantName, e.Message), true);
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x0001E82D File Offset: 0x0001CA2D
		public static ConfigProviderFactory Instance
		{
			get
			{
				return ConfigProviderFactory.instance;
			}
		}

		// Token: 0x0400067B RID: 1659
		private static string LogSource = "ConfigProviderFactory";

		// Token: 0x0400067C RID: 1660
		private object lockObject = new object();

		// Token: 0x0400067D RID: 1661
		private Dictionary<string, ConfigProviderTypes> KnownProviderTypes = new Dictionary<string, ConfigProviderTypes> { 
		{
			"cscfg",
			new ConfigProviderTypes("Microsoft.ApplicationServer.Caching.Configuration.AzureClusterPropertiesReader, Microsoft.ApplicationServer.Caching.DedicatedCacheProviders", "Microsoft.ApplicationServer.Caching.Configuration.AzureClusterHostConfigurationReader, Microsoft.ApplicationServer.Caching.DedicatedCacheProviders")
		} };

		// Token: 0x0400067E RID: 1662
		private static ConfigProviderFactory instance = new ConfigProviderFactory();
	}
}
