using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200006B RID: 107
	[Serializable]
	internal sealed class MixedTasksException : ReportCatalogException
	{
		// Token: 0x0600020B RID: 523 RVA: 0x00004948 File Offset: 0x00002B48
		public MixedTasksException()
			: base(ErrorCode.rsMixedTasks, ErrorStringsWrapper.rsMixedTasks, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000495E File Offset: 0x00002B5E
		private MixedTasksException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
