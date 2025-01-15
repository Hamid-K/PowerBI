using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200001B RID: 27
	[Key("Id")]
	[EntitySet("CatalogItems")]
	[OriginalName("CatalogItem")]
	public abstract class CatalogItem : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000363C File Offset: 0x0000183C
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00003644 File Offset: 0x00001844
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

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00003658 File Offset: 0x00001858
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00003660 File Offset: 0x00001860
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

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00003674 File Offset: 0x00001874
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x0000367C File Offset: 0x0000187C
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

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00003690 File Offset: 0x00001890
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00003698 File Offset: 0x00001898
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Path")]
		public string Path
		{
			get
			{
				return this._Path;
			}
			set
			{
				this._Path = value;
				this.OnPropertyChanged("Path");
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000036AC File Offset: 0x000018AC
		// (set) Token: 0x060000FC RID: 252 RVA: 0x000036B4 File Offset: 0x000018B4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Type")]
		public CatalogItemType Type
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

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000036C8 File Offset: 0x000018C8
		// (set) Token: 0x060000FE RID: 254 RVA: 0x000036D0 File Offset: 0x000018D0
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Hidden")]
		public bool Hidden
		{
			get
			{
				return this._Hidden;
			}
			set
			{
				this._Hidden = value;
				this.OnPropertyChanged("Hidden");
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000036E4 File Offset: 0x000018E4
		// (set) Token: 0x06000100 RID: 256 RVA: 0x000036EC File Offset: 0x000018EC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Size")]
		public long Size
		{
			get
			{
				return this._Size;
			}
			set
			{
				this._Size = value;
				this.OnPropertyChanged("Size");
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00003700 File Offset: 0x00001900
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00003708 File Offset: 0x00001908
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

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000371C File Offset: 0x0000191C
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00003724 File Offset: 0x00001924
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ModifiedDate")]
		public DateTimeOffset ModifiedDate
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

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00003738 File Offset: 0x00001938
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00003740 File Offset: 0x00001940
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CreatedBy")]
		public string CreatedBy
		{
			get
			{
				return this._CreatedBy;
			}
			set
			{
				this._CreatedBy = value;
				this.OnPropertyChanged("CreatedBy");
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00003754 File Offset: 0x00001954
		// (set) Token: 0x06000108 RID: 264 RVA: 0x0000375C File Offset: 0x0000195C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CreatedDate")]
		public DateTimeOffset CreatedDate
		{
			get
			{
				return this._CreatedDate;
			}
			set
			{
				this._CreatedDate = value;
				this.OnPropertyChanged("CreatedDate");
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00003770 File Offset: 0x00001970
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00003778 File Offset: 0x00001978
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ParentFolderId")]
		public Guid? ParentFolderId
		{
			get
			{
				return this._ParentFolderId;
			}
			set
			{
				this._ParentFolderId = value;
				this.OnPropertyChanged("ParentFolderId");
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000378C File Offset: 0x0000198C
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00003794 File Offset: 0x00001994
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("IsFavorite")]
		public bool IsFavorite
		{
			get
			{
				return this._IsFavorite;
			}
			set
			{
				this._IsFavorite = value;
				this.OnPropertyChanged("IsFavorite");
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000037A8 File Offset: 0x000019A8
		// (set) Token: 0x0600010E RID: 270 RVA: 0x000037B0 File Offset: 0x000019B0
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Roles")]
		public ObservableCollection<Role> Roles
		{
			get
			{
				return this._Roles;
			}
			set
			{
				this._Roles = value;
				this.OnPropertyChanged("Roles");
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600010F RID: 271 RVA: 0x000037C4 File Offset: 0x000019C4
		// (set) Token: 0x06000110 RID: 272 RVA: 0x000037CC File Offset: 0x000019CC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ContentType")]
		public string ContentType
		{
			get
			{
				return this._ContentType;
			}
			set
			{
				this._ContentType = value;
				this.OnPropertyChanged("ContentType");
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000037E0 File Offset: 0x000019E0
		// (set) Token: 0x06000112 RID: 274 RVA: 0x000037E8 File Offset: 0x000019E8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Content")]
		public byte[] Content
		{
			get
			{
				return this._Content;
			}
			set
			{
				this._Content = value;
				this.OnPropertyChanged("Content");
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000037FC File Offset: 0x000019FC
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00003804 File Offset: 0x00001A04
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ParentFolder")]
		public Folder ParentFolder
		{
			get
			{
				return this._ParentFolder;
			}
			set
			{
				this._ParentFolder = value;
				this.OnPropertyChanged("ParentFolder");
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00003818 File Offset: 0x00001A18
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00003820 File Offset: 0x00001A20
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Properties")]
		public DataServiceCollection<Property> Properties
		{
			get
			{
				return this._Properties;
			}
			set
			{
				this._Properties = value;
				this.OnPropertyChanged("Properties");
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00003834 File Offset: 0x00001A34
		// (set) Token: 0x06000118 RID: 280 RVA: 0x0000383C File Offset: 0x00001A3C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Comments")]
		public DataServiceCollection<Comment> Comments
		{
			get
			{
				return this._Comments;
			}
			set
			{
				this._Comments = value;
				this.OnPropertyChanged("Comments");
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00003850 File Offset: 0x00001A50
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00003858 File Offset: 0x00001A58
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("AlertSubscriptions")]
		public DataServiceCollection<AlertSubscription> AlertSubscriptions
		{
			get
			{
				return this._AlertSubscriptions;
			}
			set
			{
				this._AlertSubscriptions = value;
				this.OnPropertyChanged("AlertSubscriptions");
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000386C File Offset: 0x00001A6C
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00003874 File Offset: 0x00001A74
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("AllowedActions")]
		public DataServiceCollection<AllowedAction> AllowedActions
		{
			get
			{
				return this._AllowedActions;
			}
			set
			{
				this._AllowedActions = value;
				this.OnPropertyChanged("AllowedActions");
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00003888 File Offset: 0x00001A88
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00003890 File Offset: 0x00001A90
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Policies")]
		public DataServiceCollection<ItemPolicy> Policies
		{
			get
			{
				return this._Policies;
			}
			set
			{
				this._Policies = value;
				this.OnPropertyChanged("Policies");
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000038A4 File Offset: 0x00001AA4
		// (set) Token: 0x06000120 RID: 288 RVA: 0x000038AC File Offset: 0x00001AAC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DependentItems")]
		public DataServiceCollection<CatalogItem> DependentItems
		{
			get
			{
				return this._DependentItems;
			}
			set
			{
				this._DependentItems = value;
				this.OnPropertyChanged("DependentItems");
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000121 RID: 289 RVA: 0x000038C0 File Offset: 0x00001AC0
		// (remove) Token: 0x06000122 RID: 290 RVA: 0x000038F8 File Offset: 0x00001AF8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000123 RID: 291 RVA: 0x0000392D File Offset: 0x00001B2D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000394C File Offset: 0x00001B4C
		[OriginalName("AccessToken")]
		public DataServiceQuerySingle<CatalogItemAccessToken> AccessToken()
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuerySingle<CatalogItemAccessToken>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.AccessToken", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000039CC File Offset: 0x00001BCC
		[OriginalName("GetContentTrusted")]
		public DataServiceActionQuerySingle<string> GetContentTrusted(string TrustedProcessToken)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<string>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.GetContentTrusted", new BodyOperationParameter[]
			{
				new BodyOperationParameter("TrustedProcessToken", TrustedProcessToken)
			});
		}

		// Token: 0x04000095 RID: 149
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x04000096 RID: 150
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x04000097 RID: 151
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Description;

		// Token: 0x04000098 RID: 152
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Path;

		// Token: 0x04000099 RID: 153
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CatalogItemType _Type;

		// Token: 0x0400009A RID: 154
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Hidden;

		// Token: 0x0400009B RID: 155
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private long _Size;

		// Token: 0x0400009C RID: 156
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ModifiedBy;

		// Token: 0x0400009D RID: 157
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset _ModifiedDate;

		// Token: 0x0400009E RID: 158
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _CreatedBy;

		// Token: 0x0400009F RID: 159
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset _CreatedDate;

		// Token: 0x040000A0 RID: 160
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid? _ParentFolderId;

		// Token: 0x040000A1 RID: 161
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsFavorite;

		// Token: 0x040000A2 RID: 162
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<Role> _Roles = new ObservableCollection<Role>();

		// Token: 0x040000A3 RID: 163
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ContentType;

		// Token: 0x040000A4 RID: 164
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private byte[] _Content;

		// Token: 0x040000A5 RID: 165
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Folder _ParentFolder;

		// Token: 0x040000A6 RID: 166
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<Property> _Properties = new DataServiceCollection<Property>(null, TrackingMode.None);

		// Token: 0x040000A7 RID: 167
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<Comment> _Comments = new DataServiceCollection<Comment>(null, TrackingMode.None);

		// Token: 0x040000A8 RID: 168
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<AlertSubscription> _AlertSubscriptions = new DataServiceCollection<AlertSubscription>(null, TrackingMode.None);

		// Token: 0x040000A9 RID: 169
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<AllowedAction> _AllowedActions = new DataServiceCollection<AllowedAction>(null, TrackingMode.None);

		// Token: 0x040000AA RID: 170
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<ItemPolicy> _Policies = new DataServiceCollection<ItemPolicy>(null, TrackingMode.None);

		// Token: 0x040000AB RID: 171
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<CatalogItem> _DependentItems = new DataServiceCollection<CatalogItem>(null, TrackingMode.None);
	}
}
