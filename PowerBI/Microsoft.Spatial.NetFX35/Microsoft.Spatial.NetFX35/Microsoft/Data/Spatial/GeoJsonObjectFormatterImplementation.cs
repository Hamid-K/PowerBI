using System;
using System.Collections.Generic;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200000B RID: 11
	internal class GeoJsonObjectFormatterImplementation : GeoJsonObjectFormatter
	{
		// Token: 0x06000085 RID: 133 RVA: 0x000029F3 File Offset: 0x00000BF3
		public GeoJsonObjectFormatterImplementation(SpatialImplementation creator)
		{
			this.creator = creator;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002A04 File Offset: 0x00000C04
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

		// Token: 0x06000087 RID: 135 RVA: 0x00002A80 File Offset: 0x00000C80
		public override IDictionary<string, object> Write(ISpatial value)
		{
			GeoJsonObjectWriter geoJsonObjectWriter = new GeoJsonObjectWriter();
			value.SendTo(new ForwardingSegment(geoJsonObjectWriter));
			return geoJsonObjectWriter.JsonObject;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002AAA File Offset: 0x00000CAA
		public override SpatialPipeline CreateWriter(IGeoJsonWriter writer)
		{
			return new ForwardingSegment(new WrappedGeoJsonWriter(writer));
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002ABC File Offset: 0x00000CBC
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

		// Token: 0x0400000D RID: 13
		private readonly SpatialImplementation creator;

		// Token: 0x0400000E RID: 14
		private SpatialBuilder builder;

		// Token: 0x0400000F RID: 15
		private SpatialPipeline parsePipeline;
	}
}
