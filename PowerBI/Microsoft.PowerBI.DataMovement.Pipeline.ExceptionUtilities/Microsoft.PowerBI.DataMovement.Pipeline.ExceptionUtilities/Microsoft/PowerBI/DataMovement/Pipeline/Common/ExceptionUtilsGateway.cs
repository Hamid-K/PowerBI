using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common
{
	// Token: 0x02000007 RID: 7
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class ExceptionUtilsGateway
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020B4 File Offset: 0x000002B4
		internal static GatewayPipelineException EnsureGatewayPipelineException(this Exception exception)
		{
			RuntimeChecks.CheckValue(exception, "exception");
			GatewayPipelineException ex = exception as GatewayPipelineException;
			if (ex == null)
			{
				TraceSourceBase<ExceptionUtilsTraceSource>.Tracer.TraceError("EnsureGatewayPipelineException encountered a non-GatewayPipelineException: {0}", new object[] { exception });
				ex = GatewayExceptionUtils.InnerExceptionCreator.GetPipelineInnerException(exception, null);
			}
			if (ex != null)
			{
				ex.SetGatewayVersion("3000.170.2");
			}
			return ex;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		internal static bool IsRetriableBindingException(GatewayPipelineException ex)
		{
			return ex.GatewayPipelineErrorCode == "DM_GWPipeline_Client_GatewayUnreachable" || ex.GatewayPipelineErrorCode == "DM_GWPipeline_Client_LoadBalancer_NoCandidateAvailable" || ex.GatewayPipelineErrorCode == "DM_GWPipeline_Gateway_InvalidConnectionCredentials" || ex.GatewayPipelineErrorCode == "DM_GWPipeline_Gateway_ImpersonationError" || ex.GatewayPipelineErrorCode == "DM_GWPipeline_Gateway_BadUsernameFormat";
		}
	}
}
