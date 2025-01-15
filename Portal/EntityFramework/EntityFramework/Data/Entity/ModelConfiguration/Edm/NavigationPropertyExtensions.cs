using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x0200016E RID: 366
	internal static class NavigationPropertyExtensions
	{
		// Token: 0x0600169E RID: 5790 RVA: 0x0003BAEC File Offset: 0x00039CEC
		public static object GetConfiguration(this NavigationProperty navigationProperty)
		{
			return navigationProperty.Annotations.GetConfiguration();
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x0003BAF9 File Offset: 0x00039CF9
		public static void SetConfiguration(this NavigationProperty navigationProperty, object configuration)
		{
			navigationProperty.GetMetadataProperties().SetConfiguration(configuration);
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0003BB07 File Offset: 0x00039D07
		public static AssociationEndMember GetFromEnd(this NavigationProperty navProp)
		{
			if (navProp.Association.SourceEnd != navProp.ResultEnd)
			{
				return navProp.Association.SourceEnd;
			}
			return navProp.Association.TargetEnd;
		}
	}
}
