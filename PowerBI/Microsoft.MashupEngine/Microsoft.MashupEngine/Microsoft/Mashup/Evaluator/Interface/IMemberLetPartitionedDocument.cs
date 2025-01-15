using System;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E0B RID: 7691
	public interface IMemberLetPartitionedDocument : IPartitionedDocument
	{
		// Token: 0x0600BDD1 RID: 48593
		bool IsPartitionResultOfMember(IPartitionKey partitionKey);

		// Token: 0x0600BDD2 RID: 48594
		bool ArePartitionsOfSameMember(IPartitionKey partitionKey1, IPartitionKey partitionKey2);

		// Token: 0x0600BDD3 RID: 48595
		IMemberLetPartitionKey GetSpecificPartitionKey(IMemberLetPartitionKey partitionKey);
	}
}
