using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Threading;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.Model;
using Microsoft.DataShaping.Processing;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.DataShapeQueryGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x02000013 RID: 19
	[DataContract]
	internal class ExecuteSemanticQueryTelemetry
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00002E9F File Offset: 0x0000109F
		public static ExecuteSemanticQueryTelemetry ForProcessing()
		{
			return new ExecuteSemanticQueryTelemetry
			{
				Processing = new ProcessingTelemetry()
			};
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002EB1 File Offset: 0x000010B1
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002EB9 File Offset: 0x000010B9
		[DataMember(Name = "ID", EmitDefaultValue = true, Order = 10)]
		public string QueryId { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002EC2 File Offset: 0x000010C2
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002ECA File Offset: 0x000010CA
		[DataMember(Name = "DSQG", EmitDefaultValue = false, Order = 20)]
		public DataShapeGenerationTelemetry DataShapeGeneration { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002ED3 File Offset: 0x000010D3
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002EDB File Offset: 0x000010DB
		[DataMember(Name = "DSQT", EmitDefaultValue = false, Order = 30)]
		public DataShapeQueryTranslationTelemetry DataShapeQueryTranslation { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002EE4 File Offset: 0x000010E4
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002EEC File Offset: 0x000010EC
		[DataMember(Name = "PROC", EmitDefaultValue = false, Order = 40)]
		public ProcessingTelemetry Processing { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002EF5 File Offset: 0x000010F5
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002EFD File Offset: 0x000010FD
		[DataMember(Name = "ERR", EmitDefaultValue = false, Order = 50)]
		public string Exception { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002F06 File Offset: 0x00001106
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002F0E File Offset: 0x0000110E
		[DataMember(Name = "CANCEL", EmitDefaultValue = false, Order = 50)]
		public bool Cancelled { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002F17 File Offset: 0x00001117
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002F1F File Offset: 0x0000111F
		[DataMember(Name = "MODEL", EmitDefaultValue = false, Order = 60)]
		public ModelTelemetry Model { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002F28 File Offset: 0x00001128
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00002F30 File Offset: 0x00001130
		[DataMember(Name = "METRICS", EmitDefaultValue = false, Order = 70)]
		public ExecutionMetricsTelemetry Metrics { get; set; }

		// Token: 0x06000073 RID: 115 RVA: 0x00002F39 File Offset: 0x00001139
		public void RegisterException(Exception ex)
		{
			this.Exception = ex.FormatExceptionDetails(false);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002F48 File Offset: 0x00001148
		public void SetCancelStatus(CancellationToken cancelToken)
		{
			this.Cancelled = cancelToken.IsCancellationRequested;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002F58 File Offset: 0x00001158
		internal void Write(ITelemetryService telemetry, ITracer tracer)
		{
			string text = this.SerializeForTelemetry(delegate(string msg)
			{
				tracer.SanitizedTrace(TraceLevel.Warning, msg);
			});
			telemetry.FireSanitizedEvent(DataShapingEvents.ExecuteSemanticQueryInfo, new object[] { text });
		}
	}
}
