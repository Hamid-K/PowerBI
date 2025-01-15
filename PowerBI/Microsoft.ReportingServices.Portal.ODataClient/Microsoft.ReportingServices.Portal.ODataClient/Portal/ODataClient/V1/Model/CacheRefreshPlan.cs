using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000B4 RID: 180
	[Key("Id")]
	[EntitySet("CacheRefreshPlan")]
	[OriginalName("CacheRefreshPlan")]
	public class CacheRefreshPlan : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x0600077A RID: 1914 RVA: 0x0000F403 File Offset: 0x0000D603
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static CacheRefreshPlan CreateCacheRefreshPlan(Guid ID)
		{
			return new CacheRefreshPlan
			{
				Id = ID
			};
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x0000F411 File Offset: 0x0000D611
		// (set) Token: 0x0600077C RID: 1916 RVA: 0x0000F419 File Offset: 0x0000D619
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

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x0000F42D File Offset: 0x0000D62D
		// (set) Token: 0x0600077E RID: 1918 RVA: 0x0000F435 File Offset: 0x0000D635
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

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x0000F449 File Offset: 0x0000D649
		// (set) Token: 0x06000780 RID: 1920 RVA: 0x0000F451 File Offset: 0x0000D651
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

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x0000F465 File Offset: 0x0000D665
		// (set) Token: 0x06000782 RID: 1922 RVA: 0x0000F46D File Offset: 0x0000D66D
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

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x0000F481 File Offset: 0x0000D681
		// (set) Token: 0x06000784 RID: 1924 RVA: 0x0000F489 File Offset: 0x0000D689
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

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x0000F49D File Offset: 0x0000D69D
		// (set) Token: 0x06000786 RID: 1926 RVA: 0x0000F4A5 File Offset: 0x0000D6A5
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

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x0000F4B9 File Offset: 0x0000D6B9
		// (set) Token: 0x06000788 RID: 1928 RVA: 0x0000F4C1 File Offset: 0x0000D6C1
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

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x0000F4D5 File Offset: 0x0000D6D5
		// (set) Token: 0x0600078A RID: 1930 RVA: 0x0000F4DD File Offset: 0x0000D6DD
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

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x0000F4F1 File Offset: 0x0000D6F1
		// (set) Token: 0x0600078C RID: 1932 RVA: 0x0000F4F9 File Offset: 0x0000D6F9
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

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x0000F50D File Offset: 0x0000D70D
		// (set) Token: 0x0600078E RID: 1934 RVA: 0x0000F515 File Offset: 0x0000D715
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

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x0000F529 File Offset: 0x0000D729
		// (set) Token: 0x06000790 RID: 1936 RVA: 0x0000F531 File Offset: 0x0000D731
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

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06000791 RID: 1937 RVA: 0x0000F548 File Offset: 0x0000D748
		// (remove) Token: 0x06000792 RID: 1938 RVA: 0x0000F580 File Offset: 0x0000D780
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000793 RID: 1939 RVA: 0x0000F5B5 File Offset: 0x0000D7B5
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400039F RID: 927
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x040003A0 RID: 928
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Owner;

		// Token: 0x040003A1 RID: 929
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Description;

		// Token: 0x040003A2 RID: 930
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _CatalogItemPath;

		// Token: 0x040003A3 RID: 931
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _EventType;

		// Token: 0x040003A4 RID: 932
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ScheduleReference _Schedule;

		// Token: 0x040003A5 RID: 933
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset? _LastRunTime;

		// Token: 0x040003A6 RID: 934
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _LastStatus;

		// Token: 0x040003A7 RID: 935
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ModifiedBy;

		// Token: 0x040003A8 RID: 936
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset? _ModifiedDate;

		// Token: 0x040003A9 RID: 937
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<ParameterValue> _ParameterValues = new ObservableCollection<ParameterValue>();
	}
}
