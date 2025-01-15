using System;
using System.Collections.Generic;

namespace Microsoft.Spatial
{
	// Token: 0x02000031 RID: 49
	public abstract class GeoJsonObjectFormatter
	{
		// Token: 0x0600017E RID: 382 RVA: 0x000043C4 File Offset: 0x000025C4
		public static GeoJsonObjectFormatter Create()
		{
			return SpatialImplementation.CurrentImplementation.CreateGeoJsonObjectFormatter();
		}

		// Token: 0x0600017F RID: 383
		public abstract T Read<T>(IDictionary<string, object> source) where T : class, ISpatial;

		// Token: 0x06000180 RID: 384
		public abstract IDictionary<string, object> Write(ISpatial value);

		// Token: 0x06000181 RID: 385
		public abstract SpatialPipeline CreateWriter(IGeoJsonWriter writer);
	}
}
