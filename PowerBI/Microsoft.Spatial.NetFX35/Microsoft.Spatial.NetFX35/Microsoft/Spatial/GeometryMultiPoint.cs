using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x0200002A RID: 42
	public abstract class GeometryMultiPoint : GeometryCollection
	{
		// Token: 0x0600013B RID: 315 RVA: 0x00004040 File Offset: 0x00002240
		protected GeometryMultiPoint(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600013C RID: 316
		public abstract ReadOnlyCollection<GeometryPoint> Points { get; }

		// Token: 0x0600013D RID: 317 RVA: 0x0000404C File Offset: 0x0000224C
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeometryMultiPoint other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return Enumerable.SequenceEqual<GeometryPoint>(this.Points, other.Points);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004083 File Offset: 0x00002283
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeometryMultiPoint);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004091 File Offset: 0x00002291
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeometryPoint>(base.CoordinateSystem, this.Points);
		}
	}
}
