using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000B6 RID: 182
	[Key("Id")]
	[EntitySet("CatalogItems")]
	[OriginalName("CatalogItem")]
	public abstract class CatalogItem : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x0000F73D File Offset: 0x0000D93D
		// (set) Token: 0x0600079E RID: 1950 RVA: 0x0000F745 File Offset: 0x0000D945
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

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x0000F759 File Offset: 0x0000D959
		// (set) Token: 0x060007A0 RID: 1952 RVA: 0x0000F761 File Offset: 0x0000D961
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

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0000F775 File Offset: 0x0000D975
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x0000F77D File Offset: 0x0000D97D
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

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0000F791 File Offset: 0x0000D991
		// (set) Token: 0x060007A4 RID: 1956 RVA: 0x0000F799 File Offset: 0x0000D999
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

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x0000F7AD File Offset: 0x0000D9AD
		// (set) Token: 0x060007A6 RID: 1958 RVA: 0x0000F7B5 File Offset: 0x0000D9B5
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

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x0000F7C9 File Offset: 0x0000D9C9
		// (set) Token: 0x060007A8 RID: 1960 RVA: 0x0000F7D1 File Offset: 0x0000D9D1
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

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x0000F7E5 File Offset: 0x0000D9E5
		// (set) Token: 0x060007AA RID: 1962 RVA: 0x0000F7ED File Offset: 0x0000D9ED
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

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0000F801 File Offset: 0x0000DA01
		// (set) Token: 0x060007AC RID: 1964 RVA: 0x0000F809 File Offset: 0x0000DA09
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

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x0000F81D File Offset: 0x0000DA1D
		// (set) Token: 0x060007AE RID: 1966 RVA: 0x0000F825 File Offset: 0x0000DA25
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

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x0000F839 File Offset: 0x0000DA39
		// (set) Token: 0x060007B0 RID: 1968 RVA: 0x0000F841 File Offset: 0x0000DA41
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

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x0000F855 File Offset: 0x0000DA55
		// (set) Token: 0x060007B2 RID: 1970 RVA: 0x0000F85D File Offset: 0x0000DA5D
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

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0000F871 File Offset: 0x0000DA71
		// (set) Token: 0x060007B4 RID: 1972 RVA: 0x0000F879 File Offset: 0x0000DA79
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

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0000F88D File Offset: 0x0000DA8D
		// (set) Token: 0x060007B6 RID: 1974 RVA: 0x0000F895 File Offset: 0x0000DA95
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

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x0000F8A9 File Offset: 0x0000DAA9
		// (set) Token: 0x060007B8 RID: 1976 RVA: 0x0000F8B1 File Offset: 0x0000DAB1
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

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0000F8C5 File Offset: 0x0000DAC5
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x0000F8CD File Offset: 0x0000DACD
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Properties")]
		public ObservableCollection<Property> Properties
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

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x0000F8E1 File Offset: 0x0000DAE1
		// (set) Token: 0x060007BC RID: 1980 RVA: 0x0000F8E9 File Offset: 0x0000DAE9
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

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0000F8FD File Offset: 0x0000DAFD
		// (set) Token: 0x060007BE RID: 1982 RVA: 0x0000F905 File Offset: 0x0000DB05
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Policies")]
		public ObservableCollection<ItemPolicy> Policies
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

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x0000F919 File Offset: 0x0000DB19
		// (set) Token: 0x060007C0 RID: 1984 RVA: 0x0000F921 File Offset: 0x0000DB21
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

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0000F935 File Offset: 0x0000DB35
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x0000F93D File Offset: 0x0000DB3D
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

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0000F951 File Offset: 0x0000DB51
		// (set) Token: 0x060007C4 RID: 1988 RVA: 0x0000F959 File Offset: 0x0000DB59
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

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x0000F96D File Offset: 0x0000DB6D
		// (set) Token: 0x060007C6 RID: 1990 RVA: 0x0000F975 File Offset: 0x0000DB75
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

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x0000F989 File Offset: 0x0000DB89
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x0000F991 File Offset: 0x0000DB91
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

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x0000F9A5 File Offset: 0x0000DBA5
		// (set) Token: 0x060007CA RID: 1994 RVA: 0x0000F9AD File Offset: 0x0000DBAD
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

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x060007CB RID: 1995 RVA: 0x0000F9C4 File Offset: 0x0000DBC4
		// (remove) Token: 0x060007CC RID: 1996 RVA: 0x0000F9FC File Offset: 0x0000DBFC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060007CD RID: 1997 RVA: 0x0000FA31 File Offset: 0x0000DC31
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0000FA50 File Offset: 0x0000DC50
		[OriginalName("GetDependentItems")]
		public DataServiceQuery<CatalogItem> GetDependentItems()
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuery<CatalogItem>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.GetDependentItems", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0000FAD0 File Offset: 0x0000DCD0
		[OriginalName("SearchItems")]
		public DataServiceQuery<CatalogItem> SearchItems(string SearchText)
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuery<CatalogItem>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.SearchItems", false, new UriOperationParameter[]
			{
				new UriOperationParameter("SearchText", SearchText)
			});
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0000FB5C File Offset: 0x0000DD5C
		[OriginalName("GetRoles")]
		public DataServiceQuery<Role> GetRoles()
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuery<Role>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.GetRoles", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0000FBDC File Offset: 0x0000DDDC
		[OriginalName("GetPolicies")]
		public DataServiceQuery<ItemPolicy> GetPolicies()
		{
			Uri uri;
			base.Context.TryGetUri(this, out uri);
			return base.Context.CreateFunctionQuery<ItemPolicy>(string.Join("/", from s in uri.Segments.Skip(base.Context.BaseUri.Segments.Length)
				select s.Trim(new char[] { '/' })), "Model.GetPolicies", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0000FC5C File Offset: 0x0000DE5C
		[OriginalName("AddToFavorites")]
		public DataServiceActionQuerySingle<bool> AddToFavorites()
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<bool>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.AddToFavorites", Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0000FCC0 File Offset: 0x0000DEC0
		[OriginalName("RemoveFromFavorites")]
		public DataServiceActionQuerySingle<bool> RemoveFromFavorites()
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<bool>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.RemoveFromFavorites", Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0000FD24 File Offset: 0x0000DF24
		[OriginalName("GetProperties")]
		public DataServiceActionQuery<Property> GetProperties(ICollection<Property> RequestedProperties)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuery<Property>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.GetProperties", new BodyOperationParameter[]
			{
				new BodyOperationParameter("RequestedProperties", RequestedProperties)
			});
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0000FD98 File Offset: 0x0000DF98
		[OriginalName("SetProperties")]
		public DataServiceActionQuerySingle<bool> SetProperties(ICollection<Property> Properties)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<bool>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.SetProperties", new BodyOperationParameter[]
			{
				new BodyOperationParameter("Properties", Properties)
			});
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0000FE0C File Offset: 0x0000E00C
		[OriginalName("SetPolicies")]
		public DataServiceActionQuerySingle<bool> SetPolicies(ItemPolicy Policy)
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<bool>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.SetPolicies", new BodyOperationParameter[]
			{
				new BodyOperationParameter("Policy", Policy)
			});
		}

		// Token: 0x040003B0 RID: 944
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x040003B1 RID: 945
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x040003B2 RID: 946
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Description;

		// Token: 0x040003B3 RID: 947
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Path;

		// Token: 0x040003B4 RID: 948
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CatalogItemType _Type;

		// Token: 0x040003B5 RID: 949
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Hidden;

		// Token: 0x040003B6 RID: 950
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private long _Size;

		// Token: 0x040003B7 RID: 951
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ModifiedBy;

		// Token: 0x040003B8 RID: 952
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset _ModifiedDate;

		// Token: 0x040003B9 RID: 953
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _CreatedBy;

		// Token: 0x040003BA RID: 954
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset _CreatedDate;

		// Token: 0x040003BB RID: 955
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid? _ParentFolderId;

		// Token: 0x040003BC RID: 956
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ContentType;

		// Token: 0x040003BD RID: 957
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private byte[] _Content;

		// Token: 0x040003BE RID: 958
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<Property> _Properties = new ObservableCollection<Property>();

		// Token: 0x040003BF RID: 959
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsFavorite;

		// Token: 0x040003C0 RID: 960
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<ItemPolicy> _Policies = new ObservableCollection<ItemPolicy>();

		// Token: 0x040003C1 RID: 961
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<Role> _Roles = new ObservableCollection<Role>();

		// Token: 0x040003C2 RID: 962
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Folder _ParentFolder;

		// Token: 0x040003C3 RID: 963
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<Comment> _Comments = new DataServiceCollection<Comment>(null, TrackingMode.None);

		// Token: 0x040003C4 RID: 964
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<AlertSubscription> _AlertSubscriptions = new DataServiceCollection<AlertSubscription>(null, TrackingMode.None);

		// Token: 0x040003C5 RID: 965
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<AllowedAction> _AllowedActions = new DataServiceCollection<AllowedAction>(null, TrackingMode.None);

		// Token: 0x040003C6 RID: 966
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<CatalogItem> _DependentItems = new DataServiceCollection<CatalogItem>(null, TrackingMode.None);
	}
}
