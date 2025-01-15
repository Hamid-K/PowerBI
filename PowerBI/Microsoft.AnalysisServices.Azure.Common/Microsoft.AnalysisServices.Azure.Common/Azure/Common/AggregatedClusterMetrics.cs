using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.AnalysisServices.Azure.Common.Utils;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000042 RID: 66
	[DataContract]
	public sealed class AggregatedClusterMetrics
	{
		// Token: 0x06000369 RID: 873 RVA: 0x0000EB5E File Offset: 0x0000CD5E
		public AggregatedClusterMetrics(int availableMemoryInMB, int countOfBoundServices, int countOfUnBoundServices, Dictionary<string, int> countOfThreadPools, DatabaseType databaseType, DateTime timestamp)
		{
			this.Timestamp = timestamp;
			this.CountOfBoundServices = countOfBoundServices;
			this.CountOfUnBoundServices = countOfUnBoundServices;
			this.AvailableMemoryInMB = availableMemoryInMB;
			this.ASEngineThreadPoolPerformanceCountersDictionary = countOfThreadPools;
			this.DatabaseType = databaseType;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000EB93 File Offset: 0x0000CD93
		public AggregatedClusterMetrics(int availableMemoryInMB, int countOfBoundServices, int countOfUnBoundServices, DatabaseType databaseType, DateTime timestamp)
			: this(availableMemoryInMB, countOfBoundServices, countOfUnBoundServices, null, databaseType, timestamp)
		{
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000EBA3 File Offset: 0x0000CDA3
		// (set) Token: 0x0600036C RID: 876 RVA: 0x0000EBAB File Offset: 0x0000CDAB
		[DataMember]
		public int CountOfBoundServices { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000EBB4 File Offset: 0x0000CDB4
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0000EBBC File Offset: 0x0000CDBC
		[DataMember]
		public int CountOfUnBoundServices { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000EBC5 File Offset: 0x0000CDC5
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0000EBCD File Offset: 0x0000CDCD
		[DataMember]
		public int AvailableMemoryInMB { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000EBD6 File Offset: 0x0000CDD6
		// (set) Token: 0x06000372 RID: 882 RVA: 0x0000EBDE File Offset: 0x0000CDDE
		[DataMember]
		public Dictionary<string, int> ASEngineThreadPoolPerformanceCountersDictionary { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000EBE7 File Offset: 0x0000CDE7
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0000EBEF File Offset: 0x0000CDEF
		[DataMember]
		public DateTime Timestamp { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000EBF8 File Offset: 0x0000CDF8
		// (set) Token: 0x06000376 RID: 886 RVA: 0x0000EC00 File Offset: 0x0000CE00
		[DataMember]
		public DatabaseType DatabaseType { get; set; }

		// Token: 0x06000377 RID: 887 RVA: 0x0000EC0C File Offset: 0x0000CE0C
		public byte[] Serialize()
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new DataContractSerializer(typeof(AggregatedClusterMetrics)).WriteObject(memoryStream, this);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000EC5C File Offset: 0x0000CE5C
		public static AggregatedClusterMetrics Deserialize(byte[] data)
		{
			AggregatedClusterMetrics aggregatedClusterMetrics;
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				aggregatedClusterMetrics = (AggregatedClusterMetrics)new DataContractSerializer(typeof(AggregatedClusterMetrics)).ReadObject(memoryStream);
			}
			return aggregatedClusterMetrics;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000ECA8 File Offset: 0x0000CEA8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AggregatedClusterMetrics:[CountOfBoundServices={0};CountOfUnBoundServices={1};AvailableMemoryInMB={2};".FormatWithInvariantCulture(new object[] { this.CountOfBoundServices, this.CountOfUnBoundServices, this.AvailableMemoryInMB }));
			if (this.ASEngineThreadPoolPerformanceCountersDictionary != null)
			{
				stringBuilder.Append(CommonUtils.BuildThreadPoolCountersString(this.ASEngineThreadPoolPerformanceCountersDictionary));
			}
			stringBuilder.Append("Timestamp={0};DatabaseType={1};]".FormatWithInvariantCulture(new object[] { this.Timestamp, this.DatabaseType }));
			return stringBuilder.ToString();
		}
	}
}
