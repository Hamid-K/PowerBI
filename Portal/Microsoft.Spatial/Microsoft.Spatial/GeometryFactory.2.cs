using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000022 RID: 34
	public class GeometryFactory<T> : SpatialFactory where T : Geometry
	{
		// Token: 0x0600012F RID: 303 RVA: 0x00003D2C File Offset: 0x00001F2C
		internal GeometryFactory(CoordinateSystem coordinateSystem)
		{
			SpatialBuilder spatialBuilder = SpatialBuilder.Create();
			this.provider = spatialBuilder;
			this.buildChain = SpatialValidator.Create().ChainTo(spatialBuilder).StartingLink;
			this.buildChain.SetCoordinateSystem(coordinateSystem);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00003D74 File Offset: 0x00001F74
		[SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "Operator used to build")]
		public static implicit operator T(GeometryFactory<T> factory)
		{
			if (factory != null)
			{
				return factory.Build();
			}
			return default(T);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00003D94 File Offset: 0x00001F94
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public GeometryFactory<T> Point(double x, double y, double? z, double? m)
		{
			this.BeginGeo(SpatialType.Point);
			this.LineTo(x, y, z, m);
			return this;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00003DAC File Offset: 0x00001FAC
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public GeometryFactory<T> Point(double x, double y)
		{
			return this.Point(x, y, null, null);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00003403 File Offset: 0x00001603
		public GeometryFactory<T> Point()
		{
			this.BeginGeo(SpatialType.Point);
			return this;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00003DD3 File Offset: 0x00001FD3
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public GeometryFactory<T> LineString(double x, double y, double? z, double? m)
		{
			this.BeginGeo(SpatialType.LineString);
			this.LineTo(x, y, z, m);
			return this;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00003DEC File Offset: 0x00001FEC
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public GeometryFactory<T> LineString(double x, double y)
		{
			return this.LineString(x, y, null, null);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000344B File Offset: 0x0000164B
		public GeometryFactory<T> LineString()
		{
			this.BeginGeo(SpatialType.LineString);
			return this;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00003455 File Offset: 0x00001655
		public GeometryFactory<T> Polygon()
		{
			this.BeginGeo(SpatialType.Polygon);
			return this;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000345F File Offset: 0x0000165F
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Multi is meaningful")]
		public GeometryFactory<T> MultiPoint()
		{
			this.BeginGeo(SpatialType.MultiPoint);
			return this;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00003469 File Offset: 0x00001669
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Multi is meaningful")]
		public GeometryFactory<T> MultiLineString()
		{
			this.BeginGeo(SpatialType.MultiLineString);
			return this;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00003473 File Offset: 0x00001673
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Multi is meaningful")]
		public GeometryFactory<T> MultiPolygon()
		{
			this.BeginGeo(SpatialType.MultiPolygon);
			return this;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000347D File Offset: 0x0000167D
		public GeometryFactory<T> Collection()
		{
			this.BeginGeo(SpatialType.Collection);
			return this;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00003487 File Offset: 0x00001687
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public GeometryFactory<T> Ring(double x, double y, double? z, double? m)
		{
			this.StartRing(x, y, z, m);
			return this;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00003E14 File Offset: 0x00002014
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public GeometryFactory<T> Ring(double x, double y)
		{
			return this.Ring(x, y, null, null);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000034BF File Offset: 0x000016BF
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public GeometryFactory<T> LineTo(double x, double y, double? z, double? m)
		{
			this.AddPos(x, y, z, m);
			return this;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00003E3C File Offset: 0x0000203C
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public GeometryFactory<T> LineTo(double x, double y)
		{
			return this.LineTo(x, y, null, null);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00003E63 File Offset: 0x00002063
		public T Build()
		{
			this.Finish();
			return (T)((object)this.provider.ConstructedGeometry);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00003E7B File Offset: 0x0000207B
		protected override void BeginGeo(SpatialType type)
		{
			base.BeginGeo(type);
			this.buildChain.BeginGeometry(type);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00003E90 File Offset: 0x00002090
		protected override void BeginFigure(double x, double y, double? z, double? m)
		{
			base.BeginFigure(x, y, z, m);
			this.buildChain.BeginFigure(new GeometryPosition(x, y, z, m));
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00003EB2 File Offset: 0x000020B2
		protected override void AddLine(double x, double y, double? z, double? m)
		{
			base.AddLine(x, y, z, m);
			this.buildChain.LineTo(new GeometryPosition(x, y, z, m));
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00003ED4 File Offset: 0x000020D4
		protected override void EndFigure()
		{
			base.EndFigure();
			this.buildChain.EndFigure();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00003EE7 File Offset: 0x000020E7
		protected override void EndGeo()
		{
			base.EndGeo();
			this.buildChain.EndGeometry();
		}

		// Token: 0x0400001C RID: 28
		private IGeometryProvider provider;

		// Token: 0x0400001D RID: 29
		private GeometryPipeline buildChain;
	}
}
