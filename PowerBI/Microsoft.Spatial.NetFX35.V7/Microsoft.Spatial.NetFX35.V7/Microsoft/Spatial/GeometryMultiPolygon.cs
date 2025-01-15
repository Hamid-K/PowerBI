using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000023 RID: 35
	public abstract class GeometryMultiPolygon : GeometryMultiSurface
	{
		// Token: 0x060000EA RID: 234 RVA: 0x00003510 File Offset: 0x00001710
		protected GeometryMultiPolygon(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000EB RID: 235
		public abstract ReadOnlyCollection<GeometryPolygon> Polygons { get; }

		// Token: 0x060000EC RID: 236 RVA: 0x0000351C File Offset: 0x0000171C
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

		// Token: 0x060000ED RID: 237 RVA: 0x00003553 File Offset: 0x00001753
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeometryMultiPolygon);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003561 File Offset: 0x00001761
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeometryPolygon>(base.CoordinateSystem, this.Polygons);
		}
	}
}
