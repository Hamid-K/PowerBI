using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x0200016A RID: 362
	internal static class ForeignKeyBuilderExtensions
	{
		// Token: 0x06001688 RID: 5768 RVA: 0x0003B843 File Offset: 0x00039A43
		public static string GetPreferredName(this ForeignKeyBuilder fk)
		{
			return (string)fk.Annotations.GetAnnotation("PreferredName");
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x0003B85A File Offset: 0x00039A5A
		public static void SetPreferredName(this ForeignKeyBuilder fk, string name)
		{
			fk.GetMetadataProperties().SetAnnotation("PreferredName", name);
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x0003B870 File Offset: 0x00039A70
		public static bool GetIsTypeConstraint(this ForeignKeyBuilder fk)
		{
			object annotation = fk.Annotations.GetAnnotation("IsTypeConstraint");
			return annotation != null && (bool)annotation;
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x0003B899 File Offset: 0x00039A99
		public static void SetIsTypeConstraint(this ForeignKeyBuilder fk)
		{
			fk.GetMetadataProperties().SetAnnotation("IsTypeConstraint", true);
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x0003B8B1 File Offset: 0x00039AB1
		public static void SetIsSplitConstraint(this ForeignKeyBuilder fk)
		{
			fk.GetMetadataProperties().SetAnnotation("IsSplitConstraint", true);
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x0003B8C9 File Offset: 0x00039AC9
		public static AssociationType GetAssociationType(this ForeignKeyBuilder fk)
		{
			return fk.Annotations.GetAnnotation("AssociationType") as AssociationType;
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0003B8E0 File Offset: 0x00039AE0
		public static void SetAssociationType(this ForeignKeyBuilder fk, AssociationType associationType)
		{
			fk.GetMetadataProperties().SetAnnotation("AssociationType", associationType);
		}

		// Token: 0x04000A09 RID: 2569
		private const string IsTypeConstraint = "IsTypeConstraint";

		// Token: 0x04000A0A RID: 2570
		private const string IsSplitConstraint = "IsSplitConstraint";

		// Token: 0x04000A0B RID: 2571
		private const string AssociationType = "AssociationType";

		// Token: 0x04000A0C RID: 2572
		private const string PreferredNameAnnotation = "PreferredName";
	}
}
