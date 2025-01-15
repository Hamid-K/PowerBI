using System;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.AnalysisServices.Azure.Common.Utils;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200004B RID: 75
	[DataContract]
	[CLSCompliant(true)]
	public sealed class DatabaseEntity : IPersistable
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x0000F322 File Offset: 0x0000D522
		public DatabaseEntity()
		{
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000F334 File Offset: 0x0000D534
		public DatabaseEntity(DatabaseMoniker databaseMoniker, DatabaseType databaseType, DatabaseSource databaseSource = DatabaseSource.PowerBI, string containerId = null, string storageAccountName = null, bool routingDisabled = false, string restaExternalStoreConnectionInfo = null, PushDataVersion pushDataVersion = PushDataVersion.None, string pbiDedicatedCapacity = null, string v2StorageAccountName = null, ModelPowerBIDatasourceFormatVersion powerBIDatasourceFormatVersion = ModelPowerBIDatasourceFormatVersion.PowerBI_V1, Guid? tenantKeyObjectId = null, DatabaseStorageMode storageMode = DatabaseStorageMode.Abf, bool unavailable = false)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<DatabaseMoniker>(databaseMoniker, "databaseMoniker");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(databaseMoniker.VirtualServerName, "VirtualServerName");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(databaseMoniker.DatabaseName, "DatabaseName");
			if (pushDataVersion < PushDataVersion.V2)
			{
				ExtendedDiagnostics.EnsureNull<string>(restaExternalStoreConnectionInfo, "restaExternalStoreConnectionInfo");
			}
			this.StorageAccountName = storageAccountName;
			this.DatabaseMoniker = databaseMoniker;
			this.DatabaseSource = databaseSource;
			this.Type = databaseType;
			this.CreatedDate = DateTime.UtcNow;
			this.ContainerId = (string.IsNullOrWhiteSpace(containerId) ? Guid.NewGuid().ToString() : containerId);
			this.IsCold = false;
			this.RoutingDisabled = routingDisabled;
			this.RestaExternalStoreConnectionInfo = restaExternalStoreConnectionInfo;
			this.PushDataVersion = pushDataVersion;
			this.PBIDedicatedCapacity = pbiDedicatedCapacity;
			this.V2StorageAccountName = v2StorageAccountName;
			this.PowerBIDatasourceFormatVersion = powerBIDatasourceFormatVersion;
			this.TenantKeyObjectId = tenantKeyObjectId;
			this.StorageMode = storageMode;
			this.Unavailable = unavailable;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000F428 File Offset: 0x0000D628
		public DatabaseEntity(string vsName, string databaseName, DatabaseType databaseType, DatabaseSource databaseSource)
			: this(new DatabaseMoniker(vsName, databaseName), databaseType, databaseSource, string.Empty, null, false, null, PushDataVersion.None, null, null, ModelPowerBIDatasourceFormatVersion.PowerBI_V1, null, DatabaseStorageMode.Abf, false)
		{
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000F45C File Offset: 0x0000D65C
		public DatabaseEntity(string vsName, string databaseName, DatabaseType databaseType)
			: this(new DatabaseMoniker(vsName, databaseName), databaseType, DatabaseSource.PowerBI, string.Empty, null, false, null, PushDataVersion.None, null, null, ModelPowerBIDatasourceFormatVersion.PowerBI_V1, null, DatabaseStorageMode.Abf, false)
		{
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000F490 File Offset: 0x0000D690
		public DatabaseEntity(DatabaseEntity source)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<DatabaseEntity>(source, "source");
			this.DatabaseMoniker = source.DatabaseMoniker;
			this.DatabaseSource = source.DatabaseSource;
			this.Type = source.Type;
			this.CreatedDate = source.CreatedDate;
			this.ContainerId = source.ContainerId;
			this.IsCold = source.IsCold;
			this.MappedServiceEntityKey = source.MappedServiceEntityKey;
			this.MappedVirtualServerEntityKey = source.MappedVirtualServerEntityKey;
			this.InitialLoadInMB = source.InitialLoadInMB;
			this.StorageAccountName = source.StorageAccountName;
			this.RoutingDisabled = source.RoutingDisabled;
			this.RestaExternalStoreConnectionInfo = source.RestaExternalStoreConnectionInfo;
			this.PushDataVersion = source.PushDataVersion;
			this.PBIDedicatedCapacity = source.PBIDedicatedCapacity;
			this.V2StorageAccountName = source.V2StorageAccountName;
			this.PowerBIDatasourceFormatVersion = source.PowerBIDatasourceFormatVersion;
			this.TenantKeyObjectId = source.TenantKeyObjectId;
			this.StorageMode = source.StorageMode;
			this.Unavailable = source.Unavailable;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000F599 File Offset: 0x0000D799
		public string Id
		{
			get
			{
				return this.DatabaseMoniker.FullName;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000F5A6 File Offset: 0x0000D7A6
		public string FriendlyName
		{
			get
			{
				return this.DatabaseMoniker.DatabaseName;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000E3C2 File Offset: 0x0000C5C2
		public bool IsGeoReplicated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000F5B3 File Offset: 0x0000D7B3
		// (set) Token: 0x060003DA RID: 986 RVA: 0x0000F5BB File Offset: 0x0000D7BB
		[DataMember]
		public DatabaseMoniker DatabaseMoniker { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000F5C4 File Offset: 0x0000D7C4
		// (set) Token: 0x060003DC RID: 988 RVA: 0x0000F5CC File Offset: 0x0000D7CC
		[DataMember]
		public DatabaseSource DatabaseSource { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000F5D5 File Offset: 0x0000D7D5
		// (set) Token: 0x060003DE RID: 990 RVA: 0x0000F5DD File Offset: 0x0000D7DD
		[DataMember]
		public DatabaseType Type { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000F5E6 File Offset: 0x0000D7E6
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x0000F5EE File Offset: 0x0000D7EE
		[DataMember]
		public DateTime CreatedDate { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000F5F7 File Offset: 0x0000D7F7
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0000F601 File Offset: 0x0000D801
		[DataMember]
		public string MappedServiceEntityKey
		{
			get
			{
				return this.mappedServiceEntityKey;
			}
			set
			{
				this.mappedServiceEntityKey = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000F60C File Offset: 0x0000D80C
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x0000F614 File Offset: 0x0000D814
		[DataMember]
		public string MappedVirtualServerEntityKey { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000F61D File Offset: 0x0000D81D
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0000F625 File Offset: 0x0000D825
		[DataMember]
		public string StorageAccountName { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000F62E File Offset: 0x0000D82E
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0000F636 File Offset: 0x0000D836
		[DataMember]
		public string ContainerId { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000F63F File Offset: 0x0000D83F
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x0000F649 File Offset: 0x0000D849
		[DataMember]
		public bool IsCold
		{
			get
			{
				return this.isCold;
			}
			set
			{
				this.isCold = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000F654 File Offset: 0x0000D854
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0000F65C File Offset: 0x0000D85C
		[DataMember]
		public string ClusterName { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000F665 File Offset: 0x0000D865
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x0000F66D File Offset: 0x0000D86D
		[DataMember]
		public int InitialLoadInMB { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000F676 File Offset: 0x0000D876
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0000F67E File Offset: 0x0000D87E
		[DataMember]
		public bool RoutingDisabled { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000F687 File Offset: 0x0000D887
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0000F68F File Offset: 0x0000D88F
		[DataMember]
		public PushDataVersion PushDataVersion { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000F698 File Offset: 0x0000D898
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0000F6A0 File Offset: 0x0000D8A0
		[DataMember]
		public string RestaExternalStoreConnectionInfo { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000F6A9 File Offset: 0x0000D8A9
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x0000F6B1 File Offset: 0x0000D8B1
		[DataMember]
		public string PBIDedicatedCapacity { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000F6BA File Offset: 0x0000D8BA
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x0000F6C2 File Offset: 0x0000D8C2
		[DataMember]
		public string V2StorageAccountName { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0000F6CB File Offset: 0x0000D8CB
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x0000F6D3 File Offset: 0x0000D8D3
		[DataMember]
		public Guid? TenantKeyObjectId { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000F6DC File Offset: 0x0000D8DC
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x0000F6EE File Offset: 0x0000D8EE
		[DataMember]
		public DatabaseStorageMode StorageMode
		{
			get
			{
				if (this.storageMode != DatabaseStorageMode.Unknown)
				{
					return this.storageMode;
				}
				return DatabaseStorageMode.Abf;
			}
			set
			{
				this.storageMode = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000F6F7 File Offset: 0x0000D8F7
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x0000F6FF File Offset: 0x0000D8FF
		[DataMember]
		public bool Unavailable { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000F708 File Offset: 0x0000D908
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x0000F710 File Offset: 0x0000D910
		[DataMember]
		public ModelPowerBIDatasourceFormatVersion PowerBIDatasourceFormatVersion { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000F719 File Offset: 0x0000D919
		public string Key
		{
			get
			{
				return CommonUtils.GenerateEntityKey(PersistableItemTypes.DatabaseEntity, this.DatabaseMoniker.FullName);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000A3EB File Offset: 0x000085EB
		public bool IsBackupEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000F72C File Offset: 0x0000D92C
		public byte[] Serialize()
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new DataContractSerializer(typeof(DatabaseEntity)).WriteObject(memoryStream, this);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000F77C File Offset: 0x0000D97C
		public T Deserialize<T>(byte[] data)
		{
			T t;
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				t = (T)((object)new DataContractSerializer(typeof(T)).ReadObject(memoryStream));
			}
			return t;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000F7C8 File Offset: 0x0000D9C8
		public IPersistable Clone()
		{
			return new DatabaseEntity(this);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000A3EB File Offset: 0x000085EB
		public PersistableItemTypes EnumType()
		{
			return PersistableItemTypes.DatabaseEntity;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000F7D0 File Offset: 0x0000D9D0
		public override string ToString()
		{
			return "DatabaseEntity:[Id={0};IsCold={1};MappedService={2};AzureStorage={3};ContainerId={4};DatabaseSource={5};RoutingDisabled={6};PushDataVersion={7};RestaExternalStoreConnectionInfo={8};PBIDedicatedCapacity={9};v2StorageAccountName={10};TenantKeyObjectId={11};StorageMode={12};Unavailable={13}]".FormatWithInvariantCulture(new object[]
			{
				this.Key,
				this.IsCold,
				this.MappedServiceEntityKey,
				string.IsNullOrEmpty(this.StorageAccountName) ? "default" : this.StorageAccountName,
				this.ContainerId,
				this.DatabaseSource,
				this.RoutingDisabled,
				this.PushDataVersion,
				this.RestaExternalStoreConnectionInfo,
				this.PBIDedicatedCapacity,
				this.V2StorageAccountName,
				(this.TenantKeyObjectId == null) ? "none" : this.TenantKeyObjectId.ToString(),
				this.StorageMode.ToString(),
				this.Unavailable
			});
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000F8D1 File Offset: 0x0000DAD1
		public override int GetHashCode()
		{
			return this.DatabaseMoniker.GetHashCode();
		}

		// Token: 0x04000115 RID: 277
		private volatile string mappedServiceEntityKey;

		// Token: 0x04000116 RID: 278
		private volatile bool isCold;

		// Token: 0x04000117 RID: 279
		private DatabaseStorageMode storageMode = DatabaseStorageMode.Abf;
	}
}
