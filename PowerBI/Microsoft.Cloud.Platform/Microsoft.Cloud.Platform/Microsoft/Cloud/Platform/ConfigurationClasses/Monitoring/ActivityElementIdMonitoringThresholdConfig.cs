using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Monitoring
{
	// Token: 0x02000444 RID: 1092
	[Serializable]
	public sealed class ActivityElementIdMonitoringThresholdConfig : ConfigurationClass
	{
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x0007D69A File Offset: 0x0007B89A
		// (set) Token: 0x060021D8 RID: 8664 RVA: 0x0007D6A2 File Offset: 0x0007B8A2
		[ConfigurationProperty]
		public string ElementId { get; set; }

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x0007D6AB File Offset: 0x0007B8AB
		// (set) Token: 0x060021DA RID: 8666 RVA: 0x0007D6B3 File Offset: 0x0007B8B3
		[ConfigurationProperty]
		public string ActivityType { get; set; }

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060021DB RID: 8667 RVA: 0x0007D6BC File Offset: 0x0007B8BC
		// (set) Token: 0x060021DC RID: 8668 RVA: 0x0007D6C4 File Offset: 0x0007B8C4
		[ConfigurationProperty]
		public int WindowsEventLogWriteInterval { get; set; }

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060021DD RID: 8669 RVA: 0x0007D6CD File Offset: 0x0007B8CD
		// (set) Token: 0x060021DE RID: 8670 RVA: 0x0007D6D5 File Offset: 0x0007B8D5
		[ConfigurationProperty]
		public int MinConsecutiveIntervals { get; set; }

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060021DF RID: 8671 RVA: 0x0007D6DE File Offset: 0x0007B8DE
		// (set) Token: 0x060021E0 RID: 8672 RVA: 0x0007D6E6 File Offset: 0x0007B8E6
		[ConfigurationProperty]
		public int MinActivitiesCompletedPerInterval { get; set; }

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060021E1 RID: 8673 RVA: 0x0007D6EF File Offset: 0x0007B8EF
		// (set) Token: 0x060021E2 RID: 8674 RVA: 0x0007D6F7 File Offset: 0x0007B8F7
		[ConfigurationProperty]
		public int ActivityFailureThreshold
		{
			get
			{
				return this.m_ActivityFailureThreshold;
			}
			set
			{
				base.ValidateMoreOrEqual((double)value, 0.0);
				base.ValidateLessOrEqual((double)value, 100.0);
				this.m_ActivityFailureThreshold = value;
			}
		}

		// Token: 0x04000BAC RID: 2988
		private int m_ActivityFailureThreshold;
	}
}
