using System;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E48 RID: 7752
	public static class PartitionKeyExtensions
	{
		// Token: 0x0600BE8A RID: 48778 RVA: 0x002687EF File Offset: 0x002669EF
		public static IPartitionKey ToPartitionKey(this string serializedString, PartitioningScheme partitioningScheme)
		{
			if (partitioningScheme == PartitioningScheme.MemberLet1)
			{
				return MemberLetPartitionKeySerializer.Deserialize(serializedString);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600BE8B RID: 48779 RVA: 0x00268801 File Offset: 0x00266A01
		public static string ToSerializedString(this IPartitionKey partitionKey)
		{
			if (partitionKey.PartitioningScheme == PartitioningScheme.MemberLet1)
			{
				return MemberLetPartitionKeySerializer.Serialize((IMemberLetPartitionKey)partitionKey);
			}
			throw new InvalidOperationException();
		}
	}
}
