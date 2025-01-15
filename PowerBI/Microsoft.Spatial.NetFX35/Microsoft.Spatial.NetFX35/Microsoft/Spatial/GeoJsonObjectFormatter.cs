using System;
using System.Collections.Generic;
using Microsoft.Data.Spatial;

namespace Microsoft.Spatial
{
	// Token: 0x0200000A RID: 10
	public abstract class GeoJsonObjectFormatter
	{
		// Token: 0x06000080 RID: 128 RVA: 0x000029DF File Offset: 0x00000BDF
		public static GeoJsonObjectFormatter Create()
		{
			return SpatialImplementation.CurrentImplementation.CreateGeoJsonObjectFormatter();
		}

		// Token: 0x06000081 RID: 129
		public abstract T Read<T>(IDictionary<string, object> source) where T : class, ISpatial;

		// Token: 0x06000082 RID: 130
		public abstract IDictionary<string, object> Write(ISpatial value);

		// Token: 0x06000083 RID: 131
		public abstract SpatialPipeline CreateWriter(IGeoJsonWriter writer);
	}
}
