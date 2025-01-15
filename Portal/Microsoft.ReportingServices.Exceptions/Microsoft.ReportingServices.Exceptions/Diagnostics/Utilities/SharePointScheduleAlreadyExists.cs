using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000044 RID: 68
	[Serializable]
	internal sealed class SharePointScheduleAlreadyExists : ReportCatalogException
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x0000437B File Offset: 0x0000257B
		public SharePointScheduleAlreadyExists(string name, string path)
			: base(ErrorCode.rsScheduleAlreadyExists, ErrorStringsWrapper.rsSharePoitScheduleAlreadyExists(name, path), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00004393 File Offset: 0x00002593
		private SharePointScheduleAlreadyExists(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
