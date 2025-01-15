using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000049 RID: 73
	[Serializable]
	internal sealed class InvalidDataSourceTypeException : ReportCatalogException
	{
		// Token: 0x060001BC RID: 444 RVA: 0x00004422 File Offset: 0x00002622
		public InvalidDataSourceTypeException(string datasourcePath)
			: base(ErrorCode.rsInvalidDataSourceType, ErrorStringsWrapper.rsInvalidDataSourceType(datasourcePath), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000443C File Offset: 0x0000263C
		private InvalidDataSourceTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
