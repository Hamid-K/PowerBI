using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002F2 RID: 754
	internal class CloudUtility
	{
		// Token: 0x06001C52 RID: 7250 RVA: 0x00055934 File Offset: 0x00053B34
		public static string GetLogLocationForCurrentInstance()
		{
			string logLocationForCurrentInstance = CloudUtility.CloudProvider.GetLogLocationForCurrentInstance();
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string>("DataCache.ConfigManager", "Azure Log location - {0}", logLocationForCurrentInstance);
			}
			return logLocationForCurrentInstance;
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x00055968 File Offset: 0x00053B68
		public static TraceLevel GetLogLevelFromCloudConfiguration()
		{
			int logLevel = CloudUtility.CloudProvider.GetLogLevel();
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose("DataCache.ConfigManager", "Azure Log level - " + logLevel);
			}
			return logLevel + TraceLevel.Error;
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x000559A8 File Offset: 0x00053BA8
		public static string GetCurrentEndpointAddress()
		{
			string currentEndpointAddress = CloudUtility.CloudProvider.GetCurrentEndpointAddress();
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string>("DataCache.ConfigManager", "Azure Returning address - {0}", currentEndpointAddress);
			}
			return currentEndpointAddress;
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x000559DC File Offset: 0x00053BDC
		public static Uri GetCurrentInternalEndpointUri()
		{
			return CloudUtility.CloudProvider.GetCurrentInternalEndpointUri();
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x000559F5 File Offset: 0x00053BF5
		public static string GetCurrentInstanceId()
		{
			return CloudUtility.CloudProvider.GetCurrentInstanceId();
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x00055A04 File Offset: 0x00053C04
		public static List<IHostConfiguration> GetRoleInstances()
		{
			IEnumerable<CacheHostConfiguration> allHosts = CloudUtility.CloudProvider.GetAllHosts();
			List<IHostConfiguration> list = new List<IHostConfiguration>();
			foreach (CacheHostConfiguration cacheHostConfiguration in allHosts)
			{
				IHostConfiguration hostConfiguration = new HostConfiguration(cacheHostConfiguration);
				list.Add(hostConfiguration);
				string text = " My neighbours .. {0} service: {1} sep: {2}, sep internal: {7},  cep: {3} aep: {4} rep: {5} nodeID: {6}";
				object[] array = new object[] { cacheHostConfiguration.Name, hostConfiguration.ServiceURI, cacheHostConfiguration.ServicePort, cacheHostConfiguration.ClusterPort, cacheHostConfiguration.ArbitrationPort, cacheHostConfiguration.ReplicationPort, hostConfiguration.NodeId, cacheHostConfiguration.ServicePortInternal };
				EventLogWriter.WriteVerbose("DataCache.ConfigManager", "Azure Host in list - " + text, array);
			}
			return list;
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x00055B0C File Offset: 0x00053D0C
		public static IHostConfiguration GetHost(string hostName, int servicePort)
		{
			CacheHostConfiguration host = CloudUtility.CloudProvider.GetHost(hostName, servicePort);
			return new HostConfiguration(host);
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x00055B2C File Offset: 0x00053D2C
		public static IHostConfiguration GetHost(string hostName, string serviceName)
		{
			CacheHostConfiguration host = CloudUtility.CloudProvider.GetHost(hostName, serviceName);
			return new HostConfiguration(host);
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x00055B4C File Offset: 0x00053D4C
		// (set) Token: 0x06001C5B RID: 7259 RVA: 0x00055B53 File Offset: 0x00053D53
		public static ICacheUsagePublisher CacheUsagePublisher { get; set; }

		// Token: 0x06001C5C RID: 7260 RVA: 0x00055B5C File Offset: 0x00053D5C
		public static ClientLocationType? GetClientLocationForIP(string ipAddress, int port)
		{
			if (CloudUtility.CacheUsagePublisher != null)
			{
				return CloudUtility.CacheUsagePublisher.GetClientLocationForIP(ipAddress, port);
			}
			return null;
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x00055B88 File Offset: 0x00053D88
		public static bool IsServiceRunningOnDevfabric()
		{
			string currentInstanceId = CloudUtility.GetCurrentInstanceId();
			return currentInstanceId.StartsWith("deployment", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x00055BA8 File Offset: 0x00053DA8
		internal static ICloudProvider CloudProvider
		{
			get
			{
				if (CloudUtility._cloudProvider == null && !string.IsNullOrEmpty(CloudUtility.ProviderType))
				{
					try
					{
						Type type = Type.GetType(CloudUtility.ProviderType, true);
						CloudUtility._cloudProvider = (ICloudProvider)Activator.CreateInstance(type);
					}
					catch (Exception ex)
					{
						EventLogWriter.WriteWarning("DataCache.ConfigManager", "Error while loading assembly = {0}, Exception= {1}", new object[]
						{
							CloudUtility.ProviderType,
							ex
						});
						if (CloudUtility.IsExpectedDuringTypeLoad(ex))
						{
							int num = 9007;
							string @string = GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, num, CloudUtility.ProviderType, ex.Message);
							throw new DataCacheException("DataCache.ConfigManager", num, @string, ex, true);
						}
						throw;
					}
				}
				return CloudUtility._cloudProvider;
			}
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x00055C5C File Offset: 0x00053E5C
		private static bool IsExpectedDuringTypeLoad(Exception e)
		{
			return e is ArgumentNullException || e is TargetInvocationException || e is ArgumentException || e is TypeLoadException || e is FileNotFoundException || e is FileLoadException || e is BadImageFormatException;
		}

		// Token: 0x04000F12 RID: 3858
		private const string devfrabricInstanceIdPrefix = "deployment";

		// Token: 0x04000F13 RID: 3859
		internal static string ProviderType = string.Empty;

		// Token: 0x04000F14 RID: 3860
		public static bool IsVASDeployment;

		// Token: 0x04000F15 RID: 3861
		private static ICloudProvider _cloudProvider;
	}
}
