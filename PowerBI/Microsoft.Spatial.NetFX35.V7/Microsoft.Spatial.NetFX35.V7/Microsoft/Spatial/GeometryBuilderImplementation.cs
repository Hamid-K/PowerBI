using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000049 RID: 73
	internal class GeometryBuilderImplementation : GeometryPipeline, IGeometryProvider
	{
		// Token: 0x060001CC RID: 460 RVA: 0x00004EDD File Offset: 0x000030DD
		public GeometryBuilderImplementation(SpatialImplementation creator)
		{
			this.builder = new GeometryBuilderImplementation.GeometryTreeBuilder(creator);
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060001CD RID: 461 RVA: 0x00004EF1 File Offset: 0x000030F1
		// (remove) Token: 0x060001CE RID: 462 RVA: 0x00004EFF File Offset: 0x000030FF
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

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00004F0D File Offset: 0x0000310D
		public Geometry ConstructedGeometry
		{
			get
			{
				return this.builder.ConstructedInstance;
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00004F1A File Offset: 0x0000311A
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "ForwardingSegment does the validation")]
		public override void LineTo(GeometryPosition position)
		{
			this.builder.LineTo(position.X, position.Y, position.Z, position.M);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00004F3F File Offset: 0x0000313F
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "ForwardingSegment does the validation")]
		public override void BeginFigure(GeometryPosition position)
		{
			this.builder.BeginFigure(position.X, position.Y, position.Z, position.M);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00004F64 File Offset: 0x00003164
		public override void BeginGeometry(SpatialType type)
		{
			this.builder.BeginGeo(type);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00004F72 File Offset: 0x00003172
		public override void EndFigure()
		{
			this.builder.EndFigure();
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00004F7F File Offset: 0x0000317F
		public override void EndGeometry()
		{
			this.builder.EndGeo();
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00004F8C File Offset: 0x0000318C
		public override void Reset()
		{
			this.builder.Reset();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00004F99 File Offset: 0x00003199
		public override void SetCoordinateSystem(CoordinateSystem coordinateSystem)
		{
			Util.CheckArgumentNull(coordinateSystem, "coordinateSystem");
			this.builder.SetCoordinateSystem(coordinateSystem.EpsgId);
		}

		// Token: 0x04000050 RID: 80
		private readonly SpatialTreeBuilder<Geometry> builder;

		// Token: 0x02000081 RID: 129
		private class GeometryTreeBuilder : SpatialTreeBuilder<Geometry>
		{
			// Token: 0x0600030B RID: 779 RVA: 0x0000779A File Offset: 0x0000599A
			public GeometryTreeBuilder(SpatialImplementation creator)
			{
				Util.CheckArgumentNull(creator, "creator");
				this.creator = creator;
			}

			// Token: 0x0600030C RID: 780 RVA: 0x000077B4 File Offset: 0x000059B4
			internal override void SetCoordinateSystem(int? epsgId)
			{
				this.buildCoordinateSystem = CoordinateSystem.Geometry(epsgId);
			}

			// Token: 0x0600030D RID: 781 RVA: 0x000077C2 File Offset: 0x000059C2
			protected override Geometry CreatePoint(bool isEmpty, double x, double y, double? z, double? m)
			{
				if (!isEmpty)
				{
					return new GeometryPointImplementation(this.buildCoordinateSystem, this.creator, x, y, z, m);
				}
				return new GeometryPointImplementation(this.buildCoordinateSystem, this.creator);
			}

			// Token: 0x0600030E RID: 782 RVA: 0x000077F0 File Offset: 0x000059F0
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

			// Token: 0x04000117 RID: 279
			private readonly SpatialImplementation creator;

			// Token: 0x04000118 RID: 280
			private CoordinateSystem buildCoordinateSystem;
		}
	}
}
