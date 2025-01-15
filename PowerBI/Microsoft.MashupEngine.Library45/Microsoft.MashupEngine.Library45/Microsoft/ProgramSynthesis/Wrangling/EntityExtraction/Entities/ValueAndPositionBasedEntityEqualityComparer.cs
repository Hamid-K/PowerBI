using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x02000217 RID: 535
	public class ValueAndPositionBasedEntityEqualityComparer : IEqualityComparer<EntityToken>
	{
		// Token: 0x06000B77 RID: 2935 RVA: 0x00002130 File Offset: 0x00000330
		private ValueAndPositionBasedEntityEqualityComparer()
		{
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x00022F8A File Offset: 0x0002118A
		public static ValueAndPositionBasedEntityEqualityComparer Instance { get; } = new ValueAndPositionBasedEntityEqualityComparer();

		// Token: 0x06000B79 RID: 2937 RVA: 0x00022F91 File Offset: 0x00021191
		public bool Equals(EntityToken x, EntityToken y)
		{
			return x == y || (x != null && y != null && (x.Start == y.Start && x.End == y.End) && x.ValueBasedEquality(y));
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00022FC8 File Offset: 0x000211C8
		public int GetHashCode(EntityToken obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return (obj.ValueBasedHashCode() * 4993) ^ (obj.Start.GetHashCode() * obj.End.GetHashCode()) ^ 3623;
		}
	}
}
