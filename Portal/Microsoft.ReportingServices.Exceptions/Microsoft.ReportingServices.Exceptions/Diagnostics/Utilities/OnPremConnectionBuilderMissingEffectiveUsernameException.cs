using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000057 RID: 87
	[Serializable]
	internal sealed class OnPremConnectionBuilderMissingEffectiveUsernameException : ReportCatalogException
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x00004614 File Offset: 0x00002814
		public OnPremConnectionBuilderMissingEffectiveUsernameException()
			: base(ErrorCode.rsOnPremConnectionBuilderMissingEffectiveUsername, ErrorStringsWrapper.rsOnPremConnectionBuilderMissingEffectiveUsername, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000462D File Offset: 0x0000282D
		private OnPremConnectionBuilderMissingEffectiveUsernameException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
