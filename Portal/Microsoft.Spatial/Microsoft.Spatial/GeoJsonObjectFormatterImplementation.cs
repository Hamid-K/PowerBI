using System;
using System.Collections.Generic;

namespace Microsoft.Spatial
{
	// Token: 0x02000005 RID: 5
	internal class GeoJsonObjectFormatterImplementation : GeoJsonObjectFormatter
	{
		// Token: 0x06000042 RID: 66 RVA: 0x0000282C File Offset: 0x00000A2C
		public GeoJsonObjectFormatterImplementation(SpatialImplementation creator)
		{
			this.creator = creator;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000283C File Offset: 0x00000A3C
		public override T Read<T>(IDictionary<string, object> source)
		{
			this.EnsureParsePipeline();
			if (typeof(Geometry).IsAssignableFrom(typeof(T)))
			{
				new GeoJsonObjectReader(this.builder).ReadGeometry(source);
				return this.builder.ConstructedGeometry as T;
			}
			new GeoJsonObjectReader(this.builder).ReadGeography(source);
			return this.builder.ConstructedGeography as T;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000028B8 File Offset: 0x00000AB8
		public override IDictionary<string, object> Write(ISpatial value)
		{
			GeoJsonObjectWriter geoJsonObjectWriter = new GeoJsonObjectWriter();
			value.SendTo(new ForwardingSegment(geoJsonObjectWriter));
			return geoJsonObjectWriter.JsonObject;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000028E2 File Offset: 0x00000AE2
		public override SpatialPipeline CreateWriter(IGeoJsonWriter writer)
		{
			return new ForwardingSegment(new WrappedGeoJsonWriter(writer));
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000028F4 File Offset: 0x00000AF4
		private void EnsureParsePipeline()
		{
			if (this.parsePipeline == null)
			{
				this.builder = this.creator.CreateBuilder();
				this.parsePipeline = this.creator.CreateValidator().ChainTo(this.builder);
				return;
			}
			this.parsePipeline.GeographyPipeline.Reset();
			this.parsePipeline.GeometryPipeline.Reset();
		}

		// Token: 0x0400000B RID: 11
		private readonly SpatialImplementation creator;

		// Token: 0x0400000C RID: 12
		private SpatialBuilder builder;

		// Token: 0x0400000D RID: 13
		private SpatialPipeline parsePipeline;
	}
}
