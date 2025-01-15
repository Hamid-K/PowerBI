using System;
using System.Configuration;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000CD RID: 205
	internal class ClientHostCollection : ConfigurationElementCollection
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00017B9F File Offset: 0x00015D9F
		public static string Name
		{
			get
			{
				return "hosts";
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00017BA6 File Offset: 0x00015DA6
		protected override ConfigurationElement CreateNewElement()
		{
			return new ClientSideHostConfiguration();
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00017BB0 File Offset: 0x00015DB0
		protected override object GetElementKey(ConfigurationElement element)
		{
			IClientSideHostConfiguration clientSideHostConfiguration = (IClientSideHostConfiguration)element;
			return ClientHostCollection.GetHostKey(clientSideHostConfiguration.Name, clientSideHostConfiguration.ServicePort);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00017BD5 File Offset: 0x00015DD5
		internal IClientSideHostConfiguration Get(string key)
		{
			return (IClientSideHostConfiguration)base.BaseGet(key);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00016D88 File Offset: 0x00014F88
		internal bool Delete(string name)
		{
			if (base.BaseGet(name) != null)
			{
				base.BaseRemove(name);
				return base.BaseGet(name) == null;
			}
			return false;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00017BE4 File Offset: 0x00015DE4
		internal bool Add(IClientSideHostConfiguration host)
		{
			ClientSideHostConfiguration clientSideHostConfiguration = host as ClientSideHostConfiguration;
			if (base.BaseGet(this.GetElementKey(clientSideHostConfiguration)) == null)
			{
				base.BaseAdd(clientSideHostConfiguration, true);
				return true;
			}
			return false;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00017C14 File Offset: 0x00015E14
		internal static string GetHostKey(string hostName, int servicePort)
		{
			return hostName + ":" + string.Format(CultureInfo.InvariantCulture, "{0}", new object[] { servicePort });
		}

		// Token: 0x040003BE RID: 958
		private const string DELIM = ":";
	}
}
