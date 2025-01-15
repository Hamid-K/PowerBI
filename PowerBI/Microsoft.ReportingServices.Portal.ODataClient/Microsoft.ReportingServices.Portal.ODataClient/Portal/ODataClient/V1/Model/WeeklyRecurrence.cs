using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000D2 RID: 210
	[OriginalName("WeeklyRecurrence")]
	public class WeeklyRecurrence : INotifyPropertyChanged
	{
		// Token: 0x06000956 RID: 2390 RVA: 0x00012FC9 File Offset: 0x000111C9
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static WeeklyRecurrence CreateWeeklyRecurrence(int weeksInterval, bool weeksIntervalSpecified)
		{
			return new WeeklyRecurrence
			{
				WeeksInterval = weeksInterval,
				WeeksIntervalSpecified = weeksIntervalSpecified
			};
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x00012FDE File Offset: 0x000111DE
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x00012FE6 File Offset: 0x000111E6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("WeeksInterval")]
		public int WeeksInterval
		{
			get
			{
				return this._WeeksInterval;
			}
			set
			{
				this._WeeksInterval = value;
				this.OnPropertyChanged("WeeksInterval");
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x00012FFA File Offset: 0x000111FA
		// (set) Token: 0x0600095A RID: 2394 RVA: 0x00013002 File Offset: 0x00011202
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("WeeksIntervalSpecified")]
		public bool WeeksIntervalSpecified
		{
			get
			{
				return this._WeeksIntervalSpecified;
			}
			set
			{
				this._WeeksIntervalSpecified = value;
				this.OnPropertyChanged("WeeksIntervalSpecified");
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x00013016 File Offset: 0x00011216
		// (set) Token: 0x0600095C RID: 2396 RVA: 0x0001301E File Offset: 0x0001121E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DaysOfWeek")]
		public DaysOfWeekSelector DaysOfWeek
		{
			get
			{
				return this._DaysOfWeek;
			}
			set
			{
				this._DaysOfWeek = value;
				this.OnPropertyChanged("DaysOfWeek");
			}
		}

		// Token: 0x14000060 RID: 96
		// (add) Token: 0x0600095D RID: 2397 RVA: 0x00013034 File Offset: 0x00011234
		// (remove) Token: 0x0600095E RID: 2398 RVA: 0x0001306C File Offset: 0x0001126C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600095F RID: 2399 RVA: 0x000130A1 File Offset: 0x000112A1
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000466 RID: 1126
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _WeeksInterval;

		// Token: 0x04000467 RID: 1127
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _WeeksIntervalSpecified;

		// Token: 0x04000468 RID: 1128
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DaysOfWeekSelector _DaysOfWeek;
	}
}
