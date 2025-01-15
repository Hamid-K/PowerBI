using System;

namespace Microsoft.InfoNav
{
	// Token: 0x02000022 RID: 34
	public static class ConceptualEntityVisibilityTypeExtensions
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00002763 File Offset: 0x00000963
		public static bool IsHidden(this ConceptualEntityVisibilityType entityVisibilityType)
		{
			return ConceptualEntityVisibilityTypeExtensions.HasFlag(entityVisibilityType, ConceptualEntityVisibilityType.Hidden);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000276C File Offset: 0x0000096C
		public static bool ShowAsVariationsOnly(this ConceptualEntityVisibilityType entityVisibilityType)
		{
			return ConceptualEntityVisibilityTypeExtensions.HasFlag(entityVisibilityType, ConceptualEntityVisibilityType.ShowAsVariationsOnly);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002775 File Offset: 0x00000975
		public static bool IsPrivate(this ConceptualEntityVisibilityType entityVisibilityType)
		{
			return ConceptualEntityVisibilityTypeExtensions.HasFlag(entityVisibilityType, ConceptualEntityVisibilityType.Private);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000277E File Offset: 0x0000097E
		public static bool IsAlwaysVisible(this ConceptualEntityVisibilityType entityVisibilityType)
		{
			return entityVisibilityType == ConceptualEntityVisibilityType.AlwaysVisible;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002784 File Offset: 0x00000984
		private static bool HasFlag(ConceptualEntityVisibilityType type, ConceptualEntityVisibilityType flag)
		{
			return (type & flag) == flag;
		}
	}
}
