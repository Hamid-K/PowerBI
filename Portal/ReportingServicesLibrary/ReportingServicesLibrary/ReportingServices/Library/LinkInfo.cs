using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000056 RID: 86
	internal struct LinkInfo
	{
		// Token: 0x060003E7 RID: 999 RVA: 0x00010D68 File Offset: 0x0000EF68
		internal LinkInfo(Guid rid, Guid lid)
		{
			this.ReportID = rid;
			this.LinkID = lid;
		}

		// Token: 0x040001A9 RID: 425
		internal Guid ReportID;

		// Token: 0x040001AA RID: 426
		internal Guid LinkID;
	}
}
