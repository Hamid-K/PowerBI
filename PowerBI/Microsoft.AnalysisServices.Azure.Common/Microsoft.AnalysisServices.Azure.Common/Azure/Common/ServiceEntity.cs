using System;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.AnalysisServices.Azure.Common.Utils;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000063 RID: 99
	[DataContract]
	public sealed class ServiceEntity : IPersistable, IEquatable<ServiceEntity>
	{
		// Token: 0x06000494 RID: 1172 RVA: 0x0000ED9F File Offset: 0x0000CF9F
		public ServiceEntity()
		{
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x000102B3 File Offset: 0x0000E4B3
		public ServiceEntity(Uri serviceUri)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Uri>(serviceUri, "Service Uri cannot be empty");
			this.ServiceUri = serviceUri;
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x000102CD File Offset: 0x0000E4CD
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x000102D5 File Offset: 0x0000E4D5
		[DataMember]
		public Uri ServiceUri { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x000102DE File Offset: 0x0000E4DE
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x000102E8 File Offset: 0x0000E4E8
		[DataMember]
		public string LocalDatabaseId
		{
			get
			{
				return this.localDatabaseId;
			}
			set
			{
				this.localDatabaseId = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x000102F3 File Offset: 0x0000E4F3
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x000102FD File Offset: 0x0000E4FD
		[DataMember]
		public string MappedDatabaseEntityKey
		{
			get
			{
				return this.mappedDatabaseEntityKey;
			}
			set
			{
				this.mappedDatabaseEntityKey = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00010308 File Offset: 0x0000E508
		public string Key
		{
			get
			{
				return CommonUtils.GenerateEntityKey(PersistableItemTypes.ServiceEntity, this.ServiceUri.AbsoluteUri);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x0000E3C2 File Offset: 0x0000C5C2
		public bool IsBackupEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001031C File Offset: 0x0000E51C
		public byte[] Serialize()
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new DataContractSerializer(typeof(ServiceEntity)).WriteObject(memoryStream, this);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001036C File Offset: 0x0000E56C
		public T Deserialize<T>(byte[] data)
		{
			T t;
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				t = (T)((object)new DataContractSerializer(typeof(T)).ReadObject(memoryStream));
			}
			return t;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000103B8 File Offset: 0x0000E5B8
		public IPersistable Clone()
		{
			return new ServiceEntity(this.ServiceUri)
			{
				LocalDatabaseId = this.LocalDatabaseId,
				MappedDatabaseEntityKey = this.MappedDatabaseEntityKey,
				ServiceUri = this.ServiceUri
			};
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000103E9 File Offset: 0x0000E5E9
		public PersistableItemTypes EnumType()
		{
			return PersistableItemTypes.ServiceEntity;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000103EC File Offset: 0x0000E5EC
		public override string ToString()
		{
			return "ServiceEntity:[Id='{0}',MappedDatabase='{1}']".FormatWithInvariantCulture(new object[] { this.Key, this.MappedDatabaseEntityKey });
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00010410 File Offset: 0x0000E610
		public bool Equals(ServiceEntity other)
		{
			return other != null && this.Key.Equals(other.Key);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00010428 File Offset: 0x0000E628
		public override bool Equals(object other)
		{
			return this.Equals(other as ServiceEntity);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00010436 File Offset: 0x0000E636
		public override int GetHashCode()
		{
			return this.Key.GetHashCode();
		}

		// Token: 0x04000185 RID: 389
		private volatile string mappedDatabaseEntityKey;

		// Token: 0x04000186 RID: 390
		private volatile string localDatabaseId;
	}
}
