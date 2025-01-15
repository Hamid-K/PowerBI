using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000058 RID: 88
	[Key("Id")]
	[EntitySet("Subscriptions")]
	[OriginalName("Subscription")]
	public class Subscription : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x060003DA RID: 986 RVA: 0x00008BC2 File Offset: 0x00006DC2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static Subscription CreateSubscription(Guid ID, bool isDataDriven, bool isActive)
		{
			return new Subscription
			{
				Id = ID,
				IsDataDriven = isDataDriven,
				IsActive = isActive
			};
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00008BDE File Offset: 0x00006DDE
		// (set) Token: 0x060003DC RID: 988 RVA: 0x00008BE6 File Offset: 0x00006DE6
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

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00008BFA File Offset: 0x00006DFA
		// (set) Token: 0x060003DE RID: 990 RVA: 0x00008C02 File Offset: 0x00006E02
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Owner")]
		public string Owner
		{
			get
			{
				return this._Owner;
			}
			set
			{
				this._Owner = value;
				this.OnPropertyChanged("Owner");
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00008C16 File Offset: 0x00006E16
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x00008C1E File Offset: 0x00006E1E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("IsDataDriven")]
		public bool IsDataDriven
		{
			get
			{
				return this._IsDataDriven;
			}
			set
			{
				this._IsDataDriven = value;
				this.OnPropertyChanged("IsDataDriven");
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00008C32 File Offset: 0x00006E32
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x00008C3A File Offset: 0x00006E3A
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

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00008C4E File Offset: 0x00006E4E
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x00008C56 File Offset: 0x00006E56
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Report")]
		public string Report
		{
			get
			{
				return this._Report;
			}
			set
			{
				this._Report = value;
				this.OnPropertyChanged("Report");
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00008C6A File Offset: 0x00006E6A
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x00008C72 File Offset: 0x00006E72
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("IsActive")]
		public bool IsActive
		{
			get
			{
				return this._IsActive;
			}
			set
			{
				this._IsActive = value;
				this.OnPropertyChanged("IsActive");
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00008C86 File Offset: 0x00006E86
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x00008C8E File Offset: 0x00006E8E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("EventType")]
		public string EventType
		{
			get
			{
				return this._EventType;
			}
			set
			{
				this._EventType = value;
				this.OnPropertyChanged("EventType");
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00008CA2 File Offset: 0x00006EA2
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x00008CAA File Offset: 0x00006EAA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Schedule")]
		public ScheduleReference Schedule
		{
			get
			{
				return this._Schedule;
			}
			set
			{
				this._Schedule = value;
				this.OnPropertyChanged("Schedule");
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00008CBE File Offset: 0x00006EBE
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x00008CC6 File Offset: 0x00006EC6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ScheduleDescription")]
		public string ScheduleDescription
		{
			get
			{
				return this._ScheduleDescription;
			}
			set
			{
				this._ScheduleDescription = value;
				this.OnPropertyChanged("ScheduleDescription");
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00008CDA File Offset: 0x00006EDA
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x00008CE2 File Offset: 0x00006EE2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("LastRunTime")]
		public DateTimeOffset? LastRunTime
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

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00008CF6 File Offset: 0x00006EF6
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x00008CFE File Offset: 0x00006EFE
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("LastStatus")]
		public string LastStatus
		{
			get
			{
				return this._LastStatus;
			}
			set
			{
				this._LastStatus = value;
				this.OnPropertyChanged("LastStatus");
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00008D12 File Offset: 0x00006F12
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x00008D1A File Offset: 0x00006F1A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DataQuery")]
		public Query DataQuery
		{
			get
			{
				return this._DataQuery;
			}
			set
			{
				this._DataQuery = value;
				this.OnPropertyChanged("DataQuery");
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00008D2E File Offset: 0x00006F2E
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x00008D36 File Offset: 0x00006F36
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ExtensionSettings")]
		public ExtensionSettings ExtensionSettings
		{
			get
			{
				return this._ExtensionSettings;
			}
			set
			{
				this._ExtensionSettings = value;
				this.OnPropertyChanged("ExtensionSettings");
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00008D4A File Offset: 0x00006F4A
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x00008D52 File Offset: 0x00006F52
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DeliveryExtension")]
		public string DeliveryExtension
		{
			get
			{
				return this._DeliveryExtension;
			}
			set
			{
				this._DeliveryExtension = value;
				this.OnPropertyChanged("DeliveryExtension");
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00008D66 File Offset: 0x00006F66
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x00008D6E File Offset: 0x00006F6E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("LocalizedDeliveryExtensionName")]
		public string LocalizedDeliveryExtensionName
		{
			get
			{
				return this._LocalizedDeliveryExtensionName;
			}
			set
			{
				this._LocalizedDeliveryExtensionName = value;
				this.OnPropertyChanged("LocalizedDeliveryExtensionName");
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00008D82 File Offset: 0x00006F82
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x00008D8A File Offset: 0x00006F8A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ModifiedBy")]
		public string ModifiedBy
		{
			get
			{
				return this._ModifiedBy;
			}
			set
			{
				this._ModifiedBy = value;
				this.OnPropertyChanged("ModifiedBy");
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00008D9E File Offset: 0x00006F9E
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x00008DA6 File Offset: 0x00006FA6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ModifiedDate")]
		public DateTimeOffset? ModifiedDate
		{
			get
			{
				return this._ModifiedDate;
			}
			set
			{
				this._ModifiedDate = value;
				this.OnPropertyChanged("ModifiedDate");
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x00008DBA File Offset: 0x00006FBA
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x00008DC2 File Offset: 0x00006FC2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ParameterValues")]
		public ObservableCollection<ParameterValue> ParameterValues
		{
			get
			{
				return this._ParameterValues;
			}
			set
			{
				this._ParameterValues = value;
				this.OnPropertyChanged("ParameterValues");
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x00008DD6 File Offset: 0x00006FD6
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x00008DDE File Offset: 0x00006FDE
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DataSource")]
		public DataSource DataSource
		{
			get
			{
				return this._DataSource;
			}
			set
			{
				this._DataSource = value;
				this.OnPropertyChanged("DataSource");
			}
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000401 RID: 1025 RVA: 0x00008DF4 File Offset: 0x00006FF4
		// (remove) Token: 0x06000402 RID: 1026 RVA: 0x00008E2C File Offset: 0x0000702C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000403 RID: 1027 RVA: 0x00008E61 File Offset: 0x00007061
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00008E80 File Offset: 0x00007080
		[OriginalName("Enable")]
		public DataServiceActionQuery Enable()
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuery(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.Enable", Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00008EE4 File Offset: 0x000070E4
		[OriginalName("Disable")]
		public DataServiceActionQuery Disable()
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuery(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.Disable", Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00008F48 File Offset: 0x00007148
		[OriginalName("Execute")]
		public DataServiceActionQuery Execute()
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuery(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.Execute", Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x040001DB RID: 475
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x040001DC RID: 476
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Owner;

		// Token: 0x040001DD RID: 477
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsDataDriven;

		// Token: 0x040001DE RID: 478
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Description;

		// Token: 0x040001DF RID: 479
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Report;

		// Token: 0x040001E0 RID: 480
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsActive;

		// Token: 0x040001E1 RID: 481
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _EventType;

		// Token: 0x040001E2 RID: 482
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ScheduleReference _Schedule;

		// Token: 0x040001E3 RID: 483
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ScheduleDescription;

		// Token: 0x040001E4 RID: 484
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset? _LastRunTime;

		// Token: 0x040001E5 RID: 485
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _LastStatus;

		// Token: 0x040001E6 RID: 486
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Query _DataQuery;

		// Token: 0x040001E7 RID: 487
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ExtensionSettings _ExtensionSettings;

		// Token: 0x040001E8 RID: 488
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _DeliveryExtension;

		// Token: 0x040001E9 RID: 489
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _LocalizedDeliveryExtensionName;

		// Token: 0x040001EA RID: 490
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ModifiedBy;

		// Token: 0x040001EB RID: 491
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset? _ModifiedDate;

		// Token: 0x040001EC RID: 492
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<ParameterValue> _ParameterValues = new ObservableCollection<ParameterValue>();

		// Token: 0x040001ED RID: 493
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataSource _DataSource;
	}
}
