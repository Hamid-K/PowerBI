using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000050 RID: 80
	[DataContract]
	public class DatabaseMigrationMoniker : IEquatable<DatabaseMigrationMoniker>
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x0000ED9F File Offset: 0x0000CF9F
		public DatabaseMigrationMoniker()
		{
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000FB47 File Offset: 0x0000DD47
		public DatabaseMigrationMoniker(string virtualServerName, string databaseName, DatabaseStorageMode storageMode)
			: this(new DatabaseMoniker(virtualServerName, databaseName), storageMode)
		{
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000FB57 File Offset: 0x0000DD57
		public DatabaseMigrationMoniker(DatabaseMoniker moniker, DatabaseStorageMode storageMode)
		{
			this.Moniker = moniker;
			this.StorageMode = storageMode;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000FB6D File Offset: 0x0000DD6D
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x0000FB75 File Offset: 0x0000DD75
		[DataMember]
		public DatabaseMoniker Moniker { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0000FB7E File Offset: 0x0000DD7E
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x0000FB86 File Offset: 0x0000DD86
		[DataMember]
		public DatabaseStorageMode StorageMode { get; set; }

		// Token: 0x0600042E RID: 1070 RVA: 0x0000FB8F File Offset: 0x0000DD8F
		public bool Equals(DatabaseMigrationMoniker other)
		{
			return other != null && this.Moniker.Equals(other.Moniker) && this.StorageMode == other.StorageMode;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000FBB7 File Offset: 0x0000DDB7
		public override bool Equals(object other)
		{
			return this.Equals(other as DatabaseMigrationMoniker);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000FBC8 File Offset: 0x0000DDC8
		public override int GetHashCode()
		{
			return this.Moniker.GetHashCode() ^ this.StorageMode.GetHashCode();
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000FBF8 File Offset: 0x0000DDF8
		public override string ToString()
		{
			return string.Format("{{{0}, {1}}}", this.Moniker, this.StorageMode.ToString());
		}
	}
}
