using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
	// Token: 0x02000064 RID: 100
	public class Subscription
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000368A File Offset: 0x0000188A
		// (set) Token: 0x06000297 RID: 663 RVA: 0x00003692 File Offset: 0x00001892
		[ReadOnly(true)]
		public Guid Id { get; set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000369B File Offset: 0x0000189B
		// (set) Token: 0x06000299 RID: 665 RVA: 0x000036A3 File Offset: 0x000018A3
		[ReadOnly(true)]
		public string Owner { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600029A RID: 666 RVA: 0x000036AC File Offset: 0x000018AC
		// (set) Token: 0x0600029B RID: 667 RVA: 0x000036B4 File Offset: 0x000018B4
		[ReadOnly(true)]
		public bool IsDataDriven { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600029C RID: 668 RVA: 0x000036BD File Offset: 0x000018BD
		// (set) Token: 0x0600029D RID: 669 RVA: 0x000036C5 File Offset: 0x000018C5
		public string Description { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600029E RID: 670 RVA: 0x000036CE File Offset: 0x000018CE
		// (set) Token: 0x0600029F RID: 671 RVA: 0x000036D6 File Offset: 0x000018D6
		public string Report { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x000036DF File Offset: 0x000018DF
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x000036E7 File Offset: 0x000018E7
		public bool IsActive { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x000036F0 File Offset: 0x000018F0
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x000036F8 File Offset: 0x000018F8
		public string EventType { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00003701 File Offset: 0x00001901
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x00003709 File Offset: 0x00001909
		public ScheduleReference Schedule { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00003712 File Offset: 0x00001912
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x0000371A File Offset: 0x0000191A
		public string ScheduleDescription { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00003723 File Offset: 0x00001923
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x0000372B File Offset: 0x0000192B
		public DateTimeOffset? LastRunTime { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00003734 File Offset: 0x00001934
		// (set) Token: 0x060002AB RID: 683 RVA: 0x0000373C File Offset: 0x0000193C
		public string LastStatus { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00003745 File Offset: 0x00001945
		// (set) Token: 0x060002AD RID: 685 RVA: 0x0000374D File Offset: 0x0000194D
		public DataSource DataSource { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00003756 File Offset: 0x00001956
		// (set) Token: 0x060002AF RID: 687 RVA: 0x0000375E File Offset: 0x0000195E
		public Query DataQuery { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00003767 File Offset: 0x00001967
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x0000376F File Offset: 0x0000196F
		public ExtensionSettings ExtensionSettings { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x00003778 File Offset: 0x00001978
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x00003780 File Offset: 0x00001980
		public string DeliveryExtension { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x00003789 File Offset: 0x00001989
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x00003791 File Offset: 0x00001991
		public string LocalizedDeliveryExtensionName { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000379A File Offset: 0x0000199A
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x000037A2 File Offset: 0x000019A2
		public string ModifiedBy { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x000037AB File Offset: 0x000019AB
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x000037B3 File Offset: 0x000019B3
		public DateTimeOffset? ModifiedDate { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060002BA RID: 698 RVA: 0x000037BC File Offset: 0x000019BC
		// (set) Token: 0x060002BB RID: 699 RVA: 0x000037C4 File Offset: 0x000019C4
		public IEnumerable<ParameterValue> ParameterValues { get; set; }
	}
}
