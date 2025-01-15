using System;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core.DomainModel
{
	// Token: 0x02000173 RID: 371
	internal static class ConceptualMultiplicityExtentions
	{
		// Token: 0x06000735 RID: 1845 RVA: 0x0000C40C File Offset: 0x0000A60C
		internal static LinguisticCardinalityNumber ToLinguisticCardinalityNumber(this ConceptualMultiplicity multiplicity)
		{
			if (multiplicity <= ConceptualMultiplicity.One)
			{
				return LinguisticCardinalityNumber.One;
			}
			if (multiplicity != ConceptualMultiplicity.Many)
			{
				throw new NotSupportedException("Unexpected value for ConceptualMultiplicity");
			}
			return LinguisticCardinalityNumber.Many;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0000C426 File Offset: 0x0000A626
		internal static bool IsAnyOne(this ConceptualMultiplicity multiplicity)
		{
			return multiplicity == ConceptualMultiplicity.ZeroOrOne || multiplicity == ConceptualMultiplicity.One;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0000C431 File Offset: 0x0000A631
		internal static bool IsMany(this ConceptualMultiplicity multiplicity)
		{
			return multiplicity == ConceptualMultiplicity.Many;
		}
	}
}
