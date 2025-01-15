using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000076 RID: 118
	[Serializable]
	internal sealed class ModelGenerationNotSupportedException : ReportCatalogException
	{
		// Token: 0x06000223 RID: 547 RVA: 0x00004ADC File Offset: 0x00002CDC
		public ModelGenerationNotSupportedException()
			: base(ErrorCode.rsModelGenerationNotSupported, ErrorStringsWrapper.rsModelGenerationNotSupported, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00004AF2 File Offset: 0x00002CF2
		private ModelGenerationNotSupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
