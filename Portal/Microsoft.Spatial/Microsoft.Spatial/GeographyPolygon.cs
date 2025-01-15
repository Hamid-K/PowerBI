using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x0200001A RID: 26
	public abstract class GeographyPolygon : GeographySurface
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x0000358E File Offset: 0x0000178E
		protected GeographyPolygon(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000F8 RID: 248
		public abstract ReadOnlyCollection<GeographyLineString> Rings { get; }

		// Token: 0x060000F9 RID: 249 RVA: 0x0000397C File Offset: 0x00001B7C
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeographyPolygon other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return this.Rings.SequenceEqual(other.Rings);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000039B3 File Offset: 0x00001BB3
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeographyPolygon);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000039C1 File Offset: 0x00001BC1
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeographyLineString>(base.CoordinateSystem, this.Rings);
		}
	}
}
