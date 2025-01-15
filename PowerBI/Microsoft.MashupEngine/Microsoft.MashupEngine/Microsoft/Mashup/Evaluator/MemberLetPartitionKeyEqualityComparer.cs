using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CF0 RID: 7408
	internal sealed class MemberLetPartitionKeyEqualityComparer : IEqualityComparer<IMemberLetPartitionKey>
	{
		// Token: 0x0600B8FB RID: 47355 RVA: 0x00257E14 File Offset: 0x00256014
		public int GetHashCode(IMemberLetPartitionKey partitionKey)
		{
			int num = partitionKey.Section.GetHashCode() + partitionKey.Member.GetHashCode();
			foreach (string text in partitionKey.Lets)
			{
				num += text.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600B8FC RID: 47356 RVA: 0x00257E7C File Offset: 0x0025607C
		public bool Equals(IMemberLetPartitionKey partitionKey1, IMemberLetPartitionKey partitionKey2)
		{
			return partitionKey1 == partitionKey2 || (partitionKey1 != null && partitionKey2 != null && (partitionKey1.Section == partitionKey2.Section && partitionKey1.Member == partitionKey2.Member) && partitionKey1.Lets.SequenceEqual(partitionKey2.Lets));
		}

		// Token: 0x04005E2D RID: 24109
		public static readonly IEqualityComparer<IMemberLetPartitionKey> Instance = new MemberLetPartitionKeyEqualityComparer();
	}
}
