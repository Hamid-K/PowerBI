using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200004D RID: 77
	[Serializable]
	public sealed class InvalidDataSourceCredentialException : ReportCatalogException
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x000044AE File Offset: 0x000026AE
		public InvalidDataSourceCredentialException()
			: base(ErrorCode.rsDatasourceCredentialsNoLongerValid, ErrorStringsWrapper.rsDatasourceCredentialsNoLongerValid, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000044C4 File Offset: 0x000026C4
		private InvalidDataSourceCredentialException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
