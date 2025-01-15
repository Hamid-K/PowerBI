using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200005A RID: 90
	[Key("Id")]
	[EntitySet("SystemResources")]
	[OriginalName("SystemResource")]
	public class SystemResource : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x00009059 File Offset: 0x00007259
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static SystemResource CreateSystemResource(Guid ID, SystemResourceType type, bool isEmbedded)
		{
			return new SystemResource
			{
				Id = ID,
				Type = type,
				IsEmbedded = isEmbedded
			};
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x00009075 File Offset: 0x00007275
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x0000907D File Offset: 0x0000727D
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

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x00009091 File Offset: 0x00007291
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x00009099 File Offset: 0x00007299
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Type")]
		public SystemResourceType Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
				this.OnPropertyChanged("Type");
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x000090AD File Offset: 0x000072AD
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x000090B5 File Offset: 0x000072B5
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("TypeName")]
		public string TypeName
		{
			get
			{
				return this._TypeName;
			}
			set
			{
				this._TypeName = value;
				this.OnPropertyChanged("TypeName");
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x000090C9 File Offset: 0x000072C9
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x000090D1 File Offset: 0x000072D1
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

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x000090E5 File Offset: 0x000072E5
		// (set) Token: 0x06000417 RID: 1047 RVA: 0x000090ED File Offset: 0x000072ED
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Version")]
		public string Version
		{
			get
			{
				return this._Version;
			}
			set
			{
				this._Version = value;
				this.OnPropertyChanged("Version");
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00009101 File Offset: 0x00007301
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x00009109 File Offset: 0x00007309
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("IsEmbedded")]
		public bool IsEmbedded
		{
			get
			{
				return this._IsEmbedded;
			}
			set
			{
				this._IsEmbedded = value;
				this.OnPropertyChanged("IsEmbedded");
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000911D File Offset: 0x0000731D
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x00009125 File Offset: 0x00007325
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("PackageContent")]
		public CatalogItem PackageContent
		{
			get
			{
				return this._PackageContent;
			}
			set
			{
				this._PackageContent = value;
				this.OnPropertyChanged("PackageContent");
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x00009139 File Offset: 0x00007339
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x00009141 File Offset: 0x00007341
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Items")]
		public DataServiceCollection<SystemResourceItem> Items
		{
			get
			{
				return this._Items;
			}
			set
			{
				this._Items = value;
				this.OnPropertyChanged("Items");
			}
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x0600041E RID: 1054 RVA: 0x00009158 File Offset: 0x00007358
		// (remove) Token: 0x0600041F RID: 1055 RVA: 0x00009190 File Offset: 0x00007390
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000420 RID: 1056 RVA: 0x000091C5 File Offset: 0x000073C5
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040001F1 RID: 497
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x040001F2 RID: 498
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private SystemResourceType _Type;

		// Token: 0x040001F3 RID: 499
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _TypeName;

		// Token: 0x040001F4 RID: 500
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x040001F5 RID: 501
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Version;

		// Token: 0x040001F6 RID: 502
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsEmbedded;

		// Token: 0x040001F7 RID: 503
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CatalogItem _PackageContent;

		// Token: 0x040001F8 RID: 504
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<SystemResourceItem> _Items = new DataServiceCollection<SystemResourceItem>(null, TrackingMode.None);
	}
}
