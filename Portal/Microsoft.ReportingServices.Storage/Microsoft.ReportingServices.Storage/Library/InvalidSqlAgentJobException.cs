using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	internal sealed class InvalidSqlAgentJobException : ReportCatalogException
	{
		// Token: 0x06000118 RID: 280 RVA: 0x00007ECA File Offset: 0x000060CA
		public InvalidSqlAgentJobException(string taskName)
			: base(ErrorCode.rsInvalidSqlAgentJob, ErrorStringsWrapper.rsInvalidSqlAgentJob(taskName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00007EE4 File Offset: 0x000060E4
		public InvalidSqlAgentJobException(string taskName, string additionalTraceMessage)
			: base(ErrorCode.rsInvalidSqlAgentJob, ErrorStringsWrapper.rsInvalidSqlAgentJob(taskName), null, additionalTraceMessage, Array.Empty<object>())
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00007D2D File Offset: 0x00005F2D
		private InvalidSqlAgentJobException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
