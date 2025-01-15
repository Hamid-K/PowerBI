using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200009C RID: 156
	[Serializable]
	internal sealed class ContainerTypeNotSupportedException : ReportCatalogException
	{
		// Token: 0x06000278 RID: 632 RVA: 0x00005120 File Offset: 0x00003320
		public ContainerTypeNotSupportedException()
			: base(ErrorCode.rsContainerNotSupported, ErrorStringsWrapper.rsContainerNotSupported, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00005139 File Offset: 0x00003339
		private ContainerTypeNotSupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
