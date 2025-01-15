using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000028 RID: 40
	[Key("Id")]
	[EntitySet("DataSources")]
	[OriginalName("DataSource")]
	public class DataSource : CatalogItem
	{
		// Token: 0x0600019F RID: 415 RVA: 0x000049B0 File Offset: 0x00002BB0
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

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00004A1E File Offset: 0x00002C1E
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00004A26 File Offset: 0x00002C26
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

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00004A3A File Offset: 0x00002C3A
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00004A42 File Offset: 0x00002C42
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

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00004A56 File Offset: 0x00002C56
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00004A5E File Offset: 0x00002C5E
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

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00004A72 File Offset: 0x00002C72
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00004A7A File Offset: 0x00002C7A
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

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00004A8E File Offset: 0x00002C8E
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00004A96 File Offset: 0x00002C96
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

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00004AAA File Offset: 0x00002CAA
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00004AB2 File Offset: 0x00002CB2
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

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00004AC6 File Offset: 0x00002CC6
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00004ACE File Offset: 0x00002CCE
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

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00004AE2 File Offset: 0x00002CE2
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00004AEA File Offset: 0x00002CEA
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

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00004AFE File Offset: 0x00002CFE
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00004B06 File Offset: 0x00002D06
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

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00004B1A File Offset: 0x00002D1A
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00004B22 File Offset: 0x00002D22
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DataSourceSubType")]
		public string DataSourceSubType
		{
			get
			{
				return this._DataSourceSubType;
			}
			set
			{
				this._DataSourceSubType = value;
				this.OnPropertyChanged("DataSourceSubType");
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00004B36 File Offset: 0x00002D36
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00004B3E File Offset: 0x00002D3E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DataModelDataSource")]
		public DataModelDataSource DataModelDataSource
		{
			get
			{
				return this._DataModelDataSource;
			}
			set
			{
				this._DataModelDataSource = value;
				this.OnPropertyChanged("DataModelDataSource");
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00004B52 File Offset: 0x00002D52
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00004B5A File Offset: 0x00002D5A
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

		// Token: 0x060001B8 RID: 440 RVA: 0x00004B70 File Offset: 0x00002D70
		[OriginalName("Upload")]
		public DataServiceActionQuerySingle<DataSource> Upload()
		{
			EntityDescriptor entityDescriptor = base.Context.EntityTracker.TryGetEntityDescriptor(this);
			if (entityDescriptor == null)
			{
				throw new Exception("cannot find entity");
			}
			return new DataServiceActionQuerySingle<DataSource>(base.Context, entityDescriptor.EditLink.OriginalString.Trim(new char[] { '/' }) + "/Model.Upload", Array.Empty<BodyOperationParameter>());
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00004BD4 File Offset: 0x00002DD4
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

		// Token: 0x040000E0 RID: 224
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsEnabled;

		// Token: 0x040000E1 RID: 225
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ConnectionString;

		// Token: 0x040000E2 RID: 226
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _DataSourceType;

		// Token: 0x040000E3 RID: 227
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsOriginalConnectionStringExpressionBased;

		// Token: 0x040000E4 RID: 228
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsConnectionStringOverridden;

		// Token: 0x040000E5 RID: 229
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CredentialRetrievalType _CredentialRetrieval;

		// Token: 0x040000E6 RID: 230
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CredentialsSuppliedByUser _CredentialsByUser;

		// Token: 0x040000E7 RID: 231
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CredentialsStoredInServer _CredentialsInServer;

		// Token: 0x040000E8 RID: 232
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsReference;

		// Token: 0x040000E9 RID: 233
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _DataSourceSubType;

		// Token: 0x040000EA RID: 234
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataModelDataSource _DataModelDataSource;

		// Token: 0x040000EB RID: 235
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceCollection<Subscription> _Subscriptions = new DataServiceCollection<Subscription>(null, TrackingMode.None);
	}
}
