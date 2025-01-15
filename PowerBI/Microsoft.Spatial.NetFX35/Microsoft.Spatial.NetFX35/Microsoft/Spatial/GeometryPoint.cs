using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x0200002D RID: 45
	public abstract class GeometryPoint : Geometry
	{
		// Token: 0x06000146 RID: 326 RVA: 0x00004110 File Offset: 0x00002310
		protected GeometryPoint(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000147 RID: 327
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "X is meaningful")]
		public abstract double X { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000148 RID: 328
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "Y is meaningful")]
		public abstract double Y { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000149 RID: 329
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z is meaningful")]
		public abstract double? Z { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600014A RID: 330
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "m is meaningful")]
		public abstract double? M { get; }

		// Token: 0x0600014B RID: 331 RVA: 0x0000411C File Offset: 0x0000231C
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x and y are meaningful")]
		public static GeometryPoint Create(double x, double y)
		{
			return GeometryPoint.Create(CoordinateSystem.DefaultGeometry, x, y, default(double?), default(double?));
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004148 File Offset: 0x00002348
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y and z are meaningful")]
		public static GeometryPoint Create(double x, double y, double? z)
		{
			return GeometryPoint.Create(CoordinateSystem.DefaultGeometry, x, y, z, default(double?));
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000416B File Offset: 0x0000236B
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		public static GeometryPoint Create(double x, double y, double? z, double? m)
		{
			return GeometryPoint.Create(CoordinateSystem.DefaultGeometry, x, y, z, m);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000417C File Offset: 0x0000237C
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

		// Token: 0x0600014F RID: 335 RVA: 0x000041CC File Offset: 0x000023CC
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeometryPoint other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				if (this.X == other.X && this.Y == other.Y)
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

		// Token: 0x06000150 RID: 336 RVA: 0x0000427B File Offset: 0x0000247B
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeometryPoint);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000428C File Offset: 0x0000248C
		public override int GetHashCode()
		{
			CoordinateSystem coordinateSystem = base.CoordinateSystem;
			double[] array = new double[4];
			array[0] = (this.IsEmpty ? 0.0 : this.X);
			array[1] = (this.IsEmpty ? 0.0 : this.Y);
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
