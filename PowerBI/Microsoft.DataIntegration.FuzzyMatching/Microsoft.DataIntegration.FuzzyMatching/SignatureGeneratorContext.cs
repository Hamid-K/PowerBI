using System;
using System.Collections.Generic;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000D0 RID: 208
	internal class SignatureGeneratorContext
	{
		// Token: 0x060007E3 RID: 2019 RVA: 0x00026638 File Offset: 0x00024838
		public void Reset()
		{
			this.RecordContext.Reset();
			this.intAllocator.Reset();
			this.byteAllocator.Reset();
			this.tranMatchAllocator.Reset();
			this.signatures.Clear();
			if (this.signatureGeneratorSession != null)
			{
				this.signatureGeneratorSession.Reset();
			}
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00026690 File Offset: 0x00024890
		public void ComputeSignatures(WeightedTokenSequence tokenSequence, ArraySegmentBuilder<WeightedTransformationMatch> tranMatchList, List<long> signatures)
		{
			this.OneDimSignatureGenerator.Reset(tokenSequence, tranMatchList);
			int num = 1;
			if (this.MultiDimSignatureGenerator != null)
			{
				num = this.MultiDimSignatureGenerator.NumHashtables;
			}
			for (int i = 0; i < num; i++)
			{
				IEnumerator<int> enumerator;
				if (this.MultiDimSignatureGenerator != null)
				{
					this.MultiDimSignatureGenerator.Reset(i);
					enumerator = this.MultiDimSignatureGenerator.GetEnumerator();
				}
				else
				{
					enumerator = this.OneDimSignatureGenerator.GetEnumerator();
				}
				while (enumerator.MoveNext())
				{
					int num2 = enumerator.Current;
					signatures.Add((long)(((ulong)i << 32) | (ulong)num2));
				}
			}
		}

		// Token: 0x04000345 RID: 837
		public ITokenToClusterMap TokenClusterProvider = new IdentityTokenClusterProvider();

		// Token: 0x04000346 RID: 838
		public RecordContext RecordContext = new RecordContext();

		// Token: 0x04000347 RID: 839
		public BlockedSegmentArray<int> intAllocator = new BlockedSegmentArray<int>();

		// Token: 0x04000348 RID: 840
		public BlockedSegmentArray<byte> byteAllocator = new BlockedSegmentArray<byte>();

		// Token: 0x04000349 RID: 841
		public BlockedSegmentArray<WeightedTransformationMatch> tranMatchAllocator = new BlockedSegmentArray<WeightedTransformationMatch>();

		// Token: 0x0400034A RID: 842
		public List<long> signatures = new List<long>();

		// Token: 0x0400034B RID: 843
		public IOneDimSignatureGenerator OneDimSignatureGenerator;

		// Token: 0x0400034C RID: 844
		public IMultiDimSignatureGenerator MultiDimSignatureGenerator;

		// Token: 0x0400034D RID: 845
		public ISession signatureGeneratorSession;
	}
}
