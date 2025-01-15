using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000072 RID: 114
	[Serializable]
	internal sealed class CannotDeleteRootPolicyException : ReportCatalogException
	{
		// Token: 0x0600021B RID: 539 RVA: 0x00004A5C File Offset: 0x00002C5C
		public CannotDeleteRootPolicyException()
			: base(ErrorCode.rsCannotDeleteRootPolicy, ErrorStringsWrapper.rsCannotDeleteRootPolicy, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00004A72 File Offset: 0x00002C72
		private CannotDeleteRootPolicyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
