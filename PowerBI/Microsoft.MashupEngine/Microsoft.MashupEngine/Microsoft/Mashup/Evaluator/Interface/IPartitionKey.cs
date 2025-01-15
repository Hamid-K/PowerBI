using System;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E21 RID: 7713
	public interface IPartitionKey : IEquatable<IPartitionKey>
	{
		// Token: 0x17002EC9 RID: 11977
		// (get) Token: 0x0600BE12 RID: 48658
		PartitioningScheme PartitioningScheme { get; }
	}
}
