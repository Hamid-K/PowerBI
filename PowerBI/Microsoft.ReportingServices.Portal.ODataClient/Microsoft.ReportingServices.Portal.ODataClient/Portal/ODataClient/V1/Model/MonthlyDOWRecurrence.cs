using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000D4 RID: 212
	[OriginalName("MonthlyDOWRecurrence")]
	public class MonthlyDOWRecurrence : INotifyPropertyChanged
	{
		// Token: 0x06000969 RID: 2409 RVA: 0x00013181 File Offset: 0x00011381
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static MonthlyDOWRecurrence CreateMonthlyDOWRecurrence(WeekNumberEnum whichWeek, bool whichWeekSpecified)
		{
			return new MonthlyDOWRecurrence
			{
				WhichWeek = whichWeek,
				WhichWeekSpecified = whichWeekSpecified
			};
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x00013196 File Offset: 0x00011396
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x0001319E File Offset: 0x0001139E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("WhichWeek")]
		public WeekNumberEnum WhichWeek
		{
			get
			{
				return this._WhichWeek;
			}
			set
			{
				this._WhichWeek = value;
				this.OnPropertyChanged("WhichWeek");
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x000131B2 File Offset: 0x000113B2
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x000131BA File Offset: 0x000113BA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("WhichWeekSpecified")]
		public bool WhichWeekSpecified
		{
			get
			{
				return this._WhichWeekSpecified;
			}
			set
			{
				this._WhichWeekSpecified = value;
				this.OnPropertyChanged("WhichWeekSpecified");
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x000131CE File Offset: 0x000113CE
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x000131D6 File Offset: 0x000113D6
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

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x000131EA File Offset: 0x000113EA
		// (set) Token: 0x06000971 RID: 2417 RVA: 0x000131F2 File Offset: 0x000113F2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("MonthsOfYear")]
		public MonthsOfYearSelector MonthsOfYear
		{
			get
			{
				return this._MonthsOfYear;
			}
			set
			{
				this._MonthsOfYear = value;
				this.OnPropertyChanged("MonthsOfYear");
			}
		}

		// Token: 0x14000062 RID: 98
		// (add) Token: 0x06000972 RID: 2418 RVA: 0x00013208 File Offset: 0x00011408
		// (remove) Token: 0x06000973 RID: 2419 RVA: 0x00013240 File Offset: 0x00011440
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000974 RID: 2420 RVA: 0x00013275 File Offset: 0x00011475
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400046D RID: 1133
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private WeekNumberEnum _WhichWeek;

		// Token: 0x0400046E RID: 1134
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _WhichWeekSpecified;

		// Token: 0x0400046F RID: 1135
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DaysOfWeekSelector _DaysOfWeek;

		// Token: 0x04000470 RID: 1136
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private MonthsOfYearSelector _MonthsOfYear;
	}
}
