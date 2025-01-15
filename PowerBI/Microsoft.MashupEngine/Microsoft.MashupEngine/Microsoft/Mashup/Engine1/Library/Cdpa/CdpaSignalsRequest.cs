using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E08 RID: 3592
	[DataContract]
	internal class CdpaSignalsRequest : CdpaRequest
	{
		// Token: 0x060060CA RID: 24778 RVA: 0x0014A2C8 File Offset: 0x001484C8
		public CdpaSignalsRequest()
		{
			this.Limit = int.MaxValue;
		}

		// Token: 0x17001C95 RID: 7317
		// (get) Token: 0x060060CB RID: 24779 RVA: 0x0014A2DB File Offset: 0x001484DB
		// (set) Token: 0x060060CC RID: 24780 RVA: 0x0014A2E3 File Offset: 0x001484E3
		[DataMember(Name = "configuration", IsRequired = true)]
		public CdpaSignalsConfiguration Configuration { get; set; }

		// Token: 0x17001C96 RID: 7318
		// (get) Token: 0x060060CD RID: 24781 RVA: 0x0014A2EC File Offset: 0x001484EC
		// (set) Token: 0x060060CE RID: 24782 RVA: 0x0014A2F4 File Offset: 0x001484F4
		[DataMember(Name = "limit", IsRequired = true)]
		public int Limit { get; set; }

		// Token: 0x17001C97 RID: 7319
		// (get) Token: 0x060060CF RID: 24783 RVA: 0x0014A2FD File Offset: 0x001484FD
		// (set) Token: 0x060060D0 RID: 24784 RVA: 0x0014A305 File Offset: 0x00148505
		[DataMember(Name = "timeRange", IsRequired = false)]
		public CdpaTimeRange TimeRange { get; set; }

		// Token: 0x060060D1 RID: 24785 RVA: 0x0014A310 File Offset: 0x00148510
		public CdpaSignalsRequest ShallowCopy()
		{
			return new CdpaSignalsRequest
			{
				Configuration = this.Configuration,
				Limit = this.Limit,
				TimeRange = this.TimeRange,
				ResponseFormat = base.ResponseFormat,
				ExtraParameters = base.ExtraParameters
			};
		}

		// Token: 0x040034D5 RID: 13525
		public const int MaxSupportedLimit = 10000;
	}
}
