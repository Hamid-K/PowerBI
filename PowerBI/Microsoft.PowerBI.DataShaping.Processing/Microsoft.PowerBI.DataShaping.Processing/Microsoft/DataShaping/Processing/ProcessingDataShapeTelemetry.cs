using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Microsoft.DataShaping.Processing
{
	// Token: 0x02000010 RID: 16
	[DataContract]
	internal sealed class ProcessingDataShapeTelemetry
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002949 File Offset: 0x00000B49
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002951 File Offset: 0x00000B51
		[DataMember(Name = "PrimaryCount", EmitDefaultValue = false, Order = 20)]
		public long PrimaryCount { get; internal set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000295A File Offset: 0x00000B5A
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002962 File Offset: 0x00000B62
		[DataMember(Name = "SecondaryCount", EmitDefaultValue = false, Order = 30)]
		public long SecondaryCount { get; internal set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000074 RID: 116 RVA: 0x0000296B File Offset: 0x00000B6B
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002973 File Offset: 0x00000B73
		[DataMember(Name = "IntersectionCount", EmitDefaultValue = false, Order = 40)]
		public long IntersectionCount { get; internal set; }

		// Token: 0x06000076 RID: 118 RVA: 0x0000297C File Offset: 0x00000B7C
		internal static string ToJson(IDictionary<string, ProcessingDataShapeTelemetry> dataShapes)
		{
			return ProcessingDataShapeTelemetry.Serializer.ToJsonString(dataShapes);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000298C File Offset: 0x00000B8C
		internal void IncrementIntersectionCount()
		{
			long intersectionCount = this.IntersectionCount;
			this.IntersectionCount = intersectionCount + 1L;
		}

		// Token: 0x04000067 RID: 103
		private static readonly DataContractJsonSerializerSettings SerializerSettings = new DataContractJsonSerializerSettings
		{
			UseSimpleDictionaryFormat = true
		};

		// Token: 0x04000068 RID: 104
		private static readonly DataContractJsonSerializer Serializer = new DataContractJsonSerializer(typeof(IDictionary<string, ProcessingDataShapeTelemetry>), ProcessingDataShapeTelemetry.SerializerSettings);
	}
}
