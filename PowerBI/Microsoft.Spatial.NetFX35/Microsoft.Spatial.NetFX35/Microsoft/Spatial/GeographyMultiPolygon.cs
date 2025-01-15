using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000021 RID: 33
	public abstract class GeographyMultiPolygon : GeographyMultiSurface
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00003B46 File Offset: 0x00001D46
		protected GeographyMultiPolygon(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600010D RID: 269
		public abstract ReadOnlyCollection<GeographyPolygon> Polygons { get; }

		// Token: 0x0600010E RID: 270 RVA: 0x00003B50 File Offset: 0x00001D50
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeographyMultiPolygon other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return Enumerable.SequenceEqual<GeographyPolygon>(this.Polygons, other.Polygons);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00003B87 File Offset: 0x00001D87
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeographyMultiPolygon);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00003B95 File Offset: 0x00001D95
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeographyPolygon>(base.CoordinateSystem, this.Polygons);
		}
	}
}
