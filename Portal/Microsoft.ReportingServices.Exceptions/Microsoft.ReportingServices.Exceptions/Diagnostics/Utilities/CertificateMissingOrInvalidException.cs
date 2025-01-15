using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000C8 RID: 200
	[Serializable]
	internal sealed class CertificateMissingOrInvalidException : ReportCatalogException
	{
		// Token: 0x060002DA RID: 730 RVA: 0x00005A7F File Offset: 0x00003C7F
		public CertificateMissingOrInvalidException(string certificateId)
			: base(ErrorCode.rsCertificateMissingOrInvalid, ErrorStringsWrapper.rsCertificateMissingOrInvalid(certificateId), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00005A99 File Offset: 0x00003C99
		private CertificateMissingOrInvalidException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
