using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000063 RID: 99
	[Serializable]
	internal sealed class DeliveryExtensionNotFoundException : ReportCatalogException
	{
		// Token: 0x060001F9 RID: 505 RVA: 0x0000482F File Offset: 0x00002A2F
		public DeliveryExtensionNotFoundException()
			: base(ErrorCode.rsDeliveryExtensionNotFound, ErrorStringsWrapper.rsDeliveryExtensionNotFound, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00004845 File Offset: 0x00002A45
		private DeliveryExtensionNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
