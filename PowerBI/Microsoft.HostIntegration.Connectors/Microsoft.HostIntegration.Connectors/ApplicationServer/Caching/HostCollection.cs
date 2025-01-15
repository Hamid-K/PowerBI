using System;
using System.Configuration;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000114 RID: 276
	internal class HostCollection : ConfigurationElementCollection
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x00017B9F File Offset: 0x00015D9F
		public static string Name
		{
			get
			{
				return "hosts";
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001C75C File Offset: 0x0001A95C
		protected override ConfigurationElement CreateNewElement()
		{
			return new HostConfiguration();
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0001C764 File Offset: 0x0001A964
		protected override object GetElementKey(ConfigurationElement element)
		{
			IHostConfiguration hostConfiguration = (IHostConfiguration)element;
			return HostCollection.GetHostKey(hostConfiguration.Name, hostConfiguration.ServiceName);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0001C789 File Offset: 0x0001A989
		internal IHostConfiguration Get(string key)
		{
			return (IHostConfiguration)base.BaseGet(key);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00016D88 File Offset: 0x00014F88
		internal bool Delete(string name)
		{
			if (base.BaseGet(name) != null)
			{
				base.BaseRemove(name);
				return base.BaseGet(name) == null;
			}
			return false;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001C798 File Offset: 0x0001A998
		internal bool Add(IHostConfiguration host)
		{
			HostConfiguration hostConfiguration = host as HostConfiguration;
			if (base.BaseGet(this.GetElementKey(hostConfiguration)) == null)
			{
				base.BaseAdd(hostConfiguration, false);
				return true;
			}
			return false;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001C7C6 File Offset: 0x0001A9C6
		internal static string GetHostKey(string hostName, string serviceName)
		{
			return (hostName + ":" + serviceName).ToUpper(CultureInfo.InvariantCulture);
		}

		// Token: 0x0400064F RID: 1615
		private const string DELIM = ":";
	}
}
