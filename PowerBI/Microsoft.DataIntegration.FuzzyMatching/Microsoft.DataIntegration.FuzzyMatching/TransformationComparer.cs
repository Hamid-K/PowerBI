using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000F2 RID: 242
	[Serializable]
	public class TransformationComparer : IEqualityComparer<Transformation>
	{
		// Token: 0x060009CF RID: 2511 RVA: 0x0002CC00 File Offset: 0x0002AE00
		public bool Equals(Transformation x, Transformation y)
		{
			return x.From.Equals(y.From) && x.To.Equals(y.To);
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0002CC2A File Offset: 0x0002AE2A
		public int GetHashCode(Transformation x)
		{
			return x.From.GetHashCode() ^ x.To.GetHashCode();
		}

		// Token: 0x040003BD RID: 957
		public static readonly TransformationComparer Instance = new TransformationComparer();
	}
}
