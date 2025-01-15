using System;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.AnalysisServices.Azure.Common.Utils;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200004A RID: 74
	[DataContract]
	public class DatabaseBindingEntity : IPersistable
	{
		// Token: 0x060003C6 RID: 966 RVA: 0x0000ED9F File Offset: 0x0000CF9F
		public DatabaseBindingEntity()
		{
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000F227 File Offset: 0x0000D427
		public DatabaseBindingEntity(string databaseEntityKey)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(databaseEntityKey, "databaseEntityKey");
			this.DatabaseFullName = CommonUtils.GetDatabaseFullName(databaseEntityKey);
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000F246 File Offset: 0x0000D446
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x0000F24E File Offset: 0x0000D44E
		[DataMember]
		public string DatabaseFullName { get; private set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000F257 File Offset: 0x0000D457
		public string Key
		{
			get
			{
				return CommonUtils.GenerateEntityKey(PersistableItemTypes.DatabaseBindingEntity, this.DatabaseFullName);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000E3C2 File Offset: 0x0000C5C2
		public bool IsBackupEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000F268 File Offset: 0x0000D468
		public byte[] Serialize()
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new DataContractSerializer(typeof(DatabaseBindingEntity)).WriteObject(memoryStream, this);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000F2B8 File Offset: 0x0000D4B8
		public T Deserialize<T>(byte[] data)
		{
			T t;
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				t = (T)((object)new DataContractSerializer(typeof(T)).ReadObject(memoryStream));
			}
			return t;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000F304 File Offset: 0x0000D504
		public IPersistable Clone()
		{
			return new DatabaseBindingEntity
			{
				DatabaseFullName = this.DatabaseFullName
			};
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000F317 File Offset: 0x0000D517
		public PersistableItemTypes EnumType()
		{
			return PersistableItemTypes.DatabaseBindingEntity;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000F31A File Offset: 0x0000D51A
		public override string ToString()
		{
			return this.Key;
		}
	}
}
