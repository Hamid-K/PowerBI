using System;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.AnalysisServices.Azure.Common.Utils;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000067 RID: 103
	[DataContract]
	public sealed class VirtualServerEntity : IPersistable
	{
		// Token: 0x060004B9 RID: 1209 RVA: 0x0000ED9F File Offset: 0x0000CF9F
		public VirtualServerEntity()
		{
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001058D File Offset: 0x0000E78D
		public VirtualServerEntity(string virtualServerName)
			: this(virtualServerName, ProvisioningConstants.WowAuthorityId, ProvisioningConstants.WowSubscription)
		{
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x000105A0 File Offset: 0x0000E7A0
		public VirtualServerEntity(string virtualServerName, string authorityId, string subscriptionName)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(virtualServerName, "virtualServerName");
			this.VirtualServerName = virtualServerName;
			this.AuthorityId = authorityId;
			this.SubscriptionName = subscriptionName;
			this.CreatedDate = DateTime.UtcNow;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x000105D3 File Offset: 0x0000E7D3
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x000105DB File Offset: 0x0000E7DB
		[DataMember]
		public string VirtualServerName { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x000105E4 File Offset: 0x0000E7E4
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x000105EC File Offset: 0x0000E7EC
		[DataMember]
		public string AuthorityId { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x000105F5 File Offset: 0x0000E7F5
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x000105FD File Offset: 0x0000E7FD
		[DataMember]
		public string SubscriptionName { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00010606 File Offset: 0x0000E806
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x0001060E File Offset: 0x0000E80E
		[DataMember]
		public DateTime CreatedDate { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00010617 File Offset: 0x0000E817
		public string Id
		{
			get
			{
				return this.VirtualServerName;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0001061F File Offset: 0x0000E81F
		public string Key
		{
			get
			{
				return CommonUtils.GenerateEntityKey(PersistableItemTypes.VirtualServerEntity, this.VirtualServerName);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0000A3EB File Offset: 0x000085EB
		public bool IsBackupEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00010630 File Offset: 0x0000E830
		public byte[] Serialize()
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new DataContractSerializer(typeof(VirtualServerEntity)).WriteObject(memoryStream, this);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00010680 File Offset: 0x0000E880
		public T Deserialize<T>(byte[] data)
		{
			T t;
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				t = (T)((object)new DataContractSerializer(typeof(T)).ReadObject(memoryStream));
			}
			return t;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000106CC File Offset: 0x0000E8CC
		public IPersistable Clone()
		{
			return new VirtualServerEntity(this.VirtualServerName)
			{
				VirtualServerName = this.VirtualServerName,
				AuthorityId = this.AuthorityId,
				SubscriptionName = this.SubscriptionName,
				CreatedDate = this.CreatedDate
			};
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00010709 File Offset: 0x0000E909
		public PersistableItemTypes EnumType()
		{
			return PersistableItemTypes.VirtualServerEntity;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001070C File Offset: 0x0000E90C
		public override string ToString()
		{
			return "VirtualServerEntity:[Id={0};AuthorityId={1};SubscriptionName={2};CreatedDate={3}]".FormatWithInvariantCulture(new object[] { this.Key, this.AuthorityId, this.SubscriptionName, this.CreatedDate });
		}
	}
}
