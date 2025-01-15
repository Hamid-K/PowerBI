using System;
using Microsoft.AnalysisServices.Tabular.Tmdl;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001C8 RID: 456
	internal static class MetadataObjectExtensions
	{
		// Token: 0x06001BD4 RID: 7124 RVA: 0x000C31CF File Offset: 0x000C13CF
		public static TmdlObject ToTmdlObject(this MetadataObject @object)
		{
			return MetadataObjectConfiguration.Default.Converters[@object.ObjectType].ToTMDL(@object);
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x000C31EC File Offset: 0x000C13EC
		public static TmdlObject ToTmdlObject(this MetadataObject @object, IMetadataObjectConfiguration configuration)
		{
			return configuration.Converters[@object.ObjectType].ToTMDL(@object);
		}
	}
}
