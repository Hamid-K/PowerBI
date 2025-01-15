using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000029 RID: 41
	public abstract class GeometryPoint : Geometry
	{
		// Token: 0x0600015C RID: 348 RVA: 0x00003B2C File Offset: 0x00001D2C
		protected GeometryPoint(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600015D RID: 349
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "X is meaningful")]
		public abstract double X { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600015E RID: 350
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Y is meaningful")]
		public abstract double Y { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600015F RID: 351
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z is meaningful")]
		public abstract double? Z { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000160 RID: 352
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "m is meaningful")]
		public abstract double? M { get; }

		// Token: 0x06000161 RID: 353 RVA: 0x00004084 File Offset: 0x00002284
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x and y are meaningful")]
		public static GeometryPoint Create(double x, double y)
		{
			return GeometryPoint.Create(CoordinateSystem.DefaultGeometry, x, y, null, null);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000040B0 File Offset: 0x000022B0
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y and z are meaningful")]
		public static GeometryPoint Create(double x, double y, double? z)
		{
			return GeometryPoint.Create(CoordinateSystem.DefaultGeometry, x, y, z, null);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000040D3 File Offset: 0x000022D3
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public static GeometryPoint Create(double x, double y, double? z, double? m)
		{
			return GeometryPoint.Create(CoordinateSystem.DefaultGeometry, x, y, z, m);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000040E4 File Offset: 0x000022E4
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public static GeometryPoint Create(CoordinateSystem coordinateSystem, double x, double y, double? z, double? m)
		{
			SpatialBuilder spatialBuilder = SpatialBuilder.Create();
			GeometryPipeline geometryPipeline = spatialBuilder.GeometryPipeline;
			geometryPipeline.SetCoordinateSystem(coordinateSystem);
			geometryPipeline.BeginGeometry(SpatialType.Point);
			geometryPipeline.BeginFigure(new GeometryPosition(x, y, z, m));
			geometryPipeline.EndFigure();
			geometryPipeline.EndGeometry();
			return (GeometryPoint)spatialBuilder.ConstructedGeometry;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00004134 File Offset: 0x00002334
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeometryPoint other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return this.X == other.X && this.Y == other.Y && this.Z == other.Z && this.M == other.M;
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000041DE File Offset: 0x000023DE
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeometryPoint);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000041EC File Offset: 0x000023EC
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<double>(base.CoordinateSystem, new double[]
			{
				this.IsEmpty ? 0.0 : this.X,
				this.IsEmpty ? 0.0 : this.Y,
				this.Z ?? 0.0,
				this.M ?? 0.0
			});
		}
	}
}
