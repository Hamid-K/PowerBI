using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005F2 RID: 1522
	[Serializable]
	internal sealed class ReportProcessingException_UserProfilesDependencies : Exception
	{
		// Token: 0x0600542C RID: 21548 RVA: 0x00161BF2 File Offset: 0x0015FDF2
		internal ReportProcessingException_UserProfilesDependencies()
		{
		}

		// Token: 0x0600542D RID: 21549 RVA: 0x00161BFA File Offset: 0x0015FDFA
		private ReportProcessingException_UserProfilesDependencies(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
