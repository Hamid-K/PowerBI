using System;
using System.Runtime.Serialization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x0200008E RID: 142
	[Serializable]
	public class MonitoredErrorSinkServicesException : MonitoredException
	{
		// Token: 0x06000406 RID: 1030 RVA: 0x0000EB75 File Offset: 0x0000CD75
		public MonitoredErrorSinkServicesException()
		{
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000EB7D File Offset: 0x0000CD7D
		public MonitoredErrorSinkServicesException(string message)
			: base(message)
		{
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000EB86 File Offset: 0x0000CD86
		public MonitoredErrorSinkServicesException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000EB90 File Offset: 0x0000CD90
		public MonitoredErrorSinkServicesException(Exception innerException)
			: this("MonitoredError Sink Services Failed", innerException)
		{
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000EB9E File Offset: 0x0000CD9E
		protected MonitoredErrorSinkServicesException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
