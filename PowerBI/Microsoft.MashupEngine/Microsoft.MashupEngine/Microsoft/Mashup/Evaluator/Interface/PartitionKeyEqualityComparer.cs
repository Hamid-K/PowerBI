using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E47 RID: 7751
	public sealed class PartitionKeyEqualityComparer : IEqualityComparer<IPartitionKey>
	{
		// Token: 0x0600BE86 RID: 48774 RVA: 0x0026878B File Offset: 0x0026698B
		public int GetHashCode(IPartitionKey partitionKey)
		{
			if (partitionKey.PartitioningScheme == PartitioningScheme.MemberLet1)
			{
				return MemberLetPartitionKeyEqualityComparer.Instance.GetHashCode((IMemberLetPartitionKey)partitionKey);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600BE87 RID: 48775 RVA: 0x002687AC File Offset: 0x002669AC
		public bool Equals(IPartitionKey partitionKey1, IPartitionKey partitionKey2)
		{
			if (partitionKey1.PartitioningScheme != partitionKey2.PartitioningScheme)
			{
				return false;
			}
			if (partitionKey1.PartitioningScheme == PartitioningScheme.MemberLet1)
			{
				return MemberLetPartitionKeyEqualityComparer.Instance.Equals((IMemberLetPartitionKey)partitionKey1, (IMemberLetPartitionKey)partitionKey2);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0400610A RID: 24842
		public static readonly IEqualityComparer<IPartitionKey> Instance = new PartitionKeyEqualityComparer();
	}
}
