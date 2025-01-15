using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000105 RID: 261
	public abstract class ResourceKindInfo : IEquatable<ResourceKindInfo>
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x000056E0 File Offset: 0x000038E0
		protected ResourceKindInfo(string kind, string label, bool isUri, bool isDatabase, bool isSingleton, bool supportsEncryptedConnection, bool supportsConnectionString, bool supportsNativeQuery, IEnumerable<AuthenticationInfo> authenticationInfo = null, IEnumerable<CredentialProperty> applicationProperties = null, IEnumerable<QueryPermissionChallengeType> permissionKinds = null, IEnumerable<string> connectionStringProperties = null, IEnumerable<IDataSourceLocationFactory> dslFactories = null)
			: this(kind, label, isUri, isDatabase, isSingleton, supportsEncryptedConnection, supportsConnectionString, new bool?(supportsNativeQuery), authenticationInfo, applicationProperties, permissionKinds, connectionStringProperties, dslFactories)
		{
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00005710 File Offset: 0x00003910
		protected ResourceKindInfo(string kind, string label, bool isUri, bool isDatabase, bool isSingleton, bool supportsEncryptedConnection, bool supportsConnectionString, bool? supportsNativeQuery, IEnumerable<AuthenticationInfo> authenticationInfo = null, IEnumerable<CredentialProperty> applicationProperties = null, IEnumerable<QueryPermissionChallengeType> permissionKinds = null, IEnumerable<string> connectionStringProperties = null, IEnumerable<IDataSourceLocationFactory> dslFactories = null)
		{
			this.kind = kind;
			this.label = label;
			this.isUri = isUri;
			this.isDatabase = isDatabase;
			this.isSingleton = isSingleton;
			this.supportsEncryptedConnection = supportsEncryptedConnection;
			this.supportsConnectionString = supportsConnectionString;
			this.supportsNativeQuery = supportsNativeQuery;
			this.InitializeConnectionStringProperties(connectionStringProperties);
			this.InitializeAuthenticationInfo(authenticationInfo);
			this.InitializeApplicationProperties(applicationProperties);
			this.InitializePermissionKinds(permissionKinds);
			this.InitializeDslFactories(dslFactories);
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x00005788 File Offset: 0x00003988
		public string Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00005790 File Offset: 0x00003990
		public string Label
		{
			get
			{
				return this.label;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x00005798 File Offset: 0x00003998
		public AuthenticationKind DefaultAuthenticationKind
		{
			get
			{
				return this.authenticationInfo[0].AuthenticationKind;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x000057AB File Offset: 0x000039AB
		public bool IsUri
		{
			get
			{
				return this.isUri;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x000057B3 File Offset: 0x000039B3
		public bool IsDatabase
		{
			get
			{
				return this.isDatabase;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00002139 File Offset: 0x00000339
		[Obsolete("Hasn't been relevant since Power BI v1")]
		public bool IsShareable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x000057BB File Offset: 0x000039BB
		public bool IsSingleton
		{
			get
			{
				return this.isSingleton;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x000057C3 File Offset: 0x000039C3
		public bool SupportsEncryptedConnection
		{
			get
			{
				return this.supportsEncryptedConnection;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x000057CB File Offset: 0x000039CB
		public bool SupportsConnectionString
		{
			get
			{
				return this.supportsConnectionString;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x000057D3 File Offset: 0x000039D3
		public bool SupportsNativeQuery
		{
			get
			{
				return this.supportsNativeQuery.GetValueOrDefault();
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x000057E0 File Offset: 0x000039E0
		public bool? SupportsNativeQueryChallenge
		{
			get
			{
				return this.supportsNativeQuery;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x000057E8 File Offset: 0x000039E8
		public IList<string> ConnectionStringProperties
		{
			get
			{
				return this.connectionStringProperties;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x000057F0 File Offset: 0x000039F0
		public IList<AuthenticationInfo> AuthenticationInfo
		{
			get
			{
				return this.authenticationInfo;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x000057F8 File Offset: 0x000039F8
		public IList<CredentialProperty> ApplicationProperties
		{
			get
			{
				return this.applicationProperties;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x00005800 File Offset: 0x00003A00
		public ICollection<QueryPermissionChallengeType> PermissionKinds
		{
			get
			{
				return this.permissionKinds;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00005808 File Offset: 0x00003A08
		public ICollection<IDataSourceLocationFactory> DslFactories
		{
			get
			{
				return this.dslFactories;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x000020FA File Offset: 0x000002FA
		public virtual IRecordValue ResourceRecord
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00005810 File Offset: 0x00003A10
		public virtual bool CanRefresh(IResourceCredential credential)
		{
			return credential is OAuthCredential;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000581C File Offset: 0x00003A1C
		public bool TryGetAuthenticationInfo(AuthenticationKind kind, out AuthenticationInfo info)
		{
			info = this.AuthenticationInfo.Where((AuthenticationInfo i) => i.AuthenticationKind == kind).FirstOrDefault<AuthenticationInfo>();
			return info != null;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000585C File Offset: 0x00003A5C
		public bool Equals(ResourceKindInfo other)
		{
			if (other != null && other.Kind == this.Kind && other.IsDatabase == this.IsDatabase && other.IsUri == this.IsUri && other.IsSingleton == this.IsSingleton && other.SupportsEncryptedConnection == this.SupportsEncryptedConnection && other.SupportsConnectionString == this.SupportsConnectionString)
			{
				bool? supportsNativeQueryChallenge = other.SupportsNativeQueryChallenge;
				bool? flag = this.supportsNativeQuery;
				return (supportsNativeQueryChallenge.GetValueOrDefault() == flag.GetValueOrDefault()) & (supportsNativeQueryChallenge != null == (flag != null));
			}
			return false;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000058F9 File Offset: 0x00003AF9
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ResourceKindInfo);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00005908 File Offset: 0x00003B08
		public override int GetHashCode()
		{
			return this.kind.GetHashCode() ^ (((this.isUri > false) ? 1 : 0) | (this.isDatabase ? 2 : 0) | (this.isSingleton ? 8 : 0) | (this.supportsEncryptedConnection ? 16 : 0) | (this.supportsConnectionString ? 32 : 0) | (this.SupportsNativeQuery ? 64 : 0));
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000596E File Offset: 0x00003B6E
		public virtual bool IsSubset(string permittedResourcePath, string attemptedResourcePath)
		{
			return permittedResourcePath == attemptedResourcePath;
		}

		// Token: 0x06000431 RID: 1073
		public abstract bool Validate(string resourcePath, out IResource resource, out string errorMessage);

		// Token: 0x06000432 RID: 1074 RVA: 0x00005977 File Offset: 0x00003B77
		public virtual IEnumerable<string> EnumerateKnownSupersets(string resourcePath)
		{
			return new string[] { resourcePath };
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000020FA File Offset: 0x000002FA
		public virtual string CreateTestFormula(string resourcePath)
		{
			return null;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000020FA File Offset: 0x000002FA
		public virtual IEnumerable<KeyValuePair<string, string>> GetPartLabels(string resourcePath)
		{
			return null;
		}

		// Token: 0x06000435 RID: 1077
		public abstract bool TryGetHostName(string resourcePath, out string hostName);

		// Token: 0x06000436 RID: 1078 RVA: 0x00005983 File Offset: 0x00003B83
		protected void InitializeAuthenticationInfo(IEnumerable<AuthenticationInfo> authenticationInfo)
		{
			this.authenticationInfo = new List<AuthenticationInfo>(authenticationInfo ?? ResourceKindInfo.emptyAuthenticationInfo).AsReadOnly();
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000599F File Offset: 0x00003B9F
		protected void InitializeApplicationProperties(IEnumerable<CredentialProperty> applicationProperties)
		{
			this.applicationProperties = new List<CredentialProperty>(applicationProperties ?? new CredentialProperty[0]).AsReadOnly();
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x000059BC File Offset: 0x00003BBC
		protected void InitializeConnectionStringProperties(IEnumerable<string> connectionStringProperties)
		{
			this.connectionStringProperties = new List<string>(connectionStringProperties ?? new string[0]).AsReadOnly();
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000059DC File Offset: 0x00003BDC
		protected void InitializePermissionKinds(IEnumerable<QueryPermissionChallengeType> permissionKinds)
		{
			List<QueryPermissionChallengeType> list = new List<QueryPermissionChallengeType>(permissionKinds ?? ResourceKindInfo.emptyPermissionKinds);
			if (this.SupportsNativeQueryChallenge.GetValueOrDefault(true) && !list.Contains(QueryPermissionChallengeType.EvaluateNativeQueryUnpermitted))
			{
				list.Add(QueryPermissionChallengeType.EvaluateNativeQueryUnpermitted);
			}
			this.permissionKinds = list.AsReadOnly();
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00005A26 File Offset: 0x00003C26
		protected void InitializeDslFactories(IEnumerable<IDataSourceLocationFactory> factories)
		{
			this.dslFactories = new ReadOnlyCollection<IDataSourceLocationFactory>(new List<IDataSourceLocationFactory>(factories ?? ResourceKindInfo.emptyFactories));
		}

		// Token: 0x0400026E RID: 622
		private static readonly AuthenticationInfo[] emptyAuthenticationInfo = new AuthenticationInfo[0];

		// Token: 0x0400026F RID: 623
		private static readonly QueryPermissionChallengeType[] emptyPermissionKinds = new QueryPermissionChallengeType[0];

		// Token: 0x04000270 RID: 624
		private static readonly IDataSourceLocationFactory[] emptyFactories = new IDataSourceLocationFactory[0];

		// Token: 0x04000271 RID: 625
		private readonly string kind;

		// Token: 0x04000272 RID: 626
		private readonly string label;

		// Token: 0x04000273 RID: 627
		private readonly bool isUri;

		// Token: 0x04000274 RID: 628
		private readonly bool isDatabase;

		// Token: 0x04000275 RID: 629
		private readonly bool isSingleton;

		// Token: 0x04000276 RID: 630
		private readonly bool supportsEncryptedConnection;

		// Token: 0x04000277 RID: 631
		private readonly bool supportsConnectionString;

		// Token: 0x04000278 RID: 632
		private readonly bool? supportsNativeQuery;

		// Token: 0x04000279 RID: 633
		private ReadOnlyCollection<string> connectionStringProperties;

		// Token: 0x0400027A RID: 634
		private ReadOnlyCollection<AuthenticationInfo> authenticationInfo;

		// Token: 0x0400027B RID: 635
		private ReadOnlyCollection<CredentialProperty> applicationProperties;

		// Token: 0x0400027C RID: 636
		private ReadOnlyCollection<QueryPermissionChallengeType> permissionKinds;

		// Token: 0x0400027D RID: 637
		private ReadOnlyCollection<IDataSourceLocationFactory> dslFactories;
	}
}
