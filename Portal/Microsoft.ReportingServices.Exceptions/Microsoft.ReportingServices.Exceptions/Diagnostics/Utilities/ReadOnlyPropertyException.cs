using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000029 RID: 41
	[Serializable]
	internal sealed class ReadOnlyPropertyException : ReportCatalogException
	{
		// Token: 0x0600017C RID: 380 RVA: 0x00003FFF File Offset: 0x000021FF
		public ReadOnlyPropertyException(string propertyName)
			: base(ErrorCode.rsReadOnlyProperty, ErrorStringsWrapper.rsReadOnlyProperty(propertyName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00004016 File Offset: 0x00002216
		private ReadOnlyPropertyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
