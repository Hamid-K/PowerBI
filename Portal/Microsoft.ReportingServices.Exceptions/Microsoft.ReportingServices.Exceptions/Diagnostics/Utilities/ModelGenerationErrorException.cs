using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000077 RID: 119
	[Serializable]
	internal sealed class ModelGenerationErrorException : ReportCatalogException
	{
		// Token: 0x06000225 RID: 549 RVA: 0x00004AFC File Offset: 0x00002CFC
		public ModelGenerationErrorException(Exception innerException)
			: base(ErrorCode.rsModelGenerationError, ErrorStringsWrapper.rsModelGenerationError, innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00004B12 File Offset: 0x00002D12
		private ModelGenerationErrorException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
