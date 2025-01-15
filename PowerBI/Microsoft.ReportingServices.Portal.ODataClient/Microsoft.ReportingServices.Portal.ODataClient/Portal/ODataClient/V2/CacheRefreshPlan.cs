using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000011 RID: 17
	[Key("Id")]
	[EntitySet("CacheRefreshPlans")]
	[OriginalName("CacheRefreshPlan")]
	public class CacheRefreshPlan : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00002D32 File Offset: 0x00000F32
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static CacheRefreshPlan CreateCacheRefreshPlan(Guid ID)
		{
			return new CacheRefreshPlan
			{
				Id = ID
			};
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002D40 File Offset: 0x00000F40
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00002D48 File Offset: 0x00000F48
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

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002D5C File Offset: 0x00000F5C
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00002D64 File Offset: 0x00000F64
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

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002D78 File Offset: 0x00000F78
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00002D80 File Offset: 0x00000F80
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

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002D94 File Offset: 0x00000F94
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00002D9C File Offset: 0x00000F9C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CatalogItemPath")]
		public string CatalogItemPath
		{
			get
			{
				return this._CatalogItemPath;
			}
			set
			{
				this._CatalogItemPath = value;
				this.OnPropertyChanged("CatalogItemPath");
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00002DB0 File Offset: 0x00000FB0
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00002DB8 File Offset: 0x00000FB8
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

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00002DCC File Offset: 0x00000FCC
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00002DD4 File Offset: 0x00000FD4
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

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00002DE8 File Offset: 0x00000FE8
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00002DF0 File Offset: 0x00000FF0
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

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00002E04 File Offset: 0x00001004
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00002E0C File Offset: 0x0000100C
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

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00002E20 File Offset: 0x00001020
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00002E28 File Offset: 0x00001028
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

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00002E3C File Offset: 0x0000103C
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00002E44 File Offset: 0x00001044
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

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00002E58 File Offset: 0x00001058
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00002E60 File Offset: 0x00001060
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

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00002E74 File Offset: 0x00001074
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00002E7C File Offset: 0x0000107C
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

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00002E90 File Offset: 0x00001090
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00002E98 File Offset: 0x00001098
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("History")]
		public DataServiceCollection<SubscriptionHistory> History
		{
			get
			{
				return this._History;
			}
			set
			{
				this._History = value;
				this.OnPropertyChanged("History");
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060000A6 RID: 166 RVA: 0x00002EAC File Offset: 0x000010AC
		// (remove) Token: 0x060000A7 RID: 167 RVA: 0x00002EE4 File Offset: 0x000010E4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060000A8 RID: 168 RVA: 0x00002F19 File Offset: 0x00001119
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00002F38 File Offset: 0x00001138
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

		// Token: 0x0400006D RID: 109
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x0400006E RID: 110
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Owner;

		// Token: 0x0400006F RID: 111
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Description;

		// Token: 0x04000070 RID: 112
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _CatalogItemPath;

		// Token: 0x04000071 RID: 113
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _EventType;

		// Token: 0x04000072 RID: 114
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ScheduleReference _Schedule;

		// Token: 0x04000073 RID: 115
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ScheduleDescription;

		// Token: 0x04000074 RID: 116
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset? _LastRunTime;

		// Token: 0x04000075 RID: 117
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _LastStatus;

		// Token: 0x04000076 RID: 118
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ModifiedBy;

		// Token: 0x04000077 RID: 119
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset? _ModifiedDate;

		// Token: 0x04000078 RID: 120
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<ParameterValue> _ParameterValues = new ObservableCollection<ParameterValue>();

		// Token: 0x04000079 RID: 121
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<SubscriptionHistory> _History = new DataServiceCollection<SubscriptionHistory>(null, TrackingMode.None);
	}
}
