using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000020 RID: 32
	[Serializable]
	internal sealed class MaxCountCommentsException : ReportCatalogException
	{
		// Token: 0x0600016A RID: 362 RVA: 0x00003ED6 File Offset: 0x000020D6
		public MaxCountCommentsException()
			: base(ErrorCode.rsMaxCountComments, ErrorStringsWrapper.rsMaxCountComments(), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00003EEC File Offset: 0x000020EC
		private MaxCountCommentsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
