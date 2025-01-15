using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000023 RID: 35
	public abstract class GeometryLineString : GeometryCurve
	{
		// Token: 0x06000146 RID: 326 RVA: 0x00003EFA File Offset: 0x000020FA
		protected GeometryLineString(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000147 RID: 327
		public abstract ReadOnlyCollection<GeometryPoint> Points { get; }

		// Token: 0x06000148 RID: 328 RVA: 0x00003F04 File Offset: 0x00002104
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeometryLineString other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return this.Points.SequenceEqual(other.Points);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00003F3B File Offset: 0x0000213B
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeometryLineString);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00003F49 File Offset: 0x00002149
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeometryPoint>(base.CoordinateSystem, this.Points);
		}
	}
}
