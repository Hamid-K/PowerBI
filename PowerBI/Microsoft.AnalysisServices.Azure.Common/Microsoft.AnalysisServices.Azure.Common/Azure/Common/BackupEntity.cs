using System;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.AnalysisServices.Azure.Common.Utils;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000044 RID: 68
	[DataContract]
	[KnownType(typeof(DatabaseEntity))]
	[KnownType(typeof(VirtualServerEntity))]
	public sealed class BackupEntity : IPersistable
	{
		// Token: 0x06000381 RID: 897 RVA: 0x0000ED9F File Offset: 0x0000CF9F
		public BackupEntity()
		{
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000EDA8 File Offset: 0x0000CFA8
		public BackupEntity(string entityKey, IPersistable persistableItem, EntityOperationType opType, Guid backupEntityVersion)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(entityKey, "entityKey");
			ExtendedDiagnostics.EnsureArgumentNotNull<IPersistable>(persistableItem, "persistableItem");
			ExtendedDiagnostics.EnsureArgumentNotNull<EntityOperationType>(opType, "opType");
			ExtendedDiagnostics.EnsureArgumentNotNull<Guid>(backupEntityVersion, "backupEntityVersion");
			this.EntityKey = entityKey;
			this.OpType = opType;
			this.PersistableItem = persistableItem;
			this.BackupEntityVersion = backupEntityVersion;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000EE05 File Offset: 0x0000D005
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0000EE0D File Offset: 0x0000D00D
		[DataMember]
		public EntityOperationType OpType { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000EE16 File Offset: 0x0000D016
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0000EE1E File Offset: 0x0000D01E
		[DataMember]
		public Guid BackupEntityVersion { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000EE27 File Offset: 0x0000D027
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0000EE2F File Offset: 0x0000D02F
		[DataMember]
		public string EntityKey { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000EE38 File Offset: 0x0000D038
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0000EE40 File Offset: 0x0000D040
		[DataMember]
		public IPersistable PersistableItem { get; private set; }

		// Token: 0x0600038B RID: 907 RVA: 0x0000EE49 File Offset: 0x0000D049
		public static string GetBackupItemKey(string entityKey)
		{
			return CommonUtils.GenerateEntityKey(PersistableItemTypes.BackupEntity, entityKey);
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000EE52 File Offset: 0x0000D052
		public string Key
		{
			get
			{
				return CommonUtils.GenerateEntityKey(PersistableItemTypes.BackupEntity, this.EntityKey);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000E3C2 File Offset: 0x0000C5C2
		public bool IsBackupEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000EE60 File Offset: 0x0000D060
		public byte[] Serialize()
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new DataContractSerializer(typeof(BackupEntity)).WriteObject(memoryStream, this);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000EEB0 File Offset: 0x0000D0B0
		public T Deserialize<T>(byte[] data)
		{
			T t;
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				t = (T)((object)new DataContractSerializer(typeof(T)).ReadObject(memoryStream));
			}
			return t;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000EEFC File Offset: 0x0000D0FC
		public IPersistable Clone()
		{
			return new BackupEntity(this.EntityKey, this.PersistableItem, this.OpType, Guid.NewGuid());
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000EF1A File Offset: 0x0000D11A
		public PersistableItemTypes EnumType()
		{
			return PersistableItemTypes.BackupEntity;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000EF1D File Offset: 0x0000D11D
		public override string ToString()
		{
			return "BackupEntity:[Id={0};OpType={1};BackupEntityVersion={2};PersistableItem={3}]".FormatWithInvariantCulture(new object[] { this.Key, this.OpType, this.BackupEntityVersion, this.PersistableItem });
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000EF60 File Offset: 0x0000D160
		public override bool Equals(object obj)
		{
			BackupEntity backupEntity = obj as BackupEntity;
			return this.Key == backupEntity.Key && this.BackupEntityVersion == backupEntity.BackupEntityVersion;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000EF9A File Offset: 0x0000D19A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
