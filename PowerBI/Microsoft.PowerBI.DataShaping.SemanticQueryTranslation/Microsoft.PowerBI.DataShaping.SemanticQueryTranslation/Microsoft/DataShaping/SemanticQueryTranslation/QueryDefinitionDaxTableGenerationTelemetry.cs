using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.DataShapeQueryGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation;

namespace Microsoft.DataShaping.SemanticQueryTranslation
{
	// Token: 0x02000010 RID: 16
	[DataContract]
	internal class QueryDefinitionDaxTableGenerationTelemetry
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002ED6 File Offset: 0x000010D6
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002EDE File Offset: 0x000010DE
		[DataMember(Name = "DSQG", EmitDefaultValue = false, Order = 10)]
		public DataShapeGenerationTelemetry DataShapeGeneration { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002EE7 File Offset: 0x000010E7
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002EEF File Offset: 0x000010EF
		[DataMember(Name = "DSQT", EmitDefaultValue = false, Order = 20)]
		public DataShapeQueryTranslationTelemetry DataShapeQueryTranslation { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002EF8 File Offset: 0x000010F8
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002F00 File Offset: 0x00001100
		[DataMember(Name = "ERR", EmitDefaultValue = false, Order = 30)]
		public string Exception { get; set; }

		// Token: 0x0600006B RID: 107 RVA: 0x00002F09 File Offset: 0x00001109
		public void RegisterException(Exception ex)
		{
			this.Exception = ex.FormatExceptionDetails(false);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002F18 File Offset: 0x00001118
		internal virtual void Write(ITelemetryService telemetry, ITracer tracer)
		{
			string text = this.SerializeForTelemetry(delegate(string msg)
			{
				tracer.SanitizedTrace(TraceLevel.Warning, msg);
			});
			telemetry.FireSanitizedEvent(DataShapingEvents.QueryDefinitionDaxTableGenerationInfo, new object[] { text });
		}
	}
}
