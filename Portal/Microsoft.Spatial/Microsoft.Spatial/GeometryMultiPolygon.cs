using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000027 RID: 39
	public abstract class GeometryMultiPolygon : GeometryMultiSurface
	{
		// Token: 0x06000156 RID: 342 RVA: 0x00004020 File Offset: 0x00002220
		protected GeometryMultiPolygon(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000157 RID: 343
		public abstract ReadOnlyCollection<GeometryPolygon> Polygons { get; }

		// Token: 0x06000158 RID: 344 RVA: 0x0000402C File Offset: 0x0000222C
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeometryMultiPolygon other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return this.Polygons.SequenceEqual(other.Polygons);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00004063 File Offset: 0x00002263
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeometryMultiPolygon);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00004071 File Offset: 0x00002271
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeometryPolygon>(base.CoordinateSystem, this.Polygons);
		}
	}
}
