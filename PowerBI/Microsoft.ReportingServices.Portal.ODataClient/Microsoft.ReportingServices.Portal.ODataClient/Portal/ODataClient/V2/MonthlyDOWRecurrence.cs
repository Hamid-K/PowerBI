using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000053 RID: 83
	[OriginalName("MonthlyDOWRecurrence")]
	public class MonthlyDOWRecurrence : INotifyPropertyChanged
	{
		// Token: 0x060003A6 RID: 934 RVA: 0x00008761 File Offset: 0x00006961
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static MonthlyDOWRecurrence CreateMonthlyDOWRecurrence(WeekNumberEnum whichWeek, bool whichWeekSpecified)
		{
			return new MonthlyDOWRecurrence
			{
				WhichWeek = whichWeek,
				WhichWeekSpecified = whichWeekSpecified
			};
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00008776 File Offset: 0x00006976
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x0000877E File Offset: 0x0000697E
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

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00008792 File Offset: 0x00006992
		// (set) Token: 0x060003AA RID: 938 RVA: 0x0000879A File Offset: 0x0000699A
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

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060003AB RID: 939 RVA: 0x000087AE File Offset: 0x000069AE
		// (set) Token: 0x060003AC RID: 940 RVA: 0x000087B6 File Offset: 0x000069B6
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

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060003AD RID: 941 RVA: 0x000087CA File Offset: 0x000069CA
		// (set) Token: 0x060003AE RID: 942 RVA: 0x000087D2 File Offset: 0x000069D2
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

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x060003AF RID: 943 RVA: 0x000087E8 File Offset: 0x000069E8
		// (remove) Token: 0x060003B0 RID: 944 RVA: 0x00008820 File Offset: 0x00006A20
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060003B1 RID: 945 RVA: 0x00008855 File Offset: 0x00006A55
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040001C7 RID: 455
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private WeekNumberEnum _WhichWeek;

		// Token: 0x040001C8 RID: 456
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _WhichWeekSpecified;

		// Token: 0x040001C9 RID: 457
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DaysOfWeekSelector _DaysOfWeek;

		// Token: 0x040001CA RID: 458
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private MonthsOfYearSelector _MonthsOfYear;
	}
}
