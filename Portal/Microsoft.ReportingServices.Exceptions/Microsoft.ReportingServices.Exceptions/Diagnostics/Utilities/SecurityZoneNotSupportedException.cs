using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200009F RID: 159
	[Serializable]
	internal sealed class SecurityZoneNotSupportedException : ReportCatalogException
	{
		// Token: 0x0600027E RID: 638 RVA: 0x00005189 File Offset: 0x00003389
		public SecurityZoneNotSupportedException()
			: base(ErrorCode.rsSecurityZoneNotSupported, ErrorStringsWrapper.rsSecurityZoneNotSupported, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000051A2 File Offset: 0x000033A2
		private SecurityZoneNotSupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
