using System;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x0200052B RID: 1323
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class EventAttribute : Attribute
	{
		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x060028A7 RID: 10407 RVA: 0x000924B6 File Offset: 0x000906B6
		public long Id
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x060028A8 RID: 10408 RVA: 0x000924BE File Offset: 0x000906BE
		// (set) Token: 0x060028A9 RID: 10409 RVA: 0x000924C6 File Offset: 0x000906C6
		public int Priority { get; set; }

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x060028AA RID: 10410 RVA: 0x000924CF File Offset: 0x000906CF
		// (set) Token: 0x060028AB RID: 10411 RVA: 0x000924D7 File Offset: 0x000906D7
		public EventLevel Level { get; set; }

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x060028AC RID: 10412 RVA: 0x000924E0 File Offset: 0x000906E0
		// (set) Token: 0x060028AD RID: 10413 RVA: 0x000924E8 File Offset: 0x000906E8
		public string Name { get; set; }

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x060028AE RID: 10414 RVA: 0x000924F1 File Offset: 0x000906F1
		// (set) Token: 0x060028AF RID: 10415 RVA: 0x000924F9 File Offset: 0x000906F9
		public string Format { get; set; }

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x060028B0 RID: 10416 RVA: 0x00092502 File Offset: 0x00090702
		// (set) Token: 0x060028B1 RID: 10417 RVA: 0x0009250A File Offset: 0x0009070A
		public int Order { get; private set; }

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x060028B2 RID: 10418 RVA: 0x00092513 File Offset: 0x00090713
		// (set) Token: 0x060028B3 RID: 10419 RVA: 0x0009251B File Offset: 0x0009071B
		public int Version
		{
			get
			{
				return this.eventVersion;
			}
			set
			{
				this.eventVersion = value;
			}
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x00092524 File Offset: 0x00090724
		public EventAttribute(long id, int order)
		{
			this.m_id = id;
			this.Priority = 0;
			this.Level = EventLevel.Inherit;
			this.Order = order;
			this.Version = this.eventVersion;
		}

		// Token: 0x04000E7F RID: 3711
		private readonly long m_id;

		// Token: 0x04000E85 RID: 3717
		private int eventVersion;
	}
}
