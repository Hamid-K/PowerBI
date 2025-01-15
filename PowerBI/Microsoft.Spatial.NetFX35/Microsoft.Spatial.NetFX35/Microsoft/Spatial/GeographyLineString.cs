using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x0200001B RID: 27
	public abstract class GeographyLineString : GeographyCurve
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x000039A6 File Offset: 0x00001BA6
		protected GeographyLineString(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000F7 RID: 247
		public abstract ReadOnlyCollection<GeographyPoint> Points { get; }

		// Token: 0x060000F8 RID: 248 RVA: 0x000039B0 File Offset: 0x00001BB0
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

		// Token: 0x060000F9 RID: 249 RVA: 0x000039E7 File Offset: 0x00001BE7
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeographyLineString);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000039F5 File Offset: 0x00001BF5
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeographyPoint>(base.CoordinateSystem, this.Points);
		}
	}
}
