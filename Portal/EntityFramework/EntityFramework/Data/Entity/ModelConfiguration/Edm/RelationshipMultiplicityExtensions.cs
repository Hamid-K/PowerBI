using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000170 RID: 368
	internal static class RelationshipMultiplicityExtensions
	{
		// Token: 0x060016A4 RID: 5796 RVA: 0x0003BB5A File Offset: 0x00039D5A
		public static bool IsMany(this RelationshipMultiplicity associationEndKind)
		{
			return associationEndKind == RelationshipMultiplicity.Many;
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x0003BB60 File Offset: 0x00039D60
		public static bool IsOptional(this RelationshipMultiplicity associationEndKind)
		{
			return associationEndKind == RelationshipMultiplicity.ZeroOrOne;
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x0003BB66 File Offset: 0x00039D66
		public static bool IsRequired(this RelationshipMultiplicity associationEndKind)
		{
			return associationEndKind == RelationshipMultiplicity.One;
		}
	}
}
