using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000022 RID: 34
	public abstract class GeographyPoint : Geography
	{
		// Token: 0x06000111 RID: 273 RVA: 0x00003BA8 File Offset: 0x00001DA8
		protected GeographyPoint(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000112 RID: 274
		public abstract double Latitude { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000113 RID: 275
		public abstract double Longitude { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000114 RID: 276
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z is meaningful")]
		public abstract double? Z { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000115 RID: 277
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "m is meaningful")]
		public abstract double? M { get; }

		// Token: 0x06000116 RID: 278 RVA: 0x00003BB4 File Offset: 0x00001DB4
		public static GeographyPoint Create(double latitude, double longitude)
		{
			return GeographyPoint.Create(CoordinateSystem.DefaultGeography, latitude, longitude, default(double?), default(double?));
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00003BE0 File Offset: 0x00001DE0
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z is meaningful")]
		public static GeographyPoint Create(double latitude, double longitude, double? z)
		{
			return GeographyPoint.Create(CoordinateSystem.DefaultGeography, latitude, longitude, z, default(double?));
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00003C03 File Offset: 0x00001E03
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
		public static GeographyPoint Create(double latitude, double longitude, double? z, double? m)
		{
			return GeographyPoint.Create(CoordinateSystem.DefaultGeography, latitude, longitude, z, m);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00003C14 File Offset: 0x00001E14
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
		public static GeographyPoint Create(CoordinateSystem coordinateSystem, double latitude, double longitude, double? z, double? m)
		{
			SpatialBuilder spatialBuilder = SpatialBuilder.Create();
			GeographyPipeline geographyPipeline = spatialBuilder.GeographyPipeline;
			geographyPipeline.SetCoordinateSystem(coordinateSystem);
			geographyPipeline.BeginGeography(SpatialType.Point);
			geographyPipeline.BeginFigure(new GeographyPosition(latitude, longitude, z, m));
			geographyPipeline.EndFigure();
			geographyPipeline.EndGeography();
			return (GeographyPoint)spatialBuilder.ConstructedGeography;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00003C64 File Offset: 0x00001E64
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeographyPoint other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				if (this.Latitude == other.Latitude && this.Longitude == other.Longitude)
				{
					double? z = this.Z;
					double? z2 = other.Z;
					if (z.GetValueOrDefault() == z2.GetValueOrDefault() && z != null == (z2 != null))
					{
						double? m = this.M;
						double? m2 = other.M;
						return m.GetValueOrDefault() == m2.GetValueOrDefault() && m != null == (m2 != null);
					}
				}
				return false;
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00003D13 File Offset: 0x00001F13
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeographyPoint);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00003D24 File Offset: 0x00001F24
		public override int GetHashCode()
		{
			CoordinateSystem coordinateSystem = base.CoordinateSystem;
			double[] array = new double[4];
			array[0] = (this.IsEmpty ? 0.0 : this.Latitude);
			array[1] = (this.IsEmpty ? 0.0 : this.Longitude);
			double[] array2 = array;
			int num = 2;
			double? z = this.Z;
			array2[num] = ((z != null) ? z.GetValueOrDefault() : 0.0);
			double[] array3 = array;
			int num2 = 3;
			double? m = this.M;
			array3[num2] = ((m != null) ? m.GetValueOrDefault() : 0.0);
			return Geography.ComputeHashCodeFor<double>(coordinateSystem, array);
		}
	}
}
