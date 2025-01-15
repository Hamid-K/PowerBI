using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x0200004D RID: 77
	internal class GeographyBuilderImplementation : GeographyPipeline, IGeographyProvider
	{
		// Token: 0x06000237 RID: 567 RVA: 0x00005ACB File Offset: 0x00003CCB
		public GeographyBuilderImplementation(SpatialImplementation creator)
		{
			this.builder = new GeographyBuilderImplementation.GeographyTreeBuilder(creator);
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000238 RID: 568 RVA: 0x00005ADF File Offset: 0x00003CDF
		// (remove) Token: 0x06000239 RID: 569 RVA: 0x00005AED File Offset: 0x00003CED
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

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00005AFB File Offset: 0x00003CFB
		public Geography ConstructedGeography
		{
			get
			{
				return this.builder.ConstructedInstance;
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00005B08 File Offset: 0x00003D08
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "ForwardingSegment does the validation")]
		public override void LineTo(GeographyPosition position)
		{
			this.builder.LineTo(position.Latitude, position.Longitude, position.Z, position.M);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00005B2D File Offset: 0x00003D2D
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "ForwardingSegment does the validation")]
		public override void BeginFigure(GeographyPosition position)
		{
			this.builder.BeginFigure(position.Latitude, position.Longitude, position.Z, position.M);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00005B52 File Offset: 0x00003D52
		public override void BeginGeography(SpatialType type)
		{
			this.builder.BeginGeo(type);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00005B60 File Offset: 0x00003D60
		public override void EndFigure()
		{
			this.builder.EndFigure();
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00005B6D File Offset: 0x00003D6D
		public override void EndGeography()
		{
			this.builder.EndGeo();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00005B7A File Offset: 0x00003D7A
		public override void Reset()
		{
			this.builder.Reset();
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00005B87 File Offset: 0x00003D87
		public override void SetCoordinateSystem(CoordinateSystem coordinateSystem)
		{
			Util.CheckArgumentNull(coordinateSystem, "coordinateSystem");
			this.builder.SetCoordinateSystem(coordinateSystem.EpsgId);
		}

		// Token: 0x0400005C RID: 92
		private readonly SpatialTreeBuilder<Geography> builder;

		// Token: 0x0200008C RID: 140
		private class GeographyTreeBuilder : SpatialTreeBuilder<Geography>
		{
			// Token: 0x0600038F RID: 911 RVA: 0x000083A9 File Offset: 0x000065A9
			public GeographyTreeBuilder(SpatialImplementation creator)
			{
				Util.CheckArgumentNull(creator, "creator");
				this.creator = creator;
			}

			// Token: 0x06000390 RID: 912 RVA: 0x000083C3 File Offset: 0x000065C3
			internal override void SetCoordinateSystem(int? epsgId)
			{
				this.currentCoordinateSystem = CoordinateSystem.Geography(epsgId);
			}

			// Token: 0x06000391 RID: 913 RVA: 0x000083D1 File Offset: 0x000065D1
			protected override Geography CreatePoint(bool isEmpty, double x, double y, double? z, double? m)
			{
				if (!isEmpty)
				{
					return new GeographyPointImplementation(this.currentCoordinateSystem, this.creator, x, y, z, m);
				}
				return new GeographyPointImplementation(this.currentCoordinateSystem, this.creator);
			}

			// Token: 0x06000392 RID: 914 RVA: 0x00008400 File Offset: 0x00006600
			protected override Geography CreateShapeInstance(SpatialType type, IEnumerable<Geography> spatialData)
			{
				switch (type)
				{
				case SpatialType.LineString:
					return new GeographyLineStringImplementation(this.currentCoordinateSystem, this.creator, spatialData.Cast<GeographyPoint>().ToArray<GeographyPoint>());
				case SpatialType.Polygon:
					return new GeographyPolygonImplementation(this.currentCoordinateSystem, this.creator, spatialData.Cast<GeographyLineString>().ToArray<GeographyLineString>());
				case SpatialType.MultiPoint:
					return new GeographyMultiPointImplementation(this.currentCoordinateSystem, this.creator, spatialData.Cast<GeographyPoint>().ToArray<GeographyPoint>());
				case SpatialType.MultiLineString:
					return new GeographyMultiLineStringImplementation(this.currentCoordinateSystem, this.creator, spatialData.Cast<GeographyLineString>().ToArray<GeographyLineString>());
				case SpatialType.MultiPolygon:
					return new GeographyMultiPolygonImplementation(this.currentCoordinateSystem, this.creator, spatialData.Cast<GeographyPolygon>().ToArray<GeographyPolygon>());
				case SpatialType.Collection:
					return new GeographyCollectionImplementation(this.currentCoordinateSystem, this.creator, spatialData.ToArray<Geography>());
				case SpatialType.FullGlobe:
					return new GeographyFullGlobeImplementation(this.currentCoordinateSystem, this.creator);
				}
				return null;
			}

			// Token: 0x04000131 RID: 305
			private readonly SpatialImplementation creator;

			// Token: 0x04000132 RID: 306
			private CoordinateSystem currentCoordinateSystem;
		}
	}
}
