using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200003F RID: 63
	[Serializable]
	public class EditTransformationDistanceComparer : IComparer<TransformationMatch>
	{
		// Token: 0x0600027E RID: 638 RVA: 0x0000C308 File Offset: 0x0000A508
		public int Compare(TransformationMatch x, TransformationMatch y)
		{
			float editDistance = new EditTransformationMetadata(x.Transformation.Metadata).EditDistance;
			float editDistance2 = new EditTransformationMetadata(y.Transformation.Metadata).EditDistance;
			if (editDistance < editDistance2)
			{
				return -1;
			}
			if (editDistance != editDistance2)
			{
				return 1;
			}
			return 0;
		}
	}
}
