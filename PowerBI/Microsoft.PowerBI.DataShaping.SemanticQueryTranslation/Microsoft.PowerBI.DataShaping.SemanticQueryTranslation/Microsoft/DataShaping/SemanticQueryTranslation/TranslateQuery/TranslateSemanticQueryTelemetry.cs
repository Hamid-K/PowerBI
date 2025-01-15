using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Threading;
using Microsoft.DataShaping.InternalContracts.Model;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.DataShaping.SemanticQueryTranslation.TranslateQuery
{
	// Token: 0x0200001D RID: 29
	[DataContract]
	internal sealed class TranslateSemanticQueryTelemetry : QueryDefinitionDaxTableGenerationTelemetry
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000054BF File Offset: 0x000036BF
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x000054C7 File Offset: 0x000036C7
		[DataMember(Name = "ID", EmitDefaultValue = true, Order = 10)]
		public string QueryId { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x000054D0 File Offset: 0x000036D0
		// (set) Token: 0x060000EA RID: 234 RVA: 0x000054D8 File Offset: 0x000036D8
		[DataMember(Name = "CANCEL", EmitDefaultValue = false, Order = 50)]
		public bool Cancelled { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000054E1 File Offset: 0x000036E1
		// (set) Token: 0x060000EC RID: 236 RVA: 0x000054E9 File Offset: 0x000036E9
		[DataMember(Name = "MODEL", EmitDefaultValue = false, Order = 60)]
		public ModelTelemetry Model { get; set; }

		// Token: 0x060000ED RID: 237 RVA: 0x000054F2 File Offset: 0x000036F2
		public void SetCancelStatus(CancellationToken cancelToken)
		{
			this.Cancelled = cancelToken.IsCancellationRequested;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005504 File Offset: 0x00003704
		internal override void Write(ITelemetryService telemetry, ITracer tracer)
		{
			string text = this.SerializeForTelemetry(delegate(string msg)
			{
				tracer.SanitizedTrace(TraceLevel.Warning, msg);
			});
			telemetry.FireSanitizedEvent(DataShapingEvents.TranslateSemanticQueryInfo, new object[] { text });
		}
	}
}
