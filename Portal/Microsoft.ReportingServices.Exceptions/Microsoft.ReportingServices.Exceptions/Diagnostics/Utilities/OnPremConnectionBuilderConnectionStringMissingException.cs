using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000055 RID: 85
	[Serializable]
	internal sealed class OnPremConnectionBuilderConnectionStringMissingException : ReportCatalogException
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x000045C3 File Offset: 0x000027C3
		public OnPremConnectionBuilderConnectionStringMissingException()
			: base(ErrorCode.rsOnPremConnectionBuilderConnectionStringMissing, ErrorStringsWrapper.rsOnPremConnectionBuilderConnectionStringMissing, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000045DC File Offset: 0x000027DC
		private OnPremConnectionBuilderConnectionStringMissingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
