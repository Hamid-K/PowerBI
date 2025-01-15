using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x0200001F RID: 31
	internal static class MetdataItemExtensions
	{
		// Token: 0x06000374 RID: 884 RVA: 0x0000EB48 File Offset: 0x0000CD48
		public static T GetMetadataPropertyValue<T>(this MetadataItem item, string propertyName)
		{
			MetadataProperty metadataProperty = item.MetadataProperties.FirstOrDefault((MetadataProperty p) => p.Name == propertyName);
			if (metadataProperty != null)
			{
				return (T)((object)metadataProperty.Value);
			}
			return default(T);
		}
	}
}
