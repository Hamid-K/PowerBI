using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000048 RID: 72
	internal class GeographyBuilderImplementation : GeographyPipeline, IGeographyProvider
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x00004E03 File Offset: 0x00003003
		public GeographyBuilderImplementation(SpatialImplementation creator)
		{
			this.builder = new GeographyBuilderImplementation.GeographyTreeBuilder(creator);
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060001C2 RID: 450 RVA: 0x00004E17 File Offset: 0x00003017
		// (remove) Token: 0x060001C3 RID: 451 RVA: 0x00004E25 File Offset: 0x00003025
		[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Not following the event-handler pattern")]
		public event Action<Geography> ProduceGeography
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
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00004E33 File Offset: 0x00003033
		public Geography ConstructedGeography
		{
			get
			{
				return this.builder.ConstructedInstance;
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00004E40 File Offset: 0x00003040
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "ForwardingSegment does the validation")]
		public override void LineTo(GeographyPosition position)
		{
			this.builder.LineTo(position.Latitude, position.Longitude, position.Z, position.M);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00004E65 File Offset: 0x00003065
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "ForwardingSegment does the validation")]
		public override void BeginFigure(GeographyPosition position)
		{
			this.builder.BeginFigure(position.Latitude, position.Longitude, position.Z, position.M);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00004E8A File Offset: 0x0000308A
		public override void BeginGeography(SpatialType type)
		{
			this.builder.BeginGeo(type);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00004E98 File Offset: 0x00003098
		public override void EndFigure()
		{
			this.builder.EndFigure();
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00004EA5 File Offset: 0x000030A5
		public override void EndGeography()
		{
			this.builder.EndGeo();
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00004EB2 File Offset: 0x000030B2
		public override void Reset()
		{
			this.builder.Reset();
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00004EBF File Offset: 0x000030BF
		public override void SetCoordinateSystem(CoordinateSystem coordinateSystem)
		{
			Util.CheckArgumentNull(coordinateSystem, "coordinateSystem");
			this.builder.SetCoordinateSystem(coordinateSystem.EpsgId);
		}

		// Token: 0x0400004F RID: 79
		private readonly SpatialTreeBuilder<Geography> builder;

		// Token: 0x02000080 RID: 128
		private class GeographyTreeBuilder : SpatialTreeBuilder<Geography>
		{
			// Token: 0x06000307 RID: 775 RVA: 0x00007645 File Offset: 0x00005845
			public GeographyTreeBuilder(SpatialImplementation creator)
			{
				Util.CheckArgumentNull(creator, "creator");
				this.creator = creator;
			}

			// Token: 0x06000308 RID: 776 RVA: 0x0000765F File Offset: 0x0000585F
			internal override void SetCoordinateSystem(int? epsgId)
			{
				this.currentCoordinateSystem = CoordinateSystem.Geography(epsgId);
			}

			// Token: 0x06000309 RID: 777 RVA: 0x0000766D File Offset: 0x0000586D
			protected override Geography CreatePoint(bool isEmpty, double x, double y, double? z, double? m)
			{
				if (!isEmpty)
				{
					return new GeographyPointImplementation(this.currentCoordinateSystem, this.creator, x, y, z, m);
				}
				return new GeographyPointImplementation(this.currentCoordinateSystem, this.creator);
			}

			// Token: 0x0600030A RID: 778 RVA: 0x0000769C File Offset: 0x0000589C
			protected override Geography CreateShapeInstance(SpatialType type, IEnumerable<Geography> spatialData)
			{
				switch (type)
				{
				case SpatialType.LineString:
					return new GeographyLineStringImplementation(this.currentCoordinateSystem, this.creator, Enumerable.ToArray<GeographyPoint>(Enumerable.Cast<GeographyPoint>(spatialData)));
				case SpatialType.Polygon:
					return new GeographyPolygonImplementation(this.currentCoordinateSystem, this.creator, Enumerable.ToArray<GeographyLineString>(Enumerable.Cast<GeographyLineString>(spatialData)));
				case SpatialType.MultiPoint:
					return new GeographyMultiPointImplementation(this.currentCoordinateSystem, this.creator, Enumerable.ToArray<GeographyPoint>(Enumerable.Cast<GeographyPoint>(spatialData)));
				case SpatialType.MultiLineString:
					return new GeographyMultiLineStringImplementation(this.currentCoordinateSystem, this.creator, Enumerable.ToArray<GeographyLineString>(Enumerable.Cast<GeographyLineString>(spatialData)));
				case SpatialType.MultiPolygon:
					return new GeographyMultiPolygonImplementation(this.currentCoordinateSystem, this.creator, Enumerable.ToArray<GeographyPolygon>(Enumerable.Cast<GeographyPolygon>(spatialData)));
				case SpatialType.Collection:
					return new GeographyCollectionImplementation(this.currentCoordinateSystem, this.creator, Enumerable.ToArray<Geography>(spatialData));
				case SpatialType.FullGlobe:
					return new GeographyFullGlobeImplementation(this.currentCoordinateSystem, this.creator);
				}
				return null;
			}

			// Token: 0x04000115 RID: 277
			private readonly SpatialImplementation creator;

			// Token: 0x04000116 RID: 278
			private CoordinateSystem currentCoordinateSystem;
		}
	}
}
