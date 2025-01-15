using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000099 RID: 153
	[Serializable]
	public sealed class IdentitySignatureGenerator : IOneDimSignatureGenerator, IEnumerable<int>, IEnumerable
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0001A5FD File Offset: 0x000187FD
		public int Indexes
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001A600 File Offset: 0x00018800
		public void ResetDimension(int signIdx)
		{
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001A602 File Offset: 0x00018802
		public void Reset(WeightedTokenSequence tokenSequence, ArraySegmentBuilder<WeightedTransformationMatch> matchList)
		{
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001A604 File Offset: 0x00018804
		public IEnumerator<int> GetEnumerator()
		{
			return this.m_signatures.GetEnumerator();
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001A611 File Offset: 0x00018811
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_signatures.GetEnumerator();
		}

		// Token: 0x04000202 RID: 514
		private int[] m_signatures = new int[] { 1 };
	}
}
