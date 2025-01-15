using System;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x0200052A RID: 1322
	[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	public sealed class EventsKitAttribute : Attribute
	{
		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x0600289F RID: 10399 RVA: 0x00092449 File Offset: 0x00090649
		public long Id
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x060028A0 RID: 10400 RVA: 0x00092451 File Offset: 0x00090651
		// (set) Token: 0x060028A1 RID: 10401 RVA: 0x00092459 File Offset: 0x00090659
		public int Priority { get; set; }

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x060028A2 RID: 10402 RVA: 0x00092462 File Offset: 0x00090662
		// (set) Token: 0x060028A3 RID: 10403 RVA: 0x0009246A File Offset: 0x0009066A
		public EventLevel Level
		{
			get
			{
				return this.m_level;
			}
			set
			{
				if (value == EventLevel.Inherit)
				{
					throw new InvalidOperationException("Event kit cannot inherit severity options");
				}
				this.m_level = value;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x060028A4 RID: 10404 RVA: 0x00092481 File Offset: 0x00090681
		// (set) Token: 0x060028A5 RID: 10405 RVA: 0x00092489 File Offset: 0x00090689
		public bool Abstract { get; set; }

		// Token: 0x060028A6 RID: 10406 RVA: 0x00092492 File Offset: 0x00090692
		public EventsKitAttribute(long id)
		{
			this.m_id = id;
			this.Priority = 0;
			this.Level = EventLevel.Verbose;
			this.Abstract = false;
		}

		// Token: 0x04000E7B RID: 3707
		private readonly long m_id;

		// Token: 0x04000E7C RID: 3708
		private EventLevel m_level;
	}
}
