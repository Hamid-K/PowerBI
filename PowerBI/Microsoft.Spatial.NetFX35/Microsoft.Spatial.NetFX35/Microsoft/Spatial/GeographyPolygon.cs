using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000023 RID: 35
	public abstract class GeographyPolygon : GeographySurface
	{
		// Token: 0x0600011D RID: 285 RVA: 0x00003DCA File Offset: 0x00001FCA
		protected GeographyPolygon(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600011E RID: 286
		public abstract ReadOnlyCollection<GeographyLineString> Rings { get; }

		// Token: 0x0600011F RID: 287 RVA: 0x00003DD4 File Offset: 0x00001FD4
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

		// Token: 0x06000120 RID: 288 RVA: 0x00003E0B File Offset: 0x0000200B
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeographyPolygon);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00003E19 File Offset: 0x00002019
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeographyLineString>(base.CoordinateSystem, this.Rings);
		}
	}
}
