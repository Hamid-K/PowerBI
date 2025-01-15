using System;
using System.Data.Entity.Core.Mapping;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000171 RID: 369
	internal static class StorageAssociationSetMappingExtensions
	{
		// Token: 0x060016A7 RID: 5799 RVA: 0x0003BB6C File Offset: 0x00039D6C
		public static AssociationSetMapping Initialize(this AssociationSetMapping associationSetMapping)
		{
			associationSetMapping.SourceEndMapping = new EndPropertyMapping();
			associationSetMapping.TargetEndMapping = new EndPropertyMapping();
			return associationSetMapping;
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x0003BB85 File Offset: 0x00039D85
		public static object GetConfiguration(this AssociationSetMapping associationSetMapping)
		{
			return associationSetMapping.Annotations.GetConfiguration();
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x0003BB92 File Offset: 0x00039D92
		public static void SetConfiguration(this AssociationSetMapping associationSetMapping, object configuration)
		{
			associationSetMapping.Annotations.SetConfiguration(configuration);
		}
	}
}
