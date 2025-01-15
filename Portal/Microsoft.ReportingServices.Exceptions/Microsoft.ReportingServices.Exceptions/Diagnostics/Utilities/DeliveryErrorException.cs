using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000064 RID: 100
	[Serializable]
	internal sealed class DeliveryErrorException : ReportCatalogException
	{
		// Token: 0x060001FB RID: 507 RVA: 0x0000484F File Offset: 0x00002A4F
		public DeliveryErrorException(Exception innerException)
			: base(ErrorCode.rsDeliveryError, ErrorStringsWrapper.rsDeliverError, innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00004865 File Offset: 0x00002A65
		private DeliveryErrorException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
