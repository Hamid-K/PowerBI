using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
	// Token: 0x02000023 RID: 35
	public class CacheRefreshPlan
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000025A1 File Offset: 0x000007A1
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000025A9 File Offset: 0x000007A9
		[ReadOnly(true)]
		public Guid Id { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000025B2 File Offset: 0x000007B2
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000025BA File Offset: 0x000007BA
		[ReadOnly(true)]
		public string Owner { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000025C3 File Offset: 0x000007C3
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000025CB File Offset: 0x000007CB
		public string Description { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000025D4 File Offset: 0x000007D4
		// (set) Token: 0x06000092 RID: 146 RVA: 0x000025DC File Offset: 0x000007DC
		public string CatalogItemPath { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000025E5 File Offset: 0x000007E5
		// (set) Token: 0x06000094 RID: 148 RVA: 0x000025ED File Offset: 0x000007ED
		public string EventType { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000025F6 File Offset: 0x000007F6
		// (set) Token: 0x06000096 RID: 150 RVA: 0x000025FE File Offset: 0x000007FE
		public ScheduleReference Schedule { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00002607 File Offset: 0x00000807
		// (set) Token: 0x06000098 RID: 152 RVA: 0x0000260F File Offset: 0x0000080F
		public string ScheduleDescription { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00002618 File Offset: 0x00000818
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00002620 File Offset: 0x00000820
		public DateTimeOffset? LastRunTime { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00002629 File Offset: 0x00000829
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00002631 File Offset: 0x00000831
		public string LastStatus { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000263A File Offset: 0x0000083A
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00002642 File Offset: 0x00000842
		public string ModifiedBy { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000264B File Offset: 0x0000084B
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00002653 File Offset: 0x00000853
		public DateTimeOffset? ModifiedDate { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000265C File Offset: 0x0000085C
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00002664 File Offset: 0x00000864
		public IEnumerable<ParameterValue> ParameterValues { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00002670 File Offset: 0x00000870
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00002696 File Offset: 0x00000896
		public IList<SubscriptionHistory> History
		{
			get
			{
				IList<SubscriptionHistory> list;
				if ((list = this._history) == null)
				{
					list = (this._history = this.LoadSubscriptionHistory());
				}
				return list;
			}
			set
			{
				this._history = value;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000269F File Offset: 0x0000089F
		protected virtual IList<SubscriptionHistory> LoadSubscriptionHistory()
		{
			return new List<SubscriptionHistory>();
		}

		// Token: 0x040000D2 RID: 210
		private IList<SubscriptionHistory> _history;
	}
}
