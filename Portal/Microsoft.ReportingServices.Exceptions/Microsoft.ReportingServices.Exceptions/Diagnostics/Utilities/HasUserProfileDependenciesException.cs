using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000041 RID: 65
	[Serializable]
	internal sealed class HasUserProfileDependenciesException : ReportCatalogException
	{
		// Token: 0x060001AC RID: 428 RVA: 0x00004318 File Offset: 0x00002518
		public HasUserProfileDependenciesException(string reportName)
			: base(ErrorCode.rsHasUserProfileDependencies, ErrorStringsWrapper.rsHasUserProfileDependencies(reportName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000432F File Offset: 0x0000252F
		private HasUserProfileDependenciesException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
