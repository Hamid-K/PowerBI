using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200006A RID: 106
	[Serializable]
	internal sealed class TaskNotFoundException : ReportCatalogException
	{
		// Token: 0x06000209 RID: 521 RVA: 0x00004927 File Offset: 0x00002B27
		public TaskNotFoundException(string taskID)
			: base(ErrorCode.rsTaskNotFound, ErrorStringsWrapper.rsTaskNotFound(taskID), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000493E File Offset: 0x00002B3E
		private TaskNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
