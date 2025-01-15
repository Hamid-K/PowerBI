using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent
{
	// Token: 0x020000B9 RID: 185
	[Serializable]
	public class GenericConcurrentFastIntToIntDictionary : ConcurrentFastDictionary<int, int, IntToIntFastHashHelper>
	{
		// Token: 0x06000797 RID: 1943 RVA: 0x00028304 File Offset: 0x00026504
		public GenericConcurrentFastIntToIntDictionary()
			: base(0.7f)
		{
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00028311 File Offset: 0x00026511
		public GenericConcurrentFastIntToIntDictionary(float load)
			: base(load)
		{
		}
	}
}
