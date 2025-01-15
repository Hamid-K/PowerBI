using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000033 RID: 51
	[Serializable]
	internal sealed class UnexpectedCountOfPowerViewDataSourcesException : RSException
	{
		// Token: 0x06000139 RID: 313 RVA: 0x00008094 File Offset: 0x00006294
		public UnexpectedCountOfPowerViewDataSourcesException()
			: base(ErrorCode.pvInternalError, null, null, RSTrace.RenderingTracer, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000080AE File Offset: 0x000062AE
		public UnexpectedCountOfPowerViewDataSourcesException(Exception innerException)
			: base(ErrorCode.pvInternalError, null, innerException, RSTrace.RenderingTracer, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000080C8 File Offset: 0x000062C8
		private UnexpectedCountOfPowerViewDataSourcesException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
