using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000C9 RID: 201
	[Key("Id")]
	[OriginalName("DataSource")]
	public class DataSource : CatalogItem
	{
		// Token: 0x060008E6 RID: 2278 RVA: 0x00012628 File Offset: 0x00010828
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static DataSource CreateDataSource(Guid ID, CatalogItemType type, bool hidden, long size, DateTimeOffset modifiedDate, DateTimeOffset createdDate, bool isFavorite, bool isEnabled, bool isOriginalConnectionStringExpressionBased, bool isConnectionStringOverridden, CredentialRetrievalType credentialRetrieval, bool isReference)
		{
			return new DataSource
			{
				Id = ID,
				Type = type,
				Hidden = hidden,
				Size = size,
				ModifiedDate = modifiedDate,
				CreatedDate = createdDate,
				IsFavorite = isFavorite,
				IsEnabled = isEnabled,
				IsOriginalConnectionStringExpressionBased = isOriginalConnectionStringExpressionBased,
				IsConnectionStringOverridden = isConnectionStringOverridden,
				CredentialRetrieval = credentialRetrieval,
				IsReference = isReference
			};
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x00012696 File Offset: 0x00010896
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x0001269E File Offset: 0x0001089E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("IsEnabled")]
		public bool IsEnabled
		{
			get
			{
				return this._IsEnabled;
			}
			set
			{
				this._IsEnabled = value;
				this.OnPropertyChanged("IsEnabled");
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x000126B2 File Offset: 0x000108B2
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x000126BA File Offset: 0x000108BA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ConnectionString")]
		public string ConnectionString
		{
			get
			{
				return this._ConnectionString;
			}
			set
			{
				this._ConnectionString = value;
				this.OnPropertyChanged("ConnectionString");
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x000126CE File Offset: 0x000108CE
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x000126D6 File Offset: 0x000108D6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DataSourceType")]
		public string DataSourceType
		{
			get
			{
				return this._DataSourceType;
			}
			set
			{
				this._DataSourceType = value;
				this.OnPropertyChanged("DataSourceType");
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x000126EA File Offset: 0x000108EA
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x000126F2 File Offset: 0x000108F2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("IsOriginalConnectionStringExpressionBased")]
		public bool IsOriginalConnectionStringExpressionBased
		{
			get
			{
				return this._IsOriginalConnectionStringExpressionBased;
			}
			set
			{
				this._IsOriginalConnectionStringExpressionBased = value;
				this.OnPropertyChanged("IsOriginalConnectionStringExpressionBased");
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x00012706 File Offset: 0x00010906
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x0001270E File Offset: 0x0001090E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("IsConnectionStringOverridden")]
		public bool IsConnectionStringOverridden
		{
			get
			{
				return this._IsConnectionStringOverridden;
			}
			set
			{
				this._IsConnectionStringOverridden = value;
				this.OnPropertyChanged("IsConnectionStringOverridden");
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x00012722 File Offset: 0x00010922
		// (set) Token: 0x060008F2 RID: 2290 RVA: 0x0001272A File Offset: 0x0001092A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CredentialRetrieval")]
		public CredentialRetrievalType CredentialRetrieval
		{
			get
			{
				return this._CredentialRetrieval;
			}
			set
			{
				this._CredentialRetrieval = value;
				this.OnPropertyChanged("CredentialRetrieval");
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x0001273E File Offset: 0x0001093E
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x00012746 File Offset: 0x00010946
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CredentialsByUser")]
		public CredentialsSuppliedByUser CredentialsByUser
		{
			get
			{
				return this._CredentialsByUser;
			}
			set
			{
				this._CredentialsByUser = value;
				this.OnPropertyChanged("CredentialsByUser");
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x0001275A File Offset: 0x0001095A
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x00012762 File Offset: 0x00010962
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CredentialsInServer")]
		public CredentialsStoredInServer CredentialsInServer
		{
			get
			{
				return this._CredentialsInServer;
			}
			set
			{
				this._CredentialsInServer = value;
				this.OnPropertyChanged("CredentialsInServer");
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00012776 File Offset: 0x00010976
		// (set) Token: 0x060008F8 RID: 2296 RVA: 0x0001277E File Offset: 0x0001097E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("IsReference")]
		public bool IsReference
		{
			get
			{
				return this._IsReference;
			}
			set
			{
				this._IsReference = value;
				this.OnPropertyChanged("IsReference");
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00012792 File Offset: 0x00010992
		// (set) Token: 0x060008FA RID: 2298 RVA: 0x0001279A File Offset: 0x0001099A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DSIDNum")]
		public long? DSIDNum
		{
			get
			{
				return this._DSIDNum;
			}
			set
			{
				this._DSIDNum = value;
				this.OnPropertyChanged("DSIDNum");
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x000127AE File Offset: 0x000109AE
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x000127B6 File Offset: 0x000109B6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Subscriptions")]
		public DataServiceCollection<Subscription> Subscriptions
		{
			get
			{
				return this._Subscriptions;
			}
			set
			{
				this._Subscriptions = value;
				this.OnPropertyChanged("Subscriptions");
			}
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x000127CC File Offset: 0x000109CC
		[OriginalName("CheckConnection")]
		public DataServiceActionQuerySingle<DataSourceCheckResult> CheckConnection()
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<DataSourceCheckResult>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.CheckConnection", Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x0400043C RID: 1084
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsEnabled;

		// Token: 0x0400043D RID: 1085
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ConnectionString;

		// Token: 0x0400043E RID: 1086
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _DataSourceType;

		// Token: 0x0400043F RID: 1087
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsOriginalConnectionStringExpressionBased;

		// Token: 0x04000440 RID: 1088
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsConnectionStringOverridden;

		// Token: 0x04000441 RID: 1089
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CredentialRetrievalType _CredentialRetrieval;

		// Token: 0x04000442 RID: 1090
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CredentialsSuppliedByUser _CredentialsByUser;

		// Token: 0x04000443 RID: 1091
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CredentialsStoredInServer _CredentialsInServer;

		// Token: 0x04000444 RID: 1092
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsReference;

		// Token: 0x04000445 RID: 1093
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private long? _DSIDNum;

		// Token: 0x04000446 RID: 1094
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<Subscription> _Subscriptions = new DataServiceCollection<Subscription>(null, TrackingMode.None);
	}
}
