using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.Processing
{
	// Token: 0x0200000A RID: 10
	[DataContract]
	internal class CalculationTypeTelemetry
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000258E File Offset: 0x0000078E
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002596 File Offset: 0x00000796
		[DataMember(Name = "NVE", EmitDefaultValue = false, Order = 10)]
		public int NullValueEncodedCount { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000259F File Offset: 0x0000079F
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000025A7 File Offset: 0x000007A7
		[DataMember(Name = "RVE", EmitDefaultValue = false, Order = 20)]
		public int RepeatedValueEncodedCount { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000025B0 File Offset: 0x000007B0
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000025B8 File Offset: 0x000007B8
		[DataMember(Name = "TOEF", EmitDefaultValue = false, Order = 30)]
		public int TypeOptimizedEncodingFallbackCount { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000025C1 File Offset: 0x000007C1
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000025C9 File Offset: 0x000007C9
		[DataMember(Name = "Calcs", EmitDefaultValue = false, Order = 40)]
		public List<CalculationSchemaInfo> CalculationSchemaInfos { get; set; }

		// Token: 0x06000038 RID: 56 RVA: 0x000025D2 File Offset: 0x000007D2
		internal void AddCalculationSchemaInfo(string id, string dictionaryId)
		{
			if (this.CalculationSchemaInfos == null)
			{
				this.CalculationSchemaInfos = new List<CalculationSchemaInfo>();
			}
			this.CalculationSchemaInfos.Add(new CalculationSchemaInfo
			{
				Id = id,
				DictionaryId = dictionaryId
			});
		}
	}
}
