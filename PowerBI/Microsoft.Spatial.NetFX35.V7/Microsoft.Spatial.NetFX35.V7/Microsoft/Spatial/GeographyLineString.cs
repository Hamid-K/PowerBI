using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000011 RID: 17
	public abstract class GeographyLineString : GeographyCurve
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00002E42 File Offset: 0x00001042
		protected GeographyLineString(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000097 RID: 151
		public abstract ReadOnlyCollection<GeographyPoint> Points { get; }

		// Token: 0x06000098 RID: 152 RVA: 0x00002E4C File Offset: 0x0000104C
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeographyLineString other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return Enumerable.SequenceEqual<GeographyPoint>(this.Points, other.Points);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002E83 File Offset: 0x00001083
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeographyLineString);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002E91 File Offset: 0x00001091
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeographyPoint>(base.CoordinateSystem, this.Points);
		}
	}
}
