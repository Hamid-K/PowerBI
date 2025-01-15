using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x0200002A RID: 42
	public abstract class GeometryPolygon : GeometrySurface
	{
		// Token: 0x06000168 RID: 360 RVA: 0x0000428C File Offset: 0x0000248C
		protected GeometryPolygon(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000169 RID: 361
		public abstract ReadOnlyCollection<GeometryLineString> Rings { get; }

		// Token: 0x0600016A RID: 362 RVA: 0x00004298 File Offset: 0x00002498
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeometryPolygon other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return this.Rings.SequenceEqual(other.Rings);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000042CF File Offset: 0x000024CF
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeometryPolygon);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000042DD File Offset: 0x000024DD
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeometryLineString>(base.CoordinateSystem, this.Rings);
		}
	}
}
