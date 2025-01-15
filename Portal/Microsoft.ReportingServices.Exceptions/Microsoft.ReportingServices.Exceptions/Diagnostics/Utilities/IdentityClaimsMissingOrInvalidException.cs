using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000056 RID: 86
	[Serializable]
	internal sealed class IdentityClaimsMissingOrInvalidException : ReportCatalogException
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x000045E6 File Offset: 0x000027E6
		public IdentityClaimsMissingOrInvalidException(string identityClaims)
			: base(ErrorCode.rsIdentityClaimsMissingOrInvalid, ErrorStringsWrapper.rsIdentityClaimsMissingOrInvalid((identityClaims == null) ? string.Empty : identityClaims), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000460A File Offset: 0x0000280A
		private IdentityClaimsMissingOrInvalidException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
