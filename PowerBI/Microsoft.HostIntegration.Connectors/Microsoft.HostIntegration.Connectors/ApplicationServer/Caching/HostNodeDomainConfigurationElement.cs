using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200011B RID: 283
	[Serializable]
	internal class HostNodeDomainConfigurationElement : ConfigurationElement, IHostNodeDomainConfiguration, ISerializable
	{
		// Token: 0x060007EC RID: 2028 RVA: 0x00015607 File Offset: 0x00013807
		public HostNodeDomainConfigurationElement()
		{
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0001D41E File Offset: 0x0001B61E
		public HostNodeDomainConfigurationElement(IHostNodeDomainConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			this.Name = configuration.Name;
			this.DomainAddress = configuration.DomainAddress;
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x00017DAF File Offset: 0x00015FAF
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x00017DC1 File Offset: 0x00015FC1
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				base["name"] = value;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0001D44C File Offset: 0x0001B64C
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x0001D45E File Offset: 0x0001B65E
		[ConfigurationProperty("domainAddress", IsRequired = true)]
		public Uri DomainAddress
		{
			get
			{
				return (Uri)base["domainAddress"];
			}
			set
			{
				base["domainAddress"] = value;
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001D46C File Offset: 0x0001B66C
		protected HostNodeDomainConfigurationElement(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.Name = info.GetString("name");
			this.DomainAddress = (Uri)info.GetValue("domainAddress", typeof(Uri));
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001D4BE File Offset: 0x0001B6BE
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("name", this.Name);
			info.AddValue("domainAddress", this.DomainAddress);
		}

		// Token: 0x04000655 RID: 1621
		internal const string NameProperty = "name";

		// Token: 0x04000656 RID: 1622
		internal const string DomainAddressProperty = "domainAddress";
	}
}
