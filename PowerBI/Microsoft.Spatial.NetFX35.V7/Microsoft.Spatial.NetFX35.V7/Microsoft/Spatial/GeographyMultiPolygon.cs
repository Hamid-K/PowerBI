using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000015 RID: 21
	public abstract class GeographyMultiPolygon : GeographyMultiSurface
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00002F68 File Offset: 0x00001168
		protected GeographyMultiPolygon(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000A7 RID: 167
		public abstract ReadOnlyCollection<GeographyPolygon> Polygons { get; }

		// Token: 0x060000A8 RID: 168 RVA: 0x00002F74 File Offset: 0x00001174
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

		// Token: 0x060000A9 RID: 169 RVA: 0x00002FAB File Offset: 0x000011AB
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeographyMultiPolygon);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00002FB9 File Offset: 0x000011B9
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeographyPolygon>(base.CoordinateSystem, this.Polygons);
		}
	}
}
