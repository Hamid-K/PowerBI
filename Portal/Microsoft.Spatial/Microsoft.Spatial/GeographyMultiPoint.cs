using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000016 RID: 22
	public abstract class GeographyMultiPoint : GeographyCollection
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x0000364C File Offset: 0x0000184C
		protected GeographyMultiPoint(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000E1 RID: 225
		public abstract ReadOnlyCollection<GeographyPoint> Points { get; }

		// Token: 0x060000E2 RID: 226 RVA: 0x000036B8 File Offset: 0x000018B8
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
		public bool Equals(GeographyMultiPoint other)
		{
			bool? flag = base.BaseEquals(other);
			if (flag == null)
			{
				return this.Points.SequenceEqual(other.Points);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000036EF File Offset: 0x000018EF
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GeographyMultiPoint);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000036FD File Offset: 0x000018FD
		public override int GetHashCode()
		{
			return Geography.ComputeHashCodeFor<GeographyPoint>(base.CoordinateSystem, this.Points);
		}
	}
}
