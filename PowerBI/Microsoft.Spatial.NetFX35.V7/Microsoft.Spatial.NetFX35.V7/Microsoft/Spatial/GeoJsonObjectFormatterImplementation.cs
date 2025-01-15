using System;
using System.Collections.Generic;

namespace Microsoft.Spatial
{
	// Token: 0x02000005 RID: 5
	internal class GeoJsonObjectFormatterImplementation : GeoJsonObjectFormatter
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002444 File Offset: 0x00000644
		public GeoJsonObjectFormatterImplementation(SpatialImplementation creator)
		{
			this.creator = creator;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002454 File Offset: 0x00000654
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

		// Token: 0x06000032 RID: 50 RVA: 0x000024D0 File Offset: 0x000006D0
		public override IDictionary<string, object> Write(ISpatial value)
		{
			GeoJsonObjectWriter geoJsonObjectWriter = new GeoJsonObjectWriter();
			value.SendTo(new ForwardingSegment(geoJsonObjectWriter));
			return geoJsonObjectWriter.JsonObject;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000024FA File Offset: 0x000006FA
		public override SpatialPipeline CreateWriter(IGeoJsonWriter writer)
		{
			return new ForwardingSegment(new WrappedGeoJsonWriter(writer));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000250C File Offset: 0x0000070C
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

		// Token: 0x0400000A RID: 10
		private readonly SpatialImplementation creator;

		// Token: 0x0400000B RID: 11
		private SpatialBuilder builder;

		// Token: 0x0400000C RID: 12
		private SpatialPipeline parsePipeline;
	}
}
