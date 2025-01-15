using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E09 RID: 3593
	[DataContract]
	internal abstract class CdpaRequestWithTimeConfigurationAndResponseProtocol : CdpaRequest
	{
		// Token: 0x17001C98 RID: 7320
		// (get) Token: 0x060060D2 RID: 24786 RVA: 0x0014A35E File Offset: 0x0014855E
		// (set) Token: 0x060060D3 RID: 24787 RVA: 0x0014A366 File Offset: 0x00148566
		[DataMember(Name = "timeRange", IsRequired = false)]
		public CdpaTimeRange TimeRange { get; set; }

		// Token: 0x17001C99 RID: 7321
		// (get) Token: 0x060060D4 RID: 24788 RVA: 0x0014A36F File Offset: 0x0014856F
		// (set) Token: 0x060060D5 RID: 24789 RVA: 0x0014A377 File Offset: 0x00148577
		[DataMember(Name = "granularity", IsRequired = false)]
		public ITimeGranularity Granularity { get; set; }

		// Token: 0x17001C9A RID: 7322
		// (get) Token: 0x060060D6 RID: 24790 RVA: 0x0014A380 File Offset: 0x00148580
		// (set) Token: 0x060060D7 RID: 24791 RVA: 0x0014A388 File Offset: 0x00148588
		[DataMember(Name = "responseProtocol", IsRequired = true)]
		public string ResponseProtocol { get; set; }

		// Token: 0x060060D8 RID: 24792
		public abstract CdpaRequestWithTimeConfigurationAndResponseProtocol ShallowCopy();
	}
}
