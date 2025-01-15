using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x0200015E RID: 350
	internal static class AssociationTypeExtensions
	{
		// Token: 0x06001614 RID: 5652 RVA: 0x00039D48 File Offset: 0x00037F48
		public static void MarkIndependent(this AssociationType associationType)
		{
			associationType.GetMetadataProperties().SetAnnotation("IsIndependent", true);
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x00039D60 File Offset: 0x00037F60
		public static bool IsIndependent(this AssociationType associationType)
		{
			object annotation = associationType.Annotations.GetAnnotation("IsIndependent");
			return annotation != null && (bool)annotation;
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x00039D89 File Offset: 0x00037F89
		public static void MarkPrincipalConfigured(this AssociationType associationType)
		{
			associationType.GetMetadataProperties().SetAnnotation("IsPrincipalConfigured", true);
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x00039DA4 File Offset: 0x00037FA4
		public static bool IsPrincipalConfigured(this AssociationType associationType)
		{
			object annotation = associationType.Annotations.GetAnnotation("IsPrincipalConfigured");
			return annotation != null && (bool)annotation;
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x00039DCD File Offset: 0x00037FCD
		public static AssociationEndMember GetOtherEnd(this AssociationType associationType, AssociationEndMember associationEnd)
		{
			if (associationEnd != associationType.SourceEnd)
			{
				return associationType.SourceEnd;
			}
			return associationType.TargetEnd;
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x00039DE5 File Offset: 0x00037FE5
		public static object GetConfiguration(this AssociationType associationType)
		{
			return associationType.Annotations.GetConfiguration();
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00039DF2 File Offset: 0x00037FF2
		public static void SetConfiguration(this AssociationType associationType, object configuration)
		{
			associationType.GetMetadataProperties().SetConfiguration(configuration);
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x00039E00 File Offset: 0x00038000
		public static bool IsRequiredToMany(this AssociationType associationType)
		{
			return associationType.SourceEnd.IsRequired() && associationType.TargetEnd.IsMany();
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x00039E1C File Offset: 0x0003801C
		public static bool IsRequiredToRequired(this AssociationType associationType)
		{
			return associationType.SourceEnd.IsRequired() && associationType.TargetEnd.IsRequired();
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x00039E38 File Offset: 0x00038038
		public static bool IsManyToRequired(this AssociationType associationType)
		{
			return associationType.SourceEnd.IsMany() && associationType.TargetEnd.IsRequired();
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x00039E54 File Offset: 0x00038054
		public static bool IsManyToMany(this AssociationType associationType)
		{
			return associationType.SourceEnd.IsMany() && associationType.TargetEnd.IsMany();
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x00039E70 File Offset: 0x00038070
		public static bool IsOneToOne(this AssociationType associationType)
		{
			return !associationType.SourceEnd.IsMany() && !associationType.TargetEnd.IsMany();
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x00039E90 File Offset: 0x00038090
		public static bool IsSelfReferencing(this AssociationType associationType)
		{
			RelationshipEndMember sourceEnd = associationType.SourceEnd;
			AssociationEndMember targetEnd = associationType.TargetEnd;
			return sourceEnd.GetEntityType().GetRootType() == targetEnd.GetEntityType().GetRootType();
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x00039EC1 File Offset: 0x000380C1
		public static bool IsRequiredToNonRequired(this AssociationType associationType)
		{
			return (associationType.SourceEnd.IsRequired() && !associationType.TargetEnd.IsRequired()) || (associationType.TargetEnd.IsRequired() && !associationType.SourceEnd.IsRequired());
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x00039EFC File Offset: 0x000380FC
		public static bool TryGuessPrincipalAndDependentEnds(this AssociationType associationType, out AssociationEndMember principalEnd, out AssociationEndMember dependentEnd)
		{
			AssociationEndMember associationEndMember;
			dependentEnd = (associationEndMember = null);
			principalEnd = associationEndMember;
			AssociationEndMember sourceEnd = associationType.SourceEnd;
			AssociationEndMember targetEnd = associationType.TargetEnd;
			if (sourceEnd.RelationshipMultiplicity != targetEnd.RelationshipMultiplicity)
			{
				principalEnd = ((sourceEnd.IsRequired() || (sourceEnd.IsOptional() && targetEnd.IsMany())) ? sourceEnd : targetEnd);
				dependentEnd = ((principalEnd == sourceEnd) ? targetEnd : sourceEnd);
			}
			return principalEnd != null;
		}

		// Token: 0x04000A00 RID: 2560
		private const string IsIndependentAnnotation = "IsIndependent";

		// Token: 0x04000A01 RID: 2561
		private const string IsPrincipalConfiguredAnnotation = "IsPrincipalConfigured";
	}
}
