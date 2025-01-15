using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002B8 RID: 696
	internal class ServerIdentityProvider : IEndpointIdentityProvider
	{
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001983 RID: 6531 RVA: 0x0004BBC7 File Offset: 0x00049DC7
		private DataCacheSecurityMode ClusterSecurityMode
		{
			get
			{
				if (this._configManager != null)
				{
					return this._configManager.AdvancedProperties.SecurityProperties.DataCacheSecurityMode;
				}
				return this._clusterConfigurationReader.AdvancedProperties.SecurityProperties.DataCacheSecurityMode;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001984 RID: 6532 RVA: 0x0004BBFC File Offset: 0x00049DFC
		private IEnumerable<IHostConfiguration> AllHosts
		{
			get
			{
				if (this._configManager != null)
				{
					return this._configManager.GetListOfHosts();
				}
				return this._clusterConfigurationReader.GetListOfHosts();
			}
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x0004BC1D File Offset: 0x00049E1D
		internal ServerIdentityProvider(IClusterConfigurationReader clusterConfigurationReader)
		{
			if (clusterConfigurationReader == null)
			{
				throw new ArgumentNullException("clusterConfigurationReader");
			}
			this._clusterConfigurationReader = clusterConfigurationReader;
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0004BC45 File Offset: 0x00049E45
		internal ServerIdentityProvider(ServiceConfigurationManager configurationManager)
		{
			if (configurationManager == null)
			{
				throw new ArgumentNullException("configurationManager");
			}
			this._configManager = configurationManager;
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0004BC70 File Offset: 0x00049E70
		public EndpointIdentity GetEndpointIdentity(string targetHost, int targetPort)
		{
			EndpointIdentity endpointIdentity;
			try
			{
				if (this.ClusterSecurityMode != DataCacheSecurityMode.Transport)
				{
					endpointIdentity = null;
				}
				else
				{
					IHostConfiguration hostConfiguration = this.AllHosts.FirstOrDefault((IHostConfiguration host) => host.Name.Equals(targetHost, StringComparison.OrdinalIgnoreCase));
					if (hostConfiguration == null)
					{
						if (Provider.IsEnabled(TraceLevel.Error))
						{
							EventLogWriter.WriteError(this._logSource, " Host : {0} not found in the loaded Cluster Configuration.", new object[] { targetHost });
						}
						endpointIdentity = null;
					}
					else
					{
						endpointIdentity = ServerIdentityProvider.CreateUpn(hostConfiguration.Account);
					}
				}
			}
			catch (ConfigStoreException ex)
			{
				throw new CommunicationException("Problem connecting to the cache configuration store", ex);
			}
			return endpointIdentity;
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x0004BD1C File Offset: 0x00049F1C
		private static EndpointIdentity CreateUpn(string account)
		{
			if (string.IsNullOrEmpty(account) || account.EndsWith("$", StringComparison.OrdinalIgnoreCase))
			{
				return null;
			}
			string[] array = account.Split(new char[] { '\\' });
			if (array.Length != 2)
			{
				throw new ArgumentException("account not of type domain\\user");
			}
			string text = array[1];
			string text2 = array[0];
			return EndpointIdentity.CreateUpnIdentity(text + '@' + text2);
		}

		// Token: 0x04000DCB RID: 3531
		private IClusterConfigurationReader _clusterConfigurationReader;

		// Token: 0x04000DCC RID: 3532
		private ServiceConfigurationManager _configManager;

		// Token: 0x04000DCD RID: 3533
		private string _logSource = "DistributedCache.ServiceIdentityProvider";
	}
}
