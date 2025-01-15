using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.Text;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000B3 RID: 179
	[DataContract(Name = "CacheLookupTableTransfer", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal class CacheLookupTableTransfer
	{
		// Token: 0x06000435 RID: 1077 RVA: 0x000148DA File Offset: 0x00012ADA
		internal CacheLookupTableTransfer(List<CacheLookupTableEntry> entries, VersionRanges ranges, GenerationNumber generationNumber)
		{
			this.Entries = entries;
			this.Ranges = ranges;
			this.GenerationNumber = generationNumber;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000148F7 File Offset: 0x00012AF7
		internal CacheLookupTableTransfer(LookupTableTransfer casLookupTransfer)
			: this(casLookupTransfer, null, false)
		{
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00014902 File Offset: 0x00012B02
		internal CacheLookupTableTransfer(LookupTableTransfer casLookupTransfer, IList<IHostConfiguration> config, bool useSockets)
			: this(casLookupTransfer, config, useSockets, false, VelocityPacketProperty.LookupTable)
		{
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00014910 File Offset: 0x00012B10
		internal CacheLookupTableTransfer(LookupTableTransfer casLookupTransfer, IList<IHostConfiguration> config, bool useSockets, bool useSslPorts, VelocityPacketProperty lookupTableType)
		{
			this.m_ranges = casLookupTransfer.Ranges;
			this.m_generationNumber = casLookupTransfer.GenerationNumber;
			this.m_entries = new List<CacheLookupTableEntry>();
			if (casLookupTransfer.Count > 0)
			{
				IDictionary<string, int> dictionary = null;
				IDictionary<string, int> dictionary2 = null;
				if (useSockets)
				{
					dictionary = new Dictionary<string, int>(config.Count);
					dictionary2 = new Dictionary<string, int>(config.Count);
					foreach (IHostConfiguration hostConfiguration in config)
					{
						int num = (useSslPorts ? hostConfiguration.SslSocketPort : hostConfiguration.CacheSocketPort);
						if (num != 0)
						{
							dictionary[hostConfiguration.ServiceURI] = num;
							dictionary2[hostConfiguration.ServiceURI] = hostConfiguration.NodeId;
						}
						if (hostConfiguration.CacheSocketPort != 0 && hostConfiguration.ServicePortInternal != 0)
						{
							dictionary[hostConfiguration.ServiceURIInternal] = hostConfiguration.CacheSocketPort;
							dictionary2[hostConfiguration.ServiceURIInternal] = hostConfiguration.NodeId;
						}
					}
				}
				foreach (LookupTableEntry lookupTableEntry in casLookupTransfer.Entries)
				{
					CachePartitionId cachePartitionId = new CachePartitionId(lookupTableEntry.ServiceNamespace, lookupTableEntry.Pid.LowKey, lookupTableEntry.Pid.HighKey);
					string text = lookupTableEntry.Config.Primary;
					bool flag = true;
					if (useSockets)
					{
						switch (lookupTableType)
						{
						case VelocityPacketProperty.LookupTable:
						{
							int num2;
							if (dictionary.TryGetValue(text, out num2))
							{
								text = new UriBuilder(text)
								{
									Port = num2
								}.ToString();
							}
							else
							{
								flag = false;
								CacheEventHelper.WriteInformation("CacheLookupTableTransfer", "Unable to fetch host information for given URI {0}", new object[] { text });
							}
							break;
						}
						case VelocityPacketProperty.ExternalLookupTableWithIdentifiers:
						{
							int num3;
							if (dictionary2.TryGetValue(text, out num3))
							{
								text = num3.ToString(CultureInfo.InvariantCulture);
							}
							else
							{
								flag = false;
								CacheEventHelper.WriteInformation("CacheLookupTableTransfer", "Unable to fetch host information for given URI {0}", new object[] { text });
							}
							break;
						}
						}
					}
					if (flag)
					{
						CachePartitionConfig cachePartitionConfig = new CachePartitionConfig(text, lookupTableEntry.Config.Version);
						this.m_entries.Add(new CacheLookupTableEntry(cachePartitionId, cachePartitionConfig));
					}
				}
				this.m_entries.Sort(CacheLookupTableEntry.s_comparison);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00014B94 File Offset: 0x00012D94
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x00014B9C File Offset: 0x00012D9C
		[DataMember]
		internal List<CacheLookupTableEntry> Entries
		{
			get
			{
				return this.m_entries;
			}
			private set
			{
				this.m_entries = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00014BA5 File Offset: 0x00012DA5
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x00014BAD File Offset: 0x00012DAD
		[DataMember]
		internal VersionRanges Ranges
		{
			get
			{
				return this.m_ranges;
			}
			private set
			{
				this.m_ranges = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x00014BB6 File Offset: 0x00012DB6
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x00014BBE File Offset: 0x00012DBE
		[DataMember]
		internal GenerationNumber GenerationNumber
		{
			get
			{
				return this.m_generationNumber;
			}
			private set
			{
				this.m_generationNumber = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00014BC7 File Offset: 0x00012DC7
		internal CacheGenerationNumber CacheGenerationNumber
		{
			get
			{
				return new CacheGenerationNumber(this.m_generationNumber);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00014BD4 File Offset: 0x00012DD4
		internal int Count
		{
			get
			{
				return this.m_entries.Count;
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00014BE4 File Offset: 0x00012DE4
		public LookupTableTransfer GetCasLookupTableTransfer()
		{
			List<LookupTableEntry> list = new List<LookupTableEntry>(this.m_entries.Count);
			foreach (CacheLookupTableEntry cacheLookupTableEntry in this.m_entries)
			{
				PartitionId partitionId = new PartitionId(cacheLookupTableEntry.Pid.ServiceNamespace, cacheLookupTableEntry.Pid.LowKey, cacheLookupTableEntry.Pid.HighKey);
				ServiceReplicaSet serviceReplicaSet = new ServiceReplicaSet(cacheLookupTableEntry.Config.Primary, new List<string>(0), cacheLookupTableEntry.Config.Version);
				list.Add(new LookupTableEntry(partitionId, serviceReplicaSet));
			}
			return new LookupTableTransfer(list, this.m_ranges, this.m_generationNumber);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00014CAC File Offset: 0x00012EAC
		internal Message CreateMessage()
		{
			return Message.CreateMessage(MessageVersion.Default, "http://schemas.microsoft.com/velocity/msgs/CacheLookupTableTransferAction", this);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00014CC0 File Offset: 0x00012EC0
		internal string ToShortString()
		{
			return string.Format(CultureInfo.InvariantCulture, "({0}) generation: {1}", new object[] { this.m_ranges, this.m_generationNumber });
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00014CF8 File Offset: 0x00012EF8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.m_entries.Count << 7);
			stringBuilder.Append(this.ToShortString()).AppendLine();
			foreach (CacheLookupTableEntry cacheLookupTableEntry in this.m_entries)
			{
				stringBuilder.Append(cacheLookupTableEntry.ToString()).AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000339 RID: 825
		private const string LogSource = "CacheLookupTableTransfer";

		// Token: 0x0400033A RID: 826
		private List<CacheLookupTableEntry> m_entries;

		// Token: 0x0400033B RID: 827
		private VersionRanges m_ranges;

		// Token: 0x0400033C RID: 828
		private GenerationNumber m_generationNumber;
	}
}
