using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000025 RID: 37
	public abstract class GeometryPoint : Geometry
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x00003384 File Offset: 0x00001584
		protected GeometryPoint(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000F1 RID: 241
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "X is meaningful")]
		public abstract double X { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000F2 RID: 242
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Y is meaningful")]
		public abstract double Y { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000F3 RID: 243
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z is meaningful")]
		public abstract double? Z { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000F4 RID: 244
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "m is meaningful")]
		public abstract double? M { get; }

		// Token: 0x060000F5 RID: 245 RVA: 0x00003574 File Offset: 0x00001774
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x and y are meaningful")]
		public static GeometryPoint Create(double x, double y)
		{
			return GeometryPoint.Create(CoordinateSystem.DefaultGeometry, x, y, default(double?), default(double?));
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000035A0 File Offset: 0x000017A0
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y and z are meaningful")]
		public static GeometryPoint Create(double x, double y, double? z)
		{
			return GeometryPoint.Create(CoordinateSystem.DefaultGeometry, x, y, z, default(double?));
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000035C3 File Offset: 0x000017C3
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public static GeometryPoint Create(double x, double y, double? z, double? m)
		{
			return GeometryPoint.Create(CoordinateSystem.DefaultGeometry, x, y, z, m);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000035D4 File Offset: 0x000017D4
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

		// Token: 0x060000F9 RID: 249 RVA: 0x00003624 File Offset: 0x00001824
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

		// Token: 0x060000FA RID: 250 RVA: 0x000036CE File Offset: 0x000018CE
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeometryPoint);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000036DC File Offset: 0x000018DC
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
