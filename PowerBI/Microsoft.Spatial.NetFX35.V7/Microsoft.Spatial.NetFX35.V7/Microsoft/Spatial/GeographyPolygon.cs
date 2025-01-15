using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000018 RID: 24
	public abstract class GeographyPolygon : GeographySurface
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00002DE4 File Offset: 0x00000FE4
		protected GeographyPolygon(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000B9 RID: 185
		public abstract ReadOnlyCollection<GeographyLineString> Rings { get; }

		// Token: 0x060000BA RID: 186 RVA: 0x000031D4 File Offset: 0x000013D4
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeographyPolygon other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return Enumerable.SequenceEqual<GeographyLineString>(this.Rings, other.Rings);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000320B File Offset: 0x0000140B
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeographyPolygon);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003219 File Offset: 0x00001419
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeographyLineString>(base.CoordinateSystem, this.Rings);
		}
	}
}
