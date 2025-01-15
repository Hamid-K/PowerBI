using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Mashup.Client.Packaging.BinarySerialization;

namespace Microsoft.Mashup.Client.Packaging.SerializationObjectModel
{
	// Token: 0x0200000E RID: 14
	public class QueryGroupMetadataSet : IEnumerable<QueryGroupMetadata>, IEnumerable, IBinarySerializable
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002A5B File Offset: 0x00000C5B
		public int Count
		{
			get
			{
				return this.queryGroupMetadataList.Count;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002A68 File Offset: 0x00000C68
		public void Add(QueryGroupMetadata queryGroupMetadata)
		{
			this.queryGroupMetadataList.Add(queryGroupMetadata);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A76 File Offset: 0x00000C76
		public void AddRange(IEnumerable<QueryGroupMetadata> queryGroupMetadataCollection)
		{
			this.queryGroupMetadataList.AddRange(queryGroupMetadataCollection);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A84 File Offset: 0x00000C84
		public IEnumerator<QueryGroupMetadata> GetEnumerator()
		{
			return this.queryGroupMetadataList.GetEnumerator();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A96 File Offset: 0x00000C96
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002A9E File Offset: 0x00000C9E
		void IBinarySerializable.Deserialize(BinarySerializationReader reader)
		{
			this.queryGroupMetadataList = reader.ReadList<QueryGroupMetadata>();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002AAC File Offset: 0x00000CAC
		void IBinarySerializable.Serialize(BinarySerializationWriter writer)
		{
			writer.WriteList<QueryGroupMetadata>(this.queryGroupMetadataList);
		}

		// Token: 0x0400004A RID: 74
		private List<QueryGroupMetadata> queryGroupMetadataList = new List<QueryGroupMetadata>();
	}
}
