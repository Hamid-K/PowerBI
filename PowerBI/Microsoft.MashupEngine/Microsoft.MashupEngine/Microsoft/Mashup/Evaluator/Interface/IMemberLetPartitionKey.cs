using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E0C RID: 7692
	public interface IMemberLetPartitionKey : IPartitionKey, IEquatable<IPartitionKey>
	{
		// Token: 0x17002EB7 RID: 11959
		// (get) Token: 0x0600BDD4 RID: 48596
		string Section { get; }

		// Token: 0x17002EB8 RID: 11960
		// (get) Token: 0x0600BDD5 RID: 48597
		string Member { get; }

		// Token: 0x17002EB9 RID: 11961
		// (get) Token: 0x0600BDD6 RID: 48598
		IList<string> Lets { get; }
	}
}
