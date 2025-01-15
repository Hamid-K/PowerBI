using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.DataShapeResultRenderer
{
	// Token: 0x0200057E RID: 1406
	internal sealed class DataShapeResultRenderer : IDataShapeResultRenderer
	{
		// Token: 0x0600512F RID: 20783 RVA: 0x00158AD0 File Offset: 0x00156CD0
		public void RenderDataShapeResult(DataShapeResult dataShapeResult, CreateJsonStreamWriter createJsonStreamWriter)
		{
			foreach (DataShape dataShape in dataShapeResult.DataShapes)
			{
				using (IJsonStreamWriter jsonStreamWriter = createJsonStreamWriter(dataShape.ClientID))
				{
					using (JsonDataShapeResultWriter jsonDataShapeResultWriter = new JsonDataShapeResultWriter(jsonStreamWriter.JsonWriter))
					{
						try
						{
							jsonDataShapeResultWriter.WriteDataShapeResult(dataShape);
							DataShapeResultRenderer.WriteLimitTelemetry(dataShape);
						}
						catch (Exception ex)
						{
							if (AsynchronousExceptionDetection.IsStoppingException(ex))
							{
								throw;
							}
							jsonStreamWriter.WriteException(dataShape.ClientID, new DataShapeResultRenderingException(ex, dataShape.Messages, true));
						}
					}
				}
			}
		}

		// Token: 0x06005130 RID: 20784 RVA: 0x00158BA0 File Offset: 0x00156DA0
		private static void WriteLimitTelemetry(DataShape dataShape)
		{
			List<DataShapeLimit> limits = dataShape.Limits;
			if (limits == null || limits.Count == 0)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < limits.Count; i++)
			{
				DataShapeLimit dataShapeLimit = limits[i];
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				DataShapeResultRenderer.AppendLimitTelemetry(dataShapeLimit, stringBuilder);
			}
			Global.Tracer.Trace(TraceLevel.Info, "DataShapeODP Limits: " + stringBuilder.ToString());
		}

		// Token: 0x06005131 RID: 20785 RVA: 0x00158C10 File Offset: 0x00156E10
		private static void AppendLimitTelemetry(DataShapeLimit limit, StringBuilder builder)
		{
			builder.Append("{");
			builder.Append("\"Id\":").Append("\"").Append(limit.ClientID)
				.Append("\"");
			builder.Append(",");
			builder.Append("\"Kind\":").Append("\"").Append(DataShapeResultRenderer.ExtractLimitKind(limit.Operator))
				.Append("\"");
			builder.Append(",");
			builder.Append("\"Cap\":").Append(limit.Operator.Count.ToString(CultureInfo.InvariantCulture));
			builder.Append(",");
			builder.Append("\"N\":").Append(limit.CurrentCount.ToString(CultureInfo.InvariantCulture));
			builder.Append(",");
			builder.Append("\"Ex\":").Append(DataShapeResultRenderer.ToTelemetryString(limit.Exceeded));
			builder.Append("}");
		}

		// Token: 0x06005132 RID: 20786 RVA: 0x00158D29 File Offset: 0x00156F29
		private static string ToTelemetryString(bool value)
		{
			if (value)
			{
				return "true";
			}
			return "false";
		}

		// Token: 0x06005133 RID: 20787 RVA: 0x00158D39 File Offset: 0x00156F39
		private static string ExtractLimitKind(LimitOperator op)
		{
			if (op is TopLimitOperator)
			{
				return "Top";
			}
			if (op is BottomLimitOperator)
			{
				return "Bottom";
			}
			if (op is SampleLimitOperator)
			{
				return "Sample";
			}
			return string.Empty;
		}
	}
}
