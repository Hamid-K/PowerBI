using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000D7 RID: 215
	[OriginalName("ScheduleRecurrence")]
	public class ScheduleRecurrence : INotifyPropertyChanged
	{
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x0001346D File Offset: 0x0001166D
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x00013475 File Offset: 0x00011675
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

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x00013489 File Offset: 0x00011689
		// (set) Token: 0x0600098E RID: 2446 RVA: 0x00013491 File Offset: 0x00011691
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

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x000134A5 File Offset: 0x000116A5
		// (set) Token: 0x06000990 RID: 2448 RVA: 0x000134AD File Offset: 0x000116AD
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

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x000134C1 File Offset: 0x000116C1
		// (set) Token: 0x06000992 RID: 2450 RVA: 0x000134C9 File Offset: 0x000116C9
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

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x000134DD File Offset: 0x000116DD
		// (set) Token: 0x06000994 RID: 2452 RVA: 0x000134E5 File Offset: 0x000116E5
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

		// Token: 0x14000065 RID: 101
		// (add) Token: 0x06000995 RID: 2453 RVA: 0x000134FC File Offset: 0x000116FC
		// (remove) Token: 0x06000996 RID: 2454 RVA: 0x00013534 File Offset: 0x00011734
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000997 RID: 2455 RVA: 0x00013569 File Offset: 0x00011769
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400047A RID: 1146
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private MinuteRecurrence _MinuteRecurrence;

		// Token: 0x0400047B RID: 1147
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DailyRecurrence _DailyRecurrence;

		// Token: 0x0400047C RID: 1148
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private WeeklyRecurrence _WeeklyRecurrence;

		// Token: 0x0400047D RID: 1149
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private MonthlyRecurrence _MonthlyRecurrence;

		// Token: 0x0400047E RID: 1150
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private MonthlyDOWRecurrence _MonthlyDOWRecurrence;
	}
}
