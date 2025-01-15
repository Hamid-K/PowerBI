using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Exceptions;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	internal sealed class EvaluationCopyExpiredException : ReportCatalogException
	{
		// Token: 0x06000127 RID: 295 RVA: 0x000039D2 File Offset: 0x00001BD2
		public EvaluationCopyExpiredException()
			: base(ErrorCode.rsEvaluationCopyExpired, ErrorStrings.rsEvaluationCopyExpired, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000039EB File Offset: 0x00001BEB
		private EvaluationCopyExpiredException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
