using System;
using System.Collections.Generic;

namespace Microsoft.Spatial
{
	// Token: 0x0200002D RID: 45
	public abstract class GeoJsonObjectFormatter
	{
		// Token: 0x06000112 RID: 274 RVA: 0x000038B4 File Offset: 0x00001AB4
		public static GeoJsonObjectFormatter Create()
		{
			return SpatialImplementation.CurrentImplementation.CreateGeoJsonObjectFormatter();
		}

		// Token: 0x06000113 RID: 275
		public abstract T Read<T>(IDictionary<string, object> source) where T : class, ISpatial;

		// Token: 0x06000114 RID: 276
		public abstract IDictionary<string, object> Write(ISpatial value);

		// Token: 0x06000115 RID: 277
		public abstract SpatialPipeline CreateWriter(IGeoJsonWriter writer);
	}
}
