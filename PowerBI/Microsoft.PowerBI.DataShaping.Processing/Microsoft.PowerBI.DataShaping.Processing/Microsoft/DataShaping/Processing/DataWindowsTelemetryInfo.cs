using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Microsoft.DataShaping.Processing
{
	// Token: 0x0200000C RID: 12
	[DataContract]
	internal sealed class DataWindowsTelemetryInfo
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002637 File Offset: 0x00000837
		// (set) Token: 0x06000040 RID: 64 RVA: 0x0000263F File Offset: 0x0000083F
		[DataMember(Name = "Windows", EmitDefaultValue = false, Order = 10)]
		internal IList<DataWindowTelemetryInfo> WindowTelemetry { get; set; }

		// Token: 0x06000041 RID: 65 RVA: 0x00002648 File Offset: 0x00000848
		internal string ToJson()
		{
			if (this.WindowTelemetry == null)
			{
				return null;
			}
			return DataWindowsTelemetryInfo.Serializer.ToJsonString(this);
		}

		// Token: 0x0400004B RID: 75
		private static readonly DataContractJsonSerializerSettings SerializerSettings = new DataContractJsonSerializerSettings
		{
			UseSimpleDictionaryFormat = true
		};

		// Token: 0x0400004C RID: 76
		private static readonly DataContractJsonSerializer Serializer = new DataContractJsonSerializer(typeof(DataWindowsTelemetryInfo), DataWindowsTelemetryInfo.SerializerSettings);
	}
}
