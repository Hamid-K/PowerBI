using System;
using System.Collections;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.DotNet4
{
	// Token: 0x020000A9 RID: 169
	public interface IStructuralEquatable
	{
		// Token: 0x0600075A RID: 1882
		bool Equals(object other, IEqualityComparer comparer);

		// Token: 0x0600075B RID: 1883
		int GetHashCode(IEqualityComparer comparer);
	}
}
