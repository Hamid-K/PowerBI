using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000056 RID: 86
	[OriginalName("ScheduleRecurrence")]
	public class ScheduleRecurrence : INotifyPropertyChanged
	{
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00008A4D File Offset: 0x00006C4D
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x00008A55 File Offset: 0x00006C55
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("MinuteRecurrence")]
		public MinuteRecurrence MinuteRecurrence
		{
			get
			{
				return this._MinuteRecurrence;
			}
			set
			{
				this._MinuteRecurrence = value;
				this.OnPropertyChanged("MinuteRecurrence");
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00008A69 File Offset: 0x00006C69
		// (set) Token: 0x060003CB RID: 971 RVA: 0x00008A71 File Offset: 0x00006C71
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DailyRecurrence")]
		public DailyRecurrence DailyRecurrence
		{
			get
			{
				return this._DailyRecurrence;
			}
			set
			{
				this._DailyRecurrence = value;
				this.OnPropertyChanged("DailyRecurrence");
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00008A85 File Offset: 0x00006C85
		// (set) Token: 0x060003CD RID: 973 RVA: 0x00008A8D File Offset: 0x00006C8D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("WeeklyRecurrence")]
		public WeeklyRecurrence WeeklyRecurrence
		{
			get
			{
				return this._WeeklyRecurrence;
			}
			set
			{
				this._WeeklyRecurrence = value;
				this.OnPropertyChanged("WeeklyRecurrence");
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00008AA1 File Offset: 0x00006CA1
		// (set) Token: 0x060003CF RID: 975 RVA: 0x00008AA9 File Offset: 0x00006CA9
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("MonthlyRecurrence")]
		public MonthlyRecurrence MonthlyRecurrence
		{
			get
			{
				return this._MonthlyRecurrence;
			}
			set
			{
				this._MonthlyRecurrence = value;
				this.OnPropertyChanged("MonthlyRecurrence");
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00008ABD File Offset: 0x00006CBD
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x00008AC5 File Offset: 0x00006CC5
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("MonthlyDOWRecurrence")]
		public MonthlyDOWRecurrence MonthlyDOWRecurrence
		{
			get
			{
				return this._MonthlyDOWRecurrence;
			}
			set
			{
				this._MonthlyDOWRecurrence = value;
				this.OnPropertyChanged("MonthlyDOWRecurrence");
			}
		}

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x060003D2 RID: 978 RVA: 0x00008ADC File Offset: 0x00006CDC
		// (remove) Token: 0x060003D3 RID: 979 RVA: 0x00008B14 File Offset: 0x00006D14
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060003D4 RID: 980 RVA: 0x00008B49 File Offset: 0x00006D49
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040001D4 RID: 468
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private MinuteRecurrence _MinuteRecurrence;

		// Token: 0x040001D5 RID: 469
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DailyRecurrence _DailyRecurrence;

		// Token: 0x040001D6 RID: 470
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private WeeklyRecurrence _WeeklyRecurrence;

		// Token: 0x040001D7 RID: 471
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private MonthlyRecurrence _MonthlyRecurrence;

		// Token: 0x040001D8 RID: 472
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private MonthlyDOWRecurrence _MonthlyDOWRecurrence;
	}
}
