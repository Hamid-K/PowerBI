using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000026 RID: 38
	public abstract class GeometryMultiPoint : GeometryCollection
	{
		// Token: 0x06000151 RID: 337 RVA: 0x00003F5C File Offset: 0x0000215C
		protected GeometryMultiPoint(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000152 RID: 338
		public abstract ReadOnlyCollection<GeometryPoint> Points { get; }

		// Token: 0x06000153 RID: 339 RVA: 0x00003FC8 File Offset: 0x000021C8
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeometryMultiPoint other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return this.Points.SequenceEqual(other.Points);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00003FFF File Offset: 0x000021FF
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeometryMultiPoint);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000400D File Offset: 0x0000220D
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeometryPoint>(base.CoordinateSystem, this.Points);
		}
	}
}
