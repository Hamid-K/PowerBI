using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x0200002C RID: 44
	public abstract class GeometryMultiPolygon : GeometryMultiSurface
	{
		// Token: 0x06000141 RID: 321 RVA: 0x000040AE File Offset: 0x000022AE
		protected GeometryMultiPolygon(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000142 RID: 322
		public abstract ReadOnlyCollection<GeometryPolygon> Polygons { get; }

		// Token: 0x06000143 RID: 323 RVA: 0x000040B8 File Offset: 0x000022B8
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeometryMultiPolygon other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return Enumerable.SequenceEqual<GeometryPolygon>(this.Polygons, other.Polygons);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000040EF File Offset: 0x000022EF
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeometryMultiPolygon);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000040FD File Offset: 0x000022FD
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeometryPolygon>(base.CoordinateSystem, this.Polygons);
		}
	}
}
