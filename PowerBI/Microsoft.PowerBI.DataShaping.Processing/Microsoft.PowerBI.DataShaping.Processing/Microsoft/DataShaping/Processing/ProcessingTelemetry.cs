using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.DataShaping.Processing.QueryExecutionCommon;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.DataShaping.Processing
{
	// Token: 0x02000013 RID: 19
	[DataContract]
	internal sealed class ProcessingTelemetry
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002B38 File Offset: 0x00000D38
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00002B40 File Offset: 0x00000D40
		internal string DataShapeId { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00002B49 File Offset: 0x00000D49
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00002B51 File Offset: 0x00000D51
		internal QueryPatternKind QueryPattern { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002B5A File Offset: 0x00000D5A
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00002B62 File Offset: 0x00000D62
		[DataMember(Name = "QueryExecutionStats", EmitDefaultValue = false, Order = 10)]
		internal QueryExecutionStatistics QueryExecutionStats { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002B6B File Offset: 0x00000D6B
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00002B73 File Offset: 0x00000D73
		[DataMember(Name = "ConnCategory", EmitDefaultValue = false, Order = 20)]
		internal string ConnectionCategory { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00002B7C File Offset: 0x00000D7C
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00002B84 File Offset: 0x00000D84
		[DataMember(Name = "Rows", EmitDefaultValue = false, Order = 30)]
		internal long RowCount { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002B8D File Offset: 0x00000D8D
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00002B95 File Offset: 0x00000D95
		[DataMember(Name = "CachedRows", EmitDefaultValue = false, Order = 40)]
		internal long CachedRowCount { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002B9E File Offset: 0x00000D9E
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00002BA6 File Offset: 0x00000DA6
		[DataMember(Name = "Dsr", EmitDefaultValue = false, Order = 50)]
		internal long DsrSize { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00002BAF File Offset: 0x00000DAF
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00002BB7 File Offset: 0x00000DB7
		[DataMember(Name = "LimitsInfo", EmitDefaultValue = false, Order = 60)]
		internal LimitsTelemetryInfo LimitsTelemetry { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00002BC0 File Offset: 0x00000DC0
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00002BC8 File Offset: 0x00000DC8
		[DataMember(Name = "WindowsInfo", EmitDefaultValue = false, Order = 65)]
		internal DataWindowsTelemetryInfo WindowsTelemetry { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00002BD1 File Offset: 0x00000DD1
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00002BD9 File Offset: 0x00000DD9
		[DataMember(Name = "DsrVersion", EmitDefaultValue = false, Order = 70)]
		public int DsrVersion { get; internal set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00002BE2 File Offset: 0x00000DE2
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00002BEA File Offset: 0x00000DEA
		[DataMember(Name = "DataShapes", EmitDefaultValue = false, Order = 80)]
		public IDictionary<string, ProcessingDataShapeTelemetry> DataShapes { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00002BF3 File Offset: 0x00000DF3
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00002BFB File Offset: 0x00000DFB
		[DataMember(Name = "CalcGen", EmitDefaultValue = false, Order = 90)]
		public CalculationGenerationTelemetry CalculationTelemetry { get; set; }

		// Token: 0x0600009F RID: 159 RVA: 0x00002C04 File Offset: 0x00000E04
		internal void UpdateDataShapeTelemetry(string id, ProcessingDataShapeTelemetry dsStats)
		{
			if (string.CompareOrdinal(id, this.DataShapeId) != 0)
			{
				return;
			}
			if (this.DataShapes == null)
			{
				this.DataShapes = new Dictionary<string, ProcessingDataShapeTelemetry>();
			}
			this.DataShapes[id] = dsStats;
		}
	}
}
