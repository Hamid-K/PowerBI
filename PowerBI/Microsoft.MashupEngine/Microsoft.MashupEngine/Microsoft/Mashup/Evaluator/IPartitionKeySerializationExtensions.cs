using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CD7 RID: 7383
	public static class IPartitionKeySerializationExtensions
	{
		// Token: 0x0600B7DA RID: 47066 RVA: 0x002548EB File Offset: 0x00252AEB
		public static void WriteIPartitionKey(this BinaryWriter writer, IPartitionKey partitionKey)
		{
			writer.WriteInt32((int)partitionKey.PartitioningScheme);
			writer.WriteString(partitionKey.ToSerializedString());
		}

		// Token: 0x0600B7DB RID: 47067 RVA: 0x00254908 File Offset: 0x00252B08
		public static IPartitionKey ReadIPartitionKey(this BinaryReader reader)
		{
			PartitioningScheme partitioningScheme = (PartitioningScheme)reader.ReadInt32();
			return reader.ReadString().ToPartitionKey(partitioningScheme);
		}
	}
}
