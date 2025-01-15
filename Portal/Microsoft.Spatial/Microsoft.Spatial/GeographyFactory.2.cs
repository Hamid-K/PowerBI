using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000010 RID: 16
	public class GeographyFactory<T> : SpatialFactory where T : Geography
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00003368 File Offset: 0x00001568
		internal GeographyFactory(CoordinateSystem coordinateSystem)
		{
			SpatialBuilder spatialBuilder = SpatialBuilder.Create();
			this.provider = spatialBuilder;
			this.buildChain = SpatialValidator.Create().ChainTo(spatialBuilder).StartingLink;
			this.buildChain.SetCoordinateSystem(coordinateSystem);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000033AF File Offset: 0x000015AF
		[SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		public static implicit operator T(GeographyFactory<T> factory)
		{
			if (factory == null)
			{
				throw new ArgumentNullException("factory");
			}
			return factory.Build();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000033C5 File Offset: 0x000015C5
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public GeographyFactory<T> Point(double latitude, double longitude, double? z, double? m)
		{
			this.BeginGeo(SpatialType.Point);
			this.LineTo(latitude, longitude, z, m);
			return this;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000033DC File Offset: 0x000015DC
		public GeographyFactory<T> Point(double latitude, double longitude)
		{
			return this.Point(latitude, longitude, null, null);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003403 File Offset: 0x00001603
		public GeographyFactory<T> Point()
		{
			this.BeginGeo(SpatialType.Point);
			return this;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000340D File Offset: 0x0000160D
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public GeographyFactory<T> LineString(double latitude, double longitude, double? z, double? m)
		{
			this.BeginGeo(SpatialType.LineString);
			this.LineTo(latitude, longitude, z, m);
			return this;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003424 File Offset: 0x00001624
		public GeographyFactory<T> LineString(double latitude, double longitude)
		{
			return this.LineString(latitude, longitude, null, null);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000344B File Offset: 0x0000164B
		public GeographyFactory<T> LineString()
		{
			this.BeginGeo(SpatialType.LineString);
			return this;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003455 File Offset: 0x00001655
		public GeographyFactory<T> Polygon()
		{
			this.BeginGeo(SpatialType.Polygon);
			return this;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000345F File Offset: 0x0000165F
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Multi is meaningful")]
		public GeographyFactory<T> MultiPoint()
		{
			this.BeginGeo(SpatialType.MultiPoint);
			return this;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003469 File Offset: 0x00001669
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Multi is meaningful")]
		public GeographyFactory<T> MultiLineString()
		{
			this.BeginGeo(SpatialType.MultiLineString);
			return this;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003473 File Offset: 0x00001673
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Multi is meaningful")]
		public GeographyFactory<T> MultiPolygon()
		{
			this.BeginGeo(SpatialType.MultiPolygon);
			return this;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000347D File Offset: 0x0000167D
		public GeographyFactory<T> Collection()
		{
			this.BeginGeo(SpatialType.Collection);
			return this;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003487 File Offset: 0x00001687
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public GeographyFactory<T> Ring(double latitude, double longitude, double? z, double? m)
		{
			this.StartRing(latitude, longitude, z, m);
			return this;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003498 File Offset: 0x00001698
		public GeographyFactory<T> Ring(double latitude, double longitude)
		{
			return this.Ring(latitude, longitude, null, null);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000034BF File Offset: 0x000016BF
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public GeographyFactory<T> LineTo(double latitude, double longitude, double? z, double? m)
		{
			this.AddPos(latitude, longitude, z, m);
			return this;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000034D0 File Offset: 0x000016D0
		public GeographyFactory<T> LineTo(double latitude, double longitude)
		{
			return this.LineTo(latitude, longitude, null, null);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000034F7 File Offset: 0x000016F7
		public T Build()
		{
			this.Finish();
			return (T)((object)this.provider.ConstructedGeography);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000350F File Offset: 0x0000170F
		protected override void BeginGeo(SpatialType type)
		{
			base.BeginGeo(type);
			this.buildChain.BeginGeography(type);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003524 File Offset: 0x00001724
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		protected override void BeginFigure(double latitude, double longitude, double? z, double? m)
		{
			base.BeginFigure(latitude, longitude, z, m);
			this.buildChain.BeginFigure(new GeographyPosition(latitude, longitude, z, m));
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003546 File Offset: 0x00001746
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		protected override void AddLine(double latitude, double longitude, double? z, double? m)
		{
			base.AddLine(latitude, longitude, z, m);
			this.buildChain.LineTo(new GeographyPosition(latitude, longitude, z, m));
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003568 File Offset: 0x00001768
		protected override void EndFigure()
		{
			base.EndFigure();
			this.buildChain.EndFigure();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000357B File Offset: 0x0000177B
		protected override void EndGeo()
		{
			base.EndGeo();
			this.buildChain.EndGeography();
		}

		// Token: 0x04000018 RID: 24
		private IGeographyProvider provider;

		// Token: 0x04000019 RID: 25
		private GeographyPipeline buildChain;
	}
}
