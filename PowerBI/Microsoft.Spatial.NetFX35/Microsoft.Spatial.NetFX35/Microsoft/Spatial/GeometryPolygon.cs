using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x0200002F RID: 47
	public abstract class GeometryPolygon : GeometrySurface
	{
		// Token: 0x06000153 RID: 339 RVA: 0x0000433C File Offset: 0x0000253C
		protected GeometryPolygon(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000154 RID: 340
		public abstract ReadOnlyCollection<GeometryLineString> Rings { get; }

		// Token: 0x06000155 RID: 341 RVA: 0x00004348 File Offset: 0x00002548
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeometryPolygon other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return Enumerable.SequenceEqual<GeometryLineString>(this.Rings, other.Rings);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000437F File Offset: 0x0000257F
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeometryPolygon);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000438D File Offset: 0x0000258D
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeometryLineString>(base.CoordinateSystem, this.Rings);
		}
	}
}
