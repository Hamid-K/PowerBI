using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200011C RID: 284
	[Serializable]
	internal class HostNodeDomainConfigurationElementCollection : ConfigurationElementCollection, ISerializable
	{
		// Token: 0x060007F4 RID: 2036 RVA: 0x00016D51 File Offset: 0x00014F51
		public HostNodeDomainConfigurationElementCollection()
		{
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001D4F0 File Offset: 0x0001B6F0
		public void InitializeAfterSerialization()
		{
			if (this.collectionElements != null)
			{
				foreach (HostNodeDomainConfigurationElement hostNodeDomainConfigurationElement in this.collectionElements)
				{
					this.Add(hostNodeDomainConfigurationElement);
				}
				this.collectionElements = null;
			}
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001D52C File Offset: 0x0001B72C
		protected HostNodeDomainConfigurationElementCollection(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.collectionElements = info.GetValue("domain", typeof(HostNodeDomainConfigurationElement[])) as HostNodeDomainConfigurationElement[];
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001D564 File Offset: 0x0001B764
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			HostNodeDomainConfigurationElement[] array = new HostNodeDomainConfigurationElement[base.Count];
			base.CopyTo(array, 0);
			info.AddValue("domain", array, array.GetType());
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0001D5A5 File Offset: 0x0001B7A5
		protected override ConfigurationElement CreateNewElement()
		{
			return new HostNodeDomainConfigurationElement();
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0001D5AC File Offset: 0x0001B7AC
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			HostNodeDomainConfigurationElement hostNodeDomainConfigurationElement = element as HostNodeDomainConfigurationElement;
			if (element == null)
			{
				throw new ArgumentException("element is not HostDomainConfigurationElement");
			}
			return hostNodeDomainConfigurationElement.DomainAddress;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001C4A3 File Offset: 0x0001A6A3
		public void Add(HostNodeDomainConfigurationElement domainConfiguration)
		{
			if (domainConfiguration == null)
			{
				throw new ArgumentNullException("domainConfiguration");
			}
			this.BaseAdd(domainConfiguration);
		}

		// Token: 0x04000657 RID: 1623
		internal const string DomainInformationProperty = "domain";

		// Token: 0x04000658 RID: 1624
		private HostNodeDomainConfigurationElement[] collectionElements;
	}
}
