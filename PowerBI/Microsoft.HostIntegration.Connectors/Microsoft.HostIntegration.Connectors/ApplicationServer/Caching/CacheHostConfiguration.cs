using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002E4 RID: 740
	internal class CacheHostConfiguration
	{
		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x0005366D File Offset: 0x0005186D
		// (set) Token: 0x06001B6F RID: 7023 RVA: 0x00053688 File Offset: 0x00051888
		public string ServiceName
		{
			get
			{
				if (string.IsNullOrEmpty(this._serviceName))
				{
					return "AppFabricCachingService";
				}
				return this._serviceName;
			}
			set
			{
				this._serviceName = value;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x00053691 File Offset: 0x00051891
		// (set) Token: 0x06001B71 RID: 7025 RVA: 0x00053699 File Offset: 0x00051899
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001B72 RID: 7026 RVA: 0x000536A2 File Offset: 0x000518A2
		// (set) Token: 0x06001B73 RID: 7027 RVA: 0x000536AA File Offset: 0x000518AA
		public string FriendlyNodeId { get; set; }

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001B74 RID: 7028 RVA: 0x000536B3 File Offset: 0x000518B3
		// (set) Token: 0x06001B75 RID: 7029 RVA: 0x000536BB File Offset: 0x000518BB
		public int NodeId { get; set; }

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001B76 RID: 7030 RVA: 0x000536C4 File Offset: 0x000518C4
		// (set) Token: 0x06001B77 RID: 7031 RVA: 0x000536CC File Offset: 0x000518CC
		public int ServicePort { get; set; }

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001B78 RID: 7032 RVA: 0x000536D5 File Offset: 0x000518D5
		// (set) Token: 0x06001B79 RID: 7033 RVA: 0x000536DD File Offset: 0x000518DD
		public bool UseSockets { get; set; }

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001B7A RID: 7034 RVA: 0x000536E6 File Offset: 0x000518E6
		// (set) Token: 0x06001B7B RID: 7035 RVA: 0x000536EE File Offset: 0x000518EE
		public int CacheSocketPort { get; set; }

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001B7C RID: 7036 RVA: 0x000536F7 File Offset: 0x000518F7
		// (set) Token: 0x06001B7D RID: 7037 RVA: 0x000536FF File Offset: 0x000518FF
		public int CacheDiscoveryPort { get; set; }

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x00053708 File Offset: 0x00051908
		// (set) Token: 0x06001B7F RID: 7039 RVA: 0x00053710 File Offset: 0x00051910
		public int SslSocketPort { get; set; }

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001B80 RID: 7040 RVA: 0x00053719 File Offset: 0x00051919
		// (set) Token: 0x06001B81 RID: 7041 RVA: 0x00053721 File Offset: 0x00051921
		public int SslDiscoveryPort { get; set; }

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x0005372A File Offset: 0x0005192A
		// (set) Token: 0x06001B83 RID: 7043 RVA: 0x00053732 File Offset: 0x00051932
		public int RestPort { get; set; }

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001B84 RID: 7044 RVA: 0x0005373B File Offset: 0x0005193B
		// (set) Token: 0x06001B85 RID: 7045 RVA: 0x00053743 File Offset: 0x00051943
		public int RestSslPort { get; set; }

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x0005374C File Offset: 0x0005194C
		// (set) Token: 0x06001B87 RID: 7047 RVA: 0x00053754 File Offset: 0x00051954
		public int ServiceSslPort { get; set; }

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001B88 RID: 7048 RVA: 0x0005375D File Offset: 0x0005195D
		// (set) Token: 0x06001B89 RID: 7049 RVA: 0x00053765 File Offset: 0x00051965
		public int ServicePortInternal { get; set; }

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001B8A RID: 7050 RVA: 0x0005376E File Offset: 0x0005196E
		// (set) Token: 0x06001B8B RID: 7051 RVA: 0x00053776 File Offset: 0x00051976
		public int ClusterPort { get; set; }

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001B8C RID: 7052 RVA: 0x0005377F File Offset: 0x0005197F
		// (set) Token: 0x06001B8D RID: 7053 RVA: 0x00053787 File Offset: 0x00051987
		public int ArbitrationPort { get; set; }

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001B8E RID: 7054 RVA: 0x00053790 File Offset: 0x00051990
		// (set) Token: 0x06001B8F RID: 7055 RVA: 0x00053798 File Offset: 0x00051998
		public int ReplicationPort { get; set; }

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x000537A1 File Offset: 0x000519A1
		// (set) Token: 0x06001B91 RID: 7057 RVA: 0x000537A9 File Offset: 0x000519A9
		public bool IsQuorumHost { get; set; }

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001B92 RID: 7058 RVA: 0x000537B2 File Offset: 0x000519B2
		// (set) Token: 0x06001B93 RID: 7059 RVA: 0x000537BA File Offset: 0x000519BA
		public int? CacheDataSizeInPercent { get; set; }

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x000537C3 File Offset: 0x000519C3
		// (set) Token: 0x06001B95 RID: 7061 RVA: 0x000537CB File Offset: 0x000519CB
		public int? NodeEvictionHWM { get; set; }

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001B96 RID: 7062 RVA: 0x000537D4 File Offset: 0x000519D4
		// (set) Token: 0x06001B97 RID: 7063 RVA: 0x000537DC File Offset: 0x000519DC
		public int? NodeEvictionLWM { get; set; }

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001B98 RID: 7064 RVA: 0x000537E5 File Offset: 0x000519E5
		// (set) Token: 0x06001B99 RID: 7065 RVA: 0x000537ED File Offset: 0x000519ED
		public int? NodeThrottlingInternalHighThreshold { get; set; }

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001B9A RID: 7066 RVA: 0x000537F6 File Offset: 0x000519F6
		// (set) Token: 0x06001B9B RID: 7067 RVA: 0x000537FE File Offset: 0x000519FE
		public int? NodeThrottlingInternalLowThreshold { get; set; }

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001B9C RID: 7068 RVA: 0x00053807 File Offset: 0x00051A07
		// (set) Token: 0x06001B9D RID: 7069 RVA: 0x0005380F File Offset: 0x00051A0F
		public int? NodeThrottlingExternalHighThreshold { get; set; }

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001B9E RID: 7070 RVA: 0x00053818 File Offset: 0x00051A18
		// (set) Token: 0x06001B9F RID: 7071 RVA: 0x00053820 File Offset: 0x00051A20
		public int? NodeThrottlingExternalLowThreshold { get; set; }

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x00053829 File Offset: 0x00051A29
		// (set) Token: 0x06001BA1 RID: 7073 RVA: 0x00053831 File Offset: 0x00051A31
		public int? CacheEvictionHWM { get; set; }

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x0005383A File Offset: 0x00051A3A
		// (set) Token: 0x06001BA3 RID: 7075 RVA: 0x00053842 File Offset: 0x00051A42
		public int? CacheEvictionLWM { get; set; }

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x0005384B File Offset: 0x00051A4B
		// (set) Token: 0x06001BA5 RID: 7077 RVA: 0x00053853 File Offset: 0x00051A53
		public int? AverageCacheItemSizeInBytes { get; set; }

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001BA6 RID: 7078 RVA: 0x0005385C File Offset: 0x00051A5C
		public ReadOnlyCollection<IHostNodeDomainConfiguration> DomainInformation
		{
			get
			{
				return new ReadOnlyCollection<IHostNodeDomainConfiguration>(this.domainInformation);
			}
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x0005386C File Offset: 0x00051A6C
		public void AddDomain(string name, string domainAddress)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (string.IsNullOrEmpty(domainAddress))
			{
				throw new ArgumentNullException("domainAddress");
			}
			HostNodeDomainConfigurationElement hostNodeDomainConfigurationElement = new HostNodeDomainConfigurationElement();
			hostNodeDomainConfigurationElement.Name = name;
			hostNodeDomainConfigurationElement.DomainAddress = new Uri(domainAddress);
			this.domainInformation.Add(hostNodeDomainConfigurationElement);
		}

		// Token: 0x04000E92 RID: 3730
		private List<IHostNodeDomainConfiguration> domainInformation = new List<IHostNodeDomainConfiguration>();

		// Token: 0x04000E93 RID: 3731
		private string _serviceName;

		// Token: 0x04000E94 RID: 3732
		private string _name;
	}
}
