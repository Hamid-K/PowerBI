using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000013 RID: 19
	[Serializable]
	internal sealed class InvalidXmlException : ReportCatalogException
	{
		// Token: 0x0600014B RID: 331 RVA: 0x00003CB7 File Offset: 0x00001EB7
		public InvalidXmlException()
			: base(ErrorCode.rsInvalidXml, ErrorStringsWrapper.rsInvalidXml, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00003CCC File Offset: 0x00001ECC
		private InvalidXmlException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
