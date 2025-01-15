using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000051 RID: 81
	internal class GeometryBuilderImplementation : GeometryPipeline, IGeometryProvider
	{
		// Token: 0x06000221 RID: 545 RVA: 0x000060F4 File Offset: 0x000042F4
		public GeometryBuilderImplementation(SpatialImplementation creator)
		{
			this.builder = new GeometryBuilderImplementation.GeometryTreeBuilder(creator);
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000222 RID: 546 RVA: 0x00006108 File Offset: 0x00004308
		// (remove) Token: 0x06000223 RID: 547 RVA: 0x00006116 File Offset: 0x00004316
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

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00006124 File Offset: 0x00004324
		public Geometry ConstructedGeometry
		{
			get
			{
				return this.builder.ConstructedInstance;
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00006131 File Offset: 0x00004331
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "ForwardingSegment does the validation")]
		public override void LineTo(GeometryPosition position)
		{
			this.builder.LineTo(position.X, position.Y, position.Z, position.M);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00006156 File Offset: 0x00004356
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "ForwardingSegment does the validation")]
		public override void BeginFigure(GeometryPosition position)
		{
			this.builder.BeginFigure(position.X, position.Y, position.Z, position.M);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000617B File Offset: 0x0000437B
		public override void BeginGeometry(SpatialType type)
		{
			this.builder.BeginGeo(type);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00006189 File Offset: 0x00004389
		public override void EndFigure()
		{
			this.builder.EndFigure();
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00006196 File Offset: 0x00004396
		public override void EndGeometry()
		{
			this.builder.EndGeo();
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000061A3 File Offset: 0x000043A3
		public override void Reset()
		{
			this.builder.Reset();
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000061B0 File Offset: 0x000043B0
		public override void SetCoordinateSystem(CoordinateSystem coordinateSystem)
		{
			Util.CheckArgumentNull(coordinateSystem, "coordinateSystem");
			this.builder.SetCoordinateSystem(coordinateSystem.EpsgId);
		}

		// Token: 0x04000064 RID: 100
		private readonly SpatialTreeBuilder<Geometry> builder;

		// Token: 0x02000052 RID: 82
		private class GeometryTreeBuilder : SpatialTreeBuilder<Geometry>
		{
			// Token: 0x0600022C RID: 556 RVA: 0x000061CE File Offset: 0x000043CE
			public GeometryTreeBuilder(SpatialImplementation creator)
			{
				Util.CheckArgumentNull(creator, "creator");
				this.creator = creator;
			}

			// Token: 0x0600022D RID: 557 RVA: 0x000061E8 File Offset: 0x000043E8
			internal override void SetCoordinateSystem(int? epsgId)
			{
				this.buildCoordinateSystem = CoordinateSystem.Geometry(epsgId);
			}

			// Token: 0x0600022E RID: 558 RVA: 0x000061F6 File Offset: 0x000043F6
			protected override Geometry CreatePoint(bool isEmpty, double x, double y, double? z, double? m)
			{
				if (!isEmpty)
				{
					return new GeometryPointImplementation(this.buildCoordinateSystem, this.creator, x, y, z, m);
				}
				return new GeometryPointImplementation(this.buildCoordinateSystem, this.creator);
			}

			// Token: 0x0600022F RID: 559 RVA: 0x00006224 File Offset: 0x00004424
			protected override Geometry CreateShapeInstance(SpatialType type, IEnumerable<Geometry> spatialData)
			{
				switch (type)
				{
				case SpatialType.LineString:
					return new GeometryLineStringImplementation(this.buildCoordinateSystem, this.creator, Enumerable.ToArray<GeometryPoint>(Enumerable.Cast<GeometryPoint>(spatialData)));
				case SpatialType.Polygon:
					return new GeometryPolygonImplementation(this.buildCoordinateSystem, this.creator, Enumerable.ToArray<GeometryLineString>(Enumerable.Cast<GeometryLineString>(spatialData)));
				case SpatialType.MultiPoint:
					return new GeometryMultiPointImplementation(this.buildCoordinateSystem, this.creator, Enumerable.ToArray<GeometryPoint>(Enumerable.Cast<GeometryPoint>(spatialData)));
				case SpatialType.MultiLineString:
					return new GeometryMultiLineStringImplementation(this.buildCoordinateSystem, this.creator, Enumerable.ToArray<GeometryLineString>(Enumerable.Cast<GeometryLineString>(spatialData)));
				case SpatialType.MultiPolygon:
					return new GeometryMultiPolygonImplementation(this.buildCoordinateSystem, this.creator, Enumerable.ToArray<GeometryPolygon>(Enumerable.Cast<GeometryPolygon>(spatialData)));
				case SpatialType.Collection:
					return new GeometryCollectionImplementation(this.buildCoordinateSystem, this.creator, Enumerable.ToArray<Geometry>(spatialData));
				default:
					return null;
				}
			}

			// Token: 0x04000065 RID: 101
			private readonly SpatialImplementation creator;

			// Token: 0x04000066 RID: 102
			private CoordinateSystem buildCoordinateSystem;
		}
	}
}
