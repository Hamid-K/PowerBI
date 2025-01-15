using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000054 RID: 84
	[OriginalName("ScheduleDefinition")]
	public class ScheduleDefinition : INotifyPropertyChanged
	{
		// Token: 0x060003B3 RID: 947 RVA: 0x00008871 File Offset: 0x00006A71
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static ScheduleDefinition CreateScheduleDefinition(DateTimeOffset startDateTime, DateTimeOffset endDate, bool endDateSpecified)
		{
			return new ScheduleDefinition
			{
				StartDateTime = startDateTime,
				EndDate = endDate,
				EndDateSpecified = endDateSpecified
			};
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000888D File Offset: 0x00006A8D
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x00008895 File Offset: 0x00006A95
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("StartDateTime")]
		public DateTimeOffset StartDateTime
		{
			get
			{
				return this._StartDateTime;
			}
			set
			{
				this._StartDateTime = value;
				this.OnPropertyChanged("StartDateTime");
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x000088A9 File Offset: 0x00006AA9
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x000088B1 File Offset: 0x00006AB1
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("EndDate")]
		public DateTimeOffset EndDate
		{
			get
			{
				return this._EndDate;
			}
			set
			{
				this._EndDate = value;
				this.OnPropertyChanged("EndDate");
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x000088C5 File Offset: 0x00006AC5
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x000088CD File Offset: 0x00006ACD
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("EndDateSpecified")]
		public bool EndDateSpecified
		{
			get
			{
				return this._EndDateSpecified;
			}
			set
			{
				this._EndDateSpecified = value;
				this.OnPropertyChanged("EndDateSpecified");
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060003BA RID: 954 RVA: 0x000088E1 File Offset: 0x00006AE1
		// (set) Token: 0x060003BB RID: 955 RVA: 0x000088E9 File Offset: 0x00006AE9
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Recurrence")]
		public ScheduleRecurrence Recurrence
		{
			get
			{
				return this._Recurrence;
			}
			set
			{
				this._Recurrence = value;
				this.OnPropertyChanged("Recurrence");
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x060003BC RID: 956 RVA: 0x00008900 File Offset: 0x00006B00
		// (remove) Token: 0x060003BD RID: 957 RVA: 0x00008938 File Offset: 0x00006B38
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060003BE RID: 958 RVA: 0x0000896D File Offset: 0x00006B6D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040001CC RID: 460
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset _StartDateTime;

		// Token: 0x040001CD RID: 461
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset _EndDate;

		// Token: 0x040001CE RID: 462
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _EndDateSpecified;

		// Token: 0x040001CF RID: 463
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ScheduleRecurrence _Recurrence;
	}
}
