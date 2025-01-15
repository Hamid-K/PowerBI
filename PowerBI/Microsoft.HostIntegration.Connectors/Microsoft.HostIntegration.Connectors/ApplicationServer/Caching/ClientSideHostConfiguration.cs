using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200011A RID: 282
	[Serializable]
	internal class ClientSideHostConfiguration : ConfigurationElement, IClientSideHostConfiguration, ISerializable
	{
		// Token: 0x060007E4 RID: 2020 RVA: 0x00015607 File Offset: 0x00013807
		public ClientSideHostConfiguration()
		{
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00017DAF File Offset: 0x00015FAF
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x00017DC1 File Offset: 0x00015FC1
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

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x0001D388 File Offset: 0x0001B588
		// (set) Token: 0x060007E8 RID: 2024 RVA: 0x0001D39A File Offset: 0x0001B59A
		[ConfigurationProperty("cachePort", DefaultValue = 22233, IsRequired = true)]
		[IntegerValidator(MinValue = 1)]
		public int ServicePort
		{
			get
			{
				return (int)base["cachePort"];
			}
			set
			{
				base["cachePort"] = value;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x0001D3AD File Offset: 0x0001B5AD
		public string ServiceURI
		{
			get
			{
				return Utility.GetServiceUri(this.Name, this.ServicePort, TransportProtocol.NetTcp);
			}
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001D3C1 File Offset: 0x0001B5C1
		public ClientSideHostConfiguration(SerializationInfo info, StreamingContext context)
		{
			this.Name = info.GetString("name");
			this.ServicePort = (int)info.GetValue("cachePort", typeof(int));
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0001D3FA File Offset: 0x0001B5FA
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("name", this.Name);
			info.AddValue("cachePort", this.ServicePort);
		}

		// Token: 0x04000653 RID: 1619
		internal const string NAME = "name";

		// Token: 0x04000654 RID: 1620
		internal const string SERVICE_PORT = "cachePort";
	}
}
