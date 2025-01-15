using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000041 RID: 65
	[Serializable]
	public class TransformationRhsWeightComparer
	{
		// Token: 0x06000289 RID: 649 RVA: 0x0000C3FB File Offset: 0x0000A5FB
		public TransformationRhsWeightComparer(ITokenWeightProvider weightProvider)
		{
			this.m_weightProvider = weightProvider;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000C40C File Offset: 0x0000A60C
		public int Compare(TransformationMatch x, TransformationMatch y)
		{
			int weight = this.m_weightProvider.GetWeight(null, x.Transformation.To[0]);
			int weight2 = this.m_weightProvider.GetWeight(null, y.Transformation.To[0]);
			if (weight > weight2)
			{
				return -1;
			}
			if (weight != weight2)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x040000D0 RID: 208
		private ITokenWeightProvider m_weightProvider;
	}
}
