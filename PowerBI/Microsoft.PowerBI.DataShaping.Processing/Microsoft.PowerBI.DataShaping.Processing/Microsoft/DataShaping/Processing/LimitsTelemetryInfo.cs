using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Microsoft.DataShaping.Processing
{
	// Token: 0x0200000E RID: 14
	[DataContract]
	internal sealed class LimitsTelemetryInfo
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000274A File Offset: 0x0000094A
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002752 File Offset: 0x00000952
		[DataMember(Name = "Limits", EmitDefaultValue = false, Order = 10)]
		internal IList<LimitTelemetryInfo> LimitTelemetry { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000275B File Offset: 0x0000095B
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002763 File Offset: 0x00000963
		[DataMember(Name = "Ad", EmitDefaultValue = false, Order = 20)]
		internal Dictionary<string, object> AdditionalInfo { get; set; }

		// Token: 0x06000057 RID: 87 RVA: 0x0000276C File Offset: 0x0000096C
		internal string ToJson()
		{
			if (this.LimitTelemetry == null && this.AdditionalInfo == null)
			{
				return null;
			}
			return LimitsTelemetryInfo.Serializer.ToJsonString(this);
		}

		// Token: 0x04000054 RID: 84
		private static readonly DataContractJsonSerializerSettings SerializerSettings = new DataContractJsonSerializerSettings
		{
			UseSimpleDictionaryFormat = true
		};

		// Token: 0x04000055 RID: 85
		private static readonly DataContractJsonSerializer Serializer = new DataContractJsonSerializer(typeof(LimitsTelemetryInfo), LimitsTelemetryInfo.SerializerSettings);
	}
}
