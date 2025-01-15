using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200004E RID: 78
	[Key("Id")]
	[EntitySet("Schedules")]
	[OriginalName("Schedule")]
	public class Schedule : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000368 RID: 872 RVA: 0x00008180 File Offset: 0x00006380
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

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000369 RID: 873 RVA: 0x000081BB File Offset: 0x000063BB
		// (set) Token: 0x0600036A RID: 874 RVA: 0x000081C3 File Offset: 0x000063C3
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

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600036B RID: 875 RVA: 0x000081D7 File Offset: 0x000063D7
		// (set) Token: 0x0600036C RID: 876 RVA: 0x000081DF File Offset: 0x000063DF
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

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600036D RID: 877 RVA: 0x000081F3 File Offset: 0x000063F3
		// (set) Token: 0x0600036E RID: 878 RVA: 0x000081FB File Offset: 0x000063FB
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

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000820F File Offset: 0x0000640F
		// (set) Token: 0x06000370 RID: 880 RVA: 0x00008217 File Offset: 0x00006417
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

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000822B File Offset: 0x0000642B
		// (set) Token: 0x06000372 RID: 882 RVA: 0x00008233 File Offset: 0x00006433
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

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00008247 File Offset: 0x00006447
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0000824F File Offset: 0x0000644F
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

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00008263 File Offset: 0x00006463
		// (set) Token: 0x06000376 RID: 886 RVA: 0x0000826B File Offset: 0x0000646B
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

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000827F File Offset: 0x0000647F
		// (set) Token: 0x06000378 RID: 888 RVA: 0x00008287 File Offset: 0x00006487
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

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000829B File Offset: 0x0000649B
		// (set) Token: 0x0600037A RID: 890 RVA: 0x000082A3 File Offset: 0x000064A3
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

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600037B RID: 891 RVA: 0x000082B7 File Offset: 0x000064B7
		// (set) Token: 0x0600037C RID: 892 RVA: 0x000082BF File Offset: 0x000064BF
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

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600037D RID: 893 RVA: 0x000082D3 File Offset: 0x000064D3
		// (set) Token: 0x0600037E RID: 894 RVA: 0x000082DB File Offset: 0x000064DB
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

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x0600037F RID: 895 RVA: 0x000082F0 File Offset: 0x000064F0
		// (remove) Token: 0x06000380 RID: 896 RVA: 0x00008328 File Offset: 0x00006528
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000381 RID: 897 RVA: 0x0000835D File Offset: 0x0000655D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000837C File Offset: 0x0000657C
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

		// Token: 0x06000383 RID: 899 RVA: 0x000083E0 File Offset: 0x000065E0
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

		// Token: 0x040001B0 RID: 432
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x040001B1 RID: 433
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x040001B2 RID: 434
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ScheduleDefinition _Definition;

		// Token: 0x040001B3 RID: 435
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Description;

		// Token: 0x040001B4 RID: 436
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Creator;

		// Token: 0x040001B5 RID: 437
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset _NextRunTime;

		// Token: 0x040001B6 RID: 438
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _NextRunTimeSpecified;

		// Token: 0x040001B7 RID: 439
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset _LastRunTime;

		// Token: 0x040001B8 RID: 440
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _LastRunTimeSpecified;

		// Token: 0x040001B9 RID: 441
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _ReferencesPresent;

		// Token: 0x040001BA RID: 442
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ScheduleStateEnum _State;
	}
}
