using System;
using System.Runtime.Serialization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000035 RID: 53
	[DataContract]
	public class EntityRecord
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00005049 File Offset: 0x00003249
		public EntityRecord(EntityRecord other)
			: this(other.Id, other.CreationDate, other.Epoch)
		{
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005063 File Offset: 0x00003263
		public EntityRecord(long id, DateTime creationDate, Guid epoch)
		{
			this.Id = id;
			this.CreationDate = creationDate;
			this.Epoch = epoch;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00005080 File Offset: 0x00003280
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00005088 File Offset: 0x00003288
		[DataMember]
		public long Id { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00005091 File Offset: 0x00003291
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00005099 File Offset: 0x00003299
		[DataMember]
		public DateTime CreationDate { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000050A2 File Offset: 0x000032A2
		// (set) Token: 0x06000145 RID: 325 RVA: 0x000050AA File Offset: 0x000032AA
		[DataMember]
		public Guid Epoch { get; private set; }

		// Token: 0x06000146 RID: 326 RVA: 0x000050B4 File Offset: 0x000032B4
		public override bool Equals(object obj)
		{
			EntityRecord entityRecord = obj as EntityRecord;
			return entityRecord != null && (this.Id == entityRecord.Id || this.Id == -1L || entityRecord.Id == -1L) && this.Epoch.Equals(entityRecord.Epoch);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00005103 File Offset: 0x00003303
		public override int GetHashCode()
		{
			return ExtendedMath.Fold(this.Id);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00005110 File Offset: 0x00003310
		public override string ToString()
		{
			return "<Id={0}, Created={1}, Epoch={2}>".FormatWithInvariantCulture(new object[] { this.Id, this.CreationDate, this.Epoch });
		}
	}
}
