using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000048 RID: 72
	[Serializable]
	internal sealed class InvalidDataSourceReferenceException : ReportCatalogException
	{
		// Token: 0x060001BA RID: 442 RVA: 0x00004401 File Offset: 0x00002601
		public InvalidDataSourceReferenceException(string datasourceName)
			: base(ErrorCode.rsInvalidDataSourceReference, ErrorStringsWrapper.rsInvalidDataSourceReference(datasourceName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00004418 File Offset: 0x00002618
		private InvalidDataSourceReferenceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
