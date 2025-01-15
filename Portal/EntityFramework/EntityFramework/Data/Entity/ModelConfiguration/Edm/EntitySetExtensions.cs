using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000167 RID: 359
	internal static class EntitySetExtensions
	{
		// Token: 0x06001670 RID: 5744 RVA: 0x0003B409 File Offset: 0x00039609
		public static object GetConfiguration(this EntitySet entitySet)
		{
			return entitySet.Annotations.GetConfiguration();
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x0003B416 File Offset: 0x00039616
		public static void SetConfiguration(this EntitySet entitySet, object configuration)
		{
			entitySet.GetMetadataProperties().SetConfiguration(configuration);
		}
	}
}
