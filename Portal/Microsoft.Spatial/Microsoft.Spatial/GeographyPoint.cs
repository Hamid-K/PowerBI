using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000019 RID: 25
	public abstract class GeographyPoint : Geography
	{
		// Token: 0x060000EB RID: 235 RVA: 0x000035E0 File Offset: 0x000017E0
		protected GeographyPoint(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000EC RID: 236
		public abstract double Latitude { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000ED RID: 237
		public abstract double Longitude { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000EE RID: 238
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z is meaningful")]
		public abstract double? Z { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000EF RID: 239
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "m is meaningful")]
		public abstract double? M { get; }

		// Token: 0x060000F0 RID: 240 RVA: 0x00003774 File Offset: 0x00001974
		public static GeographyPoint Create(double latitude, double longitude)
		{
			return GeographyPoint.Create(CoordinateSystem.DefaultGeography, latitude, longitude, null, null);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000037A0 File Offset: 0x000019A0
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z is meaningful")]
		public static GeographyPoint Create(double latitude, double longitude, double? z)
		{
			return GeographyPoint.Create(CoordinateSystem.DefaultGeography, latitude, longitude, z, null);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000037C3 File Offset: 0x000019C3
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
		public static GeographyPoint Create(double latitude, double longitude, double? z, double? m)
		{
			return GeographyPoint.Create(CoordinateSystem.DefaultGeography, latitude, longitude, z, m);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000037D4 File Offset: 0x000019D4
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

		// Token: 0x060000F4 RID: 244 RVA: 0x00003824 File Offset: 0x00001A24
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

		// Token: 0x060000F5 RID: 245 RVA: 0x000038CE File Offset: 0x00001ACE
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeographyPoint);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000038DC File Offset: 0x00001ADC
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
