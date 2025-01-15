using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000013 RID: 19
	public abstract class GeographyLineString : GeographyCurve
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x000035EA File Offset: 0x000017EA
		protected GeographyLineString(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000D6 RID: 214
		public abstract ReadOnlyCollection<GeographyPoint> Points { get; }

		// Token: 0x060000D7 RID: 215 RVA: 0x000035F4 File Offset: 0x000017F4
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeographyLineString other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return this.Points.SequenceEqual(other.Points);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000362B File Offset: 0x0000182B
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeographyLineString);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003639 File Offset: 0x00001839
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeographyPoint>(base.CoordinateSystem, this.Points);
		}
	}
}
