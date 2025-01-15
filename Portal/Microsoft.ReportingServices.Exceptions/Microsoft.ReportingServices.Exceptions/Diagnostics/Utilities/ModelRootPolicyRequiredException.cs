using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000073 RID: 115
	[Serializable]
	internal sealed class ModelRootPolicyRequiredException : ReportCatalogException
	{
		// Token: 0x0600021D RID: 541 RVA: 0x00004A7C File Offset: 0x00002C7C
		public ModelRootPolicyRequiredException()
			: base(ErrorCode.rsModelRootPolicyRequired, ErrorStringsWrapper.rsModelRootPolicyRequired, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00004A92 File Offset: 0x00002C92
		private ModelRootPolicyRequiredException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
