using System;
using System.IO;
using System.Runtime.Serialization;
using System.Threading;
using Microsoft.AnalysisServices.Azure.Common.Utils;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200004C RID: 76
	[DataContract]
	public sealed class DatabaseLastAccessTimeEntity : IPersistable, IEquatable<DatabaseLastAccessTimeEntity>
	{
		// Token: 0x06000409 RID: 1033 RVA: 0x0000ED9F File Offset: 0x0000CF9F
		public DatabaseLastAccessTimeEntity()
		{
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000F8DE File Offset: 0x0000DADE
		public DatabaseLastAccessTimeEntity(string databaseKey)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(databaseKey, "databaseKey");
			this.DatabaseKey = databaseKey;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000F8F8 File Offset: 0x0000DAF8
		public DatabaseLastAccessTimeEntity(string databaseKey, DateTime lastAccessedDate)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(databaseKey, "databaseKey");
			ExtendedDiagnostics.EnsureArgumentNotNull<DateTime>(lastAccessedDate, "lastAccessedDate");
			this.DatabaseKey = databaseKey;
			this.LastAccessedDate = lastAccessedDate;
			this.hashCode = this.DatabaseKey.GetHashCode();
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000E3C2 File Offset: 0x0000C5C2
		public bool IsBackupEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000F935 File Offset: 0x0000DB35
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x0000F93D File Offset: 0x0000DB3D
		[DataMember]
		public string DatabaseKey { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000F946 File Offset: 0x0000DB46
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x0000F958 File Offset: 0x0000DB58
		[DataMember]
		public DateTime LastAccessedDate
		{
			get
			{
				return new DateTime(Interlocked.Read(ref this.lastAccessedTime));
			}
			set
			{
				Interlocked.Exchange(ref this.lastAccessedTime, value.Ticks);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000F96D File Offset: 0x0000DB6D
		public string Key
		{
			get
			{
				return CommonUtils.GenerateEntityKey(PersistableItemTypes.DatabaseLastAccessTimeEntity, this.DatabaseKey);
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000F97C File Offset: 0x0000DB7C
		public byte[] Serialize()
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new DataContractSerializer(typeof(DatabaseLastAccessTimeEntity)).WriteObject(memoryStream, this);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000F9CC File Offset: 0x0000DBCC
		public T Deserialize<T>(byte[] data)
		{
			T t;
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				t = (T)((object)new DataContractSerializer(typeof(T)).ReadObject(memoryStream));
			}
			return t;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000FA18 File Offset: 0x0000DC18
		public IPersistable Clone()
		{
			return new DatabaseLastAccessTimeEntity(this.DatabaseKey, this.LastAccessedDate)
			{
				lastAccessedTime = this.lastAccessedTime,
				hashCode = this.DatabaseKey.GetHashCode(),
				DatabaseKey = this.DatabaseKey,
				LastAccessedDate = this.LastAccessedDate
			};
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000FA6B File Offset: 0x0000DC6B
		public PersistableItemTypes EnumType()
		{
			return PersistableItemTypes.DatabaseLastAccessTimeEntity;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000FA6E File Offset: 0x0000DC6E
		public bool Equals(DatabaseLastAccessTimeEntity other)
		{
			return other != null && this.DatabaseKey == other.DatabaseKey;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000FA8C File Offset: 0x0000DC8C
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			DatabaseLastAccessTimeEntity databaseLastAccessTimeEntity = obj as DatabaseLastAccessTimeEntity;
			return databaseLastAccessTimeEntity != null && this.Equals(databaseLastAccessTimeEntity);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000FAB1 File Offset: 0x0000DCB1
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000FAB9 File Offset: 0x0000DCB9
		public override string ToString()
		{
			return this.Key;
		}

		// Token: 0x04000129 RID: 297
		private long lastAccessedTime;

		// Token: 0x0400012A RID: 298
		private int hashCode;
	}
}
