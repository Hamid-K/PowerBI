using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200004D RID: 77
	internal class GeographyBuilderImplementation : GeographyPipeline, IGeographyProvider
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x00005AAF File Offset: 0x00003CAF
		public GeographyBuilderImplementation(SpatialImplementation creator)
		{
			this.builder = new GeographyBuilderImplementation.GeographyTreeBuilder(creator);
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060001F9 RID: 505 RVA: 0x00005AC3 File Offset: 0x00003CC3
		// (remove) Token: 0x060001FA RID: 506 RVA: 0x00005AD1 File Offset: 0x00003CD1
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
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00005ADF File Offset: 0x00003CDF
		public Geography ConstructedGeography
		{
			get
			{
				return this.builder.ConstructedInstance;
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00005AEC File Offset: 0x00003CEC
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "ForwardingSegment does the validation")]
		public override void LineTo(GeographyPosition position)
		{
			this.builder.LineTo(position.Latitude, position.Longitude, position.Z, position.M);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00005B11 File Offset: 0x00003D11
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "ForwardingSegment does the validation")]
		public override void BeginFigure(GeographyPosition position)
		{
			this.builder.BeginFigure(position.Latitude, position.Longitude, position.Z, position.M);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00005B36 File Offset: 0x00003D36
		public override void BeginGeography(SpatialType type)
		{
			this.builder.BeginGeo(type);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00005B44 File Offset: 0x00003D44
		public override void EndFigure()
		{
			this.builder.EndFigure();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00005B51 File Offset: 0x00003D51
		public override void EndGeography()
		{
			this.builder.EndGeo();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00005B5E File Offset: 0x00003D5E
		public override void Reset()
		{
			this.builder.Reset();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00005B6B File Offset: 0x00003D6B
		public override void SetCoordinateSystem(CoordinateSystem coordinateSystem)
		{
			Util.CheckArgumentNull(coordinateSystem, "coordinateSystem");
			this.builder.SetCoordinateSystem(coordinateSystem.EpsgId);
		}

		// Token: 0x04000058 RID: 88
		private readonly SpatialTreeBuilder<Geography> builder;

		// Token: 0x02000050 RID: 80
		private class GeographyTreeBuilder : SpatialTreeBuilder<Geography>
		{
			// Token: 0x0600021D RID: 541 RVA: 0x00005F9C File Offset: 0x0000419C
			public GeographyTreeBuilder(SpatialImplementation creator)
			{
				Util.CheckArgumentNull(creator, "creator");
				this.creator = creator;
			}

			// Token: 0x0600021E RID: 542 RVA: 0x00005FB6 File Offset: 0x000041B6
			internal override void SetCoordinateSystem(int? epsgId)
			{
				this.currentCoordinateSystem = CoordinateSystem.Geography(epsgId);
			}

			// Token: 0x0600021F RID: 543 RVA: 0x00005FC4 File Offset: 0x000041C4
			protected override Geography CreatePoint(bool isEmpty, double x, double y, double? z, double? m)
			{
				if (!isEmpty)
				{
					return new GeographyPointImplementation(this.currentCoordinateSystem, this.creator, x, y, z, m);
				}
				return new GeographyPointImplementation(this.currentCoordinateSystem, this.creator);
			}

			// Token: 0x06000220 RID: 544 RVA: 0x00005FF4 File Offset: 0x000041F4
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

			// Token: 0x04000062 RID: 98
			private readonly SpatialImplementation creator;

			// Token: 0x04000063 RID: 99
			private CoordinateSystem currentCoordinateSystem;
		}
	}
}
