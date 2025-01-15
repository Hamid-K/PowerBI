using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000095 RID: 149
	[Serializable]
	internal sealed class StoredParameterNotFoundException : ReportCatalogException
	{
		// Token: 0x06000269 RID: 617 RVA: 0x00005010 File Offset: 0x00003210
		public StoredParameterNotFoundException(string storedParameterId)
			: base(ErrorCode.rsStoredParameterNotFound, ErrorStringsWrapper.rsStoredParameterNotFound(storedParameterId), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000502A File Offset: 0x0000322A
		private StoredParameterNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
