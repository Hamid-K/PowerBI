using System;
using System.IO;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000081 RID: 129
	public interface IHashAdapter<TAdapter, TBase>
	{
		// Token: 0x06000591 RID: 1425
		void Serialize(Stream s);

		// Token: 0x06000592 RID: 1426
		TAdapter Deserialize(Stream s);

		// Token: 0x06000593 RID: 1427
		bool IsDefault2(TBase v);

		// Token: 0x06000594 RID: 1428
		bool Equals3(TBase x, TBase y);

		// Token: 0x06000595 RID: 1429
		int GetBucket2(TBase v, int mask);
	}
}
