using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004F3 RID: 1267
	internal static class RelationshipMultiplicityConverter
	{
		// Token: 0x06003ED9 RID: 16089 RVA: 0x000D1056 File Offset: 0x000CF256
		internal static string MultiplicityToString(RelationshipMultiplicity multiplicity)
		{
			switch (multiplicity)
			{
			case RelationshipMultiplicity.ZeroOrOne:
				return "0..1";
			case RelationshipMultiplicity.One:
				return "1";
			case RelationshipMultiplicity.Many:
				return "*";
			default:
				return string.Empty;
			}
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x000D1084 File Offset: 0x000CF284
		internal static bool TryParseMultiplicity(string value, out RelationshipMultiplicity multiplicity)
		{
			if (value != null)
			{
				if (value == "*")
				{
					multiplicity = RelationshipMultiplicity.Many;
					return true;
				}
				if (value == "1")
				{
					multiplicity = RelationshipMultiplicity.One;
					return true;
				}
				if (value == "0..1")
				{
					multiplicity = RelationshipMultiplicity.ZeroOrOne;
					return true;
				}
			}
			multiplicity = (RelationshipMultiplicity)(-1);
			return false;
		}
	}
}
