using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000051 RID: 81
	[OriginalName("WeeklyRecurrence")]
	public class WeeklyRecurrence : INotifyPropertyChanged
	{
		// Token: 0x06000393 RID: 915 RVA: 0x000085A9 File Offset: 0x000067A9
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static WeeklyRecurrence CreateWeeklyRecurrence(int weeksInterval, bool weeksIntervalSpecified)
		{
			return new WeeklyRecurrence
			{
				WeeksInterval = weeksInterval,
				WeeksIntervalSpecified = weeksIntervalSpecified
			};
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000394 RID: 916 RVA: 0x000085BE File Offset: 0x000067BE
		// (set) Token: 0x06000395 RID: 917 RVA: 0x000085C6 File Offset: 0x000067C6
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

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000396 RID: 918 RVA: 0x000085DA File Offset: 0x000067DA
		// (set) Token: 0x06000397 RID: 919 RVA: 0x000085E2 File Offset: 0x000067E2
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

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000398 RID: 920 RVA: 0x000085F6 File Offset: 0x000067F6
		// (set) Token: 0x06000399 RID: 921 RVA: 0x000085FE File Offset: 0x000067FE
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

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x0600039A RID: 922 RVA: 0x00008614 File Offset: 0x00006814
		// (remove) Token: 0x0600039B RID: 923 RVA: 0x0000864C File Offset: 0x0000684C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600039C RID: 924 RVA: 0x00008681 File Offset: 0x00006881
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040001C0 RID: 448
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _WeeksInterval;

		// Token: 0x040001C1 RID: 449
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _WeeksIntervalSpecified;

		// Token: 0x040001C2 RID: 450
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DaysOfWeekSelector _DaysOfWeek;
	}
}
