using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000017 RID: 23
	public abstract class GeographyPoint : Geography
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00002E38 File Offset: 0x00001038
		protected GeographyPoint(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000AD RID: 173
		public abstract double Latitude { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000AE RID: 174
		public abstract double Longitude { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000AF RID: 175
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z is meaningful")]
		public abstract double? Z { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000B0 RID: 176
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "m is meaningful")]
		public abstract double? M { get; }

		// Token: 0x060000B1 RID: 177 RVA: 0x00002FCC File Offset: 0x000011CC
		public static GeographyPoint Create(double latitude, double longitude)
		{
			return GeographyPoint.Create(CoordinateSystem.DefaultGeography, latitude, longitude, default(double?), default(double?));
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00002FF8 File Offset: 0x000011F8
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z is meaningful")]
		public static GeographyPoint Create(double latitude, double longitude, double? z)
		{
			return GeographyPoint.Create(CoordinateSystem.DefaultGeography, latitude, longitude, z, default(double?));
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000301B File Offset: 0x0000121B
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
		public static GeographyPoint Create(double latitude, double longitude, double? z, double? m)
		{
			return GeographyPoint.Create(CoordinateSystem.DefaultGeography, latitude, longitude, z, m);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000302C File Offset: 0x0000122C
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

		// Token: 0x060000B5 RID: 181 RVA: 0x0000307C File Offset: 0x0000127C
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeographyPoint other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return this.Latitude == other.Latitude && this.Longitude == other.Longitude && this.Z == other.Z && this.M == other.M;
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003126 File Offset: 0x00001326
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeographyPoint);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003134 File Offset: 0x00001334
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<double>(base.CoordinateSystem, new double[]
			{
				this.IsEmpty ? 0.0 : this.Latitude,
				this.IsEmpty ? 0.0 : this.Longitude,
				this.Z ?? 0.0,
				this.M ?? 0.0
			});
		}
	}
}
