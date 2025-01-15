using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x0200016F RID: 367
	internal static class RelationshipEndMemberExtensions
	{
		// Token: 0x060016A1 RID: 5793 RVA: 0x0003BB33 File Offset: 0x00039D33
		public static bool IsMany(this RelationshipEndMember associationEnd)
		{
			return associationEnd.RelationshipMultiplicity.IsMany();
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x0003BB40 File Offset: 0x00039D40
		public static bool IsOptional(this RelationshipEndMember associationEnd)
		{
			return associationEnd.RelationshipMultiplicity.IsOptional();
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x0003BB4D File Offset: 0x00039D4D
		public static bool IsRequired(this RelationshipEndMember associationEnd)
		{
			return associationEnd.RelationshipMultiplicity.IsRequired();
		}
	}
}
