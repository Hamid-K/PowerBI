using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x0200001F RID: 31
	public abstract class GeographyMultiPoint : GeographyCollection
	{
		// Token: 0x06000106 RID: 262 RVA: 0x00003AD8 File Offset: 0x00001CD8
		protected GeographyMultiPoint(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000107 RID: 263
		public abstract ReadOnlyCollection<GeographyPoint> Points { get; }

		// Token: 0x06000108 RID: 264 RVA: 0x00003AE4 File Offset: 0x00001CE4
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeographyMultiPoint other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return Enumerable.SequenceEqual<GeographyPoint>(this.Points, other.Points);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00003B1B File Offset: 0x00001D1B
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeographyMultiPoint);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00003B29 File Offset: 0x00001D29
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeographyPoint>(base.CoordinateSystem, this.Points);
		}
	}
}
