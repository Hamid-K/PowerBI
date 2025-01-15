using System;
using Microsoft.InfoNav.Data.Contracts.DataShapeResult;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x02000049 RID: 73
	internal sealed class DsrWriterOptions
	{
		// Token: 0x0600018D RID: 397 RVA: 0x00004CEC File Offset: 0x00002EEC
		internal DsrWriterOptions(DsrNames names, DsrVersion version, bool skipNullIntersectionAndCalculations, bool skipIntersectionIds, bool writeCalcsAsParentProperties, bool writeCalcsAsArrays, bool dataMemberIdAsPropertyName, bool writeCalcsRepeatedValueEncoded, bool writeCalcsNullValueEncoded, int dictionaryEncodingCapacity, DsrWriterValueEncodingConfiguration calculationValueEncodingOptions)
		{
			this.Names = names;
			this.Version = version;
			this.SkipNullIntersectionAndCalculations = skipNullIntersectionAndCalculations;
			this.SkipIntersectionIds = skipIntersectionIds;
			this.WriteCalcsAsParentProperties = writeCalcsAsParentProperties;
			this.WriteCalcsAsArrays = writeCalcsAsArrays;
			this.DataMemberIdAsPropertyName = dataMemberIdAsPropertyName;
			this.WriteCalcsRepeatedValueEncoded = writeCalcsRepeatedValueEncoded;
			this.WriteCalcsNullValueEncoded = writeCalcsNullValueEncoded;
			this.WriteCalcsDictionaryEncoded = dictionaryEncodingCapacity != 0;
			this.CalculationValueEncodingOptions = calculationValueEncodingOptions;
			this.DictionaryEncodingCapacity = dictionaryEncodingCapacity;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00004D5F File Offset: 0x00002F5F
		internal bool WriteOptimizedCalculations
		{
			get
			{
				return this.WriteCalcsAsParentProperties || this.WriteCalcsAsArrays;
			}
		}

		// Token: 0x040000B4 RID: 180
		private const int DisabledDictionaryCapacity = 0;

		// Token: 0x040000B5 RID: 181
		private const int DefaultDictionaryCapacity = 100;

		// Token: 0x040000B6 RID: 182
		internal static readonly DsrWriterOptions V1 = new DsrWriterOptions(DsrNames.V1, DsrVersion.V1, false, false, false, false, false, false, false, 0, null);

		// Token: 0x040000B7 RID: 183
		internal static readonly DsrWriterOptions V2 = new DsrWriterOptions(DsrNames.V2, DsrVersion.V2, true, true, true, true, true, true, true, 100, DsrWriterValueEncodingConfiguration.V2);

		// Token: 0x040000B8 RID: 184
		internal readonly DsrVersion Version;

		// Token: 0x040000B9 RID: 185
		internal readonly DsrNames Names;

		// Token: 0x040000BA RID: 186
		internal readonly bool SkipNullIntersectionAndCalculations;

		// Token: 0x040000BB RID: 187
		internal readonly bool SkipIntersectionIds;

		// Token: 0x040000BC RID: 188
		internal readonly bool WriteCalcsAsParentProperties;

		// Token: 0x040000BD RID: 189
		internal readonly bool WriteCalcsAsArrays;

		// Token: 0x040000BE RID: 190
		internal readonly bool DataMemberIdAsPropertyName;

		// Token: 0x040000BF RID: 191
		internal readonly bool WriteCalcsRepeatedValueEncoded;

		// Token: 0x040000C0 RID: 192
		internal readonly bool WriteCalcsNullValueEncoded;

		// Token: 0x040000C1 RID: 193
		internal readonly bool WriteCalcsDictionaryEncoded;

		// Token: 0x040000C2 RID: 194
		internal readonly int DictionaryEncodingCapacity;

		// Token: 0x040000C3 RID: 195
		internal readonly DsrWriterValueEncodingConfiguration CalculationValueEncodingOptions;
	}
}
