using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000022 RID: 34
	public abstract class GeometryMultiPoint : GeometryCollection
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x0000344C File Offset: 0x0000164C
		protected GeometryMultiPoint(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000E6 RID: 230
		public abstract ReadOnlyCollection<GeometryPoint> Points { get; }

		// Token: 0x060000E7 RID: 231 RVA: 0x000034B8 File Offset: 0x000016B8
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeometryMultiPoint other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return Enumerable.SequenceEqual<GeometryPoint>(this.Points, other.Points);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000034EF File Offset: 0x000016EF
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeometryMultiPoint);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000034FD File Offset: 0x000016FD
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeometryPoint>(base.CoordinateSystem, this.Points);
		}
	}
}
