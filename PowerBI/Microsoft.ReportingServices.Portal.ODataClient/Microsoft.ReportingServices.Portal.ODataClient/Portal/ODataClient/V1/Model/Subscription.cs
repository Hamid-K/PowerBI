using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000B2 RID: 178
	[Key("Id")]
	[EntitySet("Subscriptions")]
	[OriginalName("Subscription")]
	public class Subscription : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x0600074A RID: 1866 RVA: 0x0000F04F File Offset: 0x0000D24F
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

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x0000F06B File Offset: 0x0000D26B
		// (set) Token: 0x0600074C RID: 1868 RVA: 0x0000F073 File Offset: 0x0000D273
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

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x0000F087 File Offset: 0x0000D287
		// (set) Token: 0x0600074E RID: 1870 RVA: 0x0000F08F File Offset: 0x0000D28F
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

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x0000F0A3 File Offset: 0x0000D2A3
		// (set) Token: 0x06000750 RID: 1872 RVA: 0x0000F0AB File Offset: 0x0000D2AB
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

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x0000F0BF File Offset: 0x0000D2BF
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x0000F0C7 File Offset: 0x0000D2C7
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

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0000F0DB File Offset: 0x0000D2DB
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x0000F0E3 File Offset: 0x0000D2E3
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

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x0000F0F7 File Offset: 0x0000D2F7
		// (set) Token: 0x06000756 RID: 1878 RVA: 0x0000F0FF File Offset: 0x0000D2FF
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

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x0000F113 File Offset: 0x0000D313
		// (set) Token: 0x06000758 RID: 1880 RVA: 0x0000F11B File Offset: 0x0000D31B
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

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0000F12F File Offset: 0x0000D32F
		// (set) Token: 0x0600075A RID: 1882 RVA: 0x0000F137 File Offset: 0x0000D337
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

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0000F14B File Offset: 0x0000D34B
		// (set) Token: 0x0600075C RID: 1884 RVA: 0x0000F153 File Offset: 0x0000D353
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

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x0000F167 File Offset: 0x0000D367
		// (set) Token: 0x0600075E RID: 1886 RVA: 0x0000F16F File Offset: 0x0000D36F
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

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0000F183 File Offset: 0x0000D383
		// (set) Token: 0x06000760 RID: 1888 RVA: 0x0000F18B File Offset: 0x0000D38B
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

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x0000F19F File Offset: 0x0000D39F
		// (set) Token: 0x06000762 RID: 1890 RVA: 0x0000F1A7 File Offset: 0x0000D3A7
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

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0000F1BB File Offset: 0x0000D3BB
		// (set) Token: 0x06000764 RID: 1892 RVA: 0x0000F1C3 File Offset: 0x0000D3C3
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

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0000F1D7 File Offset: 0x0000D3D7
		// (set) Token: 0x06000766 RID: 1894 RVA: 0x0000F1DF File Offset: 0x0000D3DF
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

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x0000F1F3 File Offset: 0x0000D3F3
		// (set) Token: 0x06000768 RID: 1896 RVA: 0x0000F1FB File Offset: 0x0000D3FB
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

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x0000F20F File Offset: 0x0000D40F
		// (set) Token: 0x0600076A RID: 1898 RVA: 0x0000F217 File Offset: 0x0000D417
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

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x0000F22B File Offset: 0x0000D42B
		// (set) Token: 0x0600076C RID: 1900 RVA: 0x0000F233 File Offset: 0x0000D433
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

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x0000F247 File Offset: 0x0000D447
		// (set) Token: 0x0600076E RID: 1902 RVA: 0x0000F24F File Offset: 0x0000D44F
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

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x0000F263 File Offset: 0x0000D463
		// (set) Token: 0x06000770 RID: 1904 RVA: 0x0000F26B File Offset: 0x0000D46B
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

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06000771 RID: 1905 RVA: 0x0000F280 File Offset: 0x0000D480
		// (remove) Token: 0x06000772 RID: 1906 RVA: 0x0000F2B8 File Offset: 0x0000D4B8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000773 RID: 1907 RVA: 0x0000F2ED File Offset: 0x0000D4ED
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0000F30C File Offset: 0x0000D50C
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

		// Token: 0x06000775 RID: 1909 RVA: 0x0000F370 File Offset: 0x0000D570
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

		// Token: 0x0400038B RID: 907
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x0400038C RID: 908
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Owner;

		// Token: 0x0400038D RID: 909
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsDataDriven;

		// Token: 0x0400038E RID: 910
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Description;

		// Token: 0x0400038F RID: 911
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Report;

		// Token: 0x04000390 RID: 912
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsActive;

		// Token: 0x04000391 RID: 913
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _EventType;

		// Token: 0x04000392 RID: 914
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ScheduleReference _Schedule;

		// Token: 0x04000393 RID: 915
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ScheduleDescription;

		// Token: 0x04000394 RID: 916
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset? _LastRunTime;

		// Token: 0x04000395 RID: 917
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _LastStatus;

		// Token: 0x04000396 RID: 918
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Query _DataQuery;

		// Token: 0x04000397 RID: 919
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ExtensionSettings _ExtensionSettings;

		// Token: 0x04000398 RID: 920
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _DeliveryExtension;

		// Token: 0x04000399 RID: 921
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _LocalizedDeliveryExtensionName;

		// Token: 0x0400039A RID: 922
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ModifiedBy;

		// Token: 0x0400039B RID: 923
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset? _ModifiedDate;

		// Token: 0x0400039C RID: 924
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<ParameterValue> _ParameterValues = new ObservableCollection<ParameterValue>();

		// Token: 0x0400039D RID: 925
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataSource _DataSource;
	}
}
