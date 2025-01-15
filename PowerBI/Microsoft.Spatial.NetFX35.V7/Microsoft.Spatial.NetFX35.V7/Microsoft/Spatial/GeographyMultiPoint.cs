using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000014 RID: 20
	public abstract class GeographyMultiPoint : GeographyCollection
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00002EA4 File Offset: 0x000010A4
		protected GeographyMultiPoint(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000A2 RID: 162
		public abstract ReadOnlyCollection<GeographyPoint> Points { get; }

		// Token: 0x060000A3 RID: 163 RVA: 0x00002F10 File Offset: 0x00001110
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

		// Token: 0x060000A4 RID: 164 RVA: 0x00002F47 File Offset: 0x00001147
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeographyMultiPoint);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00002F55 File Offset: 0x00001155
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeographyPoint>(base.CoordinateSystem, this.Points);
		}
	}
}
