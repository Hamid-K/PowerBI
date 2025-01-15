using System;
using System.Linq;
using Microsoft.Mashup.Client.Packaging.BinarySerialization;
using Microsoft.Mashup.Client.Packaging.SerializationObjectModel;

namespace Microsoft.Mashup.Client.Packaging.Serializers
{
	// Token: 0x02000017 RID: 23
	public static class QueriesMetadataSerializer
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00003170 File Offset: 0x00001370
		public static bool TryGetQueryGroups(SerializedPackageItemMetadata itemMetadata, out QueryGroupMetadataSet queryGroups)
		{
			SerializedMetadataEntry serializedMetadataEntry = Enumerable.FirstOrDefault<SerializedMetadataEntry>(itemMetadata.Entries, (SerializedMetadataEntry e) => e.Type == "QueryGroups");
			if (serializedMetadataEntry == null)
			{
				queryGroups = null;
				return false;
			}
			queryGroups = QueriesMetadataSerializer.DeserializeQueryGroups(serializedMetadataEntry);
			return true;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000031B9 File Offset: 0x000013B9
		public static QueryGroupMetadataSet DeserializeQueryGroups(SerializedMetadataEntry queryGroupMetadataEntry)
		{
			return QueriesMetadataSerializer.DeserializeQueryGroups(queryGroupMetadataEntry.StringValue);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000031C6 File Offset: 0x000013C6
		public static QueryGroupMetadataSet DeserializeQueryGroups(string value)
		{
			return BinarySerializer.DeserializeBytes<QueryGroupMetadataSet>(Convert.FromBase64String(value));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000031D3 File Offset: 0x000013D3
		public static string SerializeQueryGroups(QueryGroupMetadataSet queryGroupMetadataSet)
		{
			return Convert.ToBase64String(BinarySerializer.SerializeBytes<QueryGroupMetadataSet>(queryGroupMetadataSet));
		}
	}
}
