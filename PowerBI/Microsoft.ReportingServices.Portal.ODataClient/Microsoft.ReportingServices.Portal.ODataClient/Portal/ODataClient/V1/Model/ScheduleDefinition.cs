using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000D5 RID: 213
	[OriginalName("ScheduleDefinition")]
	public class ScheduleDefinition : INotifyPropertyChanged
	{
		// Token: 0x06000976 RID: 2422 RVA: 0x00013291 File Offset: 0x00011491
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

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x000132AD File Offset: 0x000114AD
		// (set) Token: 0x06000978 RID: 2424 RVA: 0x000132B5 File Offset: 0x000114B5
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

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x000132C9 File Offset: 0x000114C9
		// (set) Token: 0x0600097A RID: 2426 RVA: 0x000132D1 File Offset: 0x000114D1
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

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x000132E5 File Offset: 0x000114E5
		// (set) Token: 0x0600097C RID: 2428 RVA: 0x000132ED File Offset: 0x000114ED
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

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00013301 File Offset: 0x00011501
		// (set) Token: 0x0600097E RID: 2430 RVA: 0x00013309 File Offset: 0x00011509
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

		// Token: 0x14000063 RID: 99
		// (add) Token: 0x0600097F RID: 2431 RVA: 0x00013320 File Offset: 0x00011520
		// (remove) Token: 0x06000980 RID: 2432 RVA: 0x00013358 File Offset: 0x00011558
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000981 RID: 2433 RVA: 0x0001338D File Offset: 0x0001158D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000472 RID: 1138
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset _StartDateTime;

		// Token: 0x04000473 RID: 1139
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset _EndDate;

		// Token: 0x04000474 RID: 1140
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _EndDateSpecified;

		// Token: 0x04000475 RID: 1141
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ScheduleRecurrence _Recurrence;
	}
}
