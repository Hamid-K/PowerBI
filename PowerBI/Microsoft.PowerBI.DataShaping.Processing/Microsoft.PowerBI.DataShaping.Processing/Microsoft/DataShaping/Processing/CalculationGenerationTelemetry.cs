using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Microsoft.DataShaping.InternalContracts;

namespace Microsoft.DataShaping.Processing
{
	// Token: 0x02000008 RID: 8
	[DataContract]
	internal class CalculationGenerationTelemetry
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002380 File Offset: 0x00000580
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002388 File Offset: 0x00000588
		[DataMember(Name = "CalcTypes", EmitDefaultValue = false, Order = 10)]
		public IDictionary<int, CalculationTypeTelemetry> CalcTypes { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002391 File Offset: 0x00000591
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002399 File Offset: 0x00000599
		[DataMember(Name = "DictInfo", EmitDefaultValue = false, Order = 20)]
		public IDictionary<string, DictionaryEncodingInfo> DictionaryEncodingInfos { get; private set; }

		// Token: 0x06000020 RID: 32 RVA: 0x000023A2 File Offset: 0x000005A2
		internal void AddCalcSchemaInfo(int typeCode, string id, string dictionaryId)
		{
			this.GetTypeTelemetry(typeCode).AddCalculationSchemaInfo(id, dictionaryId);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023B4 File Offset: 0x000005B4
		internal void AddDictionaryInfo(string dictId, int valueCount, int hitCount, int missCount)
		{
			if (this.DictionaryEncodingInfos == null)
			{
				this.DictionaryEncodingInfos = new Dictionary<string, DictionaryEncodingInfo>(StringComparer.Ordinal);
			}
			DictionaryEncodingInfo dictionaryEncodingInfo = new DictionaryEncodingInfo
			{
				ValueCount = valueCount,
				HitCount = hitCount,
				MissCount = missCount
			};
			this.DictionaryEncodingInfos.Add(dictId, dictionaryEncodingInfo);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002404 File Offset: 0x00000604
		internal void IncrementTypeFallbackCount(int typeCode)
		{
			CalculationTypeTelemetry typeTelemetry = this.GetTypeTelemetry(typeCode);
			int typeOptimizedEncodingFallbackCount = typeTelemetry.TypeOptimizedEncodingFallbackCount;
			typeTelemetry.TypeOptimizedEncodingFallbackCount = typeOptimizedEncodingFallbackCount + 1;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002428 File Offset: 0x00000628
		internal void IncrementRepeatedValueCount(int typeCode)
		{
			CalculationTypeTelemetry typeTelemetry = this.GetTypeTelemetry(typeCode);
			int repeatedValueEncodedCount = typeTelemetry.RepeatedValueEncodedCount;
			typeTelemetry.RepeatedValueEncodedCount = repeatedValueEncodedCount + 1;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000244C File Offset: 0x0000064C
		internal void IncrementNullValueCount(int typeCode)
		{
			CalculationTypeTelemetry typeTelemetry = this.GetTypeTelemetry(typeCode);
			int nullValueEncodedCount = typeTelemetry.NullValueEncodedCount;
			typeTelemetry.NullValueEncodedCount = nullValueEncodedCount + 1;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002470 File Offset: 0x00000670
		private CalculationTypeTelemetry GetTypeTelemetry(int typeCode)
		{
			if (this.CalcTypes == null)
			{
				this.CalcTypes = new Dictionary<int, CalculationTypeTelemetry>();
			}
			CalculationTypeTelemetry calculationTypeTelemetry;
			if (!this.CalcTypes.TryGetValue(typeCode, out calculationTypeTelemetry))
			{
				calculationTypeTelemetry = new CalculationTypeTelemetry();
				this.CalcTypes.Add(typeCode, calculationTypeTelemetry);
			}
			return calculationTypeTelemetry;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024B4 File Offset: 0x000006B4
		internal string ToJson()
		{
			return CalculationGenerationTelemetry.Serializer.ToJsonString(this);
		}

		// Token: 0x0400003A RID: 58
		internal static int DateTimeTypeCode = typeof(DateTime).ToConceptualTypeCode();

		// Token: 0x0400003B RID: 59
		internal static int LongTypeCode = typeof(long).ToConceptualTypeCode();

		// Token: 0x0400003C RID: 60
		internal static int DoubleTypeCode = typeof(double).ToConceptualTypeCode();

		// Token: 0x0400003D RID: 61
		internal static int DecimalTypeCode = typeof(decimal).ToConceptualTypeCode();

		// Token: 0x0400003E RID: 62
		private static readonly DataContractJsonSerializerSettings SerializerSettings = new DataContractJsonSerializerSettings
		{
			UseSimpleDictionaryFormat = true
		};

		// Token: 0x0400003F RID: 63
		private static readonly DataContractJsonSerializer Serializer = new DataContractJsonSerializer(typeof(CalculationGenerationTelemetry), CalculationGenerationTelemetry.SerializerSettings);
	}
}
