using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000DE RID: 222
	[Key("Id")]
	[EntitySet("SystemResources")]
	[OriginalName("SystemResource")]
	public class SystemResource : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x060009F1 RID: 2545 RVA: 0x000142C4 File Offset: 0x000124C4
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

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x000142E0 File Offset: 0x000124E0
		// (set) Token: 0x060009F3 RID: 2547 RVA: 0x000142E8 File Offset: 0x000124E8
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

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x000142FC File Offset: 0x000124FC
		// (set) Token: 0x060009F5 RID: 2549 RVA: 0x00014304 File Offset: 0x00012504
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

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x00014318 File Offset: 0x00012518
		// (set) Token: 0x060009F7 RID: 2551 RVA: 0x00014320 File Offset: 0x00012520
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

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x00014334 File Offset: 0x00012534
		// (set) Token: 0x060009F9 RID: 2553 RVA: 0x0001433C File Offset: 0x0001253C
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

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x00014350 File Offset: 0x00012550
		// (set) Token: 0x060009FB RID: 2555 RVA: 0x00014358 File Offset: 0x00012558
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

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x0001436C File Offset: 0x0001256C
		// (set) Token: 0x060009FD RID: 2557 RVA: 0x00014374 File Offset: 0x00012574
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

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x00014388 File Offset: 0x00012588
		// (set) Token: 0x060009FF RID: 2559 RVA: 0x00014390 File Offset: 0x00012590
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

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x000143A4 File Offset: 0x000125A4
		// (set) Token: 0x06000A01 RID: 2561 RVA: 0x000143AC File Offset: 0x000125AC
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

		// Token: 0x1400006A RID: 106
		// (add) Token: 0x06000A02 RID: 2562 RVA: 0x000143C0 File Offset: 0x000125C0
		// (remove) Token: 0x06000A03 RID: 2563 RVA: 0x000143F8 File Offset: 0x000125F8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000A04 RID: 2564 RVA: 0x0001442D File Offset: 0x0001262D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400049E RID: 1182
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x0400049F RID: 1183
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private SystemResourceType _Type;

		// Token: 0x040004A0 RID: 1184
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _TypeName;

		// Token: 0x040004A1 RID: 1185
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x040004A2 RID: 1186
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Version;

		// Token: 0x040004A3 RID: 1187
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsEmbedded;

		// Token: 0x040004A4 RID: 1188
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CatalogItem _PackageContent;

		// Token: 0x040004A5 RID: 1189
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<SystemResourceItem> _Items = new DataServiceCollection<SystemResourceItem>(null, TrackingMode.None);
	}
}
