using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000054 RID: 84
	[Serializable]
	internal sealed class OnPremConnectionBuilderUnknownErrorException : ReportCatalogException
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x000045A0 File Offset: 0x000027A0
		public OnPremConnectionBuilderUnknownErrorException(string connectionString, Exception innerException)
			: base(ErrorCode.rsOnPremConnectionBuilderUnknownError, ErrorStringsWrapper.rsOnPremConnectionBuilderUnknownError, innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000045B9 File Offset: 0x000027B9
		private OnPremConnectionBuilderUnknownErrorException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
