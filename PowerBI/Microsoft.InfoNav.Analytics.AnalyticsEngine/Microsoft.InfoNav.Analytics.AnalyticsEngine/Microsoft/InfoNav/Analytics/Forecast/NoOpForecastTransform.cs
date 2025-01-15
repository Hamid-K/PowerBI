using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics.Forecast
{
	// Token: 0x02000035 RID: 53
	internal sealed class NoOpForecastTransform : ForecastTransform
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00005DAC File Offset: 0x00003FAC
		internal NoOpForecastTransform(ServiceRuntimeContext context)
			: base(context, 0.95f)
		{
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005DBC File Offset: 0x00003FBC
		public override Task<DataTransformResult> ExecuteAsync(DataTransformExecutionContext context)
		{
			DataTransformMessage message = new DataTransformMessage("ForecastTransformUnsupported", DataTransformMessageSeverity.Warning, "Forecast transform is unsupported");
			ITracer tracer = this.ServiceRuntimeContext.Tracer;
			return Task.FromResult<DataTransformResult>(this.ServiceRuntimeContext.TelemetryService.RunInActivity<DataTransformResult>("ForecastExecute", delegate
			{
				tracer.Trace(TraceLevel.Info, "Forecast transform is unsupported. Calling NoOp forecast transform instead.");
				return DataTransformResultFactory.OriginalResultWithWarnings(context.InputRows, DataPostprocessor.EmptyForecastResult, message.ArrayWrap<DataTransformMessage>());
			}));
		}

		// Token: 0x04000114 RID: 276
		private const string ForecastUnsupportedMessageCode = "ForecastTransformUnsupported";
	}
}
