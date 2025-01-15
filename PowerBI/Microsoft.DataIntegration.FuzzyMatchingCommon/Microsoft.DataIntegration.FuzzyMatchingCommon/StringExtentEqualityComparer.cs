using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x02000020 RID: 32
	[Serializable]
	public class StringExtentEqualityComparer : IEqualityComparer<StringExtent>
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00002ED9 File Offset: 0x000010D9
		public bool Equals(StringExtent x, StringExtent y)
		{
			return x.Equals(y);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00002EE3 File Offset: 0x000010E3
		public int GetHashCode(StringExtent x)
		{
			return x.GetHashCode();
		}

		// Token: 0x0400001E RID: 30
		public static readonly StringExtentEqualityComparer Instance = new StringExtentEqualityComparer();
	}
}
