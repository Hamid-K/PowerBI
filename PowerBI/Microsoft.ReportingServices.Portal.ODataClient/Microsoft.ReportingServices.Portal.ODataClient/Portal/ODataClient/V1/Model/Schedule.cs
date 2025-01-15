using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000CF RID: 207
	[Key("Id")]
	[EntitySet("Schedules")]
	[OriginalName("Schedule")]
	public class Schedule : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x0600092B RID: 2347 RVA: 0x00012B9F File Offset: 0x00010D9F
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static Schedule CreateSchedule(Guid ID, DateTimeOffset nextRunTime, bool nextRunTimeSpecified, DateTimeOffset lastRunTime, bool lastRunTimeSpecified, bool referencesPresent, ScheduleStateEnum state)
		{
			return new Schedule
			{
				Id = ID,
				NextRunTime = nextRunTime,
				NextRunTimeSpecified = nextRunTimeSpecified,
				LastRunTime = lastRunTime,
				LastRunTimeSpecified = lastRunTimeSpecified,
				ReferencesPresent = referencesPresent,
				State = state
			};
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x00012BDA File Offset: 0x00010DDA
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x00012BE2 File Offset: 0x00010DE2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Id")]
		public Guid Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.OnPropertyChanged("Id");
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x00012BF6 File Offset: 0x00010DF6
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x00012BFE File Offset: 0x00010DFE
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Name")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
				this.OnPropertyChanged("Name");
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x00012C12 File Offset: 0x00010E12
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x00012C1A File Offset: 0x00010E1A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Definition")]
		public ScheduleDefinition Definition
		{
			get
			{
				return this._Definition;
			}
			set
			{
				this._Definition = value;
				this.OnPropertyChanged("Definition");
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x00012C2E File Offset: 0x00010E2E
		// (set) Token: 0x06000933 RID: 2355 RVA: 0x00012C36 File Offset: 0x00010E36
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Description")]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				this._Description = value;
				this.OnPropertyChanged("Description");
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x00012C4A File Offset: 0x00010E4A
		// (set) Token: 0x06000935 RID: 2357 RVA: 0x00012C52 File Offset: 0x00010E52
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Creator")]
		public string Creator
		{
			get
			{
				return this._Creator;
			}
			set
			{
				this._Creator = value;
				this.OnPropertyChanged("Creator");
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x00012C66 File Offset: 0x00010E66
		// (set) Token: 0x06000937 RID: 2359 RVA: 0x00012C6E File Offset: 0x00010E6E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("NextRunTime")]
		public DateTimeOffset NextRunTime
		{
			get
			{
				return this._NextRunTime;
			}
			set
			{
				this._NextRunTime = value;
				this.OnPropertyChanged("NextRunTime");
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x00012C82 File Offset: 0x00010E82
		// (set) Token: 0x06000939 RID: 2361 RVA: 0x00012C8A File Offset: 0x00010E8A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("NextRunTimeSpecified")]
		public bool NextRunTimeSpecified
		{
			get
			{
				return this._NextRunTimeSpecified;
			}
			set
			{
				this._NextRunTimeSpecified = value;
				this.OnPropertyChanged("NextRunTimeSpecified");
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x00012C9E File Offset: 0x00010E9E
		// (set) Token: 0x0600093B RID: 2363 RVA: 0x00012CA6 File Offset: 0x00010EA6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("LastRunTime")]
		public DateTimeOffset LastRunTime
		{
			get
			{
				return this._LastRunTime;
			}
			set
			{
				this._LastRunTime = value;
				this.OnPropertyChanged("LastRunTime");
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x00012CBA File Offset: 0x00010EBA
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x00012CC2 File Offset: 0x00010EC2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("LastRunTimeSpecified")]
		public bool LastRunTimeSpecified
		{
			get
			{
				return this._LastRunTimeSpecified;
			}
			set
			{
				this._LastRunTimeSpecified = value;
				this.OnPropertyChanged("LastRunTimeSpecified");
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x00012CD6 File Offset: 0x00010ED6
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x00012CDE File Offset: 0x00010EDE
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ReferencesPresent")]
		public bool ReferencesPresent
		{
			get
			{
				return this._ReferencesPresent;
			}
			set
			{
				this._ReferencesPresent = value;
				this.OnPropertyChanged("ReferencesPresent");
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00012CF2 File Offset: 0x00010EF2
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x00012CFA File Offset: 0x00010EFA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("State")]
		public ScheduleStateEnum State
		{
			get
			{
				return this._State;
			}
			set
			{
				this._State = value;
				this.OnPropertyChanged("State");
			}
		}

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x06000942 RID: 2370 RVA: 0x00012D10 File Offset: 0x00010F10
		// (remove) Token: 0x06000943 RID: 2371 RVA: 0x00012D48 File Offset: 0x00010F48
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000944 RID: 2372 RVA: 0x00012D7D File Offset: 0x00010F7D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00012D9C File Offset: 0x00010F9C
		[OriginalName("Pause")]
		public DataServiceActionQuery Pause()
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuery(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.Pause", Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00012E00 File Offset: 0x00011000
		[OriginalName("Resume")]
		public DataServiceActionQuery Resume()
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuery(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.Resume", Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x04000456 RID: 1110
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x04000457 RID: 1111
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x04000458 RID: 1112
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ScheduleDefinition _Definition;

		// Token: 0x04000459 RID: 1113
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Description;

		// Token: 0x0400045A RID: 1114
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Creator;

		// Token: 0x0400045B RID: 1115
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset _NextRunTime;

		// Token: 0x0400045C RID: 1116
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _NextRunTimeSpecified;

		// Token: 0x0400045D RID: 1117
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset _LastRunTime;

		// Token: 0x0400045E RID: 1118
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _LastRunTimeSpecified;

		// Token: 0x0400045F RID: 1119
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _ReferencesPresent;

		// Token: 0x04000460 RID: 1120
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ScheduleStateEnum _State;
	}
}
