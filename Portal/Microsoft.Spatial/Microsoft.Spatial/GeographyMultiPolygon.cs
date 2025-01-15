using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000017 RID: 23
	public abstract class GeographyMultiPolygon : GeographyMultiSurface
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x00003710 File Offset: 0x00001910
		protected GeographyMultiPolygon(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000E6 RID: 230
		public abstract ReadOnlyCollection<GeographyPolygon> Polygons { get; }

		// Token: 0x060000E7 RID: 231 RVA: 0x0000371C File Offset: 0x0000191C
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeographyMultiPolygon other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return this.Polygons.SequenceEqual(other.Polygons);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003753 File Offset: 0x00001953
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeographyMultiPolygon);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003761 File Offset: 0x00001961
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeographyPolygon>(base.CoordinateSystem, this.Polygons);
		}
	}
}
