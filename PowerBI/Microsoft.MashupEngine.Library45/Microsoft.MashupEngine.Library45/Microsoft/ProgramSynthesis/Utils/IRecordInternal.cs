using System;
using System.Collections;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020003DE RID: 990
	internal interface IRecordInternal
	{
		// Token: 0x06001620 RID: 5664
		int GetHashCode(IEqualityComparer comparer);

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001621 RID: 5665
		int Size { get; }

		// Token: 0x06001622 RID: 5666
		string ToStringEnd();
	}
}
