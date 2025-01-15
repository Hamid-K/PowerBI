using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x0200004E RID: 78
	internal class GeometryBuilderImplementation : GeometryPipeline, IGeometryProvider
	{
		// Token: 0x06000242 RID: 578 RVA: 0x00005BA5 File Offset: 0x00003DA5
		public GeometryBuilderImplementation(SpatialImplementation creator)
		{
			this.builder = new GeometryBuilderImplementation.GeometryTreeBuilder(creator);
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000243 RID: 579 RVA: 0x00005BB9 File Offset: 0x00003DB9
		// (remove) Token: 0x06000244 RID: 580 RVA: 0x00005BC7 File Offset: 0x00003DC7
		[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Not following the event-handler pattern")]
		public event Action<Geometry> ProduceGeometry
		{
			add
			{
				this.builder.ProduceInstance += value;
			}
			remove
			{
				this.builder.ProduceInstance -= value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00005BD5 File Offset: 0x00003DD5
		public Geometry ConstructedGeometry
		{
			get
			{
				return this.builder.ConstructedInstance;
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00005BE2 File Offset: 0x00003DE2
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "ForwardingSegment does the validation")]
		public override void LineTo(GeometryPosition position)
		{
			this.builder.LineTo(position.X, position.Y, position.Z, position.M);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00005C07 File Offset: 0x00003E07
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "ForwardingSegment does the validation")]
		public override void BeginFigure(GeometryPosition position)
		{
			this.builder.BeginFigure(position.X, position.Y, position.Z, position.M);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00005C2C File Offset: 0x00003E2C
		public override void BeginGeometry(SpatialType type)
		{
			this.builder.BeginGeo(type);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00005C3A File Offset: 0x00003E3A
		public override void EndFigure()
		{
			this.builder.EndFigure();
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00005C47 File Offset: 0x00003E47
		public override void EndGeometry()
		{
			this.builder.EndGeo();
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00005C54 File Offset: 0x00003E54
		public override void Reset()
		{
			this.builder.Reset();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00005C61 File Offset: 0x00003E61
		public override void SetCoordinateSystem(CoordinateSystem coordinateSystem)
		{
			Util.CheckArgumentNull(coordinateSystem, "coordinateSystem");
			this.builder.SetCoordinateSystem(coordinateSystem.EpsgId);
		}

		// Token: 0x0400005D RID: 93
		private readonly SpatialTreeBuilder<Geometry> builder;

		// Token: 0x0200008D RID: 141
		private class GeometryTreeBuilder : SpatialTreeBuilder<Geometry>
		{
			// Token: 0x06000393 RID: 915 RVA: 0x000084FE File Offset: 0x000066FE
			public GeometryTreeBuilder(SpatialImplementation creator)
			{
				Util.CheckArgumentNull(creator, "creator");
				this.creator = creator;
			}

			// Token: 0x06000394 RID: 916 RVA: 0x00008518 File Offset: 0x00006718
			internal override void SetCoordinateSystem(int? epsgId)
			{
				this.buildCoordinateSystem = CoordinateSystem.Geometry(epsgId);
			}

			// Token: 0x06000395 RID: 917 RVA: 0x00008526 File Offset: 0x00006726
			protected override Geometry CreatePoint(bool isEmpty, double x, double y, double? z, double? m)
			{
				if (!isEmpty)
				{
					return new GeometryPointImplementation(this.buildCoordinateSystem, this.creator, x, y, z, m);
				}
				return new GeometryPointImplementation(this.buildCoordinateSystem, this.creator);
			}

			// Token: 0x06000396 RID: 918 RVA: 0x00008554 File Offset: 0x00006754
			protected override Geometry CreateShapeInstance(SpatialType type, IEnumerable<Geometry> spatialData)
			{
				switch (type)
				{
				case SpatialType.LineString:
					return new GeometryLineStringImplementation(this.buildCoordinateSystem, this.creator, spatialData.Cast<GeometryPoint>().ToArray<GeometryPoint>());
				case SpatialType.Polygon:
					return new GeometryPolygonImplementation(this.buildCoordinateSystem, this.creator, spatialData.Cast<GeometryLineString>().ToArray<GeometryLineString>());
				case SpatialType.MultiPoint:
					return new GeometryMultiPointImplementation(this.buildCoordinateSystem, this.creator, spatialData.Cast<GeometryPoint>().ToArray<GeometryPoint>());
				case SpatialType.MultiLineString:
					return new GeometryMultiLineStringImplementation(this.buildCoordinateSystem, this.creator, spatialData.Cast<GeometryLineString>().ToArray<GeometryLineString>());
				case SpatialType.MultiPolygon:
					return new GeometryMultiPolygonImplementation(this.buildCoordinateSystem, this.creator, spatialData.Cast<GeometryPolygon>().ToArray<GeometryPolygon>());
				case SpatialType.Collection:
					return new GeometryCollectionImplementation(this.buildCoordinateSystem, this.creator, spatialData.ToArray<Geometry>());
				default:
					return null;
				}
			}

			// Token: 0x04000133 RID: 307
			private readonly SpatialImplementation creator;

			// Token: 0x04000134 RID: 308
			private CoordinateSystem buildCoordinateSystem;
		}
	}
}
